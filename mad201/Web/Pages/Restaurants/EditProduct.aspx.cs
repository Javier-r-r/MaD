using Model;
using Model.Services.RestaurantService.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.HTTP.Session;

namespace Web.Pages.Restaurants
{
    public partial class EditProduct : SpecificCulturePage
    {

        private List<ProductProperty> PropertiesList
        {
            get
            {
                if (Session["PropertiesList"] == null)
                    Session["PropertiesList"] = new List<ProductProperty>();
                return (List<ProductProperty>)Session["PropertiesList"];
            }
            set
            {
                Session["PropertiesList"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                LoadCategories();
                LoadSpecificProperties();

                string restaurantIdParam = Request.QueryString["restaurantId"];
                string productIdParam = Request.QueryString["productId"];

                if (!string.IsNullOrEmpty(restaurantIdParam) &&
                    long.TryParse(restaurantIdParam, out long restaurantId) &&
                    !string.IsNullOrEmpty(productIdParam) &&
                    long.TryParse(productIdParam, out long productId))
                {
                    LoadProduct(productId);
                }
                else
                {
                    var lista = Session["PropertiesList"] as List<ProductProperty>; // tu clase de objeto
                    rptPropertyList.DataSource = lista;
                    rptPropertyList.DataBind();
                }
            }
            else
            {
                BindPropertyList();
            }
            propertyList.Visible = PropertiesList.Count>0;

        }

        private void LoadCategories()
        {
            List<Category> categories = SessionManager.GetAllCategories();

            ddlCategory.Items.Clear();

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
                    ddlCategory.Items.Add(new ListItem(indent + child.categoryName, child.categoryName));
                }
            }

            ddlCategory.Items.Insert(0, new ListItem(GetLocalResourceObject("selectCategory").ToString(), ""));
        }

        private void LoadSpecificProperties()
        {

            List<Property> categories = SessionManager.GetAllSpecificProperties();

            ddlSpecificProperty.DataSource = categories;
            ddlSpecificProperty.DataTextField = "name";
            ddlSpecificProperty.DataValueField = "Id";
            ddlSpecificProperty.DataBind();
            ddlSpecificProperty.Items.Insert(0, new ListItem(GetLocalResourceObject("selectSpecificProperty").ToString(), ""));

        }

        private void LoadProduct(long productId)
        {
            Product loadedProduct = SessionManager.FindProduct(productId);

            txtProductName.Text = loadedProduct.name;
            price.Text = loadedProduct.price.ToString();
            txtStock.Text = loadedProduct.stock.ToString();
            ddlCategory.SelectedValue = loadedProduct.Category.categoryName;

            PropertiesList = loadedProduct.ProductPorperty?.ToList() ?? new List<ProductProperty>();

            BindPropertyList();

        }

        private void BindPropertyList()
        {
            propertyList.Visible = PropertiesList.Count > 0;
            rptPropertyList.DataSource = PropertiesList;
            rptPropertyList.DataBind();
        }

        protected void BtnAddPropertyClick(object sender, EventArgs e)
        {
            btnAddProperty.Visible = false;
            specificPropertyContainer.Visible = true;
            txtspecificPropertyValue.Text = "";
            ddlSpecificProperty.SelectedIndex = 0;
            lblspecificPropertyValue.Text = "";
            lblspecificPropertyValue.Visible = false;
            txtspecificPropertyValue.Visible = false;

        }

        protected void ddlSpecificProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSpecificProperty.SelectedValue == "")
            {
                lblspecificPropertyValue.Visible = false;
                txtspecificPropertyValue.Visible = false;
            }
            else
            {
                lblspecificPropertyValue.Visible = true;
                txtspecificPropertyValue.Visible = true;


                lblspecificPropertyValue.Text = ddlSpecificProperty.SelectedItem.Text;
            }
        }

        protected void btnSavePorpertyClick(object sender, EventArgs e)
        {
            string valor = txtspecificPropertyValue.Text.Trim();
            if (!string.IsNullOrEmpty(valor))
            {
                Property property = new Property
                {
                    Id = long.Parse(ddlSpecificProperty.SelectedValue),
                    name = ddlSpecificProperty.SelectedItem.Text
                };

                ProductProperty newProperty = new ProductProperty
                {
                    value = valor,
                    Property = property
                };

                PropertiesList.Add(newProperty);
                BindPropertyList();
            }

            // Resetear formulario
            txtspecificPropertyValue.Text = "";
            ddlSpecificProperty.SelectedIndex = 0;
            btnAddProperty.Visible = true;
            specificPropertyContainer.Visible = false;
            propertyList.Visible = true;
        }

        protected void btnDeleteProperty_Click(object sender, EventArgs e)
        {
            List<ProductProperty> lista = Session["PropertiesList"] as List<ProductProperty>;

            if (lista != null && lista.Count > 0)
            {
                lista.RemoveAt(lista.Count - 1); // Elimina el último elemento
                Session["PropertiesList"] = lista;

                rptPropertyList.DataSource = lista;
                rptPropertyList.DataBind();
            }

            // Guarda la lista actualizada en Session
            Session["PropertiesList"] = lista;

            // Vuelve a bindear el repeater con la lista actualizada
            rptPropertyList.DataSource = lista;
            rptPropertyList.DataBind();
        }


        protected void btnSaveProduct_Click(object sender, EventArgs e)
        {
            UserSession userSession = (UserSession)Context.Session["userSession"];

            string productIdParam = Request.QueryString["productId"];

            if (!string.IsNullOrEmpty(productIdParam) && long.TryParse(productIdParam, out long productId))
            {
                {
                    Product updatedProduct = new Product
                    {
                        Id = productId,
                        name = txtProductName.Text,
                        price = Convert.ToDouble(price.Text),
                        stock = Convert.ToInt32(txtStock.Text)
                    };

                    SessionManager.UpdateProduct(updatedProduct, ddlCategory.SelectedValue, PropertiesList);

                    Session.Remove("PropertiesList");


                    Response.Redirect($"~/Pages/Restaurants/RestaurantProductsList.aspx?id={userSession.UserProfileId}");
                }
            }
        }

    }


}