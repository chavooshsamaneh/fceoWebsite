// JScript File
// Copyright © 2000 by Apple Computer, Inc., All Rights Reserved.
//
// You may incorporate this Apple sample code into your own code
// without restriction. This Apple sample code has been provided "AS IS"
// and the responsibility for its operation is yours. You may redistribute
// this code, but you are not permitted to redistribute it as
// "Apple sample code" after having made changes.
//
// ************************
// layer utility routines *
// ************************

/*Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginReq);
//Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);

function beginReq(sender, args) {
// shows the Popup 
$find(ModalProgress).show();
}

function endReq(sender, args) {
//  shows the Popup 
$find(ModalProgress).hide();
} 
*/

var verticalpos = "fromtop";
var persistclose = 0;
var startX = 3;
var startY = 3;
function iecompattest() {
    return (document.compatMode && document.compatMode != "BackCompat") ? document.documentElement : document.body;
}
function staticbar() {
    barheight = document.getElementById("ctl00_ContentPlaceHolder1_DivReport").offsetHeight
    var ns = (navigator.appName.indexOf("Netscape") != -1) || window.opera;
    var d = document;
    function ml(id) {
        var el = d.getElementById(id);
        if (!persistclose || persistclose && get_cookie("remainclosed") == "")
            el.style.visibility = "visible"
        if (d.layers) el.style = el;
        el.sP = function (x, y) { this.style.left = x + "px"; this.style.top = y + "px"; };
        el.x = startX;
        if (verticalpos == "fromtop")
            el.y = startY;
        else {
            el.y = ns ? pageYOffset + innerHeight : iecompattest().scrollTop + iecompattest().clientHeight;
            el.y -= startY;
        }
        return el;
    }

    window.stayTopLeft = function () {
        if (verticalpos == "fromtop") {
            var pY = ns ? pageYOffset : iecompattest().scrollTop;
            ftlObj.y += (pY + startY - ftlObj.y) / 8;
        }
        else {
            var pY = ns ? pageYOffset + innerHeight - barheight : iecompattest().scrollTop + iecompattest().clientHeight - barheight;
            ftlObj.y += (pY - startY - ftlObj.y) / 8;
        }
        ftlObj.sP(ftlObj.x, ftlObj.y);
        setTimeout("stayTopLeft()", 10);
    }
    ftlObj = ml("ctl00_ContentPlaceHolder1_DivReport");
    stayTopLeft();
}

function ltr_override(e) {

    //‫از این تابع در onkeypress هر textBox که استفاده شود باعث می‌شود که
    //کاراکترهای تایپ شده توسط کاربر به طور مطلق از چپ به راست نمایش داده
    //شود. حتی اگر کاربر یک متن فارسی را وارد کند. کاربرد فعلی این تابع
    //در شماره نامه است که همیشه از چپ به راست خوانده می‌شود

    //‫دقت شود که رشته تولید شده توسط این تابع پر از کاراکتر یونیکد 0x202d است و
    //قبل از ذخیره در بانک اطلاعاتی یا انجام هر پردازش دیگری باید از رشته مورد نظر پاک شود

    var key;
    var obj;
    if (window.event) {
        e = window.event;
        obj = e.srcElement;
        key = e.keyCode;
    } else {
        obj = e.target;
        key = e.charCode;
    }
    if (obj.value.indexOf(String.fromCharCode(0x202d)) == -1)
    //0x202d: LEFT-TO-RIGHT OVERRIDE
    //obj.value=obj.value.replace(String.fromCharCode(0x202d),'')
        obj.value = String.fromCharCode(0x202d) + obj.value;
    if (obj.value == String.fromCharCode(0x202d))
        obj.value = '';

    return true;
}

