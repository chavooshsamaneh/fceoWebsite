using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Data;
using System.IO;

namespace TSP.DataManager.Automation
{
    public class InsertInLetter
    {
        private string[] ImgArr;
        string LetterSerialNumber;
        private int SId, CartableId, UserId;

        public InsertInLetter(string[] ImageArr, int Secretariat, int UsrId, string LttrSrlNumber)
        {
            ImgArr = ImageArr;
            SId = Secretariat;
            UserId = UsrId;
            LetterSerialNumber = LttrSrlNumber;
        }

        public string Insert()
        {            
            int LetterId = -1;

            TSP.DataManager.TransactionManager Transaction = new TSP.DataManager.TransactionManager();

            LetterCreationTypesManager CreationTypesManager = new LetterCreationTypesManager();
            CartablesManager CartableManager = new CartablesManager();
            SecretariatManager SecretariatManager = new SecretariatManager();
            SecretariatLetterCreationTypeCodesManager SecretariatLetterCreationTypeCodeManager = new SecretariatLetterCreationTypeCodesManager();
            LettersManager LetterManager = new LettersManager();            
            AttachmentsManager AttachmentManager = new AttachmentsManager();
            
            Transaction.Add(CreationTypesManager);
            Transaction.Add(CartableManager);
            Transaction.Add(SecretariatManager);
            Transaction.Add(SecretariatLetterCreationTypeCodeManager);
            Transaction.Add(LetterManager);            
            Transaction.Add(AttachmentManager);

            Transaction.BeginSave();
            try
            {
                LetterManager = InsertLetter(LetterManager, CreationTypesManager, SecretariatManager, SecretariatLetterCreationTypeCodeManager, UserId);

                if (LetterManager.Count > 0)
                {
                    LetterId = int.Parse(LetterManager[0]["LetterId"].ToString());                    

                    InsertCartable(CartableManager, LetterId, UserId);
                    InsertAttachments(AttachmentManager, LetterId, UserId);
                }
                Transaction.EndSave();
            }
            catch (Exception err)
            {
                Transaction.CancelSave();
                return SetError(err);
            }

            return "ذخیره انجام شد";
        }

        private LettersManager InsertLetter(LettersManager LetterManager, LetterCreationTypesManager CreationTypesManager, SecretariatManager SecretariatManager, SecretariatLetterCreationTypeCodesManager SecretariatLetterCreationTypeCodeManager, int UserId)
        {
            DataRow drLetter = LetterManager.NewRow();

            drLetter["LetterSerialNumber"] = LetterSerialNumber;
            String LetterNumber_P1 = GetLetterNumber(SecretariatManager, SecretariatLetterCreationTypeCodeManager);

            drLetter["LetterNumber_P1"] = LetterNumber_P1;
            drLetter["LetterNumber_P2"] = LetterManager.SelectAutomationLetterNewNumber(LetterNumber_P1);
            drLetter["Secretariat"] = SId;
            drLetter["CreationType"] = (int)AutomationLetterCreationType.In;
            drLetter["EmpId"] = DBNull.Value;
            drLetter["DivId"] = (int)AutomationLetterDivision.Public;
            drLetter["LetterDate"] = GetDateOfToday();
            drLetter["Type"] = (int)AutomationLetterTypes.Letter;
            drLetter["TitleType"] = (int)AutomationLetterTittleType.Oomoomi;
            drLetter["UsePassword"] = false;
            drLetter["CreationDate"] = GetDateOfToday();
            drLetter["CreationTime"] = GetCurrentTime();
            drLetter["Creator"] = UserId;
            drLetter["UserId"] = UserId;
            drLetter["ModifiedDate"] = DateTime.Now;

            LetterManager.AddRow(drLetter);
            LetterManager.Save();
            LetterManager.DataTable.AcceptChanges();
            return LetterManager;
        }

