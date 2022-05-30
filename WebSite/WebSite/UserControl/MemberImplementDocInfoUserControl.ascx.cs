using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_MemberImplementDocInfoUserControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void FillInfo(int MeId)
    {
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        //DocMemberFileManager.SelectImpDocLastVersionByMeId(MeId);
        //if (DocMemberFileManager.Count == 0)
        //    lblImplementDoc.Text = "- - -";
        //else
        //{
        //    lblImplementDoc.Text = DocMemberFileManager[0]["MfNo"].ToString();
        //    lblImplementDocFileDate.Text = DocMemberFileManager[0]["ExpireDate"].ToString();
        //}
        DocMemberFileManager.SelectImpDocSubRequest(MeId, -1);
        if (DocMemberFileManager.Count > 0)
        {
            lblImplementDoc.Text = DocMemberFileManager[0]["MfNo"].ToString();
            lblImplementDocFileDate.Text = DocMemberFileManager[0]["ExpireDate"].ToString();
            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["MFType"]))
                lblLastReqMFType.Text = DocMemberFileManager[0]["MFType"].ToString();
            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["TaskName"]))
                lblLastTaskName.Text = DocMemberFileManager[0]["TaskName"].ToString();
            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["CreateDate"]))
                lblLastReqCreatDate.Text = DocMemberFileManager[0]["CreateDate"].ToString();
        }     
    }

    public void Clear()
    {
        lblImplementDoc.Text = lblImplementDocFileDate.Text = "- - -";
    }
}