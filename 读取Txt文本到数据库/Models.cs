using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 读取Txt文本到数据库
{
    public class Models
    {

        /// <summary>
        /// 文档生成数据
        /// </summary>
        public class zptDataInfo
        {
            /// <summary>
            /// 序号
            /// </summary>
            public string SerialNo { get; set; }
            /// <summary>
            /// 资料生成日
            /// </summary>
            public string DateProduction { get; set; }
            /// <summary>
            /// 固定為空白
            /// </summary>
            public string Blank8 { get; set; }
            /// <summary>
            /// 宅配單號
            /// </summary>
            public string HouseNo { get; set; }
            /// <summary>
            /// 件數
            /// </summary>
            public string PiecesNo { get; set; }
            /// <summary>
            /// 規格
            /// </summary>
            public string Specification { get; set; }
            /// <summary>
            /// 客戶代號
            /// </summary>
            public string CustomerCode { get; set; }
            /// <summary>
            /// 客戶單號
            /// </summary>
            public string CustomerNo { get; set; }
            /// <summary>
            /// 寄件人名稱
            /// </summary>
            public string SenderName { get; set; }
            /// <summary>
            /// 寄件人電話
            /// </summary>
            public string SenderPhone { get; set; }
            /// <summary>
            /// 寄件人郵遞區號
            /// </summary>
            public string SenderPostalCode { get; set; }
            /// <summary>
            /// 寄件人地址
            /// </summary>
            public string SenderAddress { get; set; }
            /// <summary>
            /// 收件人名稱
            /// </summary>
            public string RecipientName { get; set; }
            /// <summary>
            /// 收件人電話
            /// </summary>
            public string RecipientPhone { get; set; }
            /// <summary>
            /// 收件人郵遞區號
            /// </summary>
            public string RecipientPostalCode { get; set; }
            /// <summary>
            /// 收件人地址
            /// </summary>
            public string ReceiverAddress { get; set; }
            /// <summary>
            /// 固定為空白
            /// </summary>
            public string Blank30 { get; set; }
            /// <summary>
            /// 固定為空白
            /// </summary>
            public string Blank10 { get; set; }
            /// <summary>
            /// 指定日期
            /// </summary>
            public string SpecifyDate { get; set; }
            /// <summary>
            /// 指定時段
            /// </summary>
            public string SpecifyTime { get; set; }
            /// <summary>
            /// 固定為[1]
            /// </summary>
            public string No1 { get; set; }
            /// <summary>
            ///代收款
            /// </summary>
            public string CollectionMoney { get; set; }
            /// <summary>
            ///商品別
            /// </summary>
            public string Commodities { get; set; }
            /// <summary>
            ///配送路線
            /// </summary>
            public string DeliveryRoute { get; set; }
            /// <summary>
            ///空白
            /// </summary>
            public string Blank1 { get; set; }
            /// <summary>
            ///作業別
            /// </summary>
            public string notWork { get; set; }
            /// <summary>
            ///備註
            /// </summary>
            public string Remark { get; set; }
            /// <summary>
            ///代收款收款記號
            /// </summary>
            public string CollectionNo { get; set; }
            /// <summary>
            ///固定為空白
            /// </summary>
            public string Blank42 { get; set; }
        }

        public Models.zptDataInfo DataInfo(string[] zptTxtData)
        {
            Models.zptDataInfo zptDat = new Models.zptDataInfo();

            zptDat.SerialNo = zptTxtData[0];
            zptDat.DateProduction = zptTxtData[1];
            zptDat.Blank8 = zptTxtData[2];
            zptDat.HouseNo = zptTxtData[3];
            zptDat.PiecesNo = zptTxtData[4];
            zptDat.Specification = zptTxtData[5];
            zptDat.CustomerCode = zptTxtData[6];
            zptDat.CustomerNo = zptTxtData[7];
            zptDat.SenderName = zptTxtData[8];
            zptDat.SenderPhone = zptTxtData[9];
            zptDat.SenderPostalCode = zptTxtData[10];
            zptDat.SenderAddress = zptTxtData[11];
            zptDat.RecipientName = zptTxtData[12];
            zptDat.RecipientPhone = zptTxtData[13];
            zptDat.RecipientPostalCode = zptTxtData[14];
            zptDat.ReceiverAddress = zptTxtData[15];
            zptDat.Blank30 = zptTxtData[16];
            zptDat.Blank10 = zptTxtData[17];
            zptDat.SpecifyDate = zptTxtData[18];
            zptDat.SpecifyTime = zptTxtData[19];
            zptDat.No1 = zptTxtData[20];
            zptDat.CollectionMoney = zptTxtData[21];
            zptDat.Commodities = zptTxtData[22];
            zptDat.DeliveryRoute = zptTxtData[23];
            zptDat.Blank1 = zptTxtData[24];
            zptDat.notWork = zptTxtData[25];
            zptDat.Remark = zptTxtData[26];
            zptDat.CollectionNo = zptTxtData[27];
            zptDat.Blank42 = zptTxtData[28];
            return zptDat;
        }

      
    }
}
