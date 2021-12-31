using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogTema.Models
{
    public class DataViewModel
    {
        public List<TBLBlog> TBLBlog { get; set; }
        public List<TBLYorumlar> TBLYorumlar { get; set; }
    }
}