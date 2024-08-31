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
    public partial class ShowAllRatingAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // make method calls to all drop down list in your page 
            if (!Page.IsPostBack)
            {
                populaterblOverAllRating();
                populateRepeaterControl();
            }
            populateGvShowAllRatings();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void populaterblOverAllRating()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"select * from OverAllRating";
            SqlDataReader dr = myCrud.getDrPassSql(mySql);
            rblOverAllRating.DataTextField = "OverAllRating";
            rblOverAllRating.DataValueField = "OverAllRatingId";
            rblOverAllRating.DataSource = dr;
            rblOverAllRating.DataBind();
        }
        protected void populateRepeaterControl()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"select RestaurantRatingCritiraId,	RestaurantRatingCritira from RestaurantRatingCritira";
            SqlDataReader dr = myCrud.getDrPassSql(mySql);
            repeaterRating.DataSource = dr;
            repeaterRating.DataBind();
        }

        protected void populateGvShowAllRatings()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"SELECT Ru.ratingId, aspu.UserName, r.restaurantName, ov.OverAllRating, Ru.restaurantRatingCritiraAndPoints, Ru.notes
                    FROM RestaurantUserRatings Ru INNER JOIN
                  Restaurant r ON Ru.restaurantId = r.restaurantId INNER JOIN
                  OverAllRating ov ON Ru.overAllRatingId = ov.OverAllRatingId INNER JOIN
                  aspnet_Users aspu ON Ru.userId = aspu.UserId";
            Dictionary<string, object> myPara = new Dictionary<string, object>();
            string userName = User.Identity.Name;
            MembershipUser user = Membership.GetUser(userName);
            Guid userId = (Guid)user.ProviderUserKey;
            myPara.Add("@userId", userId);
            SqlDataReader dr = myCrud.getDrPassSql(mySql, myPara);
            gvShowAllRatings.DataSource = dr;
            gvShowAllRatings.DataBind();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/RatingPage.aspx");
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            // Clear all TextBox controls
            txtRatingId.Text = string.Empty;
            txtNote.Text = string.Empty;

            // Clear all RadioButtonList controls
            rblOverAllRating.ClearSelection();

            // Clear Repeater controls
            foreach (RepeaterItem item in repeaterRating.Items)
            {
                RadioButtonList rblPoints = (RadioButtonList)item.FindControl("rblrestaurantRatingCriteriaPoint");
                if (rblPoints != null)
                {
                    rblPoints.ClearSelection();
                }
            }
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

            int RatingId = int.Parse((sender as LinkButton).CommandArgument);

            string mySql = @"SELECT ratingId, overAllRatingId, notes, RestaurantRatingCritiraAndPoints
                     FROM RestaurantUserRatings WHERE ratingId = @ratingId";

            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@ratingId", RatingId);
            CRUD myCrud = new CRUD();
            using (SqlDataReader dr = myCrud.getDrPassSql(mySql, myPara))
            {
                if (dr.HasRows && dr.Read())
                {
                    // Populate form fields with restaurant details
                    txtRatingId.Text = dr["ratingId"].ToString();
                    rblOverAllRating.SelectedValue = dr["overAllRatingId"].ToString();
                    txtNote.Text = dr["notes"].ToString();

                    // Parse and populate rating criteria and points
                    string criteriaAndPoints = dr["RestaurantRatingCritiraAndPoints"].ToString();
                    Dictionary<string, string> criteriaPointsDict = new Dictionary<string, string>();
                    foreach (var item in criteriaAndPoints.Split(','))
                    {
                        var parts = item.Split(':');
                        if (parts.Length == 2)
                        {
                            string criteriaId = parts[0].Trim();
                            string points = parts[1].Trim();
                            criteriaPointsDict[criteriaId] = points;
                        }
                    }

                    // Populate the Repeater with criteria names and points
                    foreach (RepeaterItem repeaterItem in repeaterRating.Items)
                    {
                        HiddenField hdnCriteriaId = (HiddenField)repeaterItem.FindControl("hdnRestaurantRatingCriteriaId");
                        RadioButtonList rblPoints = (RadioButtonList)repeaterItem.FindControl("rblrestaurantRatingCriteriaPoint");

                        if (hdnCriteriaId != null && rblPoints != null && criteriaPointsDict.ContainsKey(hdnCriteriaId.Value))
                        {
                            rblPoints.SelectedValue = criteriaPointsDict[hdnCriteriaId.Value];
                        }
                    }
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
            return !string.IsNullOrEmpty(txtRatingId.Text) ||
                   rblOverAllRating.SelectedIndex != -1 ||
                   RepeaterHasSelectedItems(repeaterRating);
        }

        private bool CheckBoxListHasSelectedItems(CheckBoxList cbl)
        {
            return cbl.Items.Cast<ListItem>().Any(item => item.Selected);
        }

        private bool RepeaterHasSelectedItems(Repeater repeater)
        {
            foreach (RepeaterItem item in repeater.Items)
            {
                RadioButtonList rblPoints = (RadioButtonList)item.FindControl("rblrestaurantRatingCriteriaPoint");
                if (rblPoints != null && rblPoints.SelectedIndex != -1)
                {
                    return true;
                }
            }
            return false;
        }

        // Event handler specifically for updating ratings only
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string strNotes = txtNote.Text;
            string strOverAllRatingId = rblOverAllRating.SelectedValue;
            string strRatingId = txtRatingId.Text;

            // Construct the RestaurantRatingCritiraAndPoints string
            List<string> criteriaAndPointsList = new List<string>();

            foreach (RepeaterItem ri in repeaterRating.Items)
            {
                HiddenField ratingCriteriaId = (HiddenField)ri.FindControl("hdnRestaurantRatingCriteriaId");
                RadioButtonList myRbl = (RadioButtonList)ri.FindControl("rblrestaurantRatingCriteriaPoint");

                if (ratingCriteriaId != null && myRbl != null)
                {
                    foreach (ListItem item in myRbl.Items)
                    {
                        if (item.Selected)
                        {
                            string criteriaName = GetCriteriaNameById(ratingCriteriaId.Value); // Fetch criteria name by ID
                            int ratingCriteriaPoint = int.Parse(item.Value);
                            criteriaAndPointsList.Add($"{criteriaName}:{ratingCriteriaPoint}");
                        }
                    }
                }
            }
            string restaurantRatingCritiraAndPoints = string.Join(", ", criteriaAndPointsList);

            string mySql = @" UPDATE RestaurantUserRatings
             SET RestaurantRatingCritiraAndPoints = @RestaurantRatingCritiraAndPoints
             , overAllRatingId = @overAllRatingId , notes = @notes
             WHERE ratingId = @ratingId";

            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@ratingId", strRatingId);
            myPara.Add("@restaurantRatingCritiraAndPoints", restaurantRatingCritiraAndPoints);
            myPara.Add("@overAllRatingId", strOverAllRatingId);
            myPara.Add("@notes", strNotes);

            CRUD myCrud = new CRUD();
            int rtn = myCrud.InsertUpdateDelete(mySql, myPara);
            lblOutput.Text = rtn >= 1 ? "Ratings updated successfully!" : "Failed to update ratings.";
            populateGvShowAllRatings();
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            CRUD myCrud = new CRUD();
            string mySql = @"DELETE FROM RestaurantUserRatings WHERE ratingId = @ratingId";
            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@ratingId", txtRatingId.Text);

            int rtn = myCrud.InsertUpdateDelete(mySql, myPara);
            if (rtn >= 1)
            {
                lblOutput.Text = "Operation Successful!";
            }
            else
            {
                lblOutput.Text = "Operation Failed!";
            }
            populateGvShowAllRatings();
        }
        private string GetCriteriaNameById(string criteriaId)
        {
            CRUD myCrud = new CRUD();
            string mySql = "SELECT RestaurantRatingCritira FROM RestaurantRatingCritira WHERE RestaurantRatingCritiraId = @criteriaId";
            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@criteriaId", criteriaId);
            SqlDataReader dr = myCrud.getDrPassSql(mySql, myPara);
            if (dr.Read())
            {
                return dr["RestaurantRatingCritira"].ToString();
            }
            return string.Empty;
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
            ExportGridToExcel(gvShowAllRatings);
        }
   
    }//cls
}//np