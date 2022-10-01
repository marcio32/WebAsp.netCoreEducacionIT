var tablaRoles;
var token;
$(document).ready(function () {
    token = getCookie('Token');
    tablaRoles = $('#roles').DataTable({
        ajax: {
            url: 'https://localhost:7215/api/Roles/BuscarRoles',
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
                        `<td><a href='javascript:EditarRol(${JSON.stringify(row)})'><i class="fa-solid fa-pen-to-square editarRol"></i></td>` +
                        `<td><a href='javascript:EliminarRol(${JSON.stringify(row)})'><i class="fa-solid fa-trash eliminarRol"></i></td>`
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

function GuardarRol(row) {
    $("#rolesAddPartial").html("");
    debugger
    $.ajax({
        type: "POST",
        url: "/Roles/RolesAddPartial",
        data: "",
        contentType: "application/json",
        dataType: "html",
        headers: { "Authorization": "Bearer " + token },
        success: function (resultado) {
            debugger
            $("#rolesAddPartial").html(resultado);
            $('#rolesModal').modal('show');
        }
    })
}

function EditarRol(row) {
    $.ajax({
        type: "POST",
        url: "/Roles/RolesAddPartial",
        data: JSON.stringify(row),
        contentType: "application/json",
        dataType: "html",
        headers: { "Authorization": "Bearer " + token },
        success: function (resultado) {
            $("#rolesAddPartial").html(resultado);
            $('#rolesModal').modal('show');
        }
    })
}

function EliminarRol(row) {
    debugger
    $.ajax({
        type: "POST",
        url: "/Roles/EliminarRol",
        data: JSON.stringify(row),
        contentType: "application/json",
        dataType: "html",
        headers: { "Authorization": "Bearer " + token },
        success: function () {
            debugger
            tablaRoles.ajax.reload();
        }
    })
}

