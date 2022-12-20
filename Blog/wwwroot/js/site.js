// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.


    $(function () {
        const validationCustomSettings = {
        validClass: "is-valid",
    errorClass: "is-invalid",
        };
    $.validator.setDefaults(validationCustomSettings);
    $.validator.unobtrusive.options = validationCustomSettings;
    });

