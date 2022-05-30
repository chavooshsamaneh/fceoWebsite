<%@ Page Title="درخواست درج صلاحیت جدید پروانه اشتغال-ثبت آزمون" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="WizardQualificationExam.aspx.cs" Inherits="Members_Documents_WizardQualificationExam" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script language="javascript" type="text/javascript">
                function btnAdd_Click() {
                    imgEndUploadImgClientflpConfAttach.SetVisible(false);
                    hpConfAttach.SetVisible(false);
                    hpConfAttach.SetNavigateUrl('');
                    HeyperLinkPeriodImg.SetVisible(false);
                    HeyperLinkPeriodImg.SetNavigateUrl('');
                    GridViewExam.PerformCallback('Add');
                }
                function ClearExam() {
                    cmbMajor.SetSelectedIndex(-1);
                    cmbTestType.SetSelectedIndex(-1);
                    ComboTestCondition.SetSelectedIndex(-1);                    
                    txtUserCode.SetText("");
                    txtEntrantCode.SetText("");
                    txtPoint.SetText("");
                    HiddenFieldDocMemberFile.Set('name', 0);
                    HiddenFieldDocMemberFile.Set('PeriodImg', 0);
                }

                function ShowMessage(Message) {
                    document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
                    document.getElementById("<%=DivReport.ClientID%>").style.display = 'inline';
                    document.getElementById('<%=LabelWarning.ClientID%>').innerHTML = Message;
                }

                function SetControlValues() {

                    GridTestCondition.GetRowValues(GridTestCondition.GetFocusedRowIndex(), 'TCondId;Title;MjName;ExpireDate', SetValue);
                    PanelPeriodImg.SetVisible(false);
                    lblPeriod.SetVisible(false);
                }
                function SetValue(values) {
                    HiddenFieldExam.Set('TCondId', values[0]);
                    txtExamDate.SetText(values[1]);
                    cmbTestType.ClearItems();
                    cmbTestType.PerformCallback(values[0]);
                }
            </script>
            <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
                CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
                FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
                WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
            </pdc:PersianDateScriptManager>
            <div align="right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomAspxMenuHorizontal ID="MenuSteps" runat="server">
                <Items>
                    <dxm:MenuItem Text="سوگند نامه" Name="Oath">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Exams" Text="آزمون ها" Selected="true">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Kardan" Text="استعلام ها">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="JobConfirm" Text="تاییدیه سابق کاری">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Summary" Text="خلاصه اطلاعات">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="End" Text="ثبت نهایی">
                    </dxm:MenuItem>
                </Items>
            </TSPControls:CustomAspxMenuHorizontal>

            <p>

                <ul class="HelpUL">
                    <li>ثبت اطلاعات آزمونی که در آن شرکت و <u>موفق شده اید</u> الزامی می باشد.<a style="color: darkviolet">(در صورتی که حوزه امتحانی شما غیر از استان فارس می باشد، در قسمت مربوط به آزمون، علاوه بر آپلود کردن پرینت قبولی، نامه تائیدیه قبولی در آزمون از اداره کل راه و شهرسازی آن استان را  آپلود نمایید.(هر دو تصویر در یک فایل تصویری اسکن و آپلود گردد.))</a> </li>
                    <li>بارگزاری تصویر واضح کارنامه قبولی برای هر رشته و زمینه مربوط به آن الزامی می باشد</li>
                    <li>نمره هر یک از زمینه های آزمونی که در آن شرکت و موفق شده اید راوارد و سپس دکمه اضافه
                                        به لیست را کلیک نمایید تا به لیست اضافه گردد.برای هر یک از زمینه های آزمون این کار
                                        را تکرار نمایید. </li>
                    <li>در صورت وارد نکردن نمرات کلیه زمینه ها در هر آزمون، نمره زمینه های وارد نشده صفر در
                                        نظر گرفته می شود. </li>
                    <li>در صورتی که متقاضی درج صلاحیت اجرا می باشید، علاوه بر تصویر کارنامه آزمون، تصویر گواهینامه دوره آموزشی ورود به حرفه اجرا نیز الزامی می باشد.</li>
                </ul>

            </p>

            <TSPControls:CustomASPxRoundPanel ID="RoundPanelExam" HeaderText="آزمون ها" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table width="100%">
                            <tbody>
                                <tr>
                                </tr>
                                <tr>
                                    <td>
                                        <dxe:ASPxLabel runat="server" Text="درخواست براساس کدام مورد می باشد*" ID="lblResRequestType" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td colspan="3">
                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                            TextField="MjName" ID="combolblResRequestType" ClientInstanceName="cmbResType" AutoPostBack="True"
                                            DataSourceID="ObjdsMajor" ValueType="System.String" ValueField="MjParentId"
                                            OnSelectedIndexChanged="cmbMajor_SelectedIndexChanged"
                                            RightToLeft="True">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="validationGroupExam">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="علت درخواست را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>

                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right" rowspan="1" width="15%">
                                        <dxe:ASPxLabel runat="server" Text="رشته*" ID="ASPxLabel8" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" width="35%">
                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                            TextField="MjName" ID="cmbMajor" ClientInstanceName="cmbMajor" AutoPostBack="True"
                                            DataSourceID="ObjdsMajor" ValueType="System.String" ValueField="MjParentId"
                                            OnSelectedIndexChanged="cmbMajor_SelectedIndexChanged"
                                            RightToLeft="True">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="validationGroupExam">
                                                <RequiredField IsRequired="True" ErrorText="رشته را انتخاب نمایید نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>

                                        </TSPControls:CustomAspxComboBox>

                                        <asp:ObjectDataSource ID="ObjdsMajor" runat="server" SelectMethod="SelectActiveLicenceExceptKardani"
                                            TypeName="TSP.DataManager.MemberLicenceManager" OldValuesParameterFormatString="original_{0}">
                                            <SelectParameters>
                                                <asp:Parameter DbType="Int32" DefaultValue="-2" Name="MeId" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>


                                    </td>
                                    <td valign="top" align="right" width="15%">
                                        <dxe:ASPxLabel runat="server" Text="آزمون*" ID="ASPxLabel9" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" width="35%">
                                        <TSPControls:CustomAspxComboBox runat="server" ValueType="System.String" DataSourceID="ObjdsTestCondition" AutoPostBack="true" OnSelectedIndexChanged="ComboTestCondition_SelectedIndexChanged"
                                            TextField="Title" ValueField="TCondId" Width="100%"
                                            ID="ComboTestCondition" ClientInstanceName="ComboTestCondition">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="ValidationExam">
                                                <RequiredField ErrorText="آزمون را انتخاب نمایید" IsRequired="true" />
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="کد کاربری" Width="100%" ID="ASPxLabel6">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" ID="txtUserCode" ClientInstanceName="txtUserCode"
                                            Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="False" ErrorText="کد کاربری خود در آزمون را وارد نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="شماره داوطلبی" ID="ASPxLabel3" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" ID="txtEntrantCode" ClientInstanceName="txtEntrantCode"
                                            Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="False" ErrorText="شماره داوطلبی خود در آزمون را وارد نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="زمینه آزمون*" ID="ASPxLabel7" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                            TextField="TTypeName" ID="cmbTestType" ValueType="System.String" ValueField="TCondDId"
                                            ClientInstanceName="cmbTestType" OnSelectedIndexChanged="cmbTestType_SelectedIndexChanged"
                                            RightToLeft="True" DataSourceID="ObjectDataSourceTestType" AutoPostBack="true">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="زمینه آزمون را وارد نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                        <asp:ObjectDataSource ID="ObjectDataSourceTestType" runat="server" SelectMethod="SelectByTestConditionForExam"
                                            TypeName="TSP.DataManager.DocTestConditionDetailManager" OldValuesParameterFormatString="original_{0}">
                                            <SelectParameters>
                                                <asp:Parameter DefaultValue="-1" Name="TCondId" Type="Int32" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="نمره آزمون*" ID="ASPxLabel4" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" ID="txtPoint" ClientInstanceName="txtPoint"
                                            Width="100%">
                                            <ValidationSettings Display="Dynamic" ValidationGroup="validationGroupExam" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="نمره خود در آزمون را وارد نمایید"></RequiredField>
                                                <RegularExpression ErrorText="نمره قبولی را با فرمت صحیح وارد نمایید" ValidationExpression="\d*"></RegularExpression>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="پایه" ID="ASPxLabel2" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                            TextField="GrdName" ID="cmbGrade" ClientEnabled="false" ClientInstanceName="cmbGrade"
                                            DataSourceID="ObjdsGrade" ValueType="System.String" ValueField="GrdId"
                                            RightToLeft="True">
                                            <ValidationSettings>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                        <asp:ObjectDataSource ID="ObjdsGrade" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.GradeManager"></asp:ObjectDataSource>
                                    </td>
                                    <td valign="top" align="right"></td>
                                    <td valign="top" align="right"></td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="تصویر کارنامه قبولی*" ID="Label50"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl ID="flpConfAttach" runat="server" ClientInstanceName="flpi"
                                                            UploadWhenFileChoosed="true" OnFileUploadComplete="flpConfAttach_FileUploadComplete">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid){
	imgEndUploadImgClientflpConfAttach.SetVisible(true);
  	 HiddenFieldDocMemberFile.Set('name',1);
	lblImageWarning.SetVisible(false);
	hpConfAttach.SetVisible(true);
	hpConfAttach.SetNavigateUrl('../../Image/DocMeFile/Exams/'+e.callbackData);
	}
	else{
	imgEndUploadImgClientflpConfAttach.SetVisible(false);
	lblImageWarning.SetVisible(true);
	hpConfAttach.SetVisible(false);
	hpConfAttach.SetNavigateUrl('');    
  	HiddenFieldDocMemberFile.Set('name',0);
	}
}" />
                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel ID="lblImageWarning" runat="server" ClientInstanceName="lblImageWarning"
                                                            ClientVisible="False" ForeColor="Red" Text="تصویر کارنامه قبولی راانتخاب نمایید">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage ID="imgEndUploadImgflpConfAttach" runat="server" ClientInstanceName="imgEndUploadImgClientflpConfAttach"
                                                            ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویرانتخاب شد">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxe:ASPxHyperLink ID="HpflpConfAttach" runat="server" ClientInstanceName="hpConfAttach"
                                            Target="_blank" Text="تصویر کارنامه قبولی">
                                        </dxe:ASPxHyperLink>
                                    </td>

                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="تصویر گواهینامه آموزشی*" ID="lblPeriod" ClientInstanceName="lblPeriod" ClientVisible="false"></dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxp:ASPxPanel runat="server" ID="PanelPeriodImg" ClientInstanceName="PanelPeriodImg" ClientVisible="false">
                                            <PanelCollection>
                                                <dxp:PanelContent>

                                                    <table>
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <TSPControls:CustomAspxUploadControl ID="UploadControlPeriodImgURL" runat="server" ClientInstanceName="UploadControlPeriodImgURL"
                                                                        UploadWhenFileChoosed="true" OnFileUploadComplete="UploadControlPeriodImgURL_FileUploadComplete">
                                                                        <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid){
	imgEndUploadPeriodImgURL.SetVisible(true);
  	 HiddenFieldDocMemberFile.Set('PeriodImg',1);
	lblImagePeriodWarning.SetVisible(false);
	HeyperLinkPeriodImg.SetVisible(true);
	HeyperLinkPeriodImg.SetNavigateUrl('../../Image/DocMeFile/ImplementPeriod/'+e.callbackData);
	}
	else{
	imgEndUploadPeriodImgURL.SetVisible(false);
	lblImagePeriodWarning.SetVisible(true);
	HeyperLinkPeriodImg.SetVisible(false);
	HeyperLinkPeriodImg.SetNavigateUrl('');    
  	HiddenFieldDocMemberFile.Set('PeriodImg',0);
	}
}" />
                                                                    </TSPControls:CustomAspxUploadControl>
                                                                    <dxe:ASPxLabel ID="lblImagePeriodWarning" runat="server" ClientInstanceName="lblImagePeriodWarning"
                                                                        ClientVisible="False" ForeColor="Red" Text="تصویر گواهینامه آموزشی راانتخاب نمایید">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxImage ID="imgEndUploadPeriodImgURL" runat="server" ClientInstanceName="imgEndUploadPeriodImgURL"
                                                                        ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویرانتخاب شد">
                                                                    </dxe:ASPxImage>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <dxe:ASPxHyperLink ID="HeyperLinkPeriodImg" runat="server" ClientInstanceName="HeyperLinkPeriodImg"
                                                        Target="_blank" Text="تصویر گواهینامه دوره آموزشی ورود به حرفه اجرا">
                                                    </dxe:ASPxHyperLink>
                                                </dxp:PanelContent>

                                            </PanelCollection>
                                        </dxp:ASPxPanel>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <br />
                        <div class="Item-center">
                            <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" ImagePosition="left" Text="اضافه به لیست"
                                ID="btnAddDetail" AutoPostBack="False"
                                UseSubmitBehavior="False" CausesValidation="true" ValidationGroup="validationGroupExam">
                                <ClientSideEvents Click="function(s, e) {                                                 
                                            if(HiddenFieldDocMemberFile.Get('name')==0)
                                            {                                            
	                                            lblImageWarning.SetVisible(true);
                                                 if(PanelPeriodImg.GetVisible()==true && HiddenFieldDocMemberFile.Get('PeriodImg')==0){                                            
	                                                lblImagePeriodWarning.SetVisible(true);  }
                                                    return;
                                                    }else{
                                                    if(PanelPeriodImg.GetVisible()==true && HiddenFieldDocMemberFile.Get('PeriodImg')==0){                                            
	                                                lblImagePeriodWarning.SetVisible(true);
                                                    return;
                                                    }

                                             }       
                                              if (ASPxClientEdit.ValidateGroup('validationGroupExam'))
                                               {
                                                   btnAdd_Click(); 
                                               }                         
                                               
                                            }" />
                            </TSPControls:CustomAspxButton>

                            <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="&nbsp;&nbsp;&nbsp;پاک کردن فرم"
                                CausesValidation="False" AutoPostBack="False" ID="btnRefresh" UseSubmitBehavior="False">
                                <ClientSideEvents Click="function(s, e) {
