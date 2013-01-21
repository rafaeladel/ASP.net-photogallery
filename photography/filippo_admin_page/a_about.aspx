<%@ Page Title="" Language="C#" MasterPageFile="~/filippo_admin_page/admin_master.Master" AutoEventWireup="true" CodeBehind="a_about.aspx.cs" Inherits="photography.filippo_admin_page.a_about" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #insert_wrapper
        {
            width: 600px !important;
        }
        
        .fieldset_format
        {
            width:570px !important;
        }
        .textarea_format 
        {
            height : 100px !important;
            width : 550px !important;
        }
        .buttons_positioner 
        {
            margin-left: 450px !important;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="whole_wrapper">
        <div id="insert_wrapper">
            <fieldset class="fieldset_format">
                <legend>Your current About Us :</legend>
                <ul>
                    <li>
                        <asp:Label AssociatedControlID="about_txt" ID="about_lbl" runat="server">About Us:</asp:Label>
                        <asp:TextBox ID="about_txt" runat="server" TextMode="MultiLine" placeholder="Your current about us" class="textarea_format"></asp:TextBox>
                    </li>
                    <li>
                            <asp:Button ID="clear" class="clear_btn buttons_positioner" runat="server" Text="Clear" CausesValidation="False" 
                            UseSubmitBehavior="False" onclientclick="reset_all(); return false;" />
                            <asp:Button ID="submit" class="submit_btn" runat="server" Text="Submit" 
                                onclick="submit_Click"/>                        
                    </li>
                </ul>
                 <asp:Label ID="msg_lbl" Visible="false" runat="server"></asp:Label>
            </fieldset>
        </div>
    </div>
</asp:Content>
