using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace EBookMan
{
    public class DataManager : IDisposable
    {
        #region constructors and accessor

        /// <summary>
        /// Constructor is privet to ensure the singleton pattern 
        /// </summary>
        private DataManager()
        {
            this.appFolder = IOHelper.CompletePath(Application.StartupPath);

            this.plugins = new List<IPlugin>();
            this.providers = new List<IDataProvider>();
            this.formats = new List<IBookFormat>();
            this.viewers = new List<IViewer>();
            this.libraries = new List<ILibrary>();


            // load all dynamic objects from the root and plugin subfolder

            LoadObjects(this.appFolder);
            LoadObjects(this.appFolder + "plugins");


            // select initiall the default library

            this.currLibrary = this.libraries.Find(delegate(ILibrary lib) { return lib.Guid.Equals(this.defaultLibrary); });
        }


        public static void Init()
        {
            if ( DataManager.instance == null )
                DataManager.instance = new DataManager();
        }


        public void Dispose()
        {
            // dispose the data provider (disposable optionally)

            if ( this.providers != null )
            {
                foreach ( IDataProvider provider in this.providers )
                {
                    IDisposable o = provider as IDisposable;
                    if ( o != null ) o.Dispose();
                }

                this.providers.Clear();
                this.providers = null;
            }


            // dispose formats (disposeable optionally)

            if ( this.formats != null )
            {
                foreach ( IBookFormat format in this.formats )
                {
                    IDisposable o = format as IDisposable;
                    if ( o != null ) o.Dispose();
                }

                this.formats.Clear();
                this.formats = null;
            }


            // dispose formats (disposeable optionally)

            if ( this.viewers != null )
            {
                foreach ( IViewer viewer in this.viewers)
                {
                    IDisposable o = viewer as IDisposable;
                    if ( o != null ) o.Dispose();
                }

                this.viewers.Clear();
                this.viewers = null;
            }


            // dispose formats (disposeable optionally)

            if ( this.libraries != null )
            {
                foreach ( ILibrary library in this.libraries)
                {
                    IDisposable o = library as IDisposable;
                    if ( o != null ) o.Dispose();
                }

                this.libraries.Clear();
                this.libraries = null;
            }


            // dispose the plug-ins (always disposable)

            if ( this.plugins != null )
            {
                foreach ( IPlugin plugin in this.plugins )
                    plugin.Dispose();

                this.plugins.Clear();
                this.plugins = null;
            }


            this.mainWindow = null;
            this.currLibrary = null;
            DataManager.instance = null;
        }


        public static DataManager Instance
        {
            get
            {
                if ( DataManager.instance == null )
                    throw new NullReferenceException();

                return DataManager.instance;
            }
        }

        #endregion

        #region public functions

        public Form MainWindow
        {
            set
            {
                if ( this.mainWindow != null )
                    throw new ArgumentException();

                this.mainWindow = value;

                foreach ( IPlugin plugin in this.plugins )
                {
                    try
                    {
                        plugin.RegisterMainWindowUI(this.mainWindow);
                    }

                    catch (Exception ex)
                    {
                        // swallow
                        Logger.Error("RegisterMainUI ({0}) {1}", plugin.ToString(), ex.Message);
                    }
                }
            }
        }


        /// <summary>
        /// Currently active library
        /// </summary>
        public ILibrary ActiveLibrary
        {
            get
            {
                return this.currLibrary;
            }

            set
            {
                this.currLibrary = value;
                FireLibraryChange();
            }
        }


        public event EventHandler ActiveLibraryChange;


        /// <summary>
        /// Get the metadata and the file from the URL using
        /// provided DataProvider and add it to the current 
        /// Library. Supposedly runs in a different thread.
        /// If the provider is not specified - it finds matching one
        /// </summary>
        public void AddBook(string url, IDataProvider provider, IAsyncProcessHost progress)
        {
            if ( provider == null )
            {
                // find appropriate data provider

                foreach ( IDataProvider p in this.providers )
                {
                    if ( p.CanHandle(url) )
                    {
                        provider = p;
                        break;
                    }
                }
            }

            if ( provider == null )
                return;

            Book book;

            if ( progress != null ) progress.SetInterval(0, 50);

            if ( provider.CreateBook(url, out book, progress) )
            {
                if ( progress != null )
                {
                    if ( progress.IsCancelled )
                    {
                        progress.Finish(null);
                        return;
                    }

                    progress.SetInterval(50, 100);
                }

                this.currLibrary.Add(book, progress);
            }
        }


        public IBookFormat GetFormatByPath(string path)
        {
            foreach ( IBookFormat format in this.formats )
            {
                if ( format.IsSupported(path) )
                    return format;
            }

            return null;
        }


        public List<IBookFormat> Formats
        {
            get { return this.formats; }
        }


        public List<IViewer> Viewers
        {
            get { return this.viewers; }
        }


        public List<ILibrary> Libraries
        {
            get { return this.libraries; }
        }


        public Guid DefaultLibrary
        {
            get { return this.defaultLibrary; }
        }


        public string AppFolder
        {
            get { return this.appFolder; }
        }

        #endregion

        #region private helpers and members

        private void FireLibraryChange()
        {
            EventHandler handler = this.ActiveLibraryChange;
            if ( handler != null )
                handler(this, EventArgs.Empty);
        }


        private void LoadObjects(string path)
        {
            if ( !Directory.Exists(path) )
                return;

            foreach ( string fileName in Directory.GetFiles(path, "*.dll") )
            {
                if ( fileName.EndsWith("core.dll", StringComparison.InvariantCultureIgnoreCase)
                    || fileName.EndsWith("utils.dll", StringComparison.InvariantCultureIgnoreCase) 
                    || fileName.EndsWith("zip.dll", StringComparison.InvariantCultureIgnoreCase))
                    continue;

                try
                {
                    Assembly assembly = Assembly.LoadFrom(fileName);

                    foreach ( Type t in assembly.GetTypes() )
                    {
                        if ( CreateObject(t, ref this.providers) )
                            continue;

                        if ( CreateObject(t, ref this.formats) )
                            continue;

                        if ( CreateObject(t, ref this.viewers) )
                            continue;

                        if ( CreateObject(t, ref this.libraries) )
                            continue;

                        if ( CreateObject(t, ref this.plugins) )
                            continue;
                    }
                }

                catch (Exception ex)
                {
                    // swallow
                    Logger.Warning("LoadObject: {0}", ex.InnerException.Message);
                }
            }
        }


        private bool CreateObject<T>(Type type, ref List<T> list) where T : class
        {
            // if wrong type - continue

            if ( !typeof(T).IsAssignableFrom(type) )
                return false;

            try
            {
                // create an instance and add it to the provided list

                object o = Activator.CreateInstance(type);
                T o2 = o as T;
                if ( o2 != null )
                    list.Add(o2);


                // if this is IPlugin - add it to the plugin list as well

                if ( o != null && o is IPlugin )
                    this.plugins.Add(o as IPlugin);

                return true;
            }

            catch (Exception ex)
            {
                // swallow
                Logger.Warning("CreateObject: {0}", ex.InnerException);
                return false;
            }
        }


        private static DataManager instance = null;

        private List<IPlugin> plugins;
        private List<IDataProvider> providers;
        private List<IBookFormat> formats;
        private List<IViewer> viewers;
        private List<ILibrary> libraries;

        private Form mainWindow;
        private ILibrary currLibrary;

        private readonly string appFolder;
        private readonly Guid defaultLibrary = new Guid("{B5ACDA8E-371B-4faa-95EB-9996EA5BB3D2}");

        #endregion
    }
}
