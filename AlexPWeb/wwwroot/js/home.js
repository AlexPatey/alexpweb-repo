$('#fileUpload').on('change', function (ev) {
    var f = ev.target.files[0];
    var fr = new FileReader();

    fr.onload = function (ev2) {
        console.dir(ev2);
        $('img').attr('src', ev2.target.result);
    };

    fr.readAsDataURL(f);
    document.getElementById('imgPreviewText').hidden = false;
    document.getElementById('convertImgToText').hidden = false;
});

$('#convertImgToText').click(function () {

    var imgSrc = document.getElementById('img').getAttribute('src');

    $.ajax({
        type: "POST",
        url: "Home/ConvertImgToText",
        async: true,
        data: {
            imgSrc: imgSrc,
        },
        success: function (result) {
            console.log(result.text);
            document.getElementById('convertedText').hidden = false;
            document.getElementById('imgText').innerHTML = result.text;
            document.getElementById('imgText').hidden = false;
            document.getElementById('saveTxtFileBtn').hidden = false;
        },
        error: function (req, status, error) {
            alert("error with convert img ajax call!")
        }
    });

});

$('#saveTxtFileBtn').click(function () {
    var textToSave = document.getElementById('imgText').textContent;
    var blob = new Blob([textToSave], { type: "text/plain;charset=utf-8" });
    saveAs(blob, "helloWorld.txt");
});
