var tablaProductos;
$(document).ready(function () {
   tablaProductos = $('#productos').DataTable({
        ajax: {
            url: 'https://localhost:7215/api/Productos/BuscarProductos',
            dataSrc: ""
        },
        columns: [
            { data: 'id', title: 'Id' },
            { data: 'nombre', title: 'Nombre' },
            { data: 'apellido', title: 'Apellido' },
            {
                data: function (row) {
                    return moment(row.fecha_Nacimiento).format("DD/MM/YYYY");
                }, title: 'Fecha de Nacimiento'
            },
            { data: 'mail', title: 'Mail' },
            { data: 'roles.nombre', title: 'Rol'},
            {
                data: function (row) {
                    return row.activo == true ? "Si" : "No"
                }, title: 'Activo' },
            {
                data: function (row) {
                    var botones =
                        `<td><a href='javascript:EditarProducto(${JSON.stringify(row)})'><i class="fa-solid fa-pen-to-square editarProducto"></i></td>` +
                        `<td><a href='javascript:EliminarProducto(${JSON.stringify(row)})'><i class="fa-solid fa-trash eliminarProducto"></i></td>`
                    ;
                    return botones;
                }

            }


        ],
        languaje: {
            url: "https://cdn.datatables.net/plug-ins/1.10.19a/i18n/Spanish.json"
        }
    });
});

function GuardarProducto(row) {
    $("#productosAddPartial").html("");
    debugger
    $.ajax({
        type: "POST",
        url: "/Productos/ProductosAddPartial",
        data: "",
        contentType: "application/json",
        dataType: "html",
        success: function (resultado) {
            debugger
            $("#productosAddPartial").html(resultado);
            $('#productosModal').modal('show');
        }
    })
}

function EditarProducto(row) {
    $.ajax({
        type: "POST", 
        url: "/Productos/ProductosAddPartial",
        data: JSON.stringify(row),
        contentType: "application/json",
        dataType: "html",
        success: function (resultado) {
            $("#productosAddPartial").html(resultado);
            $('#productosModal').modal('show');
        }
    })
}

function EliminarProducto(row) {
    debugger
    $.ajax({
        type: "POST",
        url: "/Productos/EliminarProducto",
        data: JSON.stringify(row),
        contentType: "application/json",
        dataType: "html",
        success: function () {
            tablaProductos.ajax.reload();
        }
    })
}

