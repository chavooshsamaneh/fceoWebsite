<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MeMembershipLicenceInfoUserControl.ascx.cs" Inherits="UserControl_MeMembershipLicenceInfoUserControl" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
    

            <fieldset id="RoundPanelMain"
                runat="server">
                <legend class="HelpUL">مدارک تحصیلی در عضویت</legend>
                <TSPControls:CustomAspxDevGridView2 ID="GridViewMeLicence" runat="server" Width="100%"
                  
                    ClientInstanceName="GridViewMeLicence" KeyFieldName="MlId">
                    <SettingsBehavior ColumnResizeMode="Control"></SettingsBehavior>
                    <Columns>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="LiName" Width="150px" Caption="مقطع">
                            <CellStyle Wrap="true">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="MjName" Width="150px" Caption="رشته">
                            <CellStyle Wrap="true">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="EndDate" Caption="تاریخ فارغ التحصیلی"
                            Name="EndDate" Width="110px">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="UnName" Width="200px" Caption="دانشگاه">
                            <CellStyle Wrap="true">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="Inquiry" Caption="وضعیت استعلام">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="7" FieldName="confirm" Caption="پاسخ استعلام">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="InquerySaveDate" Caption="تاریخ استعلام">
                        </dxwgv:GridViewDataTextColumn>
                          <dxwgv:GridViewDataImageColumn FieldName="FilePath" Caption="تصویر" VisibleIndex="7"
                            Width="150px">
                            <EditCellStyle Wrap="False">
                            </EditCellStyle>
                            <PropertiesImage ImageHeight="150px" ImageWidth="150px">
                            </PropertiesImage>
                        </dxwgv:GridViewDataImageColumn>
                        <dxwgv:GridViewDataImageColumn FieldName="InquiryImageURL" Caption="تصویر استعلام"
                            VisibleIndex="7" Width="150px">
                            <EditCellStyle Wrap="False">
                            </EditCellStyle>
                            <PropertiesImage ImageHeight="150px" ImageWidth="150px">
                            </PropertiesImage>
                        </dxwgv:GridViewDataImageColumn>
                    </Columns>
                </TSPControls:CustomAspxDevGridView2>
</fieldset>