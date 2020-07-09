$(document).ready(function () {

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

    $('#new-order-form').submit(function () {

        let formData = $(this).serializeArray();

        // Assume the order is correct here. Issues will be
        // caught in StoreController
        let formObj = {}
        formObj[formData[0]['name']] = formData[0]['value'];

        let lines = []

        for (let i = 1; i < formData.langth; i += 3) {

            lines.push({
                'ProductQuantity': formData[i]['value'],
                'Product': {
                    'Name': formData[i + 1]['value'],
                    'Price': formData[i + 1]['value']
                }
            })
        }

        formObj['lines'] = lines;

        $.post('/Store/CreateOrder', formObj, function (response) {

            if (response.success) {
                alert_success(response.responseText);
            }

            else {
                alert_error(response.responseText);
            }

        }).fail(function () {
            alert_error('Request failed');
        })

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