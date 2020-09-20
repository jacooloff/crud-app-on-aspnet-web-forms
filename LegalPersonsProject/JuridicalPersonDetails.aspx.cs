using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LegalPersonsProject
{
    public partial class JuridicalPersonDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["create"] == "true")
            {
                JuridicalPersonDetailsView.DefaultMode = DetailsViewMode.Insert;
            }
        }

        protected void JuridicalPersonDetailsView_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            Response.Redirect("JuridicalPersonList");
        }

        protected void JuridicalPersonDetailsView_ItemUpdated(object sender, DetailsViewUpdatedEventArgs e)
        {
            JuridicalPersonDetailsView.DataBind();
        }

        protected void JuridicalPersonDetailsView_ItemDeleted(object sender, DetailsViewDeletedEventArgs e)
        {
            JuridicalPersonDetailsView.DataBind();
        }

        protected void PhysicalPersonsDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ContactsObjectDataSource.InsertParameters["physicalPersonId"].DefaultValue = PhysicalPersonsDropDownList.SelectedValue;
        }

        protected void ChangeContactFormVisibility(bool toHide)
        {
            ChooseContactLabel.Visible = toHide;
            PhysicalPersonsDropDownList.Visible = toHide;
            PositionLabel.Visible = toHide;
            PositionTextBox.Visible = toHide;
            InsertContactButton.Visible = toHide;
            CreateContactButton.Visible = !toHide;
        }
        protected void CreateContactButton_Click(object sender, EventArgs e)
        {
            ChangeContactFormVisibility(true);
        }

        protected void InsertContactButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(PositionTextBox.Text.Trim()))
            {
                ContactsObjectDataSource.InsertParameters["position"].DefaultValue = PositionTextBox.Text.Trim();
                ContactsObjectDataSource.Insert();
                ContactsGridView.DataBind();
                ChangeContactFormVisibility(false);
            }
        }

        protected void JuridicalPersonDetailsView_DataBound(object sender, EventArgs e)
        {
            DataRowView view = (DataRowView)JuridicalPersonDetailsView.DataItem;
            if (view == null) CreateContactButton.Visible = false;
        }
    }
}