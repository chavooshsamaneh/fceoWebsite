function (s, e) {
    if (s.GetText() == '') return;

    var InputFile = s.GetText();
    var extension = new Array();

    extension[0] = ".gif";
    extension[1] = ".jpeg";
    extension[2] = ".jpg";
    extension[3] = ".png";
    extension[4] = ".bmp";
    extension[5] = ".zip";
    extension[6] = ".rar";
    extension[7] = ".txt";
    extension[8] = ".pdf";
    extension[9] = ".doc";
    extension[10] = ".docx";
    extension[11] = ".xls";
    extension[12] = ".xlsx";
    extension[13] = ".html";
    extension[14] = ".htm";
    extension[15] = ".rtf";
    extension[16] = ".swf";
    extension[17] = ".repx";
    extension[18] = ".dwg";
    extension[19] = ".edb";
    extension[20] = ".e2k";
    extension[21] = ".fdb";
    extension[22] = ".f2k";

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