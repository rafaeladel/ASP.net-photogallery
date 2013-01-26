<%@ Page Title="" Language="C#" MasterPageFile="~/gallery_master.Master" AutoEventWireup="true" CodeBehind="sessions.aspx.cs" Inherits="photography.sessions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="Stylesheet" type="text/css" href="css/sessions.css" />  
    <link href="css/jquery_ui/jquery.fancybox.css" rel="stylesheet" type="text/css" />
    <link href="css/jquery_ui/jquery.fancybox-buttons.css" rel="stylesheet" type="text/css" /> 
    <link href="css/jquery_ui/jquery.mCustomScrollbar.css" rel="stylesheet" type="text/css" />   
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel id="sessions_panel" runat="server">
        <div id="sessions_wrapper">
            <asp:Repeater ID="sessions_repeater" runat="server" 
                DataSourceID="SqlDataSource1" 
                onitemcommand="sessions_repeater_ItemCommand">
                <ItemTemplate>
                    <asp:Panel CssClass="session_box" ID="testpanel" runat="server">
                        <asp:Button Text="" CssClass="session_btn" data-session-id='<%# Eval("session_id") %>' runat="server" />
                        <asp:Image CssClass="session_icon" runat="server" ImageUrl='<%# Eval("session_cover") %>' alt="" />
                        <div class="session_info">
                            <p><%# Eval("session_name") %></p>
                            <p>Photos: <%# Eval("session_photos") %></p>
                        </div>
                    </asp:Panel>
                </ItemTemplate>
            </asp:Repeater>                            
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:DBCS %>" 
                ProviderName="<%$ ConnectionStrings:DBCS.ProviderName %>" 
                SelectCommand="SELECT * FROM [sessions]"></asp:SqlDataSource>
        </div>
    </asp:Panel>  
    
    <asp:Panel id="images_panel" runat="server">
        <div id="images_info">
            <p runat="server" id="session_title">This is Title</p>
            <p runat="server" id="session_desc">This is Desc This is Desc This is Desc This is Desc This is Desc This is Desc </p>
            <p runat="server" id="session_date">This is Date</p>
        </div>
        <div id="images_wrapper">
            <asp:Repeater runat="server" ID="img_repeater" DataSourceID="SqlDataSource2">
                <ItemTemplate>
                    <div class="session_image">
                        <asp:HyperLink NavigateUrl='<%# Eval("session_img_path") %>' CssClass="fancybox" rel="session_imgs" runat="server"><asp:image imageurl='<%# Eval("session_img_path") %>' runat="server" /></asp:HyperLink>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                ConnectionString="<%$ ConnectionStrings:DBCS %>" 
                ProviderName="<%$ ConnectionStrings:DBCS.ProviderName %>" 
                SelectCommand="SELECT [session_img_name], [session_img_path] FROM [img_session] WHERE ([session_id] = @session_id)">
                <SelectParameters>
                    <asp:QueryStringParameter Name="session_id" QueryStringField="sid" 
                        Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>
    </asp:Panel>  
    <script src="js/jquery_ui/jquery.fancybox.js" type="text/javascript"></script>
    <script src="js/jquery_ui/jquery.fancybox-buttons.js" type="text/javascript"></script>
    <script src="js/jquery_ui/jquery.mousewheel.min.js" type="text/javascript"></script>
    <script src="js/jquery_ui/jquery.mCustomScrollbar.js" type="text/javascript"></script>
    <script src="js/sessions.js" type="text/javascript"></script>
</asp:Content>
