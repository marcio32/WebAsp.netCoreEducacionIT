﻿var tablaServicios;

$(document).ready(function () {
    var token = getCookie('Token');
    tablaServicios = $('#servicios').DataTable({
        ajax: {
            url: 'https://localhost:7215/api/Servicios/BuscarServicios',
            dataSrc: "",
            headers: { "Authorization": "Bearer " + token }
        },
        columns: [
            { data: 'id', title: 'Id' },
            { data: 'nombre', title: 'Nombre' },
            {
                data: function (row) {
                    return row.activo == true ? "Si" : "No"
                }, title: 'Activo'
            },
            {
                data: function (row) {
                    var botones =
                        `<td><a href='javascript:EditarServicio(${JSON.stringify(row)})'><i class="fa-solid fa-pen-to-square editarServicio"></i></td>` +
                        `<td><a href='javascript:EliminarServicio(${JSON.stringify(row)})'><i class="fa-solid fa-trash eliminarServicio"></i></td>`
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

function GuardarServicio(row) {
    $("#serviciosAddPartial").html("");
    
    $.ajax({
        type: "POST",
        url: "/Servicios/ServiciosAddPartial",
        data: "",
        contentType: "application/json",
        dataType: "html",
        success: function (resultado) {
            
            $("#serviciosAddPartial").html(resultado);
            $('#serviciosModal').modal('show');
        }
    })
}

function EditarServicio(row) {
    $.ajax({
        type: "POST",
        url: "/Servicios/ServiciosAddPartial",
        data: JSON.stringify(row),
        contentType: "application/json",
        dataType: "html",
        success: function (resultado) {
            $("#serviciosAddPartial").html(resultado);
            $('#serviciosModal').modal('show');
        }
    })
}

function EliminarServicio(row) {
   
    Swal.fire({
        title: 'Estas Seguro?',
        text: "Vas a eliminar un servicio!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        cancelButtonText: 'Cancelar',
        confirmButtonText: 'Eliminar!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                url: "/Servicios/EliminarServicio",
                data: JSON.stringify(row),
                contentType: "application/json",
                dataType: "html",
                success: function () {
                    Swal.fire(
                        'Eliminado!'
                    )
                    tablaServicios.ajax.reload();
                }
            })
        }
    })
  
}

