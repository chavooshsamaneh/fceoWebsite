using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Employee_EmployeeHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                #region User Task And Messages
                TSP.DataManager.EmployeeManager EmployeeManager = new TSP.DataManager.EmployeeManager();
                int CartableGroup = (int)TSP.DataManager.Automation.CartableGroupsManager.DefaultGroup;
                ArrayList arrCount = EmployeeManager.FindCountOfUserTaskAndMessage(Utility.GetCurrentUser_MeId(), Utility.GetCurrentUser_LoginType(), 0, CartableGroup);
                //******ArrayList[0]: CountWfTask, ArrayList[1]: CountAutomationLetter, ArrayList[2]: CountUnRead, ArrayList[3]: CountPublicMsg
                if (arrCount.Count > 0)
                {
                    HyperLinkPublicMessages.InnerHtml = "پیام های عمومی رسیده : " + arrCount[3].ToString();
                    //if (Convert.ToInt32(arrCount[3]) > 0)
                    //{
                    //    Page.ClientScript.RegisterStartupScript(GetType(), "key3", "<script>Blink('ImgPublicMsg');</script>");
                    //}

                }
                else
                {
                    HyperLinkPublicMessages.InnerHtml = "پیام های عمومی رسیده )" + "0" + "(";
                }
                TSP.DataManager.Permission perMeRequest = TSP.DataManager.MemberRequestManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                divTempMemberReq.Visible = divMemberReq.Visible = HyperLinkMemberReq.Visible = perMeRequest.CanView;
                if (!perMeRequest.CanView)
                    PanelMemberHeader.InnerText = "";


                TSP.DataManager.Permission perMeDoc = TSP.DataManager.DocMemberFileManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                PanelDocument.Visible = divMeFileRevival.Visible = divMeFileReq.Visible = HyperLinkMeFileReq.Visible = HyperLinkMeFileRevival.Visible = perMeDoc.CanView;
                TSP.DataManager.Permission perOfficeMe = TSP.DataManager.OfficeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                divOfficeMe.Visible = HyperLinkOfficeMe.Visible = perOfficeMe.CanView;
                if (perOfficeMe.CanView)
                    PanelDocument.Visible = true;

                #endregion
                #region User Task
                if (PanelDocument.Visible)
                {
                    TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
                    DataTable dtMf = DocMemberFileManager.SelectDocMemberFileRequestFromMember();
                    dtMf.DefaultView.RowFilter = "RequesterType=" + "1";
                    HyperLinkMeFileReq.InnerHtml = "تکمیل پرونده پروانه اشتغال: " + dtMf.DefaultView.Count.ToString();
                    //if (dtMf.DefaultView.Count > 0)
                    //{
                    //    Page.ClientScript.RegisterStartupScript(GetType(), "key5", "<script>Blink('ImgMeFile');</script>");

                    //}
                    dtMf.DefaultView.RowFilter = "Type=" + ((int)TSP.DataManager.DocumentOfMemberRequestType.Revival).ToString();
                    //if (dtMf.DefaultView.Count > 0)
                    //{
                    //    Page.ClientScript.RegisterStartupScript(GetType(), "key6", "<script>Blink('ImgMeFileRevival');</script>");
                    //}
                    HyperLinkMeFileRevival.InnerHtml = "تمدید پروانه اشتغال: " + dtMf.DefaultView.Count.ToString();
                    TSP.DataManager.OfficeManager OfficeManager = new TSP.DataManager.OfficeManager();
                    DataTable dtOfficeMeReq = OfficeManager.spSelectOfficeRequestCount();
                    HyperLinkOfficeMe.InnerHtml = "تکمیل پرونده اعضای حقوقی: " + dtOfficeMeReq.Rows.Count.ToString();
                    //if (dtOfficeMeReq.Rows.Count > 0)
                    //{
                    //    Page.ClientScript.RegisterStartupScript(GetType(), "key9", "<script>Blink('ImgOfficeMe');</script>");
                    //}
                }
                if (PanelMember.Visible)
                {
                    TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager();
                    DataTable dtMe = MemberRequestManager.SelectMemberRequestFromMember(0);
                    HyperLinkMemberReq.InnerHtml = "تکمیل پرونده اعضای دائم: " + dtMe.Rows.Count.ToString();
                    //if (dtMe.Rows.Count > 0)
                    //{
                    //    Page.ClientScript.RegisterStartupScript(GetType(), "key7", "<script>Blink('ImgMemebrReq');</script>");
                    //}
                    MemberRequestManager.DataTable.Clear();
                    DataTable dtTempMe = MemberRequestManager.SelectMemberRequestFromMember(1);
                    HyperLinkTempMemberReq.InnerHtml = "تکمیل پرونده اعضای موقت: " + dtTempMe.Rows.Count.ToString();
                    //if (dtTempMe.Rows.Count > 0)
                    //{
                    //    Page.ClientScript.RegisterStartupScript(GetType(), "key8", "<script>Blink('ImgTempMemebrReq');</script>");
                    //}

                }
                linkHyperLinkMeFileReq.HRef = "~/Employee/Document/MemberFile.aspx?GrdFlt=qBdp9yy0ME7CDDGs/KZlgg==&SrchFlt=G93qPkoErUPTCBFyZvnP4EMhJB5Lz2qCxA41L3Sogmiroc9d9yQgjZnTy7iQk%20gzdwY9WvVlUHFJZQgnzGqf0Qjm4rU8cun3qBkFMufv5YT/9r9cxdxsJnV7XlreEhJkTV/3fiz9tWYAyjdIVDbGzotRX8itwDHWq0maIkdfibb/ZyjaFCggqydHGikMkaWomE%20YS/s0uyifR9BiRpAQcpqInJYx4o6gHpBCa0uuIGg0MtUL5cD%20wJhmpdQlqhGDedMppXGCP3VoNx76zQOdy2mcn3S7r/FNLJiGNyVEhpMqbCSpqlLLoxe8ocZAAl%203gWxUYSci1FrUOE9sNjK%20AN6abd5yVSxzFJuEKYvIoDnel/kSrcJ451zGFX%203Ts0lqZbCX2I/HEYBvTrCn5rQoCPHDs8n/fWPISrzU/r17CvQopSLUv3fqrdH91E1p4k6wXuMCQugurJACBaPh8wi5d7eHTkKP6b2YgSCW3moaPCIJVQVPfrtYc3tsRTX4XGT%20ks3ikhRB4A3qrwLU2zzz/zcf3bQk%20Uae56/oD4YoJCDK/erhhDH3%20GnPVhWJig47mkgJO%20UveK8h8UeQrVt/rqgbYwTsngFrINzUuH%20tTtHZQFv2gUr9z8grRx0jDI303XQO4biLxqratPmAsKpPnQgiM3%203FkxPPGzE1WegDWoR%20KV1LOrsBHCUziEPY5EFWtRDrv9P6ur5BQZNvIo6kym91uIMPs98i8hPWSPam3k8Utu46vQp2INc3e5zHqEM7hdxwXG5hq3ImcEG82xB7pwphILTZO0UiElxuOcglQ=";
                linkHyperLinkTempMemberReq.HRef = "~/Employee/MembersRegister/MembersTemp.aspx?GrdFlt=qBdp9yy0ME7CDDGs/KZlgg==&SrchFlt=G93qPkoErUPTCBFyZvnP4FC3aOJXjeK70zpqZK7V0QFXNWLjYZcpOyS6WBzEz6X1FJXMl4MQpuTvOrP1%20Hw9e%20XDnpFp2oyaCQ0adant%20gPi4wClYNv4lyNgG0%204mKcGJBAcNhPIc85YhGm/CFLnuFHbnob9m2HseXrIh/4yH0OyEO1Y0LXiwL1nzcapPOxB%20Jj3xN6ppc7EoDFjyWbfW6ikmZr0ZaWMSOta7yShC5OnCE/rXrlmBi/xQgJNeEOgNN26WeRdD9TmkmtfP%20dLPQHH2bUb64wV9uzbmfbQEqA=";
                linkHyperLinkMemberReq.HRef = "~/Employee/MembersRegister/Members.aspx?GrdFlt=qBdp9yy0ME7CDDGs/KZlgg==&SrchFlt=G93qPkoErUPTCBFyZvnP4FC3aOJXjeK70zpqZK7V0QFXNWLjYZcpOyS6WBzEz6X1FJXMl4MQpuTvOrP1 Hw9e XDnpFp2oyaCQ0adant gPi4wClYNv4lyNgG0 4mKcGJBAcNhPIc85YhGm/CFLnuBw4st4C05IAtCfd 7URmNdEAiFfuTPInRB0FHtqPfTCqIzV0x6y8dh69dUuRbbdjGR9VWUBD/2NZpTb53ria/iWnF6VIlFwG2ElaauyDkmWgHp1sDiLz6IaJXxgVTG5rBOyj6UH3bOUngVKyBYYJBHSgfEzG/0PIz9a2OvlKqLF";
                linkHyperLinkOfficeMe.HRef = "~/Employee/OfficeRegister/Office.aspx?GrdFlt=qBdp9yy0ME7CDDGs/KZlgg==&SrchFlt=VqsdSJwlDdngLn7frm8faSg82xES42fWSrnmHK0oQ48Itmu2au/XSirb0CgHDYAa4as/jfJ9vLovTBpo3KJyVH6uQ2fUeiy/MUJApYxKLaGTigG5dJHAgMJH2ZUmjIUjFTcAAwH%20s9vjHzyt7%20Lm3hntkVZQGjE/p/DXpafkRijce9sBi7iC14mB5kPFN5Z6YNxdT1csQq8RmELm7BIwbsk0y1h16JAPxY8NrHat96I=";
                linkHyperLinkMeFileRevival.HRef = "~/Employee/Document/MemberFile.aspx?GrdFlt=qBdp9yy0ME7CDDGs/KZlgg==&SrchFlt=G93qPkoErUPTCBFyZvnP4EMhJB5Lz2qCxA41L3Sogmiroc9d9yQgjZnTy7iQk gzdwY9WvVlUHFJZQgnzGqf0Qjm4rU8cun3qBkFMufv5YT/9r9cxdxsJnV7XlreEhJkTV/3fiz9tWYAyjdIVDbGzotRX8itwDHWq0maIkdfibb/ZyjaFCggqydHGikMkaWomE YS/s0uyifR9BiRpAQcpaVBbg4P4M4gEHw1FpaOdWINSarJSMA2/CzH6oRe0b2unY8mebPLmzYPuAT96aouSANw/mfZOTk/pVy irr2UQD5KbYMmR3AyVcqaAL/PMv0yctrmQUrNw7PX87uFPeQFXlFZXMcNfOWoOedRjNN3oXIL7dDauCqX7Nr2T97jqPExrAPof1RkWIQHYCsvUtb5P/v9sOLfWaShxZ8 7gtq2IuFtmp7h42E7dzpL95o2DqNge82d8/Tg6kyFR3ryw/jMq/P tw Y52h81aizqpNzrTT88ZMk0l4rKH/GJp21 z4MUFfzhYYSpDP/L7W1o/lWZqTsX3Q65CfatO6layiYqr3S766MLQg4OtqMfbS8KmMxlFgvRQrxwZYcXQ06G0xmabYiKgF5vQ3nLJtJCXtW6Wkki2tDjyrFjR3d4ObalEGa0d0FCImQOt1LnwU5c6AXrzposXLfkkoxhIfOuNaIV8uwi8SFfZYQ2/q21rQr1XGWgGgC1usuTRE88i/0P5TAwAuMJ4WKcpBRmO51mE8/lUeKw2u0Li8Rb0MZrPE9j";


                TSP.DataManager.Permission perLearning = TSP.DataManager.PeriodPresentManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                PanelLearning.Visible = perLearning.CanView;
                if (perLearning.CanView)
                {
                    TSP.DataManager.PeriodPresentRequestManager PeriodPresentRequestManager = new TSP.DataManager.PeriodPresentRequestManager();
                    PanelLearning.Visible = false;
                    divSavePeriodInfo.Visible = divLearningExpertConfirmingPeriod.Visible =
                    divLearningManagerConfirmingPeriod.Visible = divPeriodSaveExamMinute.Visible = divPeriodConfirmPointsByLearningExpert.Visible =
                   divPeriodConfirmPointsByLearningManager.Visible = divPeriodConfirmPointsByLearningAssistant.Visible = divPeriodConfirmingByRiasatSazemanAndSign.Visible = false;
                    DataTable dtSavePeriodInfo = PeriodPresentRequestManager.SelectPeriodPresentRequestCount(Utility.GetCurrentUser_NezamChartId(), (int)TSP.DataManager.WorkFlowTask.SavePeriodInfo);
                    if (dtSavePeriodInfo.Rows.Count > 0)
                    {
                        PanelLearning.Visible = true;
                        divSavePeriodInfo.Visible = true;//"PPId&-1&"
                        linkHyperLinkSavePeriodInfo.HRef = linkHyperLinkLearningExpertConfirmingPeriod.HRef = "~/Employee/Amoozesh/Periods.aspx?SrchFlt=" + Utility.EncryptQS("TaskId&" + dtSavePeriodInfo.Rows[0]["TaskId"].ToString()) + "&GrdFlt=" + Utility.EncryptQS("");
                        HyperLinkSavePeriodInfo.InnerHtml = dtSavePeriodInfo.Rows[0]["TaskName"].ToString() + ": " + dtSavePeriodInfo.Rows.Count.ToString();
                        ImgSavePeriodInfo.Src = dtSavePeriodInfo.Rows[0]["WFImageURL"].ToString();
                    }
                    DataTable dtLearningExpertConfirmingPeriod = PeriodPresentRequestManager.SelectPeriodPresentRequestCount(Utility.GetCurrentUser_NezamChartId(), (int)TSP.DataManager.WorkFlowTask.LearningExpertConfirmingPeriod);
                    if (dtLearningExpertConfirmingPeriod.Rows.Count > 0)
                    {
                        PanelLearning.Visible = true;
                        divLearningExpertConfirmingPeriod.Visible = true;
                        linkHyperLinkLearningExpertConfirmingPeriod.HRef = "~/Employee/Amoozesh/Periods.aspx?SrchFlt=" + Utility.EncryptQS("TaskId&" + dtLearningExpertConfirmingPeriod.Rows[0]["TaskId"].ToString()) + "&GrdFlt=" + Utility.EncryptQS("");
                        HyperLinkLearningExpertConfirmingPeriod.InnerHtml = dtLearningExpertConfirmingPeriod.Rows[0]["TaskName"].ToString() + ": " + dtLearningExpertConfirmingPeriod.Rows.Count.ToString();
                        ImgLearningExpertConfirmingPeriod.Src = dtLearningExpertConfirmingPeriod.Rows[0]["WFImageURL"].ToString();
                    }
                    DataTable dtLearningManagerConfirmingPeriod = PeriodPresentRequestManager.SelectPeriodPresentRequestCount(Utility.GetCurrentUser_NezamChartId(), (int)TSP.DataManager.WorkFlowTask.LearningManagerConfirmingPeriod);
                    if (dtLearningManagerConfirmingPeriod.Rows.Count > 0)
                    {
                        PanelLearning.Visible = true;
                        divLearningManagerConfirmingPeriod.Visible = true;
                        linkHyperLinkLearningManagerConfirmingPeriod.HRef = "~/Employee/Amoozesh/Periods.aspx?SrchFlt=" + Utility.EncryptQS("TaskId&" + dtLearningManagerConfirmingPeriod.Rows[0]["TaskId"].ToString()) + "&GrdFlt=" + Utility.EncryptQS("");
                        HyperLinkLearningManagerConfirmingPeriod.InnerHtml = dtLearningManagerConfirmingPeriod.Rows[0]["TaskName"].ToString() + ": " + dtLearningManagerConfirmingPeriod.Rows.Count.ToString();
                        ImgLearningManagerConfirmingPeriod.Src = dtLearningManagerConfirmingPeriod.Rows[0]["WFImageURL"].ToString();
                    }
                    DataTable dtPeriodSaveExamMinute = PeriodPresentRequestManager.SelectPeriodPresentRequestCount(Utility.GetCurrentUser_NezamChartId(), (int)TSP.DataManager.WorkFlowTask.PeriodSaveExamMinute);
                    if (dtPeriodSaveExamMinute.Rows.Count > 0)
                    {
                        PanelLearning.Visible = true;
                        divPeriodSaveExamMinute.Visible = true;
                        linkHyperLinkPeriodSaveExamMinute.HRef = "~/Employee/Amoozesh/Periods.aspx?SrchFlt=" + Utility.EncryptQS("TaskId&" + dtPeriodSaveExamMinute.Rows[0]["TaskId"].ToString()) + "&GrdFlt=" + Utility.EncryptQS("");
                        HyperLinkPeriodSaveExamMinute.InnerHtml = dtPeriodSaveExamMinute.Rows[0]["TaskName"].ToString() + ": " + dtPeriodSaveExamMinute.Rows.Count.ToString();
                        ImgPeriodSaveExamMinute.Src = dtPeriodSaveExamMinute.Rows[0]["WFImageURL"].ToString();
                    }
                    DataTable dtPeriodConfirmPointsByLearningExpert = PeriodPresentRequestManager.SelectPeriodPresentRequestCount(Utility.GetCurrentUser_NezamChartId(), (int)TSP.DataManager.WorkFlowTask.PeriodConfirmPointsByLearningExpert);
                    if (dtPeriodConfirmPointsByLearningExpert.Rows.Count > 0)
                    {
                        PanelLearning.Visible = true;
                        divPeriodConfirmPointsByLearningExpert.Visible = true;
                        linkHyperLinkPeriodConfirmPointsByLearningExpert.HRef = "~/Employee/Amoozesh/Periods.aspx?SrchFlt=" + Utility.EncryptQS("TaskId&" + dtPeriodConfirmPointsByLearningExpert.Rows[0]["TaskId"].ToString()) + "&GrdFlt=" + Utility.EncryptQS("");
                        HyperLinkPeriodConfirmPointsByLearningExpert.InnerHtml = dtPeriodConfirmPointsByLearningExpert.Rows[0]["TaskName"].ToString() + ": " + dtPeriodConfirmPointsByLearningExpert.Rows.Count.ToString();
                        ImgPeriodConfirmPointsByLearningExpert.Src = dtPeriodConfirmPointsByLearningExpert.Rows[0]["WFImageURL"].ToString();
                    }
                    DataTable dtPeriodConfirmPointsByLearningManager = PeriodPresentRequestManager.SelectPeriodPresentRequestCount(Utility.GetCurrentUser_NezamChartId(), (int)TSP.DataManager.WorkFlowTask.PeriodConfirmPointsByLearningManager);
                    if (dtPeriodConfirmPointsByLearningManager.Rows.Count > 0)
                    {
                        PanelLearning.Visible = true;
                        divPeriodConfirmPointsByLearningManager.Visible = true;
                        linkHyperLinkPeriodConfirmPointsByLearningManager.HRef = "~/Employee/Amoozesh/Periods.aspx?SrchFlt=" + Utility.EncryptQS("TaskId&" + dtPeriodConfirmPointsByLearningManager.Rows[0]["TaskId"].ToString()) + "&GrdFlt=" + Utility.EncryptQS("");
                        HyperLinkPeriodConfirmPointsByLearningManager.InnerHtml = dtPeriodConfirmPointsByLearningManager.Rows[0]["TaskName"].ToString() + ": " + dtPeriodConfirmPointsByLearningManager.Rows.Count.ToString();
                        ImgPeriodConfirmPointsByLearningManager.Src = dtPeriodConfirmPointsByLearningManager.Rows[0]["WFImageURL"].ToString();
                    }
                    DataTable dtPeriodConfirmPointsByLearningAssistant = PeriodPresentRequestManager.SelectPeriodPresentRequestCount(Utility.GetCurrentUser_NezamChartId(), (int)TSP.DataManager.WorkFlowTask.PeriodConfirmPointsByLearningAssistant);
                    if (dtPeriodConfirmPointsByLearningAssistant.Rows.Count > 0)
                    {
                        PanelLearning.Visible = true;
                        divPeriodConfirmPointsByLearningAssistant.Visible = true;
                        linkHyperLinkPeriodConfirmPointsByLearningAssistant.HRef = "~/Employee/Amoozesh/Periods.aspx?SrchFlt=" + Utility.EncryptQS("TaskId&" + dtPeriodConfirmPointsByLearningAssistant.Rows[0]["TaskId"].ToString()) + "&GrdFlt=" + Utility.EncryptQS("");
                        HyperLinkPeriodConfirmPointsByLearningAssistant.InnerHtml = dtPeriodConfirmPointsByLearningAssistant.Rows[0]["TaskName"].ToString() + ": " + dtPeriodConfirmPointsByLearningAssistant.Rows.Count.ToString();
                        ImgPeriodConfirmPointsByLearningAssistant.Src = dtPeriodConfirmPointsByLearningAssistant.Rows[0]["WFImageURL"].ToString();
                    }
                    DataTable dtPeriodConfirmingByRiasatSazemanAndSign = PeriodPresentRequestManager.SelectPeriodPresentRequestCount(Utility.GetCurrentUser_NezamChartId(), (int)TSP.DataManager.WorkFlowTask.PeriodConfirmingByRiasatSazemanAndSign);
                    if (dtPeriodConfirmingByRiasatSazemanAndSign.Rows.Count > 0)
                    {
                        PanelLearning.Visible = true;
                        divPeriodConfirmingByRiasatSazemanAndSign.Visible = true;
                        linkHyperLinkPeriodConfirmingByRiasatSazemanAndSign.HRef = "~/Employee/Amoozesh/Periods.aspx?SrchFlt=" + Utility.EncryptQS("TaskId&" + dtPeriodConfirmingByRiasatSazemanAndSign.Rows[0]["TaskId"].ToString()) + "&GrdFlt=" + Utility.EncryptQS("");
                        HyperLinkPeriodConfirmingByRiasatSazemanAndSign.InnerHtml = dtPeriodConfirmingByRiasatSazemanAndSign.Rows[0]["TaskName"].ToString() + ": " + dtPeriodConfirmingByRiasatSazemanAndSign.Rows.Count.ToString();
                        ImgPeriodConfirmingByRiasatSazemanAndSign.Src = dtPeriodConfirmingByRiasatSazemanAndSign.Rows[0]["WFImageURL"].ToString();
                    }
                }
                #endregion
                #region TS
                TSP.DataManager.Permission perChoosePlanChecker = TSP.DataManager.TechnicalServices.PlansManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                divConfirmedPlan.Visible = HyperLinkConfirmedPlan.Visible = divChooseControler.Visible = HyperLinkChooseControler.Visible = perChoosePlanChecker.CanView;
                if (perChoosePlanChecker.CanView)
                {
                    divConfirmedPlan.Visible = divChooseControler.Visible = PanelTS.Visible = true;
                    TSP.DataManager.TechnicalServices.PlansManager PlansManager = new TSP.DataManager.TechnicalServices.PlansManager();
                    int AgentId = -1;
                    if (Utility.GetCurrentUser_AgentId() != Utility.GetCurrentAgentCode())
                    {
                        AgentId = Utility.GetCurrentUser_AgentId();
                    }
                    DataTable dtChoosePlanChecker = PlansManager.CountTSPlansByTaskCode(AgentId, TSP.DataManager.WorkFlowTask.AssignControlerToPlan,"","");
                    HyperLinkChooseControlerTitle.InnerHtml = "نقشه های در انتظار اختصاص بازبین: " + (dtChoosePlanChecker.Rows.Count == 0 ? "0" : dtChoosePlanChecker.Rows[0]["cntPlans"].ToString());
                    HyperLinkChooseControler.HRef = "~/Employee/TechnicalServices/Project/PlansChooseControler.aspx";
                    Utility.Date objDate = new Utility.Date(Utility.GetDateOfToday());
                    string LastMonth = objDate.AddDays(-30);
                    DataTable dtConfirmedPlan = PlansManager.CountTSPlansByTaskCode(AgentId, TSP.DataManager.WorkFlowTask.ConfirmingPlanAndEndProccess, LastMonth, Utility.GetDateOfToday());
                    HyperLinkConfirmedPlanTitle.InnerHtml = "نقشه های تایید شده توسط بازبین در ماه جاری: " + (dtConfirmedPlan.Rows.Count == 0 ? "0" : dtConfirmedPlan.Rows[0]["cntPlans"].ToString());
                    HyperLinkConfirmedPlan.HRef = "~/Employee/TechnicalServices/Report/ReportPlans.aspx?PgMd=" + Utility.EncryptQS("Confirmed");
                }
                else
                {
                    divConfirmedPlan.Visible = divChooseControler.Visible = PanelTS.Visible = false;
                }
                #endregion

            }
            catch (Exception err)
            {
                Utility.SaveWebsiteError(err);
            }
        }
    }

}
