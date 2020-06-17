using AsiaPpayBackendAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

/// <summary>
/// MaintenanceSettingsChannelData 的摘要描述
/// </summary>
public class MaintenanceSettingsChannelData
{
    public MaintenanceSettingsChannelData() { }

    public class BankData { public List<Bank> Banks { get; set; } }

    public class SaveBank { public List<SaveBankClass> Banks { get; set; } }

    public bool CkeckPos()
    {
        ReadConfig reg = new ReadConfig();
        string JsonUrl = (reg.JsonUrl.Substring(0, reg.JsonUrl.Length - 1) == "\\") ? reg.JsonUrl : (reg.JsonUrl + "\\");
        string JsonPath = string.Format(@"{0}{1}.json", JsonUrl, "Gw_BankUpHold");

        if (!File.Exists(JsonPath)) { return false; }
        return true;
    }

    public BankData LoadData()
    {
        ReadConfig reg = new ReadConfig();
        string JsonUrl = (reg.JsonUrl.Substring(0, reg.JsonUrl.Length - 1) == "\\") ? reg.JsonUrl : (reg.JsonUrl + "\\");
        string JsonPath = string.Format(@"{0}{1}.json", JsonUrl, "Gw_BankUpHold");
        return BankUpHold(JsonPath);
    }

    public BankData BankUpHold(string Path)
    {
        using (StreamReader sr = new StreamReader(Path))
        {
            BankData maintenanceSettingsChannelData = JsonConvert.DeserializeObject<BankData>(sr.ReadToEnd());
            return maintenanceSettingsChannelData;
        }
    }

