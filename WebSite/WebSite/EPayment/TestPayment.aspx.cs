using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EPayment_TestPayment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnBankReply_Click(object sender, EventArgs e)
    {
    //    //merchantId=F244&amount=400000&paymentId=438&CustomerId=E95172522835&revertURL=http://www.farsnezam.org/Members/Accounting/EpaymentFisheView.aspx?QS=RspQR6Ft3/a0Oc/YeIrcinHGsarRDaeeEkwlsYOeHcFMqINJGCwkH7kgY6xAcnS0

    //    // string QS = "RspQR6Ft3/a0Oc/YeIrcinHGsarRDaeeEkwlsYOeHcFMqINJGCwkH7kgY6xAcnS0";
    //     string QS = "BankReply;" + ((int)TSP.DataManager.TSAccountingAccType.).ToString() + ";" + "19714";
    //    Response.Redirect("../NezamRegister/WizardMemberFinish.aspx?QS=" + Utility.EncryptQS(QS)    
    //   // Response.Redirect("../Members/Accounting/EpaymentFisheView.aspx?QS=" + QS// Utility.EncryptQS(QS)   
    //    + "&resultCode=100"
    //   + "&paymentId=438"
    //    + "&referenceId=" + "000000122782"
    //     );
    //    //Response.Redirect("../NezamRegister/WizardMemberFinish.aspx?PgMode=" + Utility.EncryptQS("BankReply")
    //    //    + "&AccType=" + Utility.EncryptQS(((int)TSP.DataManager.TSAccountingAccType.DocMemberFile).ToString())
    //    //    + "&TableId=" + Utility.EncryptQS("18710")
    //    //     + "&resultCode=100"
    //    //    + "&paymentId=438"
    //    //     + "&referenceId="+"000000122782"
    //        //);
        try
        {
            ////string merchantId = "A093";
            ////string ReferenceId = "000045473176";
            //net.tejaratbank.pg.MerchantService MerchantService = new net.tejaratbank.pg.MerchantService();
            //net.tejaratbank.pg.verifyRequest verifyRequest = new net.tejaratbank.pg.verifyRequest();
            //verifyRequest.merchantId =txtMerchantId.Text;
            //verifyRequest.referenceNumber = txtReferenceId.Text;
            //int Value = Convert.ToInt32(MerchantService.verify(verifyRequest));
            //lblMessage.Text = Value.ToString();

           string resultOfflineDebtId = TSP.DataManager.Utility.OfflineDebtAddPayment(6044, "تست", "تست1", "20000", "123000604422", "800039245518");
            if (resultOfflineDebtId == "-1")
            {
                //TransactionManager.CancelSave();
                lblMessage.Text += "خطا در ثبت سند ایجاد شده است";
             
            }
            lblMessage.Text += "resultOfflineDebtId1=" + resultOfflineDebtId;

             resultOfflineDebtId = TSP.DataManager.Utility.OfflineDebtUpdatePayment(resultOfflineDebtId);
            if (resultOfflineDebtId == "-1")
            {
                //TransactionManager.CancelSave();
                lblMessage.Text += "خطا در ثبت سند ایجاد شده است";

            }
            lblMessage.Text += "**resultOfflineDebtId=" + resultOfflineDebtId;
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            lblMessage.Text = "خطا!!!!!!!!!!!!";
        }
    }
}