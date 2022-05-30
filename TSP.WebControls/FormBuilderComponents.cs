using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: TagPrefix("TSP.WebControls", "TSPControls")]
namespace TSP.WebControls.FormBuilderComponents
{
    #region Components
    internal class Label : System.Web.UI.WebControls.Label
    {
        public Label()
        {
        }
    }

    internal class TextBox : DevExpress.Web.ASPxTextBox
    {
        public TextBox()
        {
            this.Theme = "Metropolis";
            this.ApplyTheme("Metropolis");
            this.CssPostfix = "Metropolis";
            this.CssFilePath = "~/App_Themes/Metropolis/{0}/styles.css";
            this.SpriteCssFilePath = "~/App_Themes/Metropolis/{0}/sprite.css";

            this.RightToLeft = DevExpress.Utils.DefaultBoolean.True;
            this.ValidationSettings.ErrorImage.Height = System.Web.UI.WebControls.Unit.Pixel(14);
            //this.ValidationSettings.ErrorImage.Url = "~/App_Themes/MetropolisBlue/Editors/edtError.png";
            this.ValidationSettings.ErrorFrameStyle.ImageSpacing = System.Web.UI.WebControls.Unit.Pixel(4);
            this.ValidationSettings.ErrorFrameStyle.ErrorTextPaddings.PaddingLeft = System.Web.UI.WebControls.Unit.Pixel(4);
        }
    }

    internal class Memo : DevExpress.Web.ASPxMemo
    {
        public Memo()
        {
            this.RightToLeft = DevExpress.Utils.DefaultBoolean.True;
            this.SpriteCssFilePath = "~/App_Themes/MetropolisBlue/{0}/sprite.css";
            this.CssFilePath = "~/App_Themes/MetropolisBlue/{0}/styles.css";
            this.CssPostfix = "MetropolisBlue";
            this.LoadingPanelImage.Url = "~/App_Themes/MetropolisBlue/Editors/Loading.gif";
            this.ValidationSettings.ErrorFrameStyle.ImageSpacing = System.Web.UI.WebControls.Unit.Pixel(4);
            this.ValidationSettings.ErrorFrameStyle.ErrorTextPaddings.PaddingLeft = System.Web.UI.WebControls.Unit.Pixel(4);
            this.Height = System.Web.UI.WebControls.Unit.Pixel(100);
        }
    }

    internal class ComboBox : DevExpress.Web.ASPxComboBox
    {
        public ComboBox()
        {
            this.RightToLeft = DevExpress.Utils.DefaultBoolean.True;
            this.CssFilePath = "~/App_Themes/MetropolisBlue/{0}/styles.css";
            this.CssPostfix = "MetropolisBlue";
            this.ImageFolder = "~/App_Themes/MetropolisBlue/{0}/";
            this.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            this.IncrementalFilteringMode = DevExpress.Web.IncrementalFilteringMode.Contains;
            this.ValueType = typeof(String);
            this.ValidationSettings.ErrorImage.Height = System.Web.UI.WebControls.Unit.Pixel(14);
            this.ValidationSettings.ErrorImage.Url = "~/App_Themes/MetropolisBlue/Editors/edtError.png";
            this.ValidationSettings.ErrorFrameStyle.ImageSpacing = System.Web.UI.WebControls.Unit.Pixel(4);
            this.ValidationSettings.ErrorFrameStyle.ErrorTextPaddings.PaddingLeft = System.Web.UI.WebControls.Unit.Pixel(4);
        }
    }

    internal class ListBox : DevExpress.Web.ASPxListBox
    {
        public ListBox()
        {
            this.SelectionMode = DevExpress.Web.ListEditSelectionMode.Single;
            this.RightToLeft = DevExpress.Utils.DefaultBoolean.True;
            this.CssFilePath = "~/App_Themes/MetropolisBlue/{0}/styles.css";
            this.CssPostfix = "MetropolisBlue";
            this.SpriteCssFilePath = "~/App_Themes/MetropolisBlue/{0}/sprite.css";
            this.LoadingPanelImage.Url = "~/App_Themes/MetropolisBlue/Editors/Loading.gif";
            this.ValidationSettings.ErrorFrameStyle.ImageSpacing = System.Web.UI.WebControls.Unit.Pixel(4);
            this.ValidationSettings.ErrorFrameStyle.ErrorTextPaddings.PaddingLeft = System.Web.UI.WebControls.Unit.Pixel(4);
        }
    }