    public class BankFunction
    {
        /// <summary>
        /// 時間字串處理
        /// </summary>
        public Dictionary<string, Dictionary<string, List<string[]>>> SplitString(Bank BankData, List<string> banklistDataProp)//
        {
            Dictionary<string, List<string[]>> keyValues = new Dictionary<string, List<string[]>>();
            Dictionary<string, Dictionary<string, List<string[]>>> keyValuePairs = new Dictionary<string, Dictionary<string, List<string[]>>>();

            for (int i = 0; i < banklistDataProp.Count; i++)
            {
                switch (banklistDataProp[i])
                {
                    case "DeBgTime":
                        List<string[]> SplitDeBgTime = new List<string[]>();
                        if (!string.IsNullOrEmpty(BankData.DeBgTime))
                        {
                            string[] DeBgTime = BankData.DeBgTime.Split(' ');
                            string[] DeBgTimeData = DeBgTime[0].Split('-');
                            string[] DeBgTimeTime = DeBgTime[1].Split(':');
                            SplitDeBgTime.Add(DeBgTimeData);
                            SplitDeBgTime.Add(DeBgTimeTime);
                            keyValues.Add(banklistDataProp[i], SplitDeBgTime);
                        }
                        else
                        {
                            BankData.DeBgTime = "1901-01-01 00:00:00";
                            string[] DeBgTime = BankData.DeBgTime.Split(' ');
                            string[] DeBgTimeData = DeBgTime[0].Split('-');
                            string[] DeBgTimeTime = DeBgTime[1].Split(':');
                            SplitDeBgTime.Add(DeBgTimeData);
                            SplitDeBgTime.Add(DeBgTimeTime);
                            keyValues.Add(banklistDataProp[i], SplitDeBgTime);
                        }
                        break;

                    case "DeEnTime":
                        List<string[]> SplitDeEnTime = new List<string[]>();
                        if (!string.IsNullOrEmpty(BankData.DeEnTime))
                        {
                            string[] DeEnTime = BankData.DeEnTime.Split(' ');
                            string[] DeEnTimeData = DeEnTime[0].Split('-');
                            string[] DeEnTimeTime = DeEnTime[1].Split(':');
                            SplitDeEnTime.Add(DeEnTimeData);
                            SplitDeEnTime.Add(DeEnTimeTime);
                            keyValues.Add(banklistDataProp[i], SplitDeEnTime);
                        }
                        else
                        {
                            BankData.DeEnTime = "1901-01-01 00:00:00";
                            string[] DeEnTime = BankData.DeEnTime.Split(' ');
                            string[] DeEnTimeData = DeEnTime[0].Split('-');
                            string[] DeEnTimeTime = DeEnTime[1].Split(':');
                            SplitDeEnTime.Add(DeEnTimeData);
                            SplitDeEnTime.Add(DeEnTimeTime);
                            keyValues.Add(banklistDataProp[i], SplitDeEnTime);
                        }
                        break;

                    case "DaBgTime":
                        List<string[]> SplitDaBgTime = new List<string[]>();
                        if (!string.IsNullOrEmpty(BankData.DaBgTime))
                        {
                            string[] DaBgTime = BankData.DaBgTime.Split(':');
                            SplitDaBgTime.Add(DaBgTime);
                            keyValues.Add(banklistDataProp[i], SplitDaBgTime);
                        }
                        else
                        {
                            BankData.DaBgTime = "00:00:00";
                            string[] DaBgTime = BankData.DaBgTime.Split(':');
                            SplitDaBgTime.Add(DaBgTime);
                            keyValues.Add(banklistDataProp[i], SplitDaBgTime);
                        }
                        break;

                    case "DaEnTime":
                        List<string[]> SplitDaEnTime = new List<string[]>();
                        if (!string.IsNullOrEmpty(BankData.DaEnTime))
                        {
                            string[] DaEnTime = BankData.DaEnTime.Split(':');
                            SplitDaEnTime.Add(DaEnTime);
                            keyValues.Add(banklistDataProp[i], SplitDaEnTime);
                        }
                        else
                        {
                            BankData.DaEnTime = "00:00:00";
                            string[] DaEnTime = BankData.DaEnTime.Split(':');
                            SplitDaEnTime.Add(DaEnTime);
                            keyValues.Add(banklistDataProp[i], SplitDaEnTime);
                        }
                        break;

                    case "WdBgTime":
                        List<string[]> SplitWdBgTime = new List<string[]>();
                        if (!string.IsNullOrEmpty(BankData.WdBgTime))
                        {
                            string[] WdBgTime = BankData.WdBgTime.Split(' ');
                            string[] WdBgTimeData = WdBgTime[0].Split('-');
                            string[] WdBgTimeTime = WdBgTime[1].Split(':');
                            SplitWdBgTime.Add(WdBgTimeData);
                            SplitWdBgTime.Add(WdBgTimeTime);
                            keyValues.Add(banklistDataProp[i], SplitWdBgTime);
                        }
                        else
                        {
                            BankData.WdBgTime = "1901-01-01 00:00:00";
                            string[] WdBgTime = BankData.WdBgTime.Split(' ');
                            string[] WdBgTimeData = WdBgTime[0].Split('-');
                            string[] WdBgTimeTime = WdBgTime[1].Split(':');
                            SplitWdBgTime.Add(WdBgTimeData);
                            SplitWdBgTime.Add(WdBgTimeTime);
                            keyValues.Add(banklistDataProp[i], SplitWdBgTime);
                        }
                        break;

                    case "WdEnTime":
                        List<string[]> SplitWdEnTime = new List<string[]>();
                        if (!string.IsNullOrEmpty(BankData.WdEnTime))
                        {
                            string[] WdEnTime = BankData.WdEnTime.Split(' ');
                            string[] WdEnTimeData = WdEnTime[0].Split('-');
                            string[] WdEnTimeTime = WdEnTime[1].Split(':');
                            SplitWdEnTime.Add(WdEnTimeData);
                            SplitWdEnTime.Add(WdEnTimeTime);
                            keyValues.Add(banklistDataProp[i], SplitWdEnTime);
                        }
                        else
                        {
                            BankData.WdEnTime = "1901-01-01 00:00:00";
                            string[] WdEnTime = BankData.WdEnTime.Split(' ');
                            string[] WdEnTimeData = WdEnTime[0].Split('-');
                            string[] WdEnTimeTime = WdEnTime[1].Split(':');
                            SplitWdEnTime.Add(WdEnTimeData);
                            SplitWdEnTime.Add(WdEnTimeTime);
                            keyValues.Add(banklistDataProp[i], SplitWdEnTime);
                        }
                        break;

                    case "WrmBgTime":
                        List<string[]> SplitWrmBgTime = new List<string[]>();
                        if (!string.IsNullOrEmpty(BankData.WrmBgTime))
                        {
                            string[] WrmBgTime = BankData.WrmBgTime.Split(':');
                            SplitWrmBgTime.Add(WrmBgTime);
                            keyValues.Add(banklistDataProp[i], SplitWrmBgTime);
                        }
                        else
                        {
                            BankData.WrmBgTime = "00:00:00";
                            string[] WrmBgTime = BankData.WrmBgTime.Split(':');
                            SplitWrmBgTime.Add(WrmBgTime);
                            keyValues.Add(banklistDataProp[i], SplitWrmBgTime);
                        }
                        break;

                    case "WrmEnTime":
                        List<string[]> SplitWrmEnTime = new List<string[]>();
                        if (!string.IsNullOrEmpty(BankData.WrmEnTime))
                        {
                            string[] WrmEnTime = BankData.WrmEnTime.Split(':');
                            SplitWrmEnTime.Add(WrmEnTime);
                            keyValues.Add(banklistDataProp[i], SplitWrmEnTime);
                        }
                        else
                        {
                            BankData.WrmEnTime = "00:00:00";
                            string[] WrmEnTime = BankData.WrmEnTime.Split(':');
                            SplitWrmEnTime.Add(WrmEnTime);
                            keyValues.Add(banklistDataProp[i], SplitWrmEnTime);
                        }
                        break;

                    default:
                        break;
                }
            }
            keyValuePairs.Add(BankData.BankID.ToString(), keyValues);

            return keyValuePairs;
        }
    }

