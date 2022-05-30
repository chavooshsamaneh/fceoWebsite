<%@ Page Title="" Language="C#" Async="true" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="UpdateWorkRequestDocMeFile.aspx.cs" Inherits="Employee_TechnicalServices_Capacity_UpdateWorkRequestDocMeFile" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
        [<a class="closeLink" href="#"><span style="color: #000000"></span>بستن</a>]
    </div>MeId:
    <TSPControls:CustomTextBox  runat="server" ID="txtMeId" Width="100%" NullText="MeId">
    </TSPControls:CustomTextBox>



    <br /> 
    txtListConflictUsedCapacityMeId:
    <TSPControls:CustomASPXMemo  runat="server" ID="txtListConflictUsedCapacityMeId" Width="100%" >
    </TSPControls:CustomASPXMemo>

    <br />
    txtListConflictRemainCapacityMeId:
    <TSPControls:CustomASPXMemo  runat="server" ID="txtListConflictRemainCapacityMeId" Width="100%" >
    </TSPControls:CustomASPXMemo>
    <br />
    txtListConflictZeroMeId:
    <TSPControls:CustomASPXMemo  runat="server" ID="txtListConflictZeroMeId" Width="100%" >
    </TSPControls:CustomASPXMemo>
    <br />
       txtListConflictPercentOfCapacityMeId:
    <TSPControls:CustomASPXMemo  runat="server" ID="txtListConflictPercentOfCapacityMeId" Width="100%" >
    </TSPControls:CustomASPXMemo>
    <br />
    Ok:
    <TSPControls:CustomASPXMemo  runat="server" ID="txtListMeIdOk" Width="100%" >
    </TSPControls:CustomASPXMemo>
    <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="بروزرسانی اطلاعات پروانه" ToolTip="بروزرسانی اطلاعات پروانه"
        ID="btnSave" ClientInstanceName="btnSave" UseSubmitBehavior="False" EnableViewState="False"
        EnableTheming="False" OnClick="btnSave_Click" CausesValidation="true">
    </TSPControls:CustomAspxButton>


    <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="بروزرسانی اطلاعات نمایندگی" ToolTip="بروزرسانی اطلاعات نمایندگی"
        ID="btnSaveAgent" ClientInstanceName="btnSave" UseSubmitBehavior="False" EnableViewState="False"
        EnableTheming="False" OnClick="btnSaveAgent_Click" CausesValidation="true">
    </TSPControls:CustomAspxButton>
    <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="غیرفعال کردن آماده بکاری" ToolTip="غیرفعال کردن آماده بکاری"
        ID="btnInActice" ClientInstanceName="btnSave" UseSubmitBehavior="False" EnableViewState="False"
        EnableTheming="False" OnClick="btnInActice_Click" CausesValidation="true">
    </TSPControls:CustomAspxButton>


    <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="بروزرسانی باقی مانده واقعی و درصد مصرف" ToolTip="بروزرسانی باقی مانده واقعی و درصد مصرف"
        ID="btnUpdatePercentUsage" ClientInstanceName="btnSave" UseSubmitBehavior="False" EnableViewState="False"
        EnableTheming="False" OnClick="btnUpdatePercentUsage_Click" CausesValidation="true">
    </TSPControls:CustomAspxButton>

     <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="تست وب سرویس Esup Info" ToolTip="تست  Esup Info"
        ID="btnTestEsup" ClientInstanceName="btnTestEsup" UseSubmitBehavior="False" EnableViewState="False"
        EnableTheming="False" OnClick="btnTestEsup_Click" CausesValidation="true">
    </TSPControls:CustomAspxButton>

      <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="ارسال NotSentToShahrdari" ToolTip="ارسال NotSentToShahrdari"
        ID="NotSentToShahrdari" ClientInstanceName="btnTestEsup" UseSubmitBehavior="False" EnableViewState="False"
        EnableTheming="False" OnClick="NotSentToShahrdari_Click" CausesValidation="true">
    </TSPControls:CustomAspxButton>
</asp:Content>
