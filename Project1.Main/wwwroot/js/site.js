$("#login-form").submit(function () {

    let formData = $(this).serialize();

    alert(formData);

    $.post('/Customer/Home', formData, function (response) {

        if (response.success) {
            window.location.href = '/Customer/Home';
        }
    });

    return false;
});