    public class Bank
    {
        public Bank()
        {
            DeBgTime = "1901-01-01 00:00:00";
            DeEnTime = "1901-01-01 00:00:00";
            DaBgTime = "00:00:00";
            DaEnTime = "00:00:00";
            WdBgTime = "1901-01-01 00:00:00";
            WdEnTime = "1901-01-01 00:00:00";
            WrmBgTime = "00:00:00";
            WrmEnTime = "00:00:00";
        }

        public int BankID { get; set; }
        public string Note { get; set; }
        public string DeBgTime { get; set; }
        public string DeEnTime { get; set; }
        public string DaBgTime { get; set; }
        public string DaEnTime { get; set; }
        public string WdBgTime { get; set; }
        public string WdEnTime { get; set; }
        public string WrmBgTime { get; set; }
        public string WrmEnTime { get; set; }
    }

    public class SaveBankClass
    {
        public SaveBankClass()
        {
            DeBgTime = "1901-01-01 00:00:00";
            DeEnTime = "1901-01-01 00:00:00";
            DaBgTime = "00:00:00";
            DaEnTime = "00:00:00";
            WdBgTime = "1901-01-01 00:00:00";
            WdEnTime = "1901-01-01 00:00:00";
            WrmBgTime = "00:00:00";
            WrmEnTime = "00:00:00";
        }

        public int BankID { get; set; }
        public string Note { get; set; }
        public string DeBgTime { get; set; }
        public string DeEnTime { get; set; }
        public string DaBgTime { get; set; }
        public string DaEnTime { get; set; }
        public string WdBgTime { get; set; }
        public string WdEnTime { get; set; }
        public string WrmBgTime { get; set; }
        public string WrmEnTime { get; set; }

        #region 已实现的维护时间

