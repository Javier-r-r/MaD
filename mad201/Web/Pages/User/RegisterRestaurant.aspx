<%@ Page Title="" Language="C#" MasterPageFile="~/Mad201.Master" AutoEventWireup="true" CodeBehind="RegisterRestaurant.aspx.cs" Inherits="Web.Pages.User.RegisterRestaurant" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation"
    runat="server">
    -
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <div id="form">
        <form id="RegisterForm" method="post" runat="server">

            
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclUserName" runat="server" meta:resourcekey="lclUserName" />
                </span><span
                        class="entry">
                        <asp:TextBox ID="txtLogin" runat="server" Width="100px" Columns="16"
                            meta:resourcekey="txtLoginResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtLogin"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rfvUserNameResource1"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblLoginError" runat="server" ForeColor="Red" Style="position: relative"
                            Visible="False" meta:resourcekey="lblLoginError"></asp:Label></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclPassword" runat="server" meta:resourcekey="lclPassword" /></span><span
                        class="entry">
                        <asp:TextBox TextMode="Password" ID="txtPassword" runat="server"
                            Width="100px" Columns="16" meta:resourcekey="txtPasswordResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rfvPasswordResource1"></asp:RequiredFieldValidator></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclRetypePassword" runat="server" meta:resourcekey="lclRetypePassword" /></span><span
                        class="entry">
                        <asp:TextBox TextMode="Password" ID="txtRetypePassword" runat="server" Width="100px"
                            Columns="16" meta:resourcekey="txtRetypePasswordResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvRetypePassword" runat="server" ControlToValidate="txtRetypePassword"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rfvRetypePasswordResource1"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="cvPasswordCheck" runat="server" ControlToCompare="txtPassword"
                            ControlToValidate="txtRetypePassword" meta:resourcekey="cvPasswordCheck"></asp:CompareValidator></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclRestaurantName" runat="server" meta:resourcekey="lclRestaurantName" /></span><span
                        class="entry">
                        <asp:TextBox ID="txtRestaurantName" runat="server" Width="100px"
                            Columns="16" meta:resourcekey="txtFirstNameResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvRestaurantName" runat="server" ControlToValidate="txtRestaurantName"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rfvRestaurantNameResource1"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblRestaurantNameError" runat="server" ForeColor="Red" Style="position: relative"
                            Visible="False" meta:resourcekey="lblRestaurantNameError"></asp:Label></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclAddress" runat="server" meta:resourcekey="lclAddress" /></span><span
                        class="entry">
                        <asp:TextBox ID="txtAddress" runat="server" Width="100px" Columns="16"
                            meta:resourcekey="txtAddressResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="txtAddress"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rfvAddressResource1"></asp:RequiredFieldValidator></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclEmail" runat="server" meta:resourcekey="lclEmail" /></span><span
                        class="entry">
                        <asp:TextBox ID="txtEmail" runat="server" Width="100px" Columns="16"
                            meta:resourcekey="txtEmailResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rfvEmailResource1"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                            Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            meta:resourcekey="revEmail"></asp:RegularExpressionValidator></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclSchedule" runat="server" meta:resourcekey="lclSchedule" /></span><span
                        class="entry">
                        <asp:TextBox ID="txtSchedule" runat="server" Width="100px"
                            Columns="16" meta:resourcekey="txtSchedule"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvSchedule" runat="server" ControlToValidate="txtSchedule"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rvfScheduleResource1"></asp:RequiredFieldValidator></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclRestaurantType" runat="server" meta:resourcekey="lclRestaurantType" /></span><span
                        class="entry">
                        <asp:TextBox ID="txtRestaurantType" runat="server" Width="100px"
                            Columns="16" meta:resourcekey="txtRestaurantType"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvRestaurantType" runat="server" ControlToValidate="txtRestaurantType"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rvfRestaurantTypeResource1"></asp:RequiredFieldValidator></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclLanguage" runat="server" meta:resourcekey="lclLanguage" /></span><span
                        class="entry">
                        <asp:DropDownList ID="comboLanguage" runat="server" AutoPostBack="True"
                            Width="100px" meta:resourcekey="comboLanguageResource1"
                            OnSelectedIndexChanged="ComboLanguageSelectedIndexChanged">
                        </asp:DropDownList></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclCountry" runat="server" meta:resourcekey="lclCountry" /></span><span
                        class="entry">
                        <asp:DropDownList ID="comboCountry" runat="server" Width="100px"
                            meta:resourcekey="comboCountryResource1">
                        </asp:DropDownList></span>
            </div>
            <div class="button">
                <asp:Button ID="btnRegister" runat="server" OnClick="BtnRegisterClick" meta:resourcekey="btnRegister" />
            </div>

            <div>
                <asp:Button ID="btnBack" runat="server" CssClass="custom-btn" OnClientClick="history.back(); return false;" Text="<%$ Resources:Common, btnMainPage %>" />
            </div>
        </form>
    </div>
</asp:Content>