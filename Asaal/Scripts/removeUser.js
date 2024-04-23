$(document).ready(function () {

    var removeAccountWarningMsg = $("#removeAccountWarningMsg").attr("data-removeAccountWarningMsg");

    $("#removeAccountBtn").on("click", function () {
        bootbox.confirm(removeAccountWarningMsg, function (result) {

            if (result === true) {
                $("#removeAccountForm").submit();
            }
        });
    });

});