function ChangeVisible(id) {
    id.style.display = 'none';
    return true;
}
function ChangeIcon(id) {
    id.style.cursor = "hand";
    return true;
}
function getStyleObject(objectId) {
    // cross-browser function to get an object's style object given its id
    if (document.getElementById && document.getElementById(objectId)) {
        // W3C DOM
        return document.getElementById(objectId).style;
    } else if (document.all && document.all(objectId)) {
        // MSIE 4 DOM
        return document.all(objectId).style;
    } else if (document.layers && document.layers[objectId]) {
        // NN 4 DOM.. note: this won't find nested layers
        return document.layers[objectId];
    } else {
        return false;
    }
} // getStyleObject

function changeObjectVisibility(objectId, newVisibility) {
    // get a reference to the cross-browser style object and make sure the object exists
    var styleObject = getStyleObject(objectId);
    if (styleObject) {
        styleObject.visibility = newVisibility;
        return true;
    } else {
        // we couldn't find the object, so we can't change its visibility
        return false;
    }
} // changeObjectVisibility

function moveObject(objectId, newXCoordinate, newYCoordinate) {
    // get a reference to the cross-browser style object and make sure the object exists
    var styleObject = getStyleObject(objectId);
    if (styleObject) {
        styleObject.left = newXCoordinate;
        styleObject.top = newYCoordinate;
        return true;
    } else {
        // we couldn't find the object, so we can't very well move it
        return false;
    }
} // moveObject


function MM_CheckFlashVersion(reqVerStr, msg) {
    with (navigator) {
        var isIE = (appVersion.indexOf("MSIE") != -1 && userAgent.indexOf("Opera") == -1);
        var isWin = (appVersion.toLowerCase().indexOf("win") != -1);
        if (!isIE || !isWin) {
            var flashVer = -1;
            if (plugins && plugins.length > 0) {
                var desc = plugins["Shockwave Flash"] ? plugins["Shockwave Flash"].description : "";
                desc = plugins["Shockwave Flash 2.0"] ? plugins["Shockwave Flash 2.0"].description : desc;
                if (desc == "") flashVer = -1;
                else {
                    var descArr = desc.split(" ");
                    var tempArrMajor = descArr[2].split(".");
                    var verMajor = tempArrMajor[0];
                    var tempArrMinor = (descArr[3] != "") ? descArr[3].split("r") : descArr[4].split("r");
                    var verMinor = (tempArrMinor[1] > 0) ? tempArrMinor[1] : 0;
                    flashVer = parseFloat(verMajor + "." + verMinor);
                }
            }
            // WebTV has Flash Player 4 or lower -- too low for video
            else if (userAgent.toLowerCase().indexOf("webtv") != -1) flashVer = 4.0;

            var verArr = reqVerStr.split(",");
            var reqVer = parseFloat(verArr[0] + "." + verArr[2]);

            if (flashVer < reqVer) {
                if (confirm(msg))
                    window.location = "http://www.macromedia.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash";
            }
        }
    }
}

function copyToClipboard(s) {
    if (window.clipboardData && clipboardData.setData) {
        clipboardData.setData("Text", s);
    }
    else {
        alert('کپی اطلاعات امکان پذیر نمی باشد');
        /*
        // You have to sign the code to enable this or allow the action in about:config by changing
        user_pref("signed.applets.codebase_principal_support", true);
        netscape.security.PrivilegeManager.enablePrivilege('UniversalXPConnect');

        var clip Components.classes['@mozilla.org/widget/clipboard;[[[[1]]]]'].createInstance(Components.interfaces.nsIClipboard);
        if (!clip) return;

        // create a transferable
        var trans = Components.classes['@mozilla.org/widget/transferable;[[[[1]]]]'].createInstance(Components.interfaces.nsITransferable);
        if (!trans) return;

        // specify the data we wish to handle. Plaintext in this case.
        trans.addDataFlavor('text/unicode');

        // To get the data from the transferable we need two new objects
        var str = new Object();
        var len = new Object();

        var str = Components.classes["@mozilla.org/supports-string;[[[[1]]]]"].createInstance(Components.interfaces.nsISupportsString);

        var copytext=meintext;

        str.data=copytext;

        trans.setTransferData("text/unicode",str,copytext.length*[[[[2]]]]);

        var clipid=Components.interfaces.nsIClipboard;

        if (!clip) return false;

        clip.setData(trans,null,clipid.kGlobalClipboard);
        */
    }
}

