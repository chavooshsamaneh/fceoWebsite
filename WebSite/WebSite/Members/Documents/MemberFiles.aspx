<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="MemberFiles.aspx.cs" Inherits="Members_Documents_MemberFiles"
    Title="مدیریت پروانه اشتغال به کار" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
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
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>




<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>


<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/UserControl/PhotoUploadHelp.ascx" TagName="PhotoUploadHelp" TagPrefix="UcUploadHelp" %>
<%@ Register Src="~/UserControl/DocumentChangeUserControl.ascx" TagName="DocumentChange"
    TagPrefix="UcDocumentChange" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../Script/jquery-1.11.2.min.js"></script>
    <script>
        $(function () {
            /*    window.setInterval(function () {
            // 'blink' class is toggled into 'P' tag between the interval of 500 ms
            $('#WarningText').toggleClass('BlinkText');
            }, 200);*/
        });
    </script>
    <div align="center">
        <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
            <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
            [<a class="closeLink" href="#"><span style="color: #000000"></span>بستن</a>]
        </div>
        <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
            Width="100%">
            <PanelCollection>
                <dxp:PanelContent>
                    <table>
                        <tbody>
                            <tr>

                                <td>

                                    <asp:LinkButton ID="btnEdit" CssClass="ButtonMenue" OnClick="btnEdit_Click" runat="server" OnClientClick="
                                                           	if (grid.GetFocusedRowIndex()&lt;0)
 {
   return false;                   
   alert(&quot;ردیفی انتخاب نشده است&quot;);}">ویرایش درخواست</asp:LinkButton>

                                </td>
                                <td>
                                    <asp:LinkButton ID="btnView" CssClass="ButtonMenue" OnClick="btnView_Click" runat="server" OnClientClick="
                                                           	if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 {
   return false;                   
   alert(&quot;ردیفی انتخاب نشده است&quot;);}">مشاهده درخواست</asp:LinkButton>


                                </td>
                                <td>
                                    <asp:LinkButton ID="btnDelete" CssClass="ButtonMenue" OnClick="btnDelete_Click" runat="server" OnClientClick="
                                                           	if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 {
   return false;                   
   alert(&quot;ردیفی انتخاب نشده است&quot;);}">لغو درخواست</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnSendNextStep" CssClass="ButtonMenue" runat="server" OnClientClick="
                                                           	if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 {
   return false;                   
  alert(&quot;ردیفی انتخاب نشده است&quot;);}
                                            else{
                                             txtDescription.SetText('');
	CallbackPanelWorkFlow.PerformCallback('');
	PopupWorkFlow.Show();
   return false;   
                                              }
                                            ">گردش کار</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnTracing" CssClass="ButtonMenue" OnClick="btnTracing_Click" runat="server" OnClientClick="
                                                           	if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 {
   return false;                   

  alert(&quot;ردیفی انتخاب نشده است&quot;);}">پیگیری گردش کار</asp:LinkButton>
                                </td>

                                <td>

                                    <asp:LinkButton ID="BtnNew" CssClass="ButtonMenue" OnClick="BtnNew_Click" runat="server">درخواست صدور</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnRevival2" CssClass="ButtonMenue" OnClick="btnRevival_Click" runat="server">درخواست تمدید</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnQualification2" CssClass="ButtonMenue" OnClick="btnQualification_Click" runat="server">درخواست درج صلاحیت جدید در پروانه</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnUpgrade2" CssClass="ButtonMenue" OnClick="btnUpgrade_ServerClick" runat="server">درخواست ارتقا پایه</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnprint" CssClass="ButtonMenue" runat="server" OnClientClick="
                 
  	window.open(&quot;../../Print.aspx&quot;);   return false;  ">چاپ لیست درخواست ها</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnExportExcel" CssClass="ButtonMenue" OnClick="btnExportExcel_Click" runat="server" OnClientClick="
                                                           	if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 {
   return false;                   

  alert(&quot;ردیفی انتخاب نشده است&quot;);}">خروجی Excel</asp:LinkButton>
                                </td>


                            </tr>
                        </tbody>
                    </table>
                </dxp:PanelContent>
            </PanelCollection>
        </TSPControls:CustomASPxRoundPanelMenu>
        <br />
        <table width="100%" align="center">
            <tr>
                <td width="2%" align="right">
                    <blink id="bkImgWarningMsg"><dxe:ASPxImage ID="ImgWarningMsg" ClientVisible="true" Width="25px" Height="25px" runat="server" ImageUrl="../../Images/Errors-64.png">
                                    </dxe:ASPxImage></blink>
                </td>
                <td width="98%" align="right">
                    <p id="WarningText" class="HelpUL" style="text-align: justify; font-weight: bold">
                        هشدار: در صورت بازگشت پرونده و قرار گرفتن در مرحله صدور که با علامت ستاره نمایش
                        داده شده است بعد از ذخیره کردن یا ویرایش کردن هر درخواستی باید دوباره به این صفحه
                        بازگشته و درخواست مورد نظر را از درون جدول پایین انتخاب و با کمک دکمه گردش کار به واحد
                        پروانه ارسال گردد در غیر اینصورت درخواست شما در مرحله ثبت متوقف شده و در واحد پروانه
                        امکان بررسی آن وجود نخواهد داشت
                    </p>
                    <%-- <asp:Label ID="lblWarningText" Font-Bold="true" ForeColor="DarkRed" runat="server"></asp:Label>--%>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <ul class="HelpUL">
                        <li>هشدار: مطابق آمار و مشاهدات بیشترین ایرادات پرونده پروانه اشتغال به کار و عضویت
                        اعضای سازمان مربوط به تصاویر اسکن شده و بارگذاری شده می باشد. بنابراین در اسکن و
                        بارگذاری تمامی تصاویر وسواس لازم را داشته باشید و مطابق دستورالعمل زیر اقدام نمایید
                        تصاویر باید از وضوح و کیفیت کافی برخوردار باشند و تمام متون و مهرها در فرم ها خوانا
                        باشند همچنین جهت آنها نیز درست باشند در غیر اینصورت درخواست شما باطل می شود
                        </li>

                        <li>جهت مشاهده تمامی پانوشت های مربوط به پرونده بعد از انتخاب پرونده از درون جدول دکمه
                        پیگیری گردش کار را انتخاب کنید. در این صورت می توانید تمامی سابقه گردش کار پرونده خود
                        و تمامی پانوشت های مربوط به پرونده خود را یکجا مشاهده کنید</li>
                    </ul>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <UcUploadHelp:PhotoUploadHelp ID="PhotoHelp" runat="server" />
                    <br />
                    <UcDocumentChange:DocumentChange ID="DocChange" runat="server" />
                </td>
            </tr>
        </table>
        <br />
        <TSPControls:CustomAspxDevGridView2 ID="GridViewMemberFile" runat="server" DataSourceID="ObjdsMemberFile"
            Width="100%" KeyFieldName="MfId" AutoGenerateColumns="False" ClientInstanceName="GridViewMemberFile"
            OnAutoFilterCellEditorInitialize="GridViewMemberFile_AutoFilterCellEditorInitialize"
            OnHtmlDataCellPrepared="GridViewMemberFile_HtmlDataCellPrepared" OnDetailRowExpandedChanged="GridViewMemberFile_DetailRowExpandedChanged"
            OnPageIndexChanged="GridViewMemberFile_PageIndexChanged">
             <SettingsText Title="لیست درخواست های پروانه اشتغال به کار عضو حقیقی" /> <Settings ShowTitlePanel="true" ></Settings>
            <Columns>
                <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="RequeterTypeName" Caption="ثبت کننده درخواست"
                    Width="100px">
                    <HeaderStyle Wrap="True"></HeaderStyle>
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataComboBoxColumn Caption="مرحله" FieldName="TaskId" Name="WFState"
                    VisibleIndex="0" Width="50px">
                    <DataItemTemplate>
                        <div align="center">
                            <dxe:ASPxImage ID="btnWFState" runat="server" Width="16px" Height="16px" ImageUrl='<%# Bind("WFImageURL") %>'>
                            </dxe:ASPxImage>
                        </div>
                    </DataItemTemplate>
                    <PropertiesComboBox DataSourceID="ObjdsWorkFlowTask" TextField="TaskName" ValueField="TaskId"
                        ValueType="System.String">
                    </PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="300px" FieldName="wfDescription"
                    Caption="توضیحات">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="SerialNo" Caption="شماره سریال">
                    <HeaderStyle Wrap="True"></HeaderStyle>
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="MFNoForMe" Caption="شماره پروانه">
                    <HeaderStyle Wrap="True"></HeaderStyle>
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="RegDate" Caption="تاریخ صدور">
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="ExpireDate" Caption="پایان اعتبار">
                    <HeaderStyle Wrap="True"></HeaderStyle>
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="MFType" Caption="وضعیت پروانه">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="LivertyDate"
                    Caption="تاریخ تحویل گواهینامه">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="InActives" Caption="وضعیت">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="Type" Visible="false" Name="Type">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewCommandColumn VisibleIndex="7" Width="50px" Caption=" " ShowClearFilterButton="true">
                </dxwgv:GridViewCommandColumn>
            </Columns>
            <Settings ShowHorizontalScrollBar="True"></Settings>
            <SettingsDetail ExportMode="All"></SettingsDetail>
        </TSPControls:CustomAspxDevGridView2>
        <br />

    
                            <fieldset width="100%">
                        <legend>راهنما</legend>

                        <ul class="HelpWorkflowTasksImages">
                            <li class="col-sm-4">
                                <ul>
                                    <li>
                                        <dxe:ASPxLabel ID="ASPxLabel36" runat="server" Text="صدور: فونت مشکی" ForeColor="Black">
                                        </dxe:ASPxLabel>
                                    </li>
                                    <li>
                                        <dxe:ASPxLabel ID="ASPxLabel37" runat="server" Text="تمدید: فونت سبز" ForeColor="Green">
                                        </dxe:ASPxLabel>
                                    </li>
                                    <li>
                                        <dxe:ASPxLabel ID="ASPxLabel43" runat="server" Text="ارتقاء پایه: فونت آبی تیره" ForeColor="DarkBlue">
                                        </dxe:ASPxLabel>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                        <ul class="HelpWorkflowTasksImages">
                            <li class="col-sm-4">
                                <ul>
                                    <li>
                                        <dxe:ASPxLabel ID="ASPxLabel11" runat="server" Text="درج صلاحیت جدید: فونت بنفش"
                                            ForeColor="DarkMagenta" Font-Bold="False">
                                        </dxe:ASPxLabel>
                                    </li>
                                    <li>
                                        <dxe:ASPxLabel ID="ASPxLabel12" runat="server" Text="المثنی: فونت صورتی" ForeColor="Magenta">
                                        </dxe:ASPxLabel>
                                    </li>
                                    <li>
                                        <dxe:ASPxLabel ID="ASPxLabel38" runat="server" Text="تغییرات: فونت قهوه ای" ForeColor="Brown"
                                            Font-Bold="False">
                                        </dxe:ASPxLabel>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                        <ul class="HelpWorkflowTasksImages">
                            <li class="col-sm-4">
                                <ul>
                                    <li>
                                            <dxe:ASPxLabel ID="ASPxLabel13" runat="server" Text="انتقال از دیگر استان ها: فونت طلایی"
                                                ForeColor="Gold" Font-Bold="False">
                                            </dxe:ASPxLabel>
                                    </li>
                                    <li>
                                        <dxe:ASPxLabel ID="ASPxLabel14" runat="server" Text="ابطال: فونت قرمز" ForeColor="Red"
                                            Font-Bold="False">
                                        </dxe:ASPxLabel>
                                    </li>
                                    <li>     <dxe:ASPxLabel ID="ASPxLabel6" runat="server" Text="درخواست درجریان: ردیف آبی" ForeColor="SteelBlue"
                                            Font-Bold="False">
                                        </dxe:ASPxLabel>  </li>
                                </ul>
                            </li>
                        </ul>
                        <ul class="HelpWorkflowTasksImages">
                            <li class="col-sm-4">
                                <ul>
                                    <asp:Repeater runat="server" ID="RepeaterWfHepPrint1">
                                        <ItemTemplate>
                                            <li>
                                                <asp:Image ID="Image2" Height="16px" Width="16px" runat="server" ImageUrl='<%# Eval("ImageURL") %> ' />
                                                <a><%# Eval("TaskName") %> </a>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <li></li>
                                </ul>
                            </li>
                            <li class="col-sm-4">
                                <ul>
                                    <li class="dropdown-header"></li>
                                    <asp:Repeater runat="server" ID="RepeaterWfHepPrint2">
                                        <ItemTemplate>
                                            <li>
                                                <asp:Image ID="Image2" Height="16px" Width="16px" runat="server" ImageUrl='<%# Eval("ImageURL") %> ' />
                                                <a><%# Eval("TaskName") %> </a>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <li></li>
                                </ul>
                            </li>
                            <li class="col-sm-4">
                                <ul>
                                    <li class="dropdown-header"></li>
                                    <asp:Repeater runat="server" ID="RepeaterWfHepPrint3">
                                        <ItemTemplate>
                                            <li>
                                                <asp:Image ID="Image2" Height="16px" Width="16px" runat="server" ImageUrl='<%# Eval("ImageURL") %> ' />
                                                <a><%# Eval("TaskName") %> </a>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <li></li>
                                </ul>
                            </li>
                        </ul>
                    </fieldset>
        <br />
        <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
            Width="100%">
            <PanelCollection>
                <dxp:PanelContent>
                    <table>
                        <tbody>
                            <tr>

                                <td>

                                    <asp:LinkButton ID="btnEdit2" CssClass="ButtonMenue" OnClick="btnEdit_Click" runat="server" OnClientClick="
                                                           	if (grid.GetFocusedRowIndex()&lt;0)
 {
   return false;                   
   alert(&quot;ردیفی انتخاب نشده است&quot;);}">ویرایش درخواست</asp:LinkButton>

                                </td>
                                <td>
                                    <asp:LinkButton ID="btnView2" CssClass="ButtonMenue" OnClick="btnView_Click" runat="server" OnClientClick="
                                                           	if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 {
   return false;                   
   alert(&quot;ردیفی انتخاب نشده است&quot;);}">مشاهده درخواست</asp:LinkButton>


                                </td>
                                <td>
                                    <asp:LinkButton ID="btnDelete2" CssClass="ButtonMenue" OnClick="btnDelete_Click" runat="server" OnClientClick="
                                                           	if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 {
   return false;                   
   alert(&quot;ردیفی انتخاب نشده است&quot;);}">لغو درخواست</asp:LinkButton>
                                </td>

                                <td>
                                    <asp:LinkButton ID="btnSendNextStep2" CssClass="ButtonMenue" runat="server" OnClientClick="
                                                           	if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 {
   return false;                   
  alert(&quot;ردیفی انتخاب نشده است&quot;);}
                                            else{
                                           txtDescription.SetText('');
	CallbackPanelWorkFlow.PerformCallback('');
	PopupWorkFlow.Show();
   return false;   
                                              }
                                            ">گردش کار</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnTracing2" CssClass="ButtonMenue" OnClick="btnTracing_Click" runat="server" OnClientClick="
                                                           	if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 {
   return false;                   

  alert(&quot;ردیفی انتخاب نشده است&quot;);}">پیگیری گردش کار</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnNew2" CssClass="ButtonMenue" OnClick="BtnNew_Click" runat="server">درخواست صدور</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnRevival" CssClass="ButtonMenue" OnClick="btnRevival_Click" runat="server">درخواست تمدید</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnQualification" CssClass="ButtonMenue" OnClick="btnQualification_Click" runat="server">درخواست درج صلاحیت جدید در پروانه</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnUpgrade" CssClass="ButtonMenue" OnClick="btnUpgrade_ServerClick" runat="server">درخواست ارتقا پایه</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnprint2" CssClass="ButtonMenue" runat="server" OnClientClick="
                 
  	window.open(&quot;../../Print.aspx&quot;);   return false;  ">چاپ لیست درخواست ها</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnExportExcel2" CssClass="ButtonMenue" OnClick="btnExportExcel_Click" runat="server" OnClientClick="
                                                           	if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 {
   return false;                   

  alert(&quot;ردیفی انتخاب نشده است&quot;);}">خروجی Excel</asp:LinkButton>

                                </td>

                            </tr>
                        </tbody>
                    </table>
                </dxp:PanelContent>
            </PanelCollection>
        </TSPControls:CustomASPxRoundPanelMenu>
        <dxwgv:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="GridViewMemberFile">
        </dxwgv:ASPxGridViewExporter>
        <asp:ObjectDataSource ID="ObjdsMemberFile" runat="server" TypeName="TSP.DataManager.DocMemberFileManager"
            SelectMethod="SelectDocumentMemberFileByMember" OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" TypeName="TSP.DataManager.WorkFlowTaskManager"
            SelectMethod="SelectByWorkCode">
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="WorkFlowCode" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <TSPControls:CustomASPxPopupControl ID="PopupWorkFlow" runat="server" Width="387px"
            ClientInstanceName="PopupWorkFlow"
            AllowDragging="True" CloseAction="CloseButton" HeaderText=""
            Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server">
                    <TSPControls:CustomAspxCallbackPanel runat="server" Width="100%"
                        ID="CallbackPanelWorkFlow" ClientInstanceName="CallbackPanelWorkFlow" OnCallback="CallbackPanelWorkFlow_Callback">
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                                <dxp:ASPxPanel runat="server" Width="100%" ID="PanelMain" ClientInstanceName="PanelMain">
                                    <PanelCollection>
                                        <dxp:PanelContent runat="server">
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td colspan="2">
                                                            <dxe:ASPxLabel runat="server" Text="ASPxLabel" Font-Size="X-Small" ID="lblError"
                                                                ForeColor="Red" Visible="False">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="right">
                                                            <dxe:ASPxLabel runat="server" Text="ارسال به مرحله" Font-Size="X-Small" Width="77px"
                                                                ID="lblSenBack">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td dir="ltr" valign="top" align="right">
                                                            <TSPControls:CustomAspxComboBox runat="server" Width="290px"
                                                                ID="cmbSendBackTask" ValueType="System.String">
                                                                <ValidationSettings>
                                                                    <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                    </ErrorImage>
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
                                                        <td valign="top" align="right">
                                                            <dxe:ASPxLabel runat="server" Text="توضیحات" Font-Size="X-Small" Width="56px" ID="ASPxLabel1">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td dir="rtl" valign="top" align="right">
                                                            <TSPControls:CustomASPXMemo runat="server" Height="71px" ID="txtDescription"
                                                                Width="100%" ClientInstanceName="txtDescription">
                                                                <ValidationSettings>
                                                                    <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                    </ErrorImage>
                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                    </ErrorFrameStyle>
                                                                </ValidationSettings>
                                                            </TSPControls:CustomASPXMemo>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td dir="ltr" valign="top" align="center" colspan="2">
                                                            <TSPControls:CustomAspxButton runat="server" Text="ارسال" Width="93px" ID="btnSendNextWorkStep"
                                                                AutoPostBack="False" UseSubmitBehavior="False" ClientInstanceName="btnSenNextStep">
                                                                <ClientSideEvents Click="function(s, e) {	
	CallbackPanelWorkFlow.PerformCallback('Send');
	GridViewMemberFile.PerformCallback('');
}"></ClientSideEvents>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </dxp:PanelContent>
                                    </PanelCollection>
                                </dxp:ASPxPanel>
                                <dxp:ASPxPanel runat="server" Height="100%" Width="100%" ID="PanelSaveSuccessfully"
                                    ClientInstanceName="PanelSaveSuccessfully">
                                    <PanelCollection>
                                        <dxp:PanelContent runat="server">
                                            <div align="center">
                                                <br />
                                                <dxe:ASPxLabel runat="server" Text="ذخیره با موفقیت انجام شد." Font-Size="X-Small"
                                                    ID="lblInstitueWarning" ForeColor="Red">
                                                </dxe:ASPxLabel>
                                                <br />
                                                <br />
                                                <TSPControls:CustomAspxButton runat="server" Text="خروج" Width="93px" ID="btnClose"
                                                    AutoPostBack="False" UseSubmitBehavior="False" ClientInstanceName="btnSenNextStep">
                                                    <ClientSideEvents Click="function(s, e) {	
	//CallbackPanelWorkFlow.PerformCallback('');
	PopupWorkFlow.Hide();
}"></ClientSideEvents>
                                                </TSPControls:CustomAspxButton>
                                            </div>
                                        </dxp:PanelContent>
                                    </PanelCollection>
                                </dxp:ASPxPanel>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </TSPControls:CustomAspxCallbackPanel>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
            <HeaderStyle>
                <Paddings PaddingTop="1px" PaddingRight="6px" PaddingLeft="10px"></Paddings>
            </HeaderStyle>
            <SizeGripImage Height="12px" Width="12px">
            </SizeGripImage>
            <CloseButtonImage Height="17px" Width="17px">
            </CloseButtonImage>
        </TSPControls:CustomASPxPopupControl>
        <dx:ASPxHiddenField ID="HiddenHelp" runat="server" ClientInstanceName="HiddenHelp">
        </dx:ASPxHiddenField>
    </div>
</asp:Content>
