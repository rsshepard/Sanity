using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sanity.web
{
    public class serTask
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public int IdeaID { get; set; }
        public int ProjectID { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Completed { get; set; }
        public DateTime? Due { get; set; }
        public string Notes { get; set; }
    }
}