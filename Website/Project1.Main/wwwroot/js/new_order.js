$(document).ready(function () {

    function alert_success(message) {

        $('#error-banner').css('display', 'none');
        $('#warning-banner').css('display', 'none');

        $('#success-banner').html(message);
        $('#success-banner').css('display', 'block');
    }

    function alert_warning(message) {

        $('#success-banner').css('display', 'none');
        $('#error-banner').css('display', 'none');

        $('#warning-banner').html(message);
        $('#warning-banner').css('display', 'block');
    }

    function alert_error(message) {

        $('#success-banner').css('display', 'none');
        $('#warning-banner').css('display', 'none');

        $('#error-banner').html(message);
        $('#error-banner').css('display', 'block');
    }

    $('#new-order-form').submit(function () {

        let formData = $(this).serializeArray();

        console.log(formData);

        let formObj = {};
        formObj[formData[0]['name']] = formData[0]['value'];

        let lines = [];
        let totalItems = 0;

        for (let i = 1; i < formData.length; i += 2) {

            var quantity = parseInt(formData[i]['value']);

            if (quantity <= 0) {
                continue;
            }

            totalItems += quantity;

            lines.push({
                'ProductQuantity': quantity,
                'Product': {
                    'Id': formData[i + 1]['value'],
                }
            })
        }

        if (lines.length == 0) {
            alert_warning('Order contains no items');
        }

        else if (totalItems > 20) {
            alert_warning('Order contains too many items');
        }

        else {

            formObj['lines'] = lines;
            formObj['storeName'] = $('#Name').attr('value');

            $.post('/Store/CreateOrder', formObj, function (response) {

                if (response.success) {
                    alert_success(response.responseText);
                }

                else {
                    alert_error(response.responseText);
                }

            }).fail(function () {
                alert_error('Request failed');
            });
        }

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