        private void InsertCartable(CartablesManager CartableManager, int LetterId, int UserId)
        {            
            //Secretariat's Cartable
            DataRow drSCartable = CartableManager.NewRow();
            drSCartable["LetterId"] = LetterId;
            drSCartable["CartableGroup"] = CartableGroupsManager.DefaultGroup;
            drSCartable["CartableLetterType"] = (int)CartableLetterTypesManager.LetterTypes.New;
            drSCartable["ViewState"] = false;
            drSCartable["InActive"] = false;
            drSCartable["CartableSecretariatId"] = SId;
            drSCartable["CartableUserId"] = DBNull.Value;
            drSCartable["UserId"] = UserId;
            drSCartable["ModifiedDate"] = DateTime.Now;
            CartableManager.AddRow(drSCartable);

            if (CartableManager.Save() > 0)
                CartableManager.DataTable.AcceptChanges();

            CartableId = Convert.ToInt32(CartableManager[0]["CartableId"]);
        }

        private void InsertAttachments(AttachmentsManager AttachmentManager, int LetterId, int UserId)
        {
            for (int i = 0; i < ImgArr.Length; i++)
            {
                DataRow drAttachmentManager = AttachmentManager.NewRow();
                drAttachmentManager["TtId"] = (int)TableCodes.AutomationLetters;
                drAttachmentManager["RelatedId"] = LetterId;
                drAttachmentManager["FileName"] = Path.GetFileName(ImgArr[i]);
                drAttachmentManager["FilePath"] = ImgArr[i];
                drAttachmentManager["UserId"] = UserId;
                drAttachmentManager["ModifiedDate"] = DateTime.Now;
                AttachmentManager.AddRow(drAttachmentManager);
            }
            AttachmentManager.Save();
        }

        private String GetLetterNumber(SecretariatManager SecretariatManager, SecretariatLetterCreationTypeCodesManager SecretariatLetterCreationTypeCodeManager)
        {
            String Year = GetDateOfToday().Substring(2, 2);
            SecretariatManager.FindById(SId);
            SecretariatLetterCreationTypeCodeManager.FindByLetterCreationType((int)AutomationLetterCreationType.In);
            String Number = Year;
            if (String.IsNullOrEmpty(SecretariatManager[0]["RealSNumber"].ToString()) == false)
                Number += "/" + SecretariatManager[0]["RealSNumber"];
            if (SecretariatLetterCreationTypeCodeManager.Count > 0 && String.IsNullOrEmpty(SecretariatLetterCreationTypeCodeManager[0]["Code"].ToString()) == false)
                Number += "/" + SecretariatLetterCreationTypeCodeManager[0]["Code"];
            return Number;
        }

        private string SetError(Exception err)
        {
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;

                if (se.Number == 2601)
                {
                    if (err.Message.Contains("IX_OrgName_tblOtherOrganization"))
                    {
                        return "سازمان جدید وارد شده تکراری است";
                    }
                    else
                        return "اطلاعات تکراری می باشد";
                }
                else if (se.Number == 547)
                {
                    if (err.Message.Contains("FK_Automation.LetterRecievers_tblMember") || err.Message.Contains("FK_Automation.LetterRecievers_tblOffice"))
                    {
                        return "کد اعضا وارد شده برای گیرندگان معتبر نمی باشد";
                    }
                    else
                        return "خطایی در ذخیره انجام گرفته است";
                }
                else if (se.Number == 2627)
                {
                    return "کد تکراری می باشد";
                }
                else if (se.Number == 547)
                {

                    return "اطلاعات وابسته معتبر نمی باشد";

                }
                else
                {
                    return "خطایی در ذخیره انجام گرفته است";
                }
            }            
            return "خطایی در ذخیره انجام گرفته است";            
        }

        /// <summary>
        /// Get Persian Date of Today in format: YYYY/MM/DD
        /// </summary>
        /// <returns></returns>
        private string GetDateOfToday()
        {
            System.Globalization.PersianCalendar PDate = new System.Globalization.PersianCalendar();
            String PersianDate = PDate.GetYear(DateTime.Today) + "/" + PDate.GetMonth(DateTime.Today).ToString().PadLeft(2, '0') + "/" + PDate.GetDayOfMonth(DateTime.Today).ToString().PadLeft(2, '0');
            return PersianDate;
        }

        /// <summary>
        /// Get Current time in format: HH:MM
        /// </summary>
        /// <returns></returns>
        private string GetCurrentTime()
        {
            return DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Minute.ToString().PadLeft(2, '0');
        }        
    }
}
