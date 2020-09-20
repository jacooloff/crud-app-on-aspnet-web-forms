using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace LegalPersonsProject.Models
{
    public class PhysicalPerson : Contragent
    {
        //update validation
        public PhysicalPerson(Guid id,
                                string name,
                                string secondName,
                                string lastName,
                                string binOrIin,
                                string createdAt,
                                string updatedAt,
                                string createdBy,
                                string updatedBy)
        {
            Id = id;
            Name = (string)name;
            Secondname = (string)secondName;
            Lastname = (string)lastName;
            BINorIIN = long.Parse(binOrIin);
            CreatedAt = DateTime.Parse(createdAt);
            UpdatedAt = DateTime.Parse(updatedAt);
            CreatedBy = (string)createdBy;
            UpdatedBy = (string)updatedBy;
        }
        //create validation
        public PhysicalPerson(  string name,
                                string secondName,
                                string lastName,
                                string binOrIin,
                                string createdBy)
        {
            Id = Guid.NewGuid();
            Name = (string)name;
            Secondname = (string)secondName;
            Lastname = (string)lastName;
            BINorIIN = long.Parse(binOrIin);
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            CreatedBy = (string)createdBy == null || createdBy.Trim() == ""  ? "Admin" : createdBy;
            UpdatedBy = (string)createdBy == null || createdBy.Trim() == "" ? "Admin" : createdBy;
        }

        public string Name { get; set; }

        
        public string Secondname { get; set; }

        
        public string Lastname { get; set; }
    }
}