    internal class CheckBoxList : DevExpress.Web.ASPxListBox
    {
        public CheckBoxList()
        {
            this.RightToLeft = DevExpress.Utils.DefaultBoolean.True;
            this.CssFilePath = "~/App_Themes/MetropolisBlue/{0}/styles.css";
            this.CssPostfix = "MetropolisBlue";
            this.ImageFolder = "~/App_Themes/MetropolisBlue/{0}/";
            this.SelectionMode = DevExpress.Web.ListEditSelectionMode.CheckColumn;
            this.ItemStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            this.ValidationSettings.ErrorImage.Height = System.Web.UI.WebControls.Unit.Pixel(14);
            this.ValidationSettings.ErrorImage.Url = "~/App_Themes/MetropolisBlue/Editors/edtError.png";
            this.ValidationSettings.ErrorImage.Width = System.Web.UI.WebControls.Unit.Pixel(14);
            this.ValidationSettings.ErrorFrameStyle.ImageSpacing = System.Web.UI.WebControls.Unit.Pixel(4);
            this.ValidationSettings.ErrorFrameStyle.ErrorTextPaddings.PaddingLeft = System.Web.UI.WebControls.Unit.Pixel(4);
        }
    }

    internal class RadioButtonList : DevExpress.Web.ASPxRadioButtonList
    {
        public RadioButtonList()
        {
            this.RightToLeft = DevExpress.Utils.DefaultBoolean.True;
            this.CssClass = "MetropolisBlue";
            this.CssFilePath = "~/App_Themes/MetropolisBlue/{0}/styles.css";
            this.CssPostfix = "MetropolisBlue";
        }
    }

    internal class UploadControl : System.Web.UI.WebControls.Table
    {
        public CustomAspxUploadControl CustomUploadControl;
        public DevExpress.Web.ASPxImage ImageEndUpload;

        private Unit _UploadControlWidth;
        public Unit UploadControlWidth
        {
            get { return _UploadControlWidth; }
            set
            {
                _UploadControlWidth = value;
                CustomUploadControl.Width = value;
            }
        }

        private String _UploadControlId;
        public String UploadControlId
        {
            get { return _UploadControlId; }
            set
            {
                _UploadControlId = value;
                CustomUploadControl.ID = value;
                CustomUploadControl.ClientInstanceName = value;
                ImageEndUpload.ID = "ImageEndUpload_" + value;
                ImageEndUpload.ClientInstanceName = "ImageEndUpload_" + value;
                SetUploadControlClientSideEvents();
            }
        }

        private String _JSNameElementValueChanged;
        public String JSNameElementValueChanged
        {
            get { return _JSNameElementValueChanged; }
            set
            {
                _JSNameElementValueChanged = value;
                SetUploadControlClientSideEvents();
            }
        }

        public UploadControl()
        {
            ImageEndUpload = new DevExpress.Web.ASPxImage();
            CustomUploadControl = new CustomAspxUploadControl();

            UploadControlId = "1";
            UploadControlWidth = Unit.Pixel(1);

            TableRow Row = new TableRow();
            this.Width = Unit.Percentage(100);
            TableCell CellUploadControl = new TableCell();
            CellUploadControl.Width = Unit.Percentage(95);

            CustomUploadControl.RightToLeft = DevExpress.Utils.DefaultBoolean.True;
            CustomUploadControl.InputType = CustomAspxUploadControl.InputTypes.Files;
            CustomUploadControl.UploadWhenFileChoosed = true;
            CustomUploadControl.FileUploadComplete += new EventHandler<DevExpress.Web.FileUploadCompleteEventArgs>(UploadControl_FileUploadComplete);
            SetUploadControlClientSideEvents();
            CellUploadControl.Controls.Add(CustomUploadControl);
            Row.Cells.Add(CellUploadControl);

            TableCell CellEndUpload = new TableCell();
            ImageEndUpload.ImageUrl = "~/Images/button_ok.png";
            ImageEndUpload.ClientVisible = false;
            ImageEndUpload.ToolTip = "فایل انتخاب شد";
            CellEndUpload.Width = Unit.Percentage(32);
            Row.Cells.Add(CellEndUpload);
            this.Rows.Add(Row);
            this.CellPadding = 0;
            this.CellSpacing = 0;
        }

