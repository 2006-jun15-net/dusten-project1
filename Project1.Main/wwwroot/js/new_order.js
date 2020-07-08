$(document).ready(function () {

    $('#new-order-form').submit(function () {

        let formData = $(this).serializeArray();

        console.log(formData);
        alert('FORM DATA');

        return false;
    });

    $('#new-order-form button').click(function (event) {

        event.preventDefault();

        let form = $('#new-order-form');

        form.validate();

        if (form.valid()) {
            form.submit();
        }
    })
});