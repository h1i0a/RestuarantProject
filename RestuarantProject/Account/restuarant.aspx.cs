using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using RestuarantProject.App_Code;

namespace RestuarantProject.Account
{
    public partial class restuarant : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // make method calls to all drop down list in your page 
            if (!Page.IsPostBack)
            {
                populateDdlCountry();
                populateCblFoodtype();
                populateCblRestuarantServices();
                populatecblFoodTime();
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("RatingPage.aspx");
        }
        protected void populateDdlCountry()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"select * from country";
            SqlDataReader dr = myCrud.getDrPassSql(mySql);
            ddlCountry.DataTextField = "country";
            ddlCountry.DataValueField = "countryId";
            ddlCountry.DataSource = dr;
            ddlCountry.DataBind();
        }
        protected void populateCblFoodtype()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"select RestaurantFoodTypeId, RestaurantFoodType from RestaurantFoodType";
            SqlDataReader dr = myCrud.getDrPassSql(mySql);
            cblFoodtype.DataTextField = "RestaurantFoodType";
            cblFoodtype.DataValueField = "RestaurantFoodTypeId";
            cblFoodtype.DataSource = dr;
            cblFoodtype.DataBind();
        }
        protected void populateCblRestuarantServices()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"select RestaurantServicesId, RestaurantServices from RestaurantServices";
            SqlDataReader dr = myCrud.getDrPassSql(mySql);
            cblRestuarantServices.DataTextField = "RestaurantServices";
            cblRestuarantServices.DataValueField = "RestaurantServicesId";
            cblRestuarantServices.DataSource = dr;
            cblRestuarantServices.DataBind();
        }

        protected void populatecblFoodTime()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"select RestaurantFoodTimeId, RestaurantFoodTime from RestaurantFoodTime";
            SqlDataReader dr = myCrud.getDrPassSql(mySql);
            cblFoodTime.DataTextField = "RestaurantFoodTime";
            cblFoodTime.DataValueField = "RestaurantFoodTimeId";
            cblFoodTime.DataSource = dr;
            cblFoodTime.DataBind();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                // Validation code...
                string userName = User.Identity.Name;
                MembershipUser user = Membership.GetUser(userName);
                if (user == null)
                {
                    lblOutput.Text = "User not found! (Please Register First)";
                    return;
                }
                // ... (Validation code remains the same)

                string strFoodTime = GetSelectedItems(cblFoodTime);
                string strFoodtype = GetSelectedItems(cblFoodtype);
                string strRestuarantServices = GetSelectedItems(cblRestuarantServices);

                if (string.IsNullOrEmpty(strFoodTime) || string.IsNullOrEmpty(strFoodtype) || string.IsNullOrEmpty(strRestuarantServices))
                {
                    lblOutput.Text = "Please select at least one item in each check box category!";
                    lblOutput.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                Guid gidUserId = (Guid)user.ProviderUserKey;
                string strRestaurantName = TxtRestaurantName.Text;
                string strAddress = txtAddress.Text;
                string strCity = txtCity.Text;
                string strCell = txtCell.Text;
                string strpricePerPerson = txtPricePerPerson.Text;
                string DdlCountry = ddlCountry.SelectedValue;

                CRUD myCrud = new CRUD();
                string mySql = @"INSERT INTO Restaurant(userId, restaurantName, CountryId, City, Address, Cell, pricePerPerson, RestaurantServices, RestaurantFoodType, RestaurantFoodTime)
             VALUES (@userId, @restaurantName, @CountryId, @City, @Address, @Cell, @pricePerPerson, @RestaurantServices, @RestaurantFoodType, @RestaurantFoodTime)";
                Dictionary<string, object> myPara = new Dictionary<string, object>();
                myPara.Add("@userId", gidUserId);
                myPara.Add("@restaurantName", strRestaurantName);
                myPara.Add("@CountryId", DdlCountry);
                myPara.Add("@City", strCity);
                myPara.Add("@Address", strAddress);
                myPara.Add("@Cell", strCell);
                myPara.Add("@pricePerPerson", strpricePerPerson);
                myPara.Add("@RestaurantServices", strRestuarantServices);
                myPara.Add("@RestaurantFoodType", strFoodtype);
                myPara.Add("@RestaurantFoodTime", strFoodTime);
                int rtn = myCrud.InsertUpdateDelete(mySql, myPara);

                if (rtn >= 1)
                {
                    lblOutput.Text = "Operation Successful!";
                }
                else
                {
                    lblOutput.Text = "Operation Failed!";
                }
            }
            else
            {
                lblOutput.Text = "User not authenticated!";
            }
        }

        private string GetSelectedItems(CheckBoxList cbl)
        {
            List<string> selectedItems = new List<string>();
            foreach (ListItem item in cbl.Items)
            {
                if (item.Selected)
                {
                    selectedItems.Add(item.Text);
                }
            }
            return string.Join(", ", selectedItems);
        }

    }//cls
}//np

