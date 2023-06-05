using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace TestDeltaL
{
    public partial class DelealParamSet : Form
    {

        //添加是否存储XML标志位，TRUE= 已保存；FALSE = 未保存；
        public bool isSaveXml = true;

        //layer是几层
        private int layer_value = 2;

        //是否选中频率点标题行 0未选中，1选中
        private bool isSelectFreqFlag = false;

        private Color originalHeaderColor;      //保存默认背景色

        //private int highlightedRowIndex = -1; // 记录当前高亮显示的行索引

        public static string xmlFilePath = string.Empty; //配方存放地址

        
        MarkerData   freqParam = new MarkerData();

        List<MarkerData> freqParamList = new List<MarkerData>();

        Dictionary<int, List<MarkerData>> configDict = new Dictionary<int, List<MarkerData>>();

        public XmlSerializer serializer = new XmlSerializer(typeof(List<KeyFreqLimit>));

        //存放所有数据
        public ListData delealParam = new ListData();
        public DeltaL gDeltaLData = new DeltaL
        {
            Lists = new List<ListData>(),
            Markers = new List<MarkerData>()
        };


        public DelealParamSet(DataTable tmp)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;//设置form1的开始位置为屏幕的中央
            this.tmpDt = tmp;
        }

      

        DataTable tmpDt;
        public delegate void ChangeDgvHandler(DataGridView dgv);  //定义委托
        public event ChangeDgvHandler ChangeDgv;  //定义事件


        //新建档案
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

            //默认新建一行配方
            CreateOrAddRow();
        }

        //初始化装载
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

            // 保存原始标题行的背景色
            originalHeaderColor = dgv_param_show.ColumnHeadersDefaultCellStyle.BackColor;

            //dgv_param_show.RowsDefaultCellStyle.SelectionBackColor = dgv_param_show.RowsDefaultCellStyle.BackColor;
            //dgv_param_show.RowsDefaultCellStyle.SelectionForeColor = dgv_param_show.RowsDefaultCellStyle.ForeColor;

        }

        private ListData save_xml_row_data(int cur_index)
        {
            //创建一个新的ListData对象来保存rowVals数组中的值
            ListData row_data = new ListData();

            row_data.Id = int.Parse(dgv_xml_show.Rows[cur_index].Cells[0].Value.ToString());
            row_data.TestStep = int.Parse(dgv_xml_show.Rows[cur_index].Cells[1].Value.ToString());
            row_data.Method = dgv_xml_show.Rows[cur_index].Cells[2].Value.ToString();
            row_data.Layer = dgv_xml_show.Rows[cur_index].Cells[3].Value.ToString();
            row_data.Description = dgv_xml_show.Rows[cur_index].Cells[4].Value.ToString();
            row_data.ShortLength = dgv_xml_show.Rows[cur_index].Cells[5].Value.ToString();
            row_data.MediumLength = int.Parse(dgv_xml_show.Rows[cur_index].Cells[6].Value.ToString());
            row_data.LongLength = int.Parse(dgv_xml_show.Rows[cur_index].Cells[7].Value.ToString());
            row_data.RecordPath = dgv_xml_show.Rows[cur_index].Cells[8].Value.ToString();
            row_data.SaveCurve = bool.Parse(dgv_xml_show.Rows[cur_index].Cells[9].Value.ToString());
            row_data.SaveImage = bool.Parse(dgv_xml_show.Rows[cur_index].Cells[10].Value.ToString());

            return row_data;
        }

        private MarkerData save_freq_row_data(int cur_index)
        {
            //创建一个新的ListData对象来保存rowVals数组中的值
            MarkerData row_data = new MarkerData();

            row_data.Id = int.Parse(dgv_param_show.Rows[cur_index].Cells[0].Value.ToString());
            row_data.Frequency = int.Parse(dgv_param_show.Rows[cur_index].Cells[1].Value.ToString());
            row_data.LossLowerLimite = double.Parse(dgv_param_show.Rows[cur_index].Cells[2].Value.ToString());
            row_data.LossUpperLimite = double.Parse(dgv_param_show.Rows[cur_index].Cells[3].Value.ToString());
            row_data.Uncertainty = int.Parse(dgv_param_show.Rows[cur_index].Cells[4].Value.ToString());
            row_data.Difference = int.Parse(dgv_param_show.Rows[cur_index].Cells[5].Value.ToString());

            return row_data;
        }


        //新增或者是新添加一行
        private void CreateOrAddRow()
        {
            isSaveXml = false;
            if (layer_value == 2)
            {
                delealParam.Method = "2L";
                delealParam.ShortLength = "--";

                //这里还需要将已保存的改为2L和--
            }
            else
            {
                delealParam.Method = "3L";
                delealParam.ShortLength = "3";

                //这里还需要将已保存的改为3L和3
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

                    gDeltaLData.Lists.Add(save_xml_row_data(index));
                }
                else
                {
                    string[] rowVals = new string[24];
                    delealParam.Id += 1;
                    rowVals[0] = delealParam.Id.ToString();
                    delealParam.TestStep = dgv_xml_show.Rows.Count + 1;
                    rowVals[1] = delealParam.TestStep.ToString(); //(delealParam.TestStep++).ToString();
                    rowVals[2] = delealParam.Method;
                    rowVals[3] = delealParam.Layer;
                    rowVals[4] = delealParam.Description;
                    rowVals[5] = delealParam.ShortLength.ToString();
                    rowVals[6] = delealParam.MediumLength.ToString();
                    rowVals[7] = delealParam.LongLength.ToString();
                    rowVals[8] = delealParam.RecordPath;
                    rowVals[9] = delealParam.SaveCurve.ToString();
                    rowVals[10] = delealParam.SaveImage.ToString();

                    gDeltaLData.Lists.Add(delealParam);

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

                    dgv_xml_show.Rows[index].Cells[0].Value = (dgv_xml_show.Rows.Count - 1).ToString();
                    dgv_xml_show.Rows[index].Cells[1].Value = dgv_xml_show.Rows.Count.ToString();

                    gDeltaLData.Lists.Add(save_xml_row_data(index));
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
   
                    rowVals[0] = (dgv_xml_show.Rows.Count + 1).ToString();
                    rowVals[1] = (dgv_xml_show.Rows.Count + 1).ToString();

                    //这里这个序列号可能有问题，需要再确认20230605
                    gDeltaLData.Lists.Add(save_xml_row_data(dgv_xml_show.Rows.Count));

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
                    freqParam.Id = int.Parse(dgv_xml_show.Rows[0].Cells[1].Value.ToString());
                    this.dgv_param_show.Rows[index].Cells[0].Value = freqParam.Id;
                    this.dgv_param_show.Rows[index].Cells[1].Value = freqParam.Frequency;
                    this.dgv_param_show.Rows[index].Cells[2].Value = freqParam.LossLowerLimite;
                    this.dgv_param_show.Rows[index].Cells[3].Value = freqParam.LossUpperLimite;
                    this.dgv_param_show.Rows[index].Cells[4].Value = freqParam.Uncertainty;
                    this.dgv_param_show.Rows[index].Cells[5].Value = freqParam.Difference;

                    gDeltaLData.Markers.Add(save_freq_row_data(index));

                    freqParamList.Add(freqParam);
                    int dictIndex = int.Parse(dgv_xml_show.Rows[0].Cells[1].Value.ToString());
                    configDict.Add(dictIndex, freqParamList);
                }
                else
                {
                    string[] rowVals = new string[10];
                    freqParam.Id = int.Parse(dgv_xml_show.Rows[0].Cells[1].Value.ToString());
                    rowVals[0] = freqParam.Id.ToString(); 
                    rowVals[1] = freqParam.Frequency.ToString(); ;
                    rowVals[2] = freqParam.LossLowerLimite.ToString(); ;
                    rowVals[3] = freqParam.LossUpperLimite.ToString(); ;
                    rowVals[4] = freqParam.Uncertainty.ToString(); ;
                    rowVals[5] = freqParam.Difference.ToString();

                    //gDeltaLData.Markers.Add(save_freq_row_data(index));

                    freqParamList.Add(freqParam);
                    int dictIndex = int.Parse(dgv_xml_show.Rows[0].Cells[1].Value.ToString());
                    configDict.Add(dictIndex, freqParamList);

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

                    dgv_param_show.Rows[index].Cells[0].Value = dgv_xml_show.Rows[0].Cells[1].Value;


                    int id = int.Parse(row.Cells["detail_ID"].Value.ToString());
                    double frequency = double.Parse(row.Cells["Frequency"].Value.ToString());
                    double lossLowerLimite = double.Parse(row.Cells["Loss_LowerLimit"].Value.ToString());
                    double lossUpperLimite = double.Parse(row.Cells["Loss_UpperLimit"].Value.ToString());
                    double uncertainty = double.Parse(row.Cells["Uncertainty"].Value.ToString());
                    double difference = double.Parse(row.Cells["Difference"].Value.ToString());

                    MarkerData freq = new MarkerData()
                    {
                        Id = id,
                        Frequency = frequency,
                        LossLowerLimite = lossLowerLimite,
                        LossUpperLimite = lossUpperLimite,
                        Uncertainty = uncertainty,
                        Difference = difference
                    };

                    gDeltaLData.Markers.Add(save_freq_row_data(index));

                    //这种可以添加一行
                    //freqParamList.Add(freq);

                    //下面这种方法也可以添加一行
                    //List<MarkerData> freqParamListToUpdate = configDict[id];
                    List<MarkerData> freqParamListToUpdate = configDict[1];//这里不用ID用1，表示只有第1行配方才可以加添频率点
                    freqParamListToUpdate.Add(freq);
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
                    rowVals[0] = dgv_xml_show.Rows[0].Cells[1].Value.ToString();
                    ((DataTable)dgv_param_show.DataSource).Rows.Add(rowVals);

                    int id = int.Parse(row.Cells["detail_ID"].Value.ToString());
                    double frequency = double.Parse(row.Cells["Frequency"].Value.ToString());
                    double lossLowerLimite = double.Parse(row.Cells["Loss_LowerLimit"].Value.ToString());
                    double lossUpperLimite = double.Parse(row.Cells["Loss_UpperLimit"].Value.ToString());
                    double uncertainty = double.Parse(row.Cells["Uncertainty"].Value.ToString());
                    double difference = double.Parse(row.Cells["Difference"].Value.ToString());

                    MarkerData freq = new MarkerData()
                    {
                        Id = id,
                        Frequency = frequency,
                        LossLowerLimite = lossLowerLimite,
                        LossUpperLimite = lossUpperLimite,
                        Uncertainty = uncertainty,
                        Difference = difference
                    };
                    List<MarkerData> freqParamListToUpdate = configDict[1];//这里不用ID用1，表示只有第1行配方才可以加添频率点
                    freqParamListToUpdate.Add(freq);


                    gDeltaLData.Markers.Add(save_freq_row_data(dgv_param_show.CurrentRow.Index));

                }
            }//end dgv_param_show.Rows.Count == 0

            dgv_param_show.CurrentCell = dgv_param_show.Rows[this.dgv_param_show.Rows.Count - 1].Cells[0];

        }//end CreateOrAddRow

        //拷贝一行配方
        private void CopyRecipeRow()
        {
            //复制配方行
            isSaveXml = false;

            if (dgv_xml_show.Rows.Count == 0)
            {
                MessageBox.Show("请先新建一条配方");
                return;
            }

            if (dgv_xml_show.DataSource == null)
            {
                int index = dgv_xml_show.Rows.Add();//添加一行

                DataGridViewRow row = dgv_xml_show.Rows[dgv_xml_show.CurrentRow.Index]; //获取当前行数据

                //添加一新行，并把数据赋值给新行
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    dgv_xml_show.Rows[index].Cells[i].Value = row.Cells[i].Value;
                }

                dgv_xml_show.Rows[index].Cells[1].Value = (dgv_xml_show.Rows.Count).ToString();
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

                rowVals[1] = (dgv_xml_show.Rows.Count + 1).ToString();
                ((DataTable)dgv_xml_show.DataSource).Rows.Add(rowVals);
            }

            dgv_xml_show.CurrentCell = dgv_xml_show.Rows[this.dgv_xml_show.Rows.Count - 1].Cells[0];
        }


        //拷贝一行频率点
        private void CopyFreqRow()
        {
            //复制配方行
            isSaveXml = false;

            if (dgv_param_show.Rows.Count == 0)
            {
                MessageBox.Show("请先新建一条配方");
                return;
            }

            if (dgv_param_show.DataSource == null)
            {
                int index = dgv_param_show.Rows.Add();//添加一行

                DataGridViewRow row = dgv_param_show.Rows[dgv_param_show.CurrentRow.Index]; //获取当前行数据
                                                                                  //添加一新行，并把数据赋值给新行
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    dgv_param_show.Rows[index].Cells[i].Value = row.Cells[i].Value;
                }

                dgv_param_show.Rows[index].Cells[1].Value = (dgv_param_show.Rows.Count).ToString();
            }
            else
            {
                DataGridViewRow row = dgv_param_show.Rows[dgv_param_show.CurrentRow.Index]; //获取当前行数据
                string[] rowVals = new string[24];

                //添加一新行，并把数据赋值给新行
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    rowVals[i] = row.Cells[i].Value.ToString();
                }


                rowVals[1] = (dgv_param_show.Rows.Count + 1).ToString();
                ((DataTable)dgv_param_show.DataSource).Rows.Add(rowVals);
            }

            dgv_param_show.CurrentCell = dgv_param_show.Rows[this.dgv_param_show.Rows.Count - 1].Cells[0];
        }


        private void DelFreqRow()
        {
            //删除频率一行
            isSaveXml = false;

            if (dgv_param_show.Rows.Count > 0)
            {
                dgv_param_show.Rows.Remove(dgv_param_show.CurrentRow);

                //for (int i = 0; i < dgv_param_show.RowCount; i++)
                //{
                //    dgv_param_show.Rows[i].Cells["TestStep"].Value = i + 1;
                //}
            }
        }

        private void DelRecipeRow()
        {
            //删除配方一行
            isSaveXml = false;

            if (dgv_xml_show.Rows.Count > 0)
            {
                dgv_xml_show.Rows.Remove(dgv_xml_show.CurrentRow);

                for (int i = 0; i < dgv_xml_show.RowCount; i++)
                {
                    dgv_xml_show.Rows[i].Cells["Step"].Value = i + 1;
                }
            }
        }

        private void tsb_layer2_Click(object sender, EventArgs e)
        {
            layer_value = 2;
            tsb_layer2.BackColor = SystemColors.Highlight;
            tsb_layer3.BackColor = SystemColors.Control;
            tsb_layer3.Checked = false;

            if (dgv_xml_show.Rows.Count > 0)
            {
                for (int i = 0; i < dgv_xml_show.Rows.Count; i++)
                {
                    // 获取第三列和第五列的单元格，并更新单元格的值
                    dgv_xml_show.Rows[i].Cells[2].Value = "2L";
                    dgv_xml_show.Rows[i].Cells[5].Value = "--";
                }
            }

        }

        private void tsb_layer3_Click(object sender, EventArgs e)
        {
            layer_value = 3;
            tsb_layer3.BackColor = SystemColors.Highlight;
            tsb_layer2.BackColor = SystemColors.Control;
            tsb_layer2.Checked = false;

            if (dgv_xml_show.Rows.Count > 0)
            {
                for (int i = 0; i < dgv_xml_show.Rows.Count; i++)
                {
                    // 获取第三列和第五列的单元格，并更新单元格的值
                    dgv_xml_show.Rows[i].Cells[2].Value = "3L";
                    dgv_xml_show.Rows[i].Cells[5].Value = "2";
                }
            }

        }

        //新增一行
        private void tsb_add_param_Click(object sender, EventArgs e)
        {
            if (dgv_xml_show.RowCount == 0)
            {
                //默认新建一行配方
                CreateOrAddRow();
            }
            else
            {
                // 第一行第一列被选中
                DataGridViewCell selectedCell = dgv_xml_show.SelectedCells[0];
                if ((selectedCell.ColumnIndex == 1 && selectedCell.RowIndex == 0 && isSelectFreqFlag) ||
                   (selectedCell.ColumnIndex == 1 && selectedCell.RowIndex == 0 && dgv_param_show.SelectedCells.Count > 0))
                {
                    //新增一行频率点
                    CreateOrAddRowFreqLimit();
                }
                else
                {
                    //新建一行配方
                    CreateOrAddRow();
                }
            }
        }

        //复制一行
        private void tsb_copy_param_Click(object sender, EventArgs e)
        {
            // 第一行第一列被选中
            DataGridViewCell selectedCell = dgv_xml_show.SelectedCells[0];
            if ((selectedCell.ColumnIndex == 1 && selectedCell.RowIndex == 0 && isSelectFreqFlag) ||
               (selectedCell.ColumnIndex == 1 && selectedCell.RowIndex == 0 && dgv_param_show.SelectedCells.Count > 0))
            {
                //新增一行频率点
                CopyFreqRow();
            }
            else
            {
                //新建一行配方
                CopyRecipeRow();
            }
        }

        //删除一行
        private void tsb_del_param_Click(object sender, EventArgs e)
        {
            // 第一行第一列被选中
            DataGridViewCell selectedCell = dgv_xml_show.SelectedCells[0];
            if ((selectedCell.ColumnIndex == 1 && selectedCell.RowIndex == 0 && isSelectFreqFlag) ||
               (selectedCell.ColumnIndex == 1 && selectedCell.RowIndex == 0 && dgv_param_show.SelectedCells.Count > 0))
            {
                //新增一行频率点
                DelFreqRow();
            }
            else
            {
                //删除一行配方
                DelRecipeRow();
            }
        }


