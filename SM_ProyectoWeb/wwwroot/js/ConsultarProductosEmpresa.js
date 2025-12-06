$(function () {

  var modal = document.getElementById("ratingModal");

  modal.addEventListener("show.bs.modal", function (event) {

    var trigger = event.relatedTarget;
    var id = trigger.getAttribute("data-id");
    document.getElementById("ConsecutivoProducto").value = id;
  });


  const stars = document.querySelectorAll(".star");
  const ratingValue = document.getElementById("ratingValue");
  let currentRating = 0;

  stars.forEach((star) => {
    star.addEventListener("click", () => {
      currentRating = star.getAttribute("data-value");
      ratingValue.textContent = currentRating;
      highlightStars(currentRating);
    });
  });

  function highlightStars(value) {
    stars.forEach((star) => {
      star.style.color = star.getAttribute("data-value") <= value ? "gold" : "gray";
    });
  }

  function resetStars() {
    currentRating = 0;
    ratingValue.textContent = "0";
    stars.forEach((star) => (star.style.color = "gray"));
    document.getElementById("comentario").value = "";
  }

  // Botón guardar
  document.getElementById("saveRating").addEventListener("click", () => {

    let calificacion = currentRating;
    let comentarios = document.getElementById("comentario").value;
    let consecutivoProducto = document.getElementById("ConsecutivoProducto").value;


    $.ajax({
      url: '/Producto/RegistrarCalificacion',
      type: 'POST',
      contentType: 'application/json',
      data:
        JSON.stringify({
          CantidadEstrellas: calificacion,
          Comentario: comentarios,
          ConsecutivoProducto: consecutivoProducto
        }),
      success: function (response) {

        // cerrar modal usando la API de Bootstrap
        const modalEl = document.getElementById("ratingModal");
        const modal = bootstrap.Modal.getInstance(modalEl);
        modal.hide();

        // limpiar estrellas DESPUÉS de cerrar
        resetStars();

      }
    });


  });

  // Limpieza adicional cuando el modal se abre de nuevo
  document.getElementById("ratingModal").addEventListener("shown.bs.modal", () => {
    resetStars();
  });

});