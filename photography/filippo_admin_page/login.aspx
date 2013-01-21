<%@ Page Title="" Language="C#" MasterPageFile="~/filippo_admin_page/admin_master.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="photography.filippo_admin_page.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #ContentPlaceHolder1_remembercheck , #ContentPlaceHolder1_checklbl
        {
            display:inline;           
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="whole_wrapper">
        <div id="insert_wrapper">
            <fieldset class="fieldset_format">
                <ul>
                    <li>
                        <asp:Label Text="Username:" AssociatedControlID="un_text" ID="un_lbl" runat="server" />
                        <asp:TextBox runat="server" id="un_text" placeholder="Username"/>
                    </li>
                    <li>
                        <asp:Label Text="Password:" AssociatedControlID="pw_text" ID="pw_lbl" runat="server" />
                        <asp:TextBox runat="server" id="pw_text" placeholder="Password"/>
                    </li>
                    <li>
                        <asp:CheckBox Text="" ID="remembercheck" runat="server" />
                        <asp:Label AssociatedControlID="remembercheck" Text="Remember Me!" runat="server" ID="checklbl"></asp:Label>
                        <asp:Button ID="clear" class="clear_btn buttons_positioner" runat="server" Text="Clear" CausesValidation="False" 
                            UseSubmitBehavior="False" onclientclick="reset_all(); return false;" />
                        <asp:Button class="submit_btn" runat="server" Text="Upload" id="submit" 
                            onclick="submit_Click"/>      
                    </li>
                </ul>
            </fieldset>
        </div>
    </div>
</asp:Content>
