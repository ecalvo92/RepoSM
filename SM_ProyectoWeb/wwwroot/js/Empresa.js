$(function () {

  $.validator.addMethod("extensionPng", function (value, element) {
    return this.optional(element) || /\.png$/i.test(value);
  }, "Solo se permiten imágenes en formato .png.");

  $.validator.addMethod("maxFileSize", function (value, element, param) {
    if (element.files.length === 0) return true;
    return element.files[0].size <= param;
  }, "El tamaño del archivo no debe superar los 2 MB.");

  $("#FormEmpresa").validate({
    rules: {
      NombreComercial: {
        required: true,
        maxlength: 100
      },
      ImagenComercial: {
        extensionPng: true,
        maxFileSize: 2 * 1024 * 1024
      }
    },
    messages: {
      NombreComercial: {
        required: "Requerido",
        maxlength: "Máximo 100 caracteres."
      },
      ImagenComercial: {
        extensionPng: "Solo se permiten archivos con extensión .png.",
        maxFileSize: "El archivo no debe superar los 2 MB."
      }
    }
  });

});