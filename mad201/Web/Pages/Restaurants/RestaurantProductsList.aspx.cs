using Model;
using Model.Daos.Util;
using Model.Services.CartService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.HTTP.Session;

namespace Web.Pages.Restaurants
{
    
    public partial class RestaurantProducts : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UserSession userSession = (UserSession)Context.Session["userSession"];

                if (RestaurantId.HasValue)
                {
                    ConfigureViewByRole(userSession, RestaurantId.Value);
                    LoadCategories();
                    ViewState["CurrentPage"] = 1;
                    ViewState["PageSize"] = 5;
                    ViewState["LastProductsLoaded"] = "all";
                    LoadProducts(RestaurantId.Value, CurrentPage, PageSize);
                }
            }
        }

        private void ConfigureViewByRole(UserSession userSession, long urlRestaurantId)
        {
            if (userSession != null && userSession.Role == "Restaurant" && userSession.UserProfileId == urlRestaurantId)
            {
                lclProductListTitle.Visible = false;
                lclOwnRestaurantProductListTitle.Visible = true;
                pnlSearch.Visible = true;
                pnlAddProduct.Visible = true;
                rptProducts.Visible = true;
                rptProductsForClients.Visible = false;
                btnBack.Visible = false;
            }
            else
            {
                lclProductListTitle.Visible = true;
                lclOwnRestaurantProductListTitle.Visible = false;
                pnlSearch.Visible = true;
                pnlAddProduct.Visible = false;
                rptProducts.Visible = false;
                rptProductsForClients.Visible = true;
            }
        }

        private void LoadCategories()
        {
            ddlCategory.Items.Clear();

            List<Category> categories = SessionManager.GetAllCategories();
            List<Category> parents = categories.Where(c => c.Category2 == null).ToList();

            foreach (var parent in parents)
            {
                ddlCategory.Items.Add(new ListItem(parent.categoryName, parent.Id.ToString()));

                List<Category> childs = categories
                    .Where(c => c.Category2 != null && c.Category2.Id == parent.Id)
                    .ToList();

                foreach (var child in childs)
                {
                    string indent = "\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0";
                    ddlCategory.Items.Add(new ListItem(indent + child.categoryName, child.Id.ToString()));
                }
            }

            ddlCategory.Items.Insert(0, new ListItem(GetLocalResourceObject("allCategories").ToString(), "0"));
        }

        private void LoadProducts(long restaurantId, int page, int pageSize)
        {
            PagedResult<Product> products = SessionManager.GetAllRestaurantProducts(restaurantId, page, pageSize);

            UserSession userSession = (UserSession)Context.Session["userSession"];

            if (userSession != null && userSession.Role == "Restaurant" && userSession.UserProfileId == restaurantId)
            {
                rptProducts.DataSource = products.Items;
                rptProducts.DataBind();
            }
            else
            {
                rptProductsForClients.DataSource = products.Items;
                rptProductsForClients.DataBind();
            }

            //lblPageNumber.Text = products.PageNumber.ToString();
            btnPrev.Enabled = products.PageNumber > 1;
            btnNext.Enabled = products.PageNumber < products.TotalPages;
            ViewState["LastProductsLoaded"] = "all";
        }




        private void LoadProductsFilterByCategoryAndKeywords(long categoryId, string keywords, long restaurantId, int pageNumber, int pageSize)
        {

            PagedResult<Product> products = SessionManager.GetRestaurantProductsFilterByCategoryAndKeywords(categoryId, keywords, restaurantId, pageNumber, pageSize);

            UserSession userSession = (UserSession)Context.Session["userSession"];

            if (userSession != null && userSession.Role == "Restaurant" && userSession.UserProfileId == restaurantId)
            {
                rptProducts.DataSource = products.Items;
                rptProducts.DataBind();
            }
            else
            {
                rptProductsForClients.DataSource = products.Items;
                rptProductsForClients.DataBind();
            }

            //lblPageNumber.Text = products.PageNumber.ToString();
            btnPrev.Enabled = products.PageNumber > 1;
            btnNext.Enabled = products.PageNumber < products.TotalPages;

            ViewState["LastProductsLoaded"] = "filtered";

        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

            //Este metodo actualiza las categorias automaticamente, sin dar a buscar (actualizar para que funcione paginacion)

            //if (ddlCategory.SelectedValue == "")
            //{
            //    LoadProducts(RestaurantId.Value);
            //}
            //else
            //{
            //    if (long.TryParse(ddlCategory.SelectedValue, out long selectedCategoryId))
            //    {
            //        LoadProductsFilterByCategory(selectedCategoryId, RestaurantId.Value);
            //    }
            //}
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ViewState["CurrentPage"] = 1;
            if (long.TryParse(ddlCategory.SelectedValue, out long selectedCategoryId))
            {
                LoadProductsFilterByCategoryAndKeywords(selectedCategoryId, txtSearchValue.Text, RestaurantId.Value, CurrentPage, PageSize);
            }
        }


        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            Response.Redirect($"~/Pages/Restaurants/AddProduct.aspx");
        }

        protected void btnEditProduct_Command(object sender, CommandEventArgs e)
        {
            string[] args = e.CommandArgument.ToString().Split(';');
            long productId = Convert.ToInt64(args[0]);
            long restaurantId = Convert.ToInt64(args[1]);

            Response.Redirect($"~/Pages/Restaurants/EditProduct.aspx?restaurantId={restaurantId}&productId={productId}");
        }


        protected void btnDeleteProduct_Command(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            int productId;
            if (int.TryParse(btn.CommandArgument, out productId))
            {
                SessionManager.DeleteProduct(productId);
                string idParam = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(idParam) && long.TryParse(idParam, out long restaurantId))
                {
                    LoadProducts(restaurantId, (int)(ViewState["CurrentPage"]), (int)(ViewState["PageSize"]));
                }
            }
        }

        protected void btnAddProductToCart_Command(object sender, CommandEventArgs e)
        {
            string[] args = e.CommandArgument.ToString().Split(';');
            Cart cart = SessionManager.GetCart(System.Web.HttpContext.Current);

            if (args.Length >= 3)
            {
                long productId = Convert.ToInt64(args[0]);
                string productName = args[1];
                double productPrice = Convert.ToDouble(args[2]);

                CartLineDto line = new CartLineDto(productName, productPrice);

                cart.AddToCart(line);

                Response.Redirect("~/Pages/Orders/CartManagement.aspx");
            }
        }
        

        protected bool HasProperties(object dataItem)
        {
            var product = dataItem as Product;
            return product?.ProductPorperty != null && product.ProductPorperty.Any();
        }

        protected IEnumerable<ProductProperty> GetProperties(object dataItem)
        {
            var product = dataItem as Product;
            return product?.ProductPorperty ?? new List<ProductProperty>();
        }


        protected void btnNext_Click(object sender, EventArgs e)
        {
            CurrentPage++;

            string lastLoaded = ViewState["LastProductsLoaded"]?.ToString();

            if (lastLoaded == "all")
            {
                LoadProducts(RestaurantId.Value, CurrentPage, PageSize);
            }
            else if(lastLoaded == "filtered")
            {
                if (long.TryParse(ddlCategory.SelectedValue, out long selectedCategoryId))
                {
                    LoadProductsFilterByCategoryAndKeywords(selectedCategoryId, txtSearchValue.Text, RestaurantId.Value, CurrentPage, PageSize);
                }
            }
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            if (CurrentPage > 1)
                CurrentPage--;

            string lastLoaded = ViewState["LastProductsLoaded"]?.ToString();

            if (lastLoaded == "all")
            {
                LoadProducts(RestaurantId.Value, CurrentPage, PageSize);
            }
            else if (lastLoaded == "filtered")
            {
                if (long.TryParse(ddlCategory.SelectedValue, out long selectedCategoryId))
                {
                    LoadProductsFilterByCategoryAndKeywords(selectedCategoryId, txtSearchValue.Text, RestaurantId.Value, CurrentPage, PageSize);
                }
            }
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

        private long? RestaurantId
        {
            get
            {
                string idParam = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(idParam) && long.TryParse(idParam, out long restaurantId))
                {
                    return restaurantId;
                }
                return null;
            }
        }

    }
}