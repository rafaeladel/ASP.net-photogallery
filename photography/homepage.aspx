<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="homepage.aspx.cs" Inherits="photography.WebForm2" %>
<%@ OutputCache VaryByParam="*" Duration="86400"%>

<!DOCTYPE html">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>A.Filippo Photography</title>
    <link rel="Stylesheet" type="text/css" href="css/whole.css" />
    <link rel="Stylesheet" type="text/css" href="css/homepage.css" />
    <script src="js/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="js/homepage.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="whole_container">            
            <div id="header">
                <div id="follow">
                    <asp:Repeater runat="server" DataSourceID="SqlDataSource1">
                        <ItemTemplate>
                            <a href='http://<%# Eval("facebook") %>'><img src="img/temp.png" alt="facebook" /></a>
                            <a href='http://<%# Eval("twitter") %>'><img src="img/temp.png" alt="twitter" /></a>
                            <a href='mailto:<%# Eval("email") %>'><img src="img/temp.png" alt="email" /></a>
                        </ItemTemplate>
                    </asp:Repeater>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:DBCS %>" 
                        ProviderName="<%$ ConnectionStrings:DBCS.ProviderName %>" 
                        SelectCommandType="StoredProcedure" SelectCommand="Select_Info_SP">
                    </asp:SqlDataSource>                        
                </div>
                <div id="iconlabel">
                    <a href="."><img src="img/logo.png" alt="logo" /></a>
                </div>                
            </div>
            <div id="main_content">
                <div id="slides_wrapper">
                    <a href="gallery.aspx">
                        <div id="gallery_slide">                        
                            <p>Gallery</p>
                        </div>
                    </a>
                    <a href="sessions.aspx">
                        <div id="session_slide">
                            <p>Sessions</p>
                        </div>
                    </a>
                    <a href="offers.aspx">
                        <div id="offers_slide">
                            <p>Offers</p>
                        </div>
                    </a>
                    <a href="about.aspx">
                        <div id="about_slide">
                            <p>About Us</p>
                        </div>
                    </a>
                    <a href="contact.aspx">
                        <div id="contact_slide">
                            <p>Contact Us</p>
                        </div>
                    </a>
                </div>                
            </div>
            <div id="footer">
                <div id="copyright">
                    <div id="rights">
                        <p>All rights reserverd.© - Best viewed using the latest version of <a href="http://www.google.com/chrome">Google Chrome</a> or <a href="www.mozilla.org/en-US/firefox/new/">Mozilla Firefox</a>.</p>                    
                    </div>
                    <div id="stamp">                    
                        <p>Crafted by</p>
                        <a href="http://about.me/rafael.adel">Rafael Adel</a>
                    </div>                   
                </div>
            </div>
        </asp:Panel>
    </form>
</body>
</html>	