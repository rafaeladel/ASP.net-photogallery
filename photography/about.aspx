<%@ Page Title="" Language="C#" MasterPageFile="~/gallery_master.Master" AutoEventWireup="true" CodeBehind="about.aspx.cs" Inherits="photography.about" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="Stylesheet" type="text/css" href="css/about.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="AOC">    
        <div class="sectionTitle">
            <h3>About Us</h3>
        </div>
        <div class="sectionBody">
            <div id="about_content"><pre id="about_txt" runat="server"></pre></div>
        </div>        
    </div>
    <asp:Label id="msg_lbl" Text="" runat="server" />
</asp:Content>
