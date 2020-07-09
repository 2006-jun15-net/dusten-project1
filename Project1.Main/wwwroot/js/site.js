function set_session_timeout() {
    setTimeout("keep_session_alive()", 5 * 60 * 1000);
}

function keep_session_alive() {

    $.ajax({
        type: 'POST',
        url: '/Home/KeepSessionAlive',
        success: function () {
            set_session_timeout();
        }
    })
}