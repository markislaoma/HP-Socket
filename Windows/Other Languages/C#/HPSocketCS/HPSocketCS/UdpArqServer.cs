using System;
using System.Collections.Generic;
using System.Text;

namespace HPSocketCS
{
    public class UdpArqServer<T> : UdpArqServer
    {
        public new T GetExtra(IntPtr connId)
        {
            return base.GetExtra<T>(connId);
        }

        public bool SetExtra(IntPtr connId, T obj)
        {
            return base.SetExtra(connId, obj);
        }
    }

    public class UdpArqServer : UdpServer
    {

        /// <summary>
        /// 创建socket监听&服务组件
        /// </summary>
        /// <returns></returns>
        protected override bool CreateListener()
        {
            if (IsCreate == true || pListener != IntPtr.Zero || pServer != IntPtr.Zero)
            {
                return false;
            }

            pListener = Sdk.Create_HP_UdpArqServerListener();
            if (pListener == IntPtr.Zero)
            {
                return false;
            }

            pServer = Sdk.Create_HP_UdpArqServer(pListener);
            if (pServer == IntPtr.Zero)
            {
                return false;
            }

            IsCreate = true;

            return true;
        }

        /// <summary>
        /// 终止服务并释放资源
        /// </summary>
        public override void Destroy()
        {
            Stop();

            if (pServer != IntPtr.Zero)
            {
                Sdk.Destroy_HP_UdpArqServer(pServer);
                pServer = IntPtr.Zero;
            }
            if (pListener != IntPtr.Zero)
            {
                Sdk.Destroy_HP_UdpArqServerListener(pListener);
                pListener = IntPtr.Zero;
            }

            IsCreate = false;
        }

        /// <summary>
        /// 获取或设置是否开启 NoDelay 模式 
        /// </summary>
        public bool NoDelay
        {
            get
            {
                return Sdk.HP_UdpArqServer_IsNoDelay(pServer);
            }
            set
            {
                Sdk.HP_UdpArqServer_SetNoDelay(pServer, value);
            }
        }

        /// <summary>
        /// 获取或设置是否关闭拥塞控制
        /// </summary>
        public bool TurnoffCongestCtrl
        {
            get
            {
                return Sdk.HP_UdpArqServer_IsTurnoffCongestCtrl(pServer);
            }
            set
            {
                Sdk.HP_UdpArqServer_SetTurnoffCongestCtrl(pServer, value);
            }
        }

        /// <summary>
        /// 获取或设置数据刷新间隔
        /// </summary>
        public uint FlushInterval
        {
            get
            {
                return Sdk.HP_UdpArqServer_GetFlushInterval(pServer);
            }
            set
            {
                Sdk.HP_UdpArqServer_SetFlushInterval(pServer, value);
            }
        }

        /// <summary>
        /// 获取或设置快速重传 ACK 跨越次数
        /// </summary>
        public uint ResendByAcks
        {
            get
            {
                return Sdk.HP_UdpArqServer_GetResendByAcks(pServer);
            }
            set
            {
                Sdk.HP_UdpArqServer_SetResendByAcks(pServer, value);
            }
        }

        /// <summary>
        /// 获取或设置发送窗口大小
        /// </summary>
        public uint SendWndSize
        {
            get
            {
                return Sdk.HP_UdpArqServer_GetSendWndSize(pServer);
            }
            set
            {
                Sdk.HP_UdpArqServer_SetSendWndSize(pServer, value);
            }
        }

        /// <summary>
        /// 获取或设置接收窗口大小
        /// </summary>
        public uint RecvWndSize
        {
            get
            {
                return Sdk.HP_UdpArqServer_GetRecvWndSize(pServer);
            }
            set
            {
                Sdk.HP_UdpArqServer_SetRecvWndSize(pServer, value);
            }
        }

        /// <summary>
        /// 获取或设置最小重传超时时间
        /// </summary>
        public uint MinRto
        {
            get
            {
                return Sdk.HP_UdpArqServer_GetMinRto(pServer);
            }
            set
            {
                Sdk.HP_UdpArqServer_SetMinRto(pServer, value);
            }
        }

        /// <summary>
        /// 获取或设置最大传输单元
        /// </summary>
        public uint MaxTransUnit
        {
            get
            {
                return Sdk.HP_UdpArqServer_GetMaxTransUnit(pServer);
            }
            set
            {
                Sdk.HP_UdpArqServer_SetMaxTransUnit(pServer, value);
            }
        }

        /// <summary>
        /// 获取或设置最大数据包大小
        /// </summary>
        public uint MaxMessageSize
        {
            get
            {
                return Sdk.HP_UdpArqServer_GetMaxMessageSize(pServer);
            }
            set
            {
                Sdk.HP_UdpArqServer_SetMaxMessageSize(pServer, value);
            }
        }

        /// <summary>
        /// 获取或设置握手超时时间
        /// </summary>
        public uint HandShakeTimeout
        {
            get
            {
                return Sdk.HP_UdpArqServer_GetHandShakeTimeout(pServer);
            }
            set
            {
                Sdk.HP_UdpArqServer_SetHandShakeTimeout(pServer, value);
            }
        }

        /// <summary>
        /// 获取等待发送包数量
        /// </summary>
        /// <param name="dwConnId"></param>
        /// <returns></returns>
        public int GetWaitingSendMessageCount(IntPtr dwConnId)
        {
            var count = 0;
            if (Sdk.HP_UdpArqServer_GetWaitingSendMessageCount(pServer, dwConnId, ref count))
            {
                return count;
            }

            return 0;
        }

    }
}
