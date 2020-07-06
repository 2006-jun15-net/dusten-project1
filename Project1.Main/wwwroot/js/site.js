$("#login-form").submit(function () {

    let form_data = $(this).serialize();
    window.location.href = "customer/home?" + form_data;

    return false;
});
