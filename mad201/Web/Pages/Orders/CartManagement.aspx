<%@ Page Title="" Language="C#" MasterPageFile="~/Mad201.Master" AutoEventWireup="true" CodeBehind="CartManagement.aspx.cs" Inherits="Web.Pages.Orders.CartManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuWelcome" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <h2><asp:Localize ID="hddYourCart" runat="server" meta:resourcekey="hddYourCart" /></h2>

        <asp:GridView ID="gvCart" runat="server" AutoGenerateColumns="False" OnRowCommand="gvCart_RowCommand" CssClass="table">
            <Columns>
                <asp:BoundField DataField="productName" meta:resourcekey='ProductHeader' />
                <asp:BoundField DataField="price" meta:resourcekey='PriceHeader' DataFormatString="{0:C}" />
                <asp:BoundField DataField="units" meta:resourcekey='unitsHeader' />

                <asp:TemplateField meta:resourcekey='ProductHeader'>
                    <ItemTemplate>
                        <asp:Button ID="btnIncrease" runat="server" CommandName="Increase" CommandArgument='<%# Eval("productName") %>' Text="+" />
                        <asp:Button ID="btnDecrease" runat="server" CommandName="Decrease" CommandArgument='<%# Eval("productName") %>' Text="-" />
                        <asp:Button ID="btnRemove" runat="server" CommandName="Remove" CommandArgument='<%# Eval("productName") %>' meta:resourcekey="btnRemove" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <asp:Label ID="lblEmptyCart" runat="server" meta:resourcekey="lblEmptyCart" Visible="false" ForeColor="Gray" />

        <asp:Label ID="lblTotal" runat="server" Font-Bold="true" Font-Size="Larger" />

        <asp:Panel ID="pnlBankcards" runat="server" Visible="false">
            <label for="ddlBankcards" meta:resourcekey="ddlBankcards" />
            <asp:DropDownList ID="ddlBankcards" runat="server" CssClass="form-control" />
        </asp:Panel>


        <div style="margin-top:20px;">
            <asp:Button ID="btnCheckout" runat="server"  CssClass="custom-btn" meta:resourcekey="btnMakeOrder" OnClick="btnCheckout_ServerClick" />
        </div>

        <div id="confirmationModal" style="display:none; position:fixed; top:0; left:0; width:100%; height:100%; 
            background-color: rgba(0,0,0,0.5); z-index:1000;">

          <div style="background:white; width:400px; margin:100px auto; padding:20px; border-radius:8px; position:relative;">
            <h3><asp:Localize ID="hddConfirmOrder" runat="server" meta:resourcekey="hddConfirmOrder" /></h3>
            <asp:Literal ID="litResumenPedido" runat="server" />
            <div style="margin-top:20px; text-align:right;">
              <asp:Button ID="btnConfirmarPedido" runat="server" meta:resourcekey="btnConfirm" OnClick="btnConfirmarPedido_Click" />
              <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:Common, btnCancel %>" OnClientClick="document.getElementById('confirmationModal').style.display='none'; return false;" />
            </div>
          </div>
        </div>

        <div>
                <asp:Button ID="btnBack" runat="server" CssClass="custom-btn" OnClientClick="history.back(); return false;" Text="<%$ Resources:Common, btnContinueShopping %>" />
        </div>
    </form>
</asp:Content>

