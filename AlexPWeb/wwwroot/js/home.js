$('#f').on('change', function (ev) {
    var f = ev.target.files[0];
    var fr = new FileReader();

    fr.onload = function (ev2) {
        console.dir(ev2);
        $('#i').attr('src', ev2.target.result);
    };

    fr.readAsDataURL(f);

    document.getElementById('convertImgToText').hidden = false;
});

$('#convertImgToText').click(function () {

    var imgSrc = document.getElementById("i").getAttribute("src");

    $.ajax({
        type: "POST",
        url: "Home/ConvertImgToText",
        async: true,
        data: {
            imgSrc: imgSrc,
        },
        success: function (result) {
            console.log(result.text);
        },
        error: function (req, status, error) {
            alert("error with convert img ajax call!")
        }
    });

});
