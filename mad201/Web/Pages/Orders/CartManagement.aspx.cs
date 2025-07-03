using System;
using System.Linq;
using System.Web.UI.WebControls;
using Model.Services.UserService.DTOs;
using Model.Services.CartService.DTOs;
using Model.Services.ClientService;
using Web.HTTP.Session;
using System.Collections.Generic;

namespace Web.Pages.Orders
{
    public partial class CartManagement : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCart();
                LoadBankcards();
            }

        }

        private void LoadCart()
        {
            List<CartLineDto> cart = SessionManager.GetCart(System.Web.HttpContext.Current).cart ?? new List<CartLineDto>();

            if (cart == null || !cart.Any())
            {
                gvCart.Visible = false;
                lblEmptyCart.Visible = true;
                lblTotal.Text = "";
                btnCheckout.Visible = false; 
            }
            else
            {
                gvCart.DataSource = cart;
                gvCart.DataBind();

                double total = cart.Sum(x => x.units * x.price);
                lblTotal.Text = "Total: " + total.ToString("C");
                gvCart.Visible = true;
                lblEmptyCart.Visible = false;
            }
        }

        private void LoadBankcards()
        {
            var userSession = SessionManager.GetUserSession(System.Web.HttpContext.Current);

            if (userSession != null && userSession.Role == "Client")
            {
                pnlBankcards.Visible = true;

                long clientId = userSession.UserProfileId;
                List<BankCardDetails> tarjetas = SessionManager.GetAllClientBankcards(clientId);

                ddlBankcards.Items.Clear();

                foreach (BankCardDetails tarjeta in tarjetas)
                {
                    string ultimosDigitos = tarjeta.CreditCardNumber.Length >= 4
                        ? tarjeta.CreditCardNumber.Substring(tarjeta.CreditCardNumber.Length - 4)
                        : tarjeta.CreditCardNumber;

                    ListItem item = new ListItem($"{tarjeta.CreditCardType} - **** **** **** {ultimosDigitos}", tarjeta.BankCardId.ToString());

                    if (tarjeta.IsDefault)
                        item.Selected = true;

                    ddlBankcards.Items.Add(item);
                }
            }
            else
            {
                pnlBankcards.Visible = false;
            }
        }


        protected void gvCart_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Cart cart = SessionManager.GetCart(System.Web.HttpContext.Current);
            string productName = Convert.ToString(e.CommandArgument).Trim();

            if (e.CommandName == "Increase")
            {
                cart.IncreaseProductUnits(productName);
            }
            else if (e.CommandName == "Decrease")
            {
                cart.DecreaseProductUnits(productName);
            }
            else if (e.CommandName == "Remove")
            {
                cart.RemoveFromCart(productName);
            }

            LoadCart(); // Refresh the view
        }

        protected void btnCheckout_ServerClick(object sender, EventArgs e)
        {
            if(SessionManager.GetUserSession(System.Web.HttpContext.Current) == null)
            {
                Response.Redirect("~/Pages/User/Authentication.aspx");
            }

            var cart = SessionManager.GetCart(System.Web.HttpContext.Current).cart;

            double total = cart.Sum(x => x.units * x.price);
            string resumenHtml = "<ul>";
            foreach (var item in cart)
            {
                resumenHtml += $"<li>{item.units}x {item.productName} - {item.price:C}</li>";
            }
            resumenHtml += "</ul>";
            resumenHtml += $"<p><strong>Total: {total:C}</strong></p>";

            string tarjetaSeleccionada = ddlBankcards.SelectedItem != null ? ddlBankcards.SelectedItem.Text : "No seleccionada";

            string direccionUsuario = ObtenerDireccionUsuario();

            resumenHtml += $"<p><strong>Tarjeta para pagar:</strong> {tarjetaSeleccionada}</p>";
            resumenHtml += $"<p><strong>Dirección de envío:</strong> {direccionUsuario}</p>";

            litResumenPedido.Text = resumenHtml;

            Page.ClientScript.RegisterStartupScript(this.GetType(), "showModal", "document.getElementById('confirmationModal').style.display = 'block';", true);
        }

        private string ObtenerDireccionUsuario()
        {
            var userSession = SessionManager.GetUserSession(System.Web.HttpContext.Current);

            UserSummaryDto client = SessionManager.FindUserProfileDetails(Context);

            return client.address;
        }


        protected void btnConfirmarPedido_Click(object sender, EventArgs e)
        {
            var userSession = SessionManager.GetUserSession(System.Web.HttpContext.Current);
            Cart cart = SessionManager.GetCart(System.Web.HttpContext.Current);

            if (cart == null || !cart.cart.Any())
                return;

            long clientId = userSession.UserProfileId;
            string address = ObtenerDireccionUsuario();
            long cardId = Convert.ToInt64(ddlBankcards.SelectedValue);



            // Llamar al método Purchase
            SessionManager.Purchase(clientId, address, cardId, cart.cart);

            SessionManager.ClearCart(System.Web.HttpContext.Current);

            // Redireccionar a página de confirmación
            Response.Redirect("~/Pages/Orders/ClientOrdersList.aspx");
        }


    }
}
