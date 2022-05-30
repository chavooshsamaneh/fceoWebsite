using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: TagPrefix("TSP.WebControls", "TSPControls")]
namespace TSP.WebControls.FormCreatorComponents
{
    #region Components
    public class TextBox : DevExpress.Web.ASPxTextBox
    {
        public TextBox()
        {
            this.Theme = "Metropolis";
            this.ApplyTheme("Metropolis");
            this.CssPostfix = "Metropolis";
            this.CssFilePath = "~/App_Themes/Metropolis/{0}/styles.css";
            this.SpriteCssFilePath = "~/App_Themes/Metropolis/{0}/sprite.css";
            this.ValidationSettings.ErrorImage.Height = System.Web.UI.WebControls.Unit.Pixel(14);
            this.ValidationSettings.ErrorImage.Url = "~/App_Themes/Metropolis/Editors/edtError.png";
            this.ValidationSettings.ErrorFrameStyle.ImageSpacing = System.Web.UI.WebControls.Unit.Pixel(4);
            this.ValidationSettings.ErrorFrameStyle.ErrorTextPaddings.PaddingLeft = System.Web.UI.WebControls.Unit.Pixel(4);
        }
    }

    public class ComboBox : DevExpress.Web.ASPxComboBox
    {
        public ComboBox()
        {
            this.Theme = "Metropolis";
            this.ApplyTheme("Metropolis");
            this.CssPostfix = "Metropolis";
            this.CssFilePath = "~/App_Themes/Metropolis/{0}/styles.css";
            this.SpriteCssFilePath = "~/App_Themes/Metropolis/{0}/sprite.css";
            this.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            this.EnableIncrementalFiltering = true;
            this.ValueType = typeof(String);
            this.ValidationSettings.ErrorImage.Height = System.Web.UI.WebControls.Unit.Pixel(14);
            //this.ValidationSettings.ErrorImage.Url = "~/App_Themes/Metropolis/Editors/edtError.png";
            this.ValidationSettings.ErrorFrameStyle.ImageSpacing = System.Web.UI.WebControls.Unit.Pixel(4);
            this.ValidationSettings.ErrorFrameStyle.ErrorTextPaddings.PaddingLeft = System.Web.UI.WebControls.Unit.Pixel(4);
        }
    }

    public class CheckBoxList : DevExpress.Web.ASPxListBox
    {
        public CheckBoxList()
        {
            this.Theme = "Metropolis";
            this.ApplyTheme("Metropolis");
            this.CssPostfix = "Metropolis";
            this.CssFilePath = "~/App_Themes/Metropolis/{0}/styles.css";
            this.SpriteCssFilePath = "~/App_Themes/Metropolis/{0}/sprite.css";
            this.SelectionMode = DevExpress.Web.ListEditSelectionMode.CheckColumn;
            this.ItemStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            this.ValidationSettings.ErrorImage.Height = System.Web.UI.WebControls.Unit.Pixel(14);
            this.ValidationSettings.ErrorImage.Url = "~/App_Themes/Metropolis/Editors/edtError.png";
            this.ValidationSettings.ErrorImage.Width = System.Web.UI.WebControls.Unit.Pixel(14);
            this.ValidationSettings.ErrorFrameStyle.ImageSpacing = System.Web.UI.WebControls.Unit.Pixel(4);
            this.ValidationSettings.ErrorFrameStyle.ErrorTextPaddings.PaddingLeft = System.Web.UI.WebControls.Unit.Pixel(4);
        }
    }

    public class RadioButtonList : DevExpress.Web.ASPxRadioButtonList
    {
        public RadioButtonList()
        {
            this.Theme = "Metropolis";
            this.ApplyTheme("Metropolis");
            this.CssPostfix = "Metropolis";
            this.CssFilePath = "~/App_Themes/Metropolis/{0}/styles.css";
            this.SpriteCssFilePath = "~/App_Themes/Metropolis/{0}/sprite.css";
        }
    }

    public class ListBox : DevExpress.Web.ASPxListBox
    {
        public ListBox()
        {
            this.Theme = "Metropolis";
            this.ApplyTheme("Metropolis");
            this.CssPostfix = "Metropolis";
            this.CssFilePath = "~/App_Themes/Metropolis/{0}/styles.css";
            this.SpriteCssFilePath = "~/App_Themes/Metropolis/{0}/sprite.css";
            this.LoadingPanelImage.Url = "~/App_Themes/Metropolis/Editors/Loading.gif";
            this.ValidationSettings.ErrorFrameStyle.ImageSpacing = System.Web.UI.WebControls.Unit.Pixel(4);
            this.ValidationSettings.ErrorFrameStyle.ErrorTextPaddings.PaddingLeft = System.Web.UI.WebControls.Unit.Pixel(4);
        }
    }

