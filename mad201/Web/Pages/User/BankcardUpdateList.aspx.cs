using System;
using Model.Services.ClientService;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.HTTP.Session;
using Model.Daos.Util;

namespace Web.Pages.User
{
    public partial class BankcardUpdateList : SpecificCulturePage
    {

        private long? EditingCardId
        {
            get => ViewState["EditingCardId"] as long?;
            set => ViewState["EditingCardId"] = value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["CurrentPage"] = 1;
                ViewState["PageSize"] = 3;
                LoadBankcards();
                ddlCardType.Items.Clear();
                ddlCardType.Items.Add(new ListItem(GetLocalResourceObject("lclSelectType.Text")?.ToString(), ""));
                ddlCardType.Items.Add(new ListItem(GetLocalResourceObject("opCredit.Text")?.ToString(), "Credit"));
                ddlCardType.Items.Add(new ListItem(GetLocalResourceObject("opDebit.Text")?.ToString(), "Debit"));

            }

            // Restaura visibilidad después de postback
            if (ViewState["IsFormVisible"] != null)
            {
                divNewCardForm.Visible = (bool)ViewState["IsFormVisible"];
            }

            
        }

        private void LoadBankcards()
        {
            UserSession userSession = (UserSession)Context.Session["userSession"];
            PagedResult<BankCardDetails> bankCards = SessionManager.GetClientBankcards(userSession.UserProfileId, CurrentPage, PageSize);
            rptBankCard.DataSource = bankCards.Items;
            rptBankCard.DataBind();

            btnPrev.Enabled = bankCards.PageNumber > 1; 
            btnNext.Enabled = bankCards.PageNumber < bankCards.TotalPages;
        }


        protected void btnShowForm_Click(object sender, EventArgs e)
        {
            bool isVisible = ViewState["IsFormVisible"] as bool? ?? false;
            isVisible = !isVisible;

            divNewCardForm.Visible = isVisible;
            ViewState["IsFormVisible"] = isVisible;
        }


        protected void btnSaveCard_Click(object sender, EventArgs e)
        {
            UserSession userSession = (UserSession)Context.Session["userSession"];

            string owner = txtCardOwner.Text.Trim();
            string type = ddlCardType.SelectedValue;
            string cardNumberText = txtCardNumber.Text.Trim();
            string cvvText = txtCvv.Text.Trim();
            string expirationText = txtExpirationDate.Text.Trim();

            lblError.Text = ""; // Limpia errores previos

            // Validar número de tarjeta
            if (!long.TryParse(cardNumberText, out long cardNumber))
            {
                lblError.Text = "Número de tarjeta inválido.";
                return;
            }

            // Validar CVV
            if (!int.TryParse(cvvText, out int cvv))
            {
                lblError.Text = "CVV inválido.";
                return;
            }

            // Validar fecha de expiración
            if (!DateTime.TryParse(expirationText, out DateTime expiration))
            {
                lblError.Text = "Fecha de expiración inválida.";
                return;
            }

            // Guardar la tarjeta
            try
            {

                if (EditingCardId.HasValue)
                {
                    SessionManager.UpdateBankcard(EditingCardId.Value, type, cardNumber, owner, cvv, expiration);
                }
                else
                {
                    SessionManager.AddBankcard(userSession.UserProfileId, type, cardNumber, owner, cvv, expiration);
                }
                Response.Redirect(Response.ApplyAppPathModifier("~/Pages/User/BankcardUpdateList.aspx"));
            }
            catch (Exception ex)
            {
                lblError.Text = "Error al guardar la tarjeta: " + ex.Message;
            }
        }

        protected void rptBankCard_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var cardType = DataBinder.Eval(e.Item.DataItem, "CreditCardType")?.ToString();
                var isDefault = DataBinder.Eval(e.Item.DataItem, "IsDefault")?.ToString();
                var locCardTypeTranslated = (Localize)e.Item.FindControl("locCardTypeTranslated");
                var lblDefaultCard = (Localize)e.Item.FindControl("lblDefaultCard");

