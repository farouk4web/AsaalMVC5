$(document).ready(function () {

    $("#cameraBtn").on("click", function () {
        $("#userPictureInput").click();
    });

    $("#userPictureInput").change(function () {
        $("#userPictureForm").submit();
    });
    
    // remove succes alert after page load
    $(".alert").delay(3000).fadeOut();


    $(".userStatistics").on("click", "h4", function () {
        // remove active class from anoter headings and give current head active class
        $(this).parents(".userStatistics").find("h4").removeClass("active");
        $(this).addClass("active");

        //get the targret section want to show
        var teargetItem = $(this).attr("data-target");

        $(`.userJobs #${teargetItem}`).siblings().slideUp();
        $(`.userJobs #${teargetItem}`).slideDown();

    });   



});