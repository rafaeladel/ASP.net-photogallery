<%@ Page Title="" Language="C#" MasterPageFile="~/gallery_master.Master" AutoEventWireup="true" CodeBehind="contact.aspx.cs" Inherits="photography.contact" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/contact.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="AOC">    
        <div class="sectionTitle">
            <h3>Contact Us</h3>
        </div>
        <div class="sectionBody">
            <asp:Repeater runat="server" DataSourceID="SqlDataSource1">
                <HeaderTemplate>
                    <div class="contact_info">
                </HeaderTemplate>
                <ItemTemplate>
                    <%# Eval("telephone").ToString().Trim().Length > 0 ? String.Format("<p>Tel : {0}</p>", Eval("telephone")) : "" %>
                </ItemTemplate>
                <FooterTemplate>
                    </div>
                </FooterTemplate>
            </asp:Repeater>
            
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:DBCS %>" 
                ProviderName="<%$ ConnectionStrings:DBCS.ProviderName %>" 
                SelectCommand="SELECT [telephone], [twitter], [email], [facebook] FROM [info]">
            </asp:SqlDataSource>
            
            <asp:Repeater runat="server" DataSourceID="SqlDataSource1">
                <HeaderTemplate>
                    <div class="contact_info">
                        <ul>
                </HeaderTemplate>
                <ItemTemplate>
                    <%# Eval("facebook").ToString().Trim().Length > 0 ? String.Format("<li><p>Facebook : <a href='http://{0}'>Facebook Page</a></p></li>", Eval("facebook")) : "" %>
                    <%# Eval("twitter").ToString().Trim().Length > 0 ? String.Format("<li><p>Twitter : <a href='http://{0}'>Twitter Page</a></p></li>", Eval("twitter")) : ""%>
                    <%# Eval("email").ToString().Trim().Length > 0 ? String.Format("<li><p>Email : <a href='mailto:{0}'>{0}</a></p></li>", Eval("email")) : ""%>
                </ItemTemplate>
                <FooterTemplate>
                        </ul>
                    </div>
                </FooterTemplate>
            </asp:Repeater>           
        </div>        
    </div>
</asp:Content>
