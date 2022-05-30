using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using System.Drawing;


/// <summary>
/// Summary description for SaveImageInFolder
/// </summary>
[WebService(Namespace = "http://www.fceo.ir/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class SaveImageInFolder : System.Web.Services.WebService {


    private string _UserName
    {
        get
        {
            return "SNMFSaveImage";
        }
    }

    private string _PassWord
    {
        get
        {
            return "SNMFSaveImage";
        }
    }

    public SaveImageInFolder () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(Description = "this func get a Image & path due to saving in result returns string status")]
    public string SaveImage(string UserName, string PassWord, byte[] BytImage, string path, string name)
    {
        try
        {
            string FileName = path + name;

            Image Img = byteArrayToImage(BytImage);
          
            Bitmap BmpImg = (Bitmap)Img;

            BmpImg.Save(HttpContext.Current.Server.MapPath(FileName),System.Drawing.Imaging.ImageFormat.Png);
            return "Success";
        }
        catch (Exception err)
        {
           Utility.SaveWebsiteError(err);
           return err.ToString();
        }
        
    }

    public Image byteArrayToImage(byte[] byteArrayIn)
    {
        MemoryStream ms = new MemoryStream(byteArrayIn);
        Image returnImage = Image.FromStream(ms);
        Bitmap img = (Bitmap)returnImage;
        return returnImage;
    }
    
}
