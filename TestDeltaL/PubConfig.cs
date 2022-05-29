using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDeltaL
{
    class PubConfig
    {
        public enum DeviceType
        {
            TDR = 0,
            DELTAL = 1,
        }

        //private DeviceType devType = DeviceType.TDR;

        ////设置设备类弄
        //public DeviceType DeyType
        //{
        //    get { return devType; }
        //    set { devType = value; }
        //}

        public static DeviceType gDeviceType = DeviceType.TDR;
    }
}
