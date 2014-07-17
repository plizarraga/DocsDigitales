
function ValidarRequeridos() {

    $("#btnIniciarSesion").on("click", function () {

        if ($("#correoElectronico").val() == "") {

            $("#correoElectronico").addClass('error');
            $("#errorCorreo").show();
            return false;
        }
        else {
            $("#correoElectronico").removeClass('error');
            $("#errorCorreo").hide();
            if (!validateEmail($("#correoElectronico").val())) { }
        };

        if ($("#contrasena").val() == "") {

            $("#contrasena").addClass('error');
            $("#errorContrasena").show();
            return false;
        }
        else {
            $("#contrasena").removeClass('error');
            $("#errorContrasena").show();
        };

    });
};

function validateEmail(email) {
    var emailReg = "/^([\w-\.]+@@([\w-]+\.)+[\w-]{2,4})?$/";
    if (!emailReg.test(email)) {
        return false;
    } else {
        return true;
    }
};