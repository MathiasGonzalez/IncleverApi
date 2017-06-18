using Entities;
using System.Collections.Generic;

namespace PrimaryHub.Parameters
{



    #region First Snippets
    public class FirstSnippetsOut : BaseParameter
    {
        public List<Snippet> snippets { get; set; }
    }
    public class FirstSnippetsIn : BaseParameter
    {
        public string searchTerm { get; set; }  
    }
    #endregion

    #region Add Snippet
    public class AddSnippetOut : BaseParameter
    {
   
        public Snippet snippet { get; set; }
    }
    public class AddSnippetIn : BaseParameter
    {
        public Snippet snippet { get; set; }
        public Group group { get; set; }
    
    }
    #endregion

}
