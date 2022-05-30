using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Epaymenttest2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {   TSP.Utilities.EpaymentToken.TokensClient client = new TSP.Utilities.EpaymentToken.TokensClient();
         TSP.Utilities.EpaymentToken.tokenResponse tokenResp = client.MakeToken("1000", "AA6E", "54545545", "662584852", "",
            "TestEPaymentVerify.aspx", "Test Sample");
        token.Value = tokenResp.token;
        merchantId.Value = "AA6E";

        // var token = Request["sass"];
    }
}