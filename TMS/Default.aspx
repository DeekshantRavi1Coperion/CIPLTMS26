<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<link href="Styles/Site.css" rel="stylesheet" type="text/css" />--%>
    <link href="Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link href="Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />

    <link rel="icon" href="Images/Icon04.png" />

    <%--<style type="text/css">
        body
        {
            background: url(Images/COPERION/coperion_logo.jpg) no-repeat center center fixed;            
            -webkit-background-size: cover;
            -moz-background-size: cover;
            -o-background-size: cover;
            background-size: cover;
        }
    </style>--%>

    <style type="text/css">
        .home-background {
            background: url('../Images/COPERION/coperion_logo.jpg') no-repeat center center;
            background-size: contain; /* or cover */
            width: 100%;
            height: calc(100vh - 120px); /* adjust for header/footer */
        }
    </style>


</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div>
        <video id="bgvid" playsinline autoplay muted loop>
            <source src="VIDEOS/SetangiBeach2.mp4" type="video/mp4">
            <source src="VIDEOS/SetangiBeach2.mov" type="video/mov">           
        </video>
        <div align="center" style="margin-top: 200px; color: White; font-size: 50px; font-family: Arial;">
            <asp:Label ID="Label1" runat="server" Text="Keep Flying..." />
        </div>
    </div>
</asp:Content>--%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <div class="home-background"></div>

</asp:Content>