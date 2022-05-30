using System;
//using System.Collections.Generic;
//using System.Linq;
using System.Text;
using Kiccc.Ing.PcPos;
using Kiccc.Ing.PcPos.Serial;
using System.Xml;
//using System.Xml.Linq;
using System.IO;


namespace TSP.Utilities
{


    public class PcPOS
    {
        public enum SerialPortStopBit
        {
            None = 0,
            One = 1,
            Two = 2,
            OnePointFive = 3,
        }

        public enum SerialPortParity
        {
            None = 0,
            Odd = 1,
            Even = 2,
            Mark = 3,
            Space = 4,
        }


        SerialIngenico _driver = new SerialIngenico();

        public void Closing()
        {
            _driver.Dispose();

        }

        public void InitiateService()
        {
            try
            {
                SettingPos settingPos = new SettingPos();
                ReadSeting(settingPos);

                _driver.InitiateService(settingPos.SerialNo, settingPos.AcceptorId, settingPos.TerminalId, settingPos.ComPort, Convert.ToInt32(settingPos.BaudRate)
                 , Convert.ToInt32(settingPos.DataBits),
                Convert.ToInt32(settingPos.StopBit),
                Convert.ToInt32(settingPos.Parity));

            }
            catch (System.Xml.XmlException ex)
            {

            }

        }

        public void ResetService()
        {
            try
            {
                // Reset Service
                _driver.ResetService();

            }
            catch (Exception ex)
            {
            }
        }


