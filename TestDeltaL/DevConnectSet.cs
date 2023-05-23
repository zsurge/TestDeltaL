using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestDeltaL
{
    public partial class DevConnectSet : Form
    {
        public DevConnectSet()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;//设置form1的开始位置为屏幕的中央
        }

        public delegate void ChangeTsbHandler(string addr);  //定义委托
        public event ChangeTsbHandler ChangeValue;  //定义事件

        private void DevConnectSet_Load(object sender, EventArgs e)
        {
            combDevString.BackColor = SystemColors.Window;
            combDevString.Text = INI.GetValueFromIniFile("Instrument", "AddressNA");
            CGloabal.g_InstrE5080BModule.adress = INI.GetValueFromIniFile("Instrument", "AddressNA");

            if (INI.GetValueFromIniFile("Instrument", "NA").Length > 0)
            {
                combDevType.Text = INI.GetValueFromIniFile("Instrument", "NA");
            }
        }


        private void btn_ConnectDev_Click(object sender, EventArgs e)
        {
            string sn = string.Empty;


            string NowDate = DateTime.Now.ToString("yyyyMMdd");


            if (combDevType.Text.Contains("E5080B") || combDevType.Text.Contains("PNA"))
            {
                CGloabal.g_InstrE5080BModule.adress = combDevString.Text;

                INI.WriteValueToIniFile("Instrument", "AddressNA", combDevString.Text);
                INI.WriteValueToIniFile("Instrument", "NA", combDevType.Text);

                CGloabal.g_curInstrument = CGloabal.g_InstrE5080BModule;

                int ret = E5080B.Open(CGloabal.g_curInstrument.adress, ref CGloabal.g_curInstrument.nHandle);

                E5080B.GetInstrumentIdentifier(CGloabal.g_curInstrument.nHandle, out sn);

                if (sn.Contains("MY59101265") || sn.Contains("MY59101017") || sn.Contains("MY60213234"))
                {

                    if (202202101400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
                    {
                        optStatus.isConnect = false;
                        combDevString.BackColor = Color.Red;
                        return;
                    }


                    if (ret != 0)
                    {
                        optStatus.isConnect = false;
                        combDevString.BackColor = Color.Red;
                        MessageBox.Show("error!");
                    }
                    else
                    {
                        optStatus.isConnect = true;
                        combDevString.BackColor = Color.Green;
                    }
                }
                else
                {
                    optStatus.isConnect = false;
                    combDevString.BackColor = Color.Red;
                }
            }//end 5080B
            else if (combDevType.Text.Contains("E5063A"))
            {
                CGloabal.g_InstrE5063AModule.adress = combDevString.Text;

                INI.WriteValueToIniFile("Instrument", "AddressNA", combDevString.Text);
                INI.WriteValueToIniFile("Instrument", "NA", combDevType.Text);

                CGloabal.g_curInstrument = CGloabal.g_InstrE5063AModule;

                int ret = E5063A.Open(CGloabal.g_curInstrument.adress, ref CGloabal.g_curInstrument.nHandle);

                LoggerHelper.mlog.Debug("E5063A.Open ret = " + ret.ToString());

                LoggerHelper.mlog.Debug("nInstrumentHandle = " + CGloabal.g_curInstrument.nHandle.ToString());

                E5063A.GetInstrumentIdentifier(CGloabal.g_curInstrument.nHandle, out sn);

                E5063A.GetThreePortIdentifier(CGloabal.g_curInstrument.nHandle);

                E5063A.ClearAllErrorQueue(CGloabal.g_curInstrument.nHandle);

                //深圳超能 改为20210130
                if (sn.Contains("MY54504547"))
                {
                    //已付款
                    if (209903011400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
                    {
                        optStatus.isConnect = false;
                        combDevString.BackColor = Color.Red;
                        return;
                    }

                    if (ret != 0)
                    {
                        optStatus.isConnect = false;
                        combDevString.BackColor = Color.Red;
                        MessageBox.Show("error!");
                    }
                    else
                    {
                        optStatus.isConnect = true;
                        combDevString.BackColor = Color.Green;
                    }
                }
                else
                {
                    optStatus.isConnect = false;
                    combDevString.BackColor = Color.Red;
                }
            }//end E5063A
            else if (combDevType.Text.Contains("E5071C"))
            {
                CGloabal.g_InstrE5071CModule.adress = combDevString.Text;

                INI.WriteValueToIniFile("Instrument", "AddressNA", combDevString.Text);
                INI.WriteValueToIniFile("Instrument", "NA", combDevType.Text);

                CGloabal.g_curInstrument = CGloabal.g_InstrE5071CModule;

                int ret = E5071C.Open(CGloabal.g_curInstrument.adress, ref CGloabal.g_curInstrument.nHandle);
                E5071C.GetInstrumentIdentifier(CGloabal.g_curInstrument.nHandle, out sn);
                E5071C.ClearAllErrorQueue(CGloabal.g_curInstrument.nHandle);
                E5071C.setAttribute(CGloabal.g_curInstrument.nHandle);

                if (sn.Contains("MY46734604"))
                {

                    if (202202011400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
                    {
                        optStatus.isConnect = false;
                        combDevString.BackColor = Color.Red;
                        return;
                    }


                    if (ret != 0)
                    {
                        optStatus.isConnect = false;
                        combDevString.BackColor = Color.Red;
                        MessageBox.Show("error!");
                    }
                    else
                    {
                        optStatus.isConnect = true;
                        combDevString.BackColor = Color.Green;
                    }
                }

                else
                {
                    optStatus.isConnect = false;
                    combDevString.BackColor = Color.Red;
                }
            }//end E5071C
            else
            {
                optStatus.isConnect = false;
                combDevString.BackColor = Color.Red;
                MessageBox.Show("暂不支持，请联系支持人员!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





    }///DevConnectSet
}
