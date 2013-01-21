<%@ Page Title="" Language="C#" MasterPageFile="~/filippo_admin_page/admin_master.Master" AutoEventWireup="true" CodeBehind="a_contact.aspx.cs" Inherits="photography.filippo_admin_page.a_contact" %>
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
        .textbox_format
        {
            height : 20px !important;
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
                <legend>Your current Contact Us :</legend>
                <ul>
                    <li>
                        <asp:Label AssociatedControlID="tel_txt" ID="tel_lbl" runat="server">Tel:</asp:Label>
                        <asp:TextBox ID="tel_txt" class="textbox_format" runat="server" placeholder="Telphone number, separated by ' - ' if more than one."></asp:TextBox>                        
                    </li>

                    <li>
                        <asp:Label AssociatedControlID="facebook_txt" ID="facebook_lbl" runat="server">Facebook:</asp:Label>
                        <asp:TextBox ID="facebook_txt" class="textbox_format" runat="server" placeholder="Facebook Page address"></asp:TextBox>                        
                    </li>

                    <li>
                        <asp:Label AssociatedControlID="twitter_txt" ID="twitter_lbl" runat="server">Twitter:</asp:Label>
                        <asp:TextBox ID="twitter_txt" class="textbox_format" runat="server" placeholder="Twitter Account address"></asp:TextBox>
                    </li>

                    <li>
                        <asp:Label AssociatedControlID="mail_txt" ID="main_lbl" runat="server">E-mail:</asp:Label>
                        <asp:TextBox ID="mail_txt" class="textbox_format" runat="server" placeholder="E-mail address"></asp:TextBox>
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
