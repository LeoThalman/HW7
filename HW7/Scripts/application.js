$(document).ready(function () {

    $('#search').click(function () {
        //the search terms
        var stext = $('#search-text').val().trim();
        //image limit
        var lim = '&lim=' + $('input[name="lim"]:checked').val();
        //what type of gif to return (still or animated)
        var gifType = '&gifType=' + $('input[name="gifType"]:checked').val();
        //trim out special characters and spaces
        stext = stext.replace(/[^a-zA-Z0-9]/g, ' ');
        stext = stext.trim();
        stext = stext.replace(/\s+/g, '+');
        //build query for server
        var query = "/Search/?q=" + stext + lim + gifType;

        //query the server, which then will query giphy and pass back a json object
        $.ajax({
            type: "GET",
            dataType: "json",
            url: query,
            success: loadImages,
            error: failed
        });
    });

    //on success appends the images to the gif-wrapper
    function loadImages(data) {
        $('.gif-wrapper').empty();
        var temp = JSON.parse(data);
        for (var i = 0; i < temp.length; i += 1) {
            $('.gif-wrapper').append('<img src="' + temp[i].url + '">');
        }
    }
    //on fail appends an error message to gif-wrapper
    function failed() {
        $('.gif-wrapper').empty();
        $('.gif-wrapper').append('<p>Error Loading images, try again.</p>');
    }
});