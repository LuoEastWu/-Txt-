using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 读取Txt文本到数据库
{
   public static class StrHeplClass
    {
        public static string strLenSub(this string str, int len)
        {
            str = ToTraditionalChinese(str);
            byte[] Strbyte = Encoding.GetEncoding("big5").GetBytes(str);
            if (Strbyte.Length < len)
            {
                Strbyte = Encoding.GetEncoding("big5").GetBytes(str.PadRight(len, ' '));
            }
            Byte[] ThisByte = new Byte[len];
            if (Strbyte.Length > len)
            {
                Buffer.BlockCopy(Strbyte, 0, ThisByte, 0, len);
            }
            else
            {

                Buffer.BlockCopy(Strbyte, 0, ThisByte, 0, Strbyte.Length);
            }
            return Encoding.GetEncoding("big5").GetString(ThisByte);
        }
        /// <summary>
        /// 字符串简体转繁体
        /// </summary>
        /// <param name="strSimple"></param>
        /// <returns></returns>
        public static string ToTraditionalChinese(string strSimple)
        {
            string strTraditional = Microsoft.VisualBasic.Strings.StrConv(strSimple, Microsoft.VisualBasic.VbStrConv.TraditionalChinese, 0);
            return strTraditional;
        }
    }
}
