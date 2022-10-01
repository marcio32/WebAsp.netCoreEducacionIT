var tablaProductos;
$(document).ready(function () {
    var token = getCookie('Token');
   tablaProductos = $('#productos').DataTable({
        ajax: {
            url: 'https://localhost:7215/api/Productos/BuscarProductos',
           dataSrc: "",
           headers: { "Authorization": "Bearer " + token }
        },
        columns: [
            { data: 'id', title: 'Id' },
            {
                data: 'imagen', render: function (data) {
                    debugger
                    if (data != "") {
                        return '<img src="data:image/jpeg;base64,' + data + '"width="100px" height="100px">';
                    } else {
                        return "No tiene IMG"
                    }
                    
                } ,title: 'Imagen' },
            { data: 'precio', title: 'Precio' },
            { data: 'stock', title: 'Stock' },
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
        headers: { "Authorization": "Bearer " + token },
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
        headers: { "Authorization": "Bearer " + token },
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
        headers: { "Authorization": "Bearer " + token },
        success: function () {
            tablaProductos.ajax.reload();
        }
    })
}

