using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LegalPersonsProject
{
    public partial class PhysicalPersonDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["create"] == "true")
            {
                PhysicalPersonDetailsView.DefaultMode = DetailsViewMode.Insert;
            }
        }

        protected void PhysicalPersonDetailsView_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            Response.Redirect("PhysicalPersonList");
        }

        protected void PhysicalPersonDetailsView_ItemUpdated(object sender, DetailsViewUpdatedEventArgs e)
        {
            PhysicalPersonDetailsView.DataBind();
        }

        protected void PhysicalPersonDetailsView_ItemDeleted(object sender, DetailsViewDeletedEventArgs e)
        {
            PhysicalPersonDetailsView.DataBind();
        }

    }
}