using System;
using System.IO;
using System.Xml;
using System.Drawing;
using System.Collections.Generic;
using ICSharpCode.SharpZipLib.Zip;

namespace EBookMan
{
    public class Fb2Format : IBookFormat
    {
        #region constructor

        public Fb2Format()
        {
            // fill up the genre map

            this.genres.Add("sf_history", Properties.Resource.GenreSf);
            this.genres.Add("sf_action", Properties.Resource.GenreSf);
            this.genres.Add("sf_epic", Properties.Resource.GenreSf);
            this.genres.Add("sf_heroic", Properties.Resource.GenreSf);
            this.genres.Add("sf_detective", Properties.Resource.GenreSf);
            this.genres.Add("sf_cyberpunk", Properties.Resource.GenreSf);
            this.genres.Add("sf_space", Properties.Resource.GenreSf);
            this.genres.Add("sf_social", Properties.Resource.GenreSf);
            this.genres.Add("sf_horror", Properties.Resource.GenreSf);
            this.genres.Add("sf_humor", Properties.Resource.GenreSf);
            this.genres.Add("sf_fantasy", Properties.Resource.GenreFantasy);
            this.genres.Add("sf", Properties.Resource.GenreSf);

            this.genres.Add("det_classic", Properties.Resource.GenreDetective);
            this.genres.Add("det_police", Properties.Resource.GenreDetective);
            this.genres.Add("det_action", Properties.Resource.GenreAction);
            this.genres.Add("det_irony", Properties.Resource.GenreDetective);
            this.genres.Add("det_history", Properties.Resource.GenreDetective);
            this.genres.Add("det_espionage", Properties.Resource.GenreDetective);
            this.genres.Add("det_crime", Properties.Resource.GenreDetective);
            this.genres.Add("det_political", Properties.Resource.GenreDetective);
            this.genres.Add("det_maniac", Properties.Resource.GenreDetective);
            this.genres.Add("det_hard", Properties.Resource.GenreDetective);
            this.genres.Add("detective", Properties.Resource.GenreDetective);

            this.genres.Add("thriller", Properties.Resource.GenreThriller);

            this.genres.Add("prose_classic", Properties.Resource.GenreClassic);
            this.genres.Add("prose_history", Properties.Resource.GenreHistory);
            this.genres.Add("prose_contemporary", Properties.Resource.GenreModern);
            this.genres.Add("prose_counter", Properties.Resource.GenreProse);
            this.genres.Add("prose_rus_classic", Properties.Resource.GenreClassic);
            this.genres.Add("prose_su_classics", Properties.Resource.GenreClassic);

            this.genres.Add("love_contemporary", Properties.Resource.GenreLove);
            this.genres.Add("love_history", Properties.Resource.GenreLove);
            this.genres.Add("love_detective", Properties.Resource.GenreLove);
            this.genres.Add("love_short", Properties.Resource.GenreLove);
            this.genres.Add("love_erotica", Properties.Resource.GenreErotica);

            this.genres.Add("adv_western", Properties.Resource.GenreAdventure);
            this.genres.Add("adv_history", Properties.Resource.GenreAdventure);
            this.genres.Add("adv_indian", Properties.Resource.GenreAdventure);
            this.genres.Add("adv_maritime", Properties.Resource.GenreAdventure);
            this.genres.Add("adv_geo", Properties.Resource.GenreAdventure);
            this.genres.Add("adv_animal", Properties.Resource.GenreAdventure);
            this.genres.Add("adventure", Properties.Resource.GenreAdventure);

            this.genres.Add("child_tale", Properties.Resource.GenreChild);
            this.genres.Add("child_verse", Properties.Resource.GenreChild);
            this.genres.Add("child_prose", Properties.Resource.GenreChild);
            this.genres.Add("child_sf", Properties.Resource.GenreChild);
            this.genres.Add("child_det", Properties.Resource.GenreChild);
            this.genres.Add("child_adv", Properties.Resource.GenreChild);
            this.genres.Add("child_education", Properties.Resource.GenreChild);
            this.genres.Add("children", Properties.Resource.GenreChild);

            this.genres.Add("poetry", Properties.Resource.GenrePoetry);

            this.genres.Add("dramaturgy", Properties.Resource.GenreDramaturgy);

            this.genres.Add("antique_ant", Properties.Resource.GenreAntique);
            this.genres.Add("antique_european", Properties.Resource.GenreAntique);
            this.genres.Add("antique_russian", Properties.Resource.GenreAntique);
            this.genres.Add("antique_east", Properties.Resource.GenreAntique);
            this.genres.Add("antique_myths", Properties.Resource.GenreAntique);
            this.genres.Add("antique", Properties.Resource.GenreAntique);

            this.genres.Add("sci_history", Properties.Resource.GenreHistory);
            this.genres.Add("sci_psychology", Properties.Resource.GenreSciPsycho);
            this.genres.Add("sci_culture", Properties.Resource.GenreSciSocial);
            this.genres.Add("sci_religion", Properties.Resource.GenreSciSocial);
            this.genres.Add("sci_philosophy", Properties.Resource.GenreSciSocial);
            this.genres.Add("sci_politics", Properties.Resource.GenreSciSocial);
            this.genres.Add("sci_juris", Properties.Resource.GenreSciSocial);
            this.genres.Add("sci_linguistic", Properties.Resource.GenreSciSocial);
            this.genres.Add("sci_medicine", Properties.Resource.GenreSciMedi);
            this.genres.Add("sci_phys", Properties.Resource.GenreSciNatural);
            this.genres.Add("sci_math", Properties.Resource.GenreSciNatural);
            this.genres.Add("sci_chem", Properties.Resource.GenreSciNatural);
            this.genres.Add("sci_biology", Properties.Resource.GenreSciNatural);
            this.genres.Add("sci_tech", Properties.Resource.GenreSci);
            this.genres.Add("science", Properties.Resource.GenreSci);
            this.genres.Add("comp_www", Properties.Resource.GenreComp);
            this.genres.Add("comp_programming", Properties.Resource.GenreComp);
            this.genres.Add("comp_hard", Properties.Resource.GenreComp);
            this.genres.Add("comp_soft", Properties.Resource.GenreComp);
            this.genres.Add("comp_db", Properties.Resource.GenreComp);
            this.genres.Add("comp_osnet", Properties.Resource.GenreComp);
            this.genres.Add("computers", Properties.Resource.GenreComp);

            this.genres.Add("ref_encyc", Properties.Resource.GenreRef);
            this.genres.Add("ref_dict", Properties.Resource.GenreRef);
            this.genres.Add("ref_ref", Properties.Resource.GenreRef);
            this.genres.Add("ref_guide", Properties.Resource.GenreRef);
            this.genres.Add("reference", Properties.Resource.GenreRef);

            this.genres.Add("nonf_biography", Properties.Resource.GenreProse);
            this.genres.Add("nonf_publicism", Properties.Resource.GenreModern);
            this.genres.Add("nonf_criticism", Properties.Resource.GenreModern);
            this.genres.Add("nonfiction", Properties.Resource.GenreProse);

            this.genres.Add("design", Properties.Resource.GenreSciSocial);

            this.genres.Add("religion_rel", Properties.Resource.GenreRel);
            this.genres.Add("religion_esoterics", Properties.Resource.GenreRel);
            this.genres.Add("religion_self", Properties.Resource.GenreRel);
            this.genres.Add("religion", Properties.Resource.GenreRel);

            this.genres.Add("humor_anecdote", Properties.Resource.GenreHumor);
            this.genres.Add("humor_prose", Properties.Resource.GenreHumor);
            this.genres.Add("humor_verse", Properties.Resource.GenreHumor);
            this.genres.Add("humor", Properties.Resource.GenreHumor);

            this.genres.Add("home_cooking", Properties.Resource.GenreHome);
            this.genres.Add("home_pets", Properties.Resource.GenreHome);
            this.genres.Add("home_crafts", Properties.Resource.GenreHome);
            this.genres.Add("home_entertain", Properties.Resource.GenreHome);
            this.genres.Add("home_health", Properties.Resource.GenreHome);
            this.genres.Add("home_garden", Properties.Resource.GenreHome);
            this.genres.Add("home_diy", Properties.Resource.GenreHome);
            this.genres.Add("home_sport", Properties.Resource.GenreHome);
            this.genres.Add("home_sex", Properties.Resource.GenreErotica);
            this.genres.Add("home", Properties.Resource.GenreHome);

            this.genres.Add("prose_military", Properties.Resource.GenreProse);

            this.genres.Add("love_sf", Properties.Resource.GenreLove);

            this.genres.Add("job_hunting", Properties.Resource.GenreBusiness);
            this.genres.Add("management", Properties.Resource.GenreBusiness);
            this.genres.Add("marketing", Properties.Resource.GenreBusiness);
            this.genres.Add("banking", Properties.Resource.GenreBusiness);
            this.genres.Add("stock", Properties.Resource.GenreBusiness);
            this.genres.Add("accounting", Properties.Resource.GenreBusiness);
            this.genres.Add("global_economy", Properties.Resource.GenreBusiness);
            this.genres.Add("economics", Properties.Resource.GenreBusiness);
            this.genres.Add("industries", Properties.Resource.GenreBusiness);
            this.genres.Add("org_behavior", Properties.Resource.GenreBusiness);
            this.genres.Add("personal_finance", Properties.Resource.GenreBusiness);
            this.genres.Add("real_estate", Properties.Resource.GenreBusiness);
            this.genres.Add("popular_business", Properties.Resource.GenreBusiness);
            this.genres.Add("small_business", Properties.Resource.GenreBusiness);
            this.genres.Add("paper_work", Properties.Resource.GenreBusiness);
            this.genres.Add("economics_ref", Properties.Resource.GenreBusiness);
        }
        #endregion