        public Responce sale(string Amount)
        {

            try
            {
                Responce responce = new Responce();
                InitiateService();
                string s = _driver.Sale(Amount);
                ResponceAnalayse(responce, s);
                return responce;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static Responce SaleWithPaymentId(string Amount, string PaymentId)
        {
            try
            {
                SerialIngenico Driver = new SerialIngenico();
                SettingPos settingPos = new SettingPos();
                ReadSeting(settingPos);

                Driver.InitiateService(settingPos.SerialNo, settingPos.AcceptorId, settingPos.TerminalId, settingPos.ComPort, Convert.ToInt32(settingPos.BaudRate)
                 , Convert.ToInt32(settingPos.DataBits),
                Convert.ToInt32(settingPos.StopBit),
                Convert.ToInt32(settingPos.Parity));


                //        Driver.InitiateService("9012812654", "000000000041518", "00042269", "COM4",
                // 115200, 8,
                //Convert.ToInt32(SerialPortStopBit.One),
                //Convert.ToInt32(SerialPortParity.None));
                Responce responce = new Responce();
                // InitiateService();
                string s = Driver.SaleWithPaymentId(Amount, PaymentId);
                ResponceAnalayse(responce, s);
                return responce;
                
            }
            catch (Exception err)
            {
                //Utility.SaveWebsiteError(err);
                return null;
                //Utility.SaveWebsiteError(err);
               // Utilities
                
            }

        }

        public Responce BingSale(string Amount)
        {
            try
            {
                Responce responce = new Responce();
                _driver.BeginSale(Amount);

                _driver.ResponseReceived += (s, ev) =>
                {
                    ResponceAnalayse(responce, ev.Response);
                };
                return responce;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Responce BingSaleWithPaymentId(string Amount, string PaymentId)
        {
            try
            {
                Responce responce = new Responce();
                _driver.BeginSaleWithPaymentId(Amount, PaymentId);

                _driver.ResponseReceived += (s, ev) =>
              {
                  ResponceAnalayse(responce, ev.Response);
              };
                return responce;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static Responce ResponceAnalayse(Responce responce, string ev)
        {
            StringReader StringReader = new StringReader(ev);
            XmlReader reader = XmlReader.Create(StringReader);
            while (reader.Read())
            {
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case "RRN":
                            responce.RRN = reader.ReadString();
                            break;
                        case "RespCode":
                            responce.RespCode = reader.ReadString();
                            break;
                        case "SerialNo":
                            responce.SerialNo = reader.ReadString();
                            break;
                        case "TransactionDate":
                            responce.TransactionDate = reader.ReadString();
                            break;
                        case "TransactionTime":
                            responce.TransactionTime = reader.ReadString();
                            break;
                        case "Amount":
                            responce.Amount = reader.ReadString();
                            break;
                        case "TerminalId":
                            responce.TerminalId = reader.ReadString();
                            break;
                        case "TraceNo":
                            responce.TraceNo = reader.ReadString();
                            break;
                        case "Pan":
                            responce.Pan = reader.ReadString();
                            break;
                    }
                }
            }
            return responce;
        }

        private static void ReadSeting(SettingPos settingPos)
        {
            FileStream fs = new FileStream(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/PCPosConfig.xml"), FileMode.Open);
            XmlReaderSettings settings;

            settings = new XmlReaderSettings();
            settings.ConformanceLevel = ConformanceLevel.Document;


            XmlReader reader = XmlReader.Create(fs, settings);
          
            while (reader.Read())
            {
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case "PassPhrase":
                            settingPos.PassPhrase = reader.ReadString();
                            break;
                        case "SerialNo":
                            settingPos.SerialNo = reader.ReadString();
                            break;
                        case "TerminalId":
                            settingPos.TerminalId = reader.ReadString();
                            break;
                        case "AcceptorId":
                            settingPos.AcceptorId = reader.ReadString();
                            break;
                        case "ComPort":
                            settingPos.ComPort = reader.ReadString();
                            break;
                        case "BaudRate":
                            settingPos.BaudRate = reader.ReadString();
                            break;
                        case "Parity":
                            settingPos.Parity = reader.ReadString();
                            break;
                        case "DataBits":
                            settingPos.DataBits = reader.ReadString();
                            break;
                        case "StopBit":
                            settingPos.StopBit = reader.ReadString();
                            break;
                        case "TimeOut":
                            settingPos.TimeOut = reader.ReadString();
                            break;
                    }
                }
            }

        }

        public static string InitiateServiceWeb()
        {
            
                SettingPos settingPos = new SettingPos();
                ReadSeting(settingPos);

            XmlWriterSettings writerSettings = new XmlWriterSettings();
            writerSettings.OmitXmlDeclaration = true;
            writerSettings.ConformanceLevel = ConformanceLevel.Fragment;
            writerSettings.CloseOutput = false;
            StringBuilder StringBuilder = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(StringBuilder, writerSettings))
            {
                writer.WriteStartElement("DriverConfig");
                writer.WriteElementString("PassPhrase", settingPos.PassPhrase);
                writer.WriteElementString("SerialNo", settingPos.SerialNo);
                writer.WriteElementString("AcceptorId", settingPos.AcceptorId);
                writer.WriteElementString("TerminalId", settingPos.TerminalId);
                writer.WriteElementString("ComPort", settingPos.ComPort);
                writer.WriteElementString("BaudRate", settingPos.BaudRate);
                writer.WriteElementString("Parity", settingPos.Parity);
                writer.WriteElementString("DataBits", settingPos.DataBits);
                writer.WriteElementString("StopBit", settingPos.StopBit);
                writer.WriteElementString("TimeOut", settingPos.StopBit);
                writer.WriteEndElement();
                writer.Flush();
            }
            return StringBuilder.ToString();
        
        }
        public static string SaleWithPaymentIdWeb(string Amount, string PaymentId)
        {
            XmlWriterSettings writerSettings = new XmlWriterSettings();
            writerSettings.OmitXmlDeclaration = true;
            writerSettings.ConformanceLevel = ConformanceLevel.Fragment;
            writerSettings.CloseOutput = false;
            StringBuilder StringBuilder = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(StringBuilder, writerSettings))
            {
                writer.WriteStartElement("TransactionRequest");
                writer.WriteElementString("PassPhrase", "KicccPcPosAgent");
                writer.WriteElementString("TransactionType", "1");
                writer.WriteElementString("Amount", Amount);
                writer.WriteElementString("PaymentId",PaymentId);
                writer.WriteEndElement();
                writer.Flush();
            }

            return StringBuilder.ToString();
        }

    }
    public class SettingPos
    {
        public string PassPhrase { get; set; }
        public string SerialNo { get; set; }
        public string AcceptorId { get; set; }
        public string TerminalId { get; set; }
        public string ComPort { get; set; }
        public string BaudRate { get; set; }
        public string Parity { get; set; }
        public string DataBits { get; set; }
        public string StopBit { get; set; }
        public string TimeOut { get; set; }

        // <PassPhrase>KicccPcPosAgent</PassPhrase>
        //<SerialNo>2151047993</SerialNo>
        //<AcceptorId>222490000460</AcceptorId>
        //<TerminalId>03005709</TerminalId>
        //<ComPort>COM3</ComPort>
        //<BaudRate>115200</BaudRate>
        //<Parity>0</Parity>
        //<DataBits>8</DataBits>
        //<StopBit>1</StopBit>
        //<TimeOut>220</TimeOut>

    }

    public class Responce
    {

        public string RRN { get; set; }

        public string RespCode { get; set; }

        public string SerialNo { get; set; }

        public string TransactionDate { get; set; }

        public string TransactionTime { get; set; }

        public string Amount { get; set; }

        public string TerminalId { get; set; }

        public string TraceNo { get; set; }

        public string Pan { get; set; }
    }
}
