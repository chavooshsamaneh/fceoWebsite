<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AddUpGradePoint.aspx.cs" Inherits="Employee_Document_AddUpGradePoint"
    Title="مشخصات امتیازات ارتقاء پایه" %>

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
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript">
        function SetTaskOrderError(result) {
            //alert(result);
            if (result != null) {

                document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
                document.getElementById("<%=DivReport.ClientID%>").style.display = 'block';
                document.getElementById('<%=LabelWarning.ClientID%>').innerText = result;
            }

        }

        function SetDivVisible() {
            document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'hidden';
            document.getElementById("<%=DivReport.ClientID%>").style.display = 'none';
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#"><span style="color: #000000">ب</span>ستن</a>]
            </div>

            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>



                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                            cellpadding="0">
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            CausesValidation="False" ID="BtnNew" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="BtnNew_Click">
                                            <ClientSideEvents Click="function(s, e) {

}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            Width="25px" ID="btnSave" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnSave_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBack_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/Back.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelUpGradePoint" HeaderText="مشاهده" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <fieldset runat="server" id="RoundPanelUpGradePoint1">
                            <legend class="fieldset-legend" dir="rtl"><b>اطلاعات پایه</b>
                            </legend>

                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td style="vertical-align: top; width: 15%" align="right">
                                            <dxe:ASPxLabel runat="server" Text="تمدید/ارتقاء " ID="ASPxLabel8" __designer:wfdid="w28">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="vertical-align: top; width: 35%" dir="ltr" align="right">
                                            <TSPControls:CustomAspxComboBox runat="server" ValueType="System.String"
                                                ID="CmbStatus"
                                                __designer:wfdid="w29">
                                                <Items>
                                                    <dxe:ListEditItem Text="ارتقاء" Value="1"></dxe:ListEditItem>
                                                    <dxe:ListEditItem Text="تمدید" Value="0"></dxe:ListEditItem>
                                                </Items>
                                                <ValidationSettings>
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                        <td style="vertical-align: top; width: 15%" align="right"></td>
                                        <td style="vertical-align: top; width: 35%" dir="ltr" align="right"></td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="برای ارتقاء نیاز به آزمون " ID="ASPxLabel2">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="vertical-align: top" dir="ltr" align="right">
                                            <TSPControls:CustomAspxComboBox runat="server" ValueType="System.String" AutoPostBack="True"
                                                ID="cmbIsTestNeed"
                                                 OnSelectedIndexChanged="cmbIsTestNeed_SelectedIndexChanged">
                                                <Items>
                                                    <dxe:ListEditItem Text="نیاز به آزمون دارد" Value="1"></dxe:ListEditItem>
                                                    <dxe:ListEditItem Text="نیاز به آزمون ندارد" Value="0"></dxe:ListEditItem>
                                                </Items>
                                                <ValidationSettings>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                        <td style="vertical-align: top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="ارتقاء پایه" ID="ASPxLabel4" >
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="vertical-align: top" dir="ltr" align="right">
                                            <TSPControls:CustomAspxComboBox runat="server" ValueType="System.String" DataSourceID="ObjdsUpGrade"
                                                TextField="UpGrdName" ValueField="UpGrdId" AutoPostBack="True"
                                                ID="cmbUpGrade"
                                               OnSelectedIndexChanged="cmbUpGrade_SelectedIndexChanged">
                                                <ValidationSettings>
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="رشته" ID="ASPxLabel1">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="vertical-align: top" dir="ltr" align="right">
                                            <%--      <TSPControls:CustomAspxComboBox runat="server" ValueType="System.String" DataSourceID="ObjdsMajor"
                                                TextField="MjName" ValueField="MjId" AutoPostBack="True"
                                                ID="CmbMajor"
                                                __designer:wfdid="w35" OnSelectedIndexChanged="CmbMajor_SelectedIndexChanged">
                                                <ValidationSettings>
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>  <asp:ObjectDataSource ID="ObjdsMajor" runat="server" DeleteMethod="Delete" InsertMethod="Insert"
                SelectMethod="GetData" TypeName="TSP.DataManager.MajorManager" UpdateMethod="Update"></asp:ObjectDataSource>--%>
                                              <TSPControls:CustomAspxComboBox runat="server" ValueType="System.String" DataSourceID="ODBParentMajor"
                                                TextField="MjName" ValueField="MjId" AutoPostBack="True"
                                                ID="CmbMajor"  ClientInstanceName="CmbMajor" OnSelectedIndexChanged="CmbMajor_SelectedIndexChanged">
                                                <ValidationSettings>
                                                    
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>                                           
                                            <asp:ObjectDataSource ID="ODBParentMajor" runat="server" SelectMethod="FindMjParents"
                                                TypeName="TSP.DataManager.MajorManager"></asp:ObjectDataSource>
                                        </td>
                                        <td style="vertical-align: top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="صلاحیت" ID="ASPxLabel3" >
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="vertical-align: top" dir="ltr" align="right">
                                            <TSPControls:CustomAspxComboBox runat="server" ValueType="System.String" DataSourceID="ObjdsAcceptGrade"
                                                TextField="ResName" ValueField="ResId"
                                                ID="cmbResponsibility">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">                                                   
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                    <RequiredField IsRequired="True" ErrorText="فیلد صلاحیت را انتخاب نمایید."></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>
                                            <asp:ObjectDataSource ID="ObjdsAcceptGrade" runat="server" SelectMethod="SelectDocAcceptedGradeByMajorParent"
                                                TypeName="TSP.DataManager.DocAcceptedGradeManager" OldValuesParameterFormatString="original_{0}">
                                                <SelectParameters>
                                                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="MjParentId"></asp:Parameter>
                                                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="GrdId"></asp:Parameter>
                                                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="ResId"></asp:Parameter>
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="حداقل امتیاز لازم از دوره" ID="lblMinPointPeriod"
                                                __designer:wfdid="w38">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="vertical-align: top" align="right">
                                            <TSPControls:CustomTextBox runat="server"
                                                ID="txtMinPointPeriod" __designer:wfdid="w39">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                    <RegularExpression ErrorText="امتیاز را با فرمت صحیح وارد نمایید." ValidationExpression="\d*"></RegularExpression>
                                                    <RequiredField IsRequired="True" ErrorText="حداقل امتیاز لازم از دوره را وارد نمایید."></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td style="vertical-align: top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="حداقل امتیاز لازم از سمینار" ID="lblMinPointSeminar"
                                                __designer:wfdid="w40">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="vertical-align: top" align="right">
                                            <TSPControls:CustomTextBox runat="server"
                                                ID="txtMinPointSeminar" __designer:wfdid="w41">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                    <RegularExpression ErrorText="امتیاز را با فرمت صحیح وارد نمایید." ValidationExpression="\d*"></RegularExpression>
                                                    <RequiredField IsRequired="True" ErrorText="حداقل امتیاز لازم از سمینار را وارد نمایید."></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; height: 37px" align="right">
                                            <dxe:ASPxLabel runat="server" Text="کل امتیاز مورد نیاز از دوره و سمینار"
                                                ID="lblTotalPoint" __designer:wfdid="w42">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="vertical-align: top; height: 37px" align="right">
                                            <TSPControls:CustomTextBox runat="server"
                                                ID="txtTotalPoint" __designer:wfdid="w43">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                    <RegularExpression ErrorText="امتیاز را با فرمت صحیح وارد نمایید." ValidationExpression="\d*"></RegularExpression>
                                                    <RequiredField IsRequired="True" ErrorText="کل امتیاز مورد نیاز از دوره و سمینار  را وارد نمایید."></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td style="vertical-align: top; height: 37px" align="right">
                                            <dxe:ASPxLabel runat="server" Text="تعداد دوره های مورد نیاز" ID="lblMinPeriodNeed"
                                                __designer:wfdid="w44">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="vertical-align: top; height: 37px" align="right">
                                            <TSPControls:CustomTextBox runat="server"
                                                ID="txtMinPeriodNeed" __designer:wfdid="w45">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                    <RegularExpression ErrorText="تعداد دوره ها را با فرمت صحیح وارد نمایید." ValidationExpression="\d*"></RegularExpression>
                                                    <RequiredField IsRequired="True" ErrorText="تعداد دوره های مورد نیاز را وارد نمایید."></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="میزان سابقه کار حرفه ای لازم(سال)"
                                                ID="ASPxLabel5" __designer:wfdid="w46">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="vertical-align: top" align="right">
                                            <TSPControls:CustomTextBox runat="server"
                                                ID="txtJobDuration" __designer:wfdid="w47">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                    <RegularExpression ErrorText="میزان سابقه کار را با فرمت صحیح وارد نمایید." ValidationExpression="\d*"></RegularExpression>
                                                    <RequiredField IsRequired="True" ErrorText="سابقه کار حرفه ای را وارد نمایید."></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td style="vertical-align: top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="کل امتیاز مورد نیاز" ID="ASPxLabel7"
                                                __designer:wfdid="w48">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="vertical-align: top" align="right">
                                            <TSPControls:CustomTextBox runat="server"
                                                ID="txtTotalPointNeed" __designer:wfdid="w49">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                    <RegularExpression ErrorText="امتیاز را با فرمت صحیح وارد نمایید." ValidationExpression="\d*"></RegularExpression>
                                                    <RequiredField IsRequired="True" ErrorText="کل امتیاز مورد نیاز را وارد نمایید."></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top" dir="ltr" align="right">
                                            <dxe:ASPxLabel runat="server" Text="توضیحات" Width="98px" ID="ASPxLabel6" __designer:wfdid="w50">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="vertical-align: top" align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="37px"
                                                ID="txtDescription" __designer:wfdid="w51">
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
                                </tbody>
                            </table>
                        </fieldset>
                        <br />
                        <fieldset runat="server" id="RoundPanelCourse">
                            <legend class="fieldset-legend" dir="rtl"><b>دروس و سمینار</b>
                            </legend>

                            <table runat="server" id="tableCourse" width="100%">
                                <tr runat="server">
                                    <td runat="server" valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="درس" ID="ASPxLabel9" >
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" style="vertical-align: top" dir="ltr" align="right" colspan="3">
                                        <TSPControls:CustomAspxComboBox runat="server" ValueType="System.String" DataSourceID="ObjdsCourse"
                                            TextField="CrsFullName" ValueField="CrsId"
                                            ID="CmbCourse"
                                           >
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="ValidCourse">
                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="True" ErrorText="درس را انتخاب نمایید."></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                                    <asp:ObjectDataSource ID="ObjdsCourse" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.CourseManager"></asp:ObjectDataSource>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server" valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel10" >
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" style="vertical-align: top" align="right" colspan="3">
                                        <TSPControls:CustomASPXMemo runat="server"
                                            ID="txtCourseDescription">
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
                                <tr runat="server">
                                    <td runat="server" style="vertical-align: top" align="center" colspan="4">
                                        <TSPControls:CustomAspxButton runat="server" Text="اضافه" ValidationGroup="ValidCourse"
                                            ID="btnAdd"
                                            OnClick="btnAdd_Click">
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server" style="vertical-align: top" align="center" colspan="4">
                                        <TSPControls:CustomAspxDevGridView runat="server" ClientInstanceName="grid"
                                            KeyFieldName="Id" AutoGenerateColumns="False" RightToLeft="True"
                                            ID="GridViewCourse" EnableViewState="False"  OnRowValidating="GridViewCourse_RowValidating"
                                            OnRowDeleting="GridViewCourse_RowDeleting">
                                            <ClientSideEvents RowDblClick="function(s, e) {
}"
                                                EndCallback="function(s, e) {
if(grid.cpErrorShow=='true')
{
	SetTaskOrderError(grid.cpErrorMsg);
	grid.cpErrorShow='false';
}
}"></ClientSideEvents>
                                            <Columns>
                                                <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="0" ShowDeleteButton="true">
                                                </dxwgv:GridViewCommandColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="Id" Visible="False" VisibleIndex="0">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="TypeName" Caption="دوره/سمینار" VisibleIndex="1">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="CrsName" Caption="عنوان دوره/سمینار" VisibleIndex="0">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="Description" Caption="توضیحات" VisibleIndex="2">
                                                </dxwgv:GridViewDataTextColumn>
                                            </Columns>
                                            <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="NextColumn" ConfirmDelete="True"></SettingsBehavior>
                                         
                                            <SettingsEditing Mode="PopupEditForm" PopupEditFormModal="True" EditFormColumnCount="1">
                                            </SettingsEditing>
                                            <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowGroupPanel="True"></Settings>                                                                                   
                                        </TSPControls:CustomAspxDevGridView>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>

                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            ID="btnNew2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td dir="ltr">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            Width="25px" ID="btnSave2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnSave_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            ID="btnBack2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnBack_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/Back.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldUpGradePoint">
            </dxhf:ASPxHiddenField>
            <asp:ObjectDataSource ID="ObjdsUpGrade" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.DocAcceptedUpGradeManager"></asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
        BackgroundCssClass="modalProgressGreyBackground" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                    <img src="../../Image/indicator.gif" align="middle" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
</asp:Content>
