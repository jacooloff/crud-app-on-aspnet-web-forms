using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LegalPersonsProject.Models
{
    public abstract class Contragent
    {
        public Guid Id { get; set; }
        public long BINorIIN { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}