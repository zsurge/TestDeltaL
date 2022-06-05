using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace TestDeltaL
{
    public partial class Form1 : Form
    {


        //设置参数设置窗体的表数据
        DataTable gdt;

        public Form1()
        {
            InitializeComponent();
        }

        private void deltaLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            tdrToolStripMenuItem.Checked = false;
            deltaLToolStripMenuItem.Checked = true;

            deltaLToolStripMenuItem.CheckState = CheckState.Checked;
            DialogResult dr = MessageBox.Show("量测数据将被清空，是否确定切换量测项目?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                tsb_GetTestIndex.Visible = false;
                tsb_StartTest.Visible = false;
                PubConfig.gDeviceType = PubConfig.DeviceType.DELTAL;        
            }
            else
            {

            }
        }

        private void tdrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tdrToolStripMenuItem.Checked = true;
            deltaLToolStripMenuItem.Checked = false;

            tdrToolStripMenuItem.CheckState = CheckState.Checked;
            DialogResult dr = MessageBox.Show("量测数据将被清空，是否确定切换量测项目?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                tsb_GetTestIndex.Visible = true;
                tsb_StartTest.Visible = true;
                PubConfig.gDeviceType = PubConfig.DeviceType.TDR;
            }
            else
            {

            }
        }

        public void initChart(Chart myChart)
        {
            foreach (var series in myChart.Series)
            {
                series.Points.Clear();
            }

            myChart.Dock = DockStyle.Fill;

            myChart.Series[0].LegendText = "TDR Curve";
            myChart.Series[1].LegendText = "limit";
            myChart.Series[2].LegendText = "limit";
            myChart.Series[3].LegendText = "100";
            myChart.Series[0].ChartType = SeriesChartType.Spline;
            myChart.Series[0].BorderWidth = 2;
            myChart.Series[1].BorderWidth = 2;
            myChart.Series[2].BorderWidth = 2;
            myChart.Series[3].BorderWidth = 2;

            //myChart.Series[0].IsVisibleInLegend = false;
            //myChart.Series[0].LegendText = "TDR Curve";

            myChart.Series[0].ChartType = SeriesChartType.Spline;


            myChart.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false; //仅不显示x轴方向的网格线
            myChart.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false; //仅不显示y轴方向的网格线

            //背景灰色
            myChart.BackColor = Color.Gray;
            myChart.ChartAreas[0].BackColor = Color.Gray;

            //XY标签改为白色
            myChart.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.White;
            myChart.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.White;

            //X，Y轴白色
            myChart.ChartAreas[0].AxisX.LineColor = Color.White;
            myChart.ChartAreas[0].AxisY.LineColor = Color.White;

            //刻度值颜色跟背景一致，就无法显示了
            myChart.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Gray;//刻度值颜色
            myChart.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Gray;//刻度值颜色

            //条黄色
            myChart.Series[0].Color = Color.Yellow; //线条颜色

            //网格线颜色白色
            myChart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
            myChart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;

            ////设置默认网格间距
            //myChart.ChartAreas[0].AxisX.Interval = 10;//X轴间距
            myChart.ChartAreas[0].AxisX.Maximum = 3000; //设置X坐标最大值
            myChart.ChartAreas[0].AxisX.Minimum = -3000;//设置X坐标最小值

            //myChart.ChartAreas[0].AxisY.Interval = 10;//Y轴间距
            myChart.ChartAreas[0].AxisY.Maximum = 3000;//设置Y坐标最大值
            myChart.ChartAreas[0].AxisY.Minimum = -3000;

            myChart.Series[0].Points.AddXY(10, 10);

            StripLine stripLineLimit = new StripLine();
            stripLineLimit.Text = "";
            stripLineLimit.Interval = 0;
            stripLineLimit.StripWidth = 0.1;
            stripLineLimit.Text = "Uncertainty Report(L1-L2)";
            stripLineLimit.IntervalOffset = 0;
            stripLineLimit.BackColor = Color.White;
            stripLineLimit.BorderDashStyle = ChartDashStyle.Dash;
            myChart.ChartAreas[0].AxisX.StripLines.Add(stripLineLimit);

            StripLine stripLineLimitY = new StripLine();
            stripLineLimitY.Text = "";
            stripLineLimitY.Interval = 0;
            stripLineLimitY.StripWidth = 0.1;
            stripLineLimitY.Text = "Insertion Loss per Inch(dB)";
            stripLineLimitY.IntervalOffset = 0;
            stripLineLimitY.BackColor = Color.White;
            stripLineLimitY.BorderDashStyle = ChartDashStyle.Dash;
            myChart.ChartAreas[0].AxisY.StripLines.Add(stripLineLimitY);

            //CustomLabel label = new CustomLabel();
            //label.Text = "0";
            //label.ToPosition = 0;
            //myChart.ChartAreas[0].AxisX.CustomLabels.Add(label);

            //CustomLabel labely = new CustomLabel();
            //labely.Text = "0";
            //labely.ToPosition = 0;
            //myChart.ChartAreas[0].AxisY.CustomLabels.Add(labely);
        }

        public void initChart_3layer(Chart myChart,string showText,string xText,string yText)
        {
            foreach (var series in myChart.Series)
            {
                series.Points.Clear();
            }

            myChart.Dock = DockStyle.Fill;
            myChart.Series[0].LegendText = showText;
            myChart.Series[0].ChartType = SeriesChartType.Spline;
            myChart.Series[0].BorderWidth = 2;
     

            //myChart.Series[0].IsVisibleInLegend = false;
            //myChart.Series[0].LegendText = "TDR Curve";

            myChart.Series[0].ChartType = SeriesChartType.Spline;


            myChart.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false; //仅不显示x轴方向的网格线
            myChart.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false; //仅不显示y轴方向的网格线

            //背景灰色
            myChart.BackColor = Color.Gray;
            myChart.ChartAreas[0].BackColor = Color.Gray;

            //XY标签改为白色
            myChart.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.White;
            myChart.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.White;

            //X，Y轴白色
            myChart.ChartAreas[0].AxisX.LineColor = Color.White;
            myChart.ChartAreas[0].AxisY.LineColor = Color.White;

            //刻度值颜色跟背景一致，就无法显示了
            myChart.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Gray;//刻度值颜色
            myChart.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Gray;//刻度值颜色

            //条黄色
            myChart.Series[0].Color = Color.Yellow; //线条颜色

            //网格线颜色白色
            myChart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
            myChart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;

            ////设置默认网格间距
            //myChart.ChartAreas[0].AxisX.Interval = 10;//X轴间距
            myChart.ChartAreas[0].AxisX.Maximum = 3000; //设置X坐标最大值
            myChart.ChartAreas[0].AxisX.Minimum = -3000;//设置X坐标最小值

            //myChart.ChartAreas[0].AxisY.Interval = 10;//Y轴间距
            myChart.ChartAreas[0].AxisY.Maximum = 3000;//设置Y坐标最大值
            myChart.ChartAreas[0].AxisY.Minimum = -3000;

            myChart.Series[0].Points.AddXY(10, 10);

            StripLine stripLineLimit = new StripLine();
            stripLineLimit.Text = "";
            stripLineLimit.Interval = 0;
            stripLineLimit.StripWidth = 0.1;
            stripLineLimit.Text = xText;
            stripLineLimit.IntervalOffset = 0;
            stripLineLimit.BackColor = Color.White;
            stripLineLimit.BorderDashStyle = ChartDashStyle.Dash;
            myChart.ChartAreas[0].AxisX.StripLines.Add(stripLineLimit);

            StripLine stripLineLimitY = new StripLine();
            stripLineLimitY.Text = "";
            stripLineLimitY.Interval = 0;
            stripLineLimitY.StripWidth = 0.1;
            stripLineLimitY.Text = yText;
            stripLineLimitY.IntervalOffset = 0;
            stripLineLimitY.BackColor = Color.White;
            stripLineLimitY.BorderDashStyle = ChartDashStyle.Dash;
            myChart.ChartAreas[0].AxisY.StripLines.Add(stripLineLimitY);

            //CustomLabel label = new CustomLabel();
            //label.Text = "0";
            //label.ToPosition = 0;
            //myChart.ChartAreas[0].AxisX.CustomLabels.Add(label);

            //CustomLabel labely = new CustomLabel();
            //labely.Text = "0";
            //labely.ToPosition = 0;
            //myChart.ChartAreas[0].AxisY.CustomLabels.Add(labely);
        }

        private void tsb_DevParamSet_Click(object sender, EventArgs e)
        {
            //暂时不显示，显示时，需设置dock 为 fill
            //splitContainer3.Visible = false;
            //splitContainer4.Visible = false;

            if (PubConfig.gDeviceType == PubConfig.DeviceType.TDR)
            {
                TdrParamSet devParamSet = new TdrParamSet(gdt);
                devParamSet.ChangeDgv += new TdrParamSet.ChangeDgvHandler(Change_DataGridView);
                devParamSet.Show();
            }
            else if (PubConfig.gDeviceType == PubConfig.DeviceType.DELTAL)
            {
                DelealParamSet devParamSet = new DelealParamSet(gdt);
                devParamSet.ChangeDgv += new DelealParamSet.ChangeDgvHandler(Change_DataGridView);
                devParamSet.Show();
            }
        }

        private void tsb_DevPOptSet_Click(object sender, EventArgs e)
        {
            //暂时不显示，显示时，需设置dock 为 fill
            Chart_Tdr.Visible = false;
            splitContainer3.Visible = true;
            splitContainer4.Visible = true;
            splitContainer3.Dock = DockStyle.Fill;
            splitContainer4.Dock = DockStyle.Fill;


            initChart_3layer(chart_short_medium,"short","short_medium", "Insertion Loss per Inch(dB)");
            initChart_3layer(chart_medium_long, "medium", "medium_long", "Insertion Loss per Inch(dB)");
            initChart_3layer(chart_difference, "difference", "difference", "Error Percentage");

        }
        public void Change_DataGridView(DataGridView dt)
        {
            if (dgv_CurrentResult.Rows.Count > 0)
            {
                DialogResult dr = MessageBox.Show("将要清除测试数据，是否保存", "提示", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    //DataGridViewToExcel(dgv_CurrentResult);
                    MessageBox.Show("123");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            initChart(Chart_Tdr);
        }


    }
}
