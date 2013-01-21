<%@ Page Title="" Language="C#" MasterPageFile="~/gallery_master.Master" AutoEventWireup="true" CodeBehind="offers.aspx.cs" Inherits="photography.offers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="Stylesheet" type="text/css" href="css/offers.css" />
    <script type="text/javascript">
        $(function () {
            $(".offerTitle").click(function () {
                if ($(this).siblings().css("display") != "none") {
                    $(this).siblings().slideUp(200);
                    $(this).find("img").addClass("closed").removeClass("opened");
                } else {
                    $(this).siblings().slideDown(200).end().parent().siblings().find(".offerBody").slideUp(100);
                    $(this).find("img").removeClass("closed").addClass("opened");
                    $(this).parent().siblings().find(".offerTitle img").removeClass("opened").addClass("closed");
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="AOC">
        <div class="sectionTitle">
            <h3>Latest Offers</h3>
        </div>
        <div class="sectionBody">
            <asp:Repeater ID="offers_repeater" runat="server">
                <ItemTemplate>
                    <div class="offer">
                        <div class="offerTitle">
                            <p>
                                <%# Eval("offer_title") %>
                            </p>
                            <img src="img/temp.png" class="closed" alt="Alternate Text" />
                        </div>
                        <div class="offerBody">
                            <p>
                                <%# Eval("offer_body") %>
                            </p>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>            
        </div>
    </div>
</asp:Content>
