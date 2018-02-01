using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;


namespace 读取Txt文本到数据库
{
    public class SqlServerHelp
    {
         string constr = "Server=.;DataBase=zpt; Integrated Security=True"; //设置连接字符串
         string constr2 = "server=47.90.48.6,1433;Initial Catalog=EJE;Persist Security Info=True;User ID=rcominfo;Password=rcominfo"; //设置连接字符串
         string state = "正确";
        public  void EJESqlServer(Models.zptDataInfo zptDat, string sourcePaht, string aimPath)
        {
            using (SqlConnection conn = new SqlConnection(constr))
            {
                using (SqlConnection conn2 = new SqlConnection(constr2))
                {
                    conn.Open();
                    conn2.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        using (SqlCommand cmd2 = conn2.CreateCommand())
                        {
                            cmd2.CommandText = "SELECT freightPayable FROM dbo.CFHMPring WHERE CForHM_number='" + zptDat.HouseNo + "001' ";                           //执行SQL命令
                            SqlDataAdapter myDA = new SqlDataAdapter();       //实例化数据适配器
                            myDA.SelectCommand = cmd2;                       //让适配器执行SELECT命令
                            DataSet myDS = new DataSet();
                            myDA.Fill(myDS, "CFHMPring");
                            if (myDS != null && myDS.Tables.Count > 0 && myDS.Tables[0].Rows.Count > 0)
                            {

                                decimal tabInt = (decimal)myDS.Tables[0].Rows[0]["freightPayable"];
                                int objInt = zptDat.CollectionMoney.ObjToInt();
                                if (Convert.ToInt32(tabInt) != objInt)
                                {

                                    state = "错误";
                                    sourcePaht.FileMove(aimPath + "\\"+state);
                                }
                                else
                                {
                                    state = "正确";
                                    sourcePaht.FileMove(aimPath + "\\" + state);
                                }
                            }
                            else
                            {
                                state = "没找到";
                                sourcePaht.FileMove(aimPath + "\\" + state);
                            }

                            cmd.CommandText = "INSERT INTO dbo.zptTable(SerialNo, DateProduction, HouseNo, PiecesNo, Specification, CustomerCode, CustomerNo, SenderName, SenderPhone, SenderPostalCode, SenderAddress, RecipientName, RecipientPhone, RecipientPostalCode, ReceiverAddress, SpecifyDate, SpecifyTime, No1, CollectionMoney, Commodities, DeliveryRoute, notWork, Remark, CollectionNo,state)VALUES(@SerialNo, @DateProduction, @HouseNo, @PiecesNo, @Specification, @CustomerCode, @CustomerNo, @SenderName, @SenderPhone, @SenderPostalCode, @SenderAddress, @RecipientName, @RecipientPhone, @RecipientPostalCode, @ReceiverAddress, @SpecifyDate, @SpecifyTime, @No1, @CollectionMoney, @Commodities, @DeliveryRoute, @notWork, @Remark, @CollectionNo,@state)";
                            cmd.Parameters.Clear();//清除上一次的参数
                            cmd.Parameters.Add(new SqlParameter("@SerialNo", zptDat.SerialNo));
                            cmd.Parameters.Add(new SqlParameter("@DateProduction", zptDat.DateProduction));
                            cmd.Parameters.Add(new SqlParameter("@HouseNo", zptDat.HouseNo));
                            cmd.Parameters.Add(new SqlParameter("@PiecesNo", zptDat.PiecesNo));
                            cmd.Parameters.Add(new SqlParameter("@Specification", zptDat.Specification));
                            cmd.Parameters.Add(new SqlParameter("@CustomerCode", zptDat.CustomerCode));
                            cmd.Parameters.Add(new SqlParameter("@CustomerNo", zptDat.CustomerNo));
                            cmd.Parameters.Add(new SqlParameter("@SenderName", zptDat.SenderName));
                            cmd.Parameters.Add(new SqlParameter("@SenderPhone", zptDat.SenderPhone));
                            cmd.Parameters.Add(new SqlParameter("@SenderPostalCode", zptDat.SenderPostalCode));
                            cmd.Parameters.Add(new SqlParameter("@SenderAddress", zptDat.SenderAddress));
                            cmd.Parameters.Add(new SqlParameter("@RecipientName", zptDat.RecipientName));
                            cmd.Parameters.Add(new SqlParameter("@RecipientPhone", zptDat.RecipientPhone));
                            cmd.Parameters.Add(new SqlParameter("@RecipientPostalCode", zptDat.RecipientPostalCode));
                            cmd.Parameters.Add(new SqlParameter("@ReceiverAddress", zptDat.ReceiverAddress));
                            cmd.Parameters.Add(new SqlParameter("@SpecifyDate", zptDat.SpecifyDate));
                            cmd.Parameters.Add(new SqlParameter("@SpecifyTime", zptDat.SpecifyTime));
                            cmd.Parameters.Add(new SqlParameter("@No1", zptDat.No1));
                            cmd.Parameters.Add(new SqlParameter("@CollectionMoney", zptDat.CollectionMoney));
                            cmd.Parameters.Add(new SqlParameter("@Commodities", zptDat.Commodities));
                            cmd.Parameters.Add(new SqlParameter("@DeliveryRoute", zptDat.DeliveryRoute));
                            cmd.Parameters.Add(new SqlParameter("@notWork", zptDat.notWork));
                            cmd.Parameters.Add(new SqlParameter("@Remark", zptDat.Remark));
                            cmd.Parameters.Add(new SqlParameter("@CollectionNo", zptDat.CollectionNo));
                            cmd.Parameters.Add(new SqlParameter("@state", state));
                            cmd.ExecuteNonQuery();

                        }

                    }
                }
            }
        }


        public int SqlserverConn(string houseNo)
        {
            int monery = 0;
         
            using (SqlConnection conn = new SqlConnection(constr2))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT freightPayable FROM dbo.CFHMPring WHERE CForHM_number='" + houseNo + "001' ";                           //执行SQL命令
                    SqlDataAdapter myDA = new SqlDataAdapter();       //实例化数据适配器
                    myDA.SelectCommand = cmd;                       //让适配器执行SELECT命令
                    DataSet myDS = new DataSet();
                    myDA.Fill(myDS, "CFHMPring");
                    if (myDS != null && myDS.Tables.Count > 0 && myDS.Tables[0].Rows.Count > 0)
                    {

                        decimal tabInt = (decimal)myDS.Tables[0].Rows[0]["freightPayable"];
                        monery = Convert.ToInt32(tabInt);
                    }

                }
            }
            return monery;
        }
    }
}
