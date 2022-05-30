<%@ Page Language="C#" %>

<script runat="server">

    //OboutInc.Editor.FieldsFiller image_filler;

    void Page_Load(object o, EventArgs e)
    {
        //image_filler = new OboutInc.Editor.FieldsFiller(Page,"image-upload",Page.Request["localization_path"],Page.Request["language"]);
    }
</script>
<script type="text/JavaScript">
function onLoad()
{
    document.getElementById("frmFile").name = "frmFile";
    document.getElementById("fraExecute").contentWindow.name = "fraExecute";
    document.getElementById("fraExecute").name = "fraExecute";
}
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>obout ASP.NET HTML Editor examples</title>
      <style type="text/css">
			
			.tdText {
                font:11px Verdana;
                color:#333333;
            }
      </style>
<link rel="stylesheet" href="<%= Page.Request["css"] %>" media="all" />
</head>
<body style="overflow: hidden; margin: 0px; padding: 5px;" onload="onLoad()">
<form target="fraExecute" id="frmFile" action="myImageUpload.aspx" style="margin: 0px;" enctype="multipart/form-data" method="post">

</form>

<iframe id="fraExecute" width="0" height="0" style="display: none">
</iframe>
</body>
</html>
