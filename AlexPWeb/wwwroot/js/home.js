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
    document.getElementById('loaderContainer').hidden = false;
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
            document.getElementById('imgText').value = result.text;
            document.getElementById('imgText').hidden = false;
            document.getElementById('fileName').hidden = false;
            document.getElementById('saveTxtFileBtn').hidden = false;
            document.getElementById('loaderContainer').hidden = true;
        },
        error: function (req, status, error) {
            alert("error with convert img ajax call!")
        }
    });

});

$('#saveTxtFileBtn').click(function () {
    var textToSave = document.getElementById('imgText').value;
    var fileName = document.getElementById('fileName').value;
    var blob = new Blob([textToSave], { type: "text/plain;charset=utf-8" });
    if (fileName != null && fileName != '') 
        saveAs(blob, fileName);
    else
        saveAs(blob, "ExtractedText.txt");
});
