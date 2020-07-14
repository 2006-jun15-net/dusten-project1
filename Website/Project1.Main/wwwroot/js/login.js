$(document).ready(function () {

    console.log('ready');

    function alert_success(message) {

        $('#error-banner').css('display', 'none');

        $('#success-banner').html(message);
        $('#success-banner').css('display', 'block');
    }

    function alert_error(message) {

        $('#success-banner').css('display', 'none');

        $('#error-banner').html(message);
        $('#error-banner').css('display', 'block');
    }

    var url = '';

    $('#login-form').submit(function () {

        let formData = $(this).serialize();

        $.post(url, formData, function (response) {

            if (response.SuccessFlag) {

                alert_success(response.ResponseText);
                window.location.href = '/Customer/Home';
            }

            else {
                alert_error(response.ResponseText);
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
        }

        else if ($(this).attr('value') == 'create') {
            url = '/Customer/Create';
        }

        else {
            return false;
        }

        let form = $('#login-form');
        form.validate();

        if (form.valid()) {
            form.submit();
        }
    });

    $('#search-menu a').click(function (event) {

        event.preventDefault();

        console.log('clicked')

        let names = $(this).attr('value').split(' ');

        let firstname = names[0];
        let lastname = names[1];

        $('#login-form #firstname input[type=text]').val(firstname);
        $('#login-form #lastname input[type=text]').val(lastname);
    });
});
