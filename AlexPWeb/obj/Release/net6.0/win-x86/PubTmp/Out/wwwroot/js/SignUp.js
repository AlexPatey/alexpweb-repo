$(document).ready(function () {

    function checkFields() {

        if (document.getElementById("email").value != "" && document.getElementById("password").value != "" && document.getElementById("confirmPassword").value != "" && document.getElementById("displayName").value != "") {
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

    $("#confirmPassword").keyup(function () {
        checkFields()
    });

    $("#displayName").keyup(function () {
        checkFields()
    });
});