    public class Label : System.Web.UI.WebControls.Label
    {
        public Label()
        {
        }
    }

    public class Memo : DevExpress.Web.ASPxMemo
    {
        public Memo()
        {
            this.Theme = "Metropolis";
            this.ApplyTheme("Metropolis");
            this.CssPostfix = "Metropolis";
            this.CssFilePath = "~/App_Themes/Metropolis/{0}/styles.css";
            this.SpriteCssFilePath = "~/App_Themes/Metropolis/{0}/sprite.css";
            this.LoadingPanelImage.Url = "~/App_Themes/Metropolis/Editors/Loading.gif";
            this.ValidationSettings.ErrorFrameStyle.ImageSpacing = System.Web.UI.WebControls.Unit.Pixel(4);
            this.ValidationSettings.ErrorFrameStyle.ErrorTextPaddings.PaddingLeft = System.Web.UI.WebControls.Unit.Pixel(4);
            this.Height = System.Web.UI.WebControls.Unit.Pixel(100);
        }
    }

    public class TestLabel : System.Web.UI.WebControls.Label
    {
        public TestLabel()
        {
        }
    }
    #endregion

    public class FormCreator
    {
        private DataTable dtElementsData;
        private DataTable dtElementItems;
        private String ValidationGroup;
        private String _ElementsData;
        public String ElementsData { get { return _ElementsData; } }
        private Boolean SaveElementsData;
        private Boolean IsViewMode;

