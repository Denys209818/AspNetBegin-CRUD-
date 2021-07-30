$(function () {
    var deleteItem;

    $("[data-delete-car]").on("click", function (e) {
        e.preventDefault();
        deleteItem = $(this).closest('a');
        $("#mymodal").modal("show");
    });

    $("#btnDelete").on("click", function () {
        var promise = new Promise((resolve, reject) => {
            $("#mymodal").modal("hide");
            resolve();
        })
            .then(() => {
                return new Promise((res, rej) => {
                    openModal();
                    res();
                });
            })
            .catch(() => { throw new Error("Вікно не відкрилося!"); })
            .then(() => {
                return new Promise((res, rej) => {
                    var el = axios.post(`/car/delete/${deleteItem.attr("data-id")}`);
                    return res(el);
                });
            })
            .catch(error => {
                throw error;
            })
            .then(() => {
                return new Promise((res, rej) => {
                    setTimeout(() => {
                        return res();
                    }, 2000);
                });
            }, error => {
                throw new Error(error.message);
            })
            .then(() => {
                closeModal();
                deleteItem.closest('tr').remove();
                openSuccessModal();

                Redirect();
            })
            .catch((errorMessage) => {
                closeModal();
                openFailedModal(errorMessage.message);
            });


    });

    function openModal() {
        $("#ownmodal").removeClass("d-none");
        $("#ownmodal").addClass("d-block");
    }

    function closeModal() {
        $("#ownmodal").addClass("d-none");
        $("#ownmodal").removeClass("d-block");
    }

    function openSuccessModal() {
        $("#successModal").modal("show");
    }

    function openFailedModal(str) {
        document.getElementById("failText").innerHTML = "На жаль при видаленні об'єкту виникла помилка: " + str;
        $("#failedModal").modal("show");
    }
});