$(function () {
    function IsValid(objInput) {
        objInput.addClass("is-valid");
        objInput.removeClass("is-invalid");
    }

    function IsInvalid(objInput) {
        objInput.addClass("is-invalid");
        objInput.removeClass("is-valid");
    }

    function ValidText(variable, name) {
        variable.on("input", function () {
            var $parent = variable.parent();
            var $devSpan = $parent.children("span");
            devValue = $devSpan.text();
            console.log(variable.val());
            if (variable.val().length > 0 &&
                (parseInt(variable.val()[2]) || parseInt(variable.val()[0]) || variable.attr('name') != "Price")
            ) {
                if (variable.attr('name') == "Year" && variable.val().length == 4) {
                    IsValid(variable);
                    $devSpan.text("");
                } else if (variable.attr('name') == "Year" && variable.val().length != 4) {
                    IsInvalid(variable);
                    $devSpan.text(`Поле "${name}" не коректне!`);
                } else {
                    IsValid(variable);
                    $devSpan.text("");
                }
            } else {
                IsInvalid(variable);
                $devSpan.text(`Поле "${name}" не коректне!`);
            }

        });
    }

    var $developer = $("#Developer");
    ValidText($developer, "Виробник");

    var $model = $("#Model");
    ValidText($model, "Модель");
    var $price = $("#Price");

    ValidText($price, "Ціна");

    var priceMask = IMask(document.getElementById("Price"), {
        mask: '₴ num',
        blocks: {
            num: {
                thousandsSeparator: ' ',
                mask: Number
            }
        }
    });

    var $image = $("#Image");
    ValidText($image, "Фотографія");
    var $year = $("#Year");
    ValidText($year, "Рік випуску");

    var yearMask = IMask(document.getElementById("Year"), {
        mask: Number,
    });
});