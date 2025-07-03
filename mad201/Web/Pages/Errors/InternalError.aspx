<%@ Page Title="" Language="C#" MasterPageFile="~/Mad201.Master" AutoEventWireup="true" 
    CodeBehind="InternalError.aspx.cs" Inherits="Web.Pages.Errors.InternalError" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuWelcome"
    runat="server">
    &nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <br />
    <br />
    <asp:Label ID="lblErrorTitle" runat="server" meta:resourcekey="lblErrorTitle"></asp:Label>
    &nbsp;
    <br />
    <br />
    <asp:Label ID="lblRetryLater" runat="server" meta:resourcekey="lblRetryLater"></asp:Label>
    <br />
    <br />    
</asp:Content>
