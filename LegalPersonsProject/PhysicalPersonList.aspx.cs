using LegalPersonsProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace LegalPersonsProject
{
    public partial class PhysicalPersonList : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"\A\d{1,12}\Z");
            if (regex.IsMatch(SearchTextBox.Text))
            {
                SearchPhysicalPersonObjectDataSource.SelectParameters["binOrIin"].DefaultValue =
                SearchTextBox.Text;
                PhysicalPersonsGridView.DataSourceID = "SearchPhysicalPersonObjectDataSource";
                PhysicalPersonsGridView.DataBind();
            }
        }

    }
}