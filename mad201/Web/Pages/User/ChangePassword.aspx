<%@ Page Title="" Language="C#" MasterPageFile="~/Mad201.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="Web.Pages.User.ChangePassword" %>
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
        <form id="ChangePasswordForm" method="post" runat="server">
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclOldPassword" runat="server" meta:resourcekey="lclOldPassword" /></span><span
                        class="entry">
                        <asp:TextBox ID="txtOldPassword" TextMode="Password" runat="server" Width="100" Columns="16">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvOldPassword" runat="server" ControlToValidate="txtOldPassword"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"/>
                        <asp:Label ID="lblOldPasswordError" runat="server" ForeColor="Red" Visible="False"
                            meta:resourcekey="lblOldPasswordError">
                        </asp:Label>
                    </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclNewPassword" runat="server" meta:resourcekey="lclNewPassword" /></span><span
                        class="entry">
                        <asp:TextBox TextMode="Password" ID="txtNewPassword" runat="server" Width="100" Columns="16">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ControlToValidate="txtNewPassword"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"/>
                        <asp:CompareValidator ID="cvCreateNewPassword" runat="server" ControlToCompare="txtOldPassword"
                            ControlToValidate="txtNewPassword" Operator="NotEqual" meta:resourcekey="cvCreateNewPassword"></asp:CompareValidator>
                    </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclRetypePassword" runat="server" meta:resourcekey="lclRetypePassword" /></span><span
                        class="entry">
                        <asp:TextBox TextMode="Password" ID="txtRetypePassword" runat="server" Width="100"
                            Columns="16"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvRetypePassword" runat="server" ControlToValidate="txtRetypePassword"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"/>
                        <asp:CompareValidator ID="cvPasswordCheck" runat="server" ControlToCompare="txtNewPassword"
                            ControlToValidate="txtRetypePassword" meta:resourcekey="cvPasswordCheck"></asp:CompareValidator>
                    </span>
            </div>
            <div class="button">
                <asp:Button ID="btnChangePassword" runat="server" OnClick="BtnChangePasswordClick" CssClass="custom-btn" meta:resourcekey="btnChangePassword" />
            </div>

            <div>
                <asp:Button ID="btnBack" runat="server" OnClientClick="history.back(); return false;" CssClass="custom-btn" Text="<%$ Resources:Common, btnCancel %>" />
            </div>
        </form>
    </div>
</asp:Content>
