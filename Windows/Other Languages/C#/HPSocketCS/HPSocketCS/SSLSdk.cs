using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace HPSocketCS
{
    /// <summary>
    /// SSL Session 信息类型，用于 GetSSLSessionInfo()，标识输出的 Session 信息类型
    /// </summary>
    public enum SSLSessionInfo
    {
        /// <summary>
        /// min
        /// </summary>
        Min = 0,
        /// <summary>
        ///  SSL CTX（输出类型：SSL_CTX*）
        /// </summary>
        Ctx = 0,
        /// <summary>
        /// SSL CTX Method （输出类型：SSL_METHOD*）
        /// </summary>
        CtxMethod = 1,
        /// <summary>
        /// SSL CTX Ciphers （输出类型：STACK_OF(SSL_CIPHER)*）
        /// </summary>
        CtxCiphers = 2,
        /// <summary>
        /// SSL CTX Cert Store （输出类型：X509_STORE*）
        /// </summary>
        CtxCertStore = 3,
        /// <summary>
        /// Server Name Type （输出类型：int）
        /// </summary>
        ServerNameType = 4,
        /// <summary>
        /// Server Name （输出类型：LPCSTR）
        /// </summary>
        ServerName = 5,
        /// <summary>
        /// SSL Version （输出类型：LPCSTR）
        /// </summary>
        Version = 6,
        /// <summary>
        /// SSL Method （输出类型：SSL_METHOD*）
        /// </summary>
        Method = 7,
        /// <summary>
        /// SSL Cert （输出类型：X509*）
        /// </summary>
        Cert = 8,
        /// <summary>
        /// SSL Private Key （输出类型：EVP_PKEY*）
        /// </summary>
        PrivateKey = 9,
        /// <summary>
        /// SSL Current Cipher （输出类型：SSL_CIPHER*）
        /// </summary>
        CurrentCipher = 10,
        /// <summary>
        /// SSL Available Ciphers（输出类型：STACK_OF(SSL_CIPHER)*）
        /// </summary>
        Ciphers = 11,
        /// <summary>
        /// SSL Client Ciphers （输出类型：STACK_OF(SSL_CIPHER)*）
        /// </summary>
        ClientCiphers = 12,
        /// <summary>
        /// SSL Peer Cert （输出类型：X509*）
        /// </summary>
        PeerCert = 13,
        /// <summary>
        /// SSL Peer Cert Chain （输出类型：STACK_OF(X509)*）
        /// </summary>
        PeerCertChain = 14,
        /// <summary>
        /// SSL Verified Chain （输出类型：STACK_OF(X509)*）
        /// </summary>
        VerifiedChain = 15,
        /// <summary>
        /// max
        /// </summary>
        Max = 15,
    }

    /// <summary>
    /// SSL 工作模式
    /// 描述：标识 SSL 的工作模式，客户端模式或服务端模式
    /// </summary>
    public enum SSLSessionMode
    {
        /// <summary>
        /// 客户端模式
        /// </summary>
        Client = 0,
        /// <summary>
        /// 服务端模式
        /// </summary>
        Server = 1,
    }

    /// <summary>
    /// 名称：SSL 验证模式
    /// 描述：SSL 验证模式选项，SSL_VM_PEER 可以和后面两个选项组合一起
    /// </summary>
    public enum SSLVerifyMode
    {
        /// <summary>
        /// SSL_VERIFY_NONE
        /// </summary>
        None = 0x00,
        /// <summary>
        /// SSL_VERIFY_PEER
        /// </summary>
        Peer = 0x01,
        /// <summary>
        /// SSL_VERIFY_FAIL_IF_NO_PEER_CERT
        /// </summary>
        FailIfNoPeerCert = 0x02,
        /// <summary>
        /// SSL_VERIFY_CLIENT_ONCE
        /// </summary>
        ClientOnce = 0x04,
    }


    public class SSLSdk
    {
        /// <summary>
        /// 名称：SNI 服务名称回调函数
        /// 描述：根据服务器名称选择 SSL 证书
        /// 返回值：
		/// 0	 -- 成功，使用默认 SSL 证书
        /// 正数	 -- 成功，使用返回值对应的 SNI 主机证书
        /// 负数	 -- 失败，中断 SSL 握手
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="pContext"></param>
        /// <returns></returns>
        public delegate int SNIServerNameCallback(string serverName, IntPtr pContext);

        /**************** HPSocket4C 导出函数 ****************/
        /// <summary>
        /// 创建 HP_SSLServer 对象
        /// </summary>
        /// <param name="pListener"></param>
        /// <returns></returns>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern IntPtr Create_HP_SSLServer(IntPtr pListener);

        /// <summary>
        /// 创建 HP_SSLAgent 对象
        /// </summary>
        /// <param name="pListener"></param>
        /// <returns></returns>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern IntPtr Create_HP_SSLAgent(IntPtr pListener);

        /// <summary>
        /// 创建 HP_SSLClient 对象
        /// </summary>
        /// <param name="pListener"></param>
        /// <returns></returns>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern IntPtr Create_HP_SSLClient(IntPtr pListener);

        /// <summary>
        /// 创建 HP_SSLPullServer 对象
        /// </summary>
        /// <param name="pListener"></param>
        /// <returns></returns>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern IntPtr Create_HP_SSLPullServer(IntPtr pListener);

        /// <summary>
        /// 创建 HP_SSLPullAgent 对象
        /// </summary>
        /// <param name="pListener"></param>
        /// <returns></returns>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern IntPtr Create_HP_SSLPullAgent(IntPtr pListener);

        /// <summary>
        /// 创建 HP_SSLPullClient 对象
        /// </summary>
        /// <param name="pListener"></param>
        /// <returns></returns>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern IntPtr Create_HP_SSLPullClient(IntPtr pListener);

        /// <summary>
        /// 创建 HP_SSLPackServer 对象
        /// </summary>
        /// <param name="pListener"></param>
        /// <returns></returns>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern IntPtr Create_HP_SSLPackServer(IntPtr pListener);

        /// <summary>
        /// 创建 HP_SSLPackAgent 对象
        /// </summary>
        /// <param name="pListener"></param>
        /// <returns></returns>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern IntPtr Create_HP_SSLPackAgent(IntPtr pListener);

        /// <summary>
        /// 创建 HP_SSLPackClient 对象
        /// </summary>
        /// <param name="pListener"></param>
        /// <returns></returns>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern IntPtr Create_HP_SSLPackClient(IntPtr pListener);



        /// <summary>
        /// 销毁 HP_SSLServer 对象
        /// </summary>
        /// <param name="pObj"></param>
        /// <returns></returns>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void Destroy_HP_SSLServer(IntPtr pObj);

        /// <summary>
        /// 销毁 HP_SSLAgent 对象
        /// </summary>
        /// <param name="pObj"></param>
        /// <returns></returns>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void Destroy_HP_SSLAgent(IntPtr pObj);

        /// <summary>
        /// 销毁 HP_SSLClient 对象
        /// </summary>
        /// <param name="pObj"></param>
        /// <returns></returns>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void Destroy_HP_SSLClient(IntPtr pObj);

        /// <summary>
        /// 销毁 HP_SSLPullServer 对象
        /// </summary>
        /// <param name="pObj"></param>
        /// <returns></returns>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void Destroy_HP_SSLPullServer(IntPtr pObj);

        /// <summary>
        /// 销毁 HP_SSLPullAgent 对象
        /// </summary>
        /// <param name="pObj"></param>
        /// <returns></returns>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void Destroy_HP_SSLPullAgent(IntPtr pObj);

        /// <summary>
        /// 销毁 HP_SSLPullClient 对象
        /// </summary>
        /// <param name="pObj"></param>
        /// <returns></returns>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void Destroy_HP_SSLPullClient(IntPtr pObj);

        /// <summary>
        /// 销毁 HP_SSLPackServer 对象
        /// </summary>
        /// <param name="pObj"></param>
        /// <returns></returns>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void Destroy_HP_SSLPackServer(IntPtr pObj);

        /// <summary>
        /// 销毁 HP_SSLPackAgent 对象
        /// </summary>
        /// <param name="pObj"></param>
        /// <returns></returns>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void Destroy_HP_SSLPackAgent(IntPtr pObj);

        /// <summary>
        /// 销毁 HP_SSLPackClient 对象
        /// </summary>
        /// <param name="pObj"></param>
        /// <returns></returns>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void Destroy_HP_SSLPackClient(IntPtr pObj);

        /************************ SSL 初始化方法 ****************************/

        /// <summary>
        /// 名称：初始化通信组件 SSL 环境参数
        /// 描述：SSL 环境参数必须在 SSL 通信组件启动前完成初始化，否则启动失败
        /// </summary>
        /// <param name="pAgent"></param>
        /// <param name="verifyMode">SSL 验证模式（参考 EnSSLVerifyMode）</param>
        /// <param name="lpszPemCertFile">证书文件（客户端可选）</param>
        /// <param name="lpszPemKeyFile">私钥文件（客户端可选）</param>
        /// <param name="lpszKeyPassword">私钥密码（没有密码则为空）</param>
        /// <param name="lpszCAPemCertFileOrPath"> CA 证书文件或目录（单向验证或客户端可选）</param>
        /// <returns>TRUE.成功 FALSE.失败，可通过 SYS_GetLastError() 获取失败原因</returns>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern bool HP_SSLAgent_SetupSSLContext(IntPtr pAgent, SSLVerifyMode verifyMode, string lpszPemCertFile, string lpszPemKeyFile, string lpszKeyPassword, string lpszCAPemCertFileOrPath);


        /// <summary>
        /// 初始化通信组件 SSL 环境参数（通过内存加载证书）
        /// 描述：SSL 环境参数必须在 SSL 通信组件启动前完成初始化，否则启动失败
        /// </summary>
        /// <param name="pAgent"></param>
        /// <param name="verifyMode">SSL 验证模式（参考 EnSSLVerifyMode）</param>
        /// <param name="lpszPemCert">证书内容</param>
        /// <param name="lpszPemKey">私钥内容</param>
        /// <param name="lpszKeyPassword">私钥密码（没有密码则为空）</param>
        /// <param name="lpszCAPemCert">CA 证书内容（单向验证或客户端可选）</param>
        /// <returns>TRUE	-- 成功，FALSE	-- 失败，可通过 SYS_GetLastError() 获取失败原因</returns>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern bool HP_SSLAgent_SetupSSLContextByMemory(IntPtr pAgent, SSLVerifyMode verifyMode /* SSL_VM_NONE */, string lpszPemCert /* nullptr */, string lpszPemKey /* nullptr */, string lpszKeyPassword /* nullptr */, string lpszCAPemCert /* nullptr */);


        /// <summary>
        /// 名称：初始化通信组件 SSL 环境参数
        /// 描述：SSL 环境参数必须在 SSL 通信组件启动前完成初始化，否则启动失败
        /// </summary>
        /// <param name="pClient"></param>
        /// <param name="verifyMode">SSL 验证模式（参考 EnSSLVerifyMode）</param>
        /// <param name="lpszPemCertFile">证书文件（客户端可选）</param>
        /// <param name="lpszPemKeyFile">私钥文件（客户端可选）</param>
        /// <param name="lpszKeyPassword">私钥密码（没有密码则为空）</param>
        /// <param name="lpszCAPemCertFileOrPath"> CA 证书文件或目录（单向验证或客户端可选）</param>
        /// <returns>TRUE.成功 FALSE.失败，可通过 SYS_GetLastError() 获取失败原因</returns>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern bool HP_SSLClient_SetupSSLContext(IntPtr pClient, SSLVerifyMode verifyMode, string lpszPemCertFile, string lpszPemKeyFile, string lpszKeyPassword, string lpszCAPemCertFileOrPath);


        /// <summary>
        /// 始化通信组件 SSL 环境参数（通过内存加载证书）
        /// </summary>
        /// <param name="pClient"></param>
        /// <param name="verifyMode">SSL 验证模式（参考 EnSSLVerifyMode）</param>
        /// <param name="lpszPemCert">证书内容</param>
        /// <param name="lpszPemKey">私钥内容</param>
        /// <param name="lpszKeyPassword">私钥密码（没有密码则为空）</param>
        /// <param name="lpszCAPemCert">CA 证书内容（单向验证或客户端可选）</param>
        /// <returns>TRUE	-- 成功， FALSE	-- 失败，可通过 SYS_GetLastError() 获取失败原因</returns>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern bool HP_SSLClient_SetupSSLContextByMemory(IntPtr pClient, SSLVerifyMode verifyMode /* SSL_VM_NONE */, string lpszPemCert /* nullptr */, string lpszPemKey /* nullptr */, string lpszKeyPassword /* nullptr */, string lpszCAPemCert /* nullptr */);


        /// <summary>
        /// 名称：初始化通信组件 SSL 环境参数
        /// 描述：SSL 环境参数必须在 SSL 通信组件启动前完成初始化，否则启动失败
        /// </summary>
        /// <param name="pServer"></param>
        /// <param name="verifyMode">SSL 验证模式（参考 EnSSLVerifyMode）</param>
        /// <param name="lpszPemCertFile">证书文件（客户端可选）</param>
        /// <param name="lpszPemKeyFile">私钥文件（客户端可选）</param>
        /// <param name="lpszKeyPassword">私钥密码（没有密码则为空）</param>
        /// <param name="lpszCAPemCertFileOrPath"> CA 证书文件或目录（单向验证或客户端可选）</param>
        /// <param name="fnServerNameCallback">SNI 回调函数指针（可选）</param>
        /// <returns>TRUE.成功 FALSE.失败，可通过 SYS_GetLastError() 获取失败原因</returns>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern bool HP_SSLServer_SetupSSLContext(IntPtr pServer, SSLVerifyMode verifyMode, string lpszPemCertFile, string lpszPemKeyFile, string lpszKeyPassword, string lpszCAPemCertFileOrPath, SNIServerNameCallback fnServerNameCallback);

        /// <summary>
        /// 名称：初始化通信组件 SSL 环境参数
        /// 描述：SSL 环境参数必须在 SSL 通信组件启动前完成初始化，否则启动失败
        /// </summary>
        /// <param name="pServer"></param>
        /// <param name="verifyMode">SSL 验证模式（参考 EnSSLVerifyMode）</param>
        /// <param name="lpszPemCertFile">证书文件（客户端可选）</param>
        /// <param name="lpszPemKeyFile">私钥文件（客户端可选）</param>
        /// <param name="lpszKeyPassword">私钥密码（没有密码则为空）</param>
        /// <param name="lpszCAPemCertFileOrPath"> CA 证书文件或目录（单向验证或客户端可选）</param>
        /// <returns>TRUE.成功 FALSE.失败，可通过 SYS_GetLastError() 获取失败原因</returns>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int HP_SSLServer_AddSSLContext(IntPtr pServer, SSLVerifyMode verifyMode, string lpszPemCertFile, string lpszPemKeyFile, string lpszKeyPassword, string lpszCAPemCertFileOrPath);

        /// <summary>
        /// 名称：清理通信组件 SSL 运行环境
        /// 描述：清理通信组件 SSL 运行环境，回收 SSL 相关内存
        /// 1、通信组件析构时会自动调用本方法
        /// 2、当要重新设置通信组件 SSL 环境参数时，需要先调用本方法清理原先的环境参数
        /// </summary>
        /// <param name="pAgent"></param>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void HP_SSLAgent_CleanupSSLContext(IntPtr pAgent);

        /// <summary>
        /// 名称：清理通信组件 SSL 运行环境
        /// 描述：清理通信组件 SSL 运行环境，回收 SSL 相关内存
        /// 1、通信组件析构时会自动调用本方法
        /// 2、当要重新设置通信组件 SSL 环境参数时，需要先调用本方法清理原先的环境参数
        /// </summary>
        /// <param name="pClient"></param>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void HP_SSLClient_CleanupSSLContext(IntPtr pClient);

        /// <summary>
        /// 名称：清理通信组件 SSL 运行环境
        /// 描述：清理通信组件 SSL 运行环境，回收 SSL 相关内存
        /// 1、通信组件析构时会自动调用本方法
        /// 2、当要重新设置通信组件 SSL 环境参数时，需要先调用本方法清理原先的环境参数
        /// </summary>
        /// <param name="pServer"></param>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void HP_SSLServer_CleanupSSLContext(IntPtr pServer);


        /// <summary>
        /// 初始化通信组件 SSL 环境参数（通过内存加载证书）
        /// 描述：SSL 环境参数必须在 SSL 通信组件启动前完成初始化，否则启动失败
        /// </summary>
        /// <param name="pServer"></param>
        /// <param name="verifyMode">SSL 验证模式（参考 EnSSLVerifyMode）</param>
        /// <param name="lpszPemCert">证书内容</param>
        /// <param name="lpszPemKey">私钥内容</param>
        /// <param name="lpszKeyPassword">私钥密码（没有密码则为空）</param>
        /// <param name="lpszCAPemCert">CA 证书内容（单向验证或客户端可选）</param>
        /// <param name="fnServerNameCallback">SNI 回调函数指针（可选，如果为 null 则使用 SNI 默认回调函数）</param>
        /// <returns></returns>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern bool HP_SSLServer_SetupSSLContextByMemory(IntPtr pServer, SSLVerifyMode verifyMode /* SSL_VM_NONE */, string lpszPemCert /* nullptr */, string lpszPemKey /* nullptr */, string lpszKeyPassword /* nullptr */, string lpszCAPemCert /* nullptr */, SNIServerNameCallback fnServerNameCallback /* nullptr */);


        /// <summary>
        /// 增加 SNI 主机证书（通过内存加载证书）
        /// 描述：SSL 服务端在 SetupSSLContext() 成功后可以调用本方法增加多个 SNI 主机证书
        /// </summary>
        /// <param name="pServer"></param>
        /// <param name="verifyMode">SSL 验证模式（参考 EnSSLVerifyMode）</param>
        /// <param name="lpszPemCert">证书内容</param>
        /// <param name="lpszPemKey">私钥内容</param>
        /// <param name="lpszKeyPassword">私钥密码（没有密码则为空）</param>
        /// <param name="lpszCAPemCert">CA 证书内容（单向验证可选）</param>
        /// <returns>正数		-- 成功，并返回 SNI 主机证书对应的索引，该索引用于在 SNI 回调函数中定位 SNI 主机,负数		-- 失败，可通过 SYS_GetLastError() 获取失败原因 </returns>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int HP_SSLServer_AddSSLContextByMemory(IntPtr pServer, SSLVerifyMode verifyMode, string lpszPemCert, string lpszPemKey, string lpszKeyPassword /* nullptr */, string lpszCAPemCert /* nullptr */);

        /// <summary>
        /// 绑定 SNI 主机域名
        /// 描述：SSL 服务端在 AddSSLContext() 成功后可以调用本方法绑定主机域名到 SNI 主机证书
        /// </summary>
        /// <param name="pServer"></param>
        /// <param name="lpszServerName">主机域名</param>
        /// <param name="iContextIndex">SNI 主机证书对应的索引</param>
        /// <returns>TRUE	-- 成功，FALSE	-- 失败，可通过 SYS_GetLastError() 获取失败原因</returns>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern bool HP_SSLServer_BindSSLServerName(IntPtr pServer, string lpszServerName, int iContextIndex);


        /// <summary>
        /// SNI 默认回调函数
        /// 描述：SSL Server 的 SetupSSLContext 方法中如果不指定 SNI 回调函数则使用此 SNI 默认回调函数
        /// </summary>
        /// <param name="lpszServerName">请求域名</param>
        /// <param name="pContext">SSL Context 对象</param>
        /// <returns>SNI 主机证书对应的索引</returns>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int HP_SSL_DefaultServerNameCallback(string lpszServerName, IntPtr pContext);


        /// <summary>
        /// 清理线程局部环境 SSL 资源
        /// 描述：清理 SSL 全局运行环境，回收 SSL 相关内存
        /// 任何一个操作 SSL 的线程，通信结束时都需要清理线程局部环境 SSL 资源
        /// 1、主线程和 HP-Socket 工作线程在通信结束时会自动清理线程局部环境 SSL 资源。因此，一般情况下不必手工调用本方法
        /// 2、特殊情况下，当自定义线程参与 HP-Socket 通信操作并检查到 SSL 内存泄漏时，需在每次通信结束时自定义线程调用本方法
        /// </summary>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void HP_SSL_RemoveThreadLocalState();



        /// <summary>
        /// 启动 SSL 握手
        /// 当通信组件设置为非自动握手时，需要调用本方法启动 SSL 握手
        /// </summary>
        /// <param name="pServer"></param>
        /// <param name="dwConnID"></param>
        /// <returns>TRUE	-- 成功，FALSE	-- 失败，可通过 SYS_GetLastError() 获取失败原因</returns>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern bool HP_SSLServer_StartSSLHandShake(IntPtr pServer, IntPtr dwConnID);


        /// <summary>
        /// 设置通信组件握手方式（默认：TRUE，自动握手)
        /// </summary>
        /// <param name="pServer"></param>
        /// <param name="bAutoHandShake"></param>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void HP_SSLServer_SetSSLAutoHandShake(IntPtr pServer, bool bAutoHandShake);

        /// <summary>
        /// 获取通信组件握手方式
        /// </summary>
        /// <param name="pServer"></param>
        /// <returns></returns>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern bool HP_SSLServer_IsSSLAutoHandShake(IntPtr pServer);

        /// <summary>
        /// 获取 SSL Session 信息
        /// 描述：获取指定类型的 SSL Session 信息（输出类型参考：En_HP_SSLSessionInfo）
        /// </summary>
        /// <param name="pServer"></param>
        /// <param name="dwConnID"></param>
        /// <param name="enInfo"></param>
        /// <param name="lppInfo"></param>
        /// <returns>TRUE	-- 成功， FALSE	-- 失败，可通过 SYS_GetLastError() 获取失败原因</returns>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern bool HP_SSLServer_GetSSLSessionInfo(IntPtr pServer, IntPtr dwConnID, SSLSessionInfo enInfo, ref IntPtr lppInfo);

        /// <summary>
        /// 启动 SSL 握手
        /// 当通信组件设置为非自动握手时，需要调用本方法启动 SSL 握手
        /// </summary>
        /// <param name="pAgent"></param>
        /// <param name="dwConnID"></param>
        /// <returns>TRUE	-- 成功，FALSE	-- 失败，可通过 SYS_GetLastError() 获取失败原因</returns>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern bool HP_SSLAgent_StartSSLHandShake(IntPtr pAgent, IntPtr dwConnID);

        /// <summary>
        /// 设置通信组件握手方式（默认：TRUE，自动握手）
        /// </summary>
        /// <param name="pAgent"></param>
        /// <param name="bAutoHandShake"></param>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void HP_SSLAgent_SetSSLAutoHandShake(IntPtr pAgent, bool bAutoHandShake);

        /// <summary>
        /// 获取通信组件握手方式
        /// </summary>
        /// <param name="pAgent"></param>
        /// <returns></returns>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern bool HP_SSLAgent_IsSSLAutoHandShake(IntPtr pAgent);


        /// <summary>
        /// 获取 SSL Session 信息
        /// 描述：获取指定类型的 SSL Session 信息（输出类型参考：En_HP_SSLSessionInfo）
        /// </summary>
        /// <param name="pAgent"></param>
        /// <param name="dwConnID"></param>
        /// <param name="enInfo"></param>
        /// <param name="lppInfo"></param>
        /// <returns>TRUE	-- 成功，FALSE	-- 失败，可通过 SYS_GetLastError() 获取失败原因</returns>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern bool HP_SSLAgent_GetSSLSessionInfo(IntPtr pAgent, IntPtr dwConnID, SSLSessionInfo enInfo, ref IntPtr lppInfo);


        /// <summary>
        /// 启动 SSL 握手
        /// 当通信组件设置为非自动握手时，需要调用本方法启动 SSL 握手
        /// </summary>
        /// <param name="pClient"></param>
        /// <returns>TRUE	-- 成功，FALSE	-- 失败，可通过 SYS_GetLastError() 获取失败原因</returns>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern bool HP_SSLClient_StartSSLHandShake(IntPtr pClient);

        /// <summary>
        /// 设置通信组件握手方式（默认：TRUE，自动握手）
        /// </summary>
        /// <param name="pClient"></param>
        /// <param name="bAutoHandShake"></param>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void HP_SSLClient_SetSSLAutoHandShake(IntPtr pClient, bool bAutoHandShake);

        /// <summary>
        /// 获取通信组件握手方式
        /// </summary>
        /// <param name="pClient"></param>
        /// <returns></returns>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern bool HP_SSLClient_IsSSLAutoHandShake(IntPtr pClient);


        /// <summary>
        /// 获取 SSL Session 信息
        /// 描述：获取指定类型的 SSL Session 信息（输出类型参考：En_HP_SSLSessionInfo）
        /// </summary>
        /// <param name="pClient"></param>
        /// <param name="enInfo"></param>
        /// <param name="lppInfo"></param>
        /// <returns>TRUE	-- 成功，FALSE	-- 失败，可通过 SYS_GetLastError() 获取失败原因</returns>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern bool HP_SSLClient_GetSSLSessionInfo(IntPtr pClient, SSLSessionInfo enInfo, ref IntPtr lppInfo);

    }
}