                if (locCardTypeTranslated != null && !string.IsNullOrEmpty(cardType))
                {
                    switch (cardType)
                    {
                        case "Credit":
                            locCardTypeTranslated.Text = GetLocalResourceObject("opCredit.Text")?.ToString();
                            break;
                        case "Debit":
                            locCardTypeTranslated.Text = GetLocalResourceObject("opDebit.Text")?.ToString();
                            break;
                    }
                }

                if (lblDefaultCard != null && !string.IsNullOrEmpty(isDefault))
                {
                    switch (isDefault)
                    {
                        case "True":
                            lblDefaultCard.Text = GetLocalResourceObject("lclDefYes.Text")?.ToString();
                            break;
                        case "False":
                            lblDefaultCard.Text = GetLocalResourceObject("lclDefNo.Text")?.ToString();
                            break;
                    }
                }
            }
        }

        protected void btnCancelEdit_Click(object sender, EventArgs e)
        {
            // Limpiar campos
            txtCardOwner.Text = "";
            ddlCardType.SelectedIndex = 0;
            txtCardNumber.Text = "";
            txtCvv.Text = "";
            txtExpirationDate.Text = "";

            // Restaurar botón a estado original
            btnAddBankcard.Text = GetLocalResourceObject("btnAddBankcard.Text")?.ToString() ?? "Añadir tarjeta";
            hdBankcard.Text = GetLocalResourceObject("hdBankcard.Text")?.ToString() ?? "Añadir tarjeta";
            // Ocultar formulario
            divNewCardForm.Visible = false;
            ViewState["IsFormVisible"] = false;

            // Cancelar modo edición
            EditingCardId = null;
        }



        protected void rptBankCard_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            UserSession userSession = (UserSession)Context.Session["userSession"];

            if (e.CommandName == "DeleteCard")
            {
                long cardId = Convert.ToInt64(e.CommandArgument);
                SessionManager.RemoveBankCard(cardId);
            }
            else if (e.CommandName == "SetDefaultCard")
            {
                long cardId = Convert.ToInt64(e.CommandArgument);
                SessionManager.SetDefaultBankcard(cardId);
            }
            else if (e.CommandName == "EditCard")
            {
                long cardId = Convert.ToInt64(e.CommandArgument);

                var card = SessionManager.GetBankcardData(cardId);

                if (card != null)
                {
                    txtCardOwner.Text = card.name;
                    ddlCardType.SelectedValue = card.cardtype;
                    txtCardNumber.Text = card.number.ToString();
                    txtCvv.Text = card.cvv.ToString(); // Asegúrate que `cvv` esté disponible
                    txtExpirationDate.Text = card.expirationdate.ToString("yyyy-MM");

                    EditingCardId = cardId;
                    btnAddBankcard.Text = GetLocalResourceObject("btnUpdateCard.Text")?.ToString() ?? "Actualizar tarjeta";
                    hdBankcard.Text = GetLocalResourceObject("btnUpdateCard.Text")?.ToString() ?? "Actualizar tarjeta";

                    divNewCardForm.Visible = true;
                    ViewState["IsFormVisible"] = true;
                }
            }


            PagedResult<BankCardDetails> bankCards = SessionManager.GetClientBankcards(userSession.UserProfileId, CurrentPage, PageSize);
            rptBankCard.DataSource = bankCards.Items;
            rptBankCard.DataBind();
        }


        protected void btnNext_Click(object sender, EventArgs e)
        {
            CurrentPage++;

            LoadBankcards();
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            if (CurrentPage > 1)
                CurrentPage--;

            LoadBankcards();
        }


        private int CurrentPage
        {
            get => (int)(ViewState["CurrentPage"] ?? 1);
            set => ViewState["CurrentPage"] = value;
        }

        private int PageSize
        {
            get => (int)(ViewState["PageSize"] ?? 10);
            set => ViewState["PageSize"] = value;
        }

    }
}