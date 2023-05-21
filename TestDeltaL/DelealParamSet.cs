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

        //layer是几层
        public int layer_value = 2;

        private Color originalHeaderColor;      //保存默认背景色
        private int highlightedRowIndex = -1; // 记录当前高亮显示的行索引



        DelealParam delealParam = new DelealParam();
        FreqLimit   freqParam = new FreqLimit();

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
            if (layer_value == 2)
            {
                delealParam.Method = "2L";
                delealParam.ShortLength = "--";
            }
            else
            {
                delealParam.Method = "3L";
                delealParam.ShortLength = "3";
            }

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
                    rowVals[0] = (delealParam.Id + 1).ToString();
                    rowVals[1] = (dgv_xml_show.Rows.Count + 1).ToString(); //(delealParam.TestStep++).ToString();
                    rowVals[2] = delealParam.Method;
                    rowVals[3] = delealParam.Layer;
                    rowVals[4] = delealParam.Description;
                    rowVals[5] = delealParam.ShortLength.ToString();
                    rowVals[6] = delealParam.MediumLength.ToString();
                    rowVals[7] = delealParam.LongLength.ToString();
                    rowVals[8] = delealParam.RecordPath;
                    rowVals[9] = delealParam.SaveCurve.ToString();
                    rowVals[10] = delealParam.SaveImage.ToString();
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

                    dgv_xml_show.Rows[index].Cells[0].Value = dgv_xml_show.Rows.Count.ToString();
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

                    rowVals[0] = (dgv_xml_show.Rows.Count + 1).ToString();
                    rowVals[1] = (dgv_xml_show.Rows.Count + 1).ToString();

                    ((DataTable)dgv_xml_show.DataSource).Rows.Add(rowVals);
                }
            }//end dgv_xml_show.Rows.Count == 0

            dgv_xml_show.CurrentCell = dgv_xml_show.Rows[this.dgv_xml_show.Rows.Count - 1].Cells[0];
        }//end CreateOrAddRow

        //频率点新增或者是新添加一行
        private void CreateOrAddRowFreqLimit()
        {
            isSaveXml = false;

            if (dgv_param_show.Rows.Count == 0)
            {
                if (dgv_param_show.DataSource == null)
                {
                    int index = this.dgv_param_show.Rows.Add();
                    this.dgv_param_show.Rows[index].Cells[0].Value = freqParam.Id;
                    this.dgv_param_show.Rows[index].Cells[1].Value = freqParam.Frequency;
                    this.dgv_param_show.Rows[index].Cells[2].Value = freqParam.LossLowerLimite;
                    this.dgv_param_show.Rows[index].Cells[3].Value = freqParam.LossUpperLimite;
                    this.dgv_param_show.Rows[index].Cells[4].Value = freqParam.Uncertainty;
                    this.dgv_param_show.Rows[index].Cells[5].Value = freqParam.Difference;
            
                }
                else
                {
                    string[] rowVals = new string[10];
                    rowVals[0] = (freqParam.Id + 1).ToString();
                    rowVals[1] = freqParam.Frequency.ToString(); ;
                    rowVals[2] = freqParam.LossLowerLimite.ToString(); ;
                    rowVals[3] = freqParam.LossUpperLimite.ToString(); ;
                    rowVals[4] = freqParam.Uncertainty.ToString(); ;
                    rowVals[5] = freqParam.Difference.ToString();

                    ((DataTable)dgv_param_show.DataSource).Rows.Add(rowVals);
                }
            }
            else
            {
                if (dgv_param_show.DataSource == null)
                {
                    int index = dgv_param_show.Rows.Add();//添加一行

                    DataGridViewRow row = dgv_param_show.Rows[dgv_param_show.CurrentRow.Index]; //获取当前行数据

                    //添加一新行，并把数据赋值给新行
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        dgv_param_show.Rows[index].Cells[i].Value = row.Cells[i].Value;
                    }

                    dgv_param_show.Rows[index].Cells[0].Value = dgv_param_show.Rows.Count.ToString();

                }
                else
                {
                    DataGridViewRow row = dgv_param_show.Rows[dgv_param_show.CurrentRow.Index]; //获取当前行数据
                    string[] rowVals = new string[10];

                    //添加一新行，并把数据赋值给新行
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        rowVals[i] = row.Cells[i].Value.ToString();
                    }

                    rowVals[0] = (dgv_param_show.Rows.Count + 1).ToString();
             

                    ((DataTable)dgv_param_show.DataSource).Rows.Add(rowVals);
                }
            }//end dgv_param_show.Rows.Count == 0

            dgv_param_show.CurrentCell = dgv_param_show.Rows[this.dgv_param_show.Rows.Count - 1].Cells[0];

          // 设置新行的选择背景色和选择前景色为默认值
            dgv_param_show.Rows[dgv_param_show.Rows.Count - 1].DefaultCellStyle.SelectionBackColor = dgv_param_show.DefaultCellStyle.BackColor;
            dgv_param_show.Rows[dgv_param_show.Rows.Count - 1].DefaultCellStyle.SelectionForeColor = dgv_param_show.DefaultCellStyle.ForeColor;
        }//end CreateOrAddRow

        private void tsb_layer2_Click(object sender, EventArgs e)
        {
            layer_value = 2;
            tsb_layer2.BackColor = SystemColors.Highlight;
            tsb_layer3.BackColor = SystemColors.Control;
            tsb_layer3.Checked = false;
        }

        private void tsb_layer3_Click(object sender, EventArgs e)
        {
            layer_value = 3;
            tsb_layer3.BackColor = SystemColors.Highlight;
            tsb_layer2.BackColor = SystemColors.Control;
            tsb_layer2.Checked = false;
        }

        //新增一行
        private void tsb_add_param_Click(object sender, EventArgs e)
        {
            //新建一默认行
            CreateOrAddRow();

            int a = int.Parse(dgv_xml_show.Rows[0].Cells[1].Value.ToString());
        }

        private void tsb_copy_param_Click(object sender, EventArgs e)
        {
            CreateOrAddRowFreqLimit();
        }


        private void dgv_param_show_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            // 判断是否点击的是标题行
            if (e.RowIndex == -1 && e.Button == MouseButtons.Left)
            {
                // 保存原始标题行的背景色
                originalHeaderColor = dgv_param_show.ColumnHeadersDefaultCellStyle.BackColor;

                // 高亮整个标题行
                dgv_param_show.EnableHeadersVisualStyles = false;
                dgv_param_show.ColumnHeadersDefaultCellStyle.BackColor = dgv_param_show.ColumnHeadersDefaultCellStyle.SelectionBackColor;

                // 单独设置标题行的第一列背景色为原来的颜色
                dgv_param_show.Columns[0].HeaderCell.Style.BackColor = originalHeaderColor;
            }
            else if (e.RowIndex >= 0 && e.Button == MouseButtons.Left)
            {
                // 恢复之前高亮显示的行的样式
                if (highlightedRowIndex != -1)
                {
                    dgv_param_show.Rows[highlightedRowIndex].DefaultCellStyle.SelectionBackColor = dgv_param_show.DefaultCellStyle.BackColor;
                    dgv_param_show.Rows[highlightedRowIndex].DefaultCellStyle.SelectionForeColor = dgv_param_show.DefaultCellStyle.ForeColor;
                }

                // 恢复列标题行的背景色
                dgv_param_show.EnableHeadersVisualStyles = true;
                dgv_param_show.ColumnHeadersDefaultCellStyle.BackColor = originalHeaderColor;

                // 高亮显示选定的行
                dgv_param_show.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = dgv_param_show.ColumnHeadersDefaultCellStyle.SelectionBackColor;
                dgv_param_show.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = dgv_param_show.ColumnHeadersDefaultCellStyle.SelectionForeColor;

                // 更新当前高亮显示的行索引
                highlightedRowIndex = e.RowIndex;
            }
        }


        ////////////////////////////////////////////////////
    }//end class
}//end nameplace
