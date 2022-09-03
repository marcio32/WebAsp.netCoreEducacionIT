$(document).ready(function () {
    $('#usuarios').DataTable({
        ajax: {
            url: 'https://randomuser.me/api/',
            dataSrc: "results"
        },
        columns: [
            {data: 'name.first', title:'Nombre'},
            {data: 'name.last', title:'Apellido'},
            { data: 'location.country', title:'Pais'},
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



