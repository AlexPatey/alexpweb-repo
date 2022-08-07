$(document).ready(function () {

    function checkFields() {

        if (document.getElementById("email").value != "" && document.getElementById("password").value != "") {
            $("#submit").prop("disabled", false);
        }
        else {
            $("#submit").prop("disabled", true);
        }
    }

    $("#email").keyup(function () {
        checkFields()
    });

    $("#password").keyup(function () {
        checkFields()
    });
});