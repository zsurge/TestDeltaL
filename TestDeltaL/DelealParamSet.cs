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
    public partial class DelealParamSet : Form
    {
        public DelealParamSet(DataTable tmp)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;//设置form1的开始位置为屏幕的中央
            this.tmpDt = tmp;
        }


        DataTable tmpDt;
        public delegate void ChangeDgvHandler(DataGridView dgv);  //定义委托
        public event ChangeDgvHandler ChangeDgv;  //定义事件

        private void tsb_create_xml_Click(object sender, EventArgs e)
        {

        }

        private void DelealParamSet_Load(object sender, EventArgs e)
        {
            if (PubConfig.gDeviceType == PubConfig.DeviceType.TDR)
            {
                this.Text = "TDR参数设置";
                tsb_layer2.Visible = false;
                tsb_layer3.Visible = false;
            }
            else if(PubConfig.gDeviceType == PubConfig.DeviceType.DELTAL)
            {
                this.Text = "DeleaL参数设置";
                tsb_layer2.Visible = true;
                tsb_layer3.Visible = true;
            }
        }
    }
}
