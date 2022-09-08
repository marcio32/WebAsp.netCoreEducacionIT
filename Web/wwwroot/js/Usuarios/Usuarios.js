$(document).ready(function () {
    $('#usuarios').DataTable({
        ajax: {
            url: 'https://localhost:7215/api/Usuarios/BuscarUsuarios',
            dataSrc: ""
        },
        columns: [
            {data: 'id', title:'Id'},
            {data: 'nombre', title:'Nombre'},
            {data: 'apellido', title:'Apellido'},
            { data: 'activo', title:'Activo'},
            
        ],
        languaje: {
            url:"https://cdn.datatables.net/plug-ins/1.10.19a/i18n/Spanish.json"
        }
    });
});


$('#botonpulsado').on('click', function () {
    $.ajax({
        url: 'https://randomuser.me/api/',
        dataType: 'json',
        success: function (data) {
            debugger
            console.log(data);
        }
    });

})



