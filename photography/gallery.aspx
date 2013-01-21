<%@ Page Title="" Language="C#" MasterPageFile="~/gallery_master.Master" AutoEventWireup="true" CodeBehind="gallery.aspx.cs" Inherits="photography.gallery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="Stylesheet" type="text/css" href="css/gallery.css" />
    
    <link href="css/jquery_ui/jquery.fancybox.css" rel="stylesheet" type="text/css" />
    <link href="css/jquery_ui/jquery.fancybox-buttons.css" rel="stylesheet" type="text/css" /> 
    <link href="css/jquery_ui/jquery.mCustomScrollbar.css" rel="stylesheet" type="text/css" />   
    
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="gallery_wrapper">
    <div id="cat_wrapper">
        <asp:Repeater ID="cat_repeater" runat="server" 
            onitemcommand="cat_repeater_ItemCommand">
            <HeaderTemplate>
                <ul>                    
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <asp:LinkButton runat="server" Text='<%# Eval("img_cat") %>'></asp:LinkButton>
                </li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>            
    </div>
    <div id="img_wrapper">
        <div id="backBtn"></div>
        <div id="imgSlider_wrapper">  
            <asp:Repeater ID="slider_repeater" runat="server">
                <HeaderTemplate>
                    <ul id="imgSlider_ul">
                </HeaderTemplate>
                <ItemTemplate>
                    <li>
                        <asp:HyperLink runat="server" NavigateUrl='<%# Eval("img_path") %>' title='<%# Eval("img_title") %>' class="fancybox" rel="gallery"><asp:Image runat="server" ImageUrl='<%# Eval("img_path") %>' /></asp:HyperLink>
                    </li>                
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                </FooterTemplate>
            </asp:Repeater>           
        </div>
        <div id="nextBtn"></div>
    </div>
</div>
    <script src="js/jquery_ui/jquery.event.drag-2.2.js" type="text/javascript"></script>
    <script src="js/jquery_ui/jquery.event.drop-2.2.js" type="text/javascript"></script>
    <script src="js/jquery_ui/jquery.easing.1.3.js" type="text/javascript"></script>
    <script src="js/jquery_ui/jquery.roundabout.js" type="text/javascript"></script>
    <script src="js/jquery_ui/jquery.fancybox.js" type="text/javascript"></script>
    <script src="js/jquery_ui/jquery.fancybox-buttons.js" type="text/javascript"></script>
    <script src="js/jquery_ui/jquery.mousewheel.min.js" type="text/javascript"></script>
    <script src="js/jquery_ui/jquery.mCustomScrollbar.js" type="text/javascript"></script>
    <script src="js/gallery.js" type="text/javascript"></script>
</asp:Content>