/*
        private void dgv_param_show_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            // 判断是否点击的是标题行
            if (e.RowIndex == -1 && e.Button == MouseButtons.Left)
            {
                //被选中
                isSelectFreqFlag = true;

                if (dgv_xml_show.Rows.Count > 0)
                {
                    // 设置 DataGridView 的第2行第2列为选中状态
                    dgv_xml_show.CurrentCell = dgv_xml_show[1, 0];
                    selcet_first_row();
                }


                // 恢复之前高亮显示的行的样式
                if (highlightedRowIndex != -1)
                {
                    dgv_param_show.Rows[highlightedRowIndex].DefaultCellStyle.SelectionBackColor = dgv_param_show.DefaultCellStyle.BackColor;
                    dgv_param_show.Rows[highlightedRowIndex].DefaultCellStyle.SelectionForeColor = dgv_param_show.DefaultCellStyle.ForeColor;
                }

                // 保存原始标题行的背景色
                originalHeaderColor = dgv_param_show.ColumnHeadersDefaultCellStyle.BackColor;

                // 高亮整个标题行
                dgv_param_show.EnableHeadersVisualStyles = false;
                dgv_param_show.ColumnHeadersDefaultCellStyle.BackColor = dgv_param_show.ColumnHeadersDefaultCellStyle.SelectionBackColor;

                // 单独设置标题行的第一列背景色为原来的颜色
                dgv_param_show.Columns[0].HeaderCell.Style.BackColor = originalHeaderColor;

                // 更新当前高亮显示的行索引
                highlightedRowIndex = -1;
            }
            else if (e.RowIndex >= 0 && e.Button == MouseButtons.Left)
            {
                //取消选中
                isSelectFreqFlag = false;
                // 恢复之前高亮显示的行的样式
                if (highlightedRowIndex != -1)
                {
                    dgv_param_show.Rows[highlightedRowIndex].DefaultCellStyle.SelectionBackColor = dgv_param_show.DefaultCellStyle.BackColor;
                    dgv_param_show.Rows[highlightedRowIndex].DefaultCellStyle.SelectionForeColor = dgv_param_show.DefaultCellStyle.ForeColor;
                }

                //// 恢复列标题行的背景色
                dgv_param_show.EnableHeadersVisualStyles = true;
                dgv_param_show.ColumnHeadersDefaultCellStyle.BackColor = originalHeaderColor;
                dgv_param_show.Columns[0].HeaderCell.Style.BackColor = originalHeaderColor;

                // 高亮显示选定的行
                dgv_param_show.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = dgv_param_show.ColumnHeadersDefaultCellStyle.SelectionBackColor;
                dgv_param_show.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = dgv_param_show.ColumnHeadersDefaultCellStyle.SelectionForeColor;

                

                // 更新当前高亮显示的行索引
                highlightedRowIndex = e.RowIndex;
            }
        }
        */

        /// <summary>
        /// 获取XML文件数据到datagrid
        /// </summary>
        /// <param name="filePath">XML文件路径</param>
        //private void getXmlInfo(string filePath)
        //{
        //    try
        //    {
        //        DataSet myds = new DataSet();
        //        if (filePath.Length != 0)
        //        {
        //            myds.ReadXml(filePath);
        //            dgv_xml_show.DataSource = myds.Tables[0];
        //            dgv_xml_show.Tag = Path.GetFileNameWithoutExtension(filePath);
        //        }
        //        else
        //        {
        //            MessageBox.Show("未正确装载配方文件");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("配方文件格式错误\r\n" + ex.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }

        //}

        private void getXmlInfo(string filePath)
        {
            try
            {
                DataSet myds = new DataSet();
                if (filePath.Length != 0)
                {
                    myds.ReadXml(filePath);

                    // 清空DataGridView的数据
                    dgv_xml_show.DataSource = null;
                    dgv_xml_show.Rows.Clear();

                    dgv_param_show.DataSource = null;
                    dgv_param_show.Rows.Clear();

                    
                    // 添加XML文件中的数据到DataGridView
                    foreach (DataRow row in myds.Tables[0].Rows)
                    {
                        int index = dgv_xml_show.Rows.Add();
                        dgv_xml_show.Rows[index].Cells["ID"].Value = row["ID"];
                        dgv_xml_show.Rows[index].Cells["Step"].Value = row["Step"];
                        dgv_xml_show.Rows[index].Cells["Method"].Value = row["Method"];
                        dgv_xml_show.Rows[index].Cells["Layer"].Value = row["Layer"];
                        dgv_xml_show.Rows[index].Cells["Description"].Value = row["Description"];
                        dgv_xml_show.Rows[index].Cells["Short_Length"].Value = row["Short_Length"];
                        dgv_xml_show.Rows[index].Cells["Medium_Length"].Value = row["Medium_Length"];
                        dgv_xml_show.Rows[index].Cells["Long_Length"].Value = row["Long_Length"];
                        dgv_xml_show.Rows[index].Cells["RecordPath"].Value = row["RecordPath"];
                        dgv_xml_show.Rows[index].Cells["SaveCurve"].Value = row["SaveCurve"];
                        dgv_xml_show.Rows[index].Cells["SaveImage"].Value = row["SaveImage"];
                    }

                    //foreach (DataRow row in myds.Tables[1].Rows)
                    //{
                    //    int index = dgv_param_show.Rows.Add();
                    //    dgv_param_show.Rows[index].Cells["detail_ID"].Value = row["ID"];
                    //    dgv_param_show.Rows[index].Cells["Frequency"].Value = row["Frequency"];
                    //    dgv_param_show.Rows[index].Cells["Loss_LowerLimit"].Value = row["Loss_LowerLimit"];
                    //    dgv_param_show.Rows[index].Cells["Loss_UpperLimit"].Value = row["Loss_UpperLimit"];
                    //    dgv_param_show.Rows[index].Cells["Uncertainty"].Value = row["Uncertainty"];
                    //    dgv_param_show.Rows[index].Cells["Difference"].Value = row["Difference"];
                    //}


                    
                    int param_count = myds.Tables[1].Rows.Count / myds.Tables[0].Rows.Count;
                    for (int i = 0; i < param_count; i++)
                    {
                        DataRow row = myds.Tables[1].Rows[i];
                        int index = dgv_param_show.Rows.Add();
                        dgv_param_show.Rows[index].Cells["detail_ID"].Value = row["ID"];
                        dgv_param_show.Rows[index].Cells["Frequency"].Value = row["Frequency"];
                        dgv_param_show.Rows[index].Cells["Loss_LowerLimit"].Value = row["Loss_LowerLimit"];
                        dgv_param_show.Rows[index].Cells["Loss_UpperLimit"].Value = row["Loss_UpperLimit"];
                        dgv_param_show.Rows[index].Cells["Uncertainty"].Value = row["Uncertainty"];
                        dgv_param_show.Rows[index].Cells["Difference"].Value = row["Difference"];
                    }

                }
                else
                {
                    MessageBox.Show("未正确装载配方文件");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("配方文件格式错误\r\n" + ex.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void tsb_measure_loadXml_Click(object sender, EventArgs e)
        {
            OpenFileDialog pOpenFileDialog = new OpenFileDialog();

            //设置对话框标题
            pOpenFileDialog.Title = "载入XML文件";
            pOpenFileDialog.Filter = "XML文件|*.xml";
            pOpenFileDialog.InitialDirectory = Environment.CurrentDirectory + "\\Config";
            //监测文件是否存在
            pOpenFileDialog.CheckFileExists = true;
            if (pOpenFileDialog.ShowDialog() == DialogResult.OK)  //如果点击的是打开文件
            {
                xmlFilePath = pOpenFileDialog.FileName;  //获取全路径文件名     

                //反序列化XML
                using (TextReader reader = new StreamReader(xmlFilePath))
                {
                    List<KeyFreqLimit> configList = (List<KeyFreqLimit>)serializer.Deserialize(reader);
                    //Dictionary<int, List<FreqLimit>> configDict = new Dictionary<int, List<FreqLimit>>();
                    foreach (var item in configList)
                    {
                        configDict[item.Key] = item.Data;
                    }
                }

                //将configDict 所有内容加载到
                //if (configDict.Count > 0)
                //{
                //    foreach (var item in configDict)
                //    {
                //        foreach (var limit in item.Value)
                //        {
                //            dgv_xml_show.Rows.Add(limit.Id, limit.Frequency, limit.LossLowerLimite, limit.LossUpperLimite, limit.Uncertainty, limit.Difference);
                //        }
                //    }
                //}

                //getXmlInfo(xmlFilePath);
            } 
        }

        //频率点单击事件
        private void dgv_param_show_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                tx_freq.Text = dgv_param_show.Rows[e.RowIndex].Cells[1].Value.ToString();
                tx_lower.Text = dgv_param_show.Rows[e.RowIndex].Cells[2].Value.ToString();
                tx_up.Text = dgv_param_show.Rows[e.RowIndex].Cells[3].Value.ToString();
                tx_uncertainty.Text = dgv_param_show.Rows[e.RowIndex].Cells[4].Value.ToString();
                tx_difference.Text = dgv_param_show.Rows[e.RowIndex].Cells[5].Value.ToString();
            }
        }

        /// <summary>
        /// Converts data grid view to a data table
        /// </summary>
        /// <param name="dgv"></param>
        /// <returns></returns>
        private DataTable GetDataTableFromDGV(DataGridView dgv)
        {
            var dt = new DataTable();
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                if (column.Visible)
                {
                    // You could potentially name the column based on the DGV column name (beware of dupes)
                    // or assign a type based on the data type of the data bound to this DGV column.
                    dt.Columns.Add(column.Name);
                }
            }

            object[] cellValues = new object[dgv.Columns.Count];
            foreach (DataGridViewRow row in dgv.Rows)
            {
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    cellValues[i] = row.Cells[i].Value;
                }
                dt.Rows.Add(cellValues);
            }

            return dt;
        }


        private void tsb_save_xml_Click(object sender, EventArgs e)
        {
            if (dgv_xml_show.Rows.Count == 0)
            {
                MessageBox.Show("请新建配方");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "保存XML文件";
            sfd.InitialDirectory = Environment.CurrentDirectory + "\\Config";
            sfd.Filter = "XML文件|*.xml";



            if (sfd.ShowDialog() == DialogResult.OK)
            {

                if (File.Exists(sfd.FileName))
                    File.Delete(sfd.FileName);

                //DataTable dT = GetDataTableFromDGV(dgv_xml_show);
                //DataSet dS = new DataSet();
                //dS.Tables.Add(dT);
                //dS.WriteXml(File.OpenWrite(sfd.FileName));
                //dgv_xml_show.Tag = Path.GetFileNameWithoutExtension(sfd.FileName);
                //xmlFilePath = sfd.FileName;
                isSaveXml = true;




                ////序列化configDict并写入输出流中
                List<KeyFreqLimit> configList = new List<KeyFreqLimit>();

                foreach (var item in configDict)
                {
                    configList.Add(new KeyFreqLimit
                    {
                        Key = item.Key,
                        Data = item.Value
                    });
                }

                using (TextWriter writer = new StreamWriter(sfd.FileName))
                {
                    serializer.Serialize(writer, configList);
                }

            }



            //C# 实现datagridview中的数据存放在xml中,并设置根节点的名字为 root，每一行的为一个元素，元素名设置为 page1,page2...pageN,有多少行，就有个page

            //using System.Xml;

            //// 创建 XmlDocument 实例
            //XmlDocument xmlDoc = new XmlDocument();

            //// 创建根节点 root，并添加到 XML 文档中
            //XmlElement root = xmlDoc.CreateElement("root");
            //xmlDoc.AppendChild(root);

            //// 遍历 DataGridView 中的每一行，创建一个新的 page 元素
            //for (int i = 0; i < dataGridView.Rows.Count; i++)
            //{
            //    XmlElement page = xmlDoc.CreateElement("page" + (i + 1));

            //    // 遍历当前行的每一个单元格，创建一个新的子元素并添加到当前 page 元素中
            //    for (int j = 0; j < dataGridView.Columns.Count; j++)
            //    {
            //        XmlElement cell = xmlDoc.CreateElement(dataGridView.Columns[j].Name);
            //        cell.InnerText = dataGridView.Rows[i].Cells[j].Value.ToString();
            //        page.AppendChild(cell);
            //    }

            //    // 将当前 page 元素添加到根节点 root 中
            //    root.AppendChild(page);
            //}

            //// 将保存 XML 文档到文件中
            //xmlDoc.Save("data.xml");
        }

        private void btn_confirm_Click(object sender, EventArgs e)
        {
            if (dgv_param_show.CurrentRow != null)
            {
                int rowIndex = dgv_param_show.CurrentRow.Index;
                dgv_param_show.Rows[rowIndex].Cells[1].Value = tx_freq.Text;
                dgv_param_show.Rows[rowIndex].Cells[2].Value = tx_lower.Text;
                dgv_param_show.Rows[rowIndex].Cells[3].Value = tx_up.Text;
                dgv_param_show.Rows[rowIndex].Cells[4].Value = tx_uncertainty.Text;
                dgv_param_show.Rows[rowIndex].Cells[5].Value = tx_difference.Text;


                ////这里需要更新LIST
                // 获取当前行的索引,就是key的值
                int currentRowIndex = dgv_xml_show.CurrentRow.Index+1;

                if (configDict.ContainsKey(currentRowIndex))
                {
                    List<MarkerData> updateFreq = configDict[currentRowIndex];

                    DataGridViewRow row = dgv_param_show.Rows[rowIndex]; //获取当前行数据
                    
                    MarkerData MarkerData = new MarkerData();
                    MarkerData.Id = int.Parse(row.Cells["detail_ID"].Value.ToString());
                    MarkerData.Frequency = double.Parse(row.Cells["Frequency"].Value.ToString());
                    MarkerData.LossLowerLimite = double.Parse(row.Cells["Loss_LowerLimit"].Value.ToString());
                    MarkerData.LossUpperLimite = double.Parse(row.Cells["Loss_UpperLimit"].Value.ToString());
                    MarkerData.Uncertainty = double.Parse(row.Cells["Uncertainty"].Value.ToString());
                    MarkerData.Difference = double.Parse(row.Cells["Difference"].Value.ToString());

                    updateFreq[rowIndex] = MarkerData;
                }
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            if (dgv_param_show.CurrentRow != null)
            {
                int rowIndex = dgv_param_show.CurrentRow.Index;
                tx_freq.Text = dgv_param_show.Rows[rowIndex].Cells[1].Value.ToString();
                tx_lower.Text = dgv_param_show.Rows[rowIndex].Cells[2].Value.ToString();
                tx_up.Text = dgv_param_show.Rows[rowIndex].Cells[3].Value.ToString();
                tx_uncertainty.Text = dgv_param_show.Rows[rowIndex].Cells[4].Value.ToString();
                tx_difference.Text = dgv_param_show.Rows[rowIndex].Cells[5].Value.ToString();
            }
        }

        //更新频率点limit
        private void update_freq_limit()
        {
            int rowIndex = dgv_param_show.CurrentRow.Index;
            tx_freq.Text = dgv_param_show.Rows[rowIndex].Cells[1].Value.ToString();
            tx_lower.Text = dgv_param_show.Rows[rowIndex].Cells[2].Value.ToString();
            tx_up.Text = dgv_param_show.Rows[rowIndex].Cells[3].Value.ToString();
            tx_uncertainty.Text = dgv_param_show.Rows[rowIndex].Cells[4].Value.ToString();
            tx_difference.Text = dgv_param_show.Rows[rowIndex].Cells[5].Value.ToString();
        }

        private void dgv_xml_show_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                //说明还没有数据
                if (configDict.ContainsKey(e.RowIndex + 1) == false)
                {

                    if (dgv_param_show.RowCount == 0)
                    {
                        return;
                    }

                    List<MarkerData> newList = new List<MarkerData>();
                    foreach (var MarkerData in configDict[1])
                    {
                        var newMarkerData = new MarkerData();
                        newMarkerData.Id = e.RowIndex + 1;
                        newMarkerData.Frequency = MarkerData.Frequency;
                        //newMarkerData.LossLowerLimite = MarkerData.LossLowerLimite;
                        //newMarkerData.LossUpperLimite = MarkerData.LossUpperLimite;
                        //newMarkerData.Uncertainty = MarkerData.Uncertainty;
                        //newMarkerData.Difference = MarkerData.Difference;
                        newList.Add(newMarkerData);
                    }
                    configDict[e.RowIndex + 1] = newList;
                }
                else
                {
                    // 如果 configDict[2] 的元素数量少于 configDict[1]，则进行补齐
                    List<MarkerData> tempValue = configDict[1];
                    int src_index = configDict[1].Count;
                    int cur_index = configDict[e.RowIndex + 1].Count;
                    int countDiff = src_index - cur_index;
                    if (countDiff > 0)
                    {
                        // 在 configDict[2] 中添加 countDiff 个空元素
                        for (int i = cur_index; i < src_index; i++)
                        {
                            configDict[e.RowIndex + 1].Add(tempValue[cur_index]);
                        }
                    }

                }

                // 包含键值为
                var MarkerDatas = configDict[e.RowIndex + 1];
                dgv_param_show.Rows.Clear();
                foreach (var MarkerData in MarkerDatas)
                {
                    dgv_param_show.Rows.Add(MarkerData.Id, MarkerData.Frequency, MarkerData.LossLowerLimite, MarkerData.LossUpperLimite, MarkerData.Uncertainty, MarkerData.Difference);
                }

            }
        }

        //更新频率点第一行数据
        private void update_first_row()
        {
            if (configDict.ContainsKey(1))
            {
                var MarkerDatas = configDict[1];
                dgv_param_show.Rows.Clear();
                foreach (var MarkerData in MarkerDatas)
                {
                    dgv_param_show.Rows.Add(MarkerData.Id, MarkerData.Frequency, MarkerData.LossLowerLimite, MarkerData.LossUpperLimite, MarkerData.Uncertainty, MarkerData.Difference);
                }
            }
        }

        private void set_header_default_color()
        {
            // 恢复列标题行的背景色
            dgv_param_show.EnableHeadersVisualStyles = true;
            dgv_param_show.ColumnHeadersDefaultCellStyle.BackColor = originalHeaderColor;


            if (dgv_param_show.CurrentRow != null)
            {
                // 获取当前行的索引
                int currentRowIndex = dgv_param_show.CurrentRow.Index;

                // 取消选中 DataGridView2 的当前选定行
                dgv_param_show.Rows[currentRowIndex].Selected = false;

                // 将 DataGridView2 的当前选定行所有单元格的边框颜色设置为未选定颜色
                foreach (DataGridViewRow row in dgv_param_show.Rows)
                {
                    row.DefaultCellStyle.SelectionBackColor = dgv_param_show.DefaultCellStyle.BackColor;
                    row.DefaultCellStyle.SelectionForeColor = dgv_param_show.DefaultCellStyle.ForeColor;
                }

            }
        }

        //单点标题事件
        private void dgv_param_show_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            //被选中
            isSelectFreqFlag = true;

            if (dgv_xml_show.Rows.Count > 0)
            {
                // 设置 DataGridView 的第2行第2列为选中状态
                dgv_xml_show.CurrentCell = dgv_xml_show[1, 0];
                update_first_row();
            }



            // 高亮整个标题行
            dgv_param_show.EnableHeadersVisualStyles = false;
            dgv_param_show.ColumnHeadersDefaultCellStyle.BackColor = dgv_param_show.ColumnHeadersDefaultCellStyle.SelectionBackColor;

            // 单独设置标题行的第一列背景色为原来的颜色
            dgv_param_show.Columns[0].HeaderCell.Style.BackColor = originalHeaderColor;
        }


        //丢失焦点事件
        private void dgv_param_show_Leave(object sender, EventArgs e)
        {
            isSelectFreqFlag = false;
            //// 恢复列标题行的背景色
            dgv_param_show.EnableHeadersVisualStyles = true;
            dgv_param_show.ColumnHeadersDefaultCellStyle.BackColor = originalHeaderColor;
        }





        ////////////////////////////////////////////////////
    }//end class
}//end nameplace
