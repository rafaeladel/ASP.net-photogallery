<%@ Page Title="" Language="C#" MasterPageFile="~/filippo_admin_page/admin_master.Master" AutoEventWireup="true" CodeBehind="img_session.aspx.cs" Inherits="photography.filippo_admin_page.img_session" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            if (!('multiple' in document.createElement('input'))) {
                var add_btn = $("<a href='#'>Add more photos</a>").insertAfter("#ContentPlaceHolder1_upload_lbl");
                var upload_pnl = $('<input type="file" runat="server"/>');
                var upload_holder = $("#fileinput_placeholder");
                add_btn.on("click", function () {
                    upload_holder.append(upload_pnl.clone());
                });
            }

            $("#ContentPlaceHolder1_date_txt").datepicker({
                dateFormat: "dd/mm/yy",
            });
        });
    </script>      
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="whole_wrapper">
        <div id="insert_wrapper">
            <fieldset class="fieldset_format">
                <legend>Insert Images</legend>
                <ul>
                    <li>    
                        <asp:Label ID="upload_lbl" runat="server">Select Image(s):</asp:Label>
                        <div id="fileinput_placeholder">
                            <input type="file" multiple="true" runat="server" />
                        </div>
                    </li>
                    <li>
                        <asp:Button Text="Upload" ID="Insert" OnClick="Insert_Click" runat="server" />
                    </li>
                    <asp:Label Text="" ID="upload_msg" runat="server" />
                </ul>
            </fieldset>
            <fieldset class="fieldset_format">
                <legend>Edit Session Info</legend>
                <ul>
                    <li>
                        <asp:Label Text="Session:" AssociatedControlID="session_txt" ID="session_lbl" runat="server" />
                        <asp:TextBox id="session_txt" runat="server" placeholder="Session Title"/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                            ErrorMessage="Required!" ControlToValidate="session_txt"
                            runat="server" ValidationGroup="edit_validation"/>
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
                        <asp:Label Text="Cover:" ID="cover_lbl" runat="server" />
                        <asp:Image ImageUrl="" id="cover_img" runat="server" Height="100px" Width="150px" />
                    </li>
                    <li>
                        <asp:Button ID="Button1" class="clear_btn buttons_positioner" runat="server" Text="Clear" CausesValidation="False" 
                            UseSubmitBehavior="False" onclientclick="reset_all(); return false;" />
                        <asp:Button ID="Button2" class="submit_btn" runat="server" Text="Update"
                            onclick="Button2_Click" ValidationGroup="edit_validation" Width="70px"/> 
                        <asp:HyperLink NavigateUrl="a_sessions.aspx" Text="Go Back" Font-Bold="true" runat="server" />
                    </li>
                </ul>
            </fieldset>
            <asp:Label ID="msg_lbl" runat="server" Visible="false"></asp:Label>
        </div>
        <div id="grid_wrapper">
            <asp:GridView runat="server" id="img_session_grid" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
                DataKeyNames="session_img_path" DataSourceID="SqlDataSource1" ForeColor="Black" 
                GridLines="Vertical" CssClass="mGrid" PageSize="5" 
                onselectedindexchanged="img_session_grid_SelectedIndexChanged">
                <AlternatingRowStyle BackColor="#CCCCCC" />
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowSelectButton="True" 
                        SelectText="Set as Cover" />
                    <asp:BoundField DataField="session_img_id" HeaderText="session_img_id" 
                        InsertVisible="False" ReadOnly="True" SortExpression="session_img_path" 
                        Visible="False" />
                    <asp:BoundField DataField="session_img_name" HeaderText="Name" 
                        SortExpression="session_img_name" />
                    <asp:ImageField DataImageUrlField="session_img_path" HeaderText="Image">
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
                DeleteCommand="DELETE FROM [img_session] WHERE [session_img_id] = @original_session_img_id AND [session_img_name] = @original_session_img_name AND [session_id] = @original_session_id AND [session_img_path] = @original_session_img_path" 
                InsertCommand="INSERT INTO [img_session] ([session_img_name], [session_id], [session_img_path]) VALUES (@session_img_name, @session_id, @session_img_path)" 
                OldValuesParameterFormatString="original_{0}" 
                ProviderName="<%$ ConnectionStrings:DBCS.ProviderName %>" 
                SelectCommand="Select * from img_session where session_id=@id" 
                
                UpdateCommand="UPDATE [img_session] SET [session_img_name] = @session_img_name, [session_id] = @session_id, [session_img_path] = @session_img_path WHERE [session_img_id] = @original_session_img_id AND [session_img_name] = @original_session_img_name AND [session_id] = @original_session_id AND [session_img_path] = @original_session_img_path">
                <DeleteParameters>
                    <asp:Parameter Name="original_session_img_id" Type="Int32" />
                    <asp:Parameter Name="original_session_img_name" Type="String" />
                    <asp:Parameter Name="original_session_id" Type="Int32" />
                    <asp:Parameter Name="original_session_img_path" Type="String" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="session_img_name" Type="String" />
                    <asp:Parameter Name="session_id" Type="Int32" />
                    <asp:Parameter Name="session_img_path" Type="String" />
                </InsertParameters>
                <SelectParameters>
                    <asp:QueryStringParameter Name="id" QueryStringField="sid" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="session_img_name" Type="String" />
                    <asp:Parameter Name="session_id" Type="Int32" />
                    <asp:Parameter Name="session_img_path" Type="String" />
                    <asp:Parameter Name="original_session_img_id" Type="Int32" />
                    <asp:Parameter Name="original_session_img_name" Type="String" />
                    <asp:Parameter Name="original_session_id" Type="Int32" />
                    <asp:Parameter Name="original_session_img_path" Type="String" />
                </UpdateParameters>
            </asp:SqlDataSource>
        </div>
    </div>
</asp:Content>
