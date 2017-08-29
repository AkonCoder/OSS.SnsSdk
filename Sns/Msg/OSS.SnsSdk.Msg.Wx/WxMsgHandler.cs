﻿#region Copyright (C) 2017  Kevin  （OS系列开源项目）

/***************************************************************************
*　　	文件功能描述：消息对话事件句柄基类，主要声明相关事件
*
*　　	创建人： kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-13
*       
*****************************************************************************/

#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using OSS.Common.ComModels;
using OSS.Common.ComModels.Enums;
using OSS.Common.Encrypt;
using OSS.Common.Extention;
using OSS.SnsSdk.Msg.Wx.Mos;

namespace OSS.SnsSdk.Msg.Wx
{
    /// <inheritdoc />
    /// <summary>
    /// 消息处理类
    ///  </summary>
    public class WxMsgHandler: WxMsgBaseHandler
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mConfig"></param>
        public WxMsgHandler(WxMsgConfig mConfig=null):base(mConfig)
        {
           
        }

        #region   基础消息的事件列表

        #region 事件列表  普通消息

        /// <summary>
        /// 处理文本消息
        /// </summary>
        protected virtual WxBaseReplyMsg ProcessTextMsg(WxTextRecMsg msg)
        {
            return new WxNoneReplyMsg();
        }

        /// <summary>
        /// 处理图像消息
        /// </summary>
        protected virtual WxBaseReplyMsg ProcessImageMsg(WxImageRecMsg msg)
        {
            return new WxNoneReplyMsg();
        }

        /// <summary>
        /// 处理语音消息
        /// </summary>
        protected virtual WxBaseReplyMsg ProcessVoiceMsg(WxVoiceRecMsg msg)
        {
            return new WxNoneReplyMsg();
        }

        /// <summary>
        /// 处理视频/小视频消息
        /// </summary>
        protected virtual WxBaseReplyMsg ProcessVideoMsg(WxVideoRecMsg msg)
        {
            return new WxNoneReplyMsg();
        }

        /// <summary>
        /// 处理地理位置消息
        /// </summary>
        protected virtual WxBaseReplyMsg ProcessLocationMsg(WxLocationRecMsg msg)
        {
            return new WxNoneReplyMsg();
        }

        /// <summary>
        /// 处理链接消息
        /// </summary>
        protected virtual WxBaseReplyMsg ProcessLinkMsg(WxLinkRecMsg msg)
        {
            return new WxNoneReplyMsg();
        }


        #endregion
        
        #region 事件列表  动作事件消息

        /// <summary>
        /// 处理关注/取消关注事件
        /// </summary>
        protected virtual WxBaseReplyMsg ProcessSubscribeEventMsg(WxSubscribeRecEventMsg msg)
        {
            return new WxNoneReplyMsg();
        }

        /// <summary>
        /// 处理扫描带参数二维码事件
        /// </summary>
        protected virtual WxBaseReplyMsg ProcessScanEventMsg(WxSubscribeRecEventMsg msg)
        {
            return new WxNoneReplyMsg();
        }

        /// <summary>
        /// 处理上报地理位置事件
        /// 不需要回复任何消息
        /// </summary>
        protected virtual WxNoneReplyMsg ProcessLocationEventMsg(WxLocationRecEventMsg msg)
        {
            return new WxNoneReplyMsg();
        }

        /// <summary>
        /// 处理点击菜单拉取消息时的事件推送
        /// </summary>
        protected virtual WxBaseReplyMsg ProcessClickEventMsg(WxClickRecEventMsg msg)
        {
            return new WxNoneReplyMsg();
        }

        /// <summary>
        /// 处理点击菜单跳转链接时的事件推送 
        /// </summary>
        protected virtual WxBaseReplyMsg ProcessViewEventMsg(WxViewRecEventMsg msg)
        {
            return new WxNoneReplyMsg();
        }

        #endregion

        #endregion
        
        internal override WxMsgProcessor GetBasicMsgProcessor(string msgType, string eventName)
        {
            WxMsgProcessor processor = null;
            switch (msgType.ToLower())
            {
                case "event":
                    processor = GetBasicEventMsgProcessor(eventName);
                    break;
                case "text":
                    processor = new WxMsgProcessor<WxTextRecMsg>()
                        {RecMsgInsCreater = () => new WxTextRecMsg(), ProcessFunc = ProcessTextMsg};
                    break;
                case "image":
                    processor = new WxMsgProcessor<WxImageRecMsg>()
                        {RecMsgInsCreater = () => new WxImageRecMsg(), ProcessFunc = ProcessImageMsg};
                    break;
                case "voice":
                    processor = new WxMsgProcessor<WxVoiceRecMsg>()
                        {RecMsgInsCreater = () => new WxVoiceRecMsg(), ProcessFunc = ProcessVoiceMsg};
                    break;
                case "video":
                    processor = new WxMsgProcessor<WxVideoRecMsg>()
                        {RecMsgInsCreater = () => new WxVideoRecMsg(), ProcessFunc = ProcessVideoMsg};
                    break;
                case "shortvideo":
                    processor = new WxMsgProcessor<WxVideoRecMsg>()
                        {RecMsgInsCreater = () => new WxVideoRecMsg(), ProcessFunc = ProcessVideoMsg};
                    break;
                case "location":
                    processor = new WxMsgProcessor<WxLocationRecMsg>()
                        {RecMsgInsCreater = () => new WxLocationRecMsg(), ProcessFunc = ProcessLocationMsg};
                    break;
                case "link":
                    processor = new WxMsgProcessor<WxLinkRecMsg>()
                        {RecMsgInsCreater = () => new WxLinkRecMsg(), ProcessFunc = ProcessLinkMsg};
                    break;
            }
            return processor ?? base.GetBasicMsgProcessor(msgType, eventName);
        }

