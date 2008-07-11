using System;
using System.Collections.Generic;
using System.Text;

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
        #region init

        public const string Author = "Authors";
        public const string Title = "Title";
        public const string Series = "Series";
        public const string Annotation = "Annotation";
        public const string Language = "Language";
        public const string Rating = "Rating";
        public const string Tags = "Tags";

        public const string DefaultSqlQuery = "SELECT * FROM Books";

        public Filter()
        {
            this.operations.Add(FilterOperation.Contains, "LIKE");
            this.operations.Add(FilterOperation.Equal, "=");
            this.operations.Add(FilterOperation.Greater, ">");
            this.operations.Add(FilterOperation.GreaterOrEqual, ">=");
            this.operations.Add(FilterOperation.Less, "<");
            this.operations.Add(FilterOperation.LessOrEquall, "<=");
            this.operations.Add(FilterOperation.NotEqual, "<>");
            this.operations.Add(FilterOperation.None, "");
        }

        #endregion

        #region public

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
            StringBuilder query = new StringBuilder();
            query.Append("SELECT * FROM Books");

            if ( IsEmpty )
                return query.ToString();

            if ( this.filters.ContainsKey(Filter.Tags) )
                query.Append(" INNER JOIN lkpBookTag ON lkpBookTag.BookId=Books.ID INNER JOIN Tags ON lkpBookTag.TagId=Tag.ID");

            query.Append(" WHERE");

            string and ="";

            foreach ( KeyValuePair<string, FieldParams> pair in this.filters )
            {
                if ( string.Compare(pair.Key, Filter.Tags) == 0 )
                {
                    // add clause for tags

                    string[] tags = ( (string[])(pair.Value.Value) );
                    if ( tags == null || tags.Length == 0 )
                        continue;

                    string or = "";
                    query.Append(" (");

                    foreach ( string tag in tags )
                    {
                        query.Append(string.Format(" {0}(Tags.Name = '{1}')", or, tag));
                        if ( or.Length == 0 )
                            or = "OR ";
                    }

                    query.Append(" )");
                }
                else
                {
                    // add clause for other fields

                    string val = (pair.Value.Operation == FilterOperation.Contains) 
                        ? string.Format("*{0}*", pair.Value.Value.ToString())
                        : pair.Value.Value.ToString();


                    query.Append(
                        string.Format(" {0}({1} {2} {3})",
                            and,
                            pair.Key,
                            this.operations[ pair.Value.Operation ],
                            ( IsTextField(pair.Key) ) ? string.Format("'{0}'", val) : val
                        ));
                }

                if ( and.Length == 0 )
                    and = "AND ";
            }

            return query.ToString();
        }

        #endregion

        #region private

        bool IsTextField(string name)
        {
            if ( string.Compare(name, "ID", true) == 0 )
                return false;

            if ( string.Compare(name, "SeriesNum", true) == 0 )
                return false;

            if ( string.Compare(name, "Rating", true) == 0 )
                return false;

            if ( string.Compare(name, "Position", true) == 0 )
                return false;

            return true;
        }


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
        Dictionary<FilterOperation, string> operations = new Dictionary<FilterOperation, string>();

        #endregion
    }
}
