using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Videos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ObjectDataSourcePodcast.SelectParameters["ImageType"].DefaultValue = ((int)TSP.DataManager.SiteImageManager.SiteImageType.Video).ToString();
    }
}