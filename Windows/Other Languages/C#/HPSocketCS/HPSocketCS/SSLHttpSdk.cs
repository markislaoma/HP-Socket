using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace HPSocketCS
{
    public class SSLHttpSdk
    {
        /// <summary>
        /// 创建 HP_HttpsServer 对象
        /// </summary>
        /// <param name="pListener"></param>
        /// <returns></returns>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern IntPtr Create_HP_HttpsServer(IntPtr pListener);
        /// <summary>
        /// 创建 HP_HttpsAgent 对象
        /// </summary>
        /// <param name="pListener"></param>
        /// <returns></returns>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern IntPtr Create_HP_HttpsAgent(IntPtr pListener);
        /// <summary>
        /// 创建 HP_HttpsClient 对象
        /// </summary>
        /// <param name="pListener"></param>
        /// <returns></returns>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern IntPtr Create_HP_HttpsClient(IntPtr pListener);

        /// <summary>
        /// 创建 HP_HttpsSyncClient 对象
        /// </summary>
        /// <param name="pListener"></param>
        /// <returns></returns>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern IntPtr Create_HP_HttpsSyncClient(IntPtr pListener);

        /// <summary>
        /// 销毁 HP_HttpsServer 对象
        /// </summary>
        /// <param name="pServer"></param>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void Destroy_HP_HttpsServer(IntPtr pServer);
        /// <summary>
        /// 销毁 HP_HttpsAgent 对象
        /// </summary>
        /// <param name="pAgent"></param>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void Destroy_HP_HttpsAgent(IntPtr pAgent);
        /// <summary>
        /// 销毁 HP_HttpsClient 对象
        /// </summary>
        /// <param name="pClient"></param>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void Destroy_HP_HttpsClient(IntPtr pClient);
        /// <summary>
        /// 销毁 HP_HttpsSyncClient 对象
        /// </summary>
        /// <param name="pClient"></param>
        [DllImport(Sdk.HPSOCKET_DLL_PATH, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void Destroy_HP_HttpsSyncClient(IntPtr pClient);

    }
}
