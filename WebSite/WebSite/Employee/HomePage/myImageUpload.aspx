<%@ Page Language="C#" Debug="true"%>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Drawing.Imaging" %>
<%--<%@ Register TagPrefix="ed" Namespace="OboutInc.Editor" Assembly="obout_Editor" %>--%>

<script language="C#" runat="server">
void Page_Load(object sender, EventArgs e)
{
  string uploadingFolder = "~/image/News/Editor/";

  Page.Response.Write("<html>");
  Page.Response.Write("<head>");
  Page.Response.Write("<scr"+"ipt>");
  Page.Response.Write("var imageSaved = \""+Page.Request["notsaved"]+"\";");
  Page.Response.Write("var imageFileName = null;");
  Page.Response.Write("var imageFileTitle = null;");

  if(Page.Request.Files.Count > 0)
  {
    System.Web.HttpPostedFile PFile = Page.Request.Files[0];
    string ext = Path.GetExtension(PFile.FileName).ToLower();

    if(ext==".gif" || ext==".jpg" || ext==".jpeg" || ext==".png")
    if(PFile.FileName.Trim().Length > 0 && PFile.ContentLength > 0)
    {
        string fileN= Utility.GenerateName(ext);
  
      string fName = Page.MapPath(ResolveUrl(uploadingFolder+fileN));//  Path.GetFileName(PFile.FileName)));

      try
      {
         if(File.Exists(fName)) File.Delete(fName);
         PFile.SaveAs(fName);

         string title = Page.Request["title"];

         if(title.Length > 0)
         {
           fName += ".description";
           if(File.Exists(fName)) File.Delete(fName);
           StreamWriter sw = new StreamWriter(fName);
           sw.Write(title);
           sw.Close();
         }
         Page.Response.Write("imageSaved = \""+Page.Request["saved"]+"\";");
         Page.Response.Write("imageFileName = \""+ResolveUrl(uploadingFolder)+fileN+"\";");
         Page.Response.Write("imageFileTitle = \""+title+"\";");
      }
      catch(Exception ev)
      {
         Page.Response.Write("imageSaved = \""+ev.Message.Replace("\n"," ").Replace("\r"," ").Replace("\\","\\\\").Replace("\"","\\\"")+"\\n\\nTurn to your System Administrator.\";");
      }
    }
  }

  Page.Response.Write("</scr"+"ipt>");
  Page.Response.Write("</head>");
  Page.Response.Write("<body>");
  Page.Response.Write("</body>");
  Page.Response.Write("</html>");
  Page.Response.End();
}
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title></title>
</head>
<body></body>
</html>
