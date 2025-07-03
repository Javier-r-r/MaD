<%@ Page Title="" Language="C#" MasterPageFile="~/Mad201.Master" AutoEventWireup="true" CodeBehind="ClientOrdersList.aspx.cs" Inherits="Web.Pages.Orders.ClientOrdersList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server">
        <h2><asp:Localize ID="hddYourOrders" runat="server" meta:resourcekey="hddYourOrders" /></h2>

        <asp:Repeater ID="rptOrders" runat="server">
            <ItemTemplate>
                <div style="margin-bottom: 30px; border: 1px solid #ddd; padding: 20px; border-radius: 10px;">
                    <h3><asp:Localize ID="Order" runat="server" meta:resourcekey="Order" />#<%# Eval("Id") %> - <%# Eval("orderDate", "{0:dd/MM/yyyy}") %></h3>
                    <p><strong><asp:Localize ID="lblAddress" runat="server" meta:resourcekey="lblAddress" /></strong> <%# Eval("orderAddress") %></p>
                    <p><strong><asp:Localize ID="lblCard" runat="server" meta:resourcekey="lblCard" /></strong> **** **** **** <%# Eval("bankcardNumber").ToString().Substring(Eval("bankcardNumber").ToString().Length - 4) %></p>

                    <h4><asp:Localize ID="lblProducts" runat="server" meta:resourcekey="lblProducts" /></h4>
                    <asp:Repeater ID="rptCartlines" runat="server" DataSource='<%# Eval("Cartline") %>'>
                        <ItemTemplate>
                            <p><%# string.Format("{0} - {1}: {2} - {3}: {4}", Eval("productName"), GetLocalResourceObject("Quantity.Text"), Eval("uds"), GetLocalResourceObject("Price.Text"), string.Format("{0:C}", Eval("price"))) %></p>

                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <div>
        <asp:Button ID="btnPrev" runat="server" Text="Anterior" OnClick="btnPrev_Click" CssClass="custom-btn" />
        <asp:Button ID="btnNext" runat="server" Text="Siguiente" OnClick="btnNext_Click" CssClass="custom-btn" />
        </div>
        <asp:Button ID="btnBack" runat="server" CssClass="custom-btn"  PostBackUrl="~/Pages/MainPage.aspx" Text="<%$ Resources:Common, btnMainPage %>" />

    </form>
</asp:Content>