        public FormCreator(DataTable dtElementsData, DataTable dtElementItems, String ValidationGroup, Boolean SaveElementsData, Boolean IsViewMode)
        {
            this.dtElementsData = dtElementsData;
            this.dtElementItems = dtElementItems;
            this.ValidationGroup = ValidationGroup;
            this.SaveElementsData = SaveElementsData;
            this._ElementsData = "";
            this.IsViewMode = IsViewMode;
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

        public Table Create()
        {
            Table TableForm = new Table();
            TableForm.Width = Unit.Percentage(100);

            int Position = 1, i = -1, CurrentItem = -1;
            //System.Collections.Queue QueueSkippedItems = new System.Collections.Queue();
            System.Collections.ArrayList ArraySkippedItems = new System.Collections.ArrayList();
            TableRow Row = null;

            do
            {
                do
                {
                    CurrentItem = -1;
                    if ((i + 1) < dtElementsData.Rows.Count)
                        CurrentItem = ++i;
                    //if (ArraySkippedItems.Count > 0)
                    //{
                    Boolean CurrenItemChoosed = false;
                    for (int index = 0; index < ArraySkippedItems.Count; index++)
                    {
                        if (CheckForPutElement(Position, Convert.ToInt32(dtElementsData.Rows[(int)ArraySkippedItems[index]]["Length"]), Convert.ToBoolean(dtElementsData.Rows[(int)ArraySkippedItems[index]]["SingleInRow"])))
                        {
                            if (CurrentItem > -1 && dtElementsData.Rows.Count > CurrentItem)
                                ArraySkippedItems.Add(CurrentItem);
                            CurrentItem = (int)ArraySkippedItems[index];
                            ArraySkippedItems.RemoveAt(index);
                            CurrenItemChoosed = true;
                            break;
                        }
                    }

                    if (CurrentItem > -1 && dtElementsData.Rows.Count > CurrentItem)
                    {
                        if (CurrenItemChoosed == false)
                        {
                            if (CheckForPutElement(Position, Convert.ToInt32(dtElementsData.Rows[CurrentItem]["Length"]), Convert.ToBoolean(dtElementsData.Rows[CurrentItem]["SingleInRow"])) == false)
                                ArraySkippedItems.Add(CurrentItem);
                            else
                                CurrenItemChoosed = true;
                        }

                        if (Position == 2 && (Convert.ToInt32(dtElementsData.Rows[CurrentItem]["Length"]) == 2 || Convert.ToBoolean(dtElementsData.Rows[CurrentItem]["SingleInRow"]) == true))
                        {
                            if ((i + 1) == dtElementsData.Rows.Count)
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
                    //if (CurrentItem > -1 && dtElementsData.Rows.Count > CurrentItem)
                    //    QueueSkippedItems.Enqueue(CurrentItem);
                    //CurrentItem = (int)QueueSkippedItems.Dequeue();
                    //}
                    if (CurrentItem == -1)
                        break;
                } while (true);

                if (ArraySkippedItems.Count == 0 && CurrentItem == -1)
                    break;

                if (Position == 1)
                    Row = new TableRow();

                int Length = Convert.ToInt32(dtElementsData.Rows[CurrentItem]["Length"]);
                Boolean SingleInRow = Convert.ToBoolean(dtElementsData.Rows[CurrentItem]["SingleInRow"]);

                TableCell Cell1 = new TableCell();
                System.Web.UI.WebControls.Label ElementName = new System.Web.UI.WebControls.Label();
                ElementName.Text = dtElementsData.Rows[CurrentItem]["Name"].ToString();
                Cell1.Controls.Add(ElementName);
                Cell1.Width = Unit.Percentage(12);
                Row.Cells.Add(Cell1);

                TableCell Cell2 = new TableCell();
                Cell2.Width = Unit.Percentage(32);
                Cell2.Controls.Add(CreateElement(Convert.ToInt32(dtElementsData.Rows[CurrentItem]["Type"]), Convert.ToInt32(dtElementsData.Rows[CurrentItem]["Id"]), dtElementsData.Rows[CurrentItem]["Name"].ToString(), dtElementsData.Rows[CurrentItem]["Value"].ToString(), Convert.ToBoolean(dtElementsData.Rows[CurrentItem]["IsRequired"]), dtElementItems.Select("ElementId=" + dtElementsData.Rows[CurrentItem]["Id"])));
                if (Length == 2)
                    Cell2.ColumnSpan = 4;
                Row.Cells.Add(Cell2);

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

        private Control CreateElement(int ElementType, int ElementId, String ElementName, String ElementValue, Boolean IsRequired, DataRow[] Items)
        {
            Control Ctrl = null;
            _ElementsData += (String.IsNullOrEmpty(_ElementsData)) ? "" : ";";
            switch (ElementType)
            {
                case (int)TSP.DataManager.Automation.FormCreator.ElementTypesManager.ElementTypes.TextBox:
                    TSP.WebControls.FormCreatorComponents.TextBox TextBox = new TSP.WebControls.FormCreatorComponents.TextBox();
                    TextBox.ID = "FormCreator_" + ElementId;
                    TextBox.Text = ElementValue;
                    TextBox.Width = Unit.Percentage(100);
                    TextBox.Enabled = !IsViewMode;
                    TextBox.ValidationSettings.ValidationGroup = ValidationGroup;
                    TextBox.ValidationSettings.ErrorDisplayMode = DevExpress.Web.ErrorDisplayMode.ImageWithText;
                    TextBox.ValidationSettings.Display = DevExpress.Web.Display.Dynamic;
                    TextBox.ValidationSettings.ErrorTextPosition = DevExpress.Web.ErrorTextPosition.Bottom;
                    TextBox.ValidationSettings.RequiredField.IsRequired = IsRequired;
                    TextBox.ValidationSettings.RequiredField.ErrorText = ElementName + " وارد نشده است";
                    if (SaveElementsData)
                    {
                        TextBox.ClientSideEvents.TextChanged = "function(s,e){LetterFormElementValueChanged('" + TextBox.ID + "',s.GetText());}";
                        _ElementsData += TextBox.ID + ":" + TextBox.Text;
                    }
                    Ctrl = TextBox;
                    break;
                case (int)TSP.DataManager.Automation.FormCreator.ElementTypesManager.ElementTypes.ComboBox:
                    TSP.WebControls.FormCreatorComponents.ComboBox ComboBox = new TSP.WebControls.FormCreatorComponents.ComboBox();
                    ComboBox.ID = "FormCreator_" + ElementId;
                    for (int i = 0; i < Items.Length; i++)
                        ComboBox.Items.Add(Items[i]["ItemName"].ToString());
                    if (String.IsNullOrEmpty(ElementValue) == false)
                        ComboBox.SelectedIndex = ComboBox.Items.FindByText(ElementValue).Index;
                    ComboBox.Width = Unit.Percentage(100);
                    ComboBox.Enabled = !IsViewMode;
                    ComboBox.ValidationSettings.ValidationGroup = ValidationGroup;
                    ComboBox.ValidationSettings.ErrorDisplayMode = DevExpress.Web.ErrorDisplayMode.ImageWithText;
                    ComboBox.ValidationSettings.Display = DevExpress.Web.Display.Dynamic;
                    ComboBox.ValidationSettings.ErrorTextPosition = DevExpress.Web.ErrorTextPosition.Bottom;
                    ComboBox.ValidationSettings.RequiredField.IsRequired = IsRequired;
                    ComboBox.ValidationSettings.RequiredField.ErrorText = ElementName + " انتخاب نشده است";
                    if (SaveElementsData)
                    {
                        ComboBox.ClientSideEvents.ValueChanged = "function(s,e){LetterFormElementValueChanged('" + ComboBox.ID + "',s.GetValue());}";
                        _ElementsData += ComboBox.ID + ":" + ComboBox.Value;
                    }

                    Panel p = new Panel();
                    p.Direction = ContentDirection.RightToLeft;
                    p.HorizontalAlign = HorizontalAlign.Right;
                    p.Controls.Add(ComboBox);
                    Ctrl = p;
                    break;
                case (int)TSP.DataManager.Automation.FormCreator.ElementTypesManager.ElementTypes.CheckBoxList:
                    TSP.WebControls.FormCreatorComponents.CheckBoxList CheckBox = new TSP.WebControls.FormCreatorComponents.CheckBoxList();
                    CheckBox.ID = "FormCreator_" + ElementId;
                    for (int i = 0; i < Items.Length; i++)
                        CheckBox.Items.Add(Items[i]["ItemName"].ToString());
                    if (String.IsNullOrEmpty(ElementValue) == false)
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
                    CheckBox.ValidationSettings.RequiredField.IsRequired = IsRequired;
                    CheckBox.ValidationSettings.RequiredField.ErrorText = ElementName + " انتخاب نشده است";
                    if (SaveElementsData)
                    {
                        CheckBox.ClientSideEvents.ValueChanged = "function(s,e){LetterFormElementValueChanged('" + CheckBox.ID + "',GetListBoxSelectedItems(s));}";
                        String SelectedItems = "";
                        for (int i = 0; i < CheckBox.SelectedItems.Count; i++)
                        {
                            SelectedItems += (String.IsNullOrEmpty(SelectedItems)) ? "" : "+";
                            SelectedItems += CheckBox.SelectedItems[i].Value;
                        }
                        _ElementsData += CheckBox.ID + ":" + SelectedItems;
                    }
                    Ctrl = CheckBox;
                    break;
                case (int)TSP.DataManager.Automation.FormCreator.ElementTypesManager.ElementTypes.RadioButtonList:
                    TSP.WebControls.FormCreatorComponents.RadioButtonList RadioButtonList = new TSP.WebControls.FormCreatorComponents.RadioButtonList();
                    RadioButtonList.ID = "FormCreator_" + ElementId;
                    for (int i = 0; i < Items.Length; i++)
                        RadioButtonList.Items.Add(Items[i]["ItemName"].ToString());
                    if (String.IsNullOrEmpty(ElementValue) == false)
                        RadioButtonList.SelectedIndex = RadioButtonList.Items.FindByText(ElementValue).Index;
                    RadioButtonList.Width = Unit.Percentage(100);
                    RadioButtonList.Enabled = !IsViewMode;
                    RadioButtonList.ValidationSettings.ValidationGroup = ValidationGroup;
                    RadioButtonList.ValidationSettings.ErrorDisplayMode = DevExpress.Web.ErrorDisplayMode.ImageWithText;
                    RadioButtonList.ValidationSettings.Display = DevExpress.Web.Display.Dynamic;
                    RadioButtonList.ValidationSettings.ErrorTextPosition = DevExpress.Web.ErrorTextPosition.Bottom;
                    RadioButtonList.ValidationSettings.RequiredField.IsRequired = IsRequired;
                    RadioButtonList.ValidationSettings.RequiredField.ErrorText = ElementName + " انتخاب نشده است";
                    if (SaveElementsData)
                    {
                        RadioButtonList.ClientSideEvents.ValueChanged = "function(s,e){LetterFormElementValueChanged('" + RadioButtonList.ID + "',s.GetValue());}";
                        _ElementsData += RadioButtonList.ID + ":" + RadioButtonList.Value;
                    }
                    Ctrl = RadioButtonList;
                    break;
            }
            return Ctrl;
        }
    }
}
