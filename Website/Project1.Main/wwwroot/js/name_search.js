$(document).ready(function () {

    $('#search').keyup(function (event) {

        let inputStr = $(this).val().toLowerCase();

        $('#search-menu a').each(function (index, value) {

            let textValue = $(this).attr('value').toLowerCase();

            if (textValue.indexOf(inputStr) > -1) {
                $(this).css('display', 'block');
            }

            else {
                $(this).css('display', 'none');
            }
        });
    });
});