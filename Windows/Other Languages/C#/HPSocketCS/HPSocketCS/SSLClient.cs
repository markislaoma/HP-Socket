using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace HPSocketCS
{
    public class SSLClient : TcpClient
    {
        /// <summary>
        /// 验证模式
        /// </summary>
        public SSLVerifyMode VerifyMode { get; set; }
        /// <summary>
        /// 证书文件（客户端可选）
        /// </summary>
        public string PemCertFile { get; set; }
        /// <summary>
        /// 私钥文件（客户端可选）
        /// </summary>
        public string PemKeyFile { get; set; }
        /// <summary>
        /// 私钥密码（没有密码则为空）
        /// </summary>
        public string KeyPassword { get; set; }
        /// <summary>
        /// CA 证书文件或目录（单向验证或客户端可选）
        /// </summary>
        public string CAPemCertFileOrPath { get; set; }
        public SSLClient()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="verifyModel">验证模式</param>
        /// <param name="pemCertFile">证书文件（客户端可选）</param>
        /// <param name="pemKeyFile">私钥文件（客户端可选）</param>
        /// <param name="keyPassword">私钥密码（没有密码则为空）</param>
        /// <param name="caPemCertFileOrPath">CA 证书文件或目录（单向验证或客户端可选）</param>
        public SSLClient(SSLVerifyMode verifyModel, string pemCertFile, string pemKeyFile, string keyPassword, string caPemCertFileOrPath)
        {
            this.VerifyMode = verifyModel;
            this.PemCertFile = pemCertFile;
            this.PemKeyFile = pemKeyFile;
            this.KeyPassword = keyPassword;
            this.CAPemCertFileOrPath = caPemCertFileOrPath;
        }


        protected override bool CreateListener()
        {
            if (IsCreate == true || pListener != IntPtr.Zero || pClient != IntPtr.Zero)
            {
                return false;
            }

            pListener = Sdk.Create_HP_TcpClientListener();
            if (pListener == IntPtr.Zero)
            {
                return false;
            }

            pClient = SSLSdk.Create_HP_SSLClient(pListener);
            if (pClient == IntPtr.Zero)
            {
                return false;
            }

            IsCreate = true;

            return true;
        }

        /// <summary>
        /// 初始化SSL环境
        /// <param name="memory">是否通过内存加载证书</param>
        /// </summary>
        /// <returns></returns>
        public virtual bool Initialize(bool memory = false)
        {
            if (pClient != IntPtr.Zero)
            {

                PemCertFile = string.IsNullOrWhiteSpace(PemCertFile) ? null : PemCertFile;
                PemKeyFile = string.IsNullOrWhiteSpace(PemKeyFile) ? null : PemKeyFile;
                KeyPassword = string.IsNullOrWhiteSpace(KeyPassword) ? null : KeyPassword;
                CAPemCertFileOrPath = string.IsNullOrWhiteSpace(CAPemCertFileOrPath) ? null : CAPemCertFileOrPath;

                return memory
                    ? SSLSdk.HP_SSLClient_SetupSSLContextByMemory(pClient, VerifyMode, PemCertFile, PemKeyFile, KeyPassword, CAPemCertFileOrPath)
                    : SSLSdk.HP_SSLClient_SetupSSLContext(pClient, VerifyMode, PemCertFile, PemKeyFile, KeyPassword, CAPemCertFileOrPath);
            }

            return false;
        }


        /// <summary>
        /// 反初始化SSL环境
        /// </summary>
        public virtual void UnInitialize()
        {
            if (pClient != IntPtr.Zero)
            {
                SSLSdk.HP_SSLClient_CleanupSSLContext(pClient);
            }
        }

        public override void Destroy()
        {
            Stop();
            if (pClient != IntPtr.Zero)
            {
                SSLSdk.Destroy_HP_SSLClient(pClient);
                pClient = IntPtr.Zero;
            }
            if (pListener != IntPtr.Zero)
            {
                Sdk.Destroy_HP_TcpClientListener(pListener);
                pListener = IntPtr.Zero;
            }

            IsCreate = false;
        }


        /// <summary>
        /// 启动 SSL 握手
        /// 当通信组件设置为非自动握手时，需要调用本方法启动 SSL 握手
        /// </summary>
        /// <returns></returns>
        public bool StartSSLHandShake()
        {
            return SSLSdk.HP_SSLClient_StartSSLHandShake(pClient);
        }

        /// <summary>
        /// 获取或设置通信组件握手方式（默认：TRUE，自动握手)
        /// </summary>
        public bool AutoHandShake
        {
            get
            {
                return SSLSdk.HP_SSLClient_IsSSLAutoHandShake(pClient);
            }
            set
            {
                SSLSdk.HP_SSLClient_SetSSLAutoHandShake(pClient, value);
            }
        }

        /// <summary>
        /// 获取指定类型的 SSL Session 信息（输出类型参考：SSLSessionInfo）
        /// </summary>
        /// <param name="info">指定获取内容的类型</param>
        /// <returns></returns>
        public IntPtr GetSSLSessionInfo(SSLSessionInfo info)
        {
            var ret = IntPtr.Zero;
            SSLSdk.HP_SSLClient_GetSSLSessionInfo(pClient, info, ref ret);
            return ret;
        }
    }
}
