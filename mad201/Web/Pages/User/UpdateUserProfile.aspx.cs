using Model.Services.UserService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.HTTP.Session;
using Web.HTTP.View.ApplicationObjects;

namespace Web.Pages.User
{
    public partial class UpdateUserProfile : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UserSummaryDto userProfileDetails = SessionManager.FindUserProfileDetails(Context);

                txtFirstName.Text = userProfileDetails.name;
                txtEmail.Text = userProfileDetails.email;

                UpdateComboLanguage(userProfileDetails.language);
                UpdateComboCountry(userProfileDetails.language, userProfileDetails.country);

                UserSession userSession = (UserSession)Context.Session["userSession"];

                if (userSession.Role == "Client")
                {
                    Model.Client client = SessionManager.FindClient(Context);
                    surnameField.Visible = true;
                    txtSurname.Text = client.surname;

                    scheduleField.Visible = false;
                    typeField.Visible = false;
                }
                else if (userSession.Role == "Restaurant")
                {
                    Model.Restaurant restaurant = SessionManager.FindRestaurant(Context);
                    scheduleField.Visible = true;
                    typeField.Visible = true;

                    txtSchedule.Text = restaurant.schedule;
                    txtType.Text = restaurant.type;

                    surnameField.Visible = false;
                    lnkToBankcards.Visible = false;
                }
            }
        }

        /// <summary>
        /// Loads the languages in the comboBox in the *selectedLanguage*. 
        /// Also, the selectedLanguage will appear selected in the 
        /// ComboBox
        /// </summary>
        private void UpdateComboLanguage(String selectedLanguage)
        {
            this.comboLanguage.DataSource = Languages.GetLanguages(selectedLanguage);
            this.comboLanguage.DataTextField = "text";
            this.comboLanguage.DataValueField = "value";
            this.comboLanguage.DataBind();
            this.comboLanguage.SelectedValue = selectedLanguage;
        }

        /// <summary>
        /// Loads the countries in the comboBox in the *selectedLanguage*. 
        /// Also, the *selectedCountry* will appear selected in the 
        /// ComboBox
        /// </summary>
        private void UpdateComboCountry(String selectedLanguage, String selectedCountry)
        {
            this.comboCountry.DataSource = Countries.GetCountries(selectedLanguage);
            this.comboCountry.DataTextField = "text";
            this.comboCountry.DataValueField = "value";
            this.comboCountry.DataBind();
            this.comboCountry.SelectedValue = selectedCountry;
        }

        /// <summary>
        /// Handles the Click event of the btnUpdate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance 
        /// containing the event data.</param>
        protected void BtnUpdateClick(object sender, EventArgs e)
        {
            UserSession userSession = (UserSession)Context.Session["userSession"];

            if (Page.IsValid)
            {
                string name = txtFirstName.Text;
                string email = txtEmail.Text;
                string language = comboLanguage.SelectedValue;
                string country = comboCountry.SelectedValue;

                if (userSession.Role == "Client")
                {
                    string surname = txtSurname.Text;
                    SessionManager.UpdateClientProfile(Context, userSession.UserProfileId, name, surname, email, language, country);
                }
                else if (userSession.Role == "Restaurant")
                {
                    string schedule = txtSchedule.Text;
                    string type = txtType.Text;
                    SessionManager.UpdateRestaurantProfile(Context, userSession.UserProfileId, name, email, schedule, type, language, country);
                }

                Response.Redirect(Response.ApplyAppPathModifier("~/Pages/MainPage.aspx"));
            }

        }

        protected void ComboLanguageSelectedIndexChanged(object sender, EventArgs e)
        {
            /* After a language change, the countries are printed in the
             * correct language.
             */
            this.UpdateComboCountry(comboLanguage.SelectedValue,
                comboCountry.SelectedValue);
        }
    }
}