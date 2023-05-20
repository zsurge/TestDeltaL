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
    public partial class DevOptSet : Form
    {
        public DevOptSet()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;//设置form1的开始位置为屏幕的中央
        }

        public delegate void ChangeSnHandler(string sn);  //定义委托
        public event ChangeSnHandler ChangeSn;  //定义事件

        public string historyFile = Environment.CurrentDirectory + "\\MeasureData\\History\\" + "DeltaL_" + DateTime.Now.ToString("yyyyMMdd") + "_History.csv";
        public string exportFile = Environment.CurrentDirectory + "\\MeasureData\\Report\\" + "DeltaL_" + DateTime.Now.ToString("yyyyMMdd") + "_Export.csv";

        public void OnSnChanged()
        {
            if (ChangeSn != null)
            {
                ChangeSn(optParam.snBegin); /* 事件被触发 */
            }
        }


        private void SetDefFreqLimitToGridView()
        {
            // 添加三行数据
            dgv_freq_limit.Rows.Add();
            dgv_freq_limit.Rows.Add();
            dgv_freq_limit.Rows.Add();

            dgv_freq_limit.Rows[0].Cells[0].Value = "Short";
            dgv_freq_limit.Rows[1].Cells[0].Value = "Medium";
            dgv_freq_limit.Rows[2].Cells[0].Value = "Long";

            //{-10.0f,-26.0f,-30.0f,-13.0f,-15.0f,-35.0f,-17.0f,-40.0f,-40.0f};
            int rowCount = dgv_freq_limit.Rows.Count;
            int columnCount = dgv_freq_limit.Columns.Count;

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 1; j < columnCount; j++)
                {
                    dgv_freq_limit.Rows[i].Cells[j].Value = optParam.freq_limit[(i * (columnCount - 1)) + j - 1];
                }
            }
        }

        private void BindFreqLimitToGridView(List<float> freq_limit, DataGridView dataGridView)
        {

        }

        private void DevOptSet_Load(object sender, EventArgs e)
        {
            ReadFromConfigFile();
            SetDefFreqLimitToGridView();
        }

        private void ReadFromDataGridView()
        {
            optParam.freq_limit.Clear();
            int rowCount = dgv_freq_limit.Rows.Count;
            int columnCount = dgv_freq_limit.Columns.Count;
            float freq_limit = 0.0f;

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 1; j < columnCount; j++)
                {
                    string cellValue = dgv_freq_limit.Rows[i].Cells[j].Value.ToString();
                    if (float.TryParse(cellValue, out freq_limit))
                    {
                        optParam.freq_limit.Add(freq_limit);
                    }
                    else
                    { 
                    }
                }
            }
            string freq_limit_str = string.Join(",", optParam.freq_limit.Select(x => x.ToString())); // 将列表中的每个元素转换为字符串，并用逗号连接
            INI.WriteValueToIniFile("DeltaL", "Freq_Limit", freq_limit_str);
        }

        private void ReadFromConfigFile()
        {
            string freq_limit_str = String.Empty;
            optParam.freq_limit.Clear();
            freq_limit_str = INI.GetValueFromIniFile("DeltaL", "Freq_Limit");
            string[] freq_limit_arr = freq_limit_str.Split(',');
            foreach (string str in freq_limit_arr)
            {
                optParam.freq_limit.Add(float.Parse(str));
            }
        }

        private void btn_opt_ok_Click(object sender, EventArgs e)
        {

            ReadFromDataGridView();
            //测试流程
            if (radio_pro_pass.Checked)
            {
                INI.WriteValueToIniFile("DeltaL", "TestStep", "Pass");
                optParam.testMode = 1;
            }
            else if (radio_pro_manual.Checked)
            {
                INI.WriteValueToIniFile("DeltaL", "TestStep", "Manual");
                optParam.testMode = 2;
            }
            else if (radio_pro_next.Checked)
            {
                INI.WriteValueToIniFile("DeltaL", "TestStep", "Next");
                optParam.testMode = 3;
            }
            else if (radio_pro_only_pass.Checked)
            {
                INI.WriteValueToIniFile("DeltaL", "TestStep", "PassRecord");
                optParam.testMode = 4;
            }
            else
            {
                INI.WriteValueToIniFile("DeltaL", "TestStep", "Next");
                optParam.testMode = 3;
            }

            //键盘 
            if (radio_key_close.Checked)
            {
                INI.WriteValueToIniFile("DeltaL", "Keyboard", "Disable");
                optParam.keyMode = 0;
            }
            else
            {
                INI.WriteValueToIniFile("DeltaL", "Keyboard", "Spec");
                optParam.keyMode = 1;
            }

            //流水号
            if (radio_sn_manual.Checked)
            {
                INI.WriteValueToIniFile("DeltaL", "SN Method", "Manual");
            }
            else
            {
                INI.WriteValueToIniFile("DeltaL", "SN Method", "Auto");
            }

            INI.WriteValueToIniFile("DeltaL", "SN_Head", tx_sn_prefix.Text);
            optParam.snPrefix = tx_sn_prefix.Text;

            INI.WriteValueToIniFile("DeltaL", "SerialNumber", tx_sn_begin.Text);
            optParam.snBegin = tx_sn_begin.Text;

            //sava mode
            if (radio_save_date.Checked)
            {
                optParam.exportMode = 1;
                INI.WriteValueToIniFile("DeltaL", "Naming Method", "ByDate");

                INI.WriteValueToIniFile("DeltaL", "HistoryFile", tx_history_report.Text);
                optParam.historyExportFileName = tx_history_report.Text;

                INI.WriteValueToIniFile("DeltaL", "ExportFile", tx_export_report.Text);
                optParam.outputExportFileName = tx_export_report.Text;

            }
            else
            {
                optParam.exportMode = 2;
                INI.WriteValueToIniFile("DeltaL", "Naming Method", "ByProject");

                INI.WriteValueToIniFile("DeltaL", "HistoryFile", tx_history_report.Text);

                INI.WriteValueToIniFile("DeltaL", "ExportFile", tx_export_report.Text);
            }

            //即时确认
            if (radio_real_time_on.Checked)
            {
                INI.WriteValueToIniFile("DeltaL", "Real_time_confirm", "Enable");
                optParam.real_time_enable = 1;
            }
            else
            {
                INI.WriteValueToIniFile("DeltaL", "Real_time_confirm", "Disable");
                optParam.real_time_enable = 0;
            }

            //测试模式
            if (radio_test_tdr.Checked)
            {
                INI.WriteValueToIniFile("DeltaL", "meas_type", "Pnd-TDR");
                optParam.meas_type = 0;
            }
            else
            {
                INI.WriteValueToIniFile("DeltaL", "meas_type", "S-Param");
                optParam.meas_type = 1;
            }

            //端口选择
            if (radio_port_13.Checked)
            {
                INI.WriteValueToIniFile("DeltaL", "port", "1 & 3");
                optParam.meas_type = 13;
            }
            else
            {
                INI.WriteValueToIniFile("DeltaL", "port", "2 & 4");
                optParam.meas_type = 24;
            }

            //Insertion Loss Y
            if (radio_offset_auto.Checked)
            {
                INI.WriteValueToIniFile("DeltaL", "Insertion_Loss", "");
                optParam.Compensation_mode = 1;
            }
            else
            {
                if (tx_offset.Text.Length > 0)
                {
                    INI.WriteValueToIniFile("DeltaL", "Insertion_Loss", tx_offset.Text);
                    int offsetValue;
                    if (int.TryParse(tx_offset.Text, out offsetValue))
                    {
                        optParam.offsetValue = offsetValue;
                    }
                    else
                    {
                        // 转换失败，做相应的处理
                    }
                }
                optParam.Compensation_mode = 0;
            }

            OnSnChanged();

            this.Close();
        }

        private void btn_opt_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel_loss_def_value_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            optParam.offsetValue = 3;
            tx_offset.Text = "3";
        }

        private void radio_save_date_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_save_date.Checked)
            {
                optParam.exportMode = 1;
                INI.WriteValueToIniFile("DeltaL", "Naming Method", "ByDate");
                string dateTime = DateTime.Now.ToString("yyyyMMdd");

                tx_history_report.Text = $"Deltal_{dateTime}_Export";
                INI.WriteValueToIniFile("DeltaL", "HistoryFile", tx_history_report.Text);
                optParam.historyExportFileName = tx_history_report.Text;

                tx_export_report.Text = $"Deltal_{dateTime}_Export";
                INI.WriteValueToIniFile("DeltaL", "ExportFile", tx_export_report.Text);
                optParam.outputExportFileName = tx_export_report.Text;
            }
        }

        private void radio_save_param_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_save_param.Checked)
            {
                optParam.exportMode = 2;
                INI.WriteValueToIniFile("DeltaL", "Naming Method", "ByProject");

                tx_history_report.Text = "Deltal_Project_History";
                INI.WriteValueToIniFile("DeltaL", "HistoryFile", tx_history_report.Text);

                tx_export_report.Text = "Deltal_Project_Export";
                INI.WriteValueToIniFile("DeltaL", "ExportFile", tx_export_report.Text);
            }
        }

        private void radio_offset_auto_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_offset_auto.Checked)
            {
                linkLabel_loss_def_value.Enabled = false;
                tx_offset.Enabled = false;
            }
        }

        private void radio_offset_def_CheckedChanged(object sender, EventArgs e)
        {
            if(radio_offset_def.Checked)
            {
                linkLabel_loss_def_value.Enabled = true;
                tx_offset.Enabled = true;
            }
        }







        //
    }//end DevOptSet
}//namespace