        /// <summary>
        /// 是否在定期维护期间内(存)
        /// </summary>
        public bool InTime_DaTime
        {
            get
            {
                return DefaultFormat.InTime(DateTime.Now, this.DaBgTime, this.DaEnTime);
            }
        }

        /// <summary>
        /// 是否在定期维护期间内 (提)
        /// </summary>
        public bool InTime_WrmTime
        {
            get
            {
                return DefaultFormat.InTime(DateTime.Now, this.WrmBgTime, this.WrmEnTime);
            }
        }

        /// <summary>
        /// 是否在设定的维护时间内 (存)
        /// </summary>
        public bool InTime_DeTime
        {
            get
            {
                return DefaultFormat.InTime(DateTime.Now, Convert.ToDateTime(this.DeBgTime), Convert.ToDateTime(this.DeEnTime));
            }
        }

        /// <summary>
        /// 是否在设定的维护时间内 (提)
        /// </summary>
        public bool InTime_WdTime
        {
            get
            {
                return DefaultFormat.InTime(DateTime.Now, Convert.ToDateTime(this.WdBgTime), Convert.ToDateTime(this.WdEnTime));
            }
        }

        #endregion 已实现的维护时间

        #region 即将到来的维护时间

        /// <summary>
        /// 是否有定期维护期间 (存)
        /// </summary>
        public bool BeTime_DaTime
        {
            get
            {
                if (this.DaBgTime == this.DaEnTime)
                    return false;

                return (!DefaultFormat.InTime(DateTime.Now, this.DaBgTime, this.DaEnTime));
            }
        }

        /// <summary>
        /// 是否有定期维护期间 (提)
        /// </summary>
        public bool BeTime_WrmTime
        {
            get
            {
                if (this.WrmBgTime == this.WrmEnTime)
                    return false;

                return (!DefaultFormat.InTime(DateTime.Now, this.WrmBgTime, this.WrmEnTime));
            }
        }

        /// <summary>
        /// 是否有设定的维护时间 (存)
        /// </summary>
        public bool BeTime_DeTime
        {
            get
            {
                return DefaultFormat.InTime(Convert.ToDateTime(this.DeBgTime), DateTime.Now, Convert.ToDateTime(this.DeEnTime));
            }
        }

        /// <summary>
        /// 是否有设定的维护时间 (提)
        /// </summary>
        public bool BeTime_WdTime
        {
            get
            {
                return DefaultFormat.InTime(Convert.ToDateTime(this.WdBgTime), DateTime.Now, Convert.ToDateTime(this.WdEnTime));
            }
        }

        #endregion 即将到来的维护时间
    }

    public SaveBank SaveBankOriginal(string Path)
    {
        using (StreamReader sr = new StreamReader(Path))
        {
            SaveBank SaveBankDataChannel = JsonConvert.DeserializeObject<SaveBank>(sr.ReadToEnd());
            return SaveBankDataChannel;
        }
    }

    #region 確認時間格式

    public bool CheckYear(String strNumber)
    {
        Regex NumberPattern = new Regex(@"^[0-9]*[1-9][0-9]*$");
        Regex NumberPatternY = new Regex(@"^(?:(?!0000)[0-9]{4})$");

        if (NumberPattern.IsMatch(strNumber) && !string.IsNullOrEmpty(strNumber)) { return NumberPatternY.IsMatch(strNumber); }
        else { return false; }
    }

    public bool CheckMonth(String strNumber)
    {
        Regex NumberPattern = new Regex(@"^[0-9]*[1-9][0-9]*$");
        Regex NumberPatternM = new Regex(@"^(0[1-9]|1[0-2])$");
        if (NumberPattern.IsMatch(strNumber) && !string.IsNullOrEmpty(strNumber)) { return NumberPatternM.IsMatch(strNumber); }
        else { return false; }
    }

