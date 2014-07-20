var $gird_Sucursales, $schema;

$schema = {
    model: {
        id: "id_sucursal",
        fields: {
            nombre: { type: "string" },
            calle: { type: "string" },
            colonia: { type: "string" },
            num_ext: { type: "number" },
            num_int: { type: "number" },
            codigo_postal: { type: "string" },
            ciudad: { type: "string" },
            pais: { type: "string" },
            no_empleados: { type: "number" }
        }
    }
};

var $dsSucursalesList = new kendo.data.DataSource({
    data: [],
    schema: $schema,
    aggregate: []
});

function ValidarRequeridos() {

    var valido = false;
    var Nombre = $('#frmNombre').val();
    var Calle = $('#frmCalle').val();
    var Colonia = $('#frmColonia').val();
    var NumExt = $('#frmNumExt').val();
    var NumInt = $('#frmNumInt').val();
    var CP = $('#frmCP').val();
    var Ciudad = $('#frmCiudad').val();
    var Pais = $('#frmPais').val();

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

    return valido;
};

// Si la sucursal no se encuentra registrada procede a insertar la nueva sucursal.
function IfExistSucursal() {
    $.ajax({
        type: "POST",
        url: "Sucursal/IfExistSucursal",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        data: JSON.stringify({ NombreSucursal: $("#frmNombre").val() }),
        success: function (msj) {
            console.log(msj);
            if (msj.sucursal) {
                var msj = "- Ya existe una Sucurasal registrada con el nombre: " + $("#frmNombre").val();
                $('#msjAlerta').html('<div class="alert alert-danger" role="alert">' + msj + '</div>');
                $('#msjAlerta').show();
            } else {
                AddSucursal();
            };
        },
        error: function (request, error) {
            console.log("error");
            alert(request.statusText);
        }
    })
};

function AddSucursal() {
    var SucursalModel = {
        id_sucursal: -1,
        id_empresa: -1,
        nombre: $('#frmNombre').val(),
        calle: $('#frmCalle').val(),
        colonia: $('#frmColonia').val(),
        num_ext: $('#frmNumExt').val(),
        num_int: $('#frmNumInt').val(),
        codigo_postal: $('#frmCP').val(),
        ciudad: $('#frmCiudad').val(),
        pais: $('#frmPais').val()
    };

    $.ajax({
        type: "POST",
        url: "Sucursal/AddSucursal",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        data: JSON.stringify({ model: SucursalModel }),
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

function GetSucursales() {
    $.ajax({
        type: "POST",
        url: "Sucursal/GetSucursales",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (msg) {
            if (msg.Listado.length > 0) {
                $('#mensaje').hide();
                $('#grid_sucursales_list').show();
                //Cargamos el listado de sucursales
                $dsSucursalesList = new kendo.data.DataSource({
                    data: msg.Listado,
                    schema: $schema,
                    aggregate: [
                    //{ field: "id_sucursal", aggregate: "count" }
                    ]
                });

                // Agregando el listado de sucursales al Source del Grid
                $gird_Sucursales.data('kendoGrid').setDataSource($dsSucursalesList);
            } else {
                $('#mensaje').show();
                $('#grid_sucursales_list').hide();
            }
        },       //success END                
        error: function (request, error) {
            alert(request.statusText);
        }
    }); //$.ajax
}; // GetSsucursales()

//Inicializar el grid sucursales List de KendoUI
function Ini_GridSucursaleslist() {
    $gird_Sucursales = $("#grid_sucursales_list").kendoGrid({
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
        dataSource: $dsSucursalesList,
        columns: [
                   {
                       field: "id_sucursal", title: "id_sucursal", filterable: true,
                       headerAttributes: {
                           style: "text-align: center;"
                       }
                   },
                   {
                       field: "nombre", title: "Sucursal", width: "20%",
                       headerAttributes: {
                           style: "text-align: center;"
                       },
                       attributes: {
                           style: "text-align: left;"
                       }
                   },
                   {
                       field: "calle", title: "Calle",
                       headerAttributes: {
                           style: "text-align: center;"
                       }
                   },
                   {
                       field: "colonia", title: "Colonia",
                       headerAttributes: {
                           style: "text-align: center;"
                       },
                       attributes: {
                           style: "text-align: left;"
                       }
                   },
                    {
                        field: "num_ext", title: "Num. Ext.",
                        headerAttributes: {
                            style: "text-align: center;"
                        }
                    },
                     {
                         field: "num_int", title: "Num. Int.",
                         headerAttributes: {
                             style: "text-align: left;"
                         }
                     },
                    {
                        field: "codigo_postal", title: "CP",
                        headerAttributes: {
                            style: "text-align: left;"
                        }
                    },
                     {
                         field: "ciudad", title: "Ciudad",
                         headerAttributes: {
                             style: "text-align: center;"
                         }
                     },
                     {
                         field: "pais", title: "Pais",
                         headerAttributes: {
                             style: "text-align: center;"
                         }
                     },
                     {
                         field: "no_empleados", title: "Total Empleados",
                         template: "<span class=\"badge \">#=no_empleados#</span>",
                         headerAttributes: {
                             style: "text-align: center;"
                         },
                         attributes: {
                             style: "text-align: center;"
                         }
                     }
        ]
    });

    $("#grid_sucursales_list").data("kendoGrid").hideColumn(0);
}; //Ini_GridSucursaleslist()

$(document).ready(function () {
    //Inicializar controles númericos
    $('#frmNumExt, #frmNumInt, #frmCP').numeric({ decimal: false, negative: false });

    $('#btnGuardarSucursal').on('click', function () {
        if (ValidarRequeridos()) {
            IfExistSucursal();
        };
    });

    // Limpiar el modal dialog
    $('#frmNuevaSucursal').on('hidden.bs.modal', function (e) {
        $('#frmNuevaSucursal input').val('');
    })

    Ini_GridSucursaleslist();
    GetSucursales();
});