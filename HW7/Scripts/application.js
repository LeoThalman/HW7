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
        console.log(data);
        $('#message').text('Search Worked');
    }

    function failed() {
        $('#message').text('Failed');
    }
});