/// <reference path="jquery-1.8.3-vsdoc.js" />

$(function () {
    $("#cat_wrapper").mCustomScrollbar({
        horizontalScroll: true,
        scrollButtons: {
            enable: true,
        }
    });

    //Getting current category and turn it's button on to indicate that it's the selected one
    //Also adding hovering effect on the rest of catergories buttons
    var queryS = window.location.search.substr(1);
    var catVal = queryS.split("=")[1];
    $(".catBtn").each(function () {
        if ($(this).text() == catVal) {
            $(this).parent().siblings().css("opacity", "0.5").hover(
                function(){
                    $(this).animate({opacity:1.0}, 100);
                },
                function(){
                    $(this).animate({opacity:0.5}, 100);
                }
            );
        }
    });

    $("#imgSlider_ul").roundabout({
        minOpacity: 0.2,
        btnNext: $("#nextBtn"),
        btnPrev: $("#backBtn"),
        tilt:0,
        enableDrag: true,
    });

    $(".fancybox").fancybox();
});
