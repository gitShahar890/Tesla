using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tesla.htmlPages
{
    public partial class AdminUpdateUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Admin"] != "No") // הדף זמין רק למנהל האתר
            {
                string dbFileName = "TeslaDB.accdb";

                if (Request.Form["submitUpdate"] != null)
                {
                    string firstName = Request.Form["firstName"].Trim();
                    string lastName = Request.Form["lastName"].Trim();
                    string gender = Request.Form["gender"].Trim();
                    string email = Request.Form["email"].Trim();
                    string phoneNumber = Request.Form["phoneNumber"].Trim();
                    string password = Request.Form["password"].Trim();
                    string favCar = Request.Form["favCar"].Trim();

                    string sql = "UPDATE tbl_users SET ";
                    sql += "[firstName]='" + firstName + "'";
                    sql += ",[LastName]='" + lastName + "'";
                    sql += ",[Gender]='" + gender + "'";
                    sql += ",[PhoneNumber]='" + phoneNumber + "'";
                    sql += ",[Password]='" + password + "'";
                    sql += ",[FavCar]='" + favCar + "'";
                    sql += " WHERE [Email]='" + email + "'";


                    MyAdoHelper.DoQuery(dbFileName, sql);

                    Response.Redirect("Home.aspx?code=4");// הפנייה לדף
                }
            }
            else // אם המנהל לא מחובר
                Response.Redirect("Home.aspx"); // הפנייה לדף
        }
    }
}