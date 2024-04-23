$(document).ready(function () {

    $(".navbar").addClass("navbar-fixed-top");

    var navbarHeight = $(".navbar").outerHeight(),
        footerHeight = $("footer").outerHeight(),
        windowHeight = $(window).height(),
        contentSiteHeight = windowHeight - footerHeight;

    $(".contentOfSite").css({
        "minHeight": contentSiteHeight,
        "paddingTop": navbarHeight
    });

    $("#showPanelNavbar").on("click", function () {
        $(".panelNavbar").toggle("slow");    
    });


    //set bg color of colors
    $(".card").each(function () {
        $(this).css("background", $(this).attr("data-color"));
    });


    $('.changeFavicon').on("submit", 'form', function (e) {

        if ($(this).find(":file").val() === "") {
            e.preventDefault();
            $(this).siblings(".alert").show();
        }

    });

    $('.changeLogo').on("submit", 'form', function (e) {

        if ($(this).find(":file").val() === "") {
            e.preventDefault();
            $(this).siblings(".alert").show();
        }

    });

});