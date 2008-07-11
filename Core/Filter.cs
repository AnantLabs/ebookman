using System;
using System.Collections.Generic;

namespace EBookMan
{
    public enum FilterOperation
    {
        None,
        Equal,
        Contains,
        NotEqual,
        Greater,
        GreaterOrEqual,
        Less,
        LessOrEquall
    }


    public class Filter
    {
        #region field names

        public const string Author = "Author";
        public const string Title = "Title";
        public const string Series = "Series";
        public const string Annotation = "Annotation";
        public const string Language = "Language";
        public const string Rating = "Rating";
        public const string Tags = "Tags";

        #endregion

        public void Add (string name, object value, FilterOperation op)
        {
            if (! this.filters.ContainsKey(name))
                this.filters.Add(name, new FieldParams(value, op));
            else
            {
                FieldParams rec = this.filters[name];
                rec.Value = value;
                rec.Operation = op;
            }
        }


        public bool Get(string name, out object value, out FilterOperation op)
        {
            value = null;
            op = FilterOperation.None;

            if ( this.filters.ContainsKey(name) )
            {
                FieldParams param = this.filters[ name ];
                value = param.Value;
                op = param.Operation;
                return (value != null);
            }

            return false;
        }


        public bool IsEmpty
        {
            get { return this.filters.Count == 0; }
        }


        public string GetSqlQuery()
        {
            throw new NotImplementedException();
        }

        // TODO: remove
        //public void ClearFieldFilter(string name)
        //{
        //    if ( this.filters.ContainsKey(name) )
        //        this.filters.Remove(name);
        //}


        #region private

        private struct FieldParams
        {
            public object Value;
            public FilterOperation Operation;

            public FieldParams(object value, FilterOperation op)
            {
                this.Value = value;
                this.Operation = op;
            }
        }

        Dictionary<string, FieldParams> filters = new Dictionary<string,FieldParams>();

        #endregion
    }
}
