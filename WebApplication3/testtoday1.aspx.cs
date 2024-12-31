﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class testtoday1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DayAttToday();
            NightAttToday();
            AbsentAttToday();
            AttTodayUpdate_C();
            AttTodayUpdate_Dayoff();
            AttlogToday();

            Console.WriteLine("Today Attendance Data First Update with record C successfully dated  ... '" + DateTime.Now + "' ");
        }
        // DAY SHIFT TODAY ATTENDANCE STARTS

        static void DayAttToday()
        {
            //  String ConnStr1 = "Data Source=172.177.184.39;Network Library=DBMSSOCN;Initial Catalog=EstimationNew;User ID=sqladmin;Password=Sqlegypt@123456;";
            string ConnStr1 = "Data Source=192.168.15.4;Network Library=DBMSSOCN;Initial Catalog=Rccattdb;User ID=sa;Password=sql@12345;";
            using (SqlConnection conn61 = new SqlConnection(ConnStr1))
            {
                DateTime todaydate = DateTime.Now;
                string todaydate1 = "2024-11-22";
                //    string MAINSQL = "SELECT A9.empid, A9.badgenumber, A9.CheckDate, A9.wsid, Ews.empname, Ews.weekday, Ews.wstype, MAX(CASE WHEN IsFirstEntry = 1 THEN Ews.checkin END) AS wsCheckIn, MAX(CASE WHEN IsLastEntry = 1 THEN Ews.checkout END) AS wsCheckOut, MAX(CASE WHEN IsFirstEntry = 1 THEN A9.CheckTime END) AS CheckInTime, MAX(CASE WHEN IsLastEntry = 1 THEN A9.CheckTime END) AS CheckOutTime FROM Attendance9 A9 INNER JOIN (SELECT empid, CheckDate, MIN(CheckTime) AS MinCheckTime, MAX(CheckTime) AS MaxCheckTime FROM Attendance9 WHERE empid = 1136 GROUP BY empid, CheckDate) AS Subquery ON A9.empid = Subquery.empid AND A9.CheckDate = Subquery.CheckDate INNER JOIN Empws Ews ON A9.empid = Ews.empid AND DATENAME(WEEKDAY, A9.CheckDate) = Ews.weekday OUTER APPLY(SELECT CASE WHEN A9.CheckTime = Subquery.MinCheckTime THEN 1 ELSE 0 END AS IsFirstEntry, CASE WHEN A9.CheckTime = Subquery.MaxCheckTime THEN 1 ELSE 0 END AS IsLastEntry) AS EntryInfo WHERE A9.empid = 1136 GROUP BY A9.empid, A9.badgenumber, A9.CheckDate, A9.wsid, Ews.empname, Ews.weekday, Ews.wstype; ";
                //   string MAINSQL = "SELECT A9.empid, A9.badgenumber, A9.CheckDate, A9.wsid, Ews.empname, Ews.weekday, Ews.wstype, MAX(CASE WHEN IsFirstEntry = 1 THEN Ews.checkin END) AS wsCheckIn, MAX(CASE WHEN IsLastEntry = 1 THEN Ews.checkout END) AS wsCheckOut, MAX(CASE WHEN IsFirstEntry = 1 THEN A9.CheckTime END) AS CheckInTime, MAX(CASE WHEN IsLastEntry = 1 THEN A9.CheckTime END) AS CheckOutTime FROM Attendance9 A9 INNER JOIN (SELECT empid, CheckDate, MIN(CheckTime) AS MinCheckTime, MAX(CheckTime) AS MaxCheckTime FROM Attendance9  GROUP BY empid, CheckDate) AS Subquery ON A9.empid = Subquery.empid AND A9.CheckDate = Subquery.CheckDate INNER JOIN Empws Ews ON A9.empid = Ews.empid AND DATENAME(WEEKDAY, A9.CheckDate) = Ews.weekday OUTER APPLY(SELECT CASE WHEN A9.CheckTime = Subquery.MinCheckTime THEN 1 ELSE 0 END AS IsFirstEntry, CASE WHEN A9.CheckTime = Subquery.MaxCheckTime THEN 1 ELSE 0 END AS IsLastEntry) AS EntryInfo GROUP BY A9.empid, A9.badgenumber, A9.CheckDate, A9.wsid, Ews.empname, Ews.weekday, Ews.wstype;";
                string MAINSQL = "SELECT A9.empid, A9.badgenumber, A9.CheckDate, A9.wsid, Ews.empname, Ews.weekday, Ews.wstype, MAX(CASE WHEN IsFirstEntry = 1 THEN Ews.checkin END) AS wsCheckIn, MAX(CASE WHEN IsLastEntry = 1 THEN Ews.checkout END) AS wsCheckOut, MAX(CASE WHEN IsFirstEntry = 1 THEN A9.CheckTime END) AS CheckInTime, MAX(CASE WHEN IsLastEntry = 1 THEN A9.CheckTime END) AS CheckOutTime, Ews.projectname, Ews.projectlocation, Ews.devicename FROM Attendance9 A9 INNER JOIN (SELECT empid, CheckDate, MIN(CheckTime) AS MinCheckTime, MAX(CheckTime) AS MaxCheckTime FROM Attendance9  GROUP BY empid, CheckDate) AS Subquery ON A9.empid = Subquery.empid AND A9.CheckDate = Subquery.CheckDate INNER JOIN Empws Ews ON A9.empid = Ews.empid AND DATENAME(WEEKDAY, A9.CheckDate) = Ews.weekday OUTER APPLY(SELECT CASE WHEN A9.CheckTime = Subquery.MinCheckTime THEN 1 ELSE 0 END AS IsFirstEntry, CASE WHEN A9.CheckTime = Subquery.MaxCheckTime THEN 1 ELSE 0 END AS IsLastEntry) AS EntryInfo where Ews.wstype = 'D' and (Ews.dailydate = '" + todaydate1 + "' and A9.CheckDate = '" + todaydate1 + "') GROUP BY A9.empid, A9.badgenumber, A9.CheckDate, A9.wsid, Ews.empname, Ews.weekday, Ews.wstype, Ews.projectname, Ews.projectlocation, Ews.devicename;";

                SqlDataAdapter TitlesAdpt11 = new SqlDataAdapter(MAINSQL, ConnStr1);
                DataSet Titles11 = new DataSet();
                // No need to open or close the connection
                //   since the SqlDataAdapter will do this automatically.
                TitlesAdpt11.Fill(Titles11);

                conn61.Open();

                //  GridView1.DataSource = Titles1.Tables[0];
                //  GridView1.DataBind();

                if (Titles11.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow Title12 in Titles11.Tables[0].Rows)
                    {

                        //   int delaydays_ = Convert.ToInt32(Title12[1]);
                        //   string projectid_ = Title12[0].ToString();
                        //    string projectname_ = Title12[1].ToString();
                        string wstype = Title12["wstype"].ToString();
                        //if (wstype == "D")
                        //{

                        int empid = Convert.ToInt32(Title12["empid"]);
                        TimeSpan wsCheckIn = TimeSpan.Parse(Title12["wsCheckIn"].ToString());
                        TimeSpan wsCheckOut = TimeSpan.Parse(Title12["wsCheckOut"].ToString());
                        TimeSpan checkInTime = TimeSpan.Parse(Title12["CheckInTime"].ToString());
                        TimeSpan checkOutTime = TimeSpan.Parse(Title12["CheckOutTime"].ToString());

                        TimeSpan delayTime = DTCalculateDelayTime(checkInTime, wsCheckIn);
                        TimeSpan earlyLeaveTime = DTCalculateEarlyLeaveTime(checkOutTime, wsCheckOut);

                        string badgeno = Title12["badgenumber"].ToString();
                        //   string checkDate = reader.GetString(reader.GetOrdinal("CheckDate"));
                        DateTime checkDate = Convert.ToDateTime(Title12["CheckDate"]);
                        int wsid = Convert.ToInt32(Title12["wsid"]);
                        string empname = Title12["empname"].ToString();
                        string weekday = Title12["weekday"].ToString();
                        string projectname_ = Title12["projectname"].ToString();
                        string projectlocation_ = Title12["projectlocation"].ToString();
                        string devicename_ = Title12["devicename"].ToString();

                        bool isOnePunch = DTIsOnePunch(checkInTime, checkOutTime);
                        string attendanceStatus = DTCalculateAttendanceStatus(delayTime, earlyLeaveTime, isOnePunch);

                        DTInsertIntoZHR_TB_ATT_B(empid, empname, badgeno, checkDate, wsCheckIn, wsCheckOut, checkInTime, checkOutTime, delayTime, earlyLeaveTime, wsid, wstype, attendanceStatus, projectname_, projectlocation_, devicename_);



                    }
                }
            }
        }


        static TimeSpan DTGetTimeSpan(SqlDataReader reader, string columnName)
        {
            int columnIndex = reader.GetOrdinal(columnName);
            return reader.IsDBNull(columnIndex) ? TimeSpan.Zero : (reader.GetFieldType(columnIndex) == typeof(TimeSpan) ? reader.GetTimeSpan(columnIndex) : TimeSpan.Zero);
        }

        static void DTInsertIntoZHR_TB_ATT_B(int empid, string empname, string badgeno, DateTime checkDate, TimeSpan wsCheckIn, TimeSpan wsCheckOut, TimeSpan checkInTime, TimeSpan checkOutTime, TimeSpan delayTime, TimeSpan earlyLeaveTime, int wsid, string wstype, string attendanceStatus, string projectname, string projectlocation, string devicename)
        {
            string ConnStr = "Data Source=192.168.15.4;Network Library=DBMSSOCN;Initial Catalog=Rccattdb;User ID=sa;Password=sql@12345;";

            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();

                string attcmd = "INSERT into ZHR_TB_ATT_B (MANDT, tdate, pernr, record, ename, BADGE_NO, checkin, CHECKOUT, NEXTDAYIN, NEXTDAYOUT, DELAY_DUR, EARLY_L_DUR, WSNAME_SAP, WSNAME_PMO, wsid, type, PMO_REMARK, PROJECTNAME, DEVICENAME, PROJECTLOCATION) Values (@MANDT, @tdate, @pernr, @record, @ename, @BADGE_NO, @checkin, @CHECKOUT, @NEXTDAYIN, @NEXTDAYOUT, @DELAY_DUR, @EARLY_L_DUR, @WSNAME_SAP, @WSNAME_PMO, @wsid, @type, @PMO_REMARK, @PROJECTNAME, @DEVICENAME, @PROJECTLOCATION)";

                using (SqlCommand attcmd1 = new SqlCommand(attcmd, conn))
                {
                    attcmd1.Parameters.AddWithValue("@MANDT", "300");
                    attcmd1.Parameters.AddWithValue("@tdate", checkDate.ToString("yyyy-MM-dd"));
                    attcmd1.Parameters.AddWithValue("@pernr", empid);
                    attcmd1.Parameters.AddWithValue("@record", "D");
                    attcmd1.Parameters.AddWithValue("@ename", empname);
                    attcmd1.Parameters.AddWithValue("@BADGE_NO", badgeno);

                    delayTime = DTCalculateDelayTime(checkInTime, wsCheckIn);
                    earlyLeaveTime = DTCalculateEarlyLeaveTime(checkOutTime, wsCheckOut);

                    if ((checkOutTime - checkInTime).TotalMinutes < 60)
                    {
                        // Check if it is near wscheckin or wscheckout
                        if (DTIsNearWsCheckin(checkInTime, wsCheckIn))
                        {
                            attcmd1.Parameters.AddWithValue("@checkin", checkInTime.ToString(@"hh\:mm\:ss"));
                            attcmd1.Parameters.AddWithValue("@CHECKOUT", "00:00:00");
                            attcmd1.Parameters.AddWithValue("@PMO_REMARK", attendanceStatus);

                            //if (delayTime.TotalMinutes > 15)
                            //{
                            //    attcmd1.Parameters.AddWithValue("@PMO_REMARK", "Delay + One Punch");
                            //    attcmd1.Parameters.AddWithValue("@checkin", checkInTime.ToString(@"hh\:mm\:ss"));
                            //    attcmd1.Parameters.AddWithValue("@CHECKOUT", "00:00:00");
                            //}
                            //else if (delayTime.TotalMinutes < 15)
                            //{
                            //    attcmd1.Parameters.AddWithValue("@PMO_REMARK", "One Punch");
                            //    attcmd1.Parameters.AddWithValue("@checkin", checkInTime.ToString(@"hh\:mm\:ss"));
                            //    attcmd1.Parameters.AddWithValue("@CHECKOUT", "00:00:00");
                            //}
                        }
                        //else if (DIsNearWsCheckout(checkOutTime, wsCheckOut))
                        //{
                        //    attcmd1.Parameters.AddWithValue("@checkin", "00:00:00");
                        //    attcmd1.Parameters.AddWithValue("@CHECKOUT", "00:00:00");
                        ////    attcmd1.Parameters.AddWithValue("@CHECKOUT", checkOutTime.ToString(@"hh\:mm\:ss"));

                        //    if (earlyLeaveTime.TotalMinutes < 180)
                        //    {
                        //        attcmd1.Parameters.AddWithValue("@PMO_REMARK", "Early Leave + One Punch");                               
                        //    }
                        //    else if (earlyLeaveTime.TotalMinutes > 180)
                        //    {
                        //        attcmd1.Parameters.AddWithValue("@PMO_REMARK", "One Punch");                               
                        //    }
                        //}
                        else
                        {
                            attcmd1.Parameters.AddWithValue("@checkin", checkInTime.ToString(@"hh\:mm\:ss"));
                            attcmd1.Parameters.AddWithValue("@CHECKOUT", "00:00:00");
                            attcmd1.Parameters.AddWithValue("@PMO_REMARK", attendanceStatus);
                        }
                    }
                    else
                    {
                        attcmd1.Parameters.AddWithValue("@checkin", checkInTime.ToString(@"hh\:mm\:ss"));
                        attcmd1.Parameters.AddWithValue("@CHECKOUT", "00:00:00");
                        attcmd1.Parameters.AddWithValue("@PMO_REMARK", attendanceStatus);
                    }

                    //   attcmd1.Parameters.AddWithValue("@checkin", checkInTime.ToString(@"hh\:mm\:ss")); // Format TimeSpan as hh:mm:ss
                    //   attcmd1.Parameters.AddWithValue("@checkin", checkInTime.ToString("HH:mm:ss"));
                    //   attcmd1.Parameters.AddWithValue("@CHECKOUT", checkOutTime.ToString(@"hh\:mm\:ss"));
                    attcmd1.Parameters.AddWithValue("@NEXTDAYIN", "00:00:00");
                    attcmd1.Parameters.AddWithValue("@NEXTDAYOUT", "00:00:00");
                    attcmd1.Parameters.AddWithValue("@DELAY_DUR", delayTime.ToString(@"hh\:mm\:ss"));
                    attcmd1.Parameters.AddWithValue("@EARLY_L_DUR", "00:00:00");
                    //if (checkInTime == checkOutTime)
                    //{
                    //    attcmd1.Parameters.AddWithValue("@EARLY_L_DUR", "00:00:00");
                    //}
                    //else
                    //{
                    //    attcmd1.Parameters.AddWithValue("@EARLY_L_DUR", earlyLeaveTime.ToString(@"hh\:mm\:ss"));
                    //}
                    //    attcmd1.Parameters.AddWithValue("@EARLY_L_DUR", earlyLeaveTime.ToString(@"hh\:mm\:ss"));
                    attcmd1.Parameters.AddWithValue("@WSNAME_SAP", wsCheckIn.ToString(@"hh\:mm\:ss"));
                    attcmd1.Parameters.AddWithValue("@WSNAME_PMO", wsCheckOut.ToString(@"hh\:mm\:ss"));
                    attcmd1.Parameters.AddWithValue("@wsid", wsid);
                    attcmd1.Parameters.AddWithValue("@type", wstype);
                    //  attcmd1.Parameters.AddWithValue("@PMO_REMARK", attendanceStatus);
                    attcmd1.Parameters.AddWithValue("@PROJECTNAME", projectname);
                    attcmd1.Parameters.AddWithValue("@PROJECTLOCATION", projectlocation);
                    attcmd1.Parameters.AddWithValue("@DEVICENAME", devicename);

                    attcmd1.ExecuteNonQuery();
                }
            }
        }

        static TimeSpan DTCalculateDelayTime(TimeSpan actualCheckIn, TimeSpan scheduleCheckIn)
        {
            return actualCheckIn > scheduleCheckIn ? actualCheckIn - scheduleCheckIn : TimeSpan.Zero;
        }

        static TimeSpan DTCalculateEarlyLeaveTime(TimeSpan actualCheckOut, TimeSpan scheduleCheckOut)
        {
            return actualCheckOut < scheduleCheckOut ? scheduleCheckOut - actualCheckOut : TimeSpan.Zero;
        }

        static bool DTIsOnePunch(TimeSpan actualCheckIn, TimeSpan actualCheckOut)
        {
            return actualCheckIn != TimeSpan.Zero && actualCheckOut == TimeSpan.Zero;
        }

        static string DTCalculateAttendanceStatus(TimeSpan delay, TimeSpan earlyLeave, bool isOnePunch)
        {
            //if (delay > TimeSpan.Zero && earlyLeave > TimeSpan.Zero)
            //{
            //    return "Delay + Early Leave";
            //}
            //else if (delay > TimeSpan.Zero)
            //{
            //    return "Delay";
            //}
            //else if (earlyLeave > TimeSpan.Zero)
            //{
            //    return "Early Leave";
            //}
            //else if (isOnePunch)
            //{
            //    return "One Punch";
            //}
            //else
            //{
            //    return "Normal";
            //}

            //if (delay.TotalMinutes > 15 && earlyLeave.TotalMinutes < 180)
            //{
            //    return "Delay + Early Leave";
            //}
            if (delay.TotalMinutes > 15)
            {
                return "Delay";
            }
            //else if (earlyLeave.TotalMinutes < 180)
            //{
            //    return "Early Leave";
            //}
            //else if (isOnePunch)
            //{
            //    return "One Punch";
            //}
            else
            {
                return "Initial Record";
            }
        }

        // Method to check if the time is near wscheckin or wscheckout
        static bool DTIsNearWsCheckin(TimeSpan time, TimeSpan wsCheckin)
        {
            // Set the threshold, for example, 5 minutes
            TimeSpan threshold = TimeSpan.FromMinutes(30);
            return Math.Abs(time.TotalMinutes - wsCheckin.TotalMinutes) < threshold.TotalMinutes;
        }

        static bool DTIsNearWsCheckout(TimeSpan time, TimeSpan wsCheckout)
        {
            // Set the threshold, for example, 5 minutes
            TimeSpan threshold = TimeSpan.FromMinutes(30);
            return Math.Abs(time.TotalMinutes - wsCheckout.TotalMinutes) < threshold.TotalMinutes;
        }

        // DAY SHIFT TODAY ATTENDANCE ENDS

        // NIGHT SHIFT YESTERDAY ATTENDANCE STARTS

        static void NightAttToday()
        {
            DateTime todaydate = DateTime.Now;
            string todaydate1 = "2024-11-22";
            //  String ConnStr1 = "Data Source=172.177.184.39;Network Library=DBMSSOCN;Initial Catalog=EstimationNew;User ID=sqladmin;Password=Sqlegypt@123456;";
            string ConnStr1 = "Data Source=192.168.15.4;Network Library=DBMSSOCN;Initial Catalog=Rccattdb;User ID=sa;Password=sql@12345;";
            using (SqlConnection conn61 = new SqlConnection(ConnStr1))
            {
                //    string MAINSQL = "SELECT A9.empid, A9.badgenumber, A9.CheckDate, A9.wsid, Ews.empname, Ews.weekday, Ews.wstype, MAX(CASE WHEN IsFirstEntry = 1 THEN Ews.checkin END) AS wsCheckIn, MAX(CASE WHEN IsLastEntry = 1 THEN Ews.checkout END) AS wsCheckOut, MAX(CASE WHEN IsFirstEntry = 1 THEN A9.CheckTime END) AS CheckInTime, MAX(CASE WHEN IsLastEntry = 1 THEN A9.CheckTime END) AS CheckOutTime FROM Attendance9 A9 INNER JOIN (SELECT empid, CheckDate, MIN(CheckTime) AS MinCheckTime, MAX(CheckTime) AS MaxCheckTime FROM Attendance9 WHERE empid = 1136 GROUP BY empid, CheckDate) AS Subquery ON A9.empid = Subquery.empid AND A9.CheckDate = Subquery.CheckDate INNER JOIN Empws Ews ON A9.empid = Ews.empid AND DATENAME(WEEKDAY, A9.CheckDate) = Ews.weekday OUTER APPLY(SELECT CASE WHEN A9.CheckTime = Subquery.MinCheckTime THEN 1 ELSE 0 END AS IsFirstEntry, CASE WHEN A9.CheckTime = Subquery.MaxCheckTime THEN 1 ELSE 0 END AS IsLastEntry) AS EntryInfo WHERE A9.empid = 1136 GROUP BY A9.empid, A9.badgenumber, A9.CheckDate, A9.wsid, Ews.empname, Ews.weekday, Ews.wstype; ";
                //    string MAINSQL = "WITH NightShiftEntries AS (SELECT A9.empid, A9.badgenumber, A9.CheckDate, A9.CheckTime AS CheckInTime, Ews.checkin AS wsCheckIn, Ews.checkout AS wsCheckOut, Ews.weekday, Ews.wstype, Ews.wsid, Ews.empname, ROW_NUMBER() OVER(PARTITION BY A9.empid, A9.badgenumber, A9.CheckDate ORDER BY A9.CheckTime) AS RowNum FROM Attendance9 A9 INNER JOIN Empws Ews ON A9.wsid = Ews.wsid AND A9.empid = Ews.empid AND DATENAME(WEEKDAY, A9.CheckDate) = Ews.weekday    WHERE Ews.wstype = 'N' AND CONVERT(time, A9.CheckTime) >= Ews.checkin) SELECT empid, badgenumber, empname, CheckDate, CASE WHEN DATEDIFF(MINUTE, wsCheckIn, CheckInTime) <= 180 THEN CheckInTime ELSE '00:00:00' END AS CheckInTime, COALESCE((SELECT MIN(CheckTime) FROM Attendance9 A2 WHERE A2.empid = A1.empid AND A2.CheckDate = DATEADD(DAY, 1, A1.CheckDate)), '00:00:00') AS CheckOutTime, weekday, wsCheckIn, wsCheckOut, wstype, wsid FROM NightShiftEntries A1 WHERE RowNum = 1; ";
                string MAINSQL = "WITH NightShiftEntries AS (SELECT A9.empid, A9.badgenumber, A9.CheckDate, A9.CheckTime AS CheckInTime, Ews.checkin AS wsCheckIn, Ews.checkout AS wsCheckOut, Ews.weekday, Ews.wstype, Ews.wsid, Ews.empname, Ews.projectname, Ews.projectlocation, Ews.devicename, ROW_NUMBER() OVER(PARTITION BY A9.empid, A9.badgenumber, A9.CheckDate ORDER BY A9.CheckTime) AS RowNum FROM Attendance9 A9 INNER JOIN Empws Ews ON A9.wsid = Ews.wsid AND A9.empid = Ews.empid AND DATENAME(WEEKDAY, A9.CheckDate) = Ews.weekday WHERE Ews.wstype = 'N' and (Ews.dailydate = '" + todaydate1 + "' and A9.CheckDate = '" + todaydate1 + "') AND CONVERT(time, A9.CheckTime) >= Ews.checkin) SELECT empid, badgenumber, empname, CheckDate, CASE WHEN DATEDIFF(MINUTE, wsCheckIn, CheckInTime) <= 180 THEN CheckInTime ELSE '00:00:00' END AS CheckInTime, COALESCE((SELECT MIN(CheckTime) FROM Attendance9 A2 WHERE A2.empid = A1.empid AND A2.CheckDate = DATEADD(DAY, 1, A1.CheckDate)), '00:00:00') AS CheckOutTime, weekday, wsCheckIn, wsCheckOut, wstype, wsid, projectname, projectlocation, devicename FROM NightShiftEntries A1 WHERE RowNum = 1;";

                SqlDataAdapter TitlesAdpt11 = new SqlDataAdapter(MAINSQL, ConnStr1);
                DataSet Titles11 = new DataSet();
                // No need to open or close the connection
                //   since the SqlDataAdapter will do this automatically.
                TitlesAdpt11.Fill(Titles11);

                conn61.Open();

                //  GridView1.DataSource = Titles1.Tables[0];
                //  GridView1.DataBind();

                if (Titles11.Tables[0].Rows.Count > 0)
                {


                    foreach (DataRow Title12 in Titles11.Tables[0].Rows)
                    {

                        //   int delaydays_ = Convert.ToInt32(Title12[1]);
                        //   string projectid_ = Title12[0].ToString();
                        //    string projectname_ = Title12[1].ToString();
                        string wstype = Title12["wstype"].ToString();
                        //if (wstype == "D")

                        int empid = Convert.ToInt32(Title12["empid"]);
                        TimeSpan wsCheckIn = TimeSpan.Parse(Title12["wsCheckIn"].ToString());
                        TimeSpan wsCheckOut = TimeSpan.Parse(Title12["wsCheckOut"].ToString());
                        TimeSpan checkInTime = TimeSpan.Parse(Title12["CheckInTime"].ToString());
                        TimeSpan checkOutTime = TimeSpan.Parse(Title12["CheckOutTime"].ToString());
                        TimeSpan delayTime = NTCalculateDelayTime(checkInTime, wsCheckIn);
                        TimeSpan earlyLeaveTime = NTCalculateEarlyLeaveTime(checkOutTime, wsCheckOut);

                        string badgeno = Title12["badgenumber"].ToString();
                        //   string checkDate = reader.GetString(reader.GetOrdinal("CheckDate"));
                        DateTime checkDate = Convert.ToDateTime(Title12["CheckDate"]);
                        int wsid = Convert.ToInt32(Title12["wsid"]);
                        string empname = Title12["empname"].ToString();
                        string weekday = Title12["weekday"].ToString();
                        string projectname_ = Title12["projectname"].ToString();
                        string projectlocation_ = Title12["projectlocation"].ToString();
                        string devicename_ = Title12["devicename"].ToString();

                        bool isOnePunch = NTIsOnePunch(checkInTime, checkOutTime);
                        string attendanceStatus = NTCalculateAttendanceStatus(delayTime, earlyLeaveTime, isOnePunch);

                        //   InsertIntoZHR_TB_ATT_A(empid, empname, badgeno, checkDate, wsCheckIn, wsCheckOut, checkInTime, checkOutTime, delayTime, earlyLeaveTime, wsid, wstype, attendanceStatus);
                        NTInsertIntoZHR_TB_ATT_B(empid, empname, badgeno, checkDate, wsCheckIn, wsCheckOut, checkInTime, checkOutTime, delayTime, earlyLeaveTime, wsid, wstype, attendanceStatus, projectname_, projectlocation_, devicename_);


                    }
                }
            }
        }


        static TimeSpan NTGetTimeSpan(SqlDataReader reader, string columnName)
        {
            int columnIndex = reader.GetOrdinal(columnName);
            return reader.IsDBNull(columnIndex) ? TimeSpan.Zero : (reader.GetFieldType(columnIndex) == typeof(TimeSpan) ? reader.GetTimeSpan(columnIndex) : TimeSpan.Zero);
        }


        static void NTInsertIntoZHR_TB_ATT_B(int empid, string empname, string badgeno, DateTime checkDate, TimeSpan wsCheckIn, TimeSpan wsCheckOut, TimeSpan checkInTime, TimeSpan checkOutTime, TimeSpan delayTime, TimeSpan earlyLeaveTime, int wsid, string wstype, string attendanceStatus, string projectname, string projectlocation, string devicename)
        {
            string ConnStr = "Data Source=192.168.15.4;Network Library=DBMSSOCN;Initial Catalog=Rccattdb;User ID=sa;Password=sql@12345;";

            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();

                //    string attcmd = "INSERT into ZHR_TB_ATT_A (MANDT, tdate, pernr, record, ename, BADGE_NO, checkin, CHECKOUT, NEXTDAYIN, NEXTDAYOUT, DELAY_DUR, EARLY_L_DUR, WSNAME_SAP, WSNAME_PMO, wsid, type, PMO_REMARK) Values (@MANDT, @tdate, @pernr, @record, @ename, @BADGE_NO, @checkin, @CHECKOUT, @NEXTDAYIN, @NEXTDAYOUT, @DELAY_DUR, @EARLY_L_DUR, @WSNAME_SAP, @WSNAME_PMO, @wsid, @type, @PMO_REMARK)";
                string attcmd = "INSERT into ZHR_TB_ATT_B (MANDT, tdate, pernr, record, ename, BADGE_NO, checkin, CHECKOUT, NEXTDAYIN, NEXTDAYOUT, DELAY_DUR, EARLY_L_DUR, WSNAME_SAP, WSNAME_PMO, wsid, type, PMO_REMARK, PROJECTNAME, DEVICENAME, PROJECTLOCATION) Values (@MANDT, @tdate, @pernr, @record, @ename, @BADGE_NO, @checkin, @CHECKOUT, @NEXTDAYIN, @NEXTDAYOUT, @DELAY_DUR, @EARLY_L_DUR, @WSNAME_SAP, @WSNAME_PMO, @wsid, @type, @PMO_REMARK, @PROJECTNAME, @DEVICENAME, @PROJECTLOCATION)";

                using (SqlCommand attcmd1 = new SqlCommand(attcmd, conn))
                {
                    attcmd1.Parameters.AddWithValue("@MANDT", "300");
                    attcmd1.Parameters.AddWithValue("@tdate", checkDate.ToString("yyyy-MM-dd"));
                    attcmd1.Parameters.AddWithValue("@pernr", empid);
                    attcmd1.Parameters.AddWithValue("@record", "D");
                    attcmd1.Parameters.AddWithValue("@ename", empname);
                    attcmd1.Parameters.AddWithValue("@BADGE_NO", badgeno);

                    delayTime = NTCalculateDelayTime(checkInTime, wsCheckIn);
                    earlyLeaveTime = NTCalculateEarlyLeaveTime(checkOutTime, wsCheckOut);

                    if ((checkInTime - checkOutTime).TotalMinutes < 30)
                    {
                        // Check if it is near wscheckin or wscheckout
                        if (NTIsNearWsCheckin(checkInTime, wsCheckIn))
                        {
                            attcmd1.Parameters.AddWithValue("@checkin", checkInTime.ToString(@"hh\:mm\:ss"));
                            attcmd1.Parameters.AddWithValue("@NEXTDAYOUT", "00:00:00");

                            // if (delayTime.TotalMinutes > 15 && (checkInTime.ToString(@"hh\:mm\:ss") == "00:00:00" || checkOutTime.ToString(@"hh\:mm\:ss") == "00:00:00"))
                            if (delayTime.TotalMinutes > 15)
                            {
                                attcmd1.Parameters.AddWithValue("@PMO_REMARK", "Delay + One Punch");
                            }
                            //else if (checkInTime.ToString(@"hh\:mm\:ss") == "00:00:00" || checkOutTime.ToString(@"hh\:mm\:ss") == "00:00:00")
                            //{
                            //    attcmd1.Parameters.AddWithValue("@PMO_REMARK", "One Punch");
                            //}
                        }
                        //else if (NIsNearWsCheckout(checkOutTime, wsCheckOut))
                        //{
                        //    attcmd1.Parameters.AddWithValue("@checkin", "00:00:00");
                        //    attcmd1.Parameters.AddWithValue("@NEXTDAYOUT", "00:00:00");
                        //  //  attcmd1.Parameters.AddWithValue("@NEXTDAYOUT", checkOutTime.ToString(@"hh\:mm\:ss"));

                        //    if (earlyLeaveTime.TotalMinutes < 180)
                        //    {
                        //        attcmd1.Parameters.AddWithValue("@PMO_REMARK", "Early Leave + One Punch");
                        //    }
                        //    else if (earlyLeaveTime.TotalMinutes > 180)
                        //    {
                        //        attcmd1.Parameters.AddWithValue("@PMO_REMARK", "One Punch");
                        //    }
                        //}
                        else
                        {
                            attcmd1.Parameters.AddWithValue("@checkin", checkInTime.ToString(@"hh\:mm\:ss"));
                            attcmd1.Parameters.AddWithValue("@NEXTDAYOUT", "00:00:00");
                            //   attcmd1.Parameters.AddWithValue("@NEXTDAYOUT", checkOutTime.ToString(@"hh\:mm\:ss"));
                            attcmd1.Parameters.AddWithValue("@PMO_REMARK", attendanceStatus);
                        }
                    }
                    else
                    {
                        attcmd1.Parameters.AddWithValue("@checkin", checkInTime.ToString(@"hh\:mm\:ss"));
                        attcmd1.Parameters.AddWithValue("@NEXTDAYOUT", "00:00:00");
                        //   attcmd1.Parameters.AddWithValue("@NEXTDAYOUT", checkOutTime.ToString(@"hh\:mm\:ss"));
                        attcmd1.Parameters.AddWithValue("@PMO_REMARK", attendanceStatus);
                    }

                    //   attcmd1.Parameters.AddWithValue("@checkin", checkInTime.ToString(@"hh\:mm\:ss")); // Format TimeSpan as hh:mm:ss
                    //   attcmd1.Parameters.AddWithValue("@checkin", checkInTime.ToString("HH:mm:ss"));
                    //   attcmd1.Parameters.AddWithValue("@CHECKOUT", checkOutTime.ToString(@"hh\:mm\:ss"));
                    attcmd1.Parameters.AddWithValue("@NEXTDAYIN", "00:00:00");
                    attcmd1.Parameters.AddWithValue("@CHECKOUT", "00:00:00");
                    attcmd1.Parameters.AddWithValue("@DELAY_DUR", delayTime.ToString(@"hh\:mm\:ss"));
                    attcmd1.Parameters.AddWithValue("@EARLY_L_DUR", earlyLeaveTime.ToString(@"hh\:mm\:ss"));
                    attcmd1.Parameters.AddWithValue("@WSNAME_SAP", wsCheckIn.ToString(@"hh\:mm\:ss"));
                    attcmd1.Parameters.AddWithValue("@WSNAME_PMO", wsCheckOut.ToString(@"hh\:mm\:ss"));
                    attcmd1.Parameters.AddWithValue("@wsid", wsid);
                    attcmd1.Parameters.AddWithValue("@type", wstype);
                    //   attcmd1.Parameters.AddWithValue("@PMO_REMARK", attendanceStatus);
                    attcmd1.Parameters.AddWithValue("@PROJECTNAME", projectname);
                    attcmd1.Parameters.AddWithValue("@PROJECTLOCATION", projectlocation);
                    attcmd1.Parameters.AddWithValue("@DEVICENAME", devicename);

                    attcmd1.ExecuteNonQuery();
                }
            }
        }
        static TimeSpan NTCalculateDelayTime(TimeSpan actualCheckIn, TimeSpan scheduleCheckIn)
        {
            return actualCheckIn > scheduleCheckIn ? actualCheckIn - scheduleCheckIn : TimeSpan.Zero;
        }

        static TimeSpan NTCalculateEarlyLeaveTime(TimeSpan actualCheckOut, TimeSpan scheduleCheckOut)
        {
            return actualCheckOut < scheduleCheckOut ? scheduleCheckOut - actualCheckOut : TimeSpan.Zero;
            //  return actualCheckOut < scheduleCheckOut ? actualCheckOut - scheduleCheckOut : TimeSpan.Zero;
        }

        static bool NTIsOnePunch(TimeSpan actualCheckIn, TimeSpan actualCheckOut)
        {
            return actualCheckIn != TimeSpan.Zero && actualCheckOut == TimeSpan.Zero;
        }

        static string NTCalculateAttendanceStatus(TimeSpan delay, TimeSpan earlyLeave, bool isOnePunch)
        {
            //if (delay > TimeSpan.Zero && earlyLeave > TimeSpan.Zero)
            //{
            //    return "Delay + Early Leave";
            //}
            //else if (delay > TimeSpan.Zero)
            //{
            //    return "Delay";
            //}
            //else if (earlyLeave > TimeSpan.Zero)
            //{
            //    return "Early Leave";
            //}
            //else if (isOnePunch)
            //{
            //    return "One Punch";
            //}
            //else
            //{
            //    return "Normal";
            //}

            //if (delay.TotalMinutes > 15 && earlyLeave.ToString(@"hh\:mm\:ss") != "00:00:00")
            //{
            //    return "Delay + Early Leave";
            //}
            //else if (delay.TotalMinutes > 15 && earlyLeave.TotalMinutes > 0)
            //{
            //    return "Delay";
            //}
            if (delay.TotalMinutes > 15)
            {
                return "Delay";
            }
            //else if (earlyLeave.ToString(@"hh\:mm\:ss") != "00:00:00")
            //{
            //    return "Early Leave";
            //}
            //else if (earlyLeave.TotalMinutes < 180)
            //{
            //    return "Early Leave";
            //}
            //else if (isOnePunch)
            //{
            //    return "One Punch";
            //}
            //else if (delay.TotalMinutes > 15 && isOnePunch)
            //{
            //    return "Delay + One Punch";
            //}
            else
            {
                return "Initial Record";
            }
        }

        // Method to check if the time is near wscheckin or wscheckout
        static bool NTIsNearWsCheckin(TimeSpan time, TimeSpan wsCheckin)
        {
            // Set the threshold, for example, 5 minutes
            TimeSpan threshold = TimeSpan.FromMinutes(60);
            return Math.Abs(time.TotalMinutes - wsCheckin.TotalMinutes) < threshold.TotalMinutes;
        }

        static bool NTIsNearWsCheckout(TimeSpan time, TimeSpan wsCheckout)
        {
            // Set the threshold, for example, 5 minutes
            TimeSpan threshold = TimeSpan.FromMinutes(60);
            return Math.Abs(time.TotalMinutes - wsCheckout.TotalMinutes) < threshold.TotalMinutes;
        }

        // NIGHT SHIFT YESTERDAY ATTENDANCE ENDS

        // ABSENT ATTENDANCE YESTERDAY STARTS

        static void AbsentAttToday()
        {
            DateTime todaydate = DateTime.Now;
            string todaydate1 = "2024-11-22";
            //  String ConnStr1 = "Data Source=172.177.184.39;Network Library=DBMSSOCN;Initial Catalog=EstimationNew;User ID=sqladmin;Password=Sqlegypt@123456;";
            string ConnStr1 = "Data Source=192.168.15.4;Network Library=DBMSSOCN;Initial Catalog=Rccattdb;User ID=sa;Password=sql@12345;";
            using (SqlConnection conn61 = new SqlConnection(ConnStr1))
            {
                //    string MAINSQL = "SELECT A9.empid, A9.badgenumber, A9.CheckDate, A9.wsid, Ews.empname, Ews.weekday, Ews.wstype, MAX(CASE WHEN IsFirstEntry = 1 THEN Ews.checkin END) AS wsCheckIn, MAX(CASE WHEN IsLastEntry = 1 THEN Ews.checkout END) AS wsCheckOut, MAX(CASE WHEN IsFirstEntry = 1 THEN A9.CheckTime END) AS CheckInTime, MAX(CASE WHEN IsLastEntry = 1 THEN A9.CheckTime END) AS CheckOutTime FROM Attendance9 A9 INNER JOIN (SELECT empid, CheckDate, MIN(CheckTime) AS MinCheckTime, MAX(CheckTime) AS MaxCheckTime FROM Attendance9 WHERE empid = 1136 GROUP BY empid, CheckDate) AS Subquery ON A9.empid = Subquery.empid AND A9.CheckDate = Subquery.CheckDate INNER JOIN Empws Ews ON A9.empid = Ews.empid AND DATENAME(WEEKDAY, A9.CheckDate) = Ews.weekday OUTER APPLY(SELECT CASE WHEN A9.CheckTime = Subquery.MinCheckTime THEN 1 ELSE 0 END AS IsFirstEntry, CASE WHEN A9.CheckTime = Subquery.MaxCheckTime THEN 1 ELSE 0 END AS IsLastEntry) AS EntryInfo WHERE A9.empid = 1136 GROUP BY A9.empid, A9.badgenumber, A9.CheckDate, A9.wsid, Ews.empname, Ews.weekday, Ews.wstype; ";
                //    string MAINSQL = "WITH NightShiftEntries AS (SELECT A9.empid, A9.badgenumber, A9.CheckDate, A9.CheckTime AS CheckInTime, Ews.checkin AS wsCheckIn, Ews.checkout AS wsCheckOut, Ews.weekday, Ews.wstype, Ews.wsid, Ews.empname, ROW_NUMBER() OVER(PARTITION BY A9.empid, A9.badgenumber, A9.CheckDate ORDER BY A9.CheckTime) AS RowNum FROM Attendance9 A9 INNER JOIN Empws Ews ON A9.wsid = Ews.wsid AND A9.empid = Ews.empid AND DATENAME(WEEKDAY, A9.CheckDate) = Ews.weekday    WHERE Ews.wstype = 'N' AND CONVERT(time, A9.CheckTime) >= Ews.checkin) SELECT empid, badgenumber, empname, CheckDate, CASE WHEN DATEDIFF(MINUTE, wsCheckIn, CheckInTime) <= 180 THEN CheckInTime ELSE '00:00:00' END AS CheckInTime, COALESCE((SELECT MIN(CheckTime) FROM Attendance9 A2 WHERE A2.empid = A1.empid AND A2.CheckDate = DATEADD(DAY, 1, A1.CheckDate)), '00:00:00') AS CheckOutTime, weekday, wsCheckIn, wsCheckOut, wstype, wsid FROM NightShiftEntries A1 WHERE RowNum = 1; ";
                string MAINSQL = "select weekday, checkin as wscheckin, checkout as wscheckout, wstype, projectname, empid, RIGHT('000000000'+CONVERT(VARCHAR,empid),9) as badgenumber,  empname, projectlocation, devicename, wsid, workschedule, dailydate from Empws where DATENAME(WEEKDAY, dailydate) = weekday and empid not in (select distinct empid from Attendance9 where CheckDate = '" + todaydate1 + "')";

                SqlDataAdapter TitlesAdpt11 = new SqlDataAdapter(MAINSQL, ConnStr1);
                DataSet Titles11 = new DataSet();
                // No need to open or close the connection
                //   since the SqlDataAdapter will do this automatically.
                TitlesAdpt11.Fill(Titles11);

                conn61.Open();

                //  GridView1.DataSource = Titles1.Tables[0];
                //  GridView1.DataBind();

                if (Titles11.Tables[0].Rows.Count > 0)
                {


                    foreach (DataRow Title12 in Titles11.Tables[0].Rows)
                    {

                        //   int delaydays_ = Convert.ToInt32(Title12[1]);
                        //   string projectid_ = Title12[0].ToString();
                        //    string projectname_ = Title12[1].ToString();
                        string wstype = Title12["wstype"].ToString();
                        //if (wstype == "D")

                        int empid = Convert.ToInt32(Title12["empid"]);
                        TimeSpan wsCheckIn = TimeSpan.Parse(Title12["wsCheckIn"].ToString());
                        TimeSpan wsCheckOut = TimeSpan.Parse(Title12["wsCheckOut"].ToString());
                        TimeSpan checkInTime = TimeSpan.Parse("00:00:00");
                        TimeSpan checkOutTime = TimeSpan.Parse("00:00:00");
                        TimeSpan delayTime = TimeSpan.Parse("00:00:00");
                        TimeSpan earlyLeaveTime = TimeSpan.Parse("00:00:00");

                        string badgeno = Title12["badgenumber"].ToString();
                        //   string checkDate = reader.GetString(reader.GetOrdinal("CheckDate"));
                        DateTime checkDate = Convert.ToDateTime(Title12["dailydate"]);
                        int wsid = Convert.ToInt32(Title12["wsid"]);
                        string empname = Title12["empname"].ToString();
                        string weekday = Title12["weekday"].ToString();
                        string projectname_ = Title12["projectname"].ToString();
                        string projectlocation_ = Title12["projectlocation"].ToString();
                        string devicename_ = Title12["devicename"].ToString();

                        //    bool isOnePunch = IsOnePunch(checkInTime, checkOutTime);
                        string attendanceStatus = "Absent";

                        //   InsertIntoZHR_TB_ATT_A(empid, empname, badgeno, checkDate, wsCheckIn, wsCheckOut, checkInTime, checkOutTime, delayTime, earlyLeaveTime, wsid, wstype, attendanceStatus);
                        ATInsertIntoZHR_TB_ATT_B(empid, empname, badgeno, checkDate, wsCheckIn, wsCheckOut, checkInTime, checkOutTime, delayTime, earlyLeaveTime, wsid, wstype, attendanceStatus, projectname_, projectlocation_, devicename_);


                    }
                }
            }
        }


        static TimeSpan ATGetTimeSpan(SqlDataReader reader, string columnName)
        {
            int columnIndex = reader.GetOrdinal(columnName);
            return reader.IsDBNull(columnIndex) ? TimeSpan.Zero : (reader.GetFieldType(columnIndex) == typeof(TimeSpan) ? reader.GetTimeSpan(columnIndex) : TimeSpan.Zero);
        }


        static void ATInsertIntoZHR_TB_ATT_B(int empid, string empname, string badgeno, DateTime checkDate, TimeSpan wsCheckIn, TimeSpan wsCheckOut, TimeSpan checkInTime, TimeSpan checkOutTime, TimeSpan delayTime, TimeSpan earlyLeaveTime, int wsid, string wstype, string attendanceStatus, string projectname, string projectlocation, string devicename)
        {
            string ConnStr = "Data Source=192.168.15.4;Network Library=DBMSSOCN;Initial Catalog=Rccattdb;User ID=sa;Password=sql@12345;";

            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();

                //    string attcmd = "INSERT into ZHR_TB_ATT_A (MANDT, tdate, pernr, record, ename, BADGE_NO, checkin, CHECKOUT, NEXTDAYIN, NEXTDAYOUT, DELAY_DUR, EARLY_L_DUR, WSNAME_SAP, WSNAME_PMO, wsid, type, PMO_REMARK) Values (@MANDT, @tdate, @pernr, @record, @ename, @BADGE_NO, @checkin, @CHECKOUT, @NEXTDAYIN, @NEXTDAYOUT, @DELAY_DUR, @EARLY_L_DUR, @WSNAME_SAP, @WSNAME_PMO, @wsid, @type, @PMO_REMARK)";
                string attcmd = "INSERT into ZHR_TB_ATT_B (MANDT, tdate, pernr, record, ename, BADGE_NO, checkin, CHECKOUT, NEXTDAYIN, NEXTDAYOUT, DELAY_DUR, EARLY_L_DUR, WSNAME_SAP, WSNAME_PMO, wsid, type, PMO_REMARK, PROJECTNAME, DEVICENAME, PROJECTLOCATION) Values (@MANDT, @tdate, @pernr, @record, @ename, @BADGE_NO, @checkin, @CHECKOUT, @NEXTDAYIN, @NEXTDAYOUT, @DELAY_DUR, @EARLY_L_DUR, @WSNAME_SAP, @WSNAME_PMO, @wsid, @type, @PMO_REMARK, @PROJECTNAME, @DEVICENAME, @PROJECTLOCATION)";

                using (SqlCommand attcmd1 = new SqlCommand(attcmd, conn))
                {
                    attcmd1.Parameters.AddWithValue("@MANDT", "300");
                    attcmd1.Parameters.AddWithValue("@tdate", checkDate.ToString("yyyy-MM-dd"));
                    attcmd1.Parameters.AddWithValue("@pernr", empid);
                    attcmd1.Parameters.AddWithValue("@record", "D");
                    attcmd1.Parameters.AddWithValue("@ename", empname);
                    attcmd1.Parameters.AddWithValue("@BADGE_NO", badgeno);

                    //delayTime = CalculateDelayTime(checkInTime, wsCheckIn);
                    //earlyLeaveTime = CalculateEarlyLeaveTime(checkOutTime, wsCheckOut);

                    //if ((checkInTime - checkOutTime).TotalMinutes < 30)
                    //{
                    //    // Check if it is near wscheckin or wscheckout
                    //    if (IsNearWsCheckin(checkInTime, wsCheckIn))
                    //    {
                    //        attcmd1.Parameters.AddWithValue("@checkin", checkInTime.ToString(@"hh\:mm\:ss"));
                    //        attcmd1.Parameters.AddWithValue("@NEXTDAYOUT", "00:00:00");

                    //       // if (delayTime.TotalMinutes > 15 && (checkInTime.ToString(@"hh\:mm\:ss") == "00:00:00" || checkOutTime.ToString(@"hh\:mm\:ss") == "00:00:00"))
                    //       if(delayTime.TotalMinutes > 15)
                    //        {
                    //            attcmd1.Parameters.AddWithValue("@PMO_REMARK", "Delay + One Punch");
                    //        }
                    //        //else if (checkInTime.ToString(@"hh\:mm\:ss") == "00:00:00" || checkOutTime.ToString(@"hh\:mm\:ss") == "00:00:00")
                    //        //{
                    //        //    attcmd1.Parameters.AddWithValue("@PMO_REMARK", "One Punch");
                    //        //}
                    //    }
                    //    else if (IsNearWsCheckout(checkOutTime, wsCheckOut))
                    //    {
                    //        attcmd1.Parameters.AddWithValue("@checkin", "00:00:00");
                    //        attcmd1.Parameters.AddWithValue("@NEXTDAYOUT", checkOutTime.ToString(@"hh\:mm\:ss"));

                    //        if (earlyLeaveTime.TotalMinutes < 180)
                    //        {
                    //            attcmd1.Parameters.AddWithValue("@PMO_REMARK", "Early Leave + One Punch");
                    //        }
                    //        else if (earlyLeaveTime.TotalMinutes > 180)
                    //        {
                    //            attcmd1.Parameters.AddWithValue("@PMO_REMARK", "One Punch");
                    //        }
                    //    }
                    //    else
                    //    {
                    //        attcmd1.Parameters.AddWithValue("@checkin", checkInTime.ToString(@"hh\:mm\:ss"));
                    //        attcmd1.Parameters.AddWithValue("@NEXTDAYOUT", checkOutTime.ToString(@"hh\:mm\:ss"));
                    //        attcmd1.Parameters.AddWithValue("@PMO_REMARK", attendanceStatus);
                    //    }
                    //}
                    //else
                    //{
                    //    attcmd1.Parameters.AddWithValue("@checkin", checkInTime.ToString(@"hh\:mm\:ss"));
                    //    attcmd1.Parameters.AddWithValue("@NEXTDAYOUT", checkOutTime.ToString(@"hh\:mm\:ss"));
                    //    attcmd1.Parameters.AddWithValue("@PMO_REMARK", attendanceStatus);
                    //}

                    //   attcmd1.Parameters.AddWithValue("@checkin", checkInTime.ToString(@"hh\:mm\:ss")); // Format TimeSpan as hh:mm:ss
                    //   attcmd1.Parameters.AddWithValue("@checkin", checkInTime.ToString("HH:mm:ss"));
                    //   attcmd1.Parameters.AddWithValue("@CHECKOUT", checkOutTime.ToString(@"hh\:mm\:ss"));

                    attcmd1.Parameters.AddWithValue("@CHECKIN", "00:00:00");
                    attcmd1.Parameters.AddWithValue("@CHECKOUT", "00:00:00");
                    attcmd1.Parameters.AddWithValue("@NEXTDAYIN", "00:00:00");
                    attcmd1.Parameters.AddWithValue("@NEXTDAYOUT", "00:00:00");
                    attcmd1.Parameters.AddWithValue("@DELAY_DUR", "00:00:00");
                    attcmd1.Parameters.AddWithValue("@EARLY_L_DUR", "00:00:00");
                    attcmd1.Parameters.AddWithValue("@WSNAME_SAP", wsCheckIn.ToString(@"hh\:mm\:ss"));
                    attcmd1.Parameters.AddWithValue("@WSNAME_PMO", wsCheckOut.ToString(@"hh\:mm\:ss"));
                    attcmd1.Parameters.AddWithValue("@wsid", wsid);
                    attcmd1.Parameters.AddWithValue("@type", wstype);
                    attcmd1.Parameters.AddWithValue("@PMO_REMARK", attendanceStatus);
                    attcmd1.Parameters.AddWithValue("@PROJECTNAME", projectname);
                    attcmd1.Parameters.AddWithValue("@PROJECTLOCATION", projectlocation);
                    attcmd1.Parameters.AddWithValue("@DEVICENAME", devicename);
                    attcmd1.ExecuteNonQuery();

                }
            }
        }

        // ABSENT ATTENDANCE YESTERDAY ENDS

        // DAYOFF ATTENDANCE YESTERDAY STARTS
        static void AttTodayUpdate_Dayoff()
        {
            string ConnStr = "Data Source=192.168.15.4;Network Library=DBMSSOCN;Initial Catalog=Rccattdb;User ID=sa;Password=sql@12345;";

            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                DateTime todaydate = DateTime.Now;
                string todaydate1 = "2024-11-22";

                string stringattcmdD = "UPDATE ZHR_TB_ATT_A SET PMO_REMARK = 'Day Off' where RECORD = 'T' AND (WSNAME_PMO = '00:00:00' and WSNAME_SAP = '00:00:00') and TDATE = '" + todaydate1 + "'";
                SqlCommand attcmdD = new SqlCommand(stringattcmdD, conn);
                attcmdD.ExecuteNonQuery();

            }
        }
        // DAYOFF ATTENDANCE YESTERDAY ENDS

        // UPDATE TODAY ATT DATA OF RECORD C STARTS
        static void AttTodayUpdate_C()
        {
            string ConnStr = "Data Source=192.168.15.4;Network Library=DBMSSOCN;Initial Catalog=Rccattdb;User ID=sa;Password=sql@12345;";

            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                DateTime todaydate = DateTime.Now;
                string todaydate1 = "2024-11-22";

                string stringattcmdA = "UPDATE ZHR_TB_ATT_A SET ZHR_TB_ATT_A.RECORD = 'T',	ZHR_TB_ATT_A.CHECKIN = ZHR_TB_ATT_B.CHECKIN, ZHR_TB_ATT_A.CHECKOUT = ZHR_TB_ATT_B.CHECKOUT, ZHR_TB_ATT_A.NEXTDAYIN = ZHR_TB_ATT_B.NEXTDAYIN, ZHR_TB_ATT_A.NEXTDAYOUT = ZHR_TB_ATT_B.NEXTDAYOUT,	ZHR_TB_ATT_A.DELAY_DUR = ZHR_TB_ATT_B.DELAY_DUR,	ZHR_TB_ATT_A.EARLY_L_DUR = ZHR_TB_ATT_B.EARLY_L_DUR,    ZHR_TB_ATT_A.WSNAME_SAP = ZHR_TB_ATT_B.WSNAME_SAP,	ZHR_TB_ATT_A.WSNAME_PMO = ZHR_TB_ATT_B.WSNAME_PMO,	ZHR_TB_ATT_A.WSID = ZHR_TB_ATT_B.WSID,	ZHR_TB_ATT_A.TYPE = ZHR_TB_ATT_B.TYPE,	ZHR_TB_ATT_A.PROJECTNAME = ZHR_TB_ATT_B.PROJECTNAME,	ZHR_TB_ATT_A.DEVICENAME = ZHR_TB_ATT_B.DEVICENAME,	ZHR_TB_ATT_A.PROJECTLOCATION = ZHR_TB_ATT_B.PROJECTLOCATION,	ZHR_TB_ATT_A.PMO_REMARK = ZHR_TB_ATT_B.PMO_REMARK FROM ZHR_TB_ATT_A INNER JOIN ZHR_TB_ATT_B ON ZHR_TB_ATT_A.PERNR = ZHR_TB_ATT_B.PERNR and ZHR_TB_ATT_A.TDATE = ZHR_TB_ATT_B.TDATE and ZHR_TB_ATT_B.RECORD = 'D' and ZHR_TB_ATT_A.RECORD = 'T' and ZHR_TB_ATT_B.TDATE = '" + todaydate1 + "'";
                SqlCommand attcmdA = new SqlCommand(stringattcmdA, conn);
                attcmdA.ExecuteNonQuery();

            }
        }

        // UPDATE TODAY ATT DATA OF RECORD C ENDS

        // Att logs start
        static void AttlogToday()
        {
            DateTime todaydate = DateTime.Now;
            string todaydate1 = "2024-11-22";
            //  String ConnStr1 = "Data Source=172.177.184.39;Network Library=DBMSSOCN;Initial Catalog=EstimationNew;User ID=sqladmin;Password=Sqlegypt@123456;";
            string ConnStr12 = "Data Source=192.168.15.4;Network Library=DBMSSOCN;Initial Catalog=Rccattdb;User ID=sa;Password=sql@12345;";
            using (SqlConnection conn6112 = new SqlConnection(ConnStr12))
            {
                conn6112.Open();
                string reportstr152 = "INSERT into tblnotify (empid, action, category,username,reportdate,entrydate) values (@empid, @action, @category,@username,@reportdate,@entrydate)";
                SqlCommand reportstrcmd152 = new SqlCommand(reportstr152, conn6112);
                reportstrcmd152.Parameters.AddWithValue("@empid", "");
                reportstrcmd152.Parameters.AddWithValue("@action", "Attendance Today Data Second Updated Successfully dated " + todaydate1 + " ");
                reportstrcmd152.Parameters.AddWithValue("@category", "Att Today Data");
                reportstrcmd152.Parameters.AddWithValue("@username", "SYSTEM");
                reportstrcmd152.Parameters.AddWithValue("@reportdate", todaydate1);
                reportstrcmd152.Parameters.AddWithValue("@entrydate", DateTime.Now);
                reportstrcmd152.ExecuteNonQuery();

            }
        }
        // Att logs ends

    }
}