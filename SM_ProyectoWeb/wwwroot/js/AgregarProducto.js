$(function () {

    $.validator.addMethod("extensionPng", function (value, element) {
        return this.optional(element) || /\.png$/i.test(value);
    }, "Solo se permiten imágenes en formato .png.");

    $.validator.addMethod("maxFileSize", function (value, element, param) {
        if (element.files.length === 0) return true;
        return element.files[0].size <= param;
    }, "El tamaño del archivo no debe superar los 2 MB.");

    $("#FormAgregarProducto").validate({
        rules: {
            Nombre: {
                required: true,
                maxlength: 100
            },
            Descripcion: {
                required: true,
                maxlength: 2000
            },
            Precio: {
                required: true,
                number: true,
                min: 0.01,
                maxlength: 8
            },
            Imagen: {
                required: true,
                extensionPng: true,
                maxFileSize: 2 * 1024 * 1024
            }
        },
        messages: {
            Nombre: {
                required: "Requerido",
                maxlength: "Máximo 100 caracteres."
            },
            Descripcion: {
                required: "Requerido",
                maxlength: "Máximo 2000 caracteres."
            },
            Precio: {
                required: "Requerido",
                number: "Debe ingresar un valor numérico.",
                min: "El precio no puede ser negativo.",
                maxlength: "Máximo 8 dígitos.",
                pattern: "Máximo 8 dígitos enteros y opcionalmente decimales."
            },
            Imagen: {
                required: "Requerido",
                extensionPng: "Solo se permiten archivos con extensión .png.",
                maxFileSize: "El archivo no debe superar los 2 MB."
            }
        }
    });

});