$(document).ready(function () {

    $('#search').keyup(function (event) {

        let inputStr = $(this).val().toLowerCase();

        $('#product-table tbody tr').each(function (index, value) {

            let textValue = $(this).find('tr#product-name').text().toLowerCase();

            if (textValue.indexOf(inputStr) > -1) {
                $(this).css('display', 'block');
            }

            else {
                $(this).css('display', 'none');
            }
        });
    });
});