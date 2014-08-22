using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sanity.web
{

    public class serProject
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public DateTime? Created { get; set; }
        public string JobNumber { get; set; }
        public string Notes { get; set; }

    }
}