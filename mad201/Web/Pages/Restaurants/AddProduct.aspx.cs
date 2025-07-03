using Es.Udc.DotNet.ModelUtil.Exceptions;
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
    public partial class AddProduct : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                LoadCategories();
                LoadSpecificProperties();
            }

            if (PropertiesList.Count > 0)
            {
                propertyList.Visible = true;
                rptPropertyList.DataSource = PropertiesList;
                rptPropertyList.DataBind();
            }
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

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
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

        protected void BtnAddPropertyClick(object sender, EventArgs e)
        {
            btnAddProperty.Visible = false;
            specificPropertyContainer.Visible = true;
        }

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
                Property property = new Property(ddlSpecificProperty.SelectedItem.Text);
                ProductProperty newProperty = new ProductProperty();
                newProperty.value = txtspecificPropertyValue.Text;
                newProperty.Property = property;

                PropertiesList.Add(newProperty);

                rptPropertyList.DataSource = PropertiesList;
                rptPropertyList.DataBind();
            }

            txtspecificPropertyValue.Text = "";
            ddlSpecificProperty.SelectedValue = "";
            ddlSpecificProperty.SelectedIndex = 0;
            btnAddProperty.Visible = true;
            specificPropertyContainer.Visible = false;
            propertyList.Visible = true;
        }

        protected void BtnRegisterClick(object sender, EventArgs e)
        {
            foreach (IValidator validator in Page.Validators)
            {
                if (!validator.IsValid)
                {
                    System.Diagnostics.Debug.WriteLine("Validador fallando: ");
                }
            }
            if (Page.IsValid)
            {
                try
                {
                    UserSession userSession = (UserSession)Context.Session["userSession"];


                    Category category = new Category();
                    category.categoryName = ddlCategory.SelectedValue;

                    Product product =
                        new Product(txtProductName.Text, Convert.ToDouble(price.Text), DateTime.Now, Convert.ToInt32(txtStock.Text), category);

                    SessionManager.RegisterProduct(product, PropertiesList, userSession.UserProfileId);

                    Session.Remove("PropertiesList");


                    Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/MainPage.aspx"));
                }
                catch (DuplicateInstanceException)
                {
                }
            }
        }
    }


}