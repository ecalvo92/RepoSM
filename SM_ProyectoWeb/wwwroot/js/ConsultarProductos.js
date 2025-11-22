$(function () {

  $('#Tabla').DataTable({
    "language": {
      "url": "https://cdn.datatables.net/plug-ins/1.10.25/i18n/Spanish.json"
    }
  });

  var modal = document.getElementById("staticBackdrop");

  modal.addEventListener("show.bs.modal", function (event) {

    var trigger = event.relatedTarget;
    var id = trigger.getAttribute("data-id");
    var name = trigger.getAttribute("data-name");
    var status = trigger.getAttribute("data-status");

    var msg = (status === "True")
      ? "¿Está seguro de INACTIVAR el producto: "
      : "¿Está seguro de ACTIVAR el producto: ";

    document.getElementById("ConsecutivoProducto").value = id;
    document.getElementById("message").textContent = msg + name + "?";

  });

});