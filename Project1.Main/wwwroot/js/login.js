$(document).ready(function () {

    function alert_success (message) {

        $('#error-banner').css('display', 'none');

        $('#success-banner').html(message);
        $('#success-banner').css('display', 'block');
    }

    function alert_error (message) {

        $('#success-banner').css('display', 'none');

        $('#error-banner').html(message);
        $('#error-banner').css('display', 'block');
    }

    var url = '';

    $('#login-form').submit(function () {

        let formData = $(this).serialize();

        $.post(url, formData, function (response) {

            if (response.success) {

                alert_success (response.responseText);
                window.location.href = '/Customer/Home';
            }

            else {
                alert_error(response.responseText);
            }

        }).fail(function () {
            alert_error('Request failed');
        });

        return false;
    });

    $('#login-form button').click(function (event) {

        event.preventDefault();

        if ($(this).attr('value') == 'login') {

            url = '/Customer/Login';

            let form = $('#login-form');
            form.validate();

            if (form.valid()) {
                form.submit();
            }
        }

        else if ($(this).attr('value') == 'create') {

            url = '/Customer/Create';

            let form = $('#login-form');
            form.validate();

            if (form.valid()) {
                form.submit();
            }
        }
    });

});
