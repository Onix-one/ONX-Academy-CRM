$(function () {
    $(".knob").knob();
});
$(document).ready(function () {
    $("#show_hide_password a").on('click', function (event) {
        event.preventDefault();
        if ($('#show_hide_password input').attr("type") == "text") {
            $('#show_hide_password input').attr('type', 'password');
            $('#show_hide_password i').addClass("bx-hide");
            $('#show_hide_password i').removeClass("bx-show");
        } else if ($('#show_hide_password input').attr("type") == "password") {
            $('#show_hide_password input').attr('type', 'text');
            $('#show_hide_password i').removeClass("bx-hide");
            $('#show_hide_password i').addClass("bx-show");
        }
    });
});
$(document).ready(function () {
    $("#show_hide_confirm_password a").on('click', function (event) {
        event.preventDefault();
        if ($('#show_hide_confirm_password input').attr("type") == "text") {
            $('#show_hide_confirm_password input').attr('type', 'password');
            $('#show_hide_confirm_password i').addClass("bx-hide");
            $('#show_hide_confirm_password i').removeClass("bx-show");
        } else if ($('#show_hide_confirm_password input').attr("type") == "password") {
            $('#show_hide_confirm_password input').attr('type', 'text');
            $('#show_hide_confirm_password i').removeClass("bx-hide");
            $('#show_hide_confirm_password i').addClass("bx-show");
        }
    });
});
