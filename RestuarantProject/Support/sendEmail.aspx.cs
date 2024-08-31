using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using RestuarantProject.App_Code;

namespace RestuarantProject.demo
{
    public partial class sendEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSendMailViaMailMgr_Click(object sender, EventArgs e)
        {
            sendEmail2();
        }

        private void InsertContactUs()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"Insert contactus(senderEmail,subject, message) 
                        values (@senderEmail,@subject,@message)";
            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@senderEmail", txtSenderEmail.Text);
            myPara.Add("@subject", txtSubject.Text);
            myPara.Add("@message", txtBody.Text);
            int rtn = myCrud.InsertUpdateDelete(mySql, myPara);
            if (rtn >= 1)
            {
                lblMsg.Text = "Email log Successed";
                lblMsg.ForeColor = Color.Green;
            }
            else
            {
                lblMsg.Text = "Email log failed";
                lblMsg.ForeColor = Color.Red;
            }
        }

        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        protected void sendEmail2()
        {
            int intSenderId = 111;
            string senderName = Page.User.Identity.Name;
            senderName = (string.IsNullOrWhiteSpace(senderName)) ? "Admin" : Page.User.Identity.Name;
            string rtn = "";

            if (string.IsNullOrEmpty(txtSubject.Text) || string.IsNullOrEmpty(txtBody.Text))
            {
                lblMsg.Text = "Please fill Subject & email body";
                lblMsg.ForeColor = Color.Red;
                return;
            }

            string senderEmail = txtSenderEmail.Text;

            if (!IsValidEmail(senderEmail))
            {
                lblMsg.Text = "Please enter a valid email address.";
                lblMsg.ForeColor = Color.Red;
                return;
            }

            using (mailMgr myMailMgr = new mailMgr())
            {
                myMailMgr.mySubject = txtSubject.Text + " " + senderEmail + ": " + senderName;
                myMailMgr.myBody = txtBody.Text;
                if (fuAttachment.HasFile)
                {
                    foreach (HttpPostedFile file in fuAttachment.PostedFiles)
                    {
                        string fileName = Path.GetFileName(file.FileName);
                    }

                    rtn = myMailMgr.sendEmailViaGmail(fuAttachment);
                    lblMsg.Text = rtn;
                    lblMsg.ForeColor = Color.Green;
                    lblMsg.BackColor = Color.Yellow;
                }
                else
                {
                    rtn = myMailMgr.sendEmailViaGmail();
                }
                lblMsg.Text = rtn;
                lblMsg.ForeColor = Color.Green;
                lblMsg.BackColor = Color.Yellow;
            }
        }

        protected void InsertDocuments(int myClientId)
        {
            foreach (HttpPostedFile postedFile in fuAttachment.PostedFiles)
            {
                string filename = Path.GetFileName(postedFile.FileName);
                string contentType = postedFile.ContentType;
                using (Stream fs = postedFile.InputStream)
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        byte[] bytes = br.ReadBytes((Int32)fs.Length);
                        CRUD DocInsert = new CRUD();
                        string mySql = @"insert into trainerDoc(trainerId,DocName,contentType,DocData) 
                                          values (@trainerId,@DocName,@contentType,@DocData)";
                        Dictionary<string, object> myPara = new Dictionary<string, object>();
                        myPara.Add("@trainerId", myClientId);
                        myPara.Add("@DocName", filename);
                        myPara.Add("@contentType", contentType);
                        myPara.Add("@DocData", bytes);
                        int rtn = DocInsert.InsertUpdateDelete(mySql, myPara);
                        lblMsg.Text = rtn.ToString();
                    }
                }
            }
        }

        protected void lblOutputClear_txtSubject(object sender, EventArgs e)
        {
            lblMsg.Text = "";
        }

        protected void btnSendViaCode_Click(object sender, EventArgs e)
        {

        }
    }
}
