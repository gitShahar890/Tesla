using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tesla.htmlPages
{
    public partial class AdminDeleteUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Admin"] != "No") // הדף זמין רק למנהל האתר
            {
                string email = Request.QueryString["userMail"];
                if (email != null) // הפרמטר של האימייל לא עבר מסיבה כלשהי
                {
                    string dbFileName = "TeslaDB.accdb";
                    string sql = "DELETE * FROM tbl_users WHERE Email='" + email + "'"; // מחיקת המשתמש

                    MyAdoHelper.DoQuery(dbFileName, sql);
                }
                Response.Redirect("Home.aspx?code=4"); // הפנייה לדף
            }
            else // אם המנהל לא מחובר
                Response.Redirect("Home.aspx"); // הפנייה לדף
        }
    }
}