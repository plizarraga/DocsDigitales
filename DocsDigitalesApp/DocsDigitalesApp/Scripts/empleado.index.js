var $gird_Empleados, $schema;

$schema = {
    model: {
        id: "id_empleado",
        fields: {
            Nombre: { type: "string" },
            rfc: { type: "string" },
            puesto: { type: "string" },
            sucursal: { type: "string" }
        }
    }
};

var $dsEmpleadosList = new kendo.data.DataSource({
    data: [],
    schema: $schema,
    aggregate: []
});

function ValidarRequeridos() {

    var valido = false;
    var Nombre = $('#frmNombre').val();
    var RFC = $('#frmRFC').val();
    var Puesto = $('#frmPuesto').val();
    var Sucursal = $('#frmSucursal').val();

    if (Nombre == "") {
        $("#frmNombre").parent().addClass('has-error');
        $("#errorNombre").show();
        valido = false;
    }
    else {
        $("#frmNombre").parent().removeClass('has-error');
        $("#errorNombre").hide();
        valido = true;
    };

    if (RFC == "") {
        $("#frmRFC").parent().addClass('has-error');
        $("#errorRFC").show();
        valido = false;
    }
    else {
        $("#frmRFC").parent().removeClass('has-error');
        $("#errorRFC").hide();
        valido = true;
    };

    if (Puesto == "") {
        $("#frmPuesto").parent().addClass('has-error');
        $("#errorPuesto").show();
        valido = false;
    }
    else {
        $("#frmPuesto").parent().removeClass('has-error');
        $("#errorPuesto").hide();
        valido = true;
    };

    if (Sucursal == -1) {
        $("#frmSucursal").parent().addClass('has-error');
        $("#errorSucursal").show();
        valido = false;
    }
    else {
        $("#frmSucursal").parent().removeClass('has-error');
        $("#errorSucursal").hide();
        valido = true;
    };

    return valido;
};

function AddEmpleado() {

    var EmpleadoModel = {
        id_empleado: -1,
        Nombre: $('#frmNombre').val(),
        rfc: $('#frmRFC').val(),
        puesto: $('#frmPuesto').val(),
        id_sucursal: $('#frmSucursal').val()
    };

    $.ajax({
        type: "POST",
        url: "Empleado/AddEmpleado",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        data: JSON.stringify({ model: EmpleadoModel }),
        success: function (msj) {
            console.log(msj);
            if (msj.respuesta != "") {
                $("#frmNuevaSucursal").modal("hide");
                window.location.reload();
                console.log(msj.respuesta);
            }
            else {
                console.log(msj.error);
            }
        },
        error: function (request, error) {
            console.log("error");
            alert(request.statusText);
        }
    })
};

//Inicializar el grid empleados List de KendoUI
function Ini_GridEmpleadoslist() {
    $gird_Empleados = $("#grid_empleados_list").kendoGrid({
        height: 400,
        scrollable: true,
        sortable: true,
        filterable: {
            extra: false,
            messages: {
                filter: "Filtrar",
                clear: "Limpiar"
            }
        },
        groupable: true,
        selectable: "row",
        resizable: true,
        dataSource: $dsEmpleadosList,
        columns: [
                   {
                       field: "id_empleado", title: "id", filterable: true,
                       headerAttributes: {
                           style: "text-align: center;"
                       }
                   },
                   {
                       field: "Nombre", title: "Nombre", width: "20%",
                       headerAttributes: {
                           style: "text-align: center;"
                       },
                       attributes: {
                           style: "text-align: left;"
                       }
                   },
                   {
                       field: "rfc", title: "RFC",
                       headerAttributes: {
                           style: "text-align: center;"
                       }
                   },
                   {
                       field: "puesto", title: "Puesto",
                       headerAttributes: {
                           style: "text-align: center;"
                       }
                   },
                   {
                       field: "sucursal", title: "Sucursal",
                       headerAttributes: {
                           style: "text-align: center;"
                       }
                   }
        ]
    });

    $("#grid_empleados_list").data("kendoGrid").hideColumn(0);
}; //Ini_GridEmpleadoslist()

function GetEmpleados() {
    $.ajax({
        type: "POST",
        url: "Empleado/GetEmpleados",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (msg) {
            if (msg.Listado.length > 0) {
                $('#mensaje').hide();
                $('#grid_empleados_list').show();
                //Cargamos el listado de empleados
                $dsEmpleadosList = new kendo.data.DataSource({
                    data: msg.Listado,
                    schema: $schema,
                    aggregate: []
                });

                // Agregando el listado de sucursales al Source del Grid
                $gird_Empleados.data('kendoGrid').setDataSource($dsEmpleadosList);
            } else {
                $('#mensaje').show();
                $('#grid_empleados_list').hide();
            }
        },       //success END                
        error: function (request, error) {
            alert(request.statusText);
        }
    }); //$.ajax
}; // GetEmpleados()

$(document).ready(function () {

    // Evento click del boton de guardar empleado
    $('#btnGuardarEmpleado').on('click', function () {
        if (ValidarRequeridos()) {
            AddEmpleado();
        };
    });

    // Limpiar el modal dialog
    $('#frmNuevoEmpleado').on('hidden.bs.modal', function (e) {
        $('#frmNuevoEmpleado input').val('');
        $('#frmNuevoEmpleado select').val(-1);
    })

    Ini_GridEmpleadoslist();
    GetEmpleados();
});
