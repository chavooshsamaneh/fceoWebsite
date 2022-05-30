function (s, e) {
    if (s.GetText() == '') return;

    var InputFile = s.GetText();
    var extension = new Array();

    extension[0] = ".png";
    extension[1] = ".gif";
    extension[2] = ".jpg";
    extension[3] = ".jpeg";
    extension[4] = ".bmp";


    var thisext = InputFile.substr(InputFile.lastIndexOf('.')).toLowerCase();
    for (var i = 0; i < extension.length; i++) {
        if (thisext == extension[i]) {
            s.Upload();
            return;
        }
    }
    alert("شما مجاز به آپلود این فایل نیستید");
    s.ClearText();
}