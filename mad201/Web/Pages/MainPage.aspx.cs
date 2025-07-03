
using Model;
using Model.Daos.Util;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Web.HTTP.Session;

namespace Web.Pages
{
    public partial class MainPage : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UserSession userSession = (UserSession)Context.Session["userSession"];
                if (userSession != null && userSession.Role == "Restaurant")
                {
                    Response.Redirect($"~/Pages/Restaurants/RestaurantProductsList.aspx?id={userSession.UserProfileId}");
                    return;
                }
                else
                {
                    ViewState["CurrentPage"] = 1;
                    ViewState["PageSize"] = 5;
                    LoadRestaurantTypesOnDdl();
                    LoadRestaurants();
                }
            }

        }

        private void LoadRestaurantTypesOnDdl()
        {
            ddlType.Items.Insert(0, new ListItem(GetLocalResourceObject("allTypes").ToString(), ""));

            List<string> types = SessionManager.GetAllRestaurantsTypes();
            HashSet<string> addedTypes = new HashSet<string>();

            foreach (string type in types)
            {

                if (!string.IsNullOrEmpty(type) && !addedTypes.Contains(type))
                {
                    addedTypes.Add(type);
                    ddlType.Items.Add(new ListItem(type, type));
                }
            }
        } 

        private void LoadRestaurants()
        {
            PagedResult<Restaurant> restaurants = null;
            if (ddlType.SelectedValue == "")
            {
                restaurants = SessionManager.GetAllRestaurants(CurrentPage, PageSize);
            }
            else
            {
                restaurants = SessionManager.GetRestaurantsFilterByType(ddlType.SelectedValue, CurrentPage, PageSize);
            }
               
            rptRestaurants.DataSource = restaurants.Items;
            rptRestaurants.DataBind();

            btnPrev.Enabled = restaurants.PageNumber > 1;
            btnNext.Enabled = restaurants.PageNumber < restaurants.TotalPages;
        }


        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRestaurants();
        }


        protected void btnNext_Click(object sender, EventArgs e)
        {
            CurrentPage++;

            LoadRestaurants();
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            if (CurrentPage > 1)
                CurrentPage--;

            LoadRestaurants();
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