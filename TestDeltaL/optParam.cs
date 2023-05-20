using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDeltaL
{
    public static class optParam
    {
        //键盘默认模式，0是关闭，1是启用
        public static int keyMode { get; set; } = 1;

        //即时确认开启&关闭 0是关闭，1是启用
        public static int real_time_enable { get; set; } = 1;
         
        //测量模式选择 0 = pnd-tdr  1 = s-param
        public static int meas_type { get; set; } = 1;

        //端口选择 13 = 使用1&3端口  24 = 使用 2&4端口
        public static int port_select { get; set; } = 13;

        //流水号 手动&自动  0 = 手动，1 = 自动
        public static int snRecordMode { get; set; } = 1;

        //流水号前缀
        public static string snPrefix { get; set; } = "SN";

        //起始流水号
        public static string snBegin { get; set; } = "0001";

        //测试模式 1 通过；2 手动 3 直接下一笔 4 仅记录通过
        public static int testMode { get; set; } = 3;

        //报告的存储方式 1按日期；2.按量测参数
        public static int exportMode { get; set; } = 1;

        //补偿值
        public static int offsetValue { get; set; } = 3;
        //补偿值模式，0是手动，1是自动
        public static int Compensation_mode { get; set; } = 1;

        public static List<float> freq_limit { get; set; } = new List<float>() {-10.0f,-26.0f,-30.0f,-13.0f,-15.0f,-35.0f,-17.0f,-40.0f,-40.0f};

        //历史报告的默认文件名
        public static string historyExportFileName { get; set; } = "Deltal_Project_History";

        //输出报告的默认文件名
        public static string outputExportFileName { get; set; } = "Deltal_Project_Export.csv";
    }
}
