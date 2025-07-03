<%@ Page Title="" Language="C#" MasterPageFile="~/Mad201.Master" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="Web.Pages.MainPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">

    <h2><asp:Localize ID="lclRestaurantListTitle" runat="server" meta:resourcekey="lclRestaurantListTitle" /></h2>
    <form id="form1" runat="server">
        <asp:Panel ID="pnlSearch" runat="server" Visible="true">
            <span class="label">
                <asp:Label ID="SearchByType" runat="server" meta:resourcekey="SearchByType"/>
            </span>
            <span class="entry">
                <asp:DropDownList id="ddlType" runat="server" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" AutoPostBack="True" CssClass="custom-dropdown"/>
            </span>
            
        </asp:Panel>

        <asp:Repeater ID="rptRestaurants" runat="server">
           <ItemTemplate>
            <asp:HyperLink 
                NavigateUrl='<%# Eval("Id", "~/Pages/Restaurants/RestaurantProductsList.aspx?id={0}") %>' 
                runat="server" 
                Style="text-decoration: none; color: inherit;">
                <div style="
                    width: 80%;
                    margin: 0 auto 20px auto;
                    background-color: #f9f9f9;
                    border: 1px solid #ddd;
                    border-radius: 10px;
                    box-shadow: 0 4px 12px rgba(0, 0, 0, 0.05);
                    padding: 20px;
                    transition: transform 0.2s;
                    cursor: pointer;
                " onmouseover="this.style.transform='scale(1.02)'" onmouseout="this.style.transform='scale(1)'">
                    <h3 style="margin-top: 0;"><%# Eval("Name") %></h3>
                    <p><strong><asp:Localize ID="lclAddress" runat="server" meta:resourcekey="lclAddress" />:</strong> <%# Eval("Address") %></p>
                    <p><strong><asp:Localize ID="lclEmail" runat="server" meta:resourcekey="lclEmail" />:</strong> <%# Eval("Email") %></p>
                    <p><strong><asp:Localize ID="lclSchedule" runat="server" meta:resourcekey="lclSchedule" />:</strong> <%# Eval("Schedule") %></p>
                    <p><strong><asp:Localize ID="lclType" runat="server" meta:resourcekey="lclType" />:</strong> <%# Eval("Type") %></p>
                </div>
            </asp:HyperLink>
        </ItemTemplate>
        </asp:Repeater>

        <asp:Button ID="btnPrev" runat="server" Text="Anterior" OnClick="btnPrev_Click" CssClass="custom-btn" />
        <asp:Button ID="btnNext" runat="server" Text="Siguiente" OnClick="btnNext_Click" CssClass="custom-btn" />
    </form>
</asp:Content>
