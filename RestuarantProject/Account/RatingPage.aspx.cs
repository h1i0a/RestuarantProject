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
    public partial class RatingPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // make method calls to all drop down list in your page 
            if (!Page.IsPostBack)
            {
                populaterblOverAllRating();
                populateRepeaterControl();
                populateDdlRestaurant();
            }
        }
        protected void btnShowALLRatings_Click(object sender, EventArgs e)
        {
            Response.Redirect("ShowAllRatings.aspx");

        }

        protected void btnSubmitNewRestaurant_Click(object sender, EventArgs e)
        {
            Response.Redirect("restuarant.aspx");
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
        protected void populateDdlRestaurant()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"select * from Restaurant";
            SqlDataReader dr = myCrud.getDrPassSql(mySql);
            ddlRestaurant.DataTextField = "RestaurantName";
            ddlRestaurant.DataValueField = "RestaurantId";
            ddlRestaurant.DataSource = dr;
            ddlRestaurant.DataBind();
        }
        protected void populateRepeaterControl()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"select RestaurantRatingCritiraId,	RestaurantRatingCritira from RestaurantRatingCritira";
            SqlDataReader dr = myCrud.getDrPassSql(mySql);
            repeaterRating.DataSource = dr;
            repeaterRating.DataBind();
        }
        protected void btnSubmit_Click_Rating(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                // Validation code...
                string strUserName = User.Identity.Name;
                MembershipUser user = Membership.GetUser(strUserName);
                if (user == null)
                {
                    lblOutput.Text = "User not found! (Please Register First)";
                    return;
                }
                // ... (Validation code remains the same)

                Guid gidUserId = (Guid)user.ProviderUserKey;
                string strRestaurant = ddlRestaurant.SelectedValue;
                string strNotes = txtNote.Text;
                string strOverAllRating = rblOverAllRating.SelectedValue;

                bool operationSuccessful = true;
                List<string> criteriaAndPoints = new List<string>();

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
                                string criteriaName = getCriteriaNameById(ratingCriteriaId.Value); // Fetch criteria name by ID
                                int ratingCriteriaPoint = int.Parse(item.Value);
                                criteriaAndPoints.Add($"{criteriaName}:{ratingCriteriaPoint}");
                            }
                        }
                    }
                }

                string ratingCriteriaAndPoints = string.Join(", ", criteriaAndPoints);



                //string x = getRestaurantId(TxtRestaurantName.Text).ToString();
                int rtn = insertRating(gidUserId, strRestaurant, strOverAllRating, ratingCriteriaAndPoints, strNotes);





                if (rtn < 1)
                {
                    operationSuccessful = false;
                }

                if (operationSuccessful)
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
                lblOutput.Text = "User not authenticated! (Please Register or Login First)";
            }
        }

        private string getCriteriaNameById(string criteriaId)
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

        protected int insertRating(Guid gidUserId, string strRestaurant, string strOverAllRating, string strRestaurantRatingCritiraAndPoints, string strNote)
        {
            CRUD myCrud = new CRUD();
            //string mySql = @"INSERT INTO RestaurantRating (restaurantId, userId, OverAllRatingId, Notes, RestaurantRatingCritiraAndPoints)
            //                        SELECT r.restaurantId, @userId,  @OverAllRatingId, @Notes, @RestaurantRatingCritiraAndPoints
            //                        FROM Restaurant r
            //                        WHERE r.restaurantName = @restaurantName";
            string mySql = @"INSERT INTO RestaurantUserRatings(userId, restaurantId, 
                                     overAllRatingId, notes, restaurantRatingCritiraAndPoints)
						VALUES (@userId, @restaurantId, @overAllRatingId, @notes, @restaurantRatingCritiraAndPoints)";
            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@userId", gidUserId);
            myPara.Add("@restaurantId", strRestaurant);
            myPara.Add("@overAllRatingId", strOverAllRating);
            myPara.Add("@notes", strNote);
            myPara.Add("@restaurantRatingCritiraAndPoints", strRestaurantRatingCritiraAndPoints);
            return myCrud.InsertUpdateDelete(mySql, myPara);

        }




        protected void btnShowMyRatings_Click(object sender, EventArgs e)
        {
            // Get the current user's ID as a string using ASP.NET Membership
            MembershipUser user = Membership.GetUser();
            if (user != null)
            {
                Guid userId = (Guid)user.ProviderUserKey;
                bool hasRatings = CheckIfUserHasRatings(userId);

                if (hasRatings)
                {
                    Response.Redirect("ShowMyRatings.aspx");
                }
                else
                {
                    lblOutput.Text = "You need to rate first.";
                    lblOutput.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                lblOutput.Text = "User not authenticated! YOU NEED TO REGISTER OR LOGIN!!";
                lblOutput.ForeColor = System.Drawing.Color.Red;
            }
        }


        private bool CheckIfUserHasRatings(Guid userId)
        {
            string mySql = "SELECT COUNT(*) FROM RestaurantUserRatings WHERE userId = @userId";
            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@userId", userId);
            CRUD crud = new CRUD();
            int count = 0;
            SqlDataReader dr = crud.getDrPassSql(mySql, myPara);
            {
                if (dr.Read())
                {
                    count = dr.GetInt32(0);
                }
            }

            return count > 0;
        }
    }
}