        #region IBookFormat Members

        public string Name
        {
            get { return Properties.Resource.NameFb2; }
        }


        public Image Icon
        {
            // TODO: implement
            get { return null;  }
        }


        public Guid Guid
        {
            get { return this.guid; }
        }


        public string[] SupportedExtensions
        {
            get { return this.extensions; }
        }


        public bool IsSupported(string path)
        {
            return path.EndsWith(".fb2", StringComparison.InvariantCultureIgnoreCase) ||
                   path.EndsWith(".fb2.zip", StringComparison.InvariantCultureIgnoreCase);
        }


        public bool GetMetaData(string path, out Book book, IAsyncProcessHost progress)
        {
            try
            {
                if ( !File.Exists(path) )
                    throw new IOException();

                if (progress != null) progress.ReportProgress(10, Properties.Resource.PromptParsingMetadata);


                // loasd the file

                Stream stream = path.EndsWith(".fb2.zip", StringComparison.InvariantCultureIgnoreCase) 
                    ? GetZippedStream(path)
                    : File.Open(path, FileMode.Open);

                if ( stream == null )
                    throw new IOException();

                XmlDocument document = new XmlDocument();
                document.Load(stream);
                stream.Close();


                // parse metadata

                if ( progress != null ) progress.ReportProgress(40, null);

                XmlNodeList nodes = document.GetElementsByTagName("title-info");
                if ( nodes.Count != 1 )
                    throw new XmlException();

                book = new Book();

                foreach ( XmlNode node in nodes[ 0 ].ChildNodes )
                {
                    if ( string.Compare(node.Name, "book-title", true) == 0 )
                    {
                        book.Title = node.InnerText;
                        continue;
                    }

                    if ( string.Compare(node.Name, "author", true) == 0 )
                    {
                        book.Authors = GetFullAuthorName(node.ChildNodes);
                        continue;
                    }

                    if ( string.Compare(node.Name, "genre", true) == 0 )
                    {
                        book.AddTag(this.genres.ContainsKey(node.InnerText) ? this.genres[node.InnerText] : node.InnerText);
                        continue;
                    }

                    if ( string.Compare(node.Name, "annotation", true) == 0 )
                    {
                        book.Annotation = CleanHtmlTags(node.InnerText);
                        continue;
                    }

                    if ( string.Compare(node.Name, "coverpage", true) == 0 )
                    {
                        book.CoverImage = GetImage(document, node);
                        continue;
                    }

                    if ( string.Compare(node.Name, "lang", true) == 0 )
                    {
                        book.Language = node.InnerText;
                        continue;
                    }

                    if ( string.Compare(node.Name, "sequence", true) == 0 )
                    {
                        book.SeriesName = node.Attributes[ "name" ].Value;
                        book.SeriesNum = byte.Parse(node.Attributes[ "number" ].Value);
                        continue;
                    }
                }


                // get isbn tag

                if ( progress != null ) progress.ReportProgress(80, null);

                XmlNodeList isbn = document.GetElementsByTagName("isbn");
                if (isbn != null && isbn.Count == 1)
                    book.Isbn = isbn[0].InnerText;


                // add files

                book.Files = new BookFile[] { new BookFile(path, this.Guid) };

                if ( progress != null ) progress.ReportProgress(100, null);

                return true;
            }

            catch ( XmlException )
            {
                if (progress != null) progress.ReportError(Properties.Resource.ErrorInvalidXml);
                book = null;
                return false;
            }

            catch ( IOException )
            {
                if (progress != null) progress.ReportError(Properties.Resource.ErrorIO);
                book = null;
                return false;
            }
        }

