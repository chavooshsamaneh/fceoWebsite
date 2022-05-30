using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

public partial class UploadAction : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["EDA_GETSTREAMDATA"] == "EDA_YES")
        {
            //string fullFileName = Server.MapPath("testnewone.txt");
            //String fs = File.ReadAllText(fullFileName);

            String fullFileName = Server.MapPath(Request.Params["DocumentID"]);
            Byte[] fs = File.ReadAllBytes(fullFileName);

            Response.Write("Get Stream Successfully!");
            Response.Write("EDA_STREAMBOUNDARY");
            Response.BinaryWrite(fs);
            Response.Write("EDA_STREAMBOUNDARY");
	    Response.End();
        }
        else
        {
            if (Request.Params["author"] == "anyname" && Request.Params["Data"] == "2010-6-15")
            {
                Response.Write("0\n");
                Response.Write("We have receipted the right param from Office ActiveX Control.");
            }
            if (Request.Files.Count == 0)
            {
                Response.Write("0\n");
                Response.Write("There isn't file to upload.");
                Response.End();
            }
            if (Request.Files[0].ContentLength == 0)
            {
                Response.Write("0\n");
                Response.Write("Failed to receipt the data.\n\n");
                Response.End();
            }
            for (int i = 0; i < Request.Files.Count; i++)
            {
                string fullFileName = Server.MapPath(Request.Files[i].FileName);
                Request.Files[i].SaveAs(fullFileName);
            }
            Response.Write("Upload Successfully.");
            Response.End();
        }
    }
}


