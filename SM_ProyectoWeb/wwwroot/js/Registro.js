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