        #endregion

        #region Private members and methods

        private Stream GetZippedStream(string path)
        {
            using ( ZipInputStream str = new ZipInputStream(File.OpenRead(path)) )
            {
                ZipEntry en;
                while ( ( en = str.GetNextEntry() ) != null )
                {
                    if ( en.IsDirectory )
                        continue;

                    if ( !en.Name.EndsWith(".fb2", StringComparison.InvariantCultureIgnoreCase) )
                        continue;

                    byte[] data = new byte[ en.Size ];
                    str.Read(data, 0, data.Length);
                    str.Close();

                    return new MemoryStream(data);
                }
            }

            return null;
        }


        private string GetFullAuthorName(XmlNodeList nodes)
        {
            string last = "", first = "", middle = "";

            foreach ( XmlNode node in nodes )
            {
                if ( string.Compare(node.Name, "first-name", true) == 0 )
                {
                    first = node.InnerText;
                    continue;
                }

                if ( string.Compare(node.Name, "middle-name", true) == 0 )
                {
                    middle = node.InnerText;
                    continue;
                }

                if ( string.Compare(node.Name, "last-name", true) == 0 )
                {
                    last = node.InnerText;
                    continue;
                }
            }

            return string.Format("{0} {1} {2}", last, first, middle).TrimEnd(' ');
        }



        private string CleanHtmlTags(string str)
        {
            return str.Replace("<p>", "\n").Replace("<empty-line/>", "\n\n");
        }


        private Image GetImage(XmlDocument doc, XmlNode coverNode)
        {
            if (coverNode.ChildNodes.Count == 0)
                return null;

            XmlNode hrefNode = coverNode.ChildNodes[0];
            if (hrefNode.Attributes.Count == 0)
                return null;

            string name = null;

            foreach ( XmlAttribute attribute in hrefNode.Attributes )
            {
                if ( attribute.Name.ToLower().Contains("href") )
                {
                    name = attribute.Value.Trim('#', ' ');
                    break;
                }
            }

            if ( name == null )
                return null;

                            
            XmlNodeList binaries = doc.GetElementsByTagName("binary");
            if ( binaries == null )
                return null;

            foreach ( XmlNode binary in binaries )
            {
                string id = binary.Attributes[ "id" ].Value.ToString();
                if ( string.Compare(id, name, true) == 0 )
                {
                    byte[] bytes = Convert.FromBase64String(binary.InnerText);
                    if (bytes != null)
                    {
                        Image image = Image.FromStream(new MemoryStream(bytes));
                        return image;
                    }
                    else
                        return null;
                }
            }

            return null;
        }

        private readonly Guid guid = new Guid("{B5FE02DC-2BE8-49fb-8624-4EACF56F6E95}");

        private readonly string[] extensions = new string[] { "*.zip", "*.fb2" };
        private Dictionary<string, string> genres = new Dictionary<string, string>();

        #endregion
    }
}