e.processOnServer= false;
 if(confirm('آیا مطمئن به پاک کردن اطلاعات فرم هستید؟'))
 {
	ClearExam();	
  }
}"></ClientSideEvents>
                            </TSPControls:CustomAspxButton>
                        </div>
                        <br />
                        <TSPControls:CustomAspxDevGridView2 runat="server" ID="GridViewExam"
                            KeyFieldName="Id" AutoGenerateColumns="False" ClientInstanceName="GridViewExam"
                            OnRowDeleting="GridViewTestCondition_RowDeleting" Width="100%" OnCustomCallback="GridViewExam_CustomCallback">
                            <Settings ShowHorizontalScrollBar="true"></Settings>
                            <ClientSideEvents EndCallback="function(s,e){
                                        if(s.cpSaveComplete=='1'){
                                         ClearExam();
                                         s.cpSaveComplete='0';
                                         }
                                        else if(s.cpMessage!='')
                                        {
                                        // ShowMessage(s.cpMessage);
                                        alert(s.cpMessage);
                                         s.cpMessage='';
                                        }
                                        }" />
                            <Columns>
                                <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption=" " ShowDeleteButton="true" Name="Delete"
                                    Width="25px">
                                </dxwgv:GridViewCommandColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="5" FieldName="Id" Caption="Id">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="MajorName" Caption="رشته"
                                    Name="Point">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="ExamTitle" Caption="آزمون"
                                    Name="Point">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="UserCode" Caption="کد کاربری"
                                    Name="Point">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="EntranceCode" Caption="شماره داوطلبی"
                                    Name="Point">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Point" Caption="نمره آزمون"
                                    Name="Point">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="GrdName" Caption="پایه"
                                    Name="GrdName">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="TTypeName" Width="150px"
                                    Caption="زمینه آزمون" Name="TTypeName">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataHyperLinkColumn VisibleIndex="4" PropertiesHyperLinkEdit-Text="تصویر کارنامه"
                                    FieldName="FileURL" Caption="تصویر کارنامه" Name="FileURL">
                                </dxwgv:GridViewDataHyperLinkColumn>
                                <dxwgv:GridViewDataHyperLinkColumn VisibleIndex="4" PropertiesHyperLinkEdit-Text="تصویر گواهینامه دوره آموزشی"
                                    FieldName="FileURL" Caption="تصویر گواهینامه دوره آموزشی" Name="FileURL" Width="250px">
                                </dxwgv:GridViewDataHyperLinkColumn>
                            </Columns>
                        </TSPControls:CustomAspxDevGridView2>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>

            <div class="Item-center">
              

                <TSPControls:CustomAspxButton CssClass="ButtonMenue" ID="btnNext" OnClick="btnNext_Click" runat="server" Text="تایید و ادامه"
                    ToolTip="تایید و ادامه" EnableViewState="False" EnableTheming="False" UseSubmitBehavior="False"
                    CausesValidation="False">
                </TSPControls:CustomAspxButton>
            </div>
            <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldDocMemberFile" ClientInstanceName="HiddenFieldDocMemberFile">
            </dxhf:ASPxHiddenField>
            <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldExam" ClientInstanceName="HiddenFieldExam">
            </dxhf:ASPxHiddenField>
            <asp:ObjectDataSource ID="ObjdsTestCondition" runat="server" SelectMethod="SelectByMajor"
                TypeName="TSP.DataManager.DocTestConditionManager" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:Parameter Type="Int32" DefaultValue="0" Name="Inactive"></asp:Parameter>
                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="MjId"></asp:Parameter>
                    <asp:Parameter Type="Boolean" DefaultValue="true" Name="IsSortedByExpDate"></asp:Parameter>
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


