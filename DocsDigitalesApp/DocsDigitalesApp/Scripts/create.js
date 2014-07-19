
// Este script se utiliza en la vista de Create (Registar usuario y empresa)

function ValidarRequeridos() {

    var valido = false;

    if ($("#Nombre").val() == "") {

        $("#Nombre").addClass('error');
        $("#errorNombre").show();
        valido = false;
    }
    else {
        $("#Nombre").removeClass('error');
        $("#errorNombre").hide();
        valido = true;
    };

    if ($("#CorreoElectronico").val() == "") {

        $("#CorreoElectronico").addClass('error');
        $("#errorCorreo").show();
        valido = false;
    }
    else {
        $("#CorreoElectronico").removeClass('error');
        $("#errorCorreo").hide();
        valido = true;
    };

    if ($("#RFC").val() == "") {

        $("#RFC").addClass('error');
        $("#errorRFC").show();
        valido = false;
    }
    else {
        $("#RFC").removeClass('error');
        $("#errorRFC").hide();
        valido = true;
    };

    if ($("#Empresa").val() == "") {

        $("#Empresa").addClass('error');
        $("#errorEmpresa").show();
        valido = false;
    }
    else {
        $("#Empresa").removeClass('error');
        $("#errorEmpresa").hide();
        valido = true;
    };

    if ($("#Contrasena").val() == "") {

        $("#Contrasena").addClass('error');
        $("#errorContrasena").show();
        valido = false;
    }
    else {
        $("#Contrasena").removeClass('error');
        $("#errorContrasena").hide();
        valido = true;
    };

    if ($("#ContrasenaConfirm").val() == "") {

        $("#ContrasenaConfirm").addClass('error');
        $("#errorContrasenaConfirm").show();
        valido = false;
    }
    else {
        $("#ContrasenaConfirm").removeClass('error');
        $("#errorContrasenaConfirm").hide();
        valido = true;
    };

    if ($("#Contrasena").val() != "" && $("#ContrasenaConfirm").val() != "") {
        if ($("#Contrasena").val() !== $("#ContrasenaConfirm").val()) {
            $("#Contrasena, #ContrasenaConfirm").addClass('error');
            $("#Contrasena, #ContrasenaConfirm").val("");
            $("#errorContrasenas").show();
            valido = false;
        }
        else {
            $("#Contrasena, #ContrasenaConfirm").removeClass('error');
            $("#errorContrasenas").hide();
            valido = true;
        };
    };

    return valido;
};

function AddUsuario() {
    var UsuarioModel = {
        nombre: $("#Nombre").val(),
        CorreoElectronico: $("#CorreoElectronico").val(),
        RFC: $("#RFC").val(),
        Empresa: $("#Empresa").val(),
        Contrasena: $("#Contrasena").val(),
        ContrasenaConfirm: $("#ContrasenaConfirm").val()
    };

    $.ajax({
        type: "POST",
        url: "Create",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        data: JSON.stringify({ model: UsuarioModel }),
        success: function (msj) {
            console.log(msj);
            if (msj.respuesta != "") {
                msjAlerta("alert-danger", "", false);
                $("#myModal").modal("show");
                setTimeout(function () { window.open('Login', '_self', false) }, 7000);
            }
        },
        error: function (request, error) {
            console.log("error");
            alert(request.statusText);
        }
    })
};

// Si la empresa no se encuentra registrada procede a insertar el usuario y la nueva empresa.
function IfExistsEmpresa() {
    $.ajax({
        type: "POST",
        url: "IfExistEmpresaCorreo",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        data: JSON.stringify({ NombreEmpresa: $("#Empresa").val(), CorreoElectronico: $("#CorreoElectronico").val() }),
        success: function (msj) {
            console.log(msj);
            if (msj.empresa || msj.correo) {
                var msjEmpresa = msj.empresa == 1 ? "- Ya existe una empresa registrada con el nombre: " + $("#Empresa").val() + "<br>" : "";
                var msjCorreo = msj.correo == 1 ? "- Ya se encuentra registrado el correo electronico: " + $("#CorreoElectronico").val() : "";

                msjAlerta("alert-danger", msjEmpresa + msjCorreo, true);
            } else {
                AddUsuario();
                console.log("insertar usuario y empresa");
            };
        },
        error: function (request, error) {
            console.log("error");
            alert(request.statusText);
        }
    })
};

function msjAlerta(tipo, mensaje, visible) {
    var $msjAlerta = $("#msjAlerta");

    if (visible) {
        $msjAlerta.html(mensaje);
        $msjAlerta.addClass(tipo);
        $msjAlerta.show();
    }
    else {
        $msjAlerta.hide();
        $msjAlerta.html("");
    }
};

$(document).ready(function () {

    $("#btnCrearUsuario").on("click", function () {
        if (ValidarRequeridos()) {
            IfExistsEmpresa();
        };
    });

});