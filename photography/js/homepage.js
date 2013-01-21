/// <reference path="jquery-1.8.3-vsdoc.js" />

$(function () {
    $("#slides_wrapper div").hover(
            function () {
                $(this).stop().animate({ width: '200px' }, 200);
            },
            function () {
                $(this).stop().animate({ width: '120px' }, 200);
            });
});