        private WxMsgProcessor GetBasicEventMsgProcessor(string eventName)
        {
            WxMsgProcessor processor = null;
            switch (eventName)
            {
                case "subscribe":
                    processor = new WxMsgProcessor<WxSubscribeRecEventMsg>()
                        { RecMsgInsCreater = () => new WxSubscribeRecEventMsg(), ProcessFunc = ProcessSubscribeEventMsg };
                    break;
                case "unsubscribe":
                    processor = new WxMsgProcessor<WxSubscribeRecEventMsg>()
                        { RecMsgInsCreater = () => new WxSubscribeRecEventMsg(), ProcessFunc = ProcessSubscribeEventMsg };
                      break;
                case "scan":
                    processor = new WxMsgProcessor<WxSubscribeRecEventMsg>()
                        { RecMsgInsCreater = () => new WxSubscribeRecEventMsg(), ProcessFunc = ProcessScanEventMsg };
                    break;
                case "location":
                    processor = new WxMsgProcessor<WxLocationRecEventMsg>()
                        { RecMsgInsCreater = () => new WxLocationRecEventMsg(), ProcessFunc = ProcessLocationEventMsg };
                    break;
                case "click":
                    processor = new WxMsgProcessor<WxClickRecEventMsg>()
                        { RecMsgInsCreater = () => new WxClickRecEventMsg(), ProcessFunc = ProcessClickEventMsg };
                    break;
                case "view":
                    processor = new WxMsgProcessor<WxViewRecEventMsg>()
                        { RecMsgInsCreater = () => new WxViewRecEventMsg(), ProcessFunc = ProcessViewEventMsg };
                    break;
            }
            return processor;
        }
    }


    internal static class WxMsgHelper
    {
        #region   消息内容加解密辅助方法

        /// <summary>
        /// 验证签名方法
        /// </summary>
        /// <param name="token"></param>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        internal static ResultMo CheckSignature(string token, string signature,
            string timestamp, string nonce)
        {
            return signature == GenerateSignature(token, timestamp, nonce)
                ? new ResultMo() 
                : new ResultMo(ResultTypes.UnAuthorize, "签名验证失败！");
        }


        /// <summary>
        /// 验证签名方法
        /// </summary>
        /// <param name="token"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="strEncryptMsg"></param>
        /// <returns></returns>
        internal static string GenerateSignature(string token,
            string timestamp, string nonce, string strEncryptMsg = "")
        {
            var strList = new List<string>() { token, timestamp, nonce, strEncryptMsg };
            strList.Sort();

            var waitEncropyStr = string.Join(string.Empty, strList);
            return Sha1.Encrypt(waitEncropyStr, Encoding.ASCII);
        }



        /// <summary>
        ///  加密消息体
        /// </summary>
        /// <param name="sReplyMsg"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        internal static ResultMo<string> EncryptMsg(string sReplyMsg, WxMsgConfig config)
        {
            string raw;
            try
            {
                raw = Cryptography.AesEncrypt(sReplyMsg, config.EncodingAesKey, config.AppId);
            }
            catch (Exception)
            {
                return new ResultMo<string>(ResultTypes.InnerError, "加密响应消息体出错！");
            }
            var date = DateTime.Now;

            var sTimeStamp = date.ToUtcSeconds().ToString();
            var sNonce = date.ToString("yyyyMMddHHssff");


            var msgSigature = GenerateSignature(config.Token, sTimeStamp, sNonce, raw);
            if (string.IsNullOrEmpty(msgSigature))
            {
                return new ResultMo<string>(ResultTypes.InnerError, "生成签名信息出错！");
            }

            var sEncryptMsg = new StringBuilder();

            const string encryptLabelHead = "<Encrypt><![CDATA[";
            const string encryptLabelTail = "]]></Encrypt>";
            const string msgSigLabelHead = "<MsgSignature><![CDATA[";
            const string timeStampLabelHead = "<TimeStamp><![CDATA[";

            const string timeStampLabelTail = "]]></TimeStamp>";
            const string nonceLabelHead = "<Nonce><![CDATA[";
            const string nonceLabelTail = "]]></Nonce>";

            sEncryptMsg.Append("<xml>").Append(encryptLabelHead).Append(raw).Append(encryptLabelTail);
            sEncryptMsg.Append(msgSigLabelHead).Append(msgSigature).Append("]]></MsgSignature>");
            sEncryptMsg.Append(timeStampLabelHead).Append(sTimeStamp).Append(timeStampLabelTail);
            sEncryptMsg.Append(nonceLabelHead).Append(sNonce).Append(nonceLabelTail);
            sEncryptMsg.Append("</xml>");

            return new ResultMo<string>(sEncryptMsg.ToString());
        }


        #endregion

        #region  消息内容辅助类
        
        /// <summary>
        /// 把xml文本转化成字典对象
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="xmlDoc">返回格式化后的xml对象</param>
        /// <returns></returns>
        internal static Dictionary<string, string> ChangXmlToDir(string xml,ref XmlDocument xmlDoc)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }


            xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            var xmlNode = xmlDoc.FirstChild;
            var nodes = xmlNode.ChildNodes;

            var dirs = new Dictionary<string, string>(nodes.Count);

            foreach (XmlNode xn in nodes)
            {
                var xe = (XmlElement)xn;
                dirs[xe.Name] = xe.InnerText;
            }

            return dirs;
        }
        #endregion
    }
}
