$(document).ready(function () {

    $('#search').click(function () {
        var stext = $('#search-text').val().trim();
        stext = stext.relpace(/[^a-zA-Z0-9]/g, ' ');
        stext = stext.trim();
        stext = stext.replace(/\s+/g, '+');
        console.log(stext);
        var query = "/Search/?q=" + stext;

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
});