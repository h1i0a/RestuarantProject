using RestuarantProject.App_Code;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestuarantProject.Admin
{
    public partial class ShowAllRestaurantsAdmin : System.Web.UI.Page
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
            populateGvShowAllRestaurants();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

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
        protected void populateGvShowAllRestaurants()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"SELECT r.restaurantId, r.restaurantName, c.Country, r.City, r.Address, r.Cell, r.pricePerPerson, r.RestaurantServices, r.RestaurantFoodType, r.RestaurantFoodTime
                                FROM Restaurant r INNER JOIN Country c ON r.CountryId = c.CountryId";
            Dictionary<string, object> myPara = new Dictionary<string, object>();
            string userName = User.Identity.Name;
            MembershipUser user = Membership.GetUser(userName);
            Guid userId = (Guid)user.ProviderUserKey;
            myPara.Add("@userId", userId);
            SqlDataReader dr = myCrud.getDrPassSql(mySql, myPara);
            gvShowAllRestaurants.DataSource = dr;
            gvShowAllRestaurants.DataBind();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/RatingPage.aspx");
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            // Clear all TextBox controls
            txtRestaurantId.Text = string.Empty;
            TxtRestaurantName.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtCell.Text = string.Empty;
            txtPricePerPerson.Text = string.Empty;

            // Clear all CheckBoxList controls
            ClearCheckBoxList(cblRestuarantServices);
            ClearCheckBoxList(cblFoodtype);
            ClearCheckBoxList(cblFoodTime);
        }

        private void ClearCheckBoxList(CheckBoxList cbl)
        {
            foreach (ListItem item in cbl.Items)
            {
                item.Selected = false;
            }
        }

        protected void populateForm_Click(object sender, EventArgs e)
        {
            if (FormHasValues())
            {
                lblOutput.Text = "Please press clear button before populating again.";
                return;
            }

            int restaurantId = int.Parse((sender as LinkButton).CommandArgument);

            string mySql = @"SELECT restaurantId, restaurantName, countryId, city, address, cell, pricePerPerson, 
                     restaurantServices, restaurantFoodType, restaurantFoodTime FROM Restaurant WHERE restaurantId = @restaurantId;";

            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@restaurantId", restaurantId);
            CRUD myCrud = new CRUD();
            using (SqlDataReader dr = myCrud.getDrPassSql(mySql, myPara))
            {
                if (dr.HasRows && dr.Read())
                {
                    // Populate form fields with restaurant details
                    txtRestaurantId.Text = dr["restaurantId"].ToString();
                    TxtRestaurantName.Text = dr["restaurantName"].ToString();
                    ddlCountry.SelectedValue = dr["countryId"].ToString();
                    txtCity.Text = dr["city"].ToString();
                    txtAddress.Text = dr["address"].ToString();
                    txtCell.Text = dr["cell"].ToString();
                    txtPricePerPerson.Text = dr["pricePerPerson"].ToString();
                    SetSelectedItems(cblRestuarantServices, dr["restaurantServices"].ToString());
                    SetSelectedItems(cblFoodtype, dr["restaurantFoodType"].ToString());
                    SetSelectedItems(cblFoodTime, dr["restaurantFoodTime"].ToString());
                }
            }
        }

        // Utility method to set selected items in CheckBoxList based on a comma-separated string
        private void SetSelectedItems(CheckBoxList cbl, string selectedValues)
        {
            if (string.IsNullOrEmpty(selectedValues)) return;

            string[] selectedArray = selectedValues.Split(',');
            foreach (ListItem item in cbl.Items)
            {
                item.Selected = selectedArray.Contains(item.Value);
            }
        }





        private bool FormHasValues()
        {
            return !string.IsNullOrEmpty(txtRestaurantId.Text) ||
                   !string.IsNullOrEmpty(TxtRestaurantName.Text) ||
                   !string.IsNullOrEmpty(txtCity.Text) ||
                   !string.IsNullOrEmpty(txtAddress.Text) ||
                   !string.IsNullOrEmpty(txtCell.Text) ||
                   !string.IsNullOrEmpty(txtPricePerPerson.Text) ||
                   CheckBoxListHasSelectedItems(cblRestuarantServices) ||
                   CheckBoxListHasSelectedItems(cblFoodtype) ||
                   CheckBoxListHasSelectedItems(cblFoodTime);
        }

        private bool CheckBoxListHasSelectedItems(CheckBoxList cbl)
        {
            return cbl.Items.Cast<ListItem>().Any(item => item.Selected);
        }

        // Event handler for updating both restaurant details and ratings
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string restaurantId = txtRestaurantId.Text;
            string restaurantName = TxtRestaurantName.Text;
            string countryId = ddlCountry.SelectedValue;
            string city = txtCity.Text;
            string address = txtAddress.Text;
            string cell = txtCell.Text;
            string pricePerPerson = txtPricePerPerson.Text;
            string restaurantServices = GetSelectedItems(cblRestuarantServices);
            string restaurantFoodType = GetSelectedItems(cblFoodtype);
            string restaurantFoodTime = GetSelectedItems(cblFoodTime);

            string mySql = @"
        UPDATE Restaurant 
        SET restaurantName = @restaurantName, 
        countryId = @countryId, 
        city = @city, 
        address = @address, 
        cell = @cell, 
        pricePerPerson = @pricePerPerson, 
        restaurantServices = @restaurantServices, 
        restaurantFoodType = @restaurantFoodType, 
        restaurantFoodTime = @restaurantFoodTime 
        WHERE restaurantId = @restaurantId";

            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@restaurantId", restaurantId);
            myPara.Add("@restaurantName", restaurantName);
            myPara.Add("@countryId", countryId);
            myPara.Add("@city", city);
            myPara.Add("@address", address);
            myPara.Add("@cell", cell);
            myPara.Add("@pricePerPerson", pricePerPerson);
            myPara.Add("@restaurantServices", restaurantServices);
            myPara.Add("@restaurantFoodType", restaurantFoodType);
            myPara.Add("@restaurantFoodTime", restaurantFoodTime);

            CRUD myCrud = new CRUD();
            int rtn = myCrud.InsertUpdateDelete(mySql, myPara);
            lblOutput.Text = rtn >= 1 ? "Restaurant details updated successfully!" : "Failed to update restaurant details.";
            populateGvShowAllRestaurants();
        }

        // Utility method to get selected items from a CheckBoxList as a comma-separated string
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
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            CRUD myCrud = new CRUD();
            string mySql = @"DELETE FROM Restaurant WHERE restaurantId = @restaurantId";
            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@restaurantId", txtRestaurantId.Text);

            int rtn = myCrud.InsertUpdateDelete(mySql, myPara);
            if (rtn >= 1)
            {
                lblOutput.Text = "Operation Successful!";
            }
            else
            {
                lblOutput.Text = "Operation Failed!";
            }
            populateGvShowAllRestaurants();
        }
        public static void ExportGridToExcel(GridView myGv) // working 1
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Charset = "";
            string FileName = "ExportedReport_" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            myGv.GridLines = GridLines.Both;
            myGv.HeaderStyle.Font.Bold = true;
            myGv.RenderControl(htmltextwrtter);
            HttpContext.Current.Response.Write(strwritter.ToString());
            HttpContext.Current.Response.End();
        }
        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            ExportGridToExcel(gvShowAllRestaurants);
        }

    }
}