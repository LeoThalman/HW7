$(document).ready(function () {

    $('#search').click(function () {
        var stext = $('#search-text').val().trim();
        stext = stext.replace(/\s+/g, '+');
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
        console.log(temp);
        for (var i = 0; i < temp.length; i+=1)
        {
            $('.gif-wrapper').append('<img src="' + temp[i].url + '">');
        }

        $('#message').text('Search Worked');
    }

    function failed() {
        $('#message').text('Failed');
    }
});