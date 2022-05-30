using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Security.Cryptography;


    public partial class Utility
    {
        /// <summary>
        /// Generate keys
        /// </summary>
        public class KeyGenerator
        {
            public enum CharacterTypes
            {
                Numbers, SmallLetters, CapitalLetters, Letters, SmallLettersAndNumbers, CapitalLettersAndNumbers, LettersAndNumbers
            }

            #region ClassProperties
            private int _MaxSize;
            public int MaxSize
            {
                get { return _MaxSize; }
                set { _MaxSize = value; }
            }

            private String CharacterSet;
            private CharacterTypes _CharacterType;
            public CharacterTypes CharacterType
            {
                get { return _CharacterType; }
                set
                {
                    _CharacterType = value;
                    switch (value)
                    {
                        case CharacterTypes.Numbers:
                            CharacterSet = "1234567890";
                            break;
                        case CharacterTypes.SmallLetters:
                            CharacterSet = "abcdefghijklmnopqrstuvwxyz";
                            break;
                        case CharacterTypes.SmallLettersAndNumbers:
                            CharacterSet = "abcdefghijklmnopqrstuvwxyz1234567890";
                            break;
                        case CharacterTypes.CapitalLetters:
                            CharacterSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                            break;
                        case CharacterTypes.CapitalLettersAndNumbers:
                            CharacterSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                            break;
                        case CharacterTypes.Letters:
                            CharacterSet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                            break;
                        case CharacterTypes.LettersAndNumbers:
                            CharacterSet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                            break;
                    }
                }
            }
            #endregion

            #region PrivateMethods
            private string RNGCharacterMask()
            {
                char[] chars = new char[62];
                chars = this.CharacterSet.ToCharArray();
                int size = this._MaxSize;
                byte[] data = new byte[1];
                RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
                crypto.GetNonZeroBytes(data);
                size = this._MaxSize;
                data = new byte[size];
                crypto.GetNonZeroBytes(data);
                StringBuilder result = new StringBuilder(size);
                foreach (byte b in data)
                { result.Append(chars[b % (chars.Length - 1)]); }
                return result.ToString();
            }


            #endregion

            #region Constructor
            /// <summary>
            /// CharacterType:LettersAndNumbers, MaxSize:10
            /// </summary>
            public KeyGenerator()
            {
                this.CharacterType = CharacterTypes.LettersAndNumbers;
                this._MaxSize = 10;
            }

            /// <summary>
            /// MaxSize:10
            /// </summary>
            /// <param name="CharacterType"></param>
            public KeyGenerator(CharacterTypes CharacterType)
            {
                this.CharacterType = CharacterType;
                this._MaxSize = 10;
            }

            /// <summary>
            /// CharacterType:LettersAndNumbers
            /// </summary>
            /// <param name="MaxSize"></param>
            public KeyGenerator(int MaxSize)
            {
                this.CharacterType = CharacterTypes.LettersAndNumbers;
                this._MaxSize = MaxSize;
            }

            public KeyGenerator(int MaxSize, CharacterTypes CharacterType)
            {
                this.CharacterType = CharacterType;
                this._MaxSize = MaxSize;
            }
            #endregion

            #region Generate
            /// <summary>
            /// Generate Key (i.e MaxSize:10, CharacterType:Numbers --> 8912345678)
            /// </summary>
            /// <returns></returns>
            public String Generate()
            {
                return RNGCharacterMask();
            }

            /// <summary>
            /// Generate Key (i.e MaxSize:10, CharacterType:Numbers --> 8912345678)
            /// </summary>
            /// <param name="CharacterType"></param>
            /// <returns></returns>
            public String Generate(CharacterTypes CharacterType)
            {
                this.CharacterType = CharacterType;
                return Generate();
            }

            /// <summary>
            /// Generate Key (i.e MaxSize:10, CharacterType:Numbers --> 8912345678)
            /// </summary>
            /// <param name="MaxSize"></param>
            /// <returns></returns>
            public String Generate(int MaxSize)
            {
                this._MaxSize = MaxSize;
                return Generate();
            }

            /// <summary>
            /// Generate Key (i.e MaxSize:10, CharacterType:Numbers --> 8912345678)
            /// </summary>
            /// <param name="MaxSize"></param>
            /// <param name="CharacterType"></param>
            /// <returns></returns>
            public String Generate(int MaxSize, CharacterTypes CharacterType)
            {
                this.CharacterType = CharacterType;
                this._MaxSize = MaxSize;
                return Generate();
            }
            #endregion

            #region GenerateWithYear
            /// <summary>
            /// Generate Key with year (i.e Year:89, MaxSize:10, CharacterType:Numbers --> 8912345678)
            /// </summary>
            /// <returns></returns>
            public String GenerateWithYear()
            {
                this._MaxSize -= 2;
                String Code = this.Generate();
                this._MaxSize += 2;

                Date objDate = new Date();
                return objDate.Year.ToString().Substring(2, 2) + Code;
            }

            /// <summary>
            /// Generate Key with year (i.e Year:89, MaxSize:10, CharacterType:Numbers --> 8912345678)
            /// </summary>
            /// <param name="CharacterType"></param>
            /// <returns></returns>
            public String GenerateWithYear(CharacterTypes CharacterType)
            {
                this.CharacterType = CharacterType;
                return GenerateWithYear();
            }

            /// <summary>
            /// Generate Key with year (i.e Year:89, MaxSize:10, CharacterType:Numbers --> 8912345678)
            /// </summary>
            /// <param name="MaxSize"></param>
            /// <returns></returns>
            public String GenerateWithYear(int MaxSize)
            {
                this._MaxSize = MaxSize;
                return GenerateWithYear();
            }

            /// <summary>
            /// Generate Key with year (i.e Year:89, MaxSize:10, CharacterType:Numbers --> 8912345678)
            /// </summary>
            /// <param name="MaxSize"></param>
            /// <param name="CharacterType"></param>
            /// <returns></returns>
            public String GenerateWithYear(int MaxSize, CharacterTypes CharacterType)
            {
                this.CharacterType = CharacterType;
                this._MaxSize = MaxSize;
                return GenerateWithYear();
            }
            #endregion
        }
    }

