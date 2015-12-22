
namespace GuitarStore.Models
{
    public class Order
    {
        #region Data
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Total { get; set; }
        #endregion

        #region ToStringEmail
        public string ToStringEmail()
        {
            return "Name: " + Name +
                "\nAddress: " + Address +
                "\nTotal: " + Total + "$\n\n\n";
        }
        #endregion

        #region ToStringWebPage
        public string ToStringWebPage()
        {
            return "Name: " + Name +
                "<br />Address: " + Address +
                "<br />Total: " + Total + "$<br /><br /><br />";
        }
        #endregion
    }
}