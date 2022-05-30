<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="ProjectIngridientMajorsInsert.aspx.cs" Inherits="Employee_TechnicalServices_BaseInfo_ProjectIngridientMajorsInsert"
    Title="رشته مجاز عوامل پروِژه" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>

<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server"
                visible="false">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>



                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            CausesValidation="False" ID="btnNew" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnNew_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" Width="25px" ID="btnEdit" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            ID="btnSave" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnSave_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
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
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشاهده" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table dir="rtl" width="100%">
                            <tbody>
                                <tr>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="عامل پروژه" ID="Label24"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomAspxComboBox runat="server"
                                            TextField="Title" ID="ASPxComboBoxProjectIngridientType" DataSourceID="ObjectdatasourceProjectIngridient"
                                            ValueType="System.Int32" ValueField="ProjectIngridientTypeId"
                                            EnableIncrementalFiltering="True" OnSelectedIndexChanged="ASPxComboBoxProjectIngridientType_SelectedIndexChanged"
                                            ClientInstanceName="ComboBoxProjectIngridientType" AutoPostBack="True" RightToLeft="True">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField ErrorText="لطفاً عامل پروژه را انتخاب نمایید" IsRequired="True" />
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="نوع" ID="Label25"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomAspxComboBox runat="server"
                                            TextField="Title" ID="ASPxComboBoxType" DataSourceID="ObjectdatasourceObserversPlansType"
                                            ValueType="System.Int32" ValueField="ObserversPlansTypeId"
                                            EnableIncrementalFiltering="True" RightToLeft="True" ClientInstanceName="ComboBoxType">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField ErrorText="لطفاً نوع را انتخاب نمایید" IsRequired="True" />
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="رشته" ID="Label29"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomAspxComboBox runat="server"
                                            TextField="MjName" ID="ASPxComboBoxMajors" DataSourceID="ObjectdatasourceMajors"
                                            ValueType="System.Int32" ValueField="MjId"
                                            EnableIncrementalFiltering="True" RightToLeft="True" ClientInstanceName="ComboStructureSystem">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="لطفاً رشته را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>

                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="پایه ی مجاز" ID="Label1"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxComboBox runat="server"
                                            TextField="GrdName" ID="cmbGrad" DataSourceID="ObjdsGrade"
                                            ValueType="System.Int32" ValueField="GrdId"
                                            EnableIncrementalFiltering="True" RightToLeft="True" ClientInstanceName="cmbGrad">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField ErrorText="" />
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                        <asp:ObjectDataSource ID="ObjdsGrade" runat="server"
                                            SelectMethod="GetData" TypeName="TSP.DataManager.GradeManager" UpdateMethod="Update"></asp:ObjectDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="گروه ساختمانی" ID="Label30"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxComboBox runat="server"
                                            TextField="GroupName" ID="ASPxComboBoxStructureGroups" DataSourceID="ObjectdatasourceStructureGroup"
                                            ValueType="System.Int32" ValueField="GroupId"
                                            EnableIncrementalFiltering="True" RightToLeft="True" ClientInstanceName="ComboBoxStructureSkeleton">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField ErrorText="" />
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="تعداد طبقات" ID="Label31"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxStep">
                                            <MaskSettings Mask="&lt;0..10000000000&gt;"></MaskSettings>
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField ErrorText=""></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="حداقل متراژ" ID="Label2"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" ID="txtFoundationMin">
                                            <MaskSettings Mask="&lt;0..10000000000&gt;"></MaskSettings>
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField ErrorText=""></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="حداکثر متراژ" ID="Label3"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" ID="txtFoundationMax">
                                            <MaskSettings Mask="&lt;0..10000000000&gt;"></MaskSettings>
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField ErrorText=""></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="درصد متراژ متراژ" ID="lblPercent"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" ID="txtPercent">
                                            <MaskSettings Mask="&lt;0..10000000000&gt;"></MaskSettings>
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField ErrorText=""></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="گروه ناظران" ID="lblObserverGroup"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxComboBox runat="server"
                                            TextField="ObserverGroupName" ID="cmbObserverGroup"
                                            ValueType="System.Int32" ValueField="ObserverGroup"
                                            EnableIncrementalFiltering="True" RightToLeft="True" ClientInstanceName="ObserverGroup">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <Items>
                                                <dxe:ListEditItem Text="گروه 1" Value="1" />
                                                <dxe:ListEditItem Text="گروه 2" Value="2" />
                                                <dxe:ListEditItem Text="گروه 3" Value="3" />
                                                <dxe:ListEditItem Text="گروه 4" Value="4" />
                                                <dxe:ListEditItem Text="گروه 5" Value="5" />
                                            </Items>
                                        </TSPControls:CustomAspxComboBox>

                                    </td>


                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="نوع اسکلت" ID="Label4"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxComboBox runat="server"
                                            TextField="Title" ID="ComboBoxStructureSkeleton" DataSourceID="ObjectdatasourceStructureSkeleton"
                                            ValueType="System.Int32" ValueField="StructureSkeletonId"
                                            EnableIncrementalFiltering="True" RightToLeft="True" ClientInstanceName="ComboBoxStructureSkeleton">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField ErrorText="" />
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                        <asp:ObjectDataSource ID="ObjectdatasourceStructureSkeleton" runat="server" SelectMethod="GetData"
                                            TypeName="TSP.DataManager.TechnicalServices.StructureSkeletonManager"></asp:ObjectDataSource>
                                    </td>
                                    <td></td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>

            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>



                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            CausesValidation="False" ID="btnNew2" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnNew_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" Width="25px" ID="btnEdit2" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            ID="btnSave2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnSave_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" ID="ASPxButton6" UseSubmitBehavior="False" EnableViewState="False"
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
        AssociatedUpdatePanelID="UpdatePanel1" BackgroundCssClass="modalProgressGreyBackground">
        <ProgressTemplate>
            <div class="modalPopup" style="font-family: Tahoma; font-size: 9pt">
                <img id="IMG1" src="../../../Image/indicator.gif" align="middle" />
                لطفا صبر نمایید ...
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
    <asp:ObjectDataSource ID="ObjectdatasourceProjectIngridient" runat="server" SelectMethod="GetData"
        TypeName="TSP.DataManager.TechnicalServices.ProjectIngridientTypeManager" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectdatasourceObserversPlansType" runat="server" SelectMethod="GetObserversPlansTypeDT"
        TypeName="TSP.DataManager.TechnicalServices.ProjectIngridientMajorsManager" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="ProjectIngridientTypeId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectdatasourceStructureGroup" runat="server" SelectMethod="GetData"
        TypeName="TSP.DataManager.TechnicalServices.StructureGroupsManager" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectdatasourceMajors" runat="server" SelectMethod="FindMjParents"
        TypeName="TSP.DataManager.MajorManager" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
    <asp:HiddenField ID="PgMode" runat="server" Visible="False" />
    <asp:HiddenField ID="PkPIMId" runat="server" Visible="False" />

</asp:Content>
