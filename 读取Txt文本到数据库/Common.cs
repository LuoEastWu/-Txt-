using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace 读取Txt文本到数据库
{
    public static class Common
    {

        /// <summary>
        /// 字符串繁体转简体
        /// </summary>
        /// <param name="strTraditional"></param>
        /// <returns></returns>
        public static string ToSimplifiedChinese(this string strTraditional)
        {
            string strSimple = Microsoft.VisualBasic.Strings.StrConv(strTraditional, VbStrConv.SimplifiedChinese, 0);
            return strSimple;
        }
        /// <summary>
        /// 转换Int
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static int ObjToInt(this object thisValue)
        {
            int reval = 0;
            if (thisValue == null) return 0;
            if (thisValue.GetType().IsEnum) return Convert.ToInt32(thisValue);
            if (thisValue != null && thisValue != DBNull.Value && int.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return reval;
        }

        /// <summary>
        /// 移动文件夹
        /// </summary>
        /// <param name="sourceFilePath"></param>
        /// <param name="pathFile"></param>
        public static void FileMove(this string sourceFilePath, string pathFile)
        {
            if (!Directory.Exists(pathFile))
            {
                Directory.CreateDirectory(pathFile);
            }

            //移动文件
            var vfile = new FileInfo(sourceFilePath);
            var destFileName = pathFile+"\\" + vfile.Name;
            if (File.Exists(destFileName))
            {
                if ("提示".MessagesShowOkCancel("文件已存在！是否覆盖") == System.Windows.Forms.DialogResult.OK)
                {
                    File.Delete(destFileName);
                }
                else
                {
                    return;
                }
            }
            File.Move(sourceFilePath, destFileName);
        }
        /// <summary>
        /// 选择路径
        /// </summary>
        /// <returns></returns>
        public static String SelectPath()
        {
            string filePath = string.Empty;
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filePath = fbd.SelectedPath;
            }
            return filePath;
        }

        /// <summary>
        /// Ok提示框
        /// </summary>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        public static void MessagesShowOk(this string title,string msg)
        {
            System.Windows.Forms.MessageBox.Show(msg, title, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Asterisk);
        }
        /// <summary>
        /// 选择框
        /// </summary>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        public static System.Windows.Forms.DialogResult MessagesShowOkCancel(this string title, string msg)
        {
           return System.Windows.Forms.MessageBox.Show(msg, title, System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Error);
        }
    }
}
