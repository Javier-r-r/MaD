<%@ Page Title="" Language="C#" MasterPageFile="~/Mad201.Master" AutoEventWireup="true" CodeBehind="RestaurantProductsList.aspx.cs" Inherits="Web.Pages.Restaurants.RestaurantProducts" %>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server">

        <h2><asp:Localize ID="lclProductListTitle" runat="server" meta:resourcekey="lclProductListTitle" /></h2>
        <h2><asp:Localize ID="lclOwnRestaurantProductListTitle" runat="server" meta:resourcekey="lclOwnRestaurantProductListTitle" /></h2>

        <asp:Panel ID="pnlAddProduct" runat="server" Visible="true">
            <asp:Button ID="btnAddProduct" runat="server" OnClick="btnAddProduct_Click"
                 Style="width: 80%; margin: 0 auto 20px auto;
                       background-color: #f9f9f9;
                       border: 1px solid #ddd;
                       border-radius: 10px;
                       box-shadow: 0 4px 12px rgba(0, 0, 0, 0.05);
                       padding: 20px;
                       transition: transform 0.2s;
                       cursor: pointer;
                       font-size: 1.2em;
                       color: #333;
                       text-align: left;"
                onmouseover="this.style.transform='scale(1.02)'"
                onmouseout="this.style.transform='scale(1)'" 
                meta:resourcekey="lclAddProductBtn" />

        </asp:Panel>

        <asp:Panel ID="pnlSearch" runat="server" Visible="true">
            <span class="label">
                <asp:Label ID="textSearch" runat="server" meta:resourcekey="textSearch"/>
            </span>
            <span class="entry">
                <asp:TextBox ID="txtSearchValue" runat="server" Width="100px" Columns="16" CssClass="custom-textbox"></asp:TextBox>
            </span>

            <span class="label">
                <asp:Label ID="SearchByCategory" runat="server" meta:resourcekey="SearchByCategory"/>
            </span>
            <span class="entry">
                <asp:DropDownList id="ddlCategory" runat="server" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="True"  CssClass="custom-dropdown"/>
            </span>

            <span class="button">
                <asp:Button id="btnSearch" runat="server" OnClick="btnSearch_Click" meta:resourcekey="searchBtn" CssClass="custom-btn"/>
            </span>
            
        </asp:Panel>

        <asp:Repeater ID="rptProducts" runat="server">
            <ItemTemplate>
                <div style="
                    width: 80%;
                    margin: 0 auto 20px auto;
                    background-color: #f9f9f9;
                    border: 1px solid #ddd;
                    border-radius: 10px;
                    box-shadow: 0 4px 12px rgba(0, 0, 0, 0.05);
                    padding: 20px;
                    transition: transform 0.2s;
                ">
                    <h3 style="margin-top: 0;"><%# Eval("Name") %></h3>
                    <p><strong><asp:Localize ID="lclPrice" runat="server" Visible="true" meta:resourcekey="lclPrice" />:</strong> <%# Eval("Price", "{0:C}") %></p>
                    <p><strong><asp:Localize ID="lclCategory" runat="server" Visible="true" meta:resourcekey="lclCategory" />:</strong> <%# Eval("Category.CategoryName") %></p>
                    <p><strong><asp:Localize ID="lclCreationDate" runat="server" Visible="true" meta:resourcekey="lclCreationDate" />:</strong> <%# Eval("CreationDate") %></p>
                    <p><strong><asp:Localize ID="lclStock" runat="server" Visible="true" meta:resourcekey="lclStock" />:</strong> <%# Eval("Stock") %></p>
                    <asp:Panel ID="pnlProperties" runat="server" Visible='<%# HasProperties(Container.DataItem) %>'>
                        <asp:Repeater ID="rptProductProperties" runat="server" DataSource='<%# GetProperties(Container.DataItem) %>'>
                            <ItemTemplate>
                                <p><strong><%# Eval("Property.Name") %></strong>: <%# Eval("Value") %></p>
                            </ItemTemplate>
                        </asp:Repeater>
                    </asp:Panel>

                    <asp:Button ID="btnEditProduct" runat="server" OnCommand="btnEditProduct_Command" CommandArgument='<%# Eval("Id") + ";" + Eval("Restaurant.Id") %>'
                     Style="width: 20%; margin: 0 auto 20px auto;
                           background-color: #f9f9f9;
                           border: 1px solid #ddd;
                           border-radius: 10px;
                           box-shadow: 0 4px 12px rgba(0, 0, 0, 0.05);
                           padding: 20px;
                           transition: transform 0.2s;
                           cursor: pointer;
                           font-size: 1.2em;
                           color: #333;
                           text-align: left;"
                    onmouseover="this.style.transform='scale(1.02)'"
                    onmouseout="this.style.transform='scale(1)'" 
                    meta:resourcekey="editProductBtn" />
                    <%-- Botón para eliminar producto, solo visible si es restaurante --%>
                    <asp:Button ID="btnDeleteProduct" runat="server" OnClick="btnDeleteProduct_Command" CommandArgument='<%# Eval("Id") %>'
                     Style="width: 20%; margin: 0 auto 20px auto;
                           background-color: #f9f9f9;
                           border: 1px solid #ddd;
                           border-radius: 10px;
                           box-shadow: 0 4px 12px rgba(0, 0, 0, 0.05);
                           padding: 20px;
                           transition: transform 0.2s;
                           cursor: pointer;
                           font-size: 1.2em;
                           color: #333;
                           text-align: left;"
                    onmouseover="this.style.transform='scale(1.02)'"
                    onmouseout="this.style.transform='scale(1)'" 
                    meta:resourcekey="deleteProductBtn" />

                </div>
            </ItemTemplate>
        </asp:Repeater>

        <asp:Repeater ID="rptProductsForClients" runat="server" >
            <ItemTemplate>
                <div style="
                    width: 80%;
                    margin: 0 auto 20px auto;
                    background-color: #f9f9f9;
                    border: 1px solid #ddd;
                    border-radius: 10px;
                    box-shadow: 0 4px 12px rgba(0, 0, 0, 0.05);
                    padding: 20px;
                    transition: transform 0.2s;
                ">
                    <h3 style="margin-top: 0;"><%# Eval("Name") %></h3>
                    <p><strong><asp:Localize ID="lclPrice" runat="server" Visible="true" meta:resourcekey="lclPrice" />:</strong> <%# Eval("Price", "{0:C}") %></p>
                    <p><strong><asp:Localize ID="lclCategory" runat="server" Visible="true" meta:resourcekey="lclCategory" />:</strong> <%# Eval("Category.CategoryName") %></p>

                    <asp:Panel ID="pnlProperties" runat="server" Visible='<%# HasProperties(Container.DataItem) %>'>
                        <asp:Repeater ID="rptProductProperties" runat="server" DataSource='<%# GetProperties(Container.DataItem) %>'>
                            <ItemTemplate>
                                <p><strong><%# Eval("Property.Name") %></strong>: <%# Eval("Value") %></p>
                            </ItemTemplate>
                        </asp:Repeater>
                    </asp:Panel>

                    <asp:Button ID="btnAddProductToCart" runat="server" OnCommand="btnAddProductToCart_Command" CommandArgument='<%# Eval("Id") + ";" + Eval("Name") + ";" + Eval("Price")%>'
                     Style="width: 20%; margin: 0 auto 20px auto;
                           background-color: #f9f9f9;
                           border: 1px solid #ddd;
                           border-radius: 10px;
                           box-shadow: 0 4px 12px rgba(0, 0, 0, 0.05);
                           padding: 20px;
                           transition: transform 0.2s;
                           cursor: pointer;
                           font-size: 1.2em;
                           color: #333;
                           text-align: left;"
                    onmouseover="this.style.transform='scale(1.02)'"
                    onmouseout="this.style.transform='scale(1)'" 
                    meta:resourcekey="btnAddProductToCart" />
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