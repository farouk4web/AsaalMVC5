$(document).ready(function () {

    var navbarHeight = $(".navbar").outerHeight(),
        footerHeight = $("footer").outerHeight(),
        windowHeight = $(window).height(),
        contentSiteHeight = windowHeight - (navbarHeight + footerHeight);

    $(".contentOfSite").css("minHeight", contentSiteHeight);

    $("p.societyDescription").each(function () {
        var newDescription = $(this).text().substring(0, 100) + " .....";
        $(this).text(newDescription).length;
    });

    $("p.questionBody").each(function () {
        var newBody = $(this).text().substring(0, 300) + " .....";
        $(this).text(newBody);
    });

    $(".lastCommentContent").each(function () {
        var newBody = $(this).text().substring(0, 80) + " .....";
        $(this).text(newBody);
    });
     
    $(".questionTitle").each(function () {
        var newBody = $(this).text().substring(0, 100) + " .....";
        $(this).text(newBody);
    });


    // change country code to country name in current langauage
    $(".country").each(function () {
        var uiCulture = $("#uiCulture").attr("data-uiCulture"),
            countryCode = $(this).attr("data-countryCode"),
            thisCountry = $(this); 

        $.ajax({
            url: "/scripts/countries.json",
            method: "get",
            success: function (countries) {
                if (uiCulture === "ar-EG") {

                    var countryInAr = countries.find(c => c.code === countryCode);
                    thisCountry.text(countryInAr.arabicName);

                }
                else if (uiCulture === "en-AU") {

                    var countryInEn = countries.find(c => c.code === countryCode);
                    thisCountry.text(countryInEn.englishName);

                }
                else {
                    console.log("sorry, there is a problem");
                }

    
            }
        });
    });
});