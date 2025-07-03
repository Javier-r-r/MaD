using System;
using System.Linq;
using System.Web.UI;
using System.Collections.Generic;
using Model;
using Web.HTTP.Session;
using Model.Daos.Util;

namespace Web.Pages.Orders
{
    public partial class ClientOrdersList : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["CurrentPage"] = 1;
                ViewState["PageSize"] = 4;
                LoadOrders();
            }
        }

        private void LoadOrders()
        {
            var userSession = SessionManager.GetUserSession(Context);
            PagedResult<Order> orders = SessionManager.GetClientOrders(userSession.UserProfileId, CurrentPage, PageSize);

            rptOrders.DataSource = orders.Items;
            rptOrders.DataBind();

            btnPrev.Enabled = orders.PageNumber > 1;
            btnNext.Enabled = orders.PageNumber < orders.TotalPages;

        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            CurrentPage++;

            LoadOrders();
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            if (CurrentPage > 1)
                CurrentPage--;

            LoadOrders();
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
