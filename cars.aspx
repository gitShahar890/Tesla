<%@ Page Title="Tesla | CARS" Language="C#" MasterPageFile="~/frame.master" AutoEventWireup="true" CodeBehind="cars.aspx.cs" Inherits="Tesla.html_pages.Cars" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../styles/cars.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <img src="../Images/model%203.png" />
        <img src="../Images/model%20s.png" />
        <img src="../Images/model%20x.png" />
        <img src="../Images/model%20y.png" />
    </div>
    <div class="pdiv">
        <p>
            Tesla, Inc. is an American electric vehicle and clean energy company founded in 2003 by Elon Musk, JB Straubel, and Martin Eberhard. The company designs, manufactures and sells electric cars, battery energy storage from home to grid-scale, solar panels and solar roof tiles, solar roof tiles, and related products and services. Tesla is committed to accelerating the world's transition to sustainable energy.

            Tesla's current car lineup includes four models: the Model S, Model 3, Model X, and Model Y.
        </p>
    </div>
    <table class="table">
        <tr>
            <th>model S </th>
            <th>model 3 </th>
            <th>model X </th>
            <th>model Y </th>
        </tr>
        <tr class="content-table">
            <td>3.4 s</td>
            <td>5.8 s</td>
            <td>4.8 s</td>
            <td>4.1 s</td>
        </tr>
    </table>
</asp:Content>
