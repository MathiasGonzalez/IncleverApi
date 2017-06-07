using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimaryHub.Parameters
{


    public class BaseParameter
    {
        public string result { get; set; }
    }

    public class FirstSnippetsOut : BaseParameter
    {
        public List<Snippet> snippets { get; set; }
    }
    public class FirstSnippetsIn : BaseParameter
    {
        public User user { get; set; }
    }

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


}
