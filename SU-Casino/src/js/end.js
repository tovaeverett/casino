$(document).ready(function () {
    $("#introInfoText").html($("#hiddenfield_text").val());
});
$("#btnLogin").click(function () {
    if ($("#username").val() === 'sysAdmin' && $("#password").val() === 'us-casino18')
        $(".admin-content").hide();
});


