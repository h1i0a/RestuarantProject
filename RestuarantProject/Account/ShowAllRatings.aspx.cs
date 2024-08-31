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
    public partial class ShowAllRatings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            populateGvShowAllRatings();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected void populateGvShowAllRatings()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"select * from v_allUserRatings";
            SqlDataReader dr = myCrud.getDrPassSql(mySql);
            gvShowAllRatings.DataSource = dr;
            gvShowAllRatings.DataBind();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/RatingPage.aspx");
        }

        protected DataTable getDt()
        {
            string mySql = @"select * from v_allRestaurantAndRatingsFullInfo";
            CRUD myCrud = new CRUD();
            DataTable dt = myCrud.getDT(mySql);
            return dt;
        }

        protected DataTable GetRecipients()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"select * from v_allRestaurantAndRatingsFullInfo";
            DataTable dt = myCrud.getDT(mySql);
            return dt;
        }

        protected void btnExportToWordPdf_Click(object sender, EventArgs e)
        {
            DataTable restaurantRatings = GetRecipients();

            var visitors = restaurantRatings.AsEnumerable()
                .GroupBy(row => row.Field<string>("UserName"));

            string resultPath = Server.MapPath("~/Result/");
            if (!Directory.Exists(resultPath))
                Directory.CreateDirectory(resultPath);

            foreach (var visitorGroup in visitors)
            {
                string visitorName = visitorGroup.Key;

                using (WordDocument document = new WordDocument())
                {
                    // Open the template Word document
                    document.Open(Server.MapPath("~/myTemplate/Restaurant Rating template.docx"));

                    // Replace the user's name in the template
                    document.Replace("«userName»", visitorName, true, true);

                    WTable templateTable = document.Sections[0].Tables[0] as WTable;

                    bool isFirstTable = true;

                    foreach (var restaurantGroup in visitorGroup.GroupBy(row => row.Field<int>("ratingId")))
                    {
                        // Clone the table template for each restaurant
                        WTable table = templateTable.Clone();

                        if (!isFirstTable)
                        {
                            // Create a new section for each subsequent table
                            WSection newSection = document.AddSection() as WSection;
                            newSection.Body.ChildEntities.Add(table);
                        }
                        else
                        {
                            // Add the first table to the existing section
                            document.Sections[0].Body.ChildEntities.Add(table);
                            isFirstTable = false;
                        }

                        foreach (var restaurantRow in restaurantGroup)
                        {
                            // Populate the table with restaurant data
                            table.Replace("«restaurantName»", restaurantRow.Field<string>("restaurantName"), true, true);
                            table.Replace("«overAllRating»", restaurantRow.Field<string>("OverAllRating"), true, true);
                            table.Replace("«Country»", restaurantRow.Field<string>("Country"), true, true);
                            table.Replace("«City»", restaurantRow.Field<string>("City"), true, true);
                            table.Replace("«Address»", restaurantRow.Field<string>("Address"), true, true);
                            table.Replace("«Cell»", restaurantRow.Field<string>("Cell"), true, true);
                            table.Replace("«pricePerPerson»", restaurantRow.Field<decimal>("pricePerPerson").ToString("F2"), true, true);
                            table.Replace("«RestaurantServices»", restaurantRow.Field<string>("RestaurantServices"), true, true);
                            table.Replace("«RestaurantFoodType»", restaurantRow.Field<string>("RestaurantFoodType"), true, true);
                            table.Replace("«RestaurantFoodTime»", restaurantRow.Field<string>("RestaurantFoodTime"), true, true);
                            table.Replace("«Notes»", restaurantRow.Field<string>("Notes"), true, true);
                            table.Replace("«restaurantRatingCritiraAndPoints»", restaurantRow.Field<string>("restaurantRatingCritiraAndPoints"), true, true);
                        }
                    }

                    // Remove the template table after processing all tables
                    document.Sections[0].Body.ChildEntities.Remove(templateTable);

                    // Save the document as Word
                    string wordFilePath = Server.MapPath($"~/Result/RestaurantRatings_{visitorName}.docx");
                    document.Save(wordFilePath, FormatType.Docx);

                    // Convert and save as PDF
                    DocToPDFConverter converter = new DocToPDFConverter();
                    PdfDocument pdfDocument = converter.ConvertToPDF(document);
                    converter.Dispose();
                    string pdfFilePath = Server.MapPath($"~/Result/RestaurantRatings_{visitorName}.pdf");
                    pdfDocument.Save(pdfFilePath);
                    pdfDocument.Close(true);
                }
            }

            // Create links to download the files
            lblOutput.Text = "Export Completed!<br/>";
            foreach (var visitorGroup in visitors)
            {
                string visitorName = visitorGroup.Key;
                string wordFileName = $"RestaurantRatings_{visitorName}.docx";
                string pdfFileName = $"RestaurantRatings_{visitorName}.pdf";
                lblOutput.Text += $"<a href='{ResolveUrl($"~/Result/{wordFileName}")}' download>{wordFileName}</a><br/>";
                lblOutput.Text += $"<a href='{ResolveUrl($"~/Result/{pdfFileName}")}' download>{pdfFileName}</a><br/>";
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CRUD myCrud = new CRUD();
            string mySql = @"select * from v_allUserRatings where restaurantName like '%'+@restaurantName+'%'";
            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@restaurantName", txtSearch.Text);
            SqlDataReader dr = myCrud.getDrPassSql(mySql, myPara);
            gvShowAllRatings.DataSource = dr;
            gvShowAllRatings.DataBind();
        }
    }
}