var tablaUsuarios;

$(document).ready(function () {
    
    var token = getCookie('Token');
   tablaUsuarios = $('#usuarios').DataTable({
        ajax: {
            url: 'https://localhost:7215/api/Usuarios/BuscarUsuarios',
           dataSrc: "",
           headers: {"Authorization": "Bearer " + token}
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
                        `<td><a href='javascript:EditarUsuario(${JSON.stringify(row)})'><i class="fa-solid fa-pen-to-square editarUsuario"></i></td>` +
                        `<td><a href='javascript:EliminarUsuario(${JSON.stringify(row)})'><i class="fa-solid fa-trash eliminarUsuario"></i></td>`
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

function GuardarUsuario(row) {
    $("#usuariosAddPartial").html("");
    $.ajax({
        type: "POST",
        url: "/Usuarios/UsuariosAddPartial",
        data: "",
        contentType: "application/json",
        dataType: "html",
        success: function (resultado) {
            
            $("#usuariosAddPartial").html(resultado);
            $('#usuariosModal').modal('show');
        }
    })
}

function EditarUsuario(row) {
    $.ajax({
        type: "POST", 
        url: "/Usuarios/UsuariosAddPartial",
        data: JSON.stringify(row),
        contentType: "application/json",
        dataType: "html",
        success: function (resultado) {
            $("#usuariosAddPartial").html(resultado);
            $('#usuariosModal').modal('show');
        }
    })
}

function EliminarUsuario(row) {
    Swal.fire({
        title: 'Estas Seguro?',
        text: "Vas a eliminar un usuario!",
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
                url: "/Usuarios/EliminarUsuario",
                data: JSON.stringify(row),
                contentType: "application/json",
                dataType: "html",
                success: function () {
                    Swal.fire(
                        'Eliminado!'
                    )
                    tablaUsuarios.ajax.reload();
                }
            })
            
        }
    })

   
}

