using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NewsRss : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Utility.IsDBNullOrNullValue(Request.QueryString["SubjectId"]))
            return;
        Int16 SubjectId = Convert.ToInt16(Utility.DecryptQS(Request.QueryString["SubjectId"].ToString()));
        string Category = Utility.DecryptQS(Request.QueryString["Category"].ToString());
        TSP.DataManager.NewsManager NewsManager = new TSP.DataManager.NewsManager();
        rptRSS.DataSource = NewsManager.SearchNews(SubjectId, "%", "%", "1", "2", -1, -1);
        rptRSS.DataBind();
    }

    protected string FormatForXML(object input)
    {
        string data = input.ToString();      // cast the input to a string

        // replace those characters disallowed in XML documents
        data = data.Replace("&", "&amp;");
        data = data.Replace("\"", "&quot;");
        data = data.Replace("'", "&apos;");
        data = data.Replace("<", "&lt;");
        data = data.Replace(">", "&gt;");

        return data;
    }

    protected string EncryptQS(string s)
    {
       return Utility.EncryptQS(s);
    }
}