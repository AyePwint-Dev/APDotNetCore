function successMessage(message) {
  Swal.fire({
    title: "Confirm",
    text: message,
  });
}

function errorMessage(message) {
  Notiflix.Report.failure("Error!", message, "Ok");
}

function confirmMessage(message) {
  let confirmMessageResult = new Promise(function (success, error) {
    Notiflix.Confirm.show(
      "Confirm",
      message,
      "Yes",
      "No",
      function okCb() {
        success();
      },
      function cancelCb() {
        error();
      }
    );
  });
  return confirmMessageResult;
}