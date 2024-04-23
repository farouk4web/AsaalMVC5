
// colors for normal design
var normalColors = [
    {
        "colorName": "--color1",
        "value": "#DD4814"
    },
    {
        "colorName": "--color2",
        "value": "#262120"
    },
    {
        "colorName": "--linkColor",
        "value": "#3399cc"
    },
    {
        "colorName": "--mainFontColor",
        "value": "#333"
    },
    {
        "colorName": "--whiteFontColor",
        "value": "#fff"
    },
    {
        "colorName": "--bgColor",
        "value": "#f2f2f2"
    },
    {
        "colorName": "--whiteBgColor",
        "value": "#fff"
    },
    {
        "colorName": "--borderColor",
        "value": "#ccc"
    }
];

// colors for darkmode colors
var darkModeColors = [
    {
        "colorName": "--color1",
        "value": "#DD4814"
    },
    {
        "colorName": "--color2",
        "value": "#323538"
    },
    {
        "colorName": "--linkColor",
        "value": "#3399cc"
    },
    {
        "colorName": "--mainFontColor",
        "value": "#c8c3bc"
    },
    {
        "colorName": "--whiteFontColor",
        "value": "#fff"
    },
    {
        "colorName": "--bgColor",
        "value": "#1F2223"
    },
    {
        "colorName": "--whiteBgColor",
        "value": "#181A1B"
    },
    {
        "colorName": "--borderColor",
        "value": "#3e4446"
    }
];

// loop to check if colors stored in local storage or not
for (var i = 0; i < normalColors.length; i++) {
    var colorInStorage = window.localStorage.getItem(normalColors[i].colorName);

    if (colorInStorage !== null) {
        // if found dark mmode colors in local storage set it 
        document.documentElement
            .style.setProperty
            (darkModeColors[i].colorName, colorInStorage);

        // get a way to know if colors in local are dark or not

        if (i === 1) {
            if (colorInStorage === "#262120") {
                // normal colors are working now
                document.getElementById("setDarkMode").style.display = "inline";

            } else {
                // DARK colors are working now
                document.getElementById("setNormalMode").style.display = "inline";
            }
        }


    } else {
        // but if we dont find it, so set normal colors
        document.documentElement
            .style.setProperty
            (normalColors[i].colorName, normalColors[i].value);

        document.getElementById("setDarkMode").style.display = "inline";
    }
}


// set dark mode 
document.getElementById("setDarkMode").onclick = function () {

    // remove ancient colors
    window.localStorage.clear();

    for (var d = 0; d < darkModeColors.length; d++) {

        // store colors on local storage
        window.localStorage.
            setItem(darkModeColors[d].colorName, darkModeColors[d].value);

        // set colors on css
        document.documentElement
            .style.setProperty
            (darkModeColors[d].colorName, darkModeColors[d].value);
    }

    // hide current btn and show another
    document.getElementById("setDarkMode").style.display = "none";
    document.getElementById("setNormalMode").style.display = "inline";
};


// set normal mode
document.getElementById("setNormalMode").onclick = function ()  {
    // remove ancient colors
    window.localStorage.clear();

    for (var n = 0; n < normalColors.length; n++) {

        // store colors on local storage
        window.localStorage.
            setItem(normalColors[n].colorName, normalColors[n].value);

        // set colors on css
        document.documentElement
            .style.setProperty
            (normalColors[n].colorName, normalColors[n].value);
    }

    // hide current btn and show another
    document.getElementById("setNormalMode").style.display = "none";
    document.getElementById("setDarkMode").style.display = "inline";
};
