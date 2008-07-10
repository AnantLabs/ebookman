using System;
using System.Drawing;
using System.Collections.Generic;

namespace EBookMan
{
    public class Book
    {
        private string title;

        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }


        private string authors;

        public string Authors
        {
            get { return this.authors; }
            set { this.authors = value; }
        }


        private string series_name;

        public string SeriesName
        {
            get { return this.series_name; }
            set { this.series_name = value; }
        }


        private byte series_num;
        
        public byte SeriesNum
        {
            get { return this.series_num; }
            set { this.series_num = value; }
        }


        private string isbn;

        public string Isbn
        {
            get { return this.isbn; }
            set { this.isbn = value; }
        }


        private byte rating;

        public byte Rating
        {
            get { return this.rating; }
            set { this.rating = value; }
        }


        private string annotation;

        public string Annotation
        {
            get { return this.annotation; }
            set { this.annotation = value; }
        }


        private Image cover_image;

        public Image CoverImage
        {
            get { return this.cover_image; }
            set { this.cover_image = value; }
        }


        private string cover_path;

        public string CoverPath
        {
            get { return this.cover_path; }
            set { this.cover_path = value; }
        }


        private Dictionary<string, object> tags;

        public void AddTag (string tag)
        {
            if ( this.tags == null ) 
                this.tags = new Dictionary<string, object>();

            this.tags.Add(tag, null);
        }

        public IEnumerable<string> Tags
        {
            get
            {
                if ( this.tags == null )
                    this.tags = new Dictionary<string, object>();

                return this.tags.Keys;
            }
        }


        private string language;

        public string Language
        {
            get { return this.language; }
            set { this.language = value; }
        }


        private BookFile[] files;

        public BookFile[] Files
        {
            get { return this.files; }
            set { this.files = value; }
        }


        private string id;

        public string ID
        {
            get { return this.id; }
            set { this.id = value; }
        }


        public bool IsValid()
        {
            return string.IsNullOrEmpty(this.title) == false && string.IsNullOrEmpty(this.authors) == false; 
        }
    }
}
