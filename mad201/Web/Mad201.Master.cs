using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.HTTP.Session;

namespace Web
{
    public partial class Mad201 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SessionManager.IsUserAuthenticated(Context))
            {

                if (lblDash2 != null)
                    lblDash2.Visible = false;
                if (lnkUpdate != null)
                    lnkUpdate.Visible = false;
                if (lblDash3 != null)
                    lblDash3.Visible = false;
                if (lnkLogout != null)
                    lnkLogout.Visible = false;
                if (lblDash5 != null)
                    lblDash5.Visible = false;
                if (lnkOrders != null)
                    lnkOrders.Visible = false;

            }
            else
            {
                if (lblWelcome != null)
                    lblWelcome.Text =
                        GetLocalResourceObject("lblWelcome.Hello.Text").ToString()
                        + " " + SessionManager.GetUserSession(Context).FirstName
                        + " (" + SessionManager.GetUserSession(Context).Role + ")";
                    
                if (lblDash1 != null)
                    lblDash1.Visible = false;
                if (lblDash4 != null)
                    lblDash4.Visible = false;
                if (lnkAuthenticate != null)
                    lnkAuthenticate.Visible = false;
                if (lnkRegisterRestaurant != null)
                    lnkRegisterRestaurant.Visible = false;
                if(SessionManager.GetUserSession(Context).Role == "Restaurant")
                {
                    if (lblDash6 != null)
                        lblDash6.Visible = false;
                    if (lnkCartManagement != null)
                        lnkCartManagement.Visible = false;
                    if (lblDash5 != null)
                        lblDash5.Visible = false;
                    if (lnkCartManagement != null)
                        lnkOrders.Visible = false;
                }
            }
            UpdateCartLink();
        }

        private void UpdateCartLink()
        {
            Cart cart = SessionManager.GetCart(System.Web.HttpContext.Current);
            int totalUnits = 0;

            if (cart != null && cart != null)
            {
                totalUnits = cart.cart.Sum(line => line.units);
            }

            if(lnkCartManagement != null)
            {
                if (lnkCartManagement.Text.Contains("("))
                {
                    lnkCartManagement.Text = lnkCartManagement.Text.Substring(0, lnkCartManagement.Text.Length - 3);
                }
                lnkCartManagement.Text += "(" + totalUnits + ")";
            }
        }
    }
}