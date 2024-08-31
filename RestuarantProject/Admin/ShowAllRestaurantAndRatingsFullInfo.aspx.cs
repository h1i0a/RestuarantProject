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
    public partial class ShowAllRestaurantAndRatingsFullInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            populateGvShowAllRestaurants();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }
        protected void populateGvShowAllRestaurants()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"select * from v_allRestaurantAndRatingsFullInfo";
            SqlDataReader dr = myCrud.getDrPassSql(mySql);
            gvShowAllRatingsAndRestaurants.DataSource = dr;
            gvShowAllRatingsAndRestaurants.DataBind();
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
            string mySql = @"select * from v_allRestaurantAndRatingsFullInfo
                                where restaurantName like '%'+@restaurantName+'%'";
            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@restaurantName", txtSearch.Text);
            SqlDataReader dr = myCrud.getDrPassSql(mySql, myPara);
            gvShowAllRatingsAndRestaurants.DataSource = dr;
            gvShowAllRatingsAndRestaurants.DataBind();

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
            ExportGridToExcel(gvShowAllRatingsAndRestaurants);
        }
    }
}