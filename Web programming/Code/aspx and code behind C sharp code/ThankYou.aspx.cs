using System;

namespace GuitarStore
{
    public partial class ThankYou : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblOrderInfo.Text = Session["OrderDetails"].ToString();
        }
    }
}