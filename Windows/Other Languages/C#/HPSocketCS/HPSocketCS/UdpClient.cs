using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace HPSocketCS
{
    public class UdpClient : ConnectionExtra, IClient
    {
        /// <summary>
        /// 准备连接了事件
        /// </summary>
        public event ClientEvent.OnPrepareConnectEventHandler OnPrepareConnect;
        /// <summary>
        /// 连接事件
        /// </summary>
        public event ClientEvent.OnConnectEventHandler OnConnect;
        /// <summary>
        /// 数据发送事件
        /// </summary>
        public event ClientEvent.OnSendEventHandler OnSend;
        /// <summary>
        /// 数据到达事件
        /// </summary>
        public event ClientEvent.OnReceiveEventHandler OnReceive;
        /// <summary>
        /// 数据到达事件(指针数据)
        /// </summary>
        public event ClientEvent.OnPointerDataReceiveEventHandler OnPointerDataReceive;
        /// <summary>
        /// 连接关闭事件
        /// </summary>
        public event ClientEvent.OnCloseEventHandler OnClose;
        /// <summary>
        /// 握手事件
        /// </summary>
        public event ClientEvent.OnHandShakeEventHandler OnHandShake;


        protected IntPtr pClient = IntPtr.Zero;

        protected IntPtr pListener = IntPtr.Zero;

        protected bool IsCreate = false;
        ConnectionExtra ExtraData = new ConnectionExtra();

        public UdpClient()
        {
            CreateListener();
        }

        ~UdpClient()
        {
            Destroy();
        }

        /// <summary>
        /// 创建socket监听&服务组件
        /// </summary>
        /// <returns></returns>
        protected virtual bool CreateListener()
        {
            if (IsCreate == true || pListener != IntPtr.Zero || pClient != IntPtr.Zero)
            {
                return false;
            }


            pListener = Sdk.Create_HP_UdpClientListener();
            if (pListener == IntPtr.Zero)
            {
                return false;
            }

            pClient = Sdk.Create_HP_UdpClient(pListener);
            if (pClient == IntPtr.Zero)
            {
                return false;
            }

            IsCreate = true;

            return true;
        }

        /// <summary>
        /// 终止服务并释放资源
        /// </summary>
        public virtual void Destroy()
        {
            Stop();

            if (pClient != IntPtr.Zero)
            {
                Sdk.Destroy_HP_UdpClient(pClient);
                pClient = IntPtr.Zero;
            }
            if (pListener != IntPtr.Zero)
            {
                Sdk.Destroy_HP_UdpClientListener(pListener);
                pListener = IntPtr.Zero;
            }

            IsCreate = false;
        }

        /// <summary>
        /// 启动通讯组件并连接到服务器
        /// </summary>
        /// <param name="address">远程地址</param>
        /// <param name="port">远程端口</param>
        /// <param name="async">是否异步</param>
        /// <returns></returns>
        public bool Connect(string address, ushort port, bool async = true)
        {
            if (string.IsNullOrEmpty(address) == true)
            {
                throw new Exception("address is null");
            }
            else if (port == 0)
            {
                throw new Exception("port is zero");
            }

            if (pClient == IntPtr.Zero)
            {
                return false;
            }

            this.SetCallback();

            return Sdk.HP_Client_Start(pClient, address, port, async);
        }

        /// <summary>
        /// 启动通讯组件并连接到服务器
        /// </summary>
        /// <param name="address">远程地址</param>
        /// <param name="port">远程端口</param>
        /// <param name="bindAddress">本地绑定到哪个ip?,多ip下可以选择绑定到指定ip</param>
        /// <param name="async">是否异步</param>
        /// <returns></returns>
        public bool Connect(string address, ushort port, string bindAddress, bool async = true)
        {
            if (string.IsNullOrEmpty(address) == true)
            {
                throw new Exception("address is null");
            }
            else if (port == 0)
            {
                throw new Exception("port is zero");
            }

            if (pClient == IntPtr.Zero)
            {
                return false;
            }

            this.SetCallback();

            return Sdk.HP_Client_StartWithBindAddress(pClient, address, port, async, bindAddress);
        }

        /// <summary>
        /// 启动通讯组件并连接到服务器
        /// </summary>
        /// <param name="address">远程地址</param>
        /// <param name="port">远程端口</param>
        /// <param name="bindAddress">本地绑定到哪个ip?,多ip下可以选择绑定到指定ip</param>
        /// <param name="usLocalPort">本地端口</param>
        /// <param name="async">是否异步</param>
        /// <returns></returns>
        public bool Connect(string address, ushort port, string bindAddress, ushort usLocalPort, bool async = true)
        {
            if (string.IsNullOrEmpty(address) == true)
            {
                throw new Exception("address is null");
            }
            else if (port == 0)
            {
                throw new Exception("port is zero");
            }

            if (pClient == IntPtr.Zero)
            {
                return false;
            }

            this.SetCallback();

            return Sdk.HP_Client_StartWithBindAddressAndLocalPort(pClient, address, port, async, bindAddress, usLocalPort);
        }


        /// <summary>
        /// 停止通讯组件
        /// </summary>
        /// <returns></returns>
        public bool Stop()
        {
            if (pClient == IntPtr.Zero)
            {
                return false;
            }
            return Sdk.HP_Client_Stop(pClient);
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public bool Send(byte[] bytes, int size)
        {
            return Sdk.HP_Client_Send(pClient, bytes, size);
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="bufferPtr"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public bool Send(IntPtr bufferPtr, int size)
        {
            return Sdk.HP_Client_Send(pClient, bufferPtr, size);
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset">针对bytes的偏移</param>
        /// <param name="size">发多大</param>
        /// <returns></returns>
        public bool Send(byte[] bytes, int offset, int size)
        {
            return Sdk.HP_Client_SendPart(pClient, bytes, size, offset);
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="bufferPtr"></param>
        /// <param name="offset">针对bufferPtr的偏移</param>
        /// <param name="size">发多大</param>
        /// <returns></returns>
        public bool Send(IntPtr bufferPtr, int offset, int size)
        {
            return Sdk.HP_Client_SendPart(pClient, bufferPtr, size, offset);
        }

        /// <summary>
        /// 发送多组数据
        /// 向指定连接发送多组数据
        /// TCP - 顺序发送所有数据包
        /// </summary>
        /// <param name="buffers">发送缓冲区数组</param>
        /// <param name="count">发送缓冲区数目</param>
        /// <returns>TRUE.成功,FALSE.失败，可通过 SYSGetLastError() 获取 Windows 错误代码</returns>
        public bool SendPackets(WSABUF[] buffers, int count)
        {
            return Sdk.HP_Client_SendPackets(pClient, buffers, count);
        }


        /// <summary>
        /// 发送多组数据
        /// 向指定连接发送多组数据
        /// TCP - 顺序发送所有数据包
        /// </summary>
        /// <param name="objects">发送缓冲区数组</param>
        /// <returns>TRUE.成功,FALSE.失败，可通过 SYSGetLastError() 获取 Windows 错误代码</returns>
        public bool SendPackets<T>(T[] objects)
        {
            bool ret = false;

            WSABUF[] buffer = new WSABUF[objects.Length];
            IntPtr[] ptrs = new IntPtr[buffer.Length];
            try
            {

                for (int i = 0; i < objects.Length; i++)
                {
                    buffer[i].Length = Marshal.SizeOf(typeof(T));

                    ptrs[i] = Marshal.AllocHGlobal(buffer[i].Length);
                    Marshal.StructureToPtr(objects[i], ptrs[i], true);

                    buffer[i].Buffer = ptrs[i];
                }
                ret = SendPackets(buffer, buffer.Length);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                foreach (var ptr in ptrs)
                {
                    if (ptr != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(ptr);
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// 获取错误码
        /// </summary>
        public SocketError ErrorCode
        {
            get
            {
                return Sdk.HP_Client_GetLastError(pClient);
            }
        }

        /// <summary>
        /// 版本号
        /// </summary>
        public string Version
        {
            get
            {
                return Sdk.GetHPSocketVersion();
            }
        }

        /// <summary>
        /// 获取错误信息
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                IntPtr ptr = Sdk.HP_Client_GetLastErrorDesc(pClient);
                string desc = Marshal.PtrToStringAnsi(ptr);
                return desc;
            }
        }


        /// <summary>
        /// 获取或设置接收状态
        /// </summary>
        public ReceiveState ReceiveState
        {
            get
            {
                int state = -1;

                if (Sdk.HP_Client_IsPauseReceive(pClient, ref state))
                {
                    return (ReceiveState)state;
                }
                return ReceiveState.Unknown;
            }
            set
            {
                Sdk.HP_Client_PauseReceive(pClient, (int)value);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public bool GetPendingDataLength(ref int length)
        {
            return Sdk.HP_Client_GetPendingDataLength(pClient, ref length);
        }

        /// <summary>
        /// 获取附加数据
        /// </summary>
        /// <returns></returns>
        public T GetExtra<T>()
        {
            return (T)ExtraData.GetExtra(pClient);
        }

        /// <summary>
        /// 设置附加数据
        /// 
        /// </summary>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public bool SetExtra(object newValue)
        {
            return ExtraData.SetExtra(pClient, newValue);
        }

        /// <summary>
        /// 删除附加数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool RemoveExtra()
        {
            return ExtraData.RemoveExtra(pClient);
        }

        /// <summary>
        /// 获取监听socket的地址信息
        /// </summary>
        /// <param name="ip"></param> 
        /// <param name="port"></param>
        /// <returns></returns>
        public bool GetListenAddress(ref string ip, ref ushort port)
        {
            int ipLength = 40;

            StringBuilder sb = new StringBuilder(ipLength);

            bool ret = Sdk.HP_Client_GetLocalAddress(pClient, sb, ref ipLength, ref port);
            if (ret == true)
            {
                ip = sb.ToString();
            }
            return ret;
        }

        /// <summary>
        /// 获取连接的远程主机信息
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public bool GetRemoteHost(ref string host, ref ushort port)
        {
            int ipLength = 40;

            StringBuilder sb = new StringBuilder(ipLength);

            bool ret = Sdk.HP_Client_GetRemoteHost(pClient, sb, ref ipLength, ref port);
            if (ret == true)
            {
                host = sb.ToString();
            }
            return ret;
        }

        /// <summary>
        /// 是否启动
        /// </summary>
        public bool IsStarted
        {
            get
            {
                if (pClient == IntPtr.Zero)
                {
                    return false;
                }
                return Sdk.HP_Client_HasStarted(pClient);
            }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public ServiceState State
        {
            get
            {
                return Sdk.HP_Client_GetState(pClient);
            }

        }

        /// <summary>
        /// 获取该组件对象的连接Id
        /// </summary>
        public IntPtr ConnectionId
        {
            get
            {
                return Sdk.HP_Client_GetConnectionID(pClient);
            }

        }

        /// <summary>
        /// 检测是否为安全连接（SSL/HTTPS）
        /// </summary>
        public bool IsSecure
        {
            get
            {
                return Sdk.HP_Client_IsSecure(pClient);
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 读取或设置内存块缓存池大小（通常设置为 -> PUSH 模型：5 - 10；PULL 模型：10 - 20 ）
        /// </summary>
        public uint FreeBufferPoolSize
        {
            get
            {
                return Sdk.HP_Client_GetFreeBufferPoolSize(pClient);
            }
            set
            {
                Sdk.HP_Client_SetFreeBufferPoolSize(pClient, value);
            }
        }

        /// <summary>
        ///  读取或设置内存块缓存池回收阀值（通常设置为内存块缓存池大小的 3 倍）
        /// </summary>
        public uint FreeBufferPoolHold
        {
            get
            {
                return Sdk.HP_Client_GetFreeBufferPoolHold(pClient);
            }
            set
            {
                Sdk.HP_Client_SetFreeBufferPoolHold(pClient, value);
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>
        /// 根据错误码返回错误信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetSocketErrorDesc(SocketError code)
        {
            IntPtr ptr = Sdk.HP_GetSocketErrorDesc(code);
            string desc = Marshal.PtrToStringAnsi(ptr);
            return desc;
        }

        ///////////////////////////////////////////////////////////////////////////////////////
        protected Sdk.OnPrepareConnect _OnPrepareConnect = null;
        protected Sdk.OnConnect _OnConnect = null;
        protected Sdk.OnReceive _OnReceive = null;
        protected Sdk.OnSend _OnSend = null;
        protected Sdk.OnClose _OnClose = null;
        protected Sdk.OnHandShake _OnHandShake = null;
        /// <summary>
        /// 设置回调函数
        /// </summary>
        protected virtual void SetCallback()
        {
            // 设置 Socket 监听器回调函数
            _OnPrepareConnect = new Sdk.OnPrepareConnect(SDK_OnPrepareConnect);
            _OnConnect = new Sdk.OnConnect(SDK_OnConnect);
            _OnSend = new Sdk.OnSend(SDK_OnSend);
            _OnReceive = new Sdk.OnReceive(SDK_OnReceive);
            _OnClose = new Sdk.OnClose(SDK_OnClose);
            _OnHandShake = new Sdk.OnHandShake(SDK_OnHandShake);

            Sdk.HP_Set_FN_Client_OnPrepareConnect(pListener, _OnPrepareConnect);
            Sdk.HP_Set_FN_Client_OnConnect(pListener, _OnConnect);
            Sdk.HP_Set_FN_Client_OnSend(pListener, _OnSend);
            Sdk.HP_Set_FN_Client_OnReceive(pListener, _OnReceive);
            Sdk.HP_Set_FN_Client_OnClose(pListener, _OnClose);
            Sdk.HP_Set_FN_Client_OnHandShake(pListener, _OnHandShake);
        }

        protected HandleResult SDK_OnPrepareConnect(IntPtr pSender, IntPtr connId, IntPtr socket)
        {
            if (OnPrepareConnect != null)
            {
                return OnPrepareConnect(this, socket);
            }
            return HandleResult.Ignore;
        }

        protected HandleResult SDK_OnConnect(IntPtr pSender, IntPtr connId)
        {
            if (OnConnect != null)
            {
                return OnConnect(this);
            }
            return HandleResult.Ignore;
        }

        protected HandleResult SDK_OnSend(IntPtr pSender, IntPtr connId, IntPtr pData, int length)
        {
            if (OnSend != null)
            {
                byte[] bytes = new byte[length];
                Marshal.Copy(pData, bytes, 0, length);
                return OnSend(this, bytes);
            }
            return HandleResult.Ignore;
        }

        protected HandleResult SDK_OnReceive(IntPtr pSender, IntPtr connId, IntPtr pData, int length)
        {
            if (OnPointerDataReceive != null)
            {
                return OnPointerDataReceive(this, pData, length);
            }
            else if (OnReceive != null)
            {
                byte[] bytes = new byte[length];
                Marshal.Copy(pData, bytes, 0, length);
                return OnReceive(this, bytes);
            }
            return HandleResult.Ignore;
        }

        protected HandleResult SDK_OnClose(IntPtr pSender, IntPtr connId, SocketOperation enOperation, int errorCode)
        {
            if (OnClose != null)
            {
                return OnClose(this, enOperation, errorCode);
            }
            return HandleResult.Ignore;
        }

        protected HandleResult SDK_OnHandShake(IntPtr pSender, IntPtr connId)
        {
            if (OnHandShake != null)
            {
                return OnHandShake(this);
            }
            return HandleResult.Ignore;
        }
        ///////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 获取系统返回的错误码
        /// </summary>
        public int SYSGetLastError()
        {
            return Sdk.SYS_GetLastError();
        }

        /// <summary>
        /// 调用系统的 ::WSAGetLastError() 方法获取通信错误代码
        /// </summary>
        public int SYSWSAGetLastError()
        {
            return Sdk.SYS_WSAGetLastError();
        }

        /// <summary>
        /// 调用系统的 setsockopt()
        /// </summary>
        /// <param name="sock"></param>
        /// <param name="level"></param>
        /// <param name="name"></param>
        /// <param name="val"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public int SYS_SetSocketOption(IntPtr sock, int level, int name, IntPtr val, int len)
        {
            return Sdk.SYS_SetSocketOption(sock, level, name, val, len);
        }

        /// <summary>
        /// 调用系统的 getsockopt()
        /// </summary>
        /// <param name="sock"></param>
        /// <param name="level"></param>
        /// <param name="name"></param>
        /// <param name="val"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public int SYSGetSocketOption(IntPtr sock, int level, int name, IntPtr val, ref int len)
        {
            return Sdk.SYS_GetSocketOption(sock, level, name, val, ref len);
        }
        /// <summary>
        /// 调用系统的 ioctlsocket()
        /// </summary>
        /// <param name="sock"></param>
        /// <param name="cmd"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public int SYSIoctlSocket(IntPtr sock, long cmd, IntPtr arg)
        {
            return Sdk.SYS_IoctlSocket(sock, cmd, arg);
        }

        /// <summary>
        /// 调用系统的 ::WSAIoctl()
        /// </summary>
        /// <param name="sock"></param>
        /// <param name="dwIoControlCode"></param>
        /// <param name="lpvInBuffer"></param>
        /// <param name="cbInBuffer"></param>
        /// <param name="lpvOutBuffer"></param>
        /// <param name="cbOutBuffer"></param>
        /// <param name="lpcbBytesReturned"></param>
        /// <returns></returns>
        public int SYS_WSAIoctl(IntPtr sock, uint dwIoControlCode, IntPtr lpvInBuffer, uint cbInBuffer,
                                              IntPtr lpvOutBuffer, uint cbOutBuffer, uint lpcbBytesReturned)
        {
            return Sdk.SYS_WSAIoctl(sock, dwIoControlCode, lpvInBuffer, cbInBuffer,
                                            lpvOutBuffer, cbOutBuffer, lpcbBytesReturned);
        }
    }
}
