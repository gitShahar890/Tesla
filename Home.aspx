<%@ Page Title="Tesla | HOME" Language="C#" MasterPageFile="~/frame.master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Tesla.homePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../styles/HomeStyle.css" rel="stylesheet" type="text/css"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <%--update user--%>
    <%if (Request.QueryString["code"] == "3" && Session["firstName"] != "guest")
            {%>
                <div id="form-container1">
                    <%=edit %>
                 </div>
            <%} %>

    <%--admin--%>
    <%if(Session["Admin"] != "No" && Request.QueryString["code"] != "3")
            {%>
                <div style="width:90%; color:white;">
        
        <h2>User's Details</h2>
        <h3>Filter search results by:</h3>
        <form name ="search" method="post" style="color:white;" action="Home.aspx">
            <table>
                <tr>
                    <td>
                        <label style="font-weight:bold;">Gender:</label>
                        <input type="radio" id="male"   name="userGender" value="male"/> male
                        <input type="radio" id="female" name="userGender" value="female"/> female
                    </td>
                    <td style="width:15%;"></td>
                    <td>
                        <label style="font-weight:bold;">Favorite Car:</label>
                        <select name="FavCar" id="FavCar">
                            <option value="choose">Choose</option>
                            <option value="Model S">Model S</option>
                            <option value="Model 3">Model 3</option>
                            <option value="Model X">Model X</option>
                            <option value="Model Y">Model Y</option>
                            <option value="CyberTruck">CyberTruck</option>
                        </select>
                    </td>
                    <td style="width:10%;"></td>
                    <td>
                        <input type="submit" name="submitSearch" id="submitSearch" value="filter"/>
                    </td>
                </tr>
            </table>
        </form>
        <br /><hr />
    
        <div style="height:25em; width:100%;overflow-y: scroll;">
            <%=adm %>
        </div>
    </div>
            <%} %>
    <%--normal--%>
    <%if (Session["firstName"] == "guest")
        {%>
       <div id="form-container">
           <div class="login-header">
                <button class="trans-form" id="signUpBtn">Sign Up</button>
                <button class="trans-form" id="logInBtn">Log In</button>
           </div>
                <div id="mover"></div>
           <%--register--%>
           <form name="reg-form" id="reg-form" action="Home.aspx" method="post">
               <div class="inp-constainer">
                   <div class="star-cont">
                       <div class="star">*</div>
                   <input type="text" placeholder="First Name" id="firstName" name="firstName" class="text-box"/>
                   </div>
                   <div class="star-cont">
                       <div class="star">*</div>
                   <input type="text" placeholder="Last Name" name="lastName" id="LastName" class="text-box"/>
                   </div>
               </div>
               <div id="gNd-cont">
                   <div id="gender-conatainer">
                       <input type="radio" id="maleradio" name="gender" value="male" checked/>
                       <label id="male">Male</label>
                       <input type="radio" id="femaleradio" name="gender" value="female"/>
                       <label id="female">Female</label>
                   </div>
                   <div id="date-cont">
                        <div class="star">*</div>
                        <input type="date" id="date" name="date" class="free-input"/>
                   </div>
               </div>
               <div>
               <div class="star-cont">
                       <div class="star">*</div>
                   <input type="text" id="email" class="text-box" style="display:block" name="email" placeholder="Enter Your Email" />
                   </div>
               <div class="star-cont">
                       <div class="star">*</div>
                   <input type="text" id="phoneNumber" class="text-box" style="display:block" name ="phoneNumber" placeholder="Enter Your Phone Number" />
                   </div>
               <div class="star-cont">
                       <div class="star">*</div>
                   <input type="password" id="password" name="password" class="text-box" style="display:block" placeholder="Enter A Password" />
                   </div>
               </div>
                <div id="favorite-car-container">
                   <label id="favorite-car-label">Favorite Car:</label>
                   <select class="free-input" id="select-cont" name="favCar">
                       <option class="option" value="Model S" >Model S</option>
                       <option class="option" value="Model 3">Model 3</option>
                       <option class="option" value="Model X">Model X</option>
                       <option class="option" value="Model Y">Model Y</option>
                       <option class="option" value="CyberTruck">CyberTruck</option>
                   </select>
               </div>
               <div id="text-area-container">
                   <textarea class="text-area free-input" id="text-area" placeholder="write what ever you want" ></textarea>
               </div>
               <div id="checkbox-container">
                   <div class="star">*</div>
                   <input type="checkbox" id="T_P-checkbox"/>
                   <label id="agreeing">I Agree To</label> <a href="#" id="undeline-T_P">Terms & Policy</a>
               </div>
               <br />
               <div class="submit-rest-container" id="submit-rest-container">
                   <input type="reset" class="submit-rest reset"/>
                   <input type="submit" name="submit" class="submit-rest submit" id="regSubmit" value="Sign Up"/>
               </div>
        </form>


           <%--log in--%>
        <form name="log-form" id="log-form" action="Home.aspx" method="post">
            <div id="log-form-cont">
                <div>
                    <input type="text" name="logEmail" class="text-box" style="display:block" value="shahar@gmail.com" placeholder="Enter Your Email" />
                    <input type="text" name="logPhone" class="text-box" style="display:block" value="053-3346589" placeholder="Enter Your Phone Number" />
                    <input type="password" name="logPass" class="text-box" style="display:block" value="Tesla58036" placeholder="Enter A Password" />
                </div>
                <div class="submit-rest-container log-s-r-cont">
                    <input type="reset" class="submit-rest"/>
                    <input type="submit" name="submitLogin" value="Log In" class="submit-rest"/>
                </div>
            </div>
        </form>
        </div>
    <%} %>
    <%--code 1 - registered user--%>
    <%if (Request.QueryString["code"] == "1"){%>
            <div class="code-cont" id="code1" style="opacity:1; position:relative;left:510px; top:20px;">
                <label>You are already registered on the site, please try to log in</label>
            </div>
    <%}%>
    <%--code 2 - unregistered user--%>
    <%if (Request.QueryString["code"] == "2"){%>
            <div class="code-cont" id="code2" style="opacity:1; position:relative;left:510px; top:20px;">
                <label>You are not registered on the site, please register</label>
            </div>
    <%}%>
    <%--normal--%>
    <%if (Session["firstName"] == "guest" || (Request.QueryString["code"] == "3" && Session["Admin"] != "No"))
        {%>
        
        <%--validation check--%>
    <div class="validation-container">
        <div class="validation-header">
            <label>Validation Check</label>
        </div>
        <div id="check1" class="check-container">
            <div class="icons">
                <ion-icon class="v-icons" id="v1" name="checkmark-outline"></ion-icon>
                <ion-icon class="x-icons" id="x1" name="close-outline"></ion-icon>
            </div>
            <div class="check-label">
                <label>all required fields are complete</label>
            </div>
        </div>
        <div id="check2" class="check-container">
            <div class="icons">
                <ion-icon class="v-icons" id="v2" name='checkmark-outline'></ion-icon>
                <ion-icon class="x-icons" id="x2" name="close-outline"></ion-icon>
            </div>
            <div class="check-label">
                <label>only english english letters on the first and last name</label>
            </div>
        </div>
        <div id="check3" class="check-container">
            <div class="icons">
                <ion-icon class="v-icons" id="v3" name="checkmark-outline"></ion-icon>
                <ion-icon class="x-icons" id="x3" name="close-outline"></ion-icon>
            </div>
            <div class="check-label">
                <label>age overcome's 18</label>
            </div>
        </div>
    <div id="check4" class="check-container">
            <div class="icons">
                <ion-icon class="v-icons" id="v4" name="checkmark-outline"></ion-icon>
                <ion-icon class="x-icons" id="x4" name="close-outline"></ion-icon>
            </div>
            <div class="check-label">
                <label>phone number must start with 0 and be filled with numbers and '-'</label>
            </div>
        </div>
        <div id="check5" class="check-container">
            <div class="icons">
                <ion-icon class="v-icons" id="v5" name="checkmark-outline"></ion-icon>
                <ion-icon class="x-icons" id="x5" name="close-outline"></ion-icon>
            </div>
            <div class="check-label">
                <label> '@' and '.' in the email in the correct location</label>
            </div>
        </div>
        <div id="check6" class="check-container">
            <div class="icons">
                <ion-icon class="v-icons" id="v6" name="checkmark-outline"></ion-icon>
                <ion-icon class="x-icons" id="x6" name="close-outline"></ion-icon>
            </div>
            <div class="check-label">
                <label>password longer then 8 charactors</label>
            </div>
        </div>
        </div>
    <%}%>

    <%--רקע סרטון:--%>
    <video controls="controls" muted autoplay loop  class="backgorund" >
        <source src="../Images/BackgroundVid.mp4" type="video/mp4"/>
    </video>
    <div class="alpha"></div>
    <script src="../script.js"></script>
    <script type="module" src="https://unpkg.com/ionicons@7.1.0/dist/ionicons/ionicons.esm.js"></script>
    <script nomodule src="https://unpkg.com/ionicons@7.1.0/dist/ionicons/ionicons.js"></script>
</asp:Content>
