using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocToPDFConverter;
using Syncfusion.Pdf;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RestuarantProject.App_Code;
using System.Collections.Generic;

namespace RestuarantProject.Account
{
    public partial class ShowAllRestaurants : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            populateGvShowAllRestaurants();
        }
        protected void populateGvShowAllRestaurants()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"select * from v_allRestaurants";
            SqlDataReader dr = myCrud.getDrPassSql(mySql);
            gvShowAllRestaurants.DataSource = dr;
            gvShowAllRestaurants.DataBind();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/RatingPage.aspx");
        }

        protected void btnCountactUs_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Support/sendEmail.aspx");
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CRUD myCrud = new CRUD();
            string mySql = @"select * from v_allRestaurants
                                where restaurantName like '%'+@restaurantName+'%'";
            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@restaurantName", txtSearch.Text);
            SqlDataReader dr = myCrud.getDrPassSql(mySql, myPara);
            gvShowAllRestaurants.DataSource = dr;
            gvShowAllRestaurants.DataBind();

        }
    }
}