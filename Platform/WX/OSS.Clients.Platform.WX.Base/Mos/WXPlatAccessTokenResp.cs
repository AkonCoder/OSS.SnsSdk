﻿namespace OSS.Clients.Platform.WX.Base.Mos
{
    /// <summary>
    ///   公众号功能接口accesstoken信息
    /// </summary>
    public class WXPlatAccessTokenResp : WXBaseResp
    {
        /// <summary>
        ///   token
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// 	凭证有效时间，单位：秒
        /// </summary>
        public int expires_in { get; set; }

        /// <summary>
        /// 【UTC】过期时间，接口获取数据后根据expires_in 计算的值(可以扣除十分钟，作为中间的缓冲值)
        /// </summary>
        public long expires_date { get; set; }
    }

}
