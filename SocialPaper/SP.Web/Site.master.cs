using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Web.UI.WebControls;
using System;

namespace SP.Web.Admin
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);

            if (!this.Page.User.Identity.Name.Equals("waelharbi90@outlook.com", StringComparison.InvariantCultureIgnoreCase))
            {
                this.Page.Response.Redirect("\\");
            }
        }
    }
}