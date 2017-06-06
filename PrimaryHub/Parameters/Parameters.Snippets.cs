using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimaryHub.Parameters
{
    public class FirstSnippetsOut
    {
        public string result { get; set; }
        public List<Snippet> snippets { get; set; }
    }
    public class FirstSnippetsIn
    {
        public User user { get; set; }
    }

    public class AddSnippetOut
    {
        public User user { get; set; }
    }
    public class AddSnippetIn
    {
        public Snippet snippet { get; set; }
        public Group group { get; set; }
        public User user { get; set; }
    }

    
}
