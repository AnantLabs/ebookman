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

            LoadObjects(this.appFolder);
            LoadObjects(this.appFolder + "plugins");
        }


        public static void Init()
        {
            if ( DataManager.instance == null )
                DataManager.instance = new DataManager();
        }


        public void Dispose()
        {
            // dispose the plug-ins (always disposable)

            if ( this.plugins != null )
            {
                foreach ( IPlugin plugin in this.plugins )
                    plugin.Dispose();

                this.plugins.Clear();
                this.plugins = null;
            }


            // dispose the data provider (disposable optionally)

            if ( this.providers != null )
            {
                foreach ( IDataProvider provider in this.providers )
                {
                    IDisposable o = provider as IDisposable;
                    if ( o != null )
                        o.Dispose();
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
                    if ( o != null )
                        o.Dispose();
                }

                this.formats.Clear();
                this.formats = null;
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

                    catch
                    {
                        // swallow
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
                EventHandler filterChanged = new EventHandler(OnFilterChange);

                if ( this.currLibrary != null )
                    this.currLibrary.FilterChanged -= filterChanged;

                this.currLibrary = value;

                if ( this.currLibrary != null )
                    this.currLibrary.FilterChanged += filterChanged;

                FireLibraryChange();
            }
        }


        public event EventHandler ActiveLibraryChange;


        public event EventHandler DataChange;


        /// <summary>
        /// Get the metadata and the file from the URL using
        /// provided DataProvider and add it to the current 
        /// Library. Supposedly runs in a different thread.
        /// If the provider is not specified - it finds matching one
        /// </summary>
        /// <param name="url"></param>
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


        public IBookFormat GetFormatByGuid(Guid guid)
        {
            foreach ( IBookFormat format in this.formats )
                if ( format.Guid == guid )
                    return format;

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

        #endregion

        #region private helpers and members

        private void FireLibraryChange()
        {
            EventHandler handler = this.ActiveLibraryChange;
            if ( handler != null )
                handler(this, EventArgs.Empty);

            handler = this.DataChange;
            if ( handler != null ) handler(this, EventArgs.Empty);
        }


        private void OnFilterChange (object sender, EventArgs args)
        {
            if (sender == this.currLibrary)
            {
                EventHandler handler = this.DataChange;
                if ( handler != null ) handler(this, EventArgs.Empty);
            }
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
                        if ( typeof(IPlugin).IsAssignableFrom(t) )
                        {
                            CreateObject<IPlugin>(ref this.plugins, t);
                            continue;
                        }

                        if ( typeof(IDataProvider).IsAssignableFrom(t) )
                        {
                            CreateObject<IDataProvider>(ref this.providers, t);
                            continue;
                        }

                        if ( typeof(IBookFormat).IsAssignableFrom(t) )
                        {
                            CreateObject<IBookFormat>(ref this.formats, t);
                            continue;
                        }

                        if ( typeof(IViewer).IsAssignableFrom(t) )
                        {
                            CreateObject<IViewer>(ref this.viewers, t);
                            continue;
                        }
                    }
                }

                catch
                {
                    // swallow
                }
            }
        }


        private void CreateObject<T>(ref List<T> list, Type t) where T : class
        {
            try
            {
                object o = Activator.CreateInstance(t);
                T o2 = o as T;
                if ( o2 != null )
                    list.Add(o2);
            }

            catch (Exception ex)
            {
                // swallow
            }
        }


        private static DataManager instance = null;

        private List<IPlugin> plugins;
        private List<IDataProvider> providers;
        private List<IBookFormat> formats;
        private List<IViewer> viewers;

        private Form mainWindow;
        private string appFolder;
        private ILibrary currLibrary;

        #endregion
    }
}
