$(function () {
    $("#FormRegistro").validate({
        rules: {
            Identificacion: {
                required: true
            },
            Nombre: {
                required: true
            },
            CorreoElectronico: {
                required: true
            },
            Contrasenna: {
                required: true
            }
        },
        messages: {
            Identificacion: {
                required: "Requerido"
            },
            Nombre: {
                required: "Requerido"
            },
            CorreoElectronico: {
                required: "Requerido"
            },
            Contrasenna: {
                required: "Requerido"
            }
        }
    });
});

function ConsultarNombre() {

    let identificacion = $("#Identificacion").val();
    $("#Nombre").val("");

    if (identificacion.length >= 9) {

        $.ajax({
            type: 'GET',
            url: 'https://apis.gometa.org/cedulas/' + identificacion,
            dataType: 'json',
            success: function (data) {
                $("#Nombre").val(data.nombre);
            }
        });

    }

}