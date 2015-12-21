using GuitarStore.Logic;
using GuitarStore.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.UI.WebControls;

namespace GuitarStore
{
    public partial class ShoppingCart : System.Web.UI.Page
    {
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            ShoppingCartActions cart = new ShoppingCartActions();

            if(Data.ShoppingCartItems.Count > 0)
            {
                lblTotal.Text = String.Format("{0:c}", cart.GetTotal());
                formPanel.Visible = true;
                CheckOutButton.Visible = true;
                UpdButton.Visible = true;
            }
            else
            {
                lblTotal.Text = "No items in shopping cart, add some!";
                UpdButton.Visible = false;
                CheckOutButton.Visible = false;
                formPanel.Visible = false;
            }
        }
        #endregion

        #region GetShoppingCartItems
        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return Data.ShoppingCartItems;
        }
        #endregion

        #region UpdateCartItems
        public List<ShoppingCartItem> UpdateCartItems()
        {
            ShoppingCartActions cart = new ShoppingCartActions();

            ShoppingCartActions.ShoppingCartUpdates[] cartUpdates = new ShoppingCartActions.ShoppingCartUpdates[CartList.Rows.Count];

            for(int i = 0; i < CartList.Rows.Count; i++)
            {
                cartUpdates[i].InstrumentId = Convert.ToInt16(CartList.Rows[i].Cells[0].Text);
                
                TextBox quantityTextBox = new TextBox();
                quantityTextBox = (TextBox)CartList.Rows[i].FindControl("PurchaseQuantity");
                cartUpdates[i].PurchaseQuantity = Convert.ToInt16(quantityTextBox.Text.ToString());
            }

            cart.UpdateShoppingCart(cartUpdates);
            CartList.DataBind();
            lblTotal.Text = String.Format("{0:c}", cart.GetTotal());

            return Data.ShoppingCartItems;

        }
        #endregion

        #region CheckOutButton_Click
        protected void CheckOutButton_Click(object sender, EventArgs e)
        {
            List<ShoppingCartItem> cartItems = GetShoppingCartItems();

            StringBuilder emailSb = new StringBuilder();
            StringBuilder newPageSb = new StringBuilder();

            Order order = new Order();

            order.Name = Name.Text;
            order.Address = Address.Text;
            order.Email = Email.Text;
            order.Total = lblTotal.Text;

            string thankYouEmail = "Thank you for your purchase!\n\nPlease find all details below\n\n";
            string thankYouWebPage = "Thank you for your purchase!<br /><br/>Please find all details below<br /><br/>";
            string separator = "-----------------------------------";

            emailSb.Append(thankYouEmail);
            emailSb.Append(order.ToStringEmail());
            newPageSb.Append(thankYouWebPage);
            newPageSb.Append(order.ToStringWebPage());

            foreach(ShoppingCartItem item in cartItems)
            {
                emailSb.Append(separator).Append(Environment.NewLine).Append(item.ToStringForEmail()).Append(Environment.NewLine).Append(separator).Append(Environment.NewLine);
                newPageSb.Append(separator).Append("<br />").Append(item.ToStringForWebPage()).Append("<br />").Append(separator).Append("<br />");
            }

            MailMessage o = new MailMessage("lakistein@gmail.com", "lakistein@gmail.com", "Purchase at Guitar Store!", emailSb.ToString());
            NetworkCredential netCred = new NetworkCredential("lakistein@gmail.com", "2580159lpftww");
            SmtpClient smtpobj = new SmtpClient("smtp.gmail.com", 587);
            smtpobj.EnableSsl = true;
            smtpobj.Credentials = netCred;
            smtpobj.Send(o);

            MailAddress from = new MailAddress("lakistein@gmail.com", "Guitar Store");
            MailAddress to = new MailAddress(Email.Text, "Guitar Store");
            MailMessage o1 = new MailMessage(from, to);
            o1.Subject = "Thank you for purchase!";
            o1.Body = emailSb.ToString();

            NetworkCredential netCred1 = new NetworkCredential("lakistein@gmail.com", "2580159lpftww");
            SmtpClient smtpobj1 = new SmtpClient("smtp.gmail.com", 587);
            smtpobj1.EnableSsl = true;
            smtpobj1.Credentials = netCred1;
            smtpobj1.Send(o1);
            
            ShoppingCartActions s = new ShoppingCartActions();

            s.EmptyCart();

            Session["OrderDetails"] = newPageSb.ToString();
            Response.Redirect("ThankYou.aspx");
        }
        #endregion

        #region UpdateBtn_Click
        protected void UpdateBtn_Click(object sender, EventArgs e)
        {
            UpdateCartItems();
            Response.Redirect("~/ShoppingCart.aspx");
        }
        #endregion
    }
}