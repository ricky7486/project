using AsiaPpayBackendAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AppControl_Manage_MaintenanceSettingsChannel : System.Web.UI.Page
{
    //protected string Refresh_State_JSCode = CheckLogin.Refresh_State_JSCode;

    #region Member

    protected string BankName = "";
    protected string BankList = "";
    protected string Item = "";

    protected string DeBgTime = "存款开始";
    protected string DeEnTime = "存款结束";
    protected string DaBgTime = "定期存款开始";
    protected string DaEnTime = "定期存款结束";
    protected string WdBgTime = "提款开始";
    protected string WdEnTime = "提款结束";
    protected string WrmBgTime = "定期提款开始";
    protected string WrmEnTime = "定期提款结束";

    protected string FailInformationDeBgTimeTable = "";
    protected string FailInformationDeEnTimeTable = "";
    protected string FailInformationDaBgTimeTable = "";
    protected string FailInformationDaEnTimeTable = "";
    protected string FailInformationWdBgTimeTable = "";
    protected string FailInformationWdEnTimeTable = "";
    protected string FailInformationWrmBgTimeTable = "";
    protected string FailInformationWrmEnTimeTable = "";

    private ReadConfig reg = new ReadConfig();
    private static MaintenanceSettingsChannelData.BankData BanklistData;
    private static MaintenanceSettingsChannelData.SaveBank SaveBankList;
    private MaintenanceSettingsChannelData SettingDataBase = new MaintenanceSettingsChannelData();
    private MaintenanceSettingsChannelData.BankFunction bankFunction = new MaintenanceSettingsChannelData.BankFunction();
    private static List<string> value = new List<string>();
    private List<string> MinItem = new List<string>();
    private Dictionary<string, Dictionary<string, List<string[]>>> SplitStringDate = null;
    private List<List<string[]>> Datas = new List<List<string[]>>();
    private Type type = typeof(MaintenanceSettingsChannelData.Bank);

    #endregion Member

    //protected override void Render(HtmlTextWriter writer)
    //{
    //    foreach (Control c in this.Controls)
    //    {
    //        this.Page.ClientScript.RegisterForEventValidation(
    //                c.UniqueID.ToString()
    //        );
    //    }
    //    base.Render(writer);
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = GetInfo.GetPageTitle(Resources.MyGlobal.MAINTENANCE_SETTINGS_CHANNEL);
        if (Page.IsPostBack)
        {
            ////停用浏览器快取
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
        }
        else
        {
            //if (LoginInfo.IsLogin && LoginInfo.IsHavePagePower)
            //{
                if (SettingDataBase.CkeckPos())
                {
                    BanklistData = SettingDataBase.LoadData();
                    for (int i = 0; i < BanklistData.Banks.Count; i++)
                    {
                        BankName = BanklistData.Banks[i].Note + "(" + BanklistData.Banks[i].BankID.ToString() + ")";
                        
                        if (/*BanklistData.Banks[i].Note == "示范" ||*/
                            PamaCheck.DataContains("999", BanklistData.Banks[i].BankID.ToString()) && !PamaCheck.DataContains("7", LoginInfo.UserPermissions.ToString()))
                        { continue; }

                        MakeList.addSelectItem(DropDownList1, BankName, BanklistData.Banks[i].BankID.ToString());//DropDownList1.Items.Add(new ListItem(BankName, i.ToString()));
                    }

                    DropDownList1.Items.Insert(0, new ListItem("请选择"));
                    //Button2.Enabled = false;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    DateTimeYear(1901, DateTime.Now.Year);
                    DateTimeMonth();
                    DateTimeDay();
                    DateTimeTime();
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                else
                {
                    Label1.Text = "档案读取失败";
                }
            //}
        }
    }

    public void DateTimeYear(int start, int end)
    {
        for (int i = start; i < end + 6; i++)
        {
            DeBgTimeYear.Items.Add(i.ToString());
            DeEnTimeYear.Items.Add(i.ToString());
            WdBgTimeYear.Items.Add(i.ToString());
            WdEnTimeYear.Items.Add(i.ToString());
        }//year

        //DeBgTimeYear.Items.Insert(0, new ListItem("请选择"));
        //DeEnTimeYear.Items.Insert(0, new ListItem("请选择"));
        //WdBgTimeYear.Items.Insert(0, new ListItem("请选择"));
        //WdEnTimeYear.Items.Insert(0, new ListItem("请选择"));
    }

    public void DateTimeMonth()
    {
        for (int i = 1; i < 13; i++)
        {
            DeBgTimeMonth.Items.Add(i.ToString("00"));
            DeEnTimeMonth.Items.Add(i.ToString("00"));
            WdBgTimeMonth.Items.Add(i.ToString("00"));
            WdEnTimeMonth.Items.Add(i.ToString("00"));
        }//Month

        //DeBgTimeMonth.Items.Insert(0, new ListItem("请选择"));
        //DeEnTimeMonth.Items.Insert(0, new ListItem("请选择"));
        //WdBgTimeMonth.Items.Insert(0, new ListItem("请选择"));
        //WdEnTimeMonth.Items.Insert(0, new ListItem("请选择"));
    }

    public void DateTimeDay()
    {
        int DeBg = 32; int DeEn = 32; int WdBg = 32; int WdEn = 32;
        for (int i = 1; i < DeBg; i++) { DeBgTimeDay.Items.Add(i.ToString("00")); }//Day
        for (int i = 1; i < DeEn; i++) { DeEnTimeDay.Items.Add(i.ToString("00")); }//Day
        for (int i = 1; i < WdBg; i++) { WdBgTimeDay.Items.Add(i.ToString("00")); }//Day
        for (int i = 1; i < WdEn; i++) { WdEnTimeDay.Items.Add(i.ToString("00")); }//Day

        //DeBgTimeDay.Items.Insert(0, new ListItem("请选择"));
        //DeEnTimeDay.Items.Insert(0, new ListItem("请选择"));
        //WdBgTimeDay.Items.Insert(0, new ListItem("请选择"));
        //WdEnTimeDay.Items.Insert(0, new ListItem("请选择"));
    }

    public void DateTimeTime()
    {
        for (int i = 0; i < 24; i++)
        {
            DeBgTimeHr.Items.Add(i.ToString("00"));
            DeEnTimeHr.Items.Add(i.ToString("00"));
            DaBgTimeHr.Items.Add(i.ToString("00"));
            DaEnTimeHr.Items.Add(i.ToString("00"));
            WdBgTimeHr.Items.Add(i.ToString("00"));
            WdEnTimeHr.Items.Add(i.ToString("00"));
            WrmBgTimeHr.Items.Add(i.ToString("00"));
            WrmEnTimeHr.Items.Add(i.ToString("00"));
        }//hr

        //DeBgTimeHr.Items.Insert(0, new ListItem("请选择"));
        //DeEnTimeHr.Items.Insert(0, new ListItem("请选择"));
        //DaBgTimeHr.Items.Insert(0, new ListItem("请选择"));
        //DaEnTimeHr.Items.Insert(0, new ListItem("请选择"));
        //WdBgTimeHr.Items.Insert(0, new ListItem("请选择"));
        //WdEnTimeHr.Items.Insert(0, new ListItem("请选择"));
        //WrmBgTimeHr.Items.Insert(0, new ListItem("请选择"));
        //WrmEnTimeHr.Items.Insert(0, new ListItem("请选择"));

        for (int i = 0; i < 60; i++)
        {
            DeBgTimeMin.Items.Add(i.ToString("00"));
            DeEnTimeMin.Items.Add(i.ToString("00"));
            DaBgTimeMin.Items.Add(i.ToString("00"));
            DaEnTimeMin.Items.Add(i.ToString("00"));
            WdBgTimeMin.Items.Add(i.ToString("00"));
            WdEnTimeMin.Items.Add(i.ToString("00"));
            WrmBgTimeMin.Items.Add(i.ToString("00"));
            WrmEnTimeMin.Items.Add(i.ToString("00"));
        }//min

        //DeBgTimeMin.Items.Insert(0, new ListItem("请选择"));
        //DeEnTimeMin.Items.Insert(0, new ListItem("请选择"));
        //DaBgTimeMin.Items.Insert(0, new ListItem("请选择"));
        //DaEnTimeMin.Items.Insert(0, new ListItem("请选择"));
        //WdBgTimeMin.Items.Insert(0, new ListItem("请选择"));
        //WdEnTimeMin.Items.Insert(0, new ListItem("请选择"));
        //WrmBgTimeMin.Items.Insert(0, new ListItem("请选择"));
        //WrmEnTimeMin.Items.Insert(0, new ListItem("请选择"));

        for (int i = 0; i < 60; i++)
        {
            DeBgTimeSec.Items.Add(i.ToString("00"));
            DeEnTimeSec.Items.Add(i.ToString("00"));
            DaBgTimeSec.Items.Add(i.ToString("00"));
            DaEnTimeSec.Items.Add(i.ToString("00"));
            WdBgTimeSec.Items.Add(i.ToString("00"));
            WdEnTimeSec.Items.Add(i.ToString("00"));
            WrmBgTimeSec.Items.Add(i.ToString("00"));
            WrmEnTimeSec.Items.Add(i.ToString("00"));
        }//sec

        //DeBgTimeSec.Items.Insert(0, new ListItem("请选择"));
        //DeEnTimeSec.Items.Insert(0, new ListItem("请选择"));
        //DaBgTimeSec.Items.Insert(0, new ListItem("请选择"));
        //DaEnTimeSec.Items.Insert(0, new ListItem("请选择"));
        //WdBgTimeSec.Items.Insert(0, new ListItem("请选择"));
        //WdEnTimeSec.Items.Insert(0, new ListItem("请选择"));
        //WrmBgTimeSec.Items.Insert(0, new ListItem("请选择"));
        //WrmEnTimeSec.Items.Insert(0, new ListItem("请选择"));
    }

    public void BankDropDownList(string Item)
    {
        DeBgTimeTable.Visible = false; DeEnTimeTable.Visible = false; DaBgTimeTable.Visible = false; DaEnTimeTable.Visible = false;
        WdBgTimeTable.Visible = false; WdEnTimeTable.Visible = false; WrmBgTimeTable.Visible = false; WrmEnTimeTable.Visible = false;

        try
        {
            //string[] BankNote = Item.Split('(');

            var query = from stu in BanklistData.Banks
                        where stu.BankID.ToString() == Item
                        select stu;

            foreach (var q in query)
            {
                string[] DeBgTime = q.DeBgTime.Split(' ');
                string[] DeBgTimeData = DeBgTime[0].Split('-');
                string[] DeBgTimeTime = DeBgTime[1].Split(':');

                DeBgTimeTable.Visible = true;
                DeBgTimeYear.Text = DeBgTimeData[0];
                DeBgTimeMonth.Text = DeBgTimeData[1];
                DeBgTimeDay.Text = DeBgTimeData[2];
                DeBgTimeHr.Text = DeBgTimeTime[0];
                DeBgTimeMin.Text = DeBgTimeTime[1];
                DeBgTimeSec.Text = DeBgTimeTime[2];

                string[] DeEnTime = q.DeEnTime.Split(' ');
                string[] DeEnTimeData = DeEnTime[0].Split('-');
                string[] DeEnTimeTime = DeEnTime[1].Split(':');

                DeEnTimeTable.Visible = true;
                DeEnTimeYear.Text = DeEnTimeData[0];
                DeEnTimeMonth.Text = DeEnTimeData[1];
                DeEnTimeDay.Text = DeEnTimeData[2];

                DeEnTimeHr.Text = DeEnTimeTime[0];
                DeEnTimeMin.Text = DeEnTimeTime[1];
                DeEnTimeSec.Text = DeEnTimeTime[2];

                string[] WdBgTime = q.WdBgTime.Split(' ');
                string[] WdBgTimeData = WdBgTime[0].Split('-');
                string[] WdBgTimeTime = WdBgTime[1].Split(':');

                WdBgTimeTable.Visible = true;
                WdBgTimeYear.Text = WdBgTimeData[0];
                WdBgTimeMonth.Text = WdBgTimeData[1];
                WdBgTimeDay.Text = WdBgTimeData[2];

                WdBgTimeHr.Text = WdBgTimeTime[0];
                WdBgTimeMin.Text = WdBgTimeTime[1];
                WdBgTimeSec.Text = WdBgTimeTime[2];

                string[] WdEnTime = q.WdEnTime.Split(' ');
                string[] WdEnTimeData = WdEnTime[0].Split('-');
                string[] WdEnTimeTime = WdEnTime[1].Split(':');

                WdEnTimeTable.Visible = true;
                WdEnTimeYear.Text = WdEnTimeData[0];
                WdEnTimeMonth.Text = WdEnTimeData[1];
                WdEnTimeDay.Text = WdEnTimeData[2];

                WdEnTimeHr.Text = WdEnTimeTime[0];
                WdEnTimeMin.Text = WdEnTimeTime[1];
                WdEnTimeSec.Text = WdEnTimeTime[2];

                ///////////////////////////////////////////////////////////

                string[] DaBgTime = q.DaBgTime.Split(':');
                DaBgTimeTable.Visible = true;
                DaBgTimeHr.Text = DaBgTime[0];
                DaBgTimeMin.Text = DaBgTime[1];
                DaBgTimeSec.Text = DaBgTime[2];

                string[] DaEnTime = q.DaEnTime.Split(':');
                DaEnTimeTable.Visible = true;
                DaEnTimeHr.Text = DaEnTime[0];
                DaEnTimeMin.Text = DaEnTime[1];
                DaEnTimeSec.Text = DaEnTime[2];

                string[] WrmBgTime = q.WrmBgTime.Split(':');
                WrmBgTimeTable.Visible = true;
                WrmBgTimeHr.Text = WrmBgTime[0];
                WrmBgTimeMin.Text = WrmBgTime[1];
                WrmBgTimeSec.Text = WrmBgTime[2];

                string[] WrmEnTime = q.WrmEnTime.Split(':');
                WrmEnTimeTable.Visible = true;
                WrmEnTimeHr.Text = WrmEnTime[0];
                WrmEnTimeMin.Text = WrmEnTime[1];
                WrmEnTimeSec.Text = WrmEnTime[2];
            }

            #region OLD

            //List<string> BanklistDataProp = type.GetProperties().Select(x => x.Name).ToList();

            //for (int i = 0; i < BanklistData.Banks.Count; i++)
            //{
            //    SplitStringDate = bankFunction.SplitString(BanklistData.Banks[i], BanklistDataProp);

            //    if (BanklistData.Banks[i].Note == BankNote[0])
            //    {
            //        for (int j = 0; j < BanklistDataProp.Count; j++)
            //        {
            //            if (SplitStringDate[BankNote[0]].ContainsKey(BanklistDataProp[j]))
            //            {
            //                MinItem.Add(BanklistDataProp[j]);
            //                Datas.Add(SplitStringDate[BankNote[0]][BanklistDataProp[j]]);
            //            }
            //        }
            //        break;
            //    }
            //}

            //for (int i = 0; i < MinItem.Count; i++)
            //{
            //    switch (MinItem[i])
            //    {
            //        case "DeBgTime":
            //            //DeBgTime = "存款开始";
            //            DeBgTimeTable.Visible = true;
            //            TextBox1.Text = Datas[i][0][0];
            //            TextBox2.Text = Datas[i][0][1];
            //            TextBox3.Text = Datas[i][0][2];
            //            TextBox4.Text = Datas[i][1][0];
            //            TextBox5.Text = Datas[i][1][1];
            //            TextBox6.Text = Datas[i][1][2];
            //            break;

            //        case "DeEnTime":
            //            //DeEnTime = "存结束";
            //            DeEnTimeTable.Visible = true;
            //            TextBox7.Text = Datas[i][0][0];
            //            TextBox8.Text = Datas[i][0][1];
            //            TextBox9.Text = Datas[i][0][2];
            //            TextBox10.Text = Datas[i][1][0];
            //            TextBox11.Text = Datas[i][1][1];
            //            TextBox12.Text = Datas[i][1][2];
            //            break;

            //        case "DaBgTime":
            //            //DaBgTime = "定期开始";
            //            DaBgTimeTable.Visible = true;
            //            TextBox13.Text = Datas[i][0][0];
            //            TextBox14.Text = Datas[i][0][1];
            //            TextBox15.Text = Datas[i][0][2];
            //            break;

            //        case "DaEnTime":
            //            //DaEnTime = "定期结束";
            //            DaEnTimeTable.Visible = true;
            //            TextBox16.Text = Datas[i][0][0];
            //            TextBox17.Text = Datas[i][0][1];
            //            TextBox18.Text = Datas[i][0][2];
            //            break;

            //        case "WdBgTime":
            //            //WdBgTime = "提开始";
            //            WdBgTimeTable.Visible = true;
            //            TextBox19.Text = Datas[i][0][0];
            //            TextBox20.Text = Datas[i][0][1];
            //            TextBox21.Text = Datas[i][0][2];
            //            TextBox22.Text = Datas[i][1][0];
            //            TextBox23.Text = Datas[i][1][1];
            //            TextBox24.Text = Datas[i][1][2];
            //            break;

            //        case "WdEnTime":
            //            //WdEnTime = "提结束";
            //            WdEnTimeTable.Visible = true;
            //            TextBox25.Text = Datas[i][0][0];
            //            TextBox26.Text = Datas[i][0][1];
            //            TextBox27.Text = Datas[i][0][2];
            //            TextBox28.Text = Datas[i][1][0];
            //            TextBox29.Text = Datas[i][1][1];
            //            TextBox30.Text = Datas[i][1][2];
            //            break;

            //        case "WrmBgTime":
            //            //WrmBgTime = "定期开始(提)";
            //            WrmBgTimeTable.Visible = true;
            //            TextBox31.Text = Datas[i][0][0];
            //            TextBox32.Text = Datas[i][0][1];
            //            TextBox33.Text = Datas[i][0][2];
            //            break;

            //        case "WrmEnTime":
            //            //WrmEnTime = "定期开始(提)";
            //            WrmEnTimeTable.Visible = true;
            //            TextBox34.Text = Datas[i][0][0];
            //            TextBox35.Text = Datas[i][0][1];
            //            TextBox36.Text = Datas[i][0][2];
            //            break;

            //        default:
            //            break;
            //    }
            //}

            #endregion OLD
        }
        catch (Exception ex)
        {
            using (DbService db = new DbService(reg.SqlConn))
            {
                db.Log("MaintainChannel", String.Format("System Error Message_{0}_(DD1)", ex));
            }
            Label1.Text = "DropDownList Load File Fail";
        }
    }

    protected void DeBgTimeYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        Item = DropDownList1.SelectedItem.Text;
        int DeBg = System.DateTime.DaysInMonth(int.Parse(DeBgTimeYear.SelectedItem.Text), int.Parse(DeBgTimeMonth.SelectedItem.Text));
        string day = DeBgTimeDay.Text;
        DeBgTimeDay.Items.Clear();
        for (int i = 1; i <= DeBg; i++) { DeBgTimeDay.Items.Add(i.ToString("00")); }//Day
        //DeBgTimeDay.Items.Insert(0, new ListItem("请选择"));
        DeBgTimeDay.Text = day;
    }

    protected void DeBgTimeMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        Item = DropDownList1.SelectedItem.Text;
        int DeBg = System.DateTime.DaysInMonth(int.Parse(DeBgTimeYear.SelectedItem.Text), int.Parse(DeBgTimeMonth.SelectedItem.Text));
        string day = DeBgTimeDay.Text;
        DeBgTimeDay.Items.Clear();
        for (int i = 1; i <= DeBg; i++) { DeBgTimeDay.Items.Add(i.ToString("00")); }//Day
        //DeBgTimeDay.Items.Insert(0, new ListItem("请选择"));
        DeBgTimeDay.Text = day;
    }

    protected void DeEnTimeYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        Item = DropDownList1.SelectedItem.Text;
        int DeEn = System.DateTime.DaysInMonth(int.Parse(DeEnTimeYear.Text), int.Parse(DeEnTimeMonth.Text));
        string day = DeEnTimeDay.Text;
        DeEnTimeDay.Items.Clear();
        for (int i = 1; i <= DeEn; i++) { DeEnTimeDay.Items.Add(i.ToString("00")); }//Day
        //DeEnTimeDay.Items.Insert(0, new ListItem("请选择"));
        DeEnTimeDay.Text = day;
    }

    protected void DeEnTimeMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        Item = DropDownList1.SelectedItem.Text;
        int DeEn = System.DateTime.DaysInMonth(int.Parse(DeEnTimeYear.Text), int.Parse(DeEnTimeMonth.Text));
        string day = DeEnTimeDay.Text;
        DeEnTimeDay.Items.Clear();
        for (int i = 1; i <= DeEn; i++) { DeEnTimeDay.Items.Add(i.ToString("00")); }//Day
        //DeEnTimeDay.Items.Insert(0, new ListItem("请选择"));
        DeEnTimeDay.Text = day;
    }

    protected void WdBgTimeYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        Item = DropDownList1.SelectedItem.Text;
        int WdBg = System.DateTime.DaysInMonth(int.Parse(WdBgTimeYear.Text), int.Parse(WdBgTimeMonth.Text));
        string day = WdBgTimeDay.Text;
        WdBgTimeDay.Items.Clear();
        for (int i = 1; i <= WdBg; i++) { WdBgTimeDay.Items.Add(i.ToString("00")); }//Day
        //WdBgTimeDay.Items.Insert(0, new ListItem("请选择"));
        WdBgTimeDay.Text = day;
    }

    protected void WdBgTimeMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        Item = DropDownList1.SelectedItem.Text;
        int WdBg = System.DateTime.DaysInMonth(int.Parse(WdBgTimeYear.Text), int.Parse(WdBgTimeMonth.Text));
        string day = WdBgTimeDay.Text;
        WdBgTimeDay.Items.Clear();
        for (int i = 1; i <= WdBg; i++) { WdBgTimeDay.Items.Add(i.ToString("00")); }//Day
        //WdBgTimeDay.Items.Insert(0, new ListItem("请选择"));
        WdBgTimeDay.Text = day;
    }

    protected void WdEnTimeYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        Item = DropDownList1.SelectedItem.Text;
        int WdEn = System.DateTime.DaysInMonth(int.Parse(WdEnTimeYear.Text), int.Parse(WdEnTimeMonth.Text));
        string day = WdEnTimeDay.Text;
        WdEnTimeDay.Items.Clear();
        for (int i = 1; i <= WdEn; i++) { WdEnTimeDay.Items.Add(i.ToString("00")); }//Day
        //WdEnTimeDay.Items.Insert(0, new ListItem("请选择"));
        WdEnTimeDay.Text = day;
    }

    protected void WdEnTimeMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        Item = DropDownList1.SelectedItem.Text;
        int WdEn = System.DateTime.DaysInMonth(int.Parse(WdEnTimeYear.Text), int.Parse(WdEnTimeMonth.Text));
        string day = WdEnTimeDay.Text;
        WdEnTimeDay.Items.Clear();
        for (int i = 1; i <= WdEn; i++) { WdEnTimeDay.Items.Add(i.ToString("00")); }//Day
        //WdEnTimeDay.Items.Insert(0, new ListItem("请选择"));
        WdEnTimeDay.Text = day;
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (SettingDataBase.CkeckPos())
        {
            Label1.Text = "";
            Internal.Visible = true;
            Table1.Visible = true;
            Button2.Enabled = true;
            Item = DropDownList1.SelectedItem.Text;
            string ItemValue = DropDownList1.SelectedItem.Value;
            BankDropDownList(ItemValue);
        }
        else
        {
            Label1.Text = "档案读取失败";
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        DropDownList1.Text = "请选择";
        if (SettingDataBase.CkeckPos())
        {
            Internal.Visible = true;
            Table1.Visible = false;
            Table2.Visible = true;

            DeBgTimeTable.Visible = false;
            DeEnTimeTable.Visible = false;
            DaBgTimeTable.Visible = false;
            DaEnTimeTable.Visible = false;
            WdBgTimeTable.Visible = false;
            WdEnTimeTable.Visible = false;
            WrmBgTimeTable.Visible = false;
            WrmEnTimeTable.Visible = false;

            Button2.Enabled = false;
            BanklistData = SettingDataBase.LoadData();
            Dictionary<string, string> Name = new Dictionary<string, string>();

            try
            {
                List<string> BanklistDataProp = type.GetProperties().Select(x => x.Name).ToList();

                foreach (var item in BanklistDataProp)
                {
                    switch (item)
                    {
                        case "DeBgTime":
                            Name.Add(item, "存款开始");
                            break;

                        case "DeEnTime":
                            Name.Add(item, "存款结束");
                            break;

                        case "DaBgTime":
                            Name.Add(item, "定期存款开始");
                            break;

                        case "DaEnTime":
                            Name.Add(item, "定期存款结束");
                            break;

                        case "WdBgTime":
                            Name.Add(item, "提款开始");
                            break;

                        case "WdEnTime":
                            Name.Add(item, "提款结束");
                            break;

                        case "WrmBgTime":
                            Name.Add(item, "定期提款开始");
                            break;

                        case "WrmEnTime":
                            Name.Add(item, "定期提款结束");
                            break;
                    }
                }

                foreach (var item in BanklistData.Banks)
                {
                    SplitStringDate = bankFunction.SplitString(item, BanklistDataProp);

                    for (int j = 0; j < BanklistDataProp.Count; j++)
                    {
                        if (SplitStringDate[item.BankID.ToString()].ContainsKey(BanklistDataProp[j]))
                        {
                            MinItem.Add(BanklistDataProp[j]);
                            Datas.Add(SplitStringDate[item.BankID.ToString()][BanklistDataProp[j]]);
                        }
                    }

                    TableHeaderRow hr = new TableHeaderRow();
                    TableHeaderCell hc1 = new TableHeaderCell();
                    TableHeaderCell hc2 = new TableHeaderCell();
                    //TableHeaderCell hc3 = new TableHeaderCell();

                    hc1.CssClass = "title";
                    hc2.CssClass = "title";
                    //hc3.CssClass = "title";

                    hc1.Text = item.Note;
                    hc2.Text = "维护时间";
                    //hc3.Text = "提示";

                    hr.Cells.Add(hc1);
                    hr.Cells.Add(hc2);
                    //hr.Cells.Add(hc3);

                    Table2.Rows.Add(hr);//标头
                    Table2.Rows.Add(hr);
                    //Table2.Rows.Add(hr);

                    for (int z = 0; z < MinItem.Count; z++)
                    {
                        TableRow Row1 = new TableRow();
                        TableCell cell1 = new TableCell();
                        cell1.Text = Name[MinItem[z]];
                        Row1.Cells.Add(cell1);
                        Table2.Rows.Add(Row1);

                        TableCell cell2 = new TableCell();

                        if (MinItem[z] == "DaBgTime" || MinItem[z] == "DaEnTime" || MinItem[z] == "WrmBgTime" || MinItem[z] == "WrmEnTime")
                        {
                            Label label = new Label();
                            label.Text = " : ";
                            label.Font.Size = 10;

                            Label labe2 = new Label();
                            labe2.Text = " : ";
                            labe2.Font.Size = 10;

                            TextBox textBox1 = new TextBox();
                            textBox1.Width = Unit.Pixel(100);
                            textBox1.MaxLength = 2;
                            textBox1.Enabled = false;

                            TextBox textBox2 = new TextBox();
                            textBox2.Width = Unit.Pixel(100);
                            textBox2.MaxLength = 2;
                            textBox2.Enabled = false;

                            TextBox textBox3 = new TextBox();
                            textBox3.Width = Unit.Pixel(100);
                            textBox3.MaxLength = 2;
                            textBox3.Enabled = false;

                            switch (MinItem[z])
                            {
                                case "DaBgTime":
                                    textBox1.Text = Datas[z][0][0];
                                    textBox2.Text = Datas[z][0][1];
                                    textBox3.Text = Datas[z][0][2];
                                    break;

                                case "DaEnTime":

                                    textBox1.Text = Datas[z][0][0];
                                    textBox2.Text = Datas[z][0][1];
                                    textBox3.Text = Datas[z][0][2];
                                    break;

                                case "WrmBgTime":

                                    textBox1.Text = Datas[z][0][0];
                                    textBox2.Text = Datas[z][0][1];
                                    textBox3.Text = Datas[z][0][2];
                                    break;

                                case "WrmEnTime":

                                    textBox1.Text = Datas[z][0][0];
                                    textBox2.Text = Datas[z][0][1];
                                    textBox3.Text = Datas[z][0][2];
                                    break;

                                default:
                                    break;
                            }

                            cell2.Controls.Add(textBox1);
                            cell2.Controls.Add(label);
                            cell2.Controls.Add(textBox2);
                            cell2.Controls.Add(labe2);
                            cell2.Controls.Add(textBox3);
                        }
                        else
                        {
                            Label label = new Label();
                            label.Text = " : ";
                            label.Font.Size = 10;

                            Label labe2 = new Label();
                            labe2.Text = " : ";
                            labe2.Font.Size = 10;

                            Label labe3 = new Label();
                            labe3.Text = " - ";
                            labe3.Font.Size = 10;

                            Label labe4 = new Label();
                            labe4.Text = " - ";
                            labe4.Font.Size = 10;

                            Label labe5 = new Label();
                            labe5.Text = "  ";

                            TextBox textBox1 = new TextBox();
                            textBox1.Width = Unit.Pixel(100);
                            textBox1.MaxLength = 4;
                            textBox1.Enabled = false;

                            TextBox textBox2 = new TextBox();
                            textBox2.Width = Unit.Pixel(100);
                            textBox2.MaxLength = 2;
                            textBox2.Enabled = false;

                            TextBox textBox3 = new TextBox();
                            textBox3.Width = Unit.Pixel(100);
                            textBox3.MaxLength = 2;
                            textBox3.Enabled = false;

                            TextBox textBox4 = new TextBox();
                            textBox4.Width = Unit.Pixel(100);
                            textBox4.MaxLength = 2;
                            textBox4.Enabled = false;

                            TextBox textBox5 = new TextBox();
                            textBox5.Width = Unit.Pixel(100);
                            textBox5.MaxLength = 2;
                            textBox5.Enabled = false;

                            TextBox textBox6 = new TextBox();
                            textBox6.Width = Unit.Pixel(100);
                            textBox6.MaxLength = 2;
                            textBox6.Enabled = false;

                            switch (MinItem[z])
                            {
                                case "DeBgTime":

                                    textBox1.Text = Datas[z][0][0];
                                    textBox2.Text = Datas[z][0][1];
                                    textBox3.Text = Datas[z][0][2];
                                    textBox4.Text = Datas[z][1][0];
                                    textBox5.Text = Datas[z][1][1];
                                    textBox6.Text = Datas[z][1][2];
                                    break;

                                case "DeEnTime":

                                    textBox1.Text = Datas[z][0][0];
                                    textBox2.Text = Datas[z][0][1];
                                    textBox3.Text = Datas[z][0][2];
                                    textBox4.Text = Datas[z][1][0];
                                    textBox5.Text = Datas[z][1][1];
                                    textBox6.Text = Datas[z][1][2];
                                    break;

                                case "WdBgTime":

                                    textBox1.Text = Datas[z][0][0];
                                    textBox2.Text = Datas[z][0][1];
                                    textBox3.Text = Datas[z][0][2];
                                    textBox4.Text = Datas[z][1][0];
                                    textBox5.Text = Datas[z][1][1];
                                    textBox6.Text = Datas[z][1][2];
                                    break;

                                case "WdEnTime":

                                    textBox1.Text = Datas[z][0][0];
                                    textBox2.Text = Datas[z][0][1];
                                    textBox3.Text = Datas[z][0][2];
                                    textBox4.Text = Datas[z][1][0];
                                    textBox5.Text = Datas[z][1][1];
                                    textBox6.Text = Datas[z][1][2];
                                    break;

                                default:
                                    break;
                            }

                            cell2.Controls.Add(textBox1);
                            cell2.Controls.Add(labe3);
                            cell2.Controls.Add(textBox2);
                            cell2.Controls.Add(labe4);
                            cell2.Controls.Add(textBox3);

                            cell2.Controls.Add(labe5);

                            cell2.Controls.Add(textBox4);
                            cell2.Controls.Add(label);
                            cell2.Controls.Add(textBox5);
                            cell2.Controls.Add(labe2);
                            cell2.Controls.Add(textBox6);
                        }

                        Row1.Cells.Add(cell2);
                        Table2.Rows.Add(Row1);
                    }
                    MinItem.Clear();
                    Datas.Clear();
                }
            }
            catch (Exception ex)
            {
                using (DbService db = new DbService(reg.SqlConn))
                {
                    db.Log("MaintainChannel", String.Format("System Error Message_{0}_(B1)", ex));
                }
                Label1.Text = "Show File Data Fail";
            }
        }
        else
        {
            Label1.Text = "档案读取失败";
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (SettingDataBase.CkeckPos())
        {
            Item = DropDownList1.SelectedItem.Text;
            string ItemValue = DropDownList1.SelectedItem.Value;
            if (Item == "请选择") { return; }

            //////////////////////////////////////////////////////year
            value.Add(DeBgTimeYear.Text); value.Add(DeEnTimeYear.Text); value.Add(WdBgTimeYear.Text); value.Add(WdEnTimeYear.Text);
            //////////////////////////////////////////////////////month
            value.Add(DeBgTimeMonth.Text); value.Add(DeEnTimeMonth.Text); value.Add(WdBgTimeMonth.Text); value.Add(WdEnTimeMonth.Text);
            //////////////////////////////////////////////////////date
            value.Add(DeBgTimeDay.Text); value.Add(DeEnTimeDay.Text); value.Add(WdBgTimeDay.Text); value.Add(WdEnTimeDay.Text);
            //////////////////////////////////////////////////////hr
            value.Add(DeBgTimeHr.Text); value.Add(DeEnTimeHr.Text); value.Add(DaBgTimeHr.Text); value.Add(DaEnTimeHr.Text);
            value.Add(WdBgTimeHr.Text); value.Add(WdEnTimeHr.Text); value.Add(WrmBgTimeHr.Text); value.Add(WrmEnTimeHr.Text);
            ///////////////////////////////////////////////////////min
            value.Add(DeBgTimeMin.Text); value.Add(DeEnTimeMin.Text); value.Add(DaBgTimeMin.Text); value.Add(DaEnTimeMin.Text);
            value.Add(WdBgTimeMin.Text); value.Add(WdEnTimeMin.Text); value.Add(WrmBgTimeMin.Text); value.Add(WrmEnTimeMin.Text);
            //////////////////////////////////////////////////////sec
            value.Add(DeBgTimeSec.Text); value.Add(DeEnTimeSec.Text); value.Add(DaBgTimeSec.Text); value.Add(DaEnTimeSec.Text);
            value.Add(WdBgTimeSec.Text); value.Add(WdEnTimeSec.Text); value.Add(WrmBgTimeSec.Text); value.Add(WrmEnTimeSec.Text);
            ////////////////////////////////////////////////////
            try
            {
                //string[] BankNote = Item.Split('(');
                List<string> BanklistDataProp = type.GetProperties().Select(x => x.Name).ToList();
                bool DeBgTime = false; bool DeEnTime = false; bool DaBgTime = false; bool DaEnTime = false;
                bool WdBgTime = false; bool WdEnTime = false; bool WrmBgTime = false; bool WrmEnTime = false;
                bool EnTimeLessBgTime = true;
                bool DeBgTimeNotTheSame = false; bool DeEnTimeNotTheSame = false; bool DaBgTimeNotTheSame = false; bool DaEnTimeNotTheSame = false;
                bool WdBgTimeNotTheSame = false; bool WdEnTimeNotTheSame = false; bool WrmBgTimeNotTheSame = false; bool WrmEnTimeNotTheSame = false;

                string JsonUrl = (reg.JsonUrl.Substring(0, reg.JsonUrl.Length - 1) == "\\") ? reg.JsonUrl : (reg.JsonUrl + "\\");
                string JsonPath = string.Format(@"{0}{1}.json", JsonUrl, "Gw_BankUpHold");
                SaveBankList = SettingDataBase.SaveBankOriginal(JsonPath);

                try
                {
                    for (int i = 0; i < BanklistData.Banks.Count; i++)
                    {
                        SplitStringDate = bankFunction.SplitString(BanklistData.Banks[i], BanklistDataProp);

                        //if (BanklistData.Banks[i].Note == BankNote[0])
                        if (ItemValue == BanklistData.Banks[i].BankID.ToString())
                        {
                            for (int j = 2; j < BanklistDataProp.Count; j++)
                            {
                                if (SplitStringDate[BanklistData.Banks[i].BankID.ToString()].ContainsKey(BanklistDataProp[j]))
                                {
                                    switch (BanklistDataProp[j])
                                    {
                                        case "DeBgTime":

                                            if (SaveBankList.Banks[i].DeBgTime != string.Format("{0}-{1}-{2} {3}:{4}:{5}", value[0], value[4], value[8], value[12], value[20], value[28]))
                                            {
                                                DeBgTimeNotTheSame = false;
                                                //if (long.Parse(string.Format("{0}{1}{2}", value[0], value[4], value[8])) >= long.Parse(DateTime.Now.ToString("yyyyMMdd")))
                                                //{
                                                DeBgTime = true;
                                                BanklistData.Banks[i].DeBgTime = string.Format("{0}-{1}-{2} {3}:{4}:{5}", value[0], value[4], value[8], value[12], value[20], value[28]);
                                                using (DbService db = new DbService(reg.SqlConn))
                                                {
                                                    db.Log("MaintainChannel", string.Format("MaintainChannel[{0}-{1}] Transfer[{2} => {3}] by [{4}]", BanklistData.Banks[i].Note,
                                                        BanklistDataProp[j], SaveBankList.Banks[i].DeBgTime, BanklistData.Banks[i].DeBgTime, LoginInfo.UserName));
                                                }
                                                //}
                                                //else
                                                //{
                                                //    FailInformationDeBgTimeTable = "存款开始日期小于今日日期(年月日)";
                                                //}
                                            }
                                            else
                                            {
                                                DeBgTime = true;
                                                DeBgTimeNotTheSame = true;
                                            }

                                            break;

                                        case "DeEnTime":

                                            if (SaveBankList.Banks[i].DeEnTime != string.Format("{0}-{1}-{2} {3}:{4}:{5}", value[1], value[5], value[9], value[13], value[21], value[29]))
                                            {
                                                DeEnTimeNotTheSame = false;
                                                if (long.Parse(string.Format("{0}{1}{2}", value[1], value[5], value[9])) < long.Parse(string.Format("{0}{1}{2}", value[0], value[4], value[8])))
                                                {
                                                    FailInformationDeEnTimeTable = "存款结束日期小于存款开始日期(年月日)";
                                                    EnTimeLessBgTime = false;
                                                }
                                                else
                                                {
                                                    //if (long.Parse(string.Format("{0}{1}{2}", value[1], value[5], value[9])) >= long.Parse(DateTime.Now.ToString("yyyyMMdd")))
                                                    //{
                                                    DeEnTime = true;
                                                    BanklistData.Banks[i].DeEnTime = string.Format("{0}-{1}-{2} {3}:{4}:{5}", value[1], value[5], value[9], value[13], value[21], value[29]);
                                                    using (DbService db = new DbService(reg.SqlConn))
                                                    {
                                                        db.Log("MaintainChannel", string.Format("MaintainChannel[{0}-{1}] Transfer[{2} => {3}] by [{4}]", BanklistData.Banks[i].Note,
                                                            BanklistDataProp[j], SaveBankList.Banks[i].DeEnTime, BanklistData.Banks[i].DeEnTime, LoginInfo.UserName));
                                                    }
                                                    //}
                                                    //else
                                                    //{
                                                    //    FailInformationDeEnTimeTable = "存款结束日期小于今日日期(年月日)";
                                                    //}
                                                }
                                            }
                                            else
                                            {
                                                DeEnTime = true;
                                                DeEnTimeNotTheSame = true;
                                                if (long.Parse(string.Format("{0}{1}{2}", value[1], value[5], value[9])) < long.Parse(string.Format("{0}{1}{2}", value[0], value[4], value[8])))
                                                {
                                                    DeEnTime = false;
                                                    FailInformationDeEnTimeTable = "存款结束日期小于存款开始日期(年月日)";
                                                    EnTimeLessBgTime = false;
                                                }
                                            }

                                            break;

                                        case "DaBgTime":

                                            if (SaveBankList.Banks[i].DaBgTime != string.Format("{0}:{1}:{2}", value[14], value[22], value[30]))
                                            {
                                                DaBgTimeNotTheSame = false;
                                                DaBgTime = true;
                                                BanklistData.Banks[i].DaBgTime = string.Format("{0}:{1}:{2}", value[14], value[22], value[30]);
                                                using (DbService db = new DbService(reg.SqlConn))
                                                {
                                                    db.Log("MaintainChannel", string.Format("MaintainChannel[{0}-{1}] Transfer[{2} => {3}] by [{4}]", BanklistData.Banks[i].Note,
                                                        BanklistDataProp[j], SaveBankList.Banks[i].DaBgTime, BanklistData.Banks[i].DaBgTime, LoginInfo.UserName));
                                                }
                                            }
                                            else
                                            {
                                                DaBgTime = true;
                                                DaBgTimeNotTheSame = true;
                                            }
                                            break;

                                        case "DaEnTime":

                                            if (SaveBankList.Banks[i].DaEnTime != string.Format("{0}:{1}:{2}", value[15], value[23], value[31]))
                                            {
                                                DaEnTimeNotTheSame = false;
                                                DaEnTime = true;
                                                BanklistData.Banks[i].DaEnTime = string.Format("{0}:{1}:{2}", value[15], value[23], value[31]);
                                                using (DbService db = new DbService(reg.SqlConn))
                                                {
                                                    db.Log("MaintainChannel", string.Format("MaintainChannel[{0}-{1}] Transfer[{2} => {3}] by [{4}]", BanklistData.Banks[i].Note,
                                                        BanklistDataProp[j], SaveBankList.Banks[i].DaEnTime, BanklistData.Banks[i].DaEnTime, LoginInfo.UserName));
                                                }
                                            }
                                            else
                                            {
                                                DaEnTime = true;
                                                DaEnTimeNotTheSame = true;
                                            }
                                            break;

                                        case "WdBgTime":

                                            if (SaveBankList.Banks[i].WdBgTime != string.Format("{0}-{1}-{2} {3}:{4}:{5}", value[2], value[6], value[10], value[16], value[24], value[32]))
                                            {
                                                WdBgTimeNotTheSame = false;
                                                //if (long.Parse(string.Format("{0}{1}{2}", value[2], value[6], value[10])) >= long.Parse(DateTime.Now.ToString("yyyyMMdd")))
                                                //{
                                                WdBgTime = true;
                                                BanklistData.Banks[i].WdBgTime = string.Format("{0}-{1}-{2} {3}:{4}:{5}", value[2], value[6], value[10], value[16], value[24], value[32]);
                                                using (DbService db = new DbService(reg.SqlConn))
                                                {
                                                    db.Log("MaintainChannel", string.Format("MaintainChannel[{0}-{1}] Transfer[{2} => {3}] by [{4}]", BanklistData.Banks[i].Note,
                                                        BanklistDataProp[j], SaveBankList.Banks[i].WdBgTime, BanklistData.Banks[i].WdBgTime, LoginInfo.UserName));
                                                }
                                                //}
                                                //else
                                                //{
                                                //    FailInformationWdBgTimeTable = "提款开始日期小于今日日期(年月日)";
                                                //}
                                            }
                                            else
                                            {
                                                WdBgTime = true;
                                                WdBgTimeNotTheSame = true;
                                            }
                                            break;

                                        case "WdEnTime":

                                            if (SaveBankList.Banks[i].WdEnTime != string.Format("{0}-{1}-{2} {3}:{4}:{5}", value[3], value[7], value[11], value[17], value[25], value[33]))
                                            {
                                                WdEnTimeNotTheSame = false;
                                                if (long.Parse(string.Format("{0}{1}{2}", value[3], value[7], value[11])) < long.Parse(string.Format("{0}{1}{2}", value[2], value[6], value[10])))
                                                {
                                                    FailInformationWdEnTimeTable = "提款结束日期小于提款开始日期(年月日)";
                                                    EnTimeLessBgTime = false;
                                                }
                                                else
                                                {
                                                    //if (long.Parse(string.Format("{0}{1}{2}", value[3], value[7], value[11])) >= long.Parse(DateTime.Now.ToString("yyyyMMdd")))
                                                    //{
                                                    WdEnTime = true;
                                                    BanklistData.Banks[i].WdEnTime = string.Format("{0}-{1}-{2} {3}:{4}:{5}", value[3], value[7], value[11], value[17], value[25], value[33]);
                                                    using (DbService db = new DbService(reg.SqlConn))
                                                    {
                                                        db.Log("MaintainChannel", string.Format("MaintainChannel[{0}-{1}] Transfer[{2} => {3}] by [{4}]", BanklistData.Banks[i].Note,
                                                            BanklistDataProp[j], SaveBankList.Banks[i].WdEnTime, BanklistData.Banks[i].WdEnTime, LoginInfo.UserName));
                                                    }
                                                    //}
                                                    //else
                                                    //{
                                                    //    FailInformationWdEnTimeTable = "提款结束日期小于今日日期(年月日)";
                                                    //}
                                                }
                                            }
                                            else
                                            {
                                                WdEnTime = true;
                                                WdEnTimeNotTheSame = true;
                                                if (long.Parse(string.Format("{0}{1}{2}", value[3], value[7], value[11])) < long.Parse(string.Format("{0}{1}{2}", value[2], value[6], value[10])))
                                                {
                                                    WdEnTime = false;
                                                    FailInformationWdEnTimeTable = "提款结束日期小于提款开始日期(年月日)";
                                                    EnTimeLessBgTime = false;
                                                }
                                            }
                                            break;

                                        case "WrmBgTime":

                                            if (SaveBankList.Banks[i].WrmBgTime != string.Format("{0}:{1}:{2}", value[18], value[26], value[34]))
                                            {
                                                WrmBgTimeNotTheSame = false;
                                                WrmBgTime = true;
                                                BanklistData.Banks[i].WrmBgTime = string.Format("{0}:{1}:{2}", value[18], value[26], value[34]);

                                                using (DbService db = new DbService(reg.SqlConn))
                                                {
                                                    db.Log("MaintainChannel", string.Format("MaintainChannel[{0}-{1}] Transfer[{2} => {3}] by [{4}]", BanklistData.Banks[i].Note,
                                                        BanklistDataProp[j], SaveBankList.Banks[i].WrmBgTime, BanklistData.Banks[i].WrmBgTime, LoginInfo.UserName));
                                                }
                                            }
                                            else
                                            {
                                                WrmBgTime = true;
                                                WrmBgTimeNotTheSame = true;
                                            }
                                            break;

                                        case "WrmEnTime":

                                            if (SaveBankList.Banks[i].WrmEnTime != string.Format("{0}:{1}:{2}", value[19], value[27], value[35]))
                                            {
                                                WrmEnTimeNotTheSame = false;
                                                WrmEnTime = true;
                                                BanklistData.Banks[i].WrmEnTime = string.Format("{0}:{1}:{2}", value[19], value[27], value[35]);

                                                using (DbService db = new DbService(reg.SqlConn))
                                                {
                                                    db.Log("MaintainChannel", string.Format("MaintainChannel[{0}-{1}] Transfer[{2} => {3}] by [{4}]", BanklistData.Banks[i].Note,
                                                        BanklistDataProp[j], SaveBankList.Banks[i].WrmEnTime, BanklistData.Banks[i].WrmEnTime, LoginInfo.UserName));
                                                }
                                            }
                                            else
                                            {
                                                WrmEnTime = true;
                                                WrmEnTimeNotTheSame = true;
                                            }

                                            break;
                                    }
                                }
                            }
                            ////////////////////////////////////////////////////////
                            value.Clear();
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    using (DbService db = new DbService(reg.SqlConn))
                    {
                        db.Log("MaintainChannel", String.Format("System Error Message_{0}_(B2)", ex));
                    }
                    Label1.Text = "Check File Error";
                }
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                StringBuilder Sth = new StringBuilder();
                if (DeBgTimeNotTheSame && DeEnTimeNotTheSame && DaBgTimeNotTheSame && DaEnTimeNotTheSame &&
                    WdBgTimeNotTheSame && WdEnTimeNotTheSame && WrmBgTimeNotTheSame && WrmEnTimeNotTheSame)
                {
                    Label1.Text = "资料无变更";
                }
                else if (DeBgTime && DeEnTime && DaBgTime && DaEnTime && WdBgTime && WdEnTime && WrmBgTime && WrmEnTime && EnTimeLessBgTime)
                {
                    try
                    {
                        string JsonBackup = (reg.JsonBackup.Substring(0, reg.JsonBackup.Length - 1) == "\\") ? reg.JsonBackup : (reg.JsonBackup + "\\");
                        if (!Directory.Exists(JsonBackup)) { Directory.CreateDirectory(JsonBackup); }
                        string Time = DateTime.Now.ToString("yyyyMMddHHmmssff");
                        string JsonBackupPath = string.Format(@"{0}{1}_{2}.json", JsonBackup + "MaintainJsonBackUp" + "\\", "Gw_BankUpHold_Backup", Time);

                        using (StreamWriter sw = new StreamWriter(JsonBackupPath))
                        {
                            string OutPutFileCopy = JsonConvert.SerializeObject(SaveBankList, Formatting.Indented);
                            sw.WriteLine(OutPutFileCopy);
                            Sth.Append("备份成功_");
                        }
                    }
                    catch (Exception ex)
                    {
                        using (DbService db = new DbService(reg.SqlConn))
                        {
                            db.Log("MaintainChannel", String.Format("System Error Message_{0}_(B2)", ex));
                        }
                        Sth.Append("备份失敗_");
                    }

                    try
                    {
                        DirectoryInfo dic = new DirectoryInfo(JsonPath);
                        using (FileStream fs = new FileStream(dic.FullName, FileMode.Create))
                        {
                            using (StreamWriter sw = new StreamWriter(fs))
                            {
                                string OutPutFile = JsonConvert.SerializeObject(BanklistData, Formatting.Indented);
                                sw.WriteLine(OutPutFile);
                                Sth.Append("储存成功");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        using (DbService db = new DbService(reg.SqlConn))
                        {
                            db.Log("MaintainChannel", String.Format("System Error Message_{0}_(B2)", ex));
                        }
                        Sth.Append("储存失敗");
                    }

                    Label1.Text = Sth.ToString();
                }
                else
                {
                    Label1.Text = "格式錯誤";
                }

                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                using (DbService db = new DbService(reg.SqlConn))
                {
                    db.Log("MaintainChannel", String.Format("System Error Message_{0}_(B2)", ex));
                }

                Label1.Text = "Save File Error";
            }
        }
        else
        {
            Label1.Text = "档案读取失败";
        }
    }
}