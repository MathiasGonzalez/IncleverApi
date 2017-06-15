using Entities;
using System.Collections.Generic;

namespace PrimaryHub.Parameters
{


    public class BaseParameter
    {
        public string result { get; set; }
    }

    #region First Snippets
    public class FirstSnippetsOut : BaseParameter
    {
        public List<Snippet> snippets { get; set; }
    }
    public class FirstSnippetsIn : BaseParameter
    {
        public User user { get; set; }
    }
    #endregion

    #region Add Snippet
    public class AddSnippetOut : BaseParameter
    {
        public User user { get; set; }
        public Snippet snippet { get; set; }
    }
    public class AddSnippetIn : BaseParameter
    {
        public Snippet snippet { get; set; }
        public Group group { get; set; }
        public User user { get; set; }
    }
    #endregion

}
