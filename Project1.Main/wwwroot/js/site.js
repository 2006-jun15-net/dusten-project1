$(document).ready(function () {

    var url = '';

    $("#login-form").submit(function () {

        let formData = $(this).serialize();

        alert(url);

        $.post(url, formData, function (response) {

            if (response.success) {
                window.location.href = '/Customer/Home';
            }

            else {
                alert(response.responseText);
            }
        });

        return false;
    });

    $('#login-form button').click(function (event) {

        event.preventDefault();

        if ($(this).attr('value') == 'login') {

            url = '/Customer/Home';
            $('#login-form').submit();
        }

        else if ($(this).attr('value') == 'create') {

            url = '/Customer/Create';
            $('#login-form').submit();
        }
    });

});
