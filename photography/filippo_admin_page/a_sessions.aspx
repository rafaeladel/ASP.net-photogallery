<%@ Page Title="" Language="C#" MasterPageFile="~/filippo_admin_page/admin_master.Master" AutoEventWireup="true" CodeBehind="a_sessions.aspx.cs" Inherits="photography.filippo_admin_page.a_sessions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#ContentPlaceHolder1_date_txt").datepicker({
                dateFormat: "dd-mm-yy",
            });
        });        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="whole_wrapper">
        <div id="insert_wrapper">
            <fieldset class="fieldset_format">
                <legend>Insert new session:</legend>
                <ul>
                    <li>
                        <asp:Label Text="Session:" AssociatedControlID="session_txt" ID="session_lbl" runat="server" />
                        <asp:TextBox id="session_txt" runat="server" placeholder="Session Title"/>
                        <asp:RequiredFieldValidator ErrorMessage="Required!" ControlToValidate="session_txt"
                            runat="server" ValidationGroup="session_validateion"/>
                    </li>
                    <li>
                        <asp:Label Text="Desc:" id="desc_lbl" AssociatedControlID="desc_txt" runat="server" />
                        <asp:TextBox id="desc_txt" TextMode="MultiLine" class="textarea_format" placeholder="Session Description" runat="server" />
                    </li>
                    <li>
                        <asp:Label Text="Taken in:" id="date_lbl" AssociatedControlID="date_txt" runat="server" />
                        <asp:TextBox ID="date_txt" runat="server" placeholder="Session Date"/>
                    </li>
                    <li>
                        <asp:Button ID="Button1" class="clear_btn buttons_positioner" runat="server" Text="Clear" CausesValidation="False" 
                            UseSubmitBehavior="False" onclientclick="reset_all(); return false;" />
                        <asp:Button ID="Button2" class="submit_btn" runat="server" Text="Upload"
                            onclick="Button2_Click" ValidationGroup="session_validation"/>                        
                    </li>
                </ul>
                <asp:Label Text="" ID="msg_lbl" runat="server" />
            </fieldset>            
        </div>
        <div id="grid_wrapper">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="session_id" DataSourceID="SqlDataSource1" AllowPaging="True" 
                AllowSorting="True" onrowdeleted="GridView1_RowDeleted" 
                onselectedindexchanged="GridView1_SelectedIndexChanged" CssClass="mGrid" 
                BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" 
                CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="5">
                <AlternatingRowStyle BackColor="#CCCCCC" />
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowSelectButton="True" />
                    <asp:BoundField DataField="session_id" HeaderText="session_id" 
                        InsertVisible="False" ReadOnly="True" SortExpression="session_id" 
                        Visible="False" />
                    <asp:BoundField DataField="session_name" HeaderText="Name" 
                        SortExpression="session_name" />
                    <asp:BoundField DataField="session_desc" HeaderText="Description" 
                        SortExpression="session_desc" />
                    <asp:BoundField DataField="session_date" HeaderText="Taken in" 
                        SortExpression="session_date" />
                    <asp:BoundField DataField="session_photos" HeaderText="Photos" 
                        SortExpression="session_photos" />
                    <asp:BoundField DataField="session_cover" HeaderText="session_cover" 
                        SortExpression="session_cover" Visible="False" />
                    <asp:ImageField DataImageUrlField="session_cover">
                        <ControlStyle Height="100px" Width="150px" />
                    </asp:ImageField>
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
                DeleteCommand="DELETE FROM [sessions] WHERE [session_id] = @original_session_id AND [session_name] = @original_session_name AND (([session_desc] = @original_session_desc) OR ([session_desc] IS NULL AND @original_session_desc IS NULL)) AND (([session_date] = @original_session_date) OR ([session_date] IS NULL AND @original_session_date IS NULL)) AND (([session_photos] = @original_session_photos) OR ([session_photos] IS NULL AND @original_session_photos IS NULL)) AND (([session_cover] = @original_session_cover) OR ([session_cover] IS NULL AND @original_session_cover IS NULL))" 
                InsertCommand="INSERT INTO [sessions] ([session_name], [session_desc], [session_date], [session_photos], [session_cover]) VALUES (@session_name, @session_desc, @session_date, @session_photos, @session_cover)" 
                OldValuesParameterFormatString="original_{0}" 
                ProviderName="<%$ ConnectionStrings:DBCS.ProviderName %>" 
                SelectCommand="SELECT * FROM [sessions]" 
                    
                
                UpdateCommand="UPDATE [sessions] SET [session_name] = @session_name, [session_desc] = @session_desc, [session_date] = @session_date, [session_photos] = @session_photos, [session_cover] = @session_cover WHERE [session_id] = @original_session_id AND [session_name] = @original_session_name AND (([session_desc] = @original_session_desc) OR ([session_desc] IS NULL AND @original_session_desc IS NULL)) AND (([session_date] = @original_session_date) OR ([session_date] IS NULL AND @original_session_date IS NULL)) AND (([session_photos] = @original_session_photos) OR ([session_photos] IS NULL AND @original_session_photos IS NULL)) AND (([session_cover] = @original_session_cover) OR ([session_cover] IS NULL AND @original_session_cover IS NULL))">
                <DeleteParameters>
                    <asp:Parameter Name="original_session_id" Type="Int32" />
                    <asp:Parameter Name="original_session_name" Type="String" />
                    <asp:Parameter Name="original_session_desc" Type="String" />
                    <asp:Parameter DbType="Date" Name="original_session_date" />
                    <asp:Parameter Name="original_session_photos" Type="Int32" />
                    <asp:Parameter Name="original_session_cover" Type="String" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="session_name" Type="String" />
                    <asp:Parameter Name="session_desc" Type="String" />
                    <asp:Parameter DbType="Date" Name="session_date" />
                    <asp:Parameter Name="session_photos" Type="Int32" />
                    <asp:Parameter Name="session_cover" Type="String" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="session_name" Type="String" />
                    <asp:Parameter Name="session_desc" Type="String" />
                    <asp:Parameter DbType="Date" Name="session_date" />
                    <asp:Parameter Name="session_photos" Type="Int32" />
                    <asp:Parameter Name="session_cover" Type="String" />
                    <asp:Parameter Name="original_session_id" Type="Int32" />
                    <asp:Parameter Name="original_session_name" Type="String" />
                    <asp:Parameter Name="original_session_desc" Type="String" />
                    <asp:Parameter DbType="Date" Name="original_session_date" />
                    <asp:Parameter Name="original_session_photos" Type="Int32" />
                    <asp:Parameter Name="original_session_cover" Type="String" />
                </UpdateParameters>
            </asp:SqlDataSource>
        </div>
    </div>
</asp:Content>
