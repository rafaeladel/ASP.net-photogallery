<%@ Page Title="" Language="C#" MasterPageFile="~/filippo_admin_page/admin_master.Master" AutoEventWireup="true" CodeBehind="a_offers.aspx.cs" Inherits="photography.filippo_admin_page.a_offers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="Stylesheet" type="text/css" href="css/a_offers.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="whole_wrapper">
        <div id="insert_wrapper">
            <fieldset class="fieldset_format">
                <legend>Insert a new offer:</legend>
                <ul>
                    <li>
                        <asp:Label Text="Title:" AssociatedControlID="title_txt" ID="title_lbl" runat="server" /> 
                        <asp:TextBox runat="server" placeholder="Offer title" ID="title_txt"></asp:TextBox>
                        <asp:RequiredFieldValidator ErrorMessage="Required!" ControlToValidate="title_txt"
                            runat="server" ForeColor="Red" ValidationGroup="insert_val" />    
                    </li>
                    <li>
                        <asp:Label Text="Description:" AssociatedControlID="body_txt" ID="body_lbl" runat="server" />
                        <asp:TextBox runat="server" class="textarea_format" TextMode="MultiLine" placeholder="Offer description" ID="body_txt"></asp:TextBox>        
                        <asp:RequiredFieldValidator ErrorMessage="Required!" ControlToValidate="body_txt"
                            runat="server" ForeColor="Red" ValidationGroup="insert_val" />
                    </li>
                    <li>
                        <asp:Button Text="Clear" class="buttons_positioner clear_btn" runat="server"
                            CausesValidation="False" UseSubmitBehavior="False" onclientclick="reset_all(); return false;" />
                        <asp:Button Text="Submit" class="submit_btn" runat="server" ID="submit_btn" 
                            onclick="submit_btn_Click" ValidationGroup="insert_val" />
                    </li>
                </ul>
                <asp:Label Text="" ID="msg_lbl" runat="server" />
            </fieldset>
        </div>
        <div id="grid_wrapper">
            <asp:GridView ID="GridView1" CssClass="mGrid" runat="server" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="offer_id" 
                DataSourceID="SqlDataSource1" BackColor="White" BorderColor="#999999" 
                BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" 
                GridLines="Vertical">
                <AlternatingRowStyle BackColor="#CCCCCC" />
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    <asp:BoundField DataField="offer_title" HeaderText="Title" 
                        SortExpression="offer_title" />
                    <asp:BoundField DataField="offer_body" HeaderText="Description" 
                        SortExpression="offer_body" />
                    <asp:BoundField DataField="offer_date" HeaderText="Added in" 
                        SortExpression="offer_date" ReadOnly="True" />
                </Columns>
                <FooterStyle BackColor="#CCCCCC" />
                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#808080" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#383838" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConflictDetection="CompareAllValues" 
                ConnectionString="<%$ ConnectionStrings:DBCS %>" 
                DeleteCommand="DELETE FROM [offers] WHERE [offer_id] = @original_offer_id AND [offer_title] = @original_offer_title AND [offer_body] = @original_offer_body AND [offer_date] = @original_offer_date" 
                InsertCommand="INSERT INTO [offers] ([offer_title], [offer_body], [offer_date]) VALUES (@offer_title, @offer_body, @offer_date)" 
                OldValuesParameterFormatString="original_{0}" 
                ProviderName="<%$ ConnectionStrings:DBCS.ProviderName %>" 
                SelectCommand="SELECT * FROM [offers]" 
                UpdateCommand="UPDATE [offers] SET [offer_title] = @offer_title, [offer_body] = @offer_body, [offer_date] = @offer_date WHERE [offer_id] = @original_offer_id AND [offer_title] = @original_offer_title AND [offer_body] = @original_offer_body AND [offer_date] = @original_offer_date">
                <DeleteParameters>
                    <asp:Parameter Name="original_offer_id" Type="Int32" />
                    <asp:Parameter Name="original_offer_title" Type="String" />
                    <asp:Parameter Name="original_offer_body" Type="String" />
                    <asp:Parameter DbType="DateTime2" Name="original_offer_date" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="offer_title" Type="String" />
                    <asp:Parameter Name="offer_body" Type="String" />
                    <asp:Parameter DbType="DateTime2" Name="offer_date" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="offer_title" Type="String" />
                    <asp:Parameter Name="offer_body" Type="String" />
                    <asp:Parameter DbType="DateTime2" Name="offer_date" />
                    <asp:Parameter Name="original_offer_id" Type="Int32" />
                    <asp:Parameter Name="original_offer_title" Type="String" />
                    <asp:Parameter Name="original_offer_body" Type="String" />
                    <asp:Parameter DbType="DateTime2" Name="original_offer_date" />
                </UpdateParameters>
            </asp:SqlDataSource>
        </div>
    </div>
</asp:Content>
