using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LegalPersonsProject.Models;
namespace LegalPersonsProject
{
    public partial class JuridicalPersonList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"\A\d{1,12}\Z");
            if (regex.IsMatch(SearchTextBox.Text))
            {
                SearchJuridicalPersonObjectDataSource.SelectParameters["binOrIin"].DefaultValue =
                SearchTextBox.Text;
                JuridicalPersonsGridView.DataSourceID = "SearchJuridicalPersonObjectDataSource";
                JuridicalPersonsGridView.DataBind();
            }
        }

        protected void JuridicalPersonsGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            JuridicalPersonContactsObjectDataSource.SelectParameters["id"].DefaultValue =
                JuridicalPersonsGridView.SelectedDataKey.Value.ToString();
            //selecting id for delete if delete button would be pressed
            JuridicalPersonObjectDataSource.DeleteParameters["id"].DefaultValue =
                JuridicalPersonsGridView.SelectedDataKey.Value.ToString();
            JuridicalPersonContactsGridView.DataBind();
        }
    }
}