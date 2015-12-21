<%@ Page Title="Instruments" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="InstrumentList.aspx.cs" Inherits="GuitarStore.InstrumentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section>
        <div style="">
            <hgroup>
                <h2><%: Page.Title %></h2>
            </hgroup>

            <asp:ListView ID="instrumentList" runat="server"
                DataKeyNames="InstrumentId" GroupItemCount="2"
                ItemType="GuitarStore.Models.Instrument" SelectMethod="GetInstruments">

                <%--If there are no elements--%>
                <EmptyDataTemplate>
                    <table>
                        <tr>
                            <td>No instruments of that type in store!</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>

                <GroupTemplate>
                    <tr id="itemPlaceholderContainer" runat="server">
                        <td id="itemPlaceholder" runat="server"></td>
                    </tr>
                </GroupTemplate>

                <%--Generate items that are requested--%>
                <ItemTemplate>
                    <td runat="server">
                        <table>
                            <tr>
                                <td>
                                    <img src="/Images/<%#:Item.InstrumentModel%>.jpg"
                                        width="300" height="150" style="border: outset;" /></a>
                                </td>
                                <td style="float: right">
                                    <span style="font-size: 40px">
                                        <%#:Item.Brand %>
                                    </span>
                                    <br />
                                    <span style="font-size: 25px">
                                        <%#:Item.InstrumentModel%>
                                    </span>
                                    </a>
                                    <br />
                                    <span>
                                        <b>Price: </b><%#:String.Format("{0:c}", Item.Price)%>
                                    </span>
                                    <br />
                                    <a href="/AddToCart.aspx?instrumentId=<%#:Item.InstrumentId %>">
                                        <span class="InstrumentListItem">
                                            <b>Add To Cart<b>
                                        </span>
                                    </a>
                                </td>
                                <hr />
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                        </p>
                    </td>
                </ItemTemplate>

                <LayoutTemplate>
                    <table style="width: 100%;">
                        <tbody>
                            <tr>
                                <td>
                                    <table id="groupPlaceholderContainer" runat="server" style="width: 100%">
                                        <tr id="groupPlaceholder"></tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                            <tr></tr>
                        </tbody>
                    </table>
                </LayoutTemplate>

            </asp:ListView>
        </div>
    </section>
</asp:Content>
