$(document).ready(function () {

    var uiCulture = $("#uiCulture").attr("data-uiCulture");

    $.ajax({
        url: "/scripts/countries.json",
        method: "get",
        success: function (countries) {

            if (uiCulture === "en-AU") {
                for (var i = 0; i < countries.length; i++) {
                    $("#CountryCode").append(`
                        <option value="${countries[i].code}">${countries[i].englishName}</option>
                    `);
                    $("#UpdateProfile_CountryCode").append(`
                        <option value="${countries[i].code}">${countries[i].englishName}</option>
                    `);
                }
            }
            else if(uiCulture === "ar-EG") {
                for (var c = 0; c < countries.length; c++) {
                    $("#CountryCode").append(`
                        <option value="${countries[c].code}">${countries[c].arabicName}</option>
                    `);
                    $("#UpdateProfile_CountryCode").append(`
                        <option value="${countries[c].code}">${countries[c].arabicName}</option>
                    `);
                }
            }

        }
    });
});