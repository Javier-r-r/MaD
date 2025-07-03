<%@ Page Title="" Language="C#" MasterPageFile="~/Mad201.Master" AutoEventWireup="true" CodeBehind="EditProduct.aspx.cs" Inherits="Web.Pages.Restaurants.EditProduct" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <h2>
        <asp:Localize ID="lclEditProductTitle" runat="server" meta:resourcekey="lclEditProductTitle" />
    </h2>

        <div id="form">
            <form id="AddProductForm" method="post" runat="server">

            
                <div class="field">
                    <span class="label">
                        <asp:Localize ID="lclProductName" runat="server" meta:resourcekey="lclProductName" />
                    </span><span
                            class="entry">
                            <asp:TextBox ID="txtProductName" runat="server" Width="100px" Columns="16"
                                meta:resourcekey="txtProductName"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvProductName" runat="server" ControlToValidate="txtProductName"
                                Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                                meta:resourcekey="rfvProductName"></asp:RequiredFieldValidator>
                     </span>
                </div>
            
                <div class="field">
                    <span class="label">
                        <asp:Localize ID="lclPrice" runat="server" meta:resourcekey="lclPrice" />
                    </span>
                    <span
                            class="entry">
                            <asp:TextBox ID="price" runat="server" Width="100px" Columns="16"
                                meta:resourcekey="price"></asp:TextBox>
                             <!-- Campo obligatorio -->
                            <asp:RequiredFieldValidator 
                                ID="rfvPrice" 
                                runat="server" 
                                ControlToValidate="price"
                                Display="Dynamic"
                                Text="<%$ Resources:Common, mandatoryField %>"
                                meta:resourcekey="rfvPrice" />

                            <!-- Validación de tipo numérico -->
                            <asp:RegularExpressionValidator 
                                ID="revPrice" 
                                runat="server" 
                                ControlToValidate="price"
                                ValidationExpression="^\d+([\., \,]\d{1,2})?$"
                                Display="Dynamic"
                                meta:resourcekey="lblNumericError" />

                    </span>
                 </div>

                 <div class="field">
                    <span class="label">
                        <asp:Localize ID="lblStock" runat="server" meta:resourcekey="lblStock" />
                    </span>
                     <span
                            class="entry">
                            <asp:TextBox ID="txtStock" runat="server" Width="100px" Columns="16" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvStock" runat="server" ControlToValidate="txtStock"
                                Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                                meta:resourcekey="rfvStock"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator 
                                ID="RegularExpressionValidator1" 
                                runat="server" 
                                ControlToValidate="txtStock"
                                ValidationExpression="^\d+(\.\d{1,2})?$"
                                Display="Dynamic"
                                meta:resourcekey="lblNumericError" />
                    </span>
                </div>
            

                <div class="field">
                    
                    <span class="label">
                        <asp:Localize ID="lblCategory" runat="server" meta:resourcekey="lblCategory" />
                    </span>
                    <span class="entry">
                        <asp:DropDownList ID="ddlCategory" runat="server" Width="150px"  AutoPostBack="True" meta:resourcekey="ddlCategory" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCategory" InitialValue="" Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>" meta:resourcekey="rfvCategory" />
                    </span>
                    
                </div>


                <div class="field" id="propertyList" runat="server" visible="true">
                    <h4>
                        <asp:Localize ID="lblPropertyList" runat="server" meta:resourcekey="lblPropertyList" />
                    </h4>

                     <div style="text-align: center;">
                      <div style="display: inline-block;">
                        <asp:Repeater ID="rptPropertyList" runat="server">
                          <ItemTemplate>
                            <div style="display: flex; gap: 8px; align-items: center; margin-bottom: 4px;">
                              <span style="font-weight: bold; min-width: 150px;"> <%# Eval("Property.name") %>: </span>
                              <span> <%# Eval("value") %> </span>
                                
                            </div>
                          </ItemTemplate>
                        </asp:Repeater>
                          <asp:Button ID="btnDeleteProperty" runat="server"  OnClick="btnDeleteProperty_Click" CausesValidation="false" meta:resourcekey="btnDeleteProperty" />
                      </div>
                    </div>


                </div>

           

                <div class="button">
                    <asp:Button ID="btnAddProperty" runat="server" OnClick="BtnAddPropertyClick" CausesValidation="false" meta:resourcekey="btnAddProperty" />
                </div>

                <div class="field"  id="specificPropertyContainer" runat="server" visible="false">
                    <div class="field"  >

                        <asp:Localize ID="lblSpecificProperty" runat="server" meta:resourcekey="lblSpecificProperty" />
                    
                        <asp:DropDownList ID="ddlSpecificProperty" runat="server" Width="250px" OnSelectedIndexChanged="ddlSpecificProperty_SelectedIndexChanged" AutoPostBack="True" meta:resourcekey="ddlSpecificProperty" />
                        
                        <div class="field">
                            <asp:Localize ID="lblspecificPropertyValue" runat="server" Visible="false" />
                            <asp:TextBox ID="txtspecificPropertyValue" runat="server" Width="100px" Columns="16" Visible="false" ></asp:TextBox>
                        </div>

                         <div class="button">
                            <asp:Button ID="btnSaveProperty" runat="server" OnClick="btnSavePorpertyClick" CausesValidation="false" meta:resourcekey="btnSaveProperty" />
                        </div>
                       
                    </div>
                </div>

           

                <div class="button">
                    <asp:Button ID="saveProductBtn" runat="server" OnClick="btnSaveProduct_Click" meta:resourcekey="saveProductBtn" />
                </div>
            </form>
    </div>
</asp:Content>
