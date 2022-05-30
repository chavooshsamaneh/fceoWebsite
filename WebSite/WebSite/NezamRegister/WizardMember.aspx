<%@ Page Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true"
    CodeFile="WizardMember.aspx.cs" Inherits="NezamRegister_WizardMember" Title="عضویت حقیقی - مشخصات فردی" %>

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
    Namespace="DevExpress.Web" TagPrefix="dxlp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <script type="text/javascript" language="javascript">
        window.onload = function contentPageLoad(sender, args) {

            if (chbSex.GetSelectedIndex() == 1) {
                lblSol.SetVisible(true);
                drdSoId.SetVisible(true);
            }
            else {
                lblSol.SetVisible(false);
                drdSoId.SetVisible(false);
                MilitaryCommitment.SetVisible(false);
                MilitaryCommitment.SetChecked(false);
            }

            if (drdSoId.GetSelectedIndex() == 3 || drdSoId.GetSelectedIndex() == 4 || drdSoId.GetSelectedIndex() == 5) {

                MilitaryCommitment.SetVisible(true);
            }
            else {
                MilitaryCommitment.SetVisible(false);
                MilitaryCommitment.SetChecked(false);
            }

            if (chbtc.GetChecked()) {
                lblFileNo.SetVisible(true);
                FileNo.SetVisible(true);
            }
            else {
                lblFileNo.SetVisible(false);
                FileNo.SetVisible(false);
            }
        }



        function PDateValidatorBirthDate() {

            var Patt = /^1[2-4][0-9]{2}\/[0-1][0-9]\/[0-3][0-9]$/;
            var bol = Patt.test(document.getElementById('<%=txtBirthDate.ClientID%>').value);
            var YearBirth = document.getElementById('<%=txtBirthDate.ClientID%>').value.slice(0, 4);
            var YearToday = flpme.Get('Date');
            var Dif = YearToday - YearBirth;
            if (Dif < 19 || !bol) {
                lblPersianDate.SetVisible(true);
                return false;
            }
            return true;
        }

         function SetKardani(Visible) {
            panelKardani.SetVisible(Visible);
        }

        function SetTrue() {
            //document.getElementById('tblT').style.display = 'block';
            panelTransfer.SetVisible(true);

            ValidatorEnable(document.getElementById('<%=PersianDateValidatorTransfer.ClientID%>'), true);
        }
        function SetFalse() {
            //document.getElementById('tblT').style.display = 'none';
            panelTransfer.SetVisible(false);
            ValidatorEnable(document.getElementById('<%=PersianDateValidatorTransfer.ClientID%>'), false);

        }
        function SetSexMen() {
            drdSoId.SetVisible(true);
            lblSol.SetVisible(true);

            lblsolfile.SetVisible(true);
            flpsol.SetVisible(true);
            lblWarnSolImage.SetVisible(true);

            lblsolfileBack.SetVisible(true);
            flpsolBack.SetVisible(true);


        }
        function SetSexWomen() {
            drdSoId.SetVisible(false);
            drdSoId.SetSelectedIndex(-1);
            lblSol.SetVisible(false);

            MilitaryCommitment.SetVisible(false);
            MilitaryCommitment.SetChecked(false);


            hsol.SetVisible(false);
            lblsolfile.SetVisible(false);
            lblWarnSolImage.SetVisible(false);
            flpsol.SetVisible(false);

            lblsolfileBack.SetVisible(false);
            flpsolBack.SetVisible(false);
            HpSoldierBack.SetVisible(false);
        }
        function SetEmpty() {
            FirstName.SetText("");
            LastName.SetText("");
            txtNameEng.SetText("");
            txtLastNameEng.SetText("");
            IdNo.SetText("");
            SSN.SetText("");
            FatherName.SetText("");
            IssuePlace.SetText("");
            BirhtPlace.SetText("");
            MobileNo.SetText("");
            //BankAccNo.SetText("");
            HomeAdr.SetText("");
            Hometel.SetText("");
            Hometel_cityCode.SetText("");
            WorkAdr.SetText("");
            WorkTel.SetText("");
            WorkTel_cityCode.SetText("");
            FaxNo.SetText("");
            FaxNo_cityCode.SetText("");
            HomePO.SetText("");
            WorkPO.SetText("");
            //mFileNo.SetText("");
            chbSex.SetSelectedIndex(-1);
            chbMar.SetSelectedIndex(-1);
            drdSoId.SetSelectedIndex(-1);
            MilitaryCommitment.SetChecked(false);
            //chbRel.SetSelectedIndex(-1);
            chbCit.SetSelectedIndex(-1);
            chbAgent.SetSelectedIndex(-1);
            //  Nationality.SetText("");
            Website.SetText("");
            Email.SetText("");
            Desc.SetText("");
            CHB.SetChecked(false);
            ComboPr.SetSelectedIndex(-1);
            FileNo.SetText("");
            MeNo.SetText("");
            Code.SetText("");
            document.getElementById('<%=txtBirthDate.ClientID%>').value = "";
            document.getElementById('<%=txtTDate.ClientID%>').value = "";
            flpme.Set("name", "0");
            flpletter.Set("name", "0");
            imgme.SetVisible(false);
            imgletter.SetVisible(false);
            //document.getElementById('tblT').style.visibility = 'hidden';
            panelTransfer.SetVisible(false);
            flpmesign.Set("name", "0");
            imgmeSign.SetVisible(false);

            flpmeidno.Set("name", "0");
            HpIdNo.SetVisible(false);
            flpmeidno.Set("IdNoP2", "0");
            HIdNoP2.SetVisible(false);
            flpmeidno.Set("IdNoPDes", "0");
            HIdNoPDes.SetVisible(false);

            flpmessn.Set("SSNFront", "0");
            HpSSN.SetVisible(false);
            flpmessn.Set("SSNBack", "0");
            HpSSNBack.SetVisible(false);

            flpSold.Set('name', "0");
            lblSolFront.SetVisible(false);

            flpSold.Set('SolBack', "0");
            lblSolBack.SetVisible(false);

            flpResident.Set('name', "0");
            lblRes.SetVisible(false);

            panelKardani.SetVisible(false);
            ChkBKardani.SetChecked(false);
            imgEndUploadImgClientKardani.SetVisible(false);
	        HDFlpResident.Set('Kardani',0);
	        lblKardani.SetVisible(true);
	        HpKardani.SetVisible(false);
	        HpKardani.SetNavigateUrl('');
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanelNezamRegister" runat="server">
        <ContentTemplate>
            <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
                CalendarDayWidth="31" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
                FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
                WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
            </pdc:PersianDateScriptManager>
            <div align="right" id="DivReport" class="DivErrors" runat="server" visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server">
                <Items>
                    <dxm:MenuItem Text="چهارچوب شئون حرفه ای" Name="Membership">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Member" Text="مشخصات فردی" Selected="true">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Madrak" Text="مدارک تحصیلی">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Job" Text="سوابق کاری">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Activity" Text="فعالیت ها">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Language" Text="زبان ها">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Summary" Text="خلاصه اطلاعات">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="End" Text="ثبت نهایی">
                    </dxm:MenuItem>
                </Items>
            </TSPControls:CustomAspxMenuHorizontal>
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelPeriod" HeaderText="مشخصات فردی"
                runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <div class="row">
                            <div class="col-md-3">نام *</div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtFirstName"
                                    ClientInstanceName="FirstName"
                                    MaxLength="30">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                        <RequiredField IsRequired="True" ErrorText="نام را وارد نمایید"></RequiredField>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </div>
                            <div class="col-md-3">نام خانوادگی *</div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtLastName"
                                    ClientInstanceName="LastName"
                                    MaxLength="50">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                        <RequiredField IsRequired="True" ErrorText="نام خانوادگی را وارد نمایید"></RequiredField>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">نام انگلیسی *</div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtFirstNameEn"
                                    ClientInstanceName="txtNameEng" MaxLength="30">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <RequiredField IsRequired="True" ErrorText="نام (انگلیسی) را وارد نمایید"></RequiredField>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </div>
                            <div class="col-md-3">نام خانوادگی انگلیسی *</div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtLastNameEn"
                                    ClientInstanceName="txtLastNameEng" MaxLength="50">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <RequiredField IsRequired="True" ErrorText="این فیلد را وارد نمایید"></RequiredField>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">شماره شناسنامه *</div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox runat="server" Width="100%" MaxLength="10" ID="txtIdNo"
                                    ClientInstanceName="IdNo">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <RequiredField IsRequired="True" ErrorText="شماره شناسنامه را وارد نمایید"></RequiredField>
                                        <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{1,10}"></RegularExpression>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </div>
                            <div class="col-md-3">کد ملی *</div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox runat="server" Width="100%" MaxLength="10" ID="txtSSN"
                                    ClientInstanceName="SSN">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <RequiredField IsRequired="True" ErrorText="کد ملی را وارد نمایید"></RequiredField>
                                        <RegularExpression ErrorText="این کد صحیح نیست" ValidationExpression="\d{10}"></RegularExpression>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">نام پدر *</div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtFatherName"
                                    ClientInstanceName="FatherName"
                                    MaxLength="30">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <RequiredField IsRequired="True" ErrorText="نام پدر را وارد نمایید"></RequiredField>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </div>
                            <div class="col-md-3">محل صدور *</div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtIssuePlace"
                                    ClientInstanceName="IssuePlace"
                                    MaxLength="50">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <RequiredField IsRequired="True" ErrorText="محل صدور را وارد نمایید"></RequiredField>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">تاریخ تولد *</div>
                            <div class="col-md-3">
                                <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="200px" ID="txtBirthDate"
                                    PickerDirection="ToRight" ShowPickerOnEvent="OnClick" IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                <pdc:PersianDateValidator runat="server" ValidateEmptyText="True"
                                    ForeColor="Red" ErrorMessage="تاریخ تولد را با فرمت درست انتخاب نمایید" ControlToValidate="txtBirthDate"
                                    ID="PersianDateValidatorBirthDate"></pdc:PersianDateValidator>
                                <dxe:ASPxLabel ID="lblPersianDate" ClientInstanceName="lblPersianDate" ClientVisible="false" Width="100%" ForeColor="Red"
                                    runat="server" Text="باید سن متقاضی حداقل 18 سال تمام باشد و فرمت نیز تاریخ درست باشد">
                                </dxe:ASPxLabel>
                            </div>
                            <div class="col-md-3">محل تولد</div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtBirhtPlace"
                                    ClientInstanceName="BirhtPlace"
                                    MaxLength="50">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <RequiredField IsRequired="False" ErrorText="محل تولد را وارد نمایید"></RequiredField>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">شماره تلفن محل سکونت *</div>
                            <div class="col-md-3">
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td style="vertical-align: top" align="right" width="60%">
                                                <TSPControls:CustomTextBox runat="server" Width="100%" MaxLength="8" ID="txtHometel"
                                                    ClientInstanceName="Hometel">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField IsRequired="True" ErrorText="شماره تلفن را وارد نمایید"></RequiredField>
                                                        <RegularExpression ErrorText="شماره تلفن را صحیح وارد نمایید" ValidationExpression="\d{5,8}"></RegularExpression>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td style="vertical-align: top" align="center" width="5%">
                                                <asp:Label runat="server" Text="-" ID="Label65"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top" align="right" width="35%">
                                                <TSPControls:CustomTextBox runat="server" Width="100%" MaxLength="4" ID="txtHometel_cityCode"
                                                    ClientInstanceName="Hometel_cityCode">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="(0)\d{2,3}"></RegularExpression>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <div class="col-md-3">شماره تلفن همراه*</div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox runat="server" Width="100%" MaxLength="11" ID="txtMobileNo"
                                    ClientInstanceName="MobileNo">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <RequiredField IsRequired="True" ErrorText="شماره همراه را وارد نمایید"></RequiredField>
                                        <RegularExpression ErrorText="شماره تلفن همراه را صحیح وارد نمایید (به فرمت 09xxxxxxxxx)"
                                            ValidationExpression="09\d{9}"></RegularExpression>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">آدرس محل سکونت*</div>
                            <div class="col-md-9">
                                <TSPControls:CustomASPXMemo runat="server" Height="30px" Width="100%" ID="txtHomeAdr"
                                    ClientInstanceName="HomeAdr">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <RequiredField IsRequired="True" ErrorText="آدرس را وارد نمایید"></RequiredField>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomASPXMemo>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">تلفن محل کار</div>
                            <div class="col-md-3">
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td style="vertical-align: top" align="right" width="60%">
                                                <TSPControls:CustomTextBox runat="server" Width="100%" MaxLength="8" ID="txtWorkTel"
                                                    ClientInstanceName="WorkTel">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField ErrorText="شماره تلفن را وارد نمایید"></RequiredField>
                                                        <RegularExpression ErrorText="شماره تلفن را صحیح وارد نمایید" ValidationExpression="\d{5,8}"></RegularExpression>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td style="vertical-align: top" align="center" width="5%">
                                                <asp:Label runat="server" Text="-" ID="Label2"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top" align="right" width="35%">
                                                <TSPControls:CustomTextBox runat="server" Width="100%" MaxLength="4" ID="txtWorkTel_cityCode"
                                                    ClientInstanceName="WorkTel_cityCode">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="(0)\d{2,3}"></RegularExpression>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <div class="col-md-3">شماره فکس</div>
                            <div class="col-md-3">
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td style="vertical-align: top" align="right" width="60%">
                                                <TSPControls:CustomTextBox runat="server" Width="100%" MaxLength="8" ID="txtFaxNo"
                                                    ClientInstanceName="FaxNo">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField ErrorText=""></RequiredField>
                                                        <RegularExpression ErrorText="شماره فکس را صحیح وارد نمایید" ValidationExpression="\d{5,8}"></RegularExpression>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td style="vertical-align: top" align="center" width="5%">
                                                <asp:Label runat="server" Text="-" ID="Label3"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top" align="right" width="35%">
                                                <TSPControls:CustomTextBox runat="server" Width="100%" MaxLength="4" ID="txtFaxNo_cityCode"
                                                    ClientInstanceName="FaxNo_cityCode">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="(0)\d{2,3}"></RegularExpression>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">آدرس محل کار</div>
                            <div class="col-md-9">
                                <TSPControls:CustomASPXMemo runat="server" Height="30px" Width="100%" ID="txtWorkAdr"
                                    ClientInstanceName="WorkAdr">
                                    <ValidationSettings>
                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomASPXMemo>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">کد پستی محل سکونت*</div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox runat="server" Width="100%" MaxLength="10" ID="txtHomePO"
                                    ClientInstanceName="HomePO">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <RegularExpression ErrorText="این کد صحیح نیست" ValidationExpression="^\d{10}$"></RegularExpression>
                                        <RequiredField IsRequired="true" ErrorText="کد پستی را وارد نمایید" />
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </div>
                            <div class="col-md-3">کد پستی محل کار</div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox runat="server" Width="100%" MaxLength="10" ID="txtWorkPO"
                                    ClientInstanceName="WorkPO">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <RegularExpression ErrorText="این کد صحیح نیست" ValidationExpression="^\d{10}$"></RegularExpression>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </div>
                        </div>
                        <div class="row">
                            <ul class="HelpUL" style="padding: 0;">* از وارد نمودن شماره کارت عابر بانک جداً خودداری نمائید</ul>
                        </div>
                        <div class="row">
                            <div class="col-md-3">شماره حساب</div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox runat="server" Width="100%" MaxLength="16" ID="txtBankAccNo"
                                    ClientInstanceName="BankAccNo">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{1,16}"></RegularExpression>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </div>
                            <div class="col-md-3"></div>
                            <div class="col-md-3"></div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">جنسیت *</div>
                            <div class="col-md-3">
                                <TSPControls:CustomAspxComboBox runat="server" EnableClientSideAPI="True" Width="100%"
                                    TextField="SexName" ID="drdSexId"
                                    RightToLeft="True" DataSourceID="ODBSex" ValueType="System.String" ValueField="SexId"
                                    ClientInstanceName="chbSex" EnableIncrementalFiltering="true">
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	if (chbSex.GetSelectedIndex()== 0)
        {
          SetSexWomen();
        }
        else
        { 
			SetSexMen();
        }
      
}"></ClientSideEvents>
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <RequiredField IsRequired="True" ErrorText="جنسیت را انتخاب نمایید"></RequiredField>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                    <ButtonStyle Width="13px">
                                    </ButtonStyle>
                                </TSPControls:CustomAspxComboBox>
                            </div>
                            <div class="col-md-3">
                                <dxe:ASPxLabel runat="server" Text="وضعیت سربازی*" ClientVisible="false" ID="lblSol"
                                    ClientInstanceName="lblSol">
                                </dxe:ASPxLabel>
                            </div>
                            <div class="col-md-3">
                                <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                    RightToLeft="True" TextField="SoName" ID="drdSoId"
                                    EnableIncrementalFiltering="true" DataSourceID="ODBSo" ValueType="System.String"
                                    ClientVisible="false" ValueField="SoId" ClientInstanceName="drdSoId">
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
   if (drdSoId.GetSelectedIndex() == 3 || drdSoId.GetSelectedIndex() == 4 || drdSoId.GetSelectedIndex() == 5)
                {
                   MilitaryCommitment.SetChecked(false);
                   MilitaryCommitment.SetVisible(true);
                }
                
            else
            { 
              MilitaryCommitment.SetVisible(false);
              MilitaryCommitment.SetChecked(false);
            }
      
}"></ClientSideEvents>
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                        <RequiredField IsRequired="false" ErrorText="وضعیت را انتخاب نمایید" />
                                    </ValidationSettings>
                                    <ButtonStyle Width="13px">
                                    </ButtonStyle>
                                </TSPControls:CustomAspxComboBox>
                            </div>
                        </div>
                        <div class="row">
                            <TSPControls:CustomASPxCheckBox runat="server" Text="اينجانب متعهد مي گردم درصورت قبولي در آزمون ورود به حرفه مهندسي و با علم به اينكه، براساس ابلاغ، اصلاحيه آيين نامه اجرايي قانون نظام مهندسي و كنترل ساختمان در تاريخ 94/12/20، ارائه كارت پايان خدمت يا كارت معافيت از خدمت جهت صدور/تمديد پروانه اشتغال الزامي مي باشد تصوير كارت مذكور را در سايت نظام مهندسي آپلود كرده و تصوير برابر اصل آن را تحويل واحد عضويت و پروانه اشتغال (حقيقي وحقوقي) بدهم در غيراينصورت امكان صدور پروانه اشتغال وجود نداشته و حق هرگونه اعتراضي را ازخود سلب مي نمايم" EnableClientSideAPI="True"
                                ID="MilitaryCommitment" ClientInstanceName="MilitaryCommitment">
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                    <RequiredField IsRequired="true" ErrorText="موارد اعلام شده مورد موافقت قرار نگرفته است" />
                                </ValidationSettings>
                            </TSPControls:CustomASPxCheckBox>
                        </div>
                        <div class="row">
                            <div class="col-md-3">وضعیت تأهل*</div>
                            <div class="col-md-3">
                                <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                    RightToLeft="True" TextField="MarName"
                                    ID="drdMarId" EnableIncrementalFiltering="true" DataSourceID="ODBMar"
                                    ValueType="System.String" ValueField="MarId"
                                    ClientInstanceName="chbMar">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                        <RequiredField IsRequired="true" ErrorText="وضعیت تاهل را انتخاب نمایید" />
                                    </ValidationSettings>
                                    <ButtonStyle Width="13px">
                                    </ButtonStyle>
                                </TSPControls:CustomAspxComboBox>
                            </div>
                            <div class="col-md-3">ملیت</div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtNationality"
                                    Text="ایرانی" ClientInstanceName="Nationality">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <RequiredField IsRequired="True" ErrorText="ملیت را وارد نمایید"></RequiredField>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">نمایندگی*</div>
                            <div class="col-md-3">
                                <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                    RightToLeft="True" TextField="Name" ID="drdAgent"
                                    EnableIncrementalFiltering="true" DataSourceID="OdbAgent"
                                    ValueType="System.String" ValueField="AgentId"
                                    ClientInstanceName="chbAgent">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                        <RequiredField ErrorText="نمایندگی را انتخاب نمایید" IsRequired="True"></RequiredField>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                    <ButtonStyle Width="13px">
                                    </ButtonStyle>
                                </TSPControls:CustomAspxComboBox>
                            </div>
                            <div class="col-md-3">محل اقامت*</div>
                            <div class="col-md-3">
                                <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                    RightToLeft="True" TextField="CitName"
                                    ID="drdCitId" EnableIncrementalFiltering="true" DataSourceID="OdbCity"
                                    ValueType="System.String" ValueField="CitId"
                                    ClientInstanceName="chbCit">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                        <RequiredField IsRequired="True" ErrorText="محل اقامت را انتخاب نمایید"></RequiredField>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                    <ButtonStyle Width="13px">
                                    </ButtonStyle>
                                </TSPControls:CustomAspxComboBox>
                            </div>
                        </div>
                        <div class="row">
                            <ul class="HelpUL" style="padding: 0;">*ابتدای پست الکترونیکی دارای .www   <u>نمی باشد</u>. لطفا از وارد نمودن .www در ابتدای پست الکترونیکی خود جدا خودداری نمایید.</ul>
                        </div>
                        <div class="row">
                            <div class="col-md-3">پست الکترونیکی*</div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox runat="server" ID="txtEmail"
                                    ClientInstanceName="Email" MaxLength="60">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <RequiredField IsRequired="true" ErrorText="پست الکترونیکی را وارد نمایید"></RequiredField>
                                        <RegularExpression ErrorText="پست الکترونیکی را با فرمت صحیح وارد نمایید" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></RegularExpression>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </div>
                            <div class="col-md-3">آدرس وب سایت</div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox runat="server" ID="txtWebsite"
                                    ClientInstanceName="Website" MaxLength="60">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <RegularExpression ErrorText="آدرس وب سایت را با فرمت http://www.Example.com وارد نمایید"
                                            ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"></RegularExpression>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </div>
                        </div>
                        <div class="row">
                            <ul class="HelpUL" style="padding: 0;"><b>اصل شناسنامه ، اصل كارت ملي ، اصل كارت پايان خدمت  ، (به صورت رنگي ) اسكن شود.(عكس برداري توسط تلفن همراه  قابل قبول نمی باشد.)</b></ul>
                            <asp:Label runat="server" ID="lblImg" CssClass="HelpUL">
                            </asp:Label>

                        </div>
                        <div class="row">
                            <div class="col-md-3">تصویر*</div>
                            <div class="col-md-3">
                                <table>
                                    <tbody>
                                        <tr>
                                            <td align="right">
                                                <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                    ID="flpImage" InputType="Images" ClientInstanceName="flpc" OnFileUploadComplete="flpImage_FileUploadComplete">
                                                    <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                            if(e.isValid){
	imgEndUploadImgClient.SetVisible(true);
	flpme.Set('name',1);
	lblValImageMe.SetVisible(false);
	imgme.SetVisible(true);
	imgme.SetImageUrl('../Image/Temp/'+e.callbackData);
	}
	else{
	imgEndUploadImgClient.SetVisible(false);
	flpme.Set('name',0);
	lblValImageMe.SetVisible(true);
	imgme.SetVisible(false);
	imgme.SetImageUrl('');
	}
}"></ClientSideEvents>
                                                </TSPControls:CustomAspxUploadControl>
                                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" ClientInstanceName="lblValImageMe" ClientVisible="False"
                                                    ForeColor="Red" Text="تصویر را انتخاب نمایید">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td>
                                                <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                    ID="imgEndUploadImg" ClientInstanceName="imgEndUploadImgClient">
                                                </dxe:ASPxImage>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <dxe:ASPxImage runat="server" Height="75px" Width="75px" ID="imgMember" ClientInstanceName="imgme"
                                    Border-BorderWidth="1px" Border-BorderStyle="Solid" ClientVisible="False" EnableClientSideAPI="True">
                                    <EmptyImage Height="75px" Width="75px" Url="~/Images/person.png">
                                    </EmptyImage>
                                </dxe:ASPxImage>
                                <div align="right" dir="rtl">
                                </div>
                            </div>
                            <div class="col-md-3">تصویر امضا</div>
                            <div class="col-md-3">
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <TSPControls:CustomAspxUploadControl ID="flpSign" runat="server" ClientInstanceName="flps"
                                                    InputType="Images" UploadWhenFileChoosed="true" OnFileUploadComplete="flpSign_FileUploadComplete">
                                                    <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                            if(e.isValid){
	imgEndUploadImgClientSign.SetVisible(true);
    flpmesign.Set('name',1);
	lblValSign.SetVisible(false);
	imgmeSign.SetVisible(true);
	imgmeSign.SetImageUrl('../Image/Temp/'+e.callbackData);
	}
	else{
	imgEndUploadImgClientSign.SetVisible(false);
    flpmesign.Set('name',0);
	lblValSign.SetVisible(true);
	imgmeSign.SetVisible(false);
	imgmeSign.SetImageUrl('');
	}
}" />
                                                </TSPControls:CustomAspxUploadControl>
                                                <dxe:ASPxLabel ID="ASPxLabel10" runat="server" ClientInstanceName="lblValSign" ClientVisible="False"
                                                    ForeColor="Red" Text="تصویر امضا را انتخاب نمایید">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td>
                                                <dxe:ASPxImage ID="imgEndUploadImgSign" runat="server" ClientInstanceName="imgEndUploadImgClientSign"
                                                    ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد">
                                                </dxe:ASPxImage>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <dxe:ASPxImage ID="ImgSign" runat="server" ClientInstanceName="imgmeSign" ClientVisible="False"
                                    Height="75px" Width="75px" Border-BorderWidth="1px" Border-BorderStyle="Solid">
                                    <EmptyImage Height="75px" Url="~/Images/noimage.gif" Width="75px" />
                                </dxe:ASPxImage>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">تصویر صفحه اول شناسنامه*</div>
                            <div class="col-md-3">
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <TSPControls:CustomAspxUploadControl ID="flpIdNo" runat="server" ClientInstanceName="flpIdNo"
                                                    InputType="Images" UploadWhenFileChoosed="true" OnFileUploadComplete="flpIdNo_FileUploadComplete">
                                                    <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                            if(e.isValid){
	imgEndUploadImgClientIdNo.SetVisible(true);
    flpmeidno.Set('name',1);
	lblValIdNo.SetVisible(false);
	HpIdNo.SetVisible(true);
	HpIdNo.SetNavigateUrl('../Image/Temp/'+e.callbackData);
	}
	else{
	imgEndUploadImgClientIdNo.SetVisible(false);
    flpmeidno.Set('name',0);
	lblValIdNo.SetVisible(true);
	HpIdNo.SetVisible(false);
	HpIdNo.SetNavigateUrl('');
	}
}" />
                                                </TSPControls:CustomAspxUploadControl>
                                                <dxe:ASPxLabel ID="ASPxLabel12" runat="server" ClientInstanceName="lblValIdNo" ClientVisible="False"
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
                                <dxe:ASPxHyperLink ID="HpIdNo" runat="server" ClientInstanceName="HpIdNo" ClientVisible="False"
                                    Target="_blank" Text="تصویر شناسنامه">
                                </dxe:ASPxHyperLink>
                            </div>
                            <div class="col-md-3">تصویر صفحه دوم شناسنامه</div>
                            <div class="col-md-3">
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
	HIdNoP2.SetNavigateUrl('../Image/Temp/'+e.callbackData);
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
                                <dxe:ASPxHyperLink ID="HIdNoP2" runat="server" ClientInstanceName="HIdNoP2" ClientVisible="False"
                                    Target="_blank" Text="تصویر صفحه دوم شناسنامه">
                                </dxe:ASPxHyperLink>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">تصویر صفحه توضیحات شناسنامه</div>
                            <div class="col-md-3">
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
	HIdNoPDes.SetNavigateUrl('../Image/Temp/'+e.callbackData);
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
                                <dxe:ASPxHyperLink ID="HIdNoPDes" runat="server" ClientInstanceName="HIdNoPDes" ClientVisible="False"
                                    Target="_blank" Text="تصویر صفحه توضیحات شناسنامه">
                                </dxe:ASPxHyperLink>
                            </div>
                            <div class="col-md-3">تصویر روی کارت ملی*</div>
                            <div class="col-md-3">
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <TSPControls:CustomAspxUploadControl ID="flpSSN" runat="server" ClientInstanceName="flpSSN"
                                                    InputType="Images" UploadWhenFileChoosed="true" OnFileUploadComplete="flpSSN_FileUploadComplete">
                                                    <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                            if(e.isValid){
	imgEndUploadImgClientSSN.SetVisible(true);
    flpmessn.Set('SSNFront',1);
	lblSSN.SetVisible(false);
	HpSSN.SetVisible(true);
	HpSSN.SetNavigateUrl('../Image/Temp/'+e.callbackData);
	}
	else{
	imgEndUploadImgClientSSN.SetVisible(false);
    flpmessn.Set('SSNFront',0);
	lblSSN.SetVisible(true);
	HpSSN.SetVisible(false);
	HpSSN.SetNavigateUrl('');
	}
}" />
                                                </TSPControls:CustomAspxUploadControl>
                                                <dxe:ASPxLabel ID="lblSSN" runat="server" ClientInstanceName="lblSSN" ClientVisible="False"
                                                    ForeColor="Red" Text="تصویر روی کارت ملی را انتخاب نمایید">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td>
                                                <dxe:ASPxImage ID="imgEndUploadImgSSN" runat="server" ClientInstanceName="imgEndUploadImgClientSSN"
                                                    ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد">
                                                </dxe:ASPxImage>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <dxe:ASPxHyperLink ID="HpSSN" runat="server" ClientInstanceName="HpSSN" ClientVisible="False"
                                    Target="_blank" Text="تصویر روی کارت ملی">
                                </dxe:ASPxHyperLink>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">تصویر پشت کارت ملی*</div>
                            <div class="col-md-3">
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
	lblSSNBack.SetVisible(false);
	HpSSNBack.SetVisible(true);
	HpSSNBack.SetNavigateUrl('../Image/Temp/'+e.callbackData);
	}
	else{
	imgEndUploadImgClientSSNBack.SetVisible(false);
    flpmessn.Set('SSNBack',0);
	lblSSNBack.SetVisible(true);
	HpSSNBack.SetVisible(false);
	HpSSNBack.SetNavigateUrl('');
	}
}" />
                                                </TSPControls:CustomAspxUploadControl>
                                                <dxe:ASPxLabel ID="lblSSNBack" runat="server" ClientInstanceName="lblSSNBack" ClientVisible="False"
                                                    ForeColor="Red" Text="تصویر پشت کارت ملی را انتخاب نمایید">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td>
                                                <dxe:ASPxImage ID="ASPxImage2" runat="server" ClientInstanceName="imgEndUploadImgClientSSNBack"
                                                    ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد">
                                                </dxe:ASPxImage>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <dxe:ASPxHyperLink ID="HpSSNBack" runat="server" ClientInstanceName="HpSSNBack" ClientVisible="False"
                                    Target="_blank" Text="تصویر پشت کارت ملی">
                                </dxe:ASPxHyperLink>
                            </div>
                            <div class="col-md-3"></div>
                            <div class="col-md-3"></div>
                        </div>
                        <div class="row">
                            <dxe:ASPxLabel ClientInstanceName="lblWarnSolImage" Font-Bold="true" ClientVisible="false" runat="server" Text="جهت دریافت پروانه اشتغال به کار ثبت تصویر کارت پایان خدمت الزامی می باشد"
                                ID="lblWarnSolImage" CssClass="HelpUL">
                            </dxe:ASPxLabel>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <dxe:ASPxLabel ID="lblSolFile" runat="server" Text="تصویر روی کارت پایان خدمت*" ClientInstanceName="lblsolfile">
                                </dxe:ASPxLabel>
                            </div>
                            <div class="col-md-3">
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                    ID="flpSoldier" InputType="Images" ClientInstanceName="flpsol" OnFileUploadComplete="flpSoldier_FileUploadComplete">
                                                    <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                            if(e.isValid){
	imgEndUploadImgClientSol.SetVisible(true);
   
	flpSold.Set('name',1);
	lblSolFront.SetVisible(false);
	hsol.SetVisible(true);
	hsol.SetNavigateUrl('../Image/Temp/'+e.callbackData);
	}
	else{
	imgEndUploadImgClientSol.SetVisible(false);
   
	flpSold.Set('name',0);
	lblSolFront.SetVisible(true);
	hsol.SetVisible(false);
	hsol.SetNavigateUrl('');
	}
}" />
                                                </TSPControls:CustomAspxUploadControl>
                                                <dxe:ASPxLabel ID="lblSolFront" runat="server" ClientInstanceName="lblSolFront" ClientVisible="False"
                                                    ForeColor="Red" Text="تصویر روی کارت پایان خدمت را انتخاب نمایید">
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
                                <dxe:ASPxHyperLink ID="HpSoldier" runat="server" ClientInstanceName="hsol" ClientVisible="False"
                                    Target="_blank" Text="تصویر روی کارت پایان خدمت">
                                </dxe:ASPxHyperLink>
                            </div>
                            <div class="col-md-3">
                                <dxe:ASPxLabel ID="lblSolFileBack" runat="server" Text="تصویر پشت کارت پایان خدمت*" ClientInstanceName="lblsolfileBack">
                                </dxe:ASPxLabel>
                            </div>
                            <div class="col-md-3">
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                    ID="flpSoldierBack" InputType="Images" ClientInstanceName="flpsolBack" OnFileUploadComplete="flpSoldier_FileUploadComplete">
                                                    <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                            if(e.isValid){
	imgEndUploadImgSolBack.SetVisible(true);
   
	flpSold.Set('SolBack',1);
	lblSolBack.SetVisible(false);
	HpSoldierBack.SetVisible(true);
	HpSoldierBack.SetNavigateUrl('../Image/Temp/'+e.callbackData);
	}
	else{
	imgEndUploadImgSolBack.SetVisible(false);
   
	flpSold.Set('SolBack',0);
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
                                <dxe:ASPxHyperLink ID="HpSoldierBack" runat="server" ClientInstanceName="HpSoldierBack" ClientVisible="False"
                                    Target="_blank" Text="تصویر پشت کارت پایان خدمت">
                                </dxe:ASPxHyperLink>
                            </div>
                        </div>
                   
                        <div class="row">
                            <dxp:ASPxPanel ID="panelResident" runat="server" ClientInstanceName="panelResident">
                                <PanelCollection>
                                    <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">                                    
                                        <div class="row">
                                            <div class="col-md-3">تصویر مدرک سکونت در استان فارس*</div>
                                            <div class="col-md-9">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                                    ID="flpResident" InputType="Images" ClientInstanceName="flpResident" OnFileUploadComplete="flpResident_FileUploadComplete">
                                                                    <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                            if(e.isValid){
	imgEndUploadImgClientRes.SetVisible(true);
	HDFlpResident.Set('name',1);
	lblRes.SetVisible(false);
	hRes.SetVisible(true);
	hRes.SetNavigateUrl('../Image/Temp/'+e.callbackData);
	}
	else{
	imgEndUploadImgClientRes.SetVisible(false);
	HDFlpResident.Set('name',0);
	lblRes.SetVisible(true);
	hRes.SetVisible(false);
	hRes.SetNavigateUrl('');
	}
}" />
                                                                </TSPControls:CustomAspxUploadControl>
                                                                <dxe:ASPxLabel ID="ASPxLabel16" runat="server" ClientInstanceName="lblRes" ClientVisible="False"
                                                                    ForeColor="Red" Text="تصویر مدرک سکونت در استان را انتخاب نمایید">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                                    ID="ASPxImage1" ClientInstanceName="imgEndUploadImgClientRes">
                                                                </dxe:ASPxImage>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <dxe:ASPxHyperLink ID="HypLinkResident" runat="server" ClientInstanceName="hRes"
                                                    ClientVisible="False" Target="_blank" Text="تصویر مدرک سکونت در استان">
                                                </dxe:ASPxHyperLink>
                                            </div>
                                        </div>
                                    </dxp:PanelContent>
                                </PanelCollection>
                            </dxp:ASPxPanel>
                        </div>
   <div class="row">
                            <div class="col-md-3">توضیحات(حداکثر255کاراکتر)</div>
                            <div class="col-md-9">
                                <TSPControls:CustomASPXMemo runat="server" Height="45px" Width="100%" ID="txtDesc"
                                    ClientInstanceName="Desc">
                                    <ValidationSettings>
                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                    <ClientSideEvents KeyPress="function(s,e){ CheckDevExpressTextboxLengthOnKeyPress(s,e,255); }" />
                                </TSPControls:CustomASPXMemo>
                            </div>
                        </div>
                        
                        <div class="row">
                            
                            <TSPControls:CustomASPxCheckBox runat="server" Text="دارای مدرک تحصیلی کارشناسی ناپیوسته یا کاردانی می باشم."
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
                        </div>
                        <div class="row">
                            <dxp:ASPxPanel ID="panelKardani" runat="server" ClientVisible="false" ClientInstanceName="panelKardani">
                                <PanelCollection>
                                    <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                                        <div class="row">
                                            <ul class="HelpUL" style="padding: 0;">چنانچه دارای مدرک کارشناسی ناپیوسته یا مدرک کاردانی می باشید باید تصویر استعلام عدم عضویت در نظام کاردانی ساختمان استان فارس بارگذاری شود .</ul>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-3">تصویر استعلام عدم عضویت در نظام کاردانی*</div>
                                            <div class="col-md-9">
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
	HpKardani.SetNavigateUrl('../image/Members/NezamKardani/Request/'+e.callbackData);
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
                                            </div>
                                        </div>

                                    </dxp:PanelContent>
                                </PanelCollection>
                            </dxp:ASPxPanel>
                        </div>
                     
                        <div class="row">
                            <TSPControls:CustomASPxCheckBox runat="server" Text="از استان دیگری به استان فارس منتقل شده ام."
                                EnableClientSideAPI="True" ID="ChEnteghali" ClientInstanceName="CHB">
                                <ClientSideEvents CheckedChanged="function(s, e) {
	if (CHB.GetChecked()== true)
        {
		SetTrue();
        }
        else
        {
		SetFalse();
       }
}"></ClientSideEvents>
                            </TSPControls:CustomASPxCheckBox>
                        </div>
                        <div class="row">
                            <dxp:ASPxPanel ID="panelTransfer" runat="server" ClientInstanceName="panelTransfer">
                                <PanelCollection>
                                    <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                                        <div class="row">
                                            <div class="col-md-3">استان قبلی</div>
                                            <div class="col-md-3">
                                                <TSPControls:CustomAspxComboBox runat="server"
                                                    RightToLeft="True" TextField="PrName" ID="ComboTPr" DataSourceID="OdbProvince"
                                                    EnableIncrementalFiltering="true" ValueType="System.String" ValueField="PrId"
                                                    ClientInstanceName="ComboPr">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField IsRequired="True" ErrorText="استان قبلی را انتخاب نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                    <ButtonStyle Width="13px">
                                                    </ButtonStyle>
                                                </TSPControls:CustomAspxComboBox>
                                            </div>
                                            <div class="col-md-3">تاریخ انتقالی</div>
                                            <div class="col-md-3">
                                                <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="200px" ShowPickerOnTop="True"
                                                    ShowPickerOnEvent="OnClick" ID="txtTDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                                <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" 
                                                    ForeColor="Red" ErrorMessage="تاریخ انتقالی را با فرمت درست انتخاب نمایید" ControlToValidate="txtTDate"
                                                    ID="PersianDateValidatorTransfer"></pdc:PersianDateValidator>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-3">شماره عضویت</div>
                                            <div class="col-md-3">
                                                <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtTMeNo" ClientInstanceName="MeNo"
                                                    Style="direction: ltr">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField IsRequired="True" ErrorText="کد عضویت را وارد  نمایید"></RequiredField>
                                                        <RegularExpression ErrorText="این کد صحیح نیست" ValidationExpression="\d*"></RegularExpression>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </div>
                                            <div class="col-md-3">تصویر نامه انتقالی*</div>
                                            <div class="col-md-3">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td align="right">
                                                                <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                                    ID="FlpTLetter" InputType="Images" ClientInstanceName="Img" OnFileUploadComplete="FlpTLetter_FileUploadComplete">
                                                                    <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                                            if(e.isValid){
	imgEndUploadImgClientLetter.SetVisible(true);
	flpletter.Set('name',1);
	lblValImageLetter.SetVisible(false);
	imgletter.SetVisible(true);
	imgletter.SetImageUrl('../Image/Temp/'+e.callbackData);
	}
	else{
	imgEndUploadImgClientLetter.SetVisible(false);
	flpletter.Set('name',0);
	lblValImageLetter.SetVisible(true);
	imgletter.SetVisible(false);
	imgletter.SetImageUrl('');
	}
}"></ClientSideEvents>
                                                                </TSPControls:CustomAspxUploadControl>
                                                                <dxe:ASPxLabel ID="ASPxLabel8" runat="server" ClientInstanceName="lblValImageLetter" ClientVisible="False"
                                                                    ForeColor="Red" Text="تصویر نامه را انتخاب نمایید">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                                    ID="imgEndUploadImgLetter" ClientInstanceName="imgEndUploadImgClientLetter">
                                                                </dxe:ASPxImage>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <dxe:ASPxImage runat="server" Height="75px" Width="75px" ID="Timg" ClientInstanceName="imgletter"
                                                    ClientVisible="False">
                                                    <EmptyImage Height="75px" Width="75px" Url="~/Images/noimage.gif">
                                                    </EmptyImage>
                                                </dxe:ASPxImage>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <TSPControls:CustomASPxCheckBox ID="ChbTCheckFileNo" runat="server" ClientInstanceName="chbtc"
                                                    Text="داری پروانه اشتغال به کار می باشم.">
                                                    <ClientSideEvents CheckedChanged="function(s, e) {
	if(chbtc.GetChecked()==true)
	{
		FileNo.SetVisible(true);
		lblFileNo.SetVisible(true);
	}
	else
	{
		FileNo.SetVisible(false);
		lblFileNo.SetVisible(false);
	}
}" />
                                                </TSPControls:CustomASPxCheckBox>
                                            </div>
                                            <div class="col-md-3">
                                                <dxcp:ASPxLabel runat="server" ID="lblTFileNo" ClientInstanceName="lblFileNo" Text="شماره پروانه"></dxcp:ASPxLabel>

                                            </div>
                                            <div class="col-md-3">
                                                <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtTFileNo" Style="direction: ltr"
                                                    ClientInstanceName="FileNo" ClientVisible="False">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" EnableCustomValidation="True"
                                                        ErrorText="شماره پروانه را وارد نمایید">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField ErrorText="شماره پروانه را وارد نمایید"></RequiredField>
                                                        <RegularExpression ErrorText="شماره را با فرمت *****-***-** وارد نمایید" ValidationExpression="^\d{2}-\d{3}-\d{3,5}"></RegularExpression>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
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
                                            </div>
                                        </div>
                                    </dxp:PanelContent>
                                </PanelCollection>
                            </dxp:ASPxPanel>
                        </div>

                    </dxp:PanelContent>
                </PanelCollection>
                <HeaderTemplate>
                    <table width="100%">
                        <tr>
                            <td align="right" style="width: 20%; height: 30px" valign="middle">
                                <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Text="مشخصات فردی" Font-Bold="true">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="left" style="width: 80%; height: 30px" valign="middle">
                                <TSPControls:CustomAspxButton IsMenuButton="true" AutoPostBack="false" ID="btnHelp" runat="server" CausesValidation="False"
                                    EnableTheming="False" EnableViewState="False"
                                    Text="" ToolTip="راهنما" UseSubmitBehavior="False">
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/Help.png" Width="25px">
                                    </Image>
                                    <ClientSideEvents Click="function(s,e){ ShowHelpWindow(HiddenHelp.Get('HelpAddress')); }"></ClientSideEvents>
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </table>
                </HeaderTemplate>
            </TSPControls:CustomASPxRoundPanel>
            <div class=" Item-center">
                <TSPControls:CustomAspxButton CssClass="ButtonMenue" ID="btnNext" runat="server" EnableTheming="False"
                    EnableViewState="False" OnClick="btnNext_Click" Text="تایید و ادامه" ToolTip="تایید و ادامه"
                    UseSubmitBehavior="False">
                    <ClientSideEvents Click="function(s, e) {

