<%@ Page Title="Shopping Cart" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShoppingCart.aspx.cs" Inherits="GuitarStore.ShoppingCart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div id="ShoppingCartTitle" runat="server">
        <h1>Shopping Cart</h1>
    </div>

    <%--Generate the table containing information about items in shopping cart--%>
    <asp:GridView ID="CartList" runat="server" AutoGenerateColumns="false" GridLines="Horizontal" CellPadding="4"
        ItemType="GuitarStore.Models.ShoppingCartItem" SelectMethod="GetShoppingCartItems">
        <Columns>
            <asp:BoundField DataField="InstrumentId" HeaderText="InstrumentId" SortExpression="InstrumentId" />
            <asp:BoundField DataField="Instrument.Brand" HeaderText="Brand" />
            <asp:BoundField DataField="Instrument.InstrumentModel" HeaderText="Model" />
            <asp:BoundField DataField="Instrument.Price" HeaderText="Price (each)" DataFormatString="{0:c}" />
            <asp:TemplateField HeaderText="Quantity">
                <ItemTemplate>
                    <asp:TextBox ID="PurchaseQuantity" Width="40" runat="server" Text="<%#: Item.Quantity %>"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item Total">
                <ItemTemplate>
                    <%#: String.Format("{0:c}", ((Convert.ToDouble(Item.Quantity)) *  Convert.ToDouble(Item.Instrument.Price)))%>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>


    <%--Label to display total amount in dollars--%>
    <div>
        <p></p>
        <strong>
            <asp:Label ID="lblTotal" runat="server" Text="Order Total: "></asp:Label>
        </strong>
    </div>
    <br />


    <%--Update button--%>
    <asp:Button ID="UpdButton" runat="server" OnClick="UpdateBtn_Click" OnClientClick="validateAmount()" Text="Update" />
    <br />
    <br />


    <%--Text boxes for order informations--%>
    <asp:Panel ID="formPanel" runat="server">
        <label>
            Name: 
            <asp:TextBox ID="Name" runat="server"></asp:TextBox>
        </label>
        <br />

        <label>
            Address: 
                    <asp:TextBox ID="Address" runat="server"></asp:TextBox></label>
        <br />

        <label>
            Telephone Number: 
                    <asp:TextBox ID="TelNumber" runat="server"></asp:TextBox>
        </label>
        <br />

        <label>
            Email: 
                    <asp:TextBox ID="Email" runat="server"></asp:TextBox>
        </label>
        <br />
    </asp:Panel>


    <%--Checkout button--%>
    <asp:Button ID="CheckOutButton" runat="server" OnClick="CheckOutButton_Click" OnClientClick="return validateTextbox()"
        Text="Checkout!" />
    <hr />


    <%--Script to validate input in text boxes above--%>
    <script type="text/javascript">
        function validateTextbox() {
            var Name = document.getElementById("<%=Name.ClientID%>").value;
            var Email = document.getElementById("<%=Email.ClientID%>").value;
            var TelNumber = document.getElementById("<%=TelNumber.ClientID%>").value;
            var Address = document.getElementById("<%=Address.ClientID%>").value;

            var tel = /[0-9]+/;
            var emailPat = /^(\".*\"|[A-Za-z]\w*)@(\[\d{1,3}(\.\d{1,3}){3}]|[A-Za-z]\w*(\.[A-Za-z]\w*)+)$/;
           
            var telMatch = TelNumber.match(tel);
            var EmailmatchArray = Email.match(emailPat);

            if (Name == "") {
                alert('Enter First Name');
                return false;
            }
            
            if (TelNumber == '' || telMatch == null || !(!isNaN(parseInt(TelNumber)) && isFinite(TelNumber))) {
                alert('Invalid phone number');
                return false;
            }

            if (EmailmatchArray == null || Email == "") {
                alert("Your email address seems incorrect. Please try again.");
                return false;
            }

            if (Address == "") {
                alert("Address field cannot be empty");
                return false;
            }
        }
    </script>

</asp:Content>
