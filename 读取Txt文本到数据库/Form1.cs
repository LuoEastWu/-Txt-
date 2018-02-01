using Collections;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 读取Txt文本到数据库
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string title = "标题";

        private void button1_Click(object sender, EventArgs e)
        {
            title.MessagesShowOk("请选择要从那个文件夹里对比文件！");
            labSourcePath.Text = Common.SelectPath();
            DirectoryInfo TheFolder = new DirectoryInfo(labSourcePath.Text);
            if (TheFolder.GetFiles("*.txt").Length == 0)
            {
                labSourcePath.Text = "没有要对比的文件！";
                return;
            }
            label1.Text = "总数：" + TheFolder.GetFiles("*.txt").Length;


            title.MessagesShowOk("请选择要保存的路径！！");
            labAimPath.Text = Common.SelectPath();
            System.Threading.Thread thread = new System.Threading.Thread(() =>
            {
                int[] state = { 1, 5, 13, 21, 33, 36, 38, 48, 80, 112, 144, 147, 211, 243, 275, 278, 342, 372, 382, 390, 392, 393, 401, 403, 408, 409, 410, 470, 471 };
                int[] end = { 4, 12, 20, 32, 35, 37, 47, 79, 111, 143, 146, 210, 242, 274, 277, 341, 371, 381, 389, 391, 392, 400, 402, 407, 408, 409, 469, 470, 514 };
                string[] zptTxtData = new string[29];


                Models zptModel = new Models();
                int daoruCount = 0;
                foreach (FileInfo info in TheFolder.GetFiles("*.txt"))
                {
                    using (FileStream file = new FileStream(info.FullName.ToString(), FileMode.Open))
                    {
                        int fsLen = (int)file.Length;
                        byte[] heByte = new byte[file.Length];
                        int r = file.Read(heByte, 0, heByte.Length);

                        string strZpt = Encoding.GetEncoding("big5").GetString(heByte);
                        byte[] zptByte = Encoding.GetEncoding("big5").GetBytes(strZpt);
                        if (zptByte.Length >= 512)
                        {
                            for (int i = 0; i < state.Length; i++)
                            {
                                int dd = (state[i] - 1);
                                int eddd = end[i] - dd;
                                Byte[] ThisByte = new Byte[eddd];
                                Buffer.BlockCopy(zptByte, dd, ThisByte, 0, eddd);
                                zptTxtData[i] = Encoding.GetEncoding("big5").GetString(ThisByte);
                            }
                            file.Close();
                            file.Dispose();




                            SqlServerHelp serverHelp = new SqlServerHelp();
                            serverHelp.EJESqlServer(zptModel.DataInfo(zptTxtData), info.FullName.ToString(), labAimPath.Text);

                            daoruCount++;
                            label2.Invoke(new System.Threading.ThreadStart(delegate ()
                            {
                                label2.Text = "导入数：" + daoruCount;
                            }));
                        }

                    }
                }
                label1.Invoke(new System.Threading.ThreadStart(delegate ()
                {
                    label1.Text = "导入完毕";
                }));
            });
            thread.IsBackground = true;
            thread.Start();





           


        }




        private void button2_Click(object sender, EventArgs e)
        {

            string FileName = @"C:\Users\Luo\Desktop\";
            int[] state = { 1, 5, 13, 21, 33, 36, 38, 48, 80, 112, 144, 147, 211, 243, 275, 278, 342, 372, 382, 390, 392, 393, 401, 403, 408, 409, 410, 470, 471 };
            int[] end = { 4, 12, 20, 32, 35, 37, 47, 79, 111, 143, 146, 210, 242, 274, 277, 341, 371, 381, 389, 391, 392, 400, 402, 407, 408, 409, 469, 470, 514 };
            string[] zptTxtData = new string[29];
            DirectoryInfo TheFolder = new DirectoryInfo(@"C:\Users\Luo\Desktop\Zpt_Send_Temp");
            foreach (DirectoryInfo NextFolder in TheFolder.GetDirectories())
            {
                foreach (FileInfo info in NextFolder.GetFiles("*.txt"))
                {
                    FileStream file = new FileStream(info.FullName.ToString(), FileMode.Open);
                    int fsLen = (int)file.Length;
                    byte[] heByte = new byte[file.Length];
                    int r = file.Read(heByte, 0, heByte.Length);
                    string strZpt = Encoding.GetEncoding("big5").GetString(heByte);
                    byte[] zptByte = Encoding.GetEncoding("big5").GetBytes(strZpt);
                    if (zptByte.Length >= 512)
                    {
                        for (int i = 0; i < state.Length; i++)
                        {
                            int dd = (state[i] - 1);
                            int eddd = end[i] - dd;
                            Byte[] ThisByte = new Byte[eddd];
                            Buffer.BlockCopy(zptByte, dd, ThisByte, 0, eddd);
                            zptTxtData[i] = Encoding.GetEncoding("big5").GetString(ThisByte);
                        }
                        file.Close();
                        file.Dispose();
                        Models zptModel = new Models();
                        SqlServerHelp zptSqlDb = new SqlServerHelp();
                        Models.zptDataInfo zptDat = zptModel.DataInfo(zptTxtData);
                        zptDat.CollectionMoney = zptSqlDb.SqlserverConn(zptDat.CollectionMoney).ToString();
                        zptDat.CollectionNo = zptDat.CollectionMoney.ObjToInt() > 0 ? "" : zptDat.CollectionNo;
                        List<Models.zptDataInfo> list = new List<Models.zptDataInfo>();
                        list.Add(zptDat);
                        DataToTxt(FileName + @"\上传目录\" + info.Name, list);

                    }
                }
            }



        }







        public Boolean DataToTxt(String FileName, List<Models.zptDataInfo> ZptList)
        {
            try
            {
                byte[] str = new byte[1];
                string zptText = String.Empty;
                foreach (var Obj in ZptList)
                {
                    zptText = Obj.SerialNo.PadLeft(4, '0').strLenSub(4);

                    zptText += Obj.DateProduction.strLenSub(8);

                    zptText += Obj.Blank8.strLenSub(8);

                    zptText += Obj.HouseNo.strLenSub(12);

                    zptText += Obj.PiecesNo.PadLeft(3, '0').strLenSub(3);

                    zptText += Obj.Specification.strLenSub(2);

                    zptText += Obj.CustomerCode.strLenSub(10);
                    ;
                    zptText += Obj.CustomerNo.strLenSub(32);

                    zptText += Obj.SenderName.strLenSub(32);

                    zptText += Obj.SenderPhone.strLenSub(32);

                    zptText += Obj.SenderPostalCode.strLenSub(3);

                    zptText += Obj.SenderAddress.strLenSub(64);

                    zptText += Obj.RecipientName.strLenSub(32);

                    zptText += Obj.RecipientPhone.strLenSub(32);

                    zptText += Obj.RecipientPostalCode.strLenSub(3);

                    zptText += Obj.ReceiverAddress.strLenSub(64);

                    zptText += Obj.Blank30.strLenSub(30);

                    zptText += Obj.Blank10.strLenSub(10);

                    zptText += Obj.SpecifyDate.strLenSub(8);

                    zptText += Obj.SpecifyTime.PadLeft(2, '0').strLenSub(2);

                    zptText += Obj.No1.strLenSub(1);

                    zptText += Obj.CollectionMoney.PadLeft(8, '0').strLenSub(8);

                    zptText += Obj.Commodities.strLenSub(2);

                    zptText += Obj.DeliveryRoute.strLenSub(5);

                    zptText += Obj.Blank1.strLenSub(1);

                    zptText += Obj.notWork.strLenSub(1);

                    zptText += Obj.Remark.strLenSub(60);

                    zptText += Obj.CollectionNo.strLenSub(1);

                    zptText += Obj.Blank42.strLenSub(42);

                }


                StreamWriter sw = new StreamWriter(FileName, true, Encoding.GetEncoding("big5"));
                sw.WriteLine(zptText);
                sw.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            string FileName = @"C:\Users\Luo\Desktop\";
            String SentPath = FileName + @"\上传目录\";
            if (!Directory.Exists(SentPath))
            {
                Directory.CreateDirectory(SentPath);
            }
            String SentBackPath = FileName + "/上传成功/";
            if (!Directory.Exists(SentBackPath))
            {
                Directory.CreateDirectory(SentBackPath);
            }
            DirectoryInfo Dr = new DirectoryInfo(SentPath);
            var Files = Dr.GetFiles().ToList();
            foreach (FileInfo info in Dr.GetFiles("*.txt"))
            {
                if (Files.Count(x => !x.Name.ToLower().Equals(Path.GetFileName(info.Name.ToLower()))) > 0)
                {
                    FtpHelper Ftp = new FtpHelper(new Uri("ftp://p2ap3.e-can.com.tw"), "webedi", "0922430089");
                    Ftp.DirectoryPath = @"/";
                    foreach (var a in Files.Where(x => !x.Name.ToLower().Equals(Path.GetFileName(info.Name.ToLower()))))
                    {
                        if (Ftp.UploadFile(a.FullName))
                        {
                            if (File.Exists(SentBackPath + Path.GetFileName(a.FullName)))
                            {
                                File.Delete(SentBackPath + Path.GetFileName(a.FullName));
                            }
                            File.Move(a.FullName, SentBackPath + Path.GetFileName(a.FullName));

                        }

                    }
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            title.MessagesShowOk("请选择要查的文件夹！");
            labSourcePath.Text= Common.SelectPath();
            DirectoryInfo TheFolder = new DirectoryInfo(labSourcePath.Text);
            if (TheFolder.GetFiles("*.txt").Length < 1)
            {
                label1.Text = "没有文件！";
                return;
            }
            label1.Text = TheFolder.GetFiles("*.txt").Length.ToString();
            title.MessagesShowOk("请选择对比结果文件夹！");
            labAimPath.Text = Common.SelectPath();
            Models zptModel = new Models();
            int[] state = { 1, 5, 13, 21, 33, 36, 38, 48, 80, 112, 144, 147, 211, 243, 275, 278, 342, 372, 382, 390, 392, 393, 401, 403, 408, 409, 410, 470, 471 };
            int[] end = { 4, 12, 20, 32, 35, 37, 47, 79, 111, 143, 146, 210, 242, 274, 277, 341, 371, 381, 389, 391, 392, 400, 402, 407, 408, 409, 469, 470, 514 };
            string[] zptTxtData = new string[29];

            System.Threading.Thread thread = new System.Threading.Thread(() =>
            {
                int count = 0;
                foreach (FileInfo info in TheFolder.GetFiles("*.txt"))
                {
                    using (FileStream file = new FileStream(info.FullName.ToString(), FileMode.Open))
                    {
                        int fsLen = (int)file.Length;
                        byte[] heByte = new byte[file.Length];
                        int r = file.Read(heByte, 0, heByte.Length);
                        string strZpt = Encoding.GetEncoding("big5").GetString(heByte);
                        byte[] zptByte = Encoding.GetEncoding("big5").GetBytes(strZpt);
                        if (zptByte.Length >= 512)
                        {
                            for (int i = 0; i < state.Length; i++)
                            {
                                int dd = (state[i] - 1);
                                int eddd = end[i] - dd;
                                Byte[] ThisByte = new Byte[eddd];
                                Buffer.BlockCopy(zptByte, dd, ThisByte, 0, eddd);
                                zptTxtData[i] = Encoding.GetEncoding("big5").GetString(ThisByte);
                            }
                            file.Close();
                            file.Dispose();
                            SqlServerHelp serverHelp = new SqlServerHelp();
                            serverHelp.EJESqlServer(zptModel.DataInfo(zptTxtData), info.FullName.ToString(), labAimPath.Text);
                            count++;
                            label2.Invoke(new System.Threading.ThreadStart(delegate ()
                            {
                                label2.Text = "检查：" + count;
                            }));
                            
                        }
                    }

                }
            });
            thread.IsBackground = true;
            thread.Start();



        }
    }
}
