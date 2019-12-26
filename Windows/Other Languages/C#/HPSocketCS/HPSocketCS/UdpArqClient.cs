using System;
using System.Collections.Generic;
using System.Text;

namespace HPSocketCS
{
    public class UdpArqClient : UdpClient
    {

        /// <summary>
        /// 创建socket监听&服务组件
        /// </summary>
        /// <returns></returns>
        protected override bool CreateListener()
        {
            if (IsCreate == true || pListener != IntPtr.Zero || pClient != IntPtr.Zero)
            {
                return false;
            }

            pListener = Sdk.Create_HP_UdpArqClientListener();
            if (pListener == IntPtr.Zero)
            {
                return false;
            }

            pClient = Sdk.Create_HP_UdpArqClient(pListener);
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
        public override void Destroy()
        {
            Stop();

            if (pClient != IntPtr.Zero)
            {
                Sdk.Destroy_HP_UdpArqClient(pClient);
                pClient = IntPtr.Zero;
            }
            if (pListener != IntPtr.Zero)
            {
                Sdk.Destroy_HP_UdpArqClientListener(pListener);
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
                return Sdk.HP_UdpArqClient_IsNoDelay(pClient);
            }
            set
            {
                Sdk.HP_UdpArqClient_SetNoDelay(pClient, value);
            }
        }

        /// <summary>
        /// 获取或设置是否关闭拥塞控制
        /// </summary>
        public bool TurnoffCongestCtrl
        {
            get
            {
                return Sdk.HP_UdpArqClient_IsTurnoffCongestCtrl(pClient);
            }
            set
            {
                Sdk.HP_UdpArqClient_SetTurnoffCongestCtrl(pClient, value);
            }
        }

        /// <summary>
        /// 获取或设置数据刷新间隔
        /// </summary>
        public uint FlushInterval
        {
            get
            {
                return Sdk.HP_UdpArqClient_GetFlushInterval(pClient);
            }
            set
            {
                Sdk.HP_UdpArqClient_SetFlushInterval(pClient, value);
            }
        }

        /// <summary>
        /// 获取或设置快速重传 ACK 跨越次数
        /// </summary>
        public uint ResendByAcks
        {
            get
            {
                return Sdk.HP_UdpArqClient_GetResendByAcks(pClient);
            }
            set
            {
                Sdk.HP_UdpArqClient_SetResendByAcks(pClient, value);
            }
        }

        /// <summary>
        /// 获取或设置发送窗口大小
        /// </summary>
        public uint SendWndSize
        {
            get
            {
                return Sdk.HP_UdpArqClient_GetSendWndSize(pClient);
            }
            set
            {
                Sdk.HP_UdpArqClient_SetSendWndSize(pClient, value);
            }
        }

        /// <summary>
        /// 获取或设置接收窗口大小
        /// </summary>
        public uint RecvWndSize
        {
            get
            {
                return Sdk.HP_UdpArqClient_GetRecvWndSize(pClient);
            }
            set
            {
                Sdk.HP_UdpArqClient_SetRecvWndSize(pClient, value);
            }
        }

        /// <summary>
        /// 获取或设置最小重传超时时间
        /// </summary>
        public uint MinRto
        {
            get
            {
                return Sdk.HP_UdpArqClient_GetMinRto(pClient);
            }
            set
            {
                Sdk.HP_UdpArqClient_SetMinRto(pClient, value);
            }
        }

        /// <summary>
        /// 获取或设置最大传输单元
        /// </summary>
        public uint MaxTransUnit
        {
            get
            {
                return Sdk.HP_UdpArqClient_GetMaxTransUnit(pClient);
            }
            set
            {
                Sdk.HP_UdpArqClient_SetMaxTransUnit(pClient, value);
            }
        }

        /// <summary>
        /// 获取或设置最大数据包大小
        /// </summary>
        public uint MaxMessageSize
        {
            get
            {
                return Sdk.HP_UdpArqClient_GetMaxMessageSize(pClient);
            }
            set
            {
                Sdk.HP_UdpArqClient_SetMaxMessageSize(pClient, value);
            }
        }

        /// <summary>
        /// 获取或设置握手超时时间
        /// </summary>
        public uint HandShakeTimeout
        {
            get
            {
                return Sdk.HP_UdpArqClient_GetHandShakeTimeout(pClient);
            }
            set
            {
                Sdk.HP_UdpArqClient_SetHandShakeTimeout(pClient, value);
            }
        }


        /// <summary>
        /// 获取等待发送包数量
        /// </summary>
        /// <returns></returns>
        public int GetWaitingSendMessageCount()
        {
            var count = 0;
            if (Sdk.HP_UdpArqClient_GetWaitingSendMessageCount(pClient, ref count))
            {
                return count;
            }

            return 0;
        }
    }
}