    public bool CheckDay(String strNumber)
    {
        Regex NumberPattern = new Regex(@"^[0-9]*[1-9][0-9]*$");
        Regex NumberPatternD = new Regex(@"^(0[1-9]|1[0-9]|2[0-9]|3[0-1])$");
        if (NumberPattern.IsMatch(strNumber) && !string.IsNullOrEmpty(strNumber)) { return NumberPatternD.IsMatch(strNumber); }
        else { return false; }
    }

    public bool CheckHour(String strNumber)
    {
        Regex NumberPattern = new Regex(@"^\d+$");
        Regex NumberPatternH = new Regex(@"^(0[0-9]|1[0-9]|2[0-3])$");
        if (NumberPattern.IsMatch(strNumber) && !string.IsNullOrEmpty(strNumber)) { return NumberPatternH.IsMatch(strNumber); }
        else { return false; }
    }

    public bool CheckMin(String strNumber)
    {
        Regex NumberPattern = new Regex(@"^\d+$");
        Regex NumberPatternMin = new Regex(@"^(0[0-9]|1[0-9]|2[0-9]|3[0-9]|4[0-9]|5[0-9])$");
        if (NumberPattern.IsMatch(strNumber) && !string.IsNullOrEmpty(strNumber)) { return NumberPatternMin.IsMatch(strNumber); }
        else { return false; }
    }

    public bool CheckSec(String strNumber)
    {
        Regex NumberPattern = new Regex(@"^\d+$");
        Regex NumberPatternS = new Regex(@"^(0[0-9]|1[0-9]|2[0-9]|3[0-9]|4[0-9]|5[0-9])$");
        if (NumberPattern.IsMatch(strNumber) && !string.IsNullOrEmpty(strNumber)) { return NumberPatternS.IsMatch(strNumber); }
        else { return false; }
    }

    public bool CheckDeBgTime(List<string> value)
    {
        bool check = false;

        if (CheckYear(value[0]) && CheckMonth(value[4]) && CheckDay(value[8]))
        {
            if (CheckHour(value[12]) && CheckMin(value[20]) && CheckSec(value[28]))
            {
                check = true;
            }
        }
        return check;
    }

    public bool CheckDeEnTime(List<string> value)
    {
        bool check = false;
        if (CheckYear(value[1]) && CheckMonth(value[5]) && CheckDay(value[9]))
        {
            if (CheckHour(value[13]) && CheckMin(value[21]) && CheckSec(value[29]))
            {
                check = true;
            }
        }
        return check;
    }

    public bool CheckDaBgTime(List<string> value)
    {
        bool check = false;
        if (CheckHour(value[14]) && CheckMin(value[22]) && CheckSec(value[30]))
        {
            check = true;
        }
        return check;
    }

    public bool CheckDaEnTime(List<string> value)
    {
        bool check = false;
        if (CheckHour(value[15]) && CheckMin(value[23]) && CheckSec(value[31]))
        {
            check = true;
        }
        return check;
    }

    public bool CheckWdBgTime(List<string> value)
    {
        bool check = false;
        if (CheckYear(value[2]) && CheckMonth(value[6]) && CheckDay(value[10]))
        {
            if (CheckHour(value[16]) && CheckMin(value[24]) && CheckSec(value[32]))
            {
                check = true;
            }
        }
        return check;
    }

    public bool CheckWdEnTime(List<string> value)
    {
        bool check = false;
        if (CheckYear(value[3]) && CheckMonth(value[7]) && CheckDay(value[11]))
        {
            if (CheckHour(value[17]) && CheckMin(value[25]) && CheckSec(value[33]))
            {
                check = true;
            }
        }
        return check;
    }

    public bool CheckWrmBgTime(List<string> value)
    {
        bool check = false;
        if (CheckHour(value[18]) && CheckMin(value[26]) && CheckSec(value[34]))
        {
            check = true;
        }
        return check;
    }

    public bool CheckWrmEnTime(List<string> value)
    {
        bool check = false;
        if (CheckHour(value[19]) && CheckMin(value[27]) && CheckSec(value[35]))
        {
            check = true;
        }
        return check;
    }

    #endregion 確認時間格式
}