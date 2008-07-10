using System;

namespace EBookMan
{
    [Flags]
    public enum TextSearchLocation
    {
        Authors,
        Title,
        Series,
        Annotation
    }


    public class FilterCriteria
    {
        private string text;

        public string Text
        {
            get { return this.text; }
            set { this.text = value; }
        }


        private TextSearchLocation location;

        public TextSearchLocation TextSearchLocation
        {
            get { return this.location; }
            set { this.location = value; }
        }


        private byte rating;

        public byte Rating
        {
            get { return this.rating; }
            set { this.rating = value; }
        }


        private bool includeHigher;

        public bool IncludeHigherRating
        {
            get { return this.includeHigher; }
            set { this.includeHigher = value; }
        }


        private IBookFormat[] formats;

        public IBookFormat[] Formats
        {
            get { return formats; }
        }


        private string[] tags;

        public string[] Tags
        {
            get { return this.tags; }
        }


        private bool noFiles;

        public bool NoFiles
        {
            get { return this.noFiles;}
            set { this.noFiles = value; }
        }


        private string language;

        public string Language
        {
            get { return this.language; }
            set { this.language = value; }
        }


        public bool IsDefault(ILibrary library)
        {
            return 
                string.IsNullOrEmpty(this.text) && 
                string.IsNullOrEmpty(this.language) &&
                this.rating == 0 &&
                this.includeHigher &&
                this.noFiles &&
                this.formats != null && 
                this.formats.Length == DataManager.Instance.Formats.Count &&
                this.tags != null &&
                this.tags.Length == library.GetTags().Length;
        }


        public void Reset(ILibrary library)
        {
            this.text = null;
            this.language = null;

            this.rating = 0;
            this.includeHigher = true;

            this.noFiles = true;
            this.formats = DataManager.Instance.Formats.ToArray();;

            this.tags = library.GetTags();
        }
    }
}
