using System;
using System.Collections.Generic;
using System.Data;
using System.EnterpriseServices;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Tesla
{
    public partial class homePage : System.Web.UI.Page
    {
        public string adm = "";
        public string edit = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //רישום משתמש
            if (Request.Form["submit"] != null)//כניסה משתמש רגיל
            {
                string dbFileName = "TeslaDB.accdb";
                string sql;

                string firstName = Request.Form["firstName"];
                string lastName = Request.Form["lastName"];
                string gender = Request.Form["gender"];
                string date = Request.Form["date"];
                string email = Request.Form["email"];
                string phoneNumber = Request.Form["phoneNumber"];
                string password = Request.Form["password"];
                string favCar = Request.Form["favCar"];

                sql = "SELECT * FROM tbl_users WHERE Email = '" + email + "'";

                if (MyAdoHelper.IsExist(dbFileName, sql))
                    Response.Redirect("Home.aspx?code=1");
                else
                {
                    sql = "INSERT INTO tbl_users ";
                    sql += "(firstName,LastName,[Gender],[Date], Email,PhoneNumber,[Password],FavCar,admin) ";
                    sql += "VALUES (";
                    sql += "'" + firstName + "'";
                    sql += ",'" + lastName + "'";
                    sql += ",'" + gender + "'";
                    sql += ",'" + date + "'";
                    sql += ",'" + email + "'";
                    sql += ",'" + phoneNumber + "'";
                    sql += ",'" + password + "'";
                    sql += ",'" + favCar + "'";
                    sql += ",'No'";
                    sql += ")";

                    Session["firstName"] = firstName;
                    MyAdoHelper.DoQuery(dbFileName, sql);
                    Response.Redirect("Home.aspx");
                }
            }
            //כניסה משתמש
            else if (Request.Form["submitLogin"] != null)
            {
                string sql, firstName;
                string dbFileName = "TeslaDB.accdb";
                string userMail = Request.Form["logEmail"];
                string userPhone = Request.Form["logPhone"];
                string userPassword = Request.Form["logPass"];

                sql = "SELECT * FROM tbl_users WHERE Email = '" + userMail + "' AND Password = '" + userPassword + "'";

                firstName = MyAdoHelper.GetItemRowData(dbFileName, sql, 0);

                if (firstName != "")
                {
                    Session["firstName"] = firstName;
                    Session["Admin"] = (string)MyAdoHelper.GetItemRowData(dbFileName, sql, 8);
                    if (Session["Admin"] != "No")
                    {
                        dbFileName = "TeslaDB.accdb";
                        string userGender = Request.Form["userGender"];
                        string FavCar = Request.Form["FavCar"];

                        // Construct the search query based on selected criteria
                        sql = "SELECT * FROM tbl_users";
                        bool sqlChanged = false;

                        if (userGender != null)
                        {
                            sql += " WHERE Gender ='" + userGender + "'";
                            sqlChanged = true;
                        }

                        if (FavCar != null && FavCar != "choose")
                        {
                            if (sqlChanged)
                                sql += " AND FavCar ='" + FavCar + "'";
                            else
                                sql += " WHERE FavCar ='" + FavCar + "'";
                        }

                        // Build the table with user data
                        DataTable table = MyAdoHelper.ExecuteDataTable(dbFileName, sql);
                        int length = table.Rows.Count;
                        adm = "";
                        if (length > 0)
                        {
                            adm = "<table class='styled-table'>";
                            adm += "<thead><tr>";
                            adm += "<th>First Name</th>";
                            adm += "<th>Last Name</th>";
                            adm += "<th>Gender</th>";
                            adm += "<th>Birth Date</th>";
                            adm += "<th>Email</th>";
                            adm += "<th>Phone Number</th>";
                            adm += "<th>Password</th>";
                            adm += "<th>Fav Car</th>";
                            adm += "<th>Admin</th>";
                            adm += "<th colspan='2'>Actions</th>";
                            adm += "</tr></thead>";
                            adm += "<tbody>";

                            for (int i = 0; i < length; i++)
                            {
                                adm += "<tr>";
                                adm += "<form method='post' action='Home.aspx?code=3'>";
                                adm += "<td><input type='text' class='inputToLabelLarge' name='userFname' value='" + table.Rows[i]["firstName"] + "' readonly/></td>";
                                adm += "<td><input type='text' class='inputToLabel' name='userLname' value='" + table.Rows[i]["LastName"] + "' readonly/></td>";
                                adm += "<td><input type='text' class='inputToLabel' name='userGender' value='" + table.Rows[i]["Gender"] + "' readonly/></td>";
                                adm += "<td><input type='text' class='inputToLabel' name='UserDate' value='" + table.Rows[i]["Date"] + "' readonly/></td>";
                                adm += "<td><input type='text' class='inputToLabel' name='userMail' value='" + table.Rows[i]["Email"] + "' readonly/></td>";
                                adm += "<td><input type='text' class='inputToLabel' name='userPhone' value='" + table.Rows[i]["PhoneNumber"] + "' readonly/></td>";
                                adm += "<td><input type='text' class='inputToLabel' name='userPwd' value='" + table.Rows[i]["Password"] + "' readonly/></td>";
                                adm += "<td><input type='text' class='inputToLabel' name='FavCar' value='" + table.Rows[i]["FavCar"] + "' readonly/></td>";
                                adm += "<td><input type='text' class='inputToLabel' name='AdminOrNot' value='" + table.Rows[i]["admin"] + "' readonly/></td>";

                                adm += "<td><input type='image' id='submitUpdate' name='submitUpdate' src='../Images/update.png' width='30' height='25' alt='עדכון פרטי משתמש' /></td>";
                                adm += "<td><a href='AdminDeleteUser.aspx?userMail=" + table.Rows[i]["Email"] + "'><img src='../Images/delete.png' width='30' height='25' alt='מחיקת משתמש מבסיס הנתונים'/></a></td>";

                                adm += "</form>";
                                adm += "</tr>";
                            }

                            adm += "</tbody></table>";
                        }
                        else
                        {
                            adm = "אין ערכים תואמים לחיפוש";
                        }

                    }//
                }
                else
                {
                    Response.Redirect("Home.aspx?code=2");
                }
            }
            //פילטר באדמין
            else if (Request.Form["submitSearch"] != null)
            {
                string sql;
                string dbFileName = "TeslaDB.accdb";
                string userGender = Request.Form["userGender"];
                string FavCar = Request.Form["FavCar"];

                // Construct the search query based on selected criteria
                sql = "SELECT * FROM tbl_users";
                bool sqlChanged = false;

                if (userGender != null)
                {
                    sql += " WHERE Gender ='" + userGender + "'";
                    sqlChanged = true;
                }

                if (FavCar != null && FavCar != "choose")
                {
                    if (sqlChanged)
                        sql += " AND FavCar ='" + FavCar + "'";
                    else
                        sql += " WHERE FavCar ='" + FavCar + "'";
                }

                // Build the table with user data
                DataTable table = MyAdoHelper.ExecuteDataTable(dbFileName, sql);
                int length = table.Rows.Count;
                adm = "";
                if (length > 0)
                {
                    adm = "<table class='styled-table'>";
                    adm += "<thead><tr>";
                    adm += "<th>First Name</th>";
                    adm += "<th>Last Name</th>";
                    adm += "<th>Gender</th>";
                    adm += "<th>Birth Date</th>";
                    adm += "<th>Email</th>";
                    adm += "<th>Phone Number</th>";
                    adm += "<th>Password</th>";
                    adm += "<th>Fav Car</th>";
                    adm += "<th>Admin</th>";
                    adm += "<th colspan='2'>Actions</th>";
                    adm += "</tr></thead>";
                    adm += "<tbody>";

                    for (int i = 0; i < length; i++)
                    {
                        adm += "<tr>";
                        adm += "<form method='post' action='Home.aspx?code=3'>";
                        adm += "<td><input style=\"background:none; border:none; color:white; font-weight:bold; text-align:center;\" type='text' class='inputToLabelLarge' name='userFname' value='" + table.Rows[i]["firstName"] + "' readonly/></td>";
                        adm += "<td><input style=\"background:none; border:none; color:white; font-weight:bold; text-align:center;\" type='text' class='inputToLabel' name='userLname' value='" + table.Rows[i]["LastName"] + "' readonly/></td>";
                        adm += "<td><input style=\"background:none; border:none; color:white; font-weight:bold; text-align:center;\" type='text' class='inputToLabel' name='userGender' value='" + table.Rows[i]["Gender"] + "' readonly/></td>";
                        adm += "<td><input style=\"background:none; border:none; color:white; font-weight:bold; text-align:center;\" type='text' class='inputToLabel' name='UserDate' value='" + table.Rows[i]["Date"] + "' readonly/></td>";
                        adm += "<td><input style=\"background:none; border:none; color:white; font-weight:bold; text-align:center;\" type='text' class='inputToLabel' name='userMail' value='" + table.Rows[i]["Email"] + "' readonly/></td>";
                        adm += "<td><input style=\"background:none; border:none; color:white; font-weight:bold; text-align:center;\" type='text' class='inputToLabel' name='userPhone' value='" + table.Rows[i]["PhoneNumber"] + "' readonly/></td>";
                        adm += "<td><input style=\"background:none; border:none; color:white; font-weight:bold; text-align:center;\" type='text' class='inputToLabel' name='userPwd' value='" + table.Rows[i]["Password"] + "' readonly/></td>";
                        adm += "<td><input style=\"background:none; border:none; color:white; font-weight:bold; text-align:center;\" type='text' class='inputToLabel' name='FavCar' value='" + table.Rows[i]["FavCar"] + "' readonly/></td>";
                        adm += "<td><input style=\"background:none; border:none; color:white; font-weight:bold; text-align:center;\" type='text' class='inputToLabel' name='AdminOrNot' value='" + table.Rows[i]["admin"] + "' readonly/></td>";

                        adm += "<td><input type='image' id='submitUpdate' name='submitUpdate' src='../Images/update.png' width='30' height='25' alt='עדכון פרטי משתמש' /></td>";
                        adm += "<td><a href='AdminDeleteUser.aspx?userMail=" + table.Rows[i]["Email"] + "'><img src='../Images/delete.png' width='30' height='25' alt='מחיקת משתמש מבסיס הנתונים'/></a></td>";

                        adm += "</form>";
                        adm += "</tr>";
                    }

                    adm += "</tbody></table>";
                }
                else
                {
                    adm = "אין ערכים תואמים לחיפוש";
                }
            }
            //עריכת משתמש באדמין
            else if (Request.QueryString["code"] == "3" && Session["Admin"] != "No")
            {

                string userFname = Request.Form["userFname"];
                string userLname = Request.Form["userLname"];
                string userGender = Request.Form["userGender"];
                string userMail = Request.Form["userMail"];
                string userPhone = Request.Form["userPhone"];
                string userPwd = Request.Form["userPwd"];
                string FavCar = Request.Form["FavCar"];

                edit += "<form name=\"reg-form\" id=\"reg-form\" action=\"AdminUpdateUser.aspx\" method=\"post\">";
                edit += "<div class=\"inp-constainer\">";
                edit += "<div class=\"star-cont\">";
                edit += "<div class=\"star\">*</div>";
                edit += "<input type=\"text\" placeholder=\"First Name\" value=\""+ userFname +"\" id=\"firstName\" name=\"firstName\" class=\"text-box\"/>";
                edit += "</div>";
                edit += "<div class=\"star-cont\">";
                edit += "<div class=\"star\">*</div>";
                edit += "<input type=\"text\" placeholder=\"Last Name\" value=" + userLname + " name=\"lastName\" id=\"lastName\" class=\"text-box\"/>";
                edit += "</div>";
                edit += "</div>";
                edit += "<div id=\"gNd-cont\">";
                edit += "<div id=\"gender-conatainer\">";
                edit += "<input type=\"radio\" id=\"maleradio\" " + IsChecked(userGender, "male") + " name=\"gender\" value=\"male\" checked/>";
                edit += "<label id=\"male\">Male</label>;";
                edit += "<input type=\"radio\" id=\"femaleradio\" " + IsChecked(userGender, "female") + " name=\"gender\" value=\"female\"/>";
                edit += "<label id=\"female\">Female</label>";
                edit += "</div>";
                edit += "</div>";
                edit += "<div>";
                edit += "<div class=\"star-cont\">";
                edit += "<div class=\"star\">*</div>";
                edit += "<input type=\"text\" readonly id=\"email\" value=\" "+ userMail + " \" class=\"text-box\" style=\"display:block\" name=\"email\" placeholder=\"Enter Your Email\" />";
                edit += "</div>";
                edit += "<div class=\"star-cont\">";
                edit += "<div class=\"star\">*</div>";
                edit += "<input type=\"text\" id=\"phoneNumber\" value=\" "+ userPhone + " \" class=\"text-box\" style=\"display:block\" name =\"phoneNumber\" placeholder=\"Enter Your Phone Number\" />";
                edit += "</div>";
                edit += "<div class=\"star-cont\">";
                edit += "<div class=\"star\">*</div>";
                edit += "<input type=\"password\" value=\" " + userPwd + " \" id=\"password\" name=\"password\" class=\"text-box\" style=\"display:block\" placeholder=\"Enter A Password\" />";
                edit += "</div>";
                edit += "</div>";
                edit += "<div id=\"favorite-car-container\">";
                edit += "<label id=\"favorite-car-label\">Favorite Car:</label>";
                edit += "<select class=\"free-input\" id=\"select-cont\" name=\"favCar\">";
                edit += "<option class=\"option\" value=\"Model S\" " + IsSelected(FavCar, "Model S") + ">Model S</option>";
                edit += "<option class=\"option\" value=\"Model 3\" " + IsSelected(FavCar, "Model 3") + ">Model 3</option>";
                edit += "<option class=\"option\" value=\"Model X\" " + IsSelected(FavCar, "Model X") + ">Model X</option>";
                edit += "<option class=\"option\" value=\"Model Y\" " + IsSelected(FavCar, "Model Y") + ">Model Y</option>";
                edit += "<option class=\"option\" value=\"CyberTruck\" " + IsSelected(FavCar, "CyberTruck") + ">CyberTruck</option>";
                edit += "</select>";
                edit += "</div>";
                edit += "<br />";
                edit += "<div class=\"submit-rest-container\" id=\"submit-rest-container\">";
                edit += "<input type=\"submit\" name=\"submitUpdate\" class=\"submit-rest submit\" id=\"regSubmit\" value=\"Update\"/>";
                edit += "</div>";
                edit += "</form>";
            }
            //בניית טבלה מלאה כאשר חוזרים מאדיט
            else if (Session["Admin"] != "No" && Request.QueryString["code"] == "4")
            {
                string sql;
                string dbFileName = "TeslaDB.accdb";
                sql = "SELECT * FROM tbl_users";

                DataTable table = MyAdoHelper.ExecuteDataTable(dbFileName, sql);
                int length = table.Rows.Count;
                adm = "";
                if (length > 0)
                {
                    adm = "<table class='styled-table'>";
                    adm += "<thead><tr>";
                    adm += "<th>First Name</th>";
                    adm += "<th>Last Name</th>";
                    adm += "<th>Gender</th>";
                    adm += "<th>Birth Date</th>";
                    adm += "<th>Email</th>";
                    adm += "<th>Phone Number</th>";
                    adm += "<th>Password</th>";
                    adm += "<th>Fav Car</th>";
                    adm += "<th>Admin</th>";
                    adm += "<th colspan='2'>Actions</th>";
                    adm += "</tr></thead>";
                    adm += "<tbody>";

                    for (int i = 0; i < length; i++)
                    {
                        adm += "<tr>";
                        adm += "<form method='post' action='Home.aspx?code=3'>";
                        adm += "<td><input type='text' class='inputToLabelLarge' name='userFname' value='" + table.Rows[i]["firstName"] + "' readonly/></td>";
                        adm += "<td><input type='text' class='inputToLabel' name='userLname' value='" + table.Rows[i]["LastName"] + "' readonly/></td>";
                        adm += "<td><input type='text' class='inputToLabel' name='userGender' value='" + table.Rows[i]["Gender"] + "' readonly/></td>";
                        adm += "<td><input type='text' class='inputToLabel' name='UserDate' value='" + table.Rows[i]["Date"] + "' readonly/></td>";
                        adm += "<td><input type='text' class='inputToLabel' name='userMail' value='" + table.Rows[i]["Email"] + "' readonly/></td>";
                        adm += "<td><input type='text' class='inputToLabel' name='userPhone' value='" + table.Rows[i]["PhoneNumber"] + "' readonly/></td>";
                        adm += "<td><input type='text' class='inputToLabel' name='userPwd' value='" + table.Rows[i]["Password"] + "' readonly/></td>";
                        adm += "<td><input type='text' class='inputToLabel' name='FavCar' value='" + table.Rows[i]["FavCar"] + "' readonly/></td>";
                        adm += "<td><input type='text' class='inputToLabel' name='AdminOrNot' value='" + table.Rows[i]["admin"] + "' readonly/></td>";

                        adm += "<td><input type='image' id='submitUpdate' name='submitUpdate' src='../Images/update.png' width='30' height='25' alt='עדכון פרטי משתמש' /></td>";
                        adm += "<td><a href='AdminDeleteUser.aspx?userMail=" + table.Rows[i]["Email"] + "'><img src='../Images/delete.png' width='30' height='25' alt='מחיקת משתמש מבסיס הנתונים'/></a></td>";

                        adm += "</form>";
                        adm += "</tr>";
                    }

                    adm += "</tbody></table>";
                }
            }

        }
            private string IsChecked(string gender, string currentValue)
            {
                if (currentValue == gender)
                    return " checked";
                return "";
            }
            private string IsSelected(string distrinct, string currentValue)
            {
                if (currentValue == distrinct)
                    return " selected";
                return "";
            }
    }
}