﻿$(document).ready(function () {

    $('#search').click(function () {
        var stext = $('#search-text').val().trim();
        var lim = '&lim=' + $('input[name="lim"]:checked').val();
        var gifType = '&gifType=' + $('input[name="gifType"]:checked').val();
        stext = stext.replace(/[^a-zA-Z0-9]/g, ' ');
        stext = stext.trim();
        stext = stext.replace(/\s+/g, '+');
        console.log(stext);
        var query = "/Search/?q=" + stext + lim + gifType;

        $.ajax({
            type: "GET",
            dataType: "json",
            url: query,
            success: loadImages,
            error: failed
        });
    });

    function loadImages(data) {
        $('.gif-wrapper').empty();
        var temp = JSON.parse(data);
        for (var i = 0; i < temp.length; i += 1) {
            $('.gif-wrapper').append('<img src="' + temp[i].url + '">');
        }
    }
    function failed() {
        $('.gif-wrapper').append('<p>Error Loading images, try again.</p>');
    }

});