        private void SetUploadControlClientSideEvents()
        {
            String FileUploadComplete = "";
            FileUploadComplete = "function(s,e){ if(e.isValid){ " + ImageEndUpload.ClientInstanceName + ".SetVisible(true); ";
            if (String.IsNullOrWhiteSpace(JSNameElementValueChanged) == false)
                FileUploadComplete += JSNameElementValueChanged + "('" + UploadControlId + "',e.callbackData); ";
            FileUploadComplete += " } else{ " + ImageEndUpload.ClientInstanceName + ".SetVisible(false); ";
            if (String.IsNullOrWhiteSpace(JSNameElementValueChanged) == false)
                FileUploadComplete += JSNameElementValueChanged + "('" + UploadControlId + "','');";
            FileUploadComplete += " } }";
            CustomUploadControl.ClientSideEvents.FileUploadComplete = FileUploadComplete;
        }

        protected void UploadControl_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            try
            {
                e.CallbackData = SaveAttachment(e.UploadedFile);
            }
            catch (Exception ex)
            {
                e.IsValid = false;
                e.ErrorText = ex.Message;
            }
        }

        private string SaveAttachment(DevExpress.Web.UploadedFile uploadedFile)
        {
            string ret = "";
            if (uploadedFile.IsValid)
            {
                do
                {
                    System.IO.FileInfo FileType = new System.IO.FileInfo(uploadedFile.PostedFile.FileName);
                    ret = System.IO.Path.GetRandomFileName() + FileType.Extension;
                } while (System.IO.File.Exists(HttpContext.Current.Request.MapPath("~/Image/FormBuilder/") + ret) == true || System.IO.File.Exists(HttpContext.Current.Request.MapPath("~/Image/Temp/") + ret) == true);
                string tempFileName = HttpContext.Current.Request.MapPath("~/Image/Temp/") + ret;
                uploadedFile.SaveAs(tempFileName, true);
                ret = "~/Image/Temp/" + ret;
            }
            return ret;
        }
    }
    #endregion

    public class FormBuilder
    {
        private String JSNameElementValueChanged;
        private DataTable dtGroupsData;
        private DataTable dtElementsData;
        private DataTable dtElementItems;
        private String ValidationGroup;
        private String _ElementsData;
        public String ElementsData { get { return _ElementsData; } }
        private Boolean SaveElementsData;
        private Boolean LoadValue;
        private Boolean IsViewMode;
        private Boolean CheckValidation;

        public FormBuilder(String JSNameElementValueChanged, DataTable dtGroupsData, DataTable dtElementsData, DataTable dtElementItems, String ValidationGroup, Boolean LoadValue, Boolean CheckValidation, Boolean IsViewMode)
        {
            this.JSNameElementValueChanged = JSNameElementValueChanged;
            this.dtGroupsData = dtGroupsData;
            this.dtElementsData = dtElementsData;
            this.dtElementItems = dtElementItems;
            this.ValidationGroup = ValidationGroup;
            this.SaveElementsData = !IsViewMode;
            this._ElementsData = "";
            this.LoadValue = LoadValue;
            this.IsViewMode = IsViewMode;
            this.CheckValidation = CheckValidation;
        }

        public static DataTable MakeEmptyDataTableGroupsData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns["Id"].AutoIncrement = true;
            dt.Columns["Id"].AutoIncrementSeed = 1;
            dt.Columns.Add("GroupId");
            dt.Columns.Add("Title");
            return dt;
        }

        public static DataTable MakeEmptyDataTableElementsData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("RowId");
            dt.Columns["RowId"].AutoIncrement = true;
            dt.Columns["RowId"].AutoIncrementSeed = 1;
            dt.Columns.Add("Id");
            dt.Columns.Add("Type");
            dt.Columns.Add("Name");
            dt.Columns.Add("Value");
            dt.Columns.Add("Length");
            dt.Columns.Add("SingleInRow");
            dt.Columns.Add("IsRequired");
            dt.Columns.Add("GroupId");
            dt.Columns.Add("Color");
            dt.Columns["IsRequired"].AllowDBNull = true;
            return dt;
        }

        public static DataTable MakeEmptyDataTableElementItems()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("RowId");
            dt.Columns["RowId"].AutoIncrement = true;
            dt.Columns["RowId"].AutoIncrementSeed = 1;
            dt.Columns.Add("ElementId");
            dt.Columns.Add("ItemName");
            return dt;
        }

        Boolean CheckForPutElement(int Position, int Length, Boolean SingleInRow)
        {
            if (Position == 1)
                return true;
            else if (Position == 2 && (Length == 2 || SingleInRow == true))
                return false;
            return true;
        }

        public Panel Build()
        {
            if (dtGroupsData == null)
                return BuildWithoutGroup();
            else
            {
                Panel PanelForm = new Panel();
                Boolean AddNewRow = false;

                PanelForm.Controls.Add(BuildElementInGroup(dtElementsData.Select("GroupId=0")));

                if (PanelForm.Controls.Count > 0)
                    AddNewRow = true;

                for (int i = 0; i < dtGroupsData.Rows.Count; i++)
                {
                    if (AddNewRow)
                    {
                        Label lblNewRow = new Label();
                        lblNewRow.Text = "<br>";
                        PanelForm.Controls.Add(lblNewRow);
                    }
                    CustomASPxRoundPanel RoundPanel = new CustomASPxRoundPanel();
                    RoundPanel.Width = Unit.Percentage(100);
                    RoundPanel.HeaderText = dtGroupsData.Rows[i]["Title"].ToString();
                    RoundPanel.Controls.Add(BuildElementInGroup(dtElementsData.Select("GroupId=" + dtGroupsData.Rows[i]["GroupId"].ToString())));
                    if (((Table)RoundPanel.Controls[0]).Rows.Count > 0)
                    {
                        AddNewRow = true;
                        PanelForm.Controls.Add(RoundPanel);
                    }
                    else
                        AddNewRow = false;
                }
                return PanelForm;
            }
        }

        private Panel BuildWithoutGroup()
        {
            Panel PanelForm = new Panel();

            PanelForm.Controls.Add(BuildElementInGroup(dtElementsData.Select()));

            if (PanelForm.Controls.Count > 0)
            {
                Label lblNewRow = new Label();
                lblNewRow.Text = "<br>";
                PanelForm.Controls.Add(lblNewRow);
            }

            return PanelForm;
        }

        private Table BuildElementInGroup(DataRow[] ElementsData)
        {
            Table TableForm = new Table();
            TableForm.Width = Unit.Percentage(100);

            int Position = 1, i = -1, CurrentItem = -1;
            System.Collections.ArrayList ArraySkippedItems = new System.Collections.ArrayList();
            TableRow Row = null;

            do
            {
                do
                {
                    CurrentItem = -1;
                    if ((i + 1) < ElementsData.Length)
                        CurrentItem = ++i;
                    Boolean CurrenItemChoosed = false;
                    for (int index = 0; index < ArraySkippedItems.Count; index++)
                    {
                        if (CheckForPutElement(Position, Convert.ToInt32(ElementsData[(int)ArraySkippedItems[index]]["Length"]), Convert.ToBoolean(ElementsData[(int)ArraySkippedItems[index]]["SingleInRow"])))
                        {
                            if (CurrentItem > -1 && ElementsData.Length > CurrentItem)
                                ArraySkippedItems.Add(CurrentItem);
                            CurrentItem = (int)ArraySkippedItems[index];
                            ArraySkippedItems.RemoveAt(index);
                            CurrenItemChoosed = true;
                            break;
                        }
                    }

                    if (CurrentItem > -1 && ElementsData.Length > CurrentItem)
                    {
                        if (CurrenItemChoosed == false)
                        {
                            if (CheckForPutElement(Position, Convert.ToInt32(ElementsData[CurrentItem]["Length"]), Convert.ToBoolean(ElementsData[CurrentItem]["SingleInRow"])) == false)
                                ArraySkippedItems.Add(CurrentItem);
                            else
                                CurrenItemChoosed = true;
                        }

                        if (Position == 2 && (Convert.ToInt32(ElementsData[CurrentItem]["Length"]) == 2 || Convert.ToBoolean(ElementsData[CurrentItem]["SingleInRow"]) == true))
                        {
                            if ((i + 1) == ElementsData.Length)
                            {
                                //new row
                                TableCell tmpCell1 = new TableCell();
                                tmpCell1.Width = Unit.Percentage(12);
                                Row.Cells.Add(tmpCell1);
                                TableCell tmpCell2 = new TableCell();
                                tmpCell2.Width = Unit.Percentage(32);
                                Row.Cells.Add(tmpCell2);
                                TableForm.Rows.Add(Row);
                                Row = new TableRow();
                                Position = 1;
                            }
                        }
                    }

                    if (CurrenItemChoosed)
                        break;
                    if (CurrentItem == -1)
                        break;
                } while (true);

                if (ArraySkippedItems.Count == 0 && CurrentItem == -1)
                    break;

                if (Position == 1)
                    Row = new TableRow();

                int Length = Convert.ToInt32(ElementsData[CurrentItem]["Length"]);
                Boolean SingleInRow = Convert.ToBoolean(ElementsData[CurrentItem]["SingleInRow"]);

                if (Convert.ToInt32(ElementsData[CurrentItem]["Type"]) != (int)TSP.DataManager.FormBuilder.ElementTypesManager.ElementTypes.Label)
                {
                    TableCell Cell1 = new TableCell();
                    System.Web.UI.WebControls.Label ElementName = new System.Web.UI.WebControls.Label();
                    ElementName.Text = ElementsData[CurrentItem]["Name"].ToString();
                    Cell1.Controls.Add(ElementName);
                    Cell1.Width = Unit.Percentage(12);
                    Row.Cells.Add(Cell1);

                    TableCell Cell2 = new TableCell();
                    if (Position == 1 && Length == 2)
                        Cell2.Width = Unit.Percentage(88);
                    else
                        Cell2.Width = Unit.Percentage(32);
                    Cell2.Controls.Add(CreateElement(Convert.ToInt32(ElementsData[CurrentItem]["Type"]), Convert.ToInt32(ElementsData[CurrentItem]["Id"]), ElementsData[CurrentItem]["Name"].ToString(), ElementsData[CurrentItem]["Value"].ToString(), Convert.ToBoolean(ElementsData[CurrentItem]["IsRequired"]), ElementsData[CurrentItem]["Color"].ToString(), dtElementItems.Select("ElementId=" + ElementsData[CurrentItem]["Id"])));
                    if (Length == 2)
                        Cell2.ColumnSpan = 4;
                    Row.Cells.Add(Cell2);
                }
                else
                {
                    TableCell Cell = new TableCell();
                    Cell.Width = Unit.Percentage((Length == 1) ? 44 : 100);
                    Cell.HorizontalAlign = HorizontalAlign.Center;
                    Cell.Controls.Add(CreateElement(Convert.ToInt32(ElementsData[CurrentItem]["Type"]), Convert.ToInt32(ElementsData[CurrentItem]["Id"]), ElementsData[CurrentItem]["Name"].ToString(), ElementsData[CurrentItem]["Value"].ToString(), Convert.ToBoolean(ElementsData[CurrentItem]["IsRequired"]), ElementsData[CurrentItem]["Color"].ToString(), dtElementItems.Select("ElementId=" + ElementsData[CurrentItem]["Id"])));
                    if (Length == 2)
                        Cell.ColumnSpan = 5;
                    Row.Cells.Add(Cell);
                }

                if (Position == 1 && Length == 1 && SingleInRow == false)
                {
                    TableCell CellSpace = new TableCell();
                    CellSpace.Width = Unit.Percentage(12);
                    Row.Cells.Add(CellSpace);
                }

                if (Position == 1 && SingleInRow == true)
                    TableForm.Rows.Add(Row);
                else
                {
                    if (Length == 2)
                        Position = 1;
                    else
                        Position = (Position == 1) ? 2 : 1;

                    if (Position == 2 || (Position == 1 && Length == 2))
                        TableForm.Rows.Add(Row);
                }
            } while (true);

            return TableForm;
        }

        private Control CreateElement(int ElementType, int ElementId, String ElementName, String ElementValue, Boolean IsRequired, String Color, DataRow[] Items)
        {
            Control Ctrl = null;
            _ElementsData += (String.IsNullOrEmpty(_ElementsData)) ? "" : ";";
            switch (ElementType)
            {
                case (int)TSP.DataManager.FormBuilder.ElementTypesManager.ElementTypes.Label:
                    #region Label
                    TSP.WebControls.FormBuilderComponents.Label Label = new TSP.WebControls.FormBuilderComponents.Label();
                    Label.ID = "FormBuilder_" + ElementId;
                    if (LoadValue)
                        Label.Text = ElementValue;
                    Label.Width = Unit.Percentage(100);
                    Label.ForeColor = System.Drawing.ColorTranslator.FromHtml(Color);
                    Ctrl = Label;
                    #endregion
                    break;
                case (int)TSP.DataManager.FormBuilder.ElementTypesManager.ElementTypes.TextBox:
                    #region TextBox
                    TSP.WebControls.FormBuilderComponents.TextBox TextBox = new TSP.WebControls.FormBuilderComponents.TextBox();
                    TextBox.ID = "FormBuilder_" + ElementId;
                    if (LoadValue)
                        TextBox.Text = ElementValue;
                    TextBox.Width = Unit.Percentage(100);
                    TextBox.Enabled = !IsViewMode;
                    TextBox.ValidationSettings.ValidationGroup = ValidationGroup;
                    TextBox.ValidationSettings.ErrorDisplayMode = DevExpress.Web.ErrorDisplayMode.ImageWithText;
                    TextBox.ValidationSettings.Display = DevExpress.Web.Display.Dynamic;
                    TextBox.ValidationSettings.ErrorTextPosition = DevExpress.Web.ErrorTextPosition.Bottom;
                    if (CheckValidation)
                        TextBox.ValidationSettings.RequiredField.IsRequired = IsRequired;
                    TextBox.ValidationSettings.RequiredField.ErrorText = ElementName + " وارد نشده است";
                    if (SaveElementsData)
                    {
                        TextBox.ClientSideEvents.TextChanged = "function(s,e){ " + JSNameElementValueChanged + "('" + TextBox.ID + "',s.GetText()); }";
                        _ElementsData += TextBox.ID + ":" + TextBox.Text;
                    }
                    Ctrl = TextBox;
                    #endregion
                    break;
                case (int)TSP.DataManager.FormBuilder.ElementTypesManager.ElementTypes.Memo:
                    #region Memo
                    TSP.WebControls.FormBuilderComponents.Memo Memo = new TSP.WebControls.FormBuilderComponents.Memo();
                    Memo.ID = "FormBuilder_" + ElementId;
                    if (LoadValue)
                        Memo.Text = ElementValue;
                    Memo.Width = Unit.Percentage(100);
                    Memo.Enabled = !IsViewMode;
                    Memo.ValidationSettings.ValidationGroup = ValidationGroup;
                    Memo.ValidationSettings.ErrorDisplayMode = DevExpress.Web.ErrorDisplayMode.ImageWithText;
                    Memo.ValidationSettings.Display = DevExpress.Web.Display.Dynamic;
                    Memo.ValidationSettings.ErrorTextPosition = DevExpress.Web.ErrorTextPosition.Bottom;
                    if (CheckValidation)
                        Memo.ValidationSettings.RequiredField.IsRequired = IsRequired;
                    Memo.ValidationSettings.RequiredField.ErrorText = ElementName + " وارد نشده است";
                    if (SaveElementsData)
                    {
                        Memo.ClientSideEvents.TextChanged = "function(s,e){ " + JSNameElementValueChanged + "('" + Memo.ID + "',s.GetText()); }";
                        _ElementsData += Memo.ID + ":" + Memo.Text;
                    }
                    Ctrl = Memo;
                    #endregion
                    break;
                case (int)TSP.DataManager.FormBuilder.ElementTypesManager.ElementTypes.ComboBox:
                    #region ComboBox
                    TSP.WebControls.FormBuilderComponents.ComboBox ComboBox = new TSP.WebControls.FormBuilderComponents.ComboBox();
                    ComboBox.ID = "FormBuilder_" + ElementId;
                    for (int i = 0; i < Items.Length; i++)
                        ComboBox.Items.Add(Items[i]["ItemName"].ToString());
                    if (LoadValue && String.IsNullOrEmpty(ElementValue) == false)
                        ComboBox.SelectedIndex = ComboBox.Items.FindByText(ElementValue).Index;
                    ComboBox.Width = Unit.Percentage(100);
                    ComboBox.Enabled = !IsViewMode;
                    ComboBox.ValidationSettings.ValidationGroup = ValidationGroup;
                    ComboBox.ValidationSettings.ErrorDisplayMode = DevExpress.Web.ErrorDisplayMode.ImageWithText;
                    ComboBox.ValidationSettings.Display = DevExpress.Web.Display.Dynamic;
                    ComboBox.ValidationSettings.ErrorTextPosition = DevExpress.Web.ErrorTextPosition.Bottom;
                    if (CheckValidation)
                        ComboBox.ValidationSettings.RequiredField.IsRequired = IsRequired;
                    ComboBox.ValidationSettings.RequiredField.ErrorText = ElementName + " انتخاب نشده است";
                    if (SaveElementsData)
                    {
                        ComboBox.ClientSideEvents.ValueChanged = "function(s,e){ " + JSNameElementValueChanged + "('" + ComboBox.ID + "',s.GetValue()); }";
                        _ElementsData += ComboBox.ID + ":" + ComboBox.Value;
                    }

                    Panel p = new Panel();
                    p.Direction = ContentDirection.RightToLeft;
                    p.HorizontalAlign = HorizontalAlign.Right;
                    p.Controls.Add(ComboBox);
                    Ctrl = p;
                    #endregion
                    break;
                case (int)TSP.DataManager.FormBuilder.ElementTypesManager.ElementTypes.ListBox:
                    #region ListBox
                    TSP.WebControls.FormBuilderComponents.ListBox ListBox = new TSP.WebControls.FormBuilderComponents.ListBox();
                    ListBox.ID = "FormBuilder_" + ElementId;
                    for (int i = 0; i < Items.Length; i++)
                        ListBox.Items.Add(Items[i]["ItemName"].ToString());
                    if (LoadValue && String.IsNullOrEmpty(ElementValue) == false)
                        ListBox.SelectedIndex = ListBox.Items.FindByText(ElementValue).Index;
                    ListBox.Width = Unit.Percentage(100);
                    ListBox.Enabled = !IsViewMode;
                    ListBox.ValidationSettings.ValidationGroup = ValidationGroup;
                    ListBox.ValidationSettings.ErrorDisplayMode = DevExpress.Web.ErrorDisplayMode.ImageWithText;
                    ListBox.ValidationSettings.Display = DevExpress.Web.Display.Dynamic;
                    ListBox.ValidationSettings.ErrorTextPosition = DevExpress.Web.ErrorTextPosition.Bottom;
                    if (CheckValidation)
                        ListBox.ValidationSettings.RequiredField.IsRequired = IsRequired;
                    ListBox.ValidationSettings.RequiredField.ErrorText = ElementName + " انتخاب نشده است";
                    if (SaveElementsData)
                    {
                        ListBox.ClientSideEvents.ValueChanged = "function(s,e){ " + JSNameElementValueChanged + "('" + ListBox.ID + "',s.GetValue()); }";
                        _ElementsData += ListBox.ID + ":" + ListBox.Value;
                    }

                    Panel p2 = new Panel();
                    p2.Direction = ContentDirection.RightToLeft;
                    p2.HorizontalAlign = HorizontalAlign.Right;
                    p2.Controls.Add(ListBox);
                    Ctrl = p2;
                    #endregion
                    break;
                case (int)TSP.DataManager.FormBuilder.ElementTypesManager.ElementTypes.CheckBoxList:
                    #region CheckBoxList
                    TSP.WebControls.FormBuilderComponents.CheckBoxList CheckBox = new TSP.WebControls.FormBuilderComponents.CheckBoxList();
                    CheckBox.ID = "FormBuilder_" + ElementId;
                    for (int i = 0; i < Items.Length; i++)
                        CheckBox.Items.Add(Items[i]["ItemName"].ToString());
                    if (LoadValue && String.IsNullOrEmpty(ElementValue) == false)
                    {
                        String[] SelectedItems = ElementValue.Split('+');
                        for (int i = 0; i < SelectedItems.Length; i++)
                            CheckBox.Items.FindByText(SelectedItems[i]).Selected = true;
                    }
                    CheckBox.Width = Unit.Percentage(100);
                    CheckBox.Enabled = !IsViewMode;
                    CheckBox.ValidationSettings.ValidationGroup = ValidationGroup;
                    CheckBox.ValidationSettings.ErrorDisplayMode = DevExpress.Web.ErrorDisplayMode.ImageWithText;
                    CheckBox.ValidationSettings.Display = DevExpress.Web.Display.Dynamic;
                    CheckBox.ValidationSettings.ErrorTextPosition = DevExpress.Web.ErrorTextPosition.Bottom;
                    if (CheckValidation)
                        CheckBox.ValidationSettings.RequiredField.IsRequired = IsRequired;
                    CheckBox.ValidationSettings.RequiredField.ErrorText = ElementName + " انتخاب نشده است";
                    if (SaveElementsData)
                    {
                        CheckBox.ClientSideEvents.ValueChanged = "function(s,e){ " + JSNameElementValueChanged + "('" + CheckBox.ID + "',GetListBoxSelectedItems(s)); }";
                        String SelectedItems = "";
                        for (int i = 0; i < CheckBox.SelectedItems.Count; i++)
                        {
                            SelectedItems += (String.IsNullOrEmpty(SelectedItems)) ? "" : "+";
                            SelectedItems += CheckBox.SelectedItems[i].Value;
                        }
                        _ElementsData += CheckBox.ID + ":" + SelectedItems;
                    }
                    Ctrl = CheckBox;
                    #endregion
                    break;
                case (int)TSP.DataManager.FormBuilder.ElementTypesManager.ElementTypes.RadioButtonList:
                    #region CheckBoxList
                    TSP.WebControls.FormBuilderComponents.RadioButtonList RadioButtonList = new TSP.WebControls.FormBuilderComponents.RadioButtonList();
                    RadioButtonList.ID = "FormBuilder_" + ElementId;
                    for (int i = 0; i < Items.Length; i++)
                        RadioButtonList.Items.Add(Items[i]["ItemName"].ToString());
                    if (LoadValue && String.IsNullOrEmpty(ElementValue) == false)
                        RadioButtonList.SelectedIndex = RadioButtonList.Items.FindByText(ElementValue).Index;
                    RadioButtonList.Width = Unit.Percentage(100);
                    RadioButtonList.Enabled = !IsViewMode;
                    RadioButtonList.ValidationSettings.ValidationGroup = ValidationGroup;
                    RadioButtonList.ValidationSettings.ErrorDisplayMode = DevExpress.Web.ErrorDisplayMode.ImageWithText;
                    RadioButtonList.ValidationSettings.Display = DevExpress.Web.Display.Dynamic;
                    RadioButtonList.ValidationSettings.ErrorTextPosition = DevExpress.Web.ErrorTextPosition.Bottom;
                    if (CheckValidation)
                        RadioButtonList.ValidationSettings.RequiredField.IsRequired = IsRequired;
                    RadioButtonList.ValidationSettings.RequiredField.ErrorText = ElementName + " انتخاب نشده است";
                    if (SaveElementsData)
                    {
                        RadioButtonList.ClientSideEvents.ValueChanged = "function(s,e){ " + JSNameElementValueChanged + "('" + RadioButtonList.ID + "',s.GetValue()); }";
                        _ElementsData += RadioButtonList.ID + ":" + RadioButtonList.Value;
                    }
                    Ctrl = RadioButtonList;
                    #endregion
                    break;
                case (int)TSP.DataManager.FormBuilder.ElementTypesManager.ElementTypes.FileUpload:
                    #region FileUpload
                    TSP.WebControls.FormBuilderComponents.UploadControl FileUpload = new TSP.WebControls.FormBuilderComponents.UploadControl();
                    FileUpload.ID = FileUpload.UploadControlId = "FormBuilder_" + ElementId;
                    FileUpload.Width = FileUpload.UploadControlWidth = Unit.Percentage(100);
                    FileUpload.Enabled = !IsViewMode;
                    if (SaveElementsData)
                    {
                        FileUpload.JSNameElementValueChanged = JSNameElementValueChanged;
                        _ElementsData += FileUpload.ID + ":";
                    }
                    Ctrl = FileUpload;
                    #endregion
                    break;
            }
            return Ctrl;
        }
    }
}
