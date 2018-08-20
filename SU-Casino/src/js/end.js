var game = $('body')[0].id;

$(document).ready(function () {
    if (game === 'end') {
        $("#introInfoText").html($("#hiddenfield_text").val());
        $("#hiddenfield_text").val("0");
    }
    if (game === 'admin')
        localStorage.getItem("in") === "3x755v11" ? $(".admin-content").hide() : $(".admin-content").show();
});
$("#btnLogin").click(function () {
    if ($("#username").val() === 'sysAdmin' && $("#password").val() === 'us-casino18') {
        $(".admin-content").hide();
        localStorage.setItem("in", "3x755v11");
    }
});


