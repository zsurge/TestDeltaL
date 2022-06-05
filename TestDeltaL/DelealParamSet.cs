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

        //添加是否存储XML标志位，TRUE= 已保存；FALSE = 未保存；
        public bool isSaveXml = true;


        DelealParam delealParam = new DelealParam();

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
            isSaveXml = false;

            if (dgv_xml_show.DataSource == null)
            {
                //清空参数表格            
                if (dgv_xml_show.Rows.Count > 0)
                {
                    for (int i = 0; i < dgv_xml_show.Rows.Count; i++)
                    {
                        dgv_xml_show.Rows.Clear();
                    }
                }
            }
            else
            {
                DataTable dt = (DataTable)dgv_xml_show.DataSource;
                dt.Rows.Clear();
                dgv_xml_show.DataSource = dt;
            }

            //新建一默认行
            CreateOrAddRow();
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

        //新增或者是新添加一行
        private void CreateOrAddRow()
        {
            isSaveXml = false;
            if (dgv_xml_show.Rows.Count == 0)
            {
                if (dgv_xml_show.DataSource == null)
                {
                    int index = this.dgv_xml_show.Rows.Add();
                    this.dgv_xml_show.Rows[index].Cells[0].Value = delealParam.Id;
                    this.dgv_xml_show.Rows[index].Cells[1].Value = dgv_xml_show.Rows.Count;
                    this.dgv_xml_show.Rows[index].Cells[2].Value = delealParam.Method;
                    this.dgv_xml_show.Rows[index].Cells[3].Value = delealParam.Layer;
                    this.dgv_xml_show.Rows[index].Cells[4].Value = delealParam.Description;
                    this.dgv_xml_show.Rows[index].Cells[5].Value = delealParam.ShortLength;
                    this.dgv_xml_show.Rows[index].Cells[6].Value = delealParam.MediumLength;
                    this.dgv_xml_show.Rows[index].Cells[7].Value = delealParam.LongLength;
                    this.dgv_xml_show.Rows[index].Cells[8].Value = delealParam.RecordPath;
                    this.dgv_xml_show.Rows[index].Cells[9].Value = delealParam.SaveCurve;
                    this.dgv_xml_show.Rows[index].Cells[10].Value = delealParam.SaveImage;
                }
                else
                {
                    string[] rowVals = new string[24];
                    rowVals[0] = delealParam.Id;
                    rowVals[1] = (dgv_xml_show.Rows.Count + 1).ToString(); //(delealParam.TestStep++).ToString();
                    rowVals[2] = delealParam.Method;
                    rowVals[3] = delealParam.Layer;
                    rowVals[4] = delealParam.Description;
                    rowVals[5] = delealParam.ShortLength.ToString();
                    rowVals[6] = delealParam.MediumLength.ToString();
                    rowVals[7] = delealParam.LongLength.ToString();
                    rowVals[8] = delealParam.RecordPath;
                    rowVals[9] = delealParam.SaveCurve.ToString();
                    rowVals[9] = delealParam.SaveImage.ToString();
                    ((DataTable)dgv_xml_show.DataSource).Rows.Add(rowVals);
                }
            }
            else
            {
                if (dgv_xml_show.DataSource == null)
                {
                    int index = dgv_xml_show.Rows.Add();//添加一行

                    DataGridViewRow row = dgv_xml_show.Rows[dgv_xml_show.CurrentRow.Index]; //获取当前行数据

                    //添加一新行，并把数据赋值给新行
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        dgv_xml_show.Rows[index].Cells[i].Value = row.Cells[i].Value;
                    }


                    //更新STEP
                    //delealParam.TestStep = dgv_xml_show.Rows.Count;
                    //dgv_xml_show.Rows[index].Cells[1].Value = (delealParam.TestStep++).ToString();      

                    dgv_xml_show.Rows[index].Cells[1].Value = dgv_xml_show.Rows.Count.ToString();
                }
                else
                {
                    DataGridViewRow row = dgv_xml_show.Rows[dgv_xml_show.CurrentRow.Index]; //获取当前行数据
                    string[] rowVals = new string[24];

                    //添加一新行，并把数据赋值给新行
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        rowVals[i] = row.Cells[i].Value.ToString();
                    }
                    //更新STEP
                    //delealParam.TestStep = dgv_xml_show.Rows.Count+1;
                    //rowVals[1] = (delealParam.TestStep++).ToString();

                    rowVals[1] = (dgv_xml_show.Rows.Count + 1).ToString();

                    ((DataTable)dgv_xml_show.DataSource).Rows.Add(rowVals);
                }
            }//end dgv_xml_show.Rows.Count == 0

            dgv_xml_show.CurrentCell = dgv_xml_show.Rows[this.dgv_xml_show.Rows.Count - 1].Cells[0];
        }//end CreateOrAddRow

        ////////////////////////////////////////////////////
    }//end class
}//end nameplace
