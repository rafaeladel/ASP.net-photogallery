/// <reference path="jquery-1.8.3-vsdoc.js" />

$(function () {
    $("#cat_wrapper").mCustomScrollbar({
        horizontalScroll: true,
        scrollButtons: {
            enable: true,
        }
    });


    $("#cat_wrapper ul li").hover(
        function(){
            $(this).siblings().animate({opacity:0.5}, 100);
        },
        function(){
            $(this).siblings().animate({opacity:1}, 100);
        }
    );


    
    $("#imgSlider_ul").roundabout({
        minOpacity: 0.2,
        btnNext: $("#nextBtn"),
        btnPrev: $("#backBtn"),
        tilt:0,
        enableDrag: true,
    });

    $(".fancybox").fancybox();
});
