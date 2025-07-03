<%@ Page Title="" Language="C#" MasterPageFile="~/Mad201.Master" AutoEventWireup="true" CodeBehind="BankcardUpdateList.aspx.cs" Inherits="Web.Pages.User.BankcardUpdateList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation"
    runat="server">
    -
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="BankcardForm" runat="server">

        <h2><asp:Localize ID="lclProductListTitle" runat="server" meta:resourcekey="lclBankcardListTitle" /></h2>

        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>

        <asp:Button ID="btnShowForm" runat="server" OnClick="btnShowForm_Click" meta:resourcekey="btnAddBankcard" CausesValidation="false" />


        <div id="divNewCardForm" runat="server" visible="false"
         style="margin-top: 20px; padding: 20px; border: 1px solid #ccc; background-color: #f1f1f1; border-radius: 10px;">
    
            <h3><asp:Localize ID="hdBankcard" runat="server" meta:resourcekey="btnAddBankcard" /></h3>

                <div class="field">
                    <span class="label">
                        <asp:Localize ID="lclCardOwner" runat="server" meta:resourcekey="lclCardOwner" />
                    </span>
                    <span class="entry">
                        <asp:TextBox ID="txtCardOwner" runat="server" Width="200px" Placeholder="Ej. Juan Pérez" />
                        <asp:RequiredFieldValidator ID="rfvCardOwner" runat="server"
                            ControlToValidate="txtCardOwner" Display="Dynamic" meta:resourcekey="revMandatory"/>
                    </span>
                </div>

                <div class="field">
                    <span class="label">
                        <asp:Localize ID="lclCardType" runat="server" meta:resourcekey="lclCardType" />
                    </span>
                    <span class="entry">
                        <asp:DropDownList ID="ddlCardType" runat="server" Width="200px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvCardType" runat="server"
                            ControlToValidate="ddlCardType" InitialValue=""
                            Display="Dynamic" meta:resourcekey="revMandatory" />
                    </span>
                </div>

                <div class="field">
                    <span class="label">
                        <asp:Localize ID="lclCardNumber" runat="server" meta:resourcekey="lclCardNumber" />
                    </span>
                    <span class="entry">
                        <asp:TextBox ID="txtCardNumber" runat="server" Width="200px" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                            ControlToValidate="txtCardNumber" Display="Dynamic"
                            meta:resourcekey="revMandatory"/>
                        <asp:RegularExpressionValidator ID="revCardNumber" runat="server"
                            ControlToValidate="txtCardNumber"
                            ValidationExpression="^\d{13,19}$"
                            Display="Dynamic"
                            meta:resourcekey="revCardNumber" 
                            />
                    </span>
                </div>

                <div class="field">
                    <span class="label">
                        <asp:Localize ID="lclCvv" runat="server" meta:resourcekey="lclCvv" />
                    </span>
                    <span class="entry">
                        <asp:TextBox ID="txtCvv" runat="server" Width="200px" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                            ControlToValidate="txtCvv" Display="Dynamic"
                            meta:resourcekey="revMandatory"/>
                    </span>
                </div>

                <div class="field">
                    <span class="label">
                        <asp:Localize ID="lclExpirationDate" runat="server" meta:resourcekey="lclExpirationDate" />
                    </span>
                    <span class="entry">
                        <asp:TextBox ID="txtExpirationDate" runat="server" Width="200px" Placeholder="yyyy-MM" />
                        <asp:RequiredFieldValidator ID="rfvExpirationDate" runat="server"
                            ControlToValidate="txtExpirationDate" Display="Dynamic"
                            meta:resourcekey="revMandatory"/>
                        <asp:RegularExpressionValidator ID="revExpirationDate" runat="server"
                            ControlToValidate="txtExpirationDate" Display="Dynamic"
                            ValidationExpression="^\d{4}-\d{2}$"
                            meta:resourcekey="revExpDate" />
                    </span>
                </div>

                <div class="button" style="margin-top: 10px;">
                    <asp:Button ID="btnAddBankcard" runat="server" OnClick="btnSaveCard_Click" meta:resourcekey="btnAddBankcard"/>
                    <asp:Button ID="btnCancelEdit" runat="server" Text="Cancelar" 
                    OnClick="btnCancelEdit_Click" CausesValidation="false" 
                    CssClass="cancel-button" />
                </div>
                

        </div>

        <div id="cardListContainer" style="margin-top: 20px;">
            <asp:Repeater ID="rptBankCard" runat="server" 
              OnItemDataBound="rptBankCard_ItemDataBound" 
              OnItemCommand="rptBankCard_ItemCommand">
                <ItemTemplate>
                    <div style="
                        width: 80%;
                        margin: 0 auto 20px auto;
                        background-color: <%# Eval("IsDefault") != null && (bool)Eval("IsDefault") ? "#d1ffd1" : "#f9f9f9" %>;
                        border: 1px solid #ddd;
                        border-radius: 10px;
                        box-shadow: 0 4px 12px rgba(0, 0, 0, 0.05);
                        padding: 20px;
                        transition: transform 0.2s;
                    ">
                        <p><strong><asp:Localize ID="lclOwner" runat="server" meta:resourcekey="lclOwner" />:</strong> <%# Eval("Name") %></p>
                        <p><strong><asp:Localize ID="lclCardType" runat="server" meta:resourcekey="lclCardType" />:</strong> <asp:Localize ID="locCardTypeTranslated" runat="server" /></p>
                        <p><strong><asp:Localize ID="lclLastCardNumber" runat="server" meta:resourcekey="lclLastCardNumber" />:</strong> <%# Eval("CreditCardNumber") %></p>
                        <p><strong><asp:Localize ID="lclExpirationDate" runat="server" meta:resourcekey="lclExpirationDate" />:</strong> <%# Eval("ExpirationDate", "{0:yyyy-MM}") %></p>
                        <p><strong><asp:Localize ID="lclDefaultCard" runat="server" meta:resourcekey="lclDefaultCard" />:</strong> 
                            <asp:Localize ID="lblDefaultCard" runat="server" />
                        </p>

                        <!-- Botones -->
                        <asp:Button ID="btnDeleteCard" runat="server" CommandName="DeleteCard"
                            CommandArgument='<%# Eval("BankCardId") %>' CausesValidation="false" meta:resourcekey="btnDelete"/>

                        <asp:Button ID="btnEditCard" runat="server" CommandName="EditCard"
                             CommandArgument='<%# Eval("BankCardId") %>' CausesValidation="false" meta:resourcekey="btnEdit"/>


                        <asp:Button ID="btnSetDefault" runat="server" CommandName="SetDefaultCard"
                            CommandArgument='<%# Eval("BankCardId") %>' CausesValidation="false" meta:resourcekey="btnSetDefault"/>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <asp:Button ID="btnPrev" runat="server" Text="Anterior" OnClick="btnPrev_Click" CssClass="custom-btn" />
        <asp:Button ID="btnNext" runat="server" Text="Siguiente" OnClick="btnNext_Click" CssClass="custom-btn" />
    </form>
</asp:Content>
