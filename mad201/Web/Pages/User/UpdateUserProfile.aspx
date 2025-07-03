<%@ Page Title="" Language="C#" MasterPageFile="~/Mad201.Master" AutoEventWireup="true" CodeBehind="UpdateUserProfile.aspx.cs" Inherits="Web.Pages.User.UpdateUserProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation"
    runat="server">
    - <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">

    <div style="text-align: right; margin-bottom: 10px; margin-right: 100px;">
        <asp:HyperLink ID="lnkToBankcards" runat="server" 
            NavigateUrl="~/Pages/User/BankcardUpdateList.aspx"
            Text="Ir a tarjetas bancarias"
            Style="background-color: #4CAF50; color: white; padding: 8px 16px; text-decoration: none; border-radius: 5px; font-weight: bold; display: inline-block;" />
    </div>
    <div id="form">
        <form id="UpdateUserProfileForm" method="POST" runat="server">
            <asp:HyperLink ID="lnkChangePassword" runat="server" 
                NavigateUrl="~/Pages/User/ChangePassword.aspx"
                meta:resourcekey="lnkChangePassword"/>
            <div class="field">
                <span class="label"><asp:Localize ID="lclFirstName" runat="server" meta:resourcekey="lclFirstName" /></span><span class="entry">
                    <asp:TextBox ID="txtFirstName" runat="server" Width="100" Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvFirstName" runat="server"
                        ControlToValidate="txtFirstName" Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"/></span>
            </div>
            <!-- Campo para Surname (solo para Client) -->
            <div class="field" runat="server" id="surnameField" visible="false">
                <span class="label"><asp:Localize ID="Localize1" runat="server" meta:resourcekey="lclSurname" /></span>
                <span class="entry">
                    <asp:TextBox ID="txtSurname" runat="server" Width="100" Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="txtSurname" Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"/>
                </span>
            </div>
            <div class="field">
                <span class="label"><asp:Localize ID="lclEmail" runat="server" meta:resourcekey="lclEmail" /></span><span class="entry">
                    <asp:TextBox ID="txtEmail" runat="server" Width="100" Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                        ControlToValidate="txtEmail" Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"/>
                    <asp:RegularExpressionValidator ID="revEmail" runat="server"
                        ControlToValidate="txtEmail" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" meta:resourcekey="revEmail"></asp:RegularExpressionValidator></span>
            </div> 
            
            <!-- Campos para Restaurant -->
            <div class="field" runat="server" id="scheduleField" visible="false">
                <span class="label"><asp:Localize ID="lclSchedule" runat="server" meta:resourcekey="lclSchedule" /></span>
                <span class="entry">
                    <asp:TextBox ID="txtSchedule" runat="server" Width="100" Columns="16"></asp:TextBox>
                </span>
            </div>

            <div class="field" runat="server" id="typeField" visible="false">
                <span class="label"><asp:Localize ID="lclType" runat="server" meta:resourcekey="lclType" /></span>
                <span class="entry">
                    <asp:TextBox ID="txtType" runat="server" Width="100" Columns="16"></asp:TextBox>
                </span>
            </div>
            <div class="field">
                <span class="label"><asp:Localize ID="lclLanguage" runat="server" meta:resourcekey="lclLanguage" /></span><span class="entry">
                    <asp:DropDownList ID="comboLanguage" runat="server" AutoPostBack="True" 
                    Width="100px" onselectedindexchanged="ComboLanguageSelectedIndexChanged">
                    </asp:DropDownList></span>
            </div>
            <div class="field">
                <span class="label"><asp:Localize ID="lclCountry" runat="server" meta:resourcekey="lclCountry" /></span><span class="entry">
                    <asp:DropDownList ID="comboCountry" runat="server" Width="100px">
                    </asp:DropDownList></span>
            </div>

           

            <div class="button">
                <asp:Button ID="btnUpdate" CssClass="custom-btn" runat="server" OnClick="BtnUpdateClick" meta:resourcekey="btnUpdate"/>
            </div>

            <div>
                <asp:Button ID="btnBack" runat="server" CssClass="custom-btn" OnClientClick="history.back(); return false;" Text="<%$ Resources:Common, btnGoBack %>" />
            </div>
        </form>
    </div>
</asp:Content>
