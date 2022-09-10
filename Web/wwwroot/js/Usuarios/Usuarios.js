﻿$(document).ready(function () {
    $('#usuarios').DataTable({
        ajax: {
            url: 'https://localhost:7215/api/Usuarios/BuscarUsuarios',
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
            { data: 'id_Rol', title: 'Id del Rol'},
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

$('#abrirModal').on('click', function () {
    $('#usuariosModal').modal('show');
});

