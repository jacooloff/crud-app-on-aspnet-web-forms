using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LegalPersonsProject.Models
{
    public class JuridicalPerson : Contragent
    {
        //update validation
        public JuridicalPerson(Guid id,
                                string name,
                                string binOrIin,
                                string createdAt,
                                string updatedAt,
                                string createdBy,
                                string updatedBy)
        {
            Id = id;
            Name = (string)name;
            BINorIIN = long.Parse(binOrIin);
            CreatedAt = DateTime.Parse(createdAt);
            UpdatedAt = DateTime.Parse(updatedAt);
            CreatedBy = (string)createdBy;
            UpdatedBy = (string)updatedBy;
        }
        //create validation
        public JuridicalPerson(string name,
                                string binOrIin,
                                string createdBy)
        {
            Id = Guid.NewGuid();
            Name = name.Trim();
            BINorIIN = long.Parse(binOrIin.Trim());
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            CreatedBy = createdBy == null || createdBy.Trim() == "" ? "Admin" : createdBy;
            UpdatedBy = createdBy == null || createdBy.Trim() == "" ? "Admin" : createdBy;
        }
        public string Name { get; set; }


    }
}