function CheckCharacterEncoding(Str) {
    for (i = 0; i < Str.length; i++) {
        if (Str.charCodeAt(i) > 128)
            return false;
    }
    return true;
}

function ShowHelpWindow(HelpFile) {
    window.open(HelpFile, null, "height=500px, width=700px,resizable=yes, status=no, toolbar=no, menubar=no, location=no, scrollbars=1");
}

function Blink(Id) {
    setInterval('BlinkIt(\'' + Id + '\')', 500);
}

function BlinkIt(ItemId) {
    s = document.getElementsByTagName('blink');
    for (i = 0; i < s.length; i++) {
        if (ItemId.toString() != '' && s[i].id.toString() != ItemId.toString()) { continue; }
        s[i].style.visibility = (s[i].style.visibility == 'visible') ? 'hidden' : 'visible';
    }
}

function SearchKeyPress(e, Type, BtnClientInstanceName) {    
    if (Type == 1)//DevExpress controls
    {
        if (e.htmlEvent.keyCode == 13) {
            BtnClientInstanceName.DoClick();
        }
    }
    else if (Type == 2)//asp controls
    {
        if (e.keyCode == 13)
            BtnClientInstanceName.DoClick();
    }
}

function CheckDevExpressTextboxLengthOnKeyPress(string, e, maxLength) {
    if (string.GetText().length >= maxLength)
        e.htmlEvent.preventDefault();
}

function isValidIranianNationalCode(input) {
    if (!/^\d{10}$/.test(input))
        return false;

    var check = parseInt(input[9]);
    var sum = 0;
    var i;
    for (i = 0; i < 9; ++i) {
        sum += parseInt(input[i]) * (10 - i);
    }
    sum %= 11;

    return (sum < 2 && check == sum) || (sum >= 2 && check + sum == 11);
}
function onIranianNationalCodeValidation(s, e) {
    var NationalCode = e.value;
    if (NationalCode == null || NationalCode == "")
        return;
    if (!isValidIranianNationalCode(NationalCode))
        e.isValid = false;
}
$(".a2a_rp_show_hide").on("click", function () { $(".a2a_kit").hasClass("a2a_rp_hide") ? $(".a2a_kit").removeClass("a2a_rp_hide") : $(".a2a_kit").addClass("a2a_rp_hide") })


$(window).bind('resize load', function () {
    if ($(this).width() < 767) {
        $('.panel-collapse').removeClass('in');
        $('.panel-collapse').addClass('out');
        $('.panel-collapse').parent().find(".glyphicon-chevron-up").removeClass("glyphicon-chevron-up").addClass("glyphicon-chevron-down");
    } else {
        $('.panel-collapse').removeClass('out');
        $('.panel-collapse').addClass('in');
        $('.panel-collapse').parent().find(".glyphicon-chevron-down").removeClass("glyphicon-chevron-down").addClass("glyphicon-chevron-up");
    }
});

$('.collapse').on('shown.bs.collapse', function () {
    $(this).parent().find(".glyphicon-chevron-down").removeClass("glyphicon-chevron-down").addClass("glyphicon-chevron-up");
}).on('hidden.bs.collapse', function () {
    $(this).parent().find(".glyphicon-chevron-up").removeClass("glyphicon-chevron-up").addClass("glyphicon-chevron-down");
});

// ===== Scroll to Top ==== 
$(window).scroll(function () {
    if ($(this).scrollTop() >= 350) {        // If page is scrolled more than 50px
        $('#return-to-top').fadeIn(200);    // Fade in the arrow
    } else {
        $('#return-to-top').fadeOut(200);   // Else fade out the arrow
    }
});
$('#return-to-top').click(function () {      // When arrow is clicked
    $('body,html').animate({
        scrollTop: 0                       // Scroll to top of body
    }, 500);
});