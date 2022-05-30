<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="MemberInsert.aspx.cs" Inherits="Employee_MembersRegister_MemberInsert"
    Title="مشخصات عضو" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">

          function SetKardani(Visible) {
            panelKardani.SetVisible(Visible);
        }
        function ShowMessage(Message) {
            document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
            document.getElementById("<%=DivReport.ClientID%>").style.display = 'inline';
            document.getElementById('<%=LabelWarning.ClientID%>').innerHTML = Message;
        }
        function ClearAttachments() {
            imgEndUploadImgClient3.SetVisible(false);
            txtDescImg.SetText("");
        }

        function setWindowOnload() {
            if (ChEnteghali.GetChecked() == true) {
                PanelEnteghali.SetVisible(true);
            }
            else {
                PanelEnteghali.SetVisible(false);
            }
            if (chbtc.GetChecked() == true) {
                PanelEntegaliDoc.SetVisible(true);
            }
            else {
                PanelEntegaliDoc.SetVisible(false);
            }
            if (CmbStatus.GetValue() == '5')//**Transfer to other province
            {

                lblOtherPr.SetVisible(true);
                ComboPrId.SetVisible(true);
                lblLastmeno.SetVisible(false);
                lblfilenoInLastProvince.SetVisible(false);
                txtMeNoLastProvince.SetVisible(false);
                rfileno.SetVisible(false);

            }
            else if (CmbStatus.GetValue() == '6')//**Return to Fars province
            {

                lblOtherPr.SetVisible(true);
                ComboPrId.SetVisible(true);
                lblLastmeno.SetVisible(true);
                lblfilenoInLastProvince.SetVisible(true);
                txtMeNoLastProvince.SetVisible(true);
                rfileno.SetVisible(true);
            }
            else {

                lblOtherPr.SetVisible(false);
                ComboPrId.SetVisible(false);
                lblLastmeno.SetVisible(false);
                lblfilenoInLastProvince.SetVisible(false);
                txtMeNoLastProvince.SetVisible(false);
                rfileno.SetVisible(false);
            }

            if (chbSex.GetSelectedIndex() == 1) {
                SetSexWomen();
            }
            else {
                SetSexMen();
            }

            if (drdSoId.GetSelectedIndex() == 3 || drdSoId.GetSelectedIndex() == 4 || drdSoId.GetSelectedIndex() == 5) {
                chbSoLdire.SetVisible(true);
            }
            else {
                chbSoLdire.SetVisible(false);
                chbSoLdire.SetChecked(false);
            }
        }

        window.onload = setWindowOnload;

        function SetControlEnabled(Enabled) {
            txtFirstName.SetEnabled(Enabled);
            txtLastName.SetEnabled(Enabled);
            txtFirstNameEn.SetEnabled(Enabled);
            txtLastNameEn.SetEnabled(Enabled);
            txtHomeAdr.SetEnabled(Enabled);
            txtWorkAdr.SetEnabled(Enabled);
            txtHometel.SetEnabled(Enabled);
            txtWorkTel.SetEnabled(Enabled);
            txtFaxNo.SetEnabled(Enabled);
            txtHometel_cityCode.SetEnabled(Enabled);
            txtWorkTel_cityCode.SetEnabled(Enabled);
            txtFaxNo_cityCode.SetEnabled(Enabled);
            txtHomePO.SetEnabled(Enabled);
            txtWorkPO.SetEnabled(Enabled);
            txtMobileNo.SetEnabled(Enabled);
            txtWebsite.SetEnabled(Enabled);
            txtEmail.SetEnabled(Enabled);
            // txtReqDesc.SetEnabled(Enabled);
            drdMarId.SetEnabled(Enabled);
            drdSoId.SetEnabled(Enabled);
            drdAgent.SetEnabled(Enabled);
            drdCitId.SetEnabled(Enabled);

            flpImage.SetVisible(Enabled);
            flpSign.SetVisible(Enabled);
            flpResident.SetVisible(Enabled);
            flpIdNo.SetVisible(Enabled);
            flpIdNoP2.SetVisible(Enabled);
            flpIdNoPDes.SetVisible(Enabled);
            flpSSN.SetVisible(Enabled);
            flpSSNBack.SetVisible(Enabled);
            flpSoldier.SetVisible(Enabled);
            flpSoldierBack.SetVisible(Enabled);
            flpResident.SetVisible(Enabled);
            flpIdNo.SetVisible(Enabled);
            flpIdNoP2.SetVisible(Enabled);
            flpIdNoPDes.SetVisible(Enabled);
            flpSSN.SetVisible(Enabled);
            flpSSNBack.SetVisible(Enabled);
            flpSoldier.SetVisible(Enabled);
            flpSoldierBack.SetVisible(Enabled);

            drdSexId.SetEnabled(Enabled);
            txtIdNo.SetEnabled(Enabled);
            txtSSN.SetEnabled(Enabled);
            txtFatherName.SetEnabled(Enabled);
            txtIssuePlace.SetEnabled(Enabled);
            txtBirhtPlace.SetEnabled(Enabled);
            txtBankAccNo.SetEnabled(Enabled);

            //*****Enteghali Controls
            ChEnteghali.SetEnabled(Enabled);
            PanelEnteghali.SetEnabled(Enabled);
            FlpTLetter.SetVisible(Enabled);
            document.getElementById('<%=txtTransferDate.ClientID%>').disabled = !Enabled;
            document.getElementById('<%=txtTransferDate.ClientID%>').setAttribute("usedatepicker", Enabled);

            document.getElementById('<%=txtFirstDocRegDate.ClientID%>').disabled = !Enabled;
            document.getElementById('<%=txtFirstDocRegDate.ClientID%>').setAttribute("usedatepicker", Enabled);

            document.getElementById('<%=txtCurrentDocRegDate.ClientID%>').disabled = !Enabled;
            document.getElementById('<%=txtCurrentDocRegDate.ClientID%>').setAttribute("usedatepicker", Enabled);

            document.getElementById('<%=txtCurrentDocExpDate.ClientID%>').disabled = !Enabled;
            document.getElementById('<%=txtCurrentDocExpDate.ClientID%>').setAttribute("usedatepicker", Enabled);
            //*****
            document.getElementById('<%=chbComId.ClientID%>').disabled = !Enabled;
            document.getElementById('<%=txtBirthDate.ClientID%>').disabled = !Enabled;
            document.getElementById('<%=txtBirthDate.ClientID%>').setAttribute("usedatepicker", Enabled);
            document.getElementById('<%=chbComId.ClientID%>').disabled = !Enabled;

            if (Enabled == false) {
                imgEndUploadImgClient2.SetVisible(Enabled);
                imgEndUploadImgClientSign.SetVisible(Enabled);
                imgEndUploadImgClientIdNo.SetVisible(Enabled);
                imgEndUploadImgClientSSN.SetVisible(Enabled);
                imgEndUploadImgClientSol.SetVisible(Enabled);
                imgEndUploadImgClientResident.SetVisible(Enabled);

                //***************Image Validation Labels
                lblPersonalImagevalidation.SetVisible(false);
                lblvalidationEmail.SetVisible(false);
                lblValidationIdNo.SetVisible(false);
                lblValidationSSNImage.SetVisible(false);
                lblValidationRes.SetVisible(false);
                //*************************************
            }
        }

        function btnAdd_Click() {
            var txtDate = document.getElementById('<%= txtaDate.ClientID %>');

            if (txtaNumber.GetIsValid() && txtaAmount.GetIsValid()) {
                grid.PerformCallback('Add');
            }
        }

        function SetFiche() {
            lblBank.SetVisible(false);
            lblBranchCode.SetVisible(false);
            lblBranchName.SetVisible(false);
            Bank.SetVisible(false);
            BranchCode.SetVisible(false);
            BranchName.SetVisible(false);
        }
        function SetCheque() {
            lblBank.SetVisible(true);
            lblBranchCode.SetVisible(true);
            lblBranchName.SetVisible(true);
            Bank.SetVisible(true);
            BranchCode.SetVisible(true);
            BranchName.SetVisible(true);
        }

        function ClearAccounting() {
            cmbAccType.SetSelectedIndex(0);
            var txtDate = document.getElementById('<%= txtaDate.ClientID %>');
            txtDate.value = "";
            txtaNumber.SetText("");
            txtaAmount.SetText(HiddenFieldInfo.Get('FishAmount'));
            txtaDesc.SetText("");
        }


        function SetSexWomen() {
            lblSolFile.SetVisible(false);
            flpSoldier.SetVisible(false);

            lblSolFileBack.SetVisible(false);
            flpSoldierBack.SetVisible(false);

            chbSoLdire.SetVisible(false);
            chbSoLdire.SetChecked(false);

            lblSol.SetVisible(false);
            drdSoId.SetVisible(false);
            HpSoldier.SetVisible(false);
            HpSoldierBack.SetVisible(false);
        }
        function SetSexMen() {
            lblSolFile.SetVisible(true);
            flpSoldier.SetVisible(true);

            lblSolFileBack.SetVisible(true);
            flpSoldierBack.SetVisible(true);

            lblSol.SetVisible(true);
            drdSoId.SetVisible(true);
            HpSoldier.SetVisible(true);
            HpSoldierBack.SetVisible(true);
        }

        function btnSaveCheck(e) {
            if (!CheckSaveMeRequest()) {
                e.processOnServer = false;
            }
        }
        function CheckSaveMeRequest() {
            var IsCheck = true;
            return IsCheck;
        }
    </script>
    <TSPControls:CustomAspxCallbackPanel ID="CallBackMembers" ClientInstanceName="CallBackMembers"
        HideContentOnCallback="false" Width="100%" runat="server" OnCallback="CallBackMembers_Callback">
        <ClientSideEvents EndCallback="function(s, e) {
                     if(s.cpPrint==1)
                     {
                        window.open(s.cpURL);
                        s.cpPrint=0;
                     }
                     }" />
        <PanelCollection>
            <dxp:PanelContent>
                <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
                    CalendarDayWidth="31" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
                    FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
                    WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
                </pdc:PersianDateScriptManager>
                <div align="right" id="DivReport" class="DivErrors" runat="server" visible="true">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]
                </div>
                <TSPControls:CustomASPxRoundPanelMenu ID="ASPxRoundPanel1" runat="server" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            CausesValidation="False" ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="BtnNew_Click">

                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" AutoPostBack="true" Text=" " ToolTip="ویرایش"
                                            UseSubmitBehavior="False" OnClick="btnEdit_Click">
                                            <ClientSideEvents Click="function(e,s){}" />

                                            <Image Height="25px" Url="../../Images/icons/edit.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave" runat="server" EnableTheming="False"
                                            EnableViewState="False" OnClick="btnSave_Click" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False">

                                            <Image Height="25px" Url="~/Images/icons/save.png" Width="25px" />
                                            <ClientSideEvents Click="function(s, e) {
                                                        
                                                       btnSaveCheck(e);
                                                        
                                                        }" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                    </td>


                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ"
                                            CausesValidation="False" ID="btnPrint" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" AutoPostBack="false">
                                            <ClientSideEvents Click="function(s,e){
                                                                    CallBackMembers.PerformCallback('Print');
                                                                    }" />

                                            <Image Url="~/Images/icons/printers.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                            ToolTip="بازگشت" UseSubmitBehavior="False">

                                            <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <TSPControls:CustomAspxMenuHorizontal ID="MenuTop" runat="server"
                    OnItemClick="MenuTop_ItemClick" ClientInstanceName="menu">
                    <Items>
                        <dxm:MenuItem Name="Request" Text="مشخصات عضو" Selected="true">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="Madrak" Text="مدارک تحصیلی">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="Job" Text="سوابق کاری">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="Language" Text="زبان ها">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="Activity" Text="فعالیت ها">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="مستندات" Name="Attach">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="گروه ها" Name="Group">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="مالی" Name="AccFish">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="گزارش تنظیمات" Name="PollAnswer">
                        </dxm:MenuItem>
                    </Items>
                </TSPControls:CustomAspxMenuHorizontal>


                <br />

                <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanelMeInsert" HeaderText="مشخصات عضو" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>

                            <table runat="server" id="tblMeInsert" dir="rtl" width="100%">
                                <tr>
                                    <td align="center" valign="top" colspan="4" dir="ltr">
                                        <dxe:ASPxLabel runat="server" Text="وضعیت گردش کار درخواست:" Font-Bold="False" ID="lblWorkFlowState"
                                            ForeColor="Red">
                                        </dxe:ASPxLabel>
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top" style="width: 15%">
                                        <dxe:ASPxLabel ID="ASPxLabel6" runat="server" Text="شماره عضویت" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td dir="ltr" align="right" valign="top" style="width: 35%">
                                        <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtMeNo"
                                            ReadOnly="True" Enabled="false">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField ErrorText="" />
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td align="right" valign="top" style="width: 15%">
                                        <asp:Label runat="server" Text="وضعیت عضویت" ID="lblMeStatus" Width="100%"></asp:Label></td>
                                    <td align="right" valign="top" style="width: 35%">
                                        <TSPControls:CustomTextBox runat="server" ID="txtMeStatus" Width="100%"
                                            ClientEnabled="False" ReadOnly="True">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RegularExpression ErrorText=""></RegularExpression>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="نام *" ID="Label14" Width="100%"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" ID="txtFirstName" Width="100%"
                                            ClientInstanceName="txtFirstName">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField IsRequired="True" ErrorText="نام را وارد نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="نام خانوادگی *" ID="Label26" Width="100%"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" ID="txtLastName" Width="100%"
                                            ClientInstanceName="txtLastName">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField IsRequired="True" ErrorText="نام خانوادگی را وارد نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel ID="ASPxLabel26" runat="server" Text="نام(انگلیسی) *" Width="100%"
                                            Wrap="False" RightToLeft="True">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" ID="txtFirstNameEn" Width="100%"
                                            ClientInstanceName="txtFirstNameEn">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField IsRequired="True" ErrorText="نام (انگلیسی) را وارد نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="نام خانوادگی(انگلیسی) *" ID="Label27" Width="100%"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" ID="txtLastNameEn" Width="100%"
                                            ClientInstanceName="txtLastNameEn">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField IsRequired="True" ErrorText="نام خانوادگی (انگلیسی) را وارد نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="شماره شناسنامه*" ID="Label44" Width="100%"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" ID="txtIdNo" Width="100%" MaxLength="10"
                                            ClientInstanceName="txtIdNo">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField IsRequired="True" ErrorText="شماره شناسنامه را وارد نمایید"></RequiredField>
                                                <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{1,10}"></RegularExpression>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="کد ملی*" ID="Label45" Width="100%"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" ID="txtSSN" Width="100%" MaxLength="10"
                                            ClientInstanceName="txtSSN">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField IsRequired="True" ErrorText="کد ملی را وارد نمایید"></RequiredField>
                                                <RegularExpression ErrorText="این کد صحیح نیست" ValidationExpression="\d{10}"></RegularExpression>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="نام پدر*" ID="Label46" Width="100%"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" ID="txtFatherName" Width="100%"
                                            ClientInstanceName="txtFatherName">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField IsRequired="True" ErrorText="نام پدر را وارد نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="محل صدور*" ID="Label47" Width="100%"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" ID="txtIssuePlace" Width="100%"
                                            ClientInstanceName="txtIssuePlace">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField IsRequired="True" ErrorText="محل صدور را وارد نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="تاریخ تولد*" ID="Label48"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="225px" ShowPickerOnTop="True"
                                            ID="txtBirthDate" PickerDirection="ToRight" ShowPickerOnEvent="OnClick" IconUrl="~/Image/Calendar.gif"
                                            Style="direction: ltr"></pdc:PersianDateTextBox>
                                        <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                            ErrorMessage="تاریخ نامعتبر" ControlToValidate="txtBirthDate" ID="PersianDateValidator1">تاریخ نامعتبر</pdc:PersianDateValidator>
                                    </td>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="محل تولد" ID="Label51"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" ID="txtBirhtPlace" Width="100%"
                                            ClientInstanceName="txtBirhtPlace">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField IsRequired="False" ErrorText="محل تولد را وارد نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="نشانی محل سکونت*" Width="100%" ID="Label28"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" colspan="3">
                                        <TSPControls:CustomASPXMemo runat="server" Height="50px" ID="txtHomeAdr" Width="100%"
                                            ClientInstanceName="txtHomeAdr">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField IsRequired="True" ErrorText="نشانی را وارد نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="شماره تلفن محل سکونت*" Width="100%" ID="Label29"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <table width="100%">
                                            <tbody>
                                                <tr>
                                                    <td style="vertical-align: top; text-align: right" style="width: 85%">
                                                        <TSPControls:CustomTextBox runat="server" ID="txtHometel" Width="100%" MaxLength="8"
                                                            ClientInstanceName="txtHometel">
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                                <RequiredField IsRequired="True" ErrorText="شماره تلفن را وارد نمایید"></RequiredField>
                                                                <RegularExpression ErrorText="شماره تلفن را صحیح وارد نمایید" ValidationExpression="\d{5,8}"></RegularExpression>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td style="vertical-align: top; text-align: right" style="width: 2%">
                                                        <asp:Label runat="server" Text="-" ID="Label65" Width="100%"></asp:Label>
                                                    </td>
                                                    <td style="vertical-align: top; text-align: right" style="width: 13%">
                                                        <TSPControls:CustomTextBox runat="server" ID="txtHometel_cityCode" Width="100%"
                                                            MaxLength="4" ClientInstanceName="txtHometel_cityCode">
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                                <RegularExpression ErrorText="پیش شماره تلفن را صحیح وارد نمایید" ValidationExpression="(0)\d{2,3}"></RegularExpression>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="شماره همراه*" ID="Label30" Width="100%"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td26" valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" ID="txtMobileNo" Width="100%"
                                            MaxLength="11" ClientInstanceName="txtMobileNo">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField IsRequired="True" ErrorText="شماره همراه را وارد نمایید"></RequiredField>
                                                <RegularExpression ErrorText="شماره همراه را صحیح وارد نمایید" ValidationExpression="0\d{1,10}"></RegularExpression>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td runat="server" id="Td27" valign="top" align="right">
                                        <asp:Label runat="server" Text="نشانی محل کار" ID="Label31" Width="100%"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td28" valign="top" align="right" colspan="3">
                                        <TSPControls:CustomASPXMemo runat="server" Height="30px" ID="txtWorkAdr" Width="100%"
                                            ClientInstanceName="txtWorkAdr">
                                            <ValidationSettings>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td runat="server" id="Td29" valign="top" align="right">
                                        <asp:Label runat="server" Text="تلفن محل کار" ID="Label32" Width="100%"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <table width="100%">
                                            <tbody>
                                                <tr>
                                                    <td align="right" style="width: 85%">
                                                        <TSPControls:CustomTextBox runat="server" ID="txtWorkTel" Width="100%" MaxLength="8"
                                                            ClientInstanceName="txtWorkTel">
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                                <RequiredField ErrorText="شماره تلفن را وارد نمایید"></RequiredField>
                                                                <RegularExpression ErrorText="شماره تلفن را صحیح وارد نمایید" ValidationExpression="\d{5,8}"></RegularExpression>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td style="vertical-align: top; text-align: right" style="width: 2%">
                                                        <asp:Label runat="server" Text="-" ID="Label33" Width="100%"></asp:Label>
                                                    </td>
                                                    <td style="vertical-align: top; text-align: right" style="width: 13%">
                                                        <TSPControls:CustomTextBox runat="server" ID="txtWorkTel_cityCode" Width="100%"
                                                            MaxLength="4" ClientInstanceName="txtWorkTel_cityCode">
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                                <RegularExpression ErrorText="پیش شماره تلفن را صحیح وارد نمایید" ValidationExpression="(0)\d{2,3}"></RegularExpression>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="شماره فکس" ID="Label34" Width="100%"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <table width="100%">
                                            <tbody>
                                                <tr>
                                                    <td style="vertical-align: top; text-align: right" style="width: 85%">
                                                        <TSPControls:CustomTextBox runat="server" ID="txtFaxNo" Width="100%" MaxLength="8"
                                                            ClientInstanceName="txtFaxNo">
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                                <RequiredField ErrorText=""></RequiredField>
                                                                <RegularExpression ErrorText="شماره فکس را صحیح وارد نمایید" ValidationExpression="\d{5,8}"></RegularExpression>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td style="vertical-align: top; text-align: right" style="width: 2%">
                                                        <asp:Label runat="server" Text="-" ID="Label35" Width="100%"></asp:Label>
                                                    </td>
                                                    <td style="vertical-align: top; text-align: right" style="width: 13%">
                                                        <TSPControls:CustomTextBox runat="server" ID="txtFaxNo_cityCode" Width="100%"
                                                            MaxLength="4" ClientInstanceName="txtFaxNo_cityCode">
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                                <RegularExpression ErrorText="پیش شماره فکس را صحیح وارد نمایید" ValidationExpression="(0)\d{2,3}"></RegularExpression>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr11">
                                    <td runat="server" id="Td33" valign="top" align="right">
                                        <asp:Label runat="server" Text="کد پستی محل سکونت" Width="100%" ID="Label36"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td34" valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" ID="txtHomePO" Width="100%" MaxLength="10"
                                            ClientInstanceName="txtHomePO">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RegularExpression ErrorText="کد پستی را صحیح وارد نمایید" ValidationExpression="^\d{10}$"></RegularExpression>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td runat="server" id="Td35" valign="top" align="right">
                                        <asp:Label runat="server" Text="کد پستی محل کار" ID="Label37" Width="100%"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td36" valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" ID="txtWorkPO" Width="100%" MaxLength="10"
                                            ClientInstanceName="txtWorkPO">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RegularExpression ErrorText="کد پستی را صحیح وارد نمایید" ValidationExpression="^\d{10}$"></RegularExpression>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr id="Tr12" runat="server">
                                    <td id="Td37" runat="server" align="right" valign="top">
                                        <asp:Label ID="Label59" runat="server" Text="جنسیت *"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxComboBox runat="server" EnableClientSideAPI="True" Width="100%" TextField="SexName" ID="drdSexId"
                                            EnableIncrementalFiltering="True" DataSourceID="ODBSex" ValueType="System.String"
                                            RightToLeft="True" ValueField="SexId" ClientInstanceName="drdSexId">
                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
		if (drdSexId.GetSelectedIndex()== 0)
        {
           SetSexWomen();
        }
        else
        {                                            
		   SetSexMen();
        }
      
}"></ClientSideEvents>
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField IsRequired="True" ErrorText="جنسیت را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="ملیت" ID="lblNationality" Width="100%"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtNationality"
                                            Text="ایرانی">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField IsRequired="True" ErrorText="ملیت را وارد نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr id="Tr13" runat="server">

                                    <td align="right" valign="top">
                                        <asp:Label ID="Label38" runat="server" Text="وضعیت تأهل*" Width="100%"></asp:Label>
                                    </td>
                                    <td align="right" dir="ltr" valign="top">
                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%" TextField="MarName" ID="drdMarId" EnableIncrementalFiltering="true"
                                            DataSourceID="ODBMar" ValueType="System.String" ValueField="MarId" ClientInstanceName="drdMarId"
                                            RightToLeft="True">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" ErrorText="وضعیت تاهل را انتخاب نمایید" />
                                            </ValidationSettings>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                    <td id="Td43" runat="server" align="right" valign="top" style="width: 15%">
                                        <dxe:ASPxLabel runat="server" Text="وضعیت سربازی*" ID="lblSol" ClientInstanceName="lblSol"
                                            Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%" TextField="SoName" ID="drdSoId" EnableIncrementalFiltering="true"
                                            DataSourceID="ODBSo" ValueType="System.String" ValueField="SoId" ClientInstanceName="drdSoId"
                                            RightToLeft="True">
                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
   if (drdSoldier.GetSelectedIndex() == 3 || drdSoldier.GetSelectedIndex() == 4 || drdSoldier.GetSelectedIndex() == 5)
                {
                   chbSoLdire.SetChecked(false);
                   chbSoLdire.SetVisible(true);
                }
                
            else
            { 
              chbSoLdire.SetVisible(false);
              chbSoLdire.SetChecked(false);
            }
      
}"></ClientSideEvents>
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" ErrorText="وضعیت سربازی را انتخاب نمایید" />
                                            </ValidationSettings>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <TSPControls:CustomASPxCheckBox runat="server" Text="اينجانب متعهد مي گردم درصورت قبولي در آزمون ورود به حرفه مهندسي و با علم به اينكه، براساس ابلاغ، اصلاحيه آيين نامه اجرايي قانون نظام مهندسي و كنترل ساختمان در تاريخ 94/12/20، ارائه كارت پايان خدمت يا كارت معافيت از خدمت جهت صدور/تمديد پروانه اشتغال الزامي مي باشد تصوير كارت مذكور را در سايت نظام مهندسي آپلود كرده و تصوير برابر اصل آن را تحويل واحد عضويت و پروانه اشتغال (حقيقي وحقوقي) بدهم در غيراينصورت امكان صدور پروانه اشتغال وجود نداشته و حق هرگونه اعتراضي را ازخود سلب مي نمايم" EnableClientSideAPI="True"
                                            ID="chbSoLdire" ClientInstanceName="chbSoLdire" ClientVisible="false">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom"
                                                ErrorDisplayMode="ImageWithTooltip">

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" ErrorText="تعهد مربوط به وضعیت سربازی مورد موافقت قرار نگرفته است" />
                                            </ValidationSettings>
                                        </TSPControls:CustomASPxCheckBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td runat="server" id="Td47" valign="top" align="right">
                                        <asp:Label runat="server" Text="محل اقامت*" ID="Label39" Width="100%"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%" TextField="CitName" ID="drdCitId" DataSourceID="OdbCity"
                                            RightToLeft="True" ValueType="System.String" ValueField="CitId" ClientInstanceName="drdCitId"
                                            EnableIncrementalFiltering="True">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField IsRequired="True" ErrorText="محل اقامت را وارد نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="نمایندگی *" ID="ASPxLabel12" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                            TextField="Name" ID="drdAgent" DataSourceID="OdbAgent"
                                            RightToLeft="True" ValueType="System.String" ValueField="AgentId" ClientInstanceName="drdAgent"
                                            EnableIncrementalFiltering="True">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField IsRequired="True" ErrorText="نمایندگی را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="شماره حساب " ID="Label49" Width="100%"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" Width="100%" MaxLength="16" ID="txtBankAccNo"
                                            ClientInstanceName="txtBankAccNo">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{1,16}"></RegularExpression>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="کد نظارت شهرداری" ID="ASPxLabel29" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" Width="100%" MaxLength="20" ID="txtArchitectorCode"
                                            ClientInstanceName="txtArchitectorCode">
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr10150">
                                    <td runat="server" id="Td3433" valign="top" align="right">
                                        <asp:Label runat="server" Text="آدرس وب سایت" ID="Label40" Width="100%"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td35454" valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" ID="txtWebsite" Width="100%" ClientInstanceName="txtWebsite">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RegularExpression ErrorText="آدرس وب سایت را با فرمت http://www.Example.com وارد نمایید"
                                                    ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"></RegularExpression>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr runat="server" id="Tr16y671">
                                    <td runat="server" id="Td3565655" valign="top" align="right">
                                        <asp:Label runat="server" Text="پست الکترونیکی" ID="lblEmail" Width="100%"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td332426" valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" ID="txtEmail" Width="100%" ClientInstanceName="txtEmail">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">


                                                <RegularExpression ErrorText="پست الکترونیکی را با فرمت صحیح وارد نمایید" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></RegularExpression>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr runat="server" id="Tr123422">
                                    <td runat="server" id="Td356567" valign="top" align="right">
                                        <asp:Label runat="server" Text="تصویر *" ID="Label50" Width="100%"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td344548" valign="top" align="right" colspan="3">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                            ID="flpImage" InputType="Images" ClientInstanceName="flpImage" OnFileUploadComplete="flpImage_FileUploadComplete">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid){
	imgEndUploadImgClient2.SetVisible(true);
	 flpme.Set('name',1);
	lblPersonalImagevalidation.SetVisible(false);
	meImg.SetVisible(true);
		meImg.SetImageUrl('../../Image/Members/Person/Request/'+e.callbackData);
        HiddenFieldInfo.Set('MeImgUpload','~/Image/Members/Person/Request/'+e.callbackData);
	}
	else{
	imgEndUploadImgClient2.SetVisible(false);
	lblPersonalImagevalidation.SetVisible(true);
	meImg.SetVisible(false);
	meImg.SetImageUrl('');
	}
}"></ClientSideEvents>
                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel ID="ASPxLabel19" runat="server" ClientInstanceName="lblPersonalImagevalidation" ClientVisible="False"
                                                            ForeColor="Red" Text="تصویر را انتخاب نمایید">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                            ID="ASPxImage3" ClientInstanceName="imgEndUploadImgClient2">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxe:ASPxImage runat="server" Height="75px" Width="75px" ID="imgMember" ClientInstanceName="meImg"
                                            Border-BorderWidth="1px" Border-BorderStyle="Solid">
                                            <EmptyImage Height="75px" Width="75px" Url="~/Images/Person.png">
                                            </EmptyImage>
                                        </dxe:ASPxImage>
                                        <br />
                                        <dxe:ASPxLabel ID="ASPxLabelImgWarning" runat="server" ForeColor="#0000C0" Text="">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td id="Td41" runat="server" valign="top" align="right" style="width: 15%">
                                        <asp:Label runat="server" Text="تصویر امضا" ID="Label42"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" colspan="3">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                            ID="flpSign" InputType="Images" ClientInstanceName="flpSign" OnFileUploadComplete="flpSign_FileUploadComplete">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid){
	
	imgEndUploadImgClientSign.SetVisible(true);
	 flpmesign.Set('name',1);
	lblvalidationEmail.SetVisible(false);
	signImg.SetVisible(true);
	signImg.SetImageUrl('../../Image/Members/Sign/Request/'+e.callbackData);
    HiddenFieldInfo.Set('MeImgSign','~/image/Members/Sign/Request/'+e.callbackData);
	}
	else{
	imgEndUploadImgClientSign.SetVisible(false);
	lblvalidationEmail.SetVisible(true);
	signImg.SetVisible(false);
	signImg.SetImageUrl('');
	}
}"></ClientSideEvents>
                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel ID="ASPxLabel20" runat="server" ClientInstanceName="lblvalidationEmail" ClientVisible="False"
                                                            ForeColor="Red" Text="تصویر امضا را انتخاب نمایید">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                            ID="ASPxImage4" ClientInstanceName="imgEndUploadImgClientSign">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxe:ASPxImage runat="server" Height="75px" Width="75px" ID="ImgSign" ClientInstanceName="signImg"
                                            Border-BorderWidth="1px" Border-BorderStyle="Solid">
                                            <EmptyImage Url="~/Images/noimage.gif">
                                            </EmptyImage>
                                        </dxe:ASPxImage>
                                    </td>
                                </tr>
                                <tr>
                                    <td id="Td48" runat="server" align="right" valign="top">
                                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="تصویر صفحه اول شناسنامه *">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" colspan="3" valign="top">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl ID="flpIdNo" runat="server" ClientInstanceName="flpIdNo"
                                                            UploadWhenFileChoosed="true" OnFileUploadComplete="flpIdNo_FileUploadComplete">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid){
	imgEndUploadImgClientIdNo.SetVisible(true);
  	 flpmeidno.Set('name',1);
	lblValidationIdNo.SetVisible(false);
	hidno.SetVisible(true);
	hidno.SetNavigateUrl('../../image/Members/IdNo/Request/'+e.callbackData);
    HiddenFieldInfo.Set('MeImageIdNo','~/image/Members/IdNo/Request/'+e.callbackData);
	}
	else{
	imgEndUploadImgClientIdNo.SetVisible(false);
	lblValidationIdNo.SetVisible(true);
	hidno.SetVisible(false);
	hidno.SetNavigateUrl('');
	}
}" />
                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel ID="ASPxLabel21" runat="server" ClientInstanceName="lblValidationIdNo" ClientVisible="False"
                                                            ForeColor="Red" Text="تصویر صفحه اول شناسنامه را انتخاب نمایید">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage ID="imgEndUploadImgIdNo" runat="server" ClientInstanceName="imgEndUploadImgClientIdNo"
                                                            ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxe:ASPxHyperLink ID="HpIdNo" runat="server" ClientInstanceName="hidno" Target="_blank"
                                            Text="تصویر صفحه اول شناسنامه">
                                        </dxe:ASPxHyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel ID="ASPxLabel30" runat="server" Text="تصویر صفحه دوم شناسنامه*">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" colspan="3" valign="top">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl ID="flpIdNoP2" runat="server" ClientInstanceName="flpIdNoP2"
                                                            InputType="Images" UploadWhenFileChoosed="true" OnFileUploadComplete="flpIdNo_FileUploadComplete">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                              
                                                                            if(e.isValid){  
                                                                
       
	imgEndUploadImgClientIdNoP2.SetVisible(true);
    flpmeidno.Set('IdNoP2',1);
	lblIdNoP2.SetVisible(false);
	HIdNoP2.SetVisible(true);
	HIdNoP2.SetNavigateUrl('../../image/Members/IdNo/Request/'+e.callbackData);
    HiddenFieldInfo.Set('MeImageIdNoP2','~/image/Members/IdNo/Request/'+e.callbackData);
	}
	else{
	imgEndUploadImgClientIdNoP2.SetVisible(false);
    flpmeidno.Set('IdNoP2',0);
	lblIdNoP2.SetVisible(true);
	HIdNoP2.SetVisible(false);
	HIdNoP2.SetNavigateUrl('');
	}
}" />
                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel ID="lblIdNoP2" runat="server" ClientInstanceName="lblIdNoP2" ClientVisible="False"
                                                            ForeColor="Red" Text="تصویر صفحه دوم شناسنامه را انتخاب نمایید">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage ID="imgEndUploadImgClientIdNoP2" runat="server" ClientInstanceName="imgEndUploadImgClientIdNoP2"
                                                            ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxe:ASPxHyperLink ID="HIdNoP2" runat="server" ClientInstanceName="HIdNoP2"
                                            Target="_blank" Text="تصویر صفحه دوم شناسنامه">
                                        </dxe:ASPxHyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel ID="ASPxLabel35" runat="server" Text="تصویر صفحه توضیحات شناسنامه*">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" colspan="3" valign="top">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl ID="flpIdNoPDes" runat="server" ClientInstanceName="flpIdNoPDes"
                                                            InputType="Images" UploadWhenFileChoosed="true" OnFileUploadComplete="flpIdNo_FileUploadComplete">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                            if(e.isValid){
	imgEndUploadImgClientIdNoPDes.SetVisible(true);
    flpmeidno.Set('IdNoPDes',1);
	lblIdNoPDes.SetVisible(false);
	HIdNoPDes.SetVisible(true);
	HIdNoPDes.SetNavigateUrl('../../image/Members/IdNo/Request/'+e.callbackData);
    HiddenFieldInfo.Set('MeImageIdNoDes','~/image/Members/IdNo/Request/'+e.callbackData);
	}
	else{
	imgEndUploadImgClientIdNoPDes.SetVisible(false);
    flpmeidno.Set('IdNoPDes',0);
	lblIdNoPDes.SetVisible(true);
	HIdNoPDes.SetVisible(false);
	HIdNoPDes.SetNavigateUrl('');
	}
}" />
                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel ID="lblIdNoPDes" runat="server" ClientInstanceName="lblIdNoPDes" ClientVisible="False"
                                                            ForeColor="Red" Text="تصویر صفحه توضیحات شناسنامه را انتخاب نمایید">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage ID="imgEndUploadImgClientIdNoPDes" runat="server" ClientInstanceName="imgEndUploadImgClientIdNoPDes"
                                                            ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxe:ASPxHyperLink ID="HIdNoPDes" runat="server" ClientInstanceName="HIdNoPDes"
                                            Target="_blank" Text="تصویر صفحه توضیحات شناسنامه">
                                        </dxe:ASPxHyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel ID="ASPxLabel13" runat="server" Text="تصویر روی کارت ملی*">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" colspan="3" valign="top">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td align="right" dir="rtl">
                                                        <TSPControls:CustomAspxUploadControl ID="flpSSN" runat="server" ClientInstanceName="flpSSN"
                                                            OnFileUploadComplete="flpSSN_FileUploadComplete" UploadWhenFileChoosed="true">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                     if(e.isValid){
	imgEndUploadImgClientSSN.SetVisible(true);
    flpmessn.Set('name',1);
	lblValidationSSNImage.SetVisible(false);
	hssn.SetVisible(true);
	hssn.SetNavigateUrl('../../image/Members/SSN/Request/'+e.callbackData);
    HiddenFieldInfo.Set('FileOfSSN','~/image/Members/SSN/Request/'+e.callbackData);
	}
	else
	{
	imgEndUploadImgClientSSN.SetVisible(false);
	lblValidationSSNImage.SetVisible(true);
	hssn.SetVisible(false);
	hssn.SetNavigateUrl('');
	}
}" />
                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel ID="ASPxLabel22" runat="server" ClientInstanceName="lblValidationSSNImage" ClientVisible="False"
                                                            ForeColor="Red" Text="تصویر روی کارت ملی را انتخاب نمایید">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage ID="imgEndUploadImgSSN" runat="server" ClientInstanceName="imgEndUploadImgClientSSN"
                                                            ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویرانتخاب شد">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxe:ASPxHyperLink ID="HpSSN" runat="server" ClientInstanceName="hssn" Target="_blank"
                                            Text="تصویر روی کارت ملی">
                                        </dxe:ASPxHyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel ID="ASPxLabel36" runat="server" Text="تصویر پشت کارت ملی*">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" colspan="3" valign="top">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl ID="flpSSNBack" runat="server" ClientInstanceName="flpSSNBack"
                                                            InputType="Images" UploadWhenFileChoosed="true" OnFileUploadComplete="flpSSN_FileUploadComplete">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                            if(e.isValid){
	imgEndUploadImgClientSSNBack.SetVisible(true);
    flpmessn.Set('SSNBack',1);
	lblssBack.SetVisible(false);
	hssnBack.SetVisible(true);
	hssnBack.SetNavigateUrl('../../image/Members/SSN/Request/'+e.callbackData);
    HiddenFieldInfo.Set('FileOfSSNBack','~/image/Members/SSN/Request/'+e.callbackData);
	}
	else{
	imgEndUploadImgClientSSNBack.SetVisible(false);
    flpmessn.Set('SSNBack',0);
	lblssBack.SetVisible(true);
	hssnBack.SetVisible(false);
	hssnBack.SetNavigateUrl('');
	}
}" />
                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel ID="ASPxLabel38" runat="server" ClientInstanceName="lblssBack" ClientVisible="False"
                                                            ForeColor="Red" Text="تصویر پشت کارت ملی را انتخاب نمایید">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage ID="ASPxImage5" runat="server" ClientInstanceName="imgEndUploadImgClientSSNBack"
                                                            ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxe:ASPxHyperLink ID="HpSSNBack" runat="server" ClientInstanceName="hssnBack"
                                            Target="_blank" Text="تصویر پشت کارت ملی">
                                        </dxe:ASPxHyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel ID="ASPxLabel27" runat="server" Text="تصویر مدرک سکونت در استان*">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" colspan="3" valign="top">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                            ID="flpResident" InputType="Images" ClientInstanceName="flpResident" OnFileUploadComplete="flpResident_FileUploadComplete">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                            if(e.isValid){
	imgEndUploadImgClientResident.SetVisible(true);
   
	HDFlpResident.Set('name',1);
	hRes.SetVisible(true);
	hRes.SetNavigateUrl('../../image/Members/Resident/Request/'+e.callbackData);
    HiddenFieldInfo.Set('FileOfResident','~/image/Members/Resident/Request/'+e.callbackData);
	}
	else{
	imgEndUploadImgClientResident.SetVisible(false);
   
	HDFlpResident.Set('name',0);
	hRes.SetVisible(false);
	hRes.SetNavigateUrl('');
	}
}" />
                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel ID="ASPxLabel28" runat="server" ClientInstanceName="lblValidationRes" ClientVisible="False"
                                                            ForeColor="Red" Text="تصویر مدرک سکونت در استان را انتخاب نمایید">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                            ID="ASPxImage1" ClientInstanceName="imgEndUploadImgClientResident">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxe:ASPxHyperLink ID="HpResident" runat="server" ClientInstanceName="hRes"
                                            Target="_blank" Text="تصویر مدرک سکونت در استان">
                                        </dxe:ASPxHyperLink>
                                    </td>
                                </tr>

                                <tr>
                                    <td colspan="4" align="right" valign="top">
                                        <TSPControls:CustomASPxCheckBox runat="server" Text="دارای مدرک تحصیلی کارشناسی ناپیوسته یا کاردانی می باشد."
                                            EnableClientSideAPI="True" ID="ChkBKardani" ClientInstanceName="ChkBKardani">
                                            <ClientSideEvents CheckedChanged="function(s, e) {
	if (ChkBKardani.GetChecked()== true)
        {
		SetKardani(true);
        }
        else
        {
		SetKardani(false);
       }
}"></ClientSideEvents>
                                        </TSPControls:CustomASPxCheckBox>
                                    </td>
                                </tr>                             
                                  <tr>
                                    <td colspan="4" align="right" valign="top">
                                        <dxp:ASPxPanel ID="panelKardani" runat="server" ClientVisible="false" ClientInstanceName="panelKardani">
                                            <PanelCollection>
                                                <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                    <table width="100%">
                                                        <tbody>
                                                            <tr>
                                                                <td colspan="2" width="100%" align="right" valign="top">
                                                                    <b style="color: blue">چنانچه دارای مدرک کارشناسی ناپیوسته یا مدرک کاردانی می باشید باید تصویر استعلام عدم عضویت در نظام کاردانی ساختمان استان فارس بارگذاری شود .</b>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" width="15%" valign="top">تصویر استعلام عدم عضویت در نظام کاردانی*
                                                                </td>
                                                                <td align="right" width="85%" valign="top">
                                                                    <table>
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                                                        ID="flpKardani" InputType="Images" ClientInstanceName="flpKardani" OnFileUploadComplete="flpKardani_FileUploadComplete">
                                                                                        <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                            if(e.isValid){
	imgEndUploadImgClientKardani.SetVisible(true);
	HDFlpResident.Set('Kardani',1);
	lblKardani.SetVisible(false);
	HpKardani.SetVisible(true);
	HpKardani.SetNavigateUrl('../../image/Members/NezamKardani/Request/'+e.callbackData);                                                                                            
    HiddenFieldInfo.Set('MeImageKardan','~/image/Members/NezamKardani/Request/'+e.callbackData);
	}
	else{
	imgEndUploadImgClientKardani.SetVisible(false);
	HDFlpResident.Set('Kardani',0);
	lblKardani.SetVisible(true);
	HpKardani.SetVisible(false);
	HpKardani.SetNavigateUrl('');
	}
}" />
                                                                                    </TSPControls:CustomAspxUploadControl>
                                                                                    <dxe:ASPxLabel ID="lblKardani" runat="server" ClientInstanceName="lblKardani" ClientVisible="False"
                                                                                        ForeColor="Red" Text="تصویر استعلام عدم عضویت در نظام کاردانها را انتخاب نمایید">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td>
                                                                                    <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                                                        ID="imgEndUploadImgClientKardani" ClientInstanceName="imgEndUploadImgClientKardani">
                                                                                    </dxe:ASPxImage>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                    <dxe:ASPxHyperLink ID="HpKardani" runat="server" ClientInstanceName="HpKardani"
                                                                        ClientVisible="False" Target="_blank" Text="تصویر استعلام عدم عضویت در نظام کاردانها">
                                                                    </dxe:ASPxHyperLink>
                                                                </td>
                                                            </tr>


                                                        </tbody>
                                                    </table>
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                        </dxp:ASPxPanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel ID="lblSolFile" ClientVisible="false" runat="server" Text="تصویر روی کارت پایان خدمت"
                                            ClientInstanceName="lblSolFile">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" colspan="3" valign="top" dir="rtl">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl runat="server" ID="flpSoldier" UploadWhenFileChoosed="true"
                                                            ClientInstanceName="flpSoldier" ClientVisible="false" OnFileUploadComplete="flpSoldier_FileUploadComplete">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid){
	imgEndUploadImgClientSol.SetVisible(true);
    flpmesol.Set('name',1);
    //lblSolFile.SetVisible(false);
	lblsol.SetVisible(false);
	HpSoldier.SetVisible(true);
	HpSoldier.SetNavigateUrl('../../image/Members/Soldier/Request/'+e.callbackData);
    HiddenFieldInfo.Set('FileOfSol','~/image/Members/Soldier/Request/'+e.callbackData);
	}
	else{
	imgEndUploadImgClientSol.SetVisible(false);
	lblsol.SetVisible(true);
	HpSoldier.SetVisible(false);
	HpSoldier.SetNavigateUrl('');
	}
}" />
                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel runat="server" Text="تصویر کارت پایان خدمت را انتخاب نمایید" ClientVisible="False"
                                                            ID="ASPxLabel24" ForeColor="Red" ClientInstanceName="lblsol">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                            ID="imgEndUploadImgSol" ClientInstanceName="imgEndUploadImgClientSol">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxe:ASPxHyperLink ID="HpSoldier" runat="server" ClientInstanceName="HpSoldier"
                                            Target="_blank" Text="تصویر کارت پایان خدمت" ClientVisible="false">
                                        </dxe:ASPxHyperLink>
                                        <TSPControls:CustomAspxButton runat="server" Text="click" CausesValidation="False" ClientVisible="False"
                                            Width="62px" ID="ASPxButton3" EnableClientSideAPI="True" AutoPostBack="False"
                                            UseSubmitBehavior="False" ClientInstanceName="but">
                                            <ClientSideEvents Click="function(s, e) {
	//flpsol.SetVisible(false);
}"></ClientSideEvents>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>

                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel ID="lblSolFileBack" runat="server" Text="تصویر پشت کارت پایان خدمت" ClientInstanceName="lblSolFileBack">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" colspan="3" valign="top">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                            ID="flpSoldierBack" InputType="Images" ClientInstanceName="flpSoldierBack" OnFileUploadComplete="flpSoldier_FileUploadComplete">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                            if(e.isValid){
	imgEndUploadImgSolBack.SetVisible(true);
   
	flpmesol.Set('SolBack',1);
	lblSolBack.SetVisible(false);
	HpSoldierBack.SetVisible(true);
	HpSoldierBack.SetNavigateUrl('../../image/Members/Soldier/Request/'+e.callbackData);
    HiddenFieldInfo.Set('FileOfSolBack','~/image/Members/Soldier/Request/'+e.callbackData);
	}
	else{
	imgEndUploadImgSolBack.SetVisible(false);
   
	flpmesol.Set('SolBack',0);
	lblSolBack.SetVisible(true);
	HpSoldierBack.SetVisible(false);
	HpSoldierBack.SetNavigateUrl('');
	}
}" />
                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel ID="lblSolBack" runat="server" ClientInstanceName="lblSolBack" ClientVisible="False"
                                                            ForeColor="Red" Text="تصویر پشت کارت پایان خدمت را انتخاب نمایید">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                            ID="imgEndUploadImgSolBack" ClientInstanceName="imgEndUploadImgSolBack">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxe:ASPxHyperLink ID="HpSoldierBack" runat="server" ClientInstanceName="HpSoldierBack"
                                            Target="_blank" Text="تصویر پشت کارت پایان خدمت" ClientVisible="false">
                                        </dxe:ASPxHyperLink>

                                    </td>
                                </tr>

                                <tr>
                                    <td align="right" colspan="4" valign="top">

                                        <fieldset>
                                            <legend class="HelpUL" dir="rtl">
                                                <TSPControls:CustomASPxCheckBox ID="ChEnteghali" runat="server" ClientInstanceName="ChEnteghali" EnableClientSideAPI="True"
                                                    Text="از استان فارس به استان دیگر/از استان دیگری به استان فارس منتقل شده است" Width="100%">
                                                    <ClientSideEvents CheckedChanged="function(s, e) {                                              
	if (ChEnteghali.GetChecked()== true)
        {                                                
		    PanelEnteghali.SetVisible(true);                                               
        }
        else
        {
		   
            PanelEnteghali.SetVisible(false);            
       }
}" />
                                                </TSPControls:CustomASPxCheckBox>
                                            </legend><%--ClientVisible="false"--%>
                                            <dxp:ASPxPanel runat="server" ID="PanelEnteghali" ClientInstanceName="PanelEnteghali" Width="100%">
                                                <PanelCollection>
                                                    <dxp:PanelContent>

                                                        <table width="100%">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="width: 15%" align="right" valign="top">نوع انتقالی
                                                                    </td>
                                                                    <td style="width: 35%" align="right" valign="top">
                                                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                                            RightToLeft="True" ID="CmbTransferStatus" ValueType="System.String"
                                                                            ClientInstanceName="CmbTransferStatus"
                                                                            SelectedIndex="-1" TextField="" ValueField="" EnableIncrementalFiltering="true">
                                                                            <Items>
                                                                                <%--         <dxe:ListEditItem Text="------------------------------------------" />--%>
                                                                                <dxe:ListEditItem Text="انتقال از دیگر استان به استان فارس" Value="1" />
                                                                                <dxe:ListEditItem Text="انتقال از فارس به استان دیگر" Value="2" />
                                                                                <dxe:ListEditItem Text="بازگشت به استان فارس" Value="3" />
                                                                            </Items>
                                                                            <ItemStyle HorizontalAlign="Right" />

                                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                                <RequiredField ErrorText="نوع انتقالی را انتخاب نمایید" IsRequired="True" />
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                                </ErrorFrameStyle>
                                                                            </ValidationSettings>
                                                                            <ButtonStyle Width="13px">
                                                                            </ButtonStyle>

                                                                        </TSPControls:CustomAspxComboBox>


                                                                    </td>
                                                                    <td style="width: 15%" align="right" valign="top"></td>
                                                                    <td style="width: 35%" align="right" valign="top"></td>

                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 15%" align="right" valign="top">
                                                                        <dxe:ASPxLabel ID="ASPxLabel8" runat="server" ClientInstanceName="lblPr" Text="دیگراستان"
                                                                            Width="100%">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td style="width: 35%" align="right" valign="top">
                                                                        <TSPControls:CustomAspxComboBox ID="ComboOtherProvince" runat="server" ClientInstanceName="ComboOtherProvince"
                                                                            DataSourceID="OdbProvince"
                                                                            TextField="PrName" ValueField="PrId" ValueType="System.String" Width="100%" RightToLeft="True">
                                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                                <RequiredField ErrorText="دیگر استان را انتخاب نمایید" IsRequired="True" />
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                                </ErrorFrameStyle>
                                                                            </ValidationSettings>
                                                                            <ButtonStyle Width="13px">
                                                                            </ButtonStyle>
                                                                        </TSPControls:CustomAspxComboBox>
                                                                    </td>
                                                                    <td style="vertical-align: top; width: 15%">
                                                                        <dxe:ASPxLabel ID="ASPxLabel14" runat="server" ClientInstanceName="lblDate" Text="تاریخ انتقالی"
                                                                            Width="100%">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td style="vertical-align: top; width: 35%">
                                                                        <pdc:PersianDateTextBox ID="txtTransferDate" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                                                            PickerDirection="ToRight" RightToLeft="False" ShowPickerOnTop="True" ShowPickerOnEvent="OnClick"
                                                                            Style="direction: ltr; text-align: right;" Width="225px"></pdc:PersianDateTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" valign="top">
                                                                        <dxe:ASPxLabel ID="ASPxLabel15" runat="server" ClientInstanceName="lblMeNo" Text="کد عضویت دیگر استان"
                                                                            Width="100%">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td style="vertical-align: top" align="right" valign="top">
                                                                        <TSPControls:CustomTextBox ID="txtTransferMeNo" runat="server" ClientInstanceName="MeNo"
                                                                            Width="100%">
                                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                                <RequiredField ErrorText="کد عضویت را وارد نمایید" IsRequired="True" />
                                                                                <RegularExpression ErrorText="این کد صحیح نیست" ValidationExpression="\d*"></RegularExpression>
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                                </ErrorFrameStyle>
                                                                            </ValidationSettings>
                                                                        </TSPControls:CustomTextBox>
                                                                    </td>
                                                                    <td align="center" style="vertical-align: top"></td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" valign="top">
                                                                        <dxe:ASPxLabel ID="ASPxLabel17" runat="server" ClientInstanceName="lblImg" Text="تصویر نامه انتقالی"
                                                                            Width="100%">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td colspan="3" align="right" valign="top">
                                                                        <table>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td>
                                                                                        <TSPControls:CustomAspxUploadControl ID="FlpTLetter" runat="server" ClientInstanceName="FlpTLetter"
                                                                                            UploadWhenFileChoosed="true" OnFileUploadComplete="FlpTLetter_FileUploadComplete"
                                                                                            ShowProgressPanel="True">
                                                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                                    if(e.isValid){
	imgEndUploadImgClientLetter.SetVisible(true);
	flpletter.Set('name',1);
	lblImageEnteghaliValidation.SetVisible(false);
	ImgTransferToFars.SetVisible(true);
	ImgTransferToFars.SetImageUrl('../../image/Members/Transport/'+e.callbackData);                                                                                                
    HiddenFieldInfo.Set('MeImageEnteghali','~/image/Members/Transport/'+e.callbackData);
	}
	else
	{
	imgEndUploadImgClientLetter.SetVisible(false);
	lblImageEnteghaliValidation.SetVisible(true);
	ImgTransferToFars.SetVisible(false);
	ImgTransferToFars.SetImageUrl('');
	}
}" />
                                                                                        </TSPControls:CustomAspxUploadControl>
                                                                                        <dxe:ASPxLabel ID="ASPxLabel18" runat="server" ClientInstanceName="lblImageEnteghaliValidation" ClientVisible="False"
                                                                                            ForeColor="Red" Text="تصویر نامه را انتخاب نمایید">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxe:ASPxImage ID="imgEndUploadImgLetter" runat="server" ClientInstanceName="imgEndUploadImgClientLetter"
                                                                                            ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد">
                                                                                        </dxe:ASPxImage>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                        <dxe:ASPxImage ID="ImgTransferToFars" runat="server" ClientInstanceName="ImgTransferToFars" Height="75px"
                                                                            Width="75px">
                                                                            <EmptyImage Height="75px" Url="~/Images/noimage.gif" Width="75px" />
                                                                        </dxe:ASPxImage>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" valign="top">
                                                                        <dxe:ASPxLabel runat="server" Text="دلایل انتقال" ID="lblReasoneToTranseferOtherProv" ClientInstanceName="lblReasoneToTranseferOtherProv">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td colspan="3" align="right" valign="top">
                                                                        <TSPControls:CustomASPXMemo runat="server" Height="70px" Width="100%" ID="txtTransferBodyResone"
                                                                            ClientInstanceName="txtTransferBodyResone">
                                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                                <RequiredField ErrorText="دلایل انتقال را وارد نمایید" IsRequired="false" />
                                                                            </ValidationSettings>
                                                                        </TSPControls:CustomASPXMemo>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" colspan="4" style="vertical-align: top; text-align: right" valign="top">
                                                                        <dxe:ASPxLabel runat="server" CssClass="HelpUL" Font-Bold="true" Text="" RightToLeft="True" ID="lblAlarmHasMeDoc" ClientInstanceName="lblAlarmHasMeDoc" Visible="false">
                                                                        </dxe:ASPxLabel>
                                                                        <TSPControls:CustomASPxCheckBox ID="ChbTCheckFileNo" runat="server" ClientInstanceName="chbtc"
                                                                            Text="داری پروانه اشتغال در سایر استان ها  با کد آن استانها می باشد (به غیر از کد 17 )" Width="100%">
                                                                            <ClientSideEvents CheckedChanged="function(s, e) {
	if(chbtc.GetChecked()==true)
	{                                           
       PanelEntegaliDoc.SetVisible(true);
	}
	else
	{
        PanelEntegaliDoc.SetVisible(false);
	}
}" />
                                                                        </TSPControls:CustomASPxCheckBox>
                                                                        <br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4" align="center">
                                                                        <dxp:ASPxPanel ID="PanelEntegaliDoc" ClientVisible="false" ClientInstanceName="PanelEntegaliDoc" runat="server"
                                                                            Width="100%">
                                                                            <PanelCollection>
                                                                                <dxp:PanelContent>
                                                                                    <table width="100%">
                                                                                        <tr>
                                                                                            <td colspan="4" align="right">
                                                                                                <%--  <dxe:ASPxLabel runat="server" Text="شماره پروانه و تاریخ ها مربوط به آخرین پروانه فرد در استان قبلی می باشد"
                                                                                                    Font-Bold="true" ForeColor="DarkRed" ID="lblAlertPreDocDate">
                                                                                                </dxe:ASPxLabel>--%>
                                                                                                <ul style="color: darkviolet; font: bold;" runat="server" id="lblAlertPreDocDate">
                                                                                                    <li>شماره پروانه و تاریخ ها مربوط به آخرین پروانه فرد در استان قبلی می باشد</li>
                                                                                                    <li>در صورت اشتباه وارد نمودن اطلاعات شماره پروانه استان قبل صدور پروانه شخص با مشکل مواجه خواهد شد و مسئولیت آن به عهده شما می باشد.</li>
                                                                                                    <li>از وارد نمودن شماره پروانه با کد استان فارس در این قسمت جددا خودداری نمایید</li>
                                                                                                </ul>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 15%" align="right" valign="top">
                                                                                                <dxe:ASPxLabel ID="lblDocPr" runat="server" ClientInstanceName="lblDocPr" Text="استان صدور پروانه"
                                                                                                    Width="100%">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td style="width: 35%" align="right" valign="top">
                                                                                                <TSPControls:CustomAspxComboBox ID="ComboDocPreProvince" runat="server" ClientInstanceName="ComboDocPreProvince"
                                                                                                    DataSourceID="OdbProvince"
                                                                                                    TextField="PrName" ValueField="PrId" ValueType="System.String"
                                                                                                    Width="100%" RightToLeft="True">
                                                                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                                                        <RequiredField ErrorText="استان صدور پروانه را انتخاب نمایید" IsRequired="True" />
                                                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                                                                        </ErrorFrameStyle>
                                                                                                    </ValidationSettings>
                                                                                                    <ButtonStyle Width="13px">
                                                                                                    </ButtonStyle>
                                                                                                    <ClientSideEvents Validation="function(s, e) {
	if(s.GetEnabled()==true)
	{
		if(s.GetText()==null || s.GetText()=='')
			e.isValid=false;
	}
	else 
		e.isValid=true;
}" />
                                                                                                </TSPControls:CustomAspxComboBox>
                                                                                            </td>
                                                                                            <td style="width: 15%"></td>
                                                                                            <td style="width: 35%"></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="right" valign="top">
                                                                                                <dxe:ASPxLabel ID="ASPxLabel16" runat="server" ClientInstanceName="lblFileNoPreProvince" Text="شماره پروانه اشتغال دیگر استان "
                                                                                                    Width="100%">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td align="right" style="vertical-align: top" valign="top">
                                                                                                <TSPControls:CustomTextBox ID="txtPreProvinceFileNo" runat="server" ClientInstanceName="FileNo"
                                                                                                    Style="direction: ltr" Width="100%">
                                                                                                    <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorText="شماره پروانه را وارد نمایید"
                                                                                                        ErrorTextPosition="Bottom">
                                                                                                        <RequiredField ErrorText="شماره پروانه را وارد نمایید" />
                                                                                                        <%--      <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="^\d{2}-\d{3}-\d{3,5}" />--%>

                                                                                                        <RegularExpression ErrorText="یا ساختار کد صحیح نیست یا ابتدای کد نباید 17 باشد" ValidationExpression="^(?!17)\d\d\-\d\d\d\-\d+$" />
                                                                                                        <ErrorFrameStyle ImageSpacing="4px" Wrap="True">
                                                                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                                                                        </ErrorFrameStyle>
                                                                                                    </ValidationSettings>
                                                                                                    <ClientSideEvents Validation="function(s, e) {
	if(s.GetEnabled()==true)
	{
		if(s.GetText()==null || s.GetText()=='')
			e.isValid=false;
	}
	else 
		e.isValid=true;
}" />
                                                                                                </TSPControls:CustomTextBox>
                                                                                            </td>
                                                                                            <td valign="top">
                                                                                                <dxe:ASPxLabel runat="server" Text="تاریخ اولین صدور" ID="ASPxLabel11" Width="100%"
                                                                                                    ClientInstanceName="lblFirstDocRegDate">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td>
                                                                                                <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="225px" ShowPickerOnTop="True"
                                                                                                    Style="direction: ltr; text-align: right;" ShowPickerOnEvent="OnClick" ID="txtFirstDocRegDate"
                                                                                                    PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                                                                                <pdc:PersianDateValidator runat="server" ValidateEmptyText="false" ClientValidationFunction="PersianDateValidator"
                                                                                                    ErrorMessage="تاریخ نامعتبر" ControlToValidate="txtFirstDocRegDate" ID="PersianDateValidator4">تاریخ نامعتبر</pdc:PersianDateValidator>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td valign="top">
                                                                                                <dxe:ASPxLabel runat="server" Text="تاریخ آخرین صدور" ID="ASPxLabel34" Width="100%"
                                                                                                    ClientInstanceName="lblCurrentDocRegDate">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td>
                                                                                                <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="225px" ShowPickerOnTop="True"
                                                                                                    ID="txtCurrentDocRegDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"
                                                                                                    ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                                                                                <pdc:PersianDateValidator runat="server" ValidateEmptyText="false" ClientValidationFunction="PersianDateValidator"
                                                                                                    ErrorMessage="تاریخ نامعتبر" ControlToValidate="txtCurrentDocRegDate" ID="PersianDateValidator5">تاریخ نامعتبر</pdc:PersianDateValidator>
                                                                                            </td>
                                                                                            <td valign="top">
                                                                                                <dxe:ASPxLabel runat="server" Text="تاریخ اعتبار" ID="ASPxLabel37" ClientInstanceName="lblCurrentDocExpDate">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td>
                                                                                                <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="225px" ShowPickerOnTop="True"
                                                                                                    ID="txtCurrentDocExpDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"
                                                                                                    ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                                                                                <pdc:PersianDateValidator runat="server" ValidateEmptyText="false" ClientValidationFunction="PersianDateValidator"
                                                                                                    ErrorMessage="تاریخ نامعتبر" ControlToValidate="txtCurrentDocExpDate" ID="PersianDateValidator6">تاریخ نامعتبر</pdc:PersianDateValidator>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </dxp:PanelContent>
                                                                            </PanelCollection>
                                                                        </dxp:ASPxPanel>
                                                                    </td>
                                                                </tr>

                                                            </tbody>
                                                        </table>

                                                    </dxp:PanelContent>
                                                </PanelCollection>
                                            </dxp:ASPxPanel>
                                        </fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td runat="server" valign="top" align="right" style="width: 15%">شرح درخواست
                                    </td>
                                    <td runat="server" id="Td40" valign="top" align="right" colspan="3">
                                        <TSPControls:CustomASPXMemo runat="server" Height="50px" ID="txtReqDesc"
                                            ClientInstanceName="txtReqDesc">
                                            <ValidationSettings>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">توضیحات
                                    </td>
                                    <td colspan="3" align="right" valign="top">
                                        <TSPControls:CustomASPXMemo runat="server" Height="50px" Width="100%" ID="txtStDesc"
                                            ClientInstanceName="TextDesc">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RequiredField ErrorText="توضیحات را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                            </table>
                            <fieldset id="ASPxRoundPanelCommissions" runat="server">
                                <legend class="HelpUL">کمیسیون های همکاری</legend>
                                <asp:CheckBoxList ID="chbComId" runat="server" DataSourceID="ODBCom" DataTextField="ComName"
                                    DataValueField="ComId" RepeatColumns="3" Width="100%">
                                </asp:CheckBoxList>
                            </fieldset>
                            <fieldset id="ASPxRoundPanelAccounting" runat="server">
                                <legend class="HelpUL">ثبت فیش</legend>
                                <asp:Panel ID="PanelAccountingInserting" runat="server">
                                    <div align="center">
                                        <table id="tableAccounting" dir="rtl" runat="server" width="100%">
                                            <tbody>
                                                <tr>
                                                    <td colspan="4" valign="top" align="center">
                                                        <dxe:ASPxLabel runat="server" ID="lblRegEnter" ClientInstanceName="lblRegEnter" ForeColor="Blue">
                                                        </dxe:ASPxLabel>
                                                        <dxe:ASPxLabel runat="server" ID="lblReg" ClientInstanceName="lblReg" ClientVisible="false"
                                                            ForeColor="Blue">
                                                        </dxe:ASPxLabel>
                                                        <br />
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right" style="width: 15%">
                                                        <dxe:ASPxLabel runat="server" Text="بابت" ID="ASPxLabel3">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" style="width: 35%">
                                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%" ID="cmbAccType"
                                                            RightToLeft="True" ValueType="System.Int32"
                                                            SelectedIndex="0" DataSourceID="ObjectDataSourceAccType"
                                                            TextField="TypeName" ValueField="AccTypeId" ClientInstanceName="cmbAccType">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <ValidationSettings Display="Dynamic">
                                                                <RequiredField ErrorText=""></RequiredField>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                            <ButtonStyle Width="13px">
                                                            </ButtonStyle>
                                                        </TSPControls:CustomAspxComboBox>
                                                        <asp:ObjectDataSource ID="ObjectDataSourceAccType" runat="server" SelectMethod="GetData"
                                                            TypeName="TSP.DataManager.TechnicalServices.AccTypeManager"></asp:ObjectDataSource>
                                                    </td>
                                                    <td valign="top" align="right" style="width: 15%">
                                                        <dxe:ASPxLabel runat="server" Text="مبلغ" ID="ASPxLabel33">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td dir="ltr" valign="top" align="right" style="width: 35%">
                                                        <TSPControls:CustomTextBox runat="server" ID="txtaAmount" Width="100%"
                                                            ClientInstanceName="txtaAmount">
                                                            <ValidationSettings Display="Dynamic" ValidationGroup="Acc" ErrorTextPosition="Bottom">

                                                                <RequiredField IsRequired="True" ErrorText="مبلغ را وارد نمایید"></RequiredField>
                                                                <RegularExpression ErrorText="نامعتبر" ValidationExpression="[1-9]\d*"></RegularExpression>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="شماره" ID="ASPxLabel31">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox runat="server" ID="txtaNumber" Width="100%"
                                                            Style="direction: ltr" ClientInstanceName="txtaNumber">
                                                            <ValidationSettings Display="Dynamic" ValidationGroup="Acc" ErrorTextPosition="Bottom">

                                                                <RequiredField IsRequired="True" ErrorText="شماره را وارد نمایید"></RequiredField>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="تاریخ" ID="ASPxLabel32">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnEvent="OnClick"
                                                            Width="220px" ShowPickerOnTop="True" ValidationGroup="Acc" ID="txtaDate" PickerDirection="ToRight"
                                                            RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                                        <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                                            ErrorMessage="تاریخ نامعتبر" ControlToValidate="txtaDate" ValidationGroup="Acc"
                                                            ID="PersianDateValidator2">تاریخ نامعتبر</pdc:PersianDateValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel25">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" colspan="3">
                                                        <TSPControls:CustomASPXMemo runat="server" Height="50px" ID="txtaDesc" Width="100%"
                                                            ClientInstanceName="txtaDesc">
                                                            <ValidationSettings>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomASPXMemo>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="middle" align="center" colspan="4">
                                                        <br />
                                                        <TSPControls:CustomAspxButton runat="server" Text="&nbsp;&nbsp;&nbsp;اضافه به لیست"
                                                            ID="btnAddAccounting" ValidationGroup="Acc"
                                                            AutoPostBack="False" Width="150px" UseSubmitBehavior="False">
                                                            <Image Width="16px" Height="16px" Url="~/Images/AddToList.png" />
                                                            <ClientSideEvents Click="function(s, e) { btnAdd_Click(); }" />
                                                        </TSPControls:CustomAspxButton>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="center" colspan="4">
                                                        <br />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>

                                    </div>
                                </asp:Panel>
                                <TSPControls:CustomAspxDevGridView2 runat="server" Width="100%" ID="GridViewAccounting"
                                    KeyFieldName="AccountingId" OnHtmlRowPrepared="GridViewAccounting_HtmlRowPrepared"
                                    OnRowDeleting="GridViewAccounting_RowDeleting" ClientInstanceName="grid" OnCustomCallback="GridViewAccounting_CustomCallback">
                                    <Settings ShowFilterBar="Hidden"></Settings>
                                    <Columns>
                                        <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption=" " Width="5%"
                                            Name="ColumnDelete" ShowDeleteButton="true">
                                            <%-- <DeleteButton Visible="True" Image-Url="~/Images/DeleteFromGrid.png">
                                        </DeleteButton>--%>
                                        </dxwgv:GridViewCommandColumn>
                                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="AccType"
                                            Caption="بابت">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="AccTypeName" Caption="بابت">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Number" Caption="شماره">
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="Date" Caption="تاریخ">
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="Amount" Caption="مبلغ">
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                            <PropertiesTextEdit DisplayFormatString="#,#">
                                            </PropertiesTextEdit>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="9">
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>
                                    <ClientSideEvents EndCallback="function(s,e){
                                        if(s.cpSaveComplete==1) ClearAccounting();
                                    else if(s.cpMessage!='')
                                       ShowMessage(s.cpMessage);

                                    s.cpMessage='';
                                    s.cpSaveComplete=-1;
                                        }" />
                                </TSPControls:CustomAspxDevGridView2>
                            </fieldset>
                            <fieldset id="ASPxRoundPanelAttachFiles" runat="server">
                                <legend class="HelpUL">فایل های پیوست</legend>
                                <table runat="server" id="TblFile" dir="rtl" width="100%">
                                    <tr runat="server" id="Tr19">
                                        <td runat="server" id="Td4541" align="right" valign="top" style="width: 15%">
                                            <asp:Label runat="server" Text="فایل" Width="24px" ID="lblimg"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td4212" align="right" valign="top" style="width: 85%">
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                                ID="flp" InputType="Files" ClientInstanceName="flpc" OnFileUploadComplete="flp_FileUploadComplete">
                                                                <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid)
	imgEndUploadImgClient3.SetVisible(true);
	else
	imgEndUploadImgClient3.SetVisible(false);	
}"></ClientSideEvents>
                                                            </TSPControls:CustomAspxUploadControl>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="فایل انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                                ID="ASPxImage2" ClientInstanceName="imgEndUploadImgClient3">
                                                            </dxe:ASPxImage>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <asp:Label runat="server" Text="توضیحات" Width="100%" ID="Label52"></asp:Label>
                                        </td>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomASPXMemo runat="server" Height="37px" Width="100%" ID="txtDescImg"
                                                ClientInstanceName="txtDescImg">
                                                <ValidationSettings>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomASPXMemo>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Tr21">
                                        <td colspan="2" align="center">
                                            <br />
                                            <TSPControls:CustomAspxButton runat="server" Text="&nbsp;&nbsp;&nbsp;اضافه به لیست"
                                                CausesValidation="False" ID="btnAddFlp" UseSubmitBehavior="False"
                                                AutoPostBack="false">
                                                <Image Width="16px" Height="16px" Url="~/Images/AddToList.png" />
                                                <ClientSideEvents Click="function(s,e){ AspxGridFlp.PerformCallback('Insert$'+txtDescImg.GetText()); }" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <TSPControls:CustomAspxDevGridView2 runat="server" Width="100%" ID="AspxGridFlp" KeyFieldName="Id"
                                    ClientInstanceName="AspxGridFlp" OnPageIndexChanged="AspxGridFlp_PageIndexChanged"
                                    RightToLeft="True" OnRowDeleting="AspxGridFlp_RowDeleting" OnCustomCallback="AspxGridFlp_CustomCallback">
                                    <Settings ShowFilterBar="Hidden"></Settings>
                                    <Columns>
                                        <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption=" " Width="5%"
                                            Name="Command" ShowDeleteButton="true">
                                            <%--     <DeleteButton Visible="True" Image-Url="~/Images/DeleteFromGrid.png">
                                        </DeleteButton>--%>
                                        </dxwgv:GridViewCommandColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="FilePath" Caption="فایل"
                                            Name="FilePath" Width="35%">
                                            <DataItemTemplate>
                                                <asp:HyperLink ID="HyperLink1" runat="server" Text="آدرس فایل" NavigateUrl='<%# Bind("TempImgUrl") %>'
                                                    Target="_blank"></asp:HyperLink>
                                            </DataItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server">LinkButton</asp:LinkButton>
                                            </EditItemTemplate>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Description" Caption="توضیحات"
                                            Name="Description" Width="65%">
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>
                                    <ClientSideEvents EndCallback="function(s,e){
                                    if(s.cpState=='1')
                                       ClearAttachments();
                                    else if(s.cpMessage!='')
                                       ShowMessage(s.cpMessage);

                                    s.cpMessage='';
                                    s.cpState=-1;
                                    }" />
                                </TSPControls:CustomAspxDevGridView2>
                            </fieldset>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>


                <br />

                <TSPControls:CustomASPxRoundPanelMenu ID="ASPxRoundPanel3" runat="server" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            CausesValidation="False" ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="BtnNew_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" AutoPostBack="true" Text=" " OnClick="btnEdit_Click"
                                            ToolTip="ویرایش" UseSubmitBehavior="False">

                                            <Image Height="25px" Url="../../Images/icons/edit.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave2" runat="server" EnableTheming="False"
                                            EnableViewState="False" OnClick="btnSave_Click" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False">

                                            <Image Height="25px" Url="~/Images/icons/save.png" Width="25px" />
                                            <ClientSideEvents Click="function(s, e) {btnSaveCheck(e);
                                                        }" />
                                        </TSPControls:CustomAspxButton>
                                    </td>

                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator5"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ"
                                            CausesValidation="False" ID="btnPrint2" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" AutoPostBack="false">
                                            <ClientSideEvents Click="function(s,e){
                                                                    CallBackMembers.PerformCallback('Print');
                                                                    }" />

                                            <Image Url="~/Images/icons/printers.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton6" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                            ToolTip="بازگشت" UseSubmitBehavior="False">

                                            <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <asp:ObjectDataSource ID="ODBSo" runat="server"
                    SelectMethod="GetData" TypeName="TSP.DataManager.SoldierManager"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ODBMar" runat="server"
                    SelectMethod="GetData" TypeName="TSP.DataManager.MaritalStatusManager"></asp:ObjectDataSource>
                <asp:HiddenField ID="MemberRequest" runat="server" Visible="False" />
                <asp:HiddenField ID="HDparam" runat="server" Visible="False" />
                <asp:HiddenField ID="PgMode" runat="server" Visible="False" />
                <dxhf:ASPxHiddenField ID="HiddenFieldPageType" ClientInstanceName="HiddenPageType"
                    runat="server">
                </dxhf:ASPxHiddenField>
                <dxhf:ASPxHiddenField ID="HiddenFieldInfo" ClientInstanceName="HiddenFieldInfo" runat="server">
                </dxhf:ASPxHiddenField>
                <asp:ObjectDataSource ID="OdbCity" runat="server" CacheDuration="30" SelectMethod="GetData"
                    TypeName="TSP.DataManager.CityManager"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="OdbAgent" runat="server" SelectMethod="GetData"
                    TypeName="TSP.DataManager.AccountingAgentManager"></asp:ObjectDataSource>
                <dxhf:ASPxHiddenField ID="HD_Flp" runat="server" ClientInstanceName="HDflp">
                </dxhf:ASPxHiddenField>
                <asp:ObjectDataSource ID="ObjectDataSourceCC" runat="server" SelectMethod="GetData"
                    TypeName="TSP.DataManager.AccountingCostCenterManager"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSourceProject" runat="server" SelectMethod="GetData"
                    TypeName="TSP.DataManager.AccountingProjectManager"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="OdbProvince" SelectMethod="GetData" runat="server" TypeName="TSP.DataManager.ProvinceManager"
                    FilterExpression="NezamCode<>{0}">
                    <FilterParameters>
                        <asp:Parameter Name="newparameter" />
                    </FilterParameters>
                </asp:ObjectDataSource>

                <asp:ObjectDataSource ID="ODBSex" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.SexManager"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ODBCom" runat="server"
                    SelectMethod="GetData" TypeName="TSP.DataManager.CommissionManager"></asp:ObjectDataSource>
                <dxhf:ASPxHiddenField ID="HDFlpLetter" runat="server" ClientInstanceName="flpletter">
                </dxhf:ASPxHiddenField>
                <dxhf:ASPxHiddenField ID="HDFlpIdNo" runat="server" ClientInstanceName="flpmeidno">
                </dxhf:ASPxHiddenField>
                <dxhf:ASPxHiddenField ID="HDFlpSSN" runat="server" ClientInstanceName="flpmessn">
                </dxhf:ASPxHiddenField>
                <dxhf:ASPxHiddenField ID="HDFlpMember" runat="server" ClientInstanceName="flpme">
                </dxhf:ASPxHiddenField>
                <dxhf:ASPxHiddenField ID="HDFlpSign" runat="server" ClientInstanceName="flpmesign">
                </dxhf:ASPxHiddenField>
                <dxhf:ASPxHiddenField ID="HD_LetterId" runat="server" ClientInstanceName="hiddenLetterId">
                </dxhf:ASPxHiddenField>
                <dxhf:ASPxHiddenField ID="HDFlpSol" runat="server" ClientInstanceName="flpmesol">
                </dxhf:ASPxHiddenField>
                <dxhf:ASPxHiddenField ID="HDFlpResident" runat="server" ClientInstanceName="HDFlpResident">
                </dxhf:ASPxHiddenField>
                <asp:HiddenField ID="HDIsMan" runat="server" Visible="False" />
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomAspxCallbackPanel>

</asp:Content>
