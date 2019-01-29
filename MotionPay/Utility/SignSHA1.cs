using MotionPay.Config;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using TuYu.Common;

namespace MotionPay.Utility
{
    public class SignSHA1
    {
        public static string getSign(Dictionary<string, string> jsonBody)
        {
            string signStr = getSignOnline(jsonBody, PayConfig.WECHAT_MINI_APPID, PayConfig.WECHAT_MINI_APPSECURE);
            return signStr;
        }
        public static string getSignOnline(Dictionary<string, string> jsonObject, string appId, string appSecret)
        {
            {
                var sb = new StringBuilder();
                Dictionary<string, string> innerMap = jsonObject;
                string signature = "";
                var sortMapByKey = new ArrayList(innerMap.Keys);
                sortMapByKey.Sort();
                foreach (string k in sortMapByKey)
                {
                    var v = innerMap[k];
                    if (null != v && "".CompareTo(v) != 0
                        && "sign".CompareTo(k) != 0 && "key".CompareTo(k) != 0)
                    {
                        sb.Append(k + "=" + v + "&");
                    }
                }
                if (!string.IsNullOrEmpty(appId))
                {
                    sb.Append("appid=" + appId + "&appsecret=" + appSecret);
                }
                Log.WriteLog("beforesign", sb.ToString());
                try
                {
                    signature = Sha1(sb.ToString());
                }
                catch (Exception ex)
                {
                    Log.WriteLog("getSignOnlineEx", ex.Message + "," + ex.StackTrace);
                }
                string upperCase = signature.ToUpper();
                return upperCase;
            }
        }
        /// <summary>
        /// 基于Sha1的自定义加密字符串方法：输入一个字符串，返回一个由40个字符组成的十六进制的哈希散列（字符串）。
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <returns>加密后的十六进制的哈希散列（字符串）</returns>
        public static string Sha1(string str)
        {
            var buffer = Encoding.UTF8.GetBytes(str);
            var data = SHA1.Create().ComputeHash(buffer);

            var sb = new StringBuilder();
            foreach (var t in data)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString();
        }
    }
}