if(PDateValidatorBirthDate()==false)
{
e.processOnServer=false;
}

if(CheckCharacterEncoding(txtNameEng.GetText())==false)
 {
txtNameEng.SetIsValid(false);
txtNameEng.SetErrorText('حروف وارد شده نامعتبر است');
	e.processOnServer=false;
}
if(CheckCharacterEncoding(txtLastNameEng.GetText())==false)
 {
txtLastNameEng.SetIsValid(false);
txtLastNameEng.SetErrorText('حروف وارد شده نامعتبر است');
	e.processOnServer=false;
}
if (ChkBKardani.GetChecked()== true)
{
    if(HDFlpResident.Get('Kardani')!=1)
    {
    lblKardani.SetVisible(true);
    e.processOnServer=false;
    }
}
if(flpme.Get('name')!=1)
{
lblValImageMe.SetVisible(true);

e.processOnServer=false;
}

if(HDFlpResident.Get('name')!=1)
{
lblRes.SetVisible(true);
e.processOnServer=false;
}

if (CHB.GetChecked()== true)
{
    if(flpletter.Get('name')!=1)
    {
    lblValImageLetter.SetVisible(true);
    e.processOnServer=false;
    }
}

if(flpmeidno.Get('name')!=1)
{
lblValIdNo.SetVisible(true);
e.processOnServer=false;
}
if(flpmessn.Get('SSNFront')!=1)
{
lblSSN.SetVisible(true);
e.processOnServer=false;
}
if(flpmessn.Get('SSNBack')!=1)
{
lblSSNBack.SetVisible(true);
e.processOnServer=false;
}
//if(flpsol.GetVisible() && flpSold.Get('name')!=1)
//{
//lblSolFront.SetVisible(true);
//e.processOnServer=false;
//}
}" />

                </TSPControls:CustomAspxButton>
                <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="پاک کردن فرم"
                    CausesValidation="False" AutoPostBack="False" ID="btnMeRefresh" UseSubmitBehavior="False">
                    <ClientSideEvents Click="function(s, e) {
 e.processOnServer= false;
 if(confirm('آیا مطمئن به پاک کردن اطلاعات فرم هستید؟'))
	SetEmpty();
}" />
                </TSPControls:CustomAspxButton>                       
            </div>
            <asp:HiddenField ID="MemberId" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
            <asp:ObjectDataSource ID="ODBSex" runat="server" TypeName="TSP.DataManager.SexManager"
                SelectMethod="GetData" CacheDuration="30"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ODBSo" runat="server" TypeName="TSP.DataManager.SoldierManager"
                SelectMethod="GetData" CacheDuration="30"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ODBMar" runat="server" TypeName="TSP.DataManager.MaritalStatusManager"
                SelectMethod="GetData" CacheDuration="30"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ODBReli" runat="server" TypeName="TSP.DataManager.ReligionManager"
                SelectMethod="GetData" CacheDuration="30"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="OdbProvince" runat="server" TypeName="TSP.DataManager.ProvinceManager"
                SelectMethod="GetData" CacheDuration="30" FilterExpression="NezamCode<>{0}">
                <FilterParameters>
                    <asp:Parameter Name="newparameter" />
                </FilterParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="OdbCity" runat="server" TypeName="TSP.DataManager.CityManager"
                SelectMethod="GetData" CacheDuration="30"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="OdbAgent" runat="server" TypeName="TSP.DataManager.AccountingAgentManager"
                SelectMethod="GetData"></asp:ObjectDataSource>
            <dxhf:ASPxHiddenField ID="HDFlpMember" runat="server" ClientInstanceName="flpme">
            </dxhf:ASPxHiddenField>
            <dxhf:ASPxHiddenField ID="HDFlpLetter" runat="server" ClientInstanceName="flpletter">
            </dxhf:ASPxHiddenField>
            <dxhf:ASPxHiddenField ID="HDFlpSign" runat="server" ClientInstanceName="flpmesign">
            </dxhf:ASPxHiddenField>
            <dxhf:ASPxHiddenField ID="HDFlpIdNo" runat="server" ClientInstanceName="flpmeidno">
            </dxhf:ASPxHiddenField>
            <dxhf:ASPxHiddenField ID="HDFlpSSN" runat="server" ClientInstanceName="flpmessn">
            </dxhf:ASPxHiddenField>
            <dxhf:ASPxHiddenField ID="HDFlpSold" runat="server" ClientInstanceName="flpSold">
            </dxhf:ASPxHiddenField>
            <dxhf:ASPxHiddenField ID="HiddenHelp" runat="server" ClientInstanceName="HiddenHelp">
            </dxhf:ASPxHiddenField>
            <dxhf:ASPxHiddenField ID="HDFlpResident" runat="server" ClientInstanceName="HDFlpResident">
            </dxhf:ASPxHiddenField>
        </ContentTemplate>
    </asp:UpdatePanel> 
</asp:Content>
