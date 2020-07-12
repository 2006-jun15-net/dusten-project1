$(document).ready(function () {

    $('#search').keyup(function (event) {

        event.preventDefault();

        let inputStr = $(this).attr('value').toLowerCase();

        $('#search-menu a').each(function (index, value) {

            console.log(value);

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