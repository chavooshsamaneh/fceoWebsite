<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="PeriodOpinionForm.aspx.cs" Inherits="Employee_Amoozesh_PeriodOpinionForm" Title="فرم نظرسنجی" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>


<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>

<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
       <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
        [<a class="closeLink" href="#">بستن</a>]
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>




                        <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                            width="100%">
                            <tbody>
                                <tr>
                                    <td style="vertical-align: top; text-align: right">
                                        <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                            <tbody>
                                                <tr>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave" runat="server"  EnableTheming="False"
                                                            EnableViewState="False" OnClick="btnSave_Click" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False">
                                                            <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به ذخیره اطلاعات می باشید؟');
}" />
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                            </HoverStyle>
                                                            <Image Height="25px" Url="~/Images/icons/save.png" Width="25px" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server" CausesValidation="False" 
                                                            EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                                            ToolTip="بازگشت" UseSubmitBehavior="False">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                            </HoverStyle>
                                                            <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel4" HeaderText="اطلاعات دوره" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>


                        <table dir="rtl" width="100%">
                            <tr>
                                <td style="vertical-align: top; text-align: right">
                                    <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="عنوان دوره :">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                    <dxe:ASPxLabel ID="lblPPName" runat="server">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                    <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="ارائه دهنده:">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                    <dxe:ASPxLabel ID="lblInsName" runat="server">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: right">
                                    <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="تاریخ شروع نظر سنجی:">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                    <dxe:ASPxLabel ID="lblStartDate" runat="server">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                    <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Text="تاریخ پایان نظر سنجی:">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="vertical-align: top; text-align: right">
                                    <dxe:ASPxLabel ID="lblExpDate" runat="server">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel1" HeaderText="فرم نظر سنجی" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>


                        <table dir="rtl" align="center">
                            <tbody>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="استاد" Width="59px" ID="ASPxLabel1"></dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top" dir="ltr">
                                        <TSPControls:CustomAspxComboBox runat="server"  TextField="TeName" ID="cmbTeacher"  AutoPostBack="True" DataSourceID="OdbPPTeachers" ValueType="System.String" ValueField="TeId"  OnSelectedIndexChanged="cmbTeacher_SelectedIndexChanged">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                <RequiredField IsRequired="True" ErrorText="استاد مربوطه را انتخاب نمایید"></RequiredField>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>

                                            <ButtonStyle Width="13px"></ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <br />
                        <div dir="rtl">
                            <TSPControls:CustomAspxDevGridView runat="server"  Width="100%" ID="CustomAspxDevGridView1" KeyFieldName="QuId" AutoGenerateColumns="False" >
                               
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="کد سؤال" FieldName="QuCode" VisibleIndex="0"
                                        Width="30px">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="QuId" Visible="False" VisibleIndex="0">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="شماره" FieldName="QuNo" VisibleIndex="1" Width="20px">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="سؤال" FieldName="Question" VisibleIndex="2">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="پاسخ" FieldName="AnsTypeId" Name="AnsTypeId"
                                        VisibleIndex="3">
                                        <DataItemTemplate>
                                            <dxe:ASPxRadioButtonList ID="rdb" runat="server" DataSourceID="OdbAnswerType" ValueField="AnsTypeId" TextField="AnsName" Value='<%#Convert.ToString(Eval("AnsTypeId")) %>' __designer:wfdid="w5" RepeatDirection="Horizontal" OnDataBinding="rdb_DataBinding">
                                                <Border BorderStyle="None"></Border>
                                            </dxe:ASPxRadioButtonList>
                                            &nbsp; 
                                        </DataItemTemplate>
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>

                            </TSPControls:CustomAspxDevGridView>
                     
                        </div>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>



                        <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                            width="100%">
                            <tbody>
                                <tr>
                                    <td style="vertical-align: top; text-align: right">
                                        <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                            <tbody>
                                                <tr>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave2" runat="server"  EnableTheming="False"
                                                            EnableViewState="False" OnClick="btnSave_Click" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False">
                                                            <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به ذخیره اطلاعات می باشید؟');
}" />
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                            </HoverStyle>
                                                            <Image Height="25px" Url="~/Images/icons/save.png" Width="25px" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton2" runat="server" CausesValidation="False" 
                                                            EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                                            ToolTip="بازگشت" UseSubmitBehavior="False">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                            </HoverStyle>
                                                            <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData"
        TypeName="TSP.DataManager.QuestionsManager" FilterExpression="QuCode={0}">
        <FilterParameters>
            <asp:Parameter Name="newparameter" />
        </FilterParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdbAnswerType" runat="server" SelectMethod="GetData"
        TypeName="TSP.DataManager.AnswerTypeManager"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdbGrid" runat="server" SelectMethod="GetData"
        TypeName="TSP.DataManager.OpinionsManager" FilterExpression="QuCode={0}">
        <FilterParameters>
            <asp:Parameter Name="newparameter" />
        </FilterParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdbPPTeachers" runat="server" SelectMethod="FindByPKCode"
        TypeName="TSP.DataManager.TrainingTeachersManager">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="PkId" Type="Int32" />
            <asp:Parameter DefaultValue="0" Name="Type" Type="Byte" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField ID="PeriodId" runat="server" Visible="False" />
    <asp:HiddenField ID="MemberId" runat="server" Visible="False" />
</asp:Content>

