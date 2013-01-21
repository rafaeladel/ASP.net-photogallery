/// <reference path="jquery-1.8.3-vsdoc.js" />
$(function () {
    $("#cat li").hover(function () { $(this).siblings().fadeTo("fast", "0.5"); }, function () { $(this).siblings().fadeTo("fast", "1.0") });
    $("#recentImages").hover(
                    function () {
                        $(this).stop().animate({ width: '50%' }, 300);
                    },
                    function () {
                        $(this).stop().animate({ width: '100%' }, 300);
                    });
});

