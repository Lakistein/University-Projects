using GuitarStore.Logic;
using System;

namespace GuitarStore
{
    public partial class AddToCart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {           
            ShoppingCartActions sc = new ShoppingCartActions();

            string Id = Request.QueryString["InstrumentId"];

            sc.AddToCart(Convert.ToInt16(Id));

            Response.Redirect("~/ShoppingCart.aspx");
        }
    }
}