namespace TestDeltaL
{
    partial class DelealParamSet
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DelealParamSet));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsb_create_xml = new System.Windows.Forms.ToolStripButton();
            this.tsb_measure_loadXml = new System.Windows.Forms.ToolStripButton();
            this.tsb_save_xml = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_add_param = new System.Windows.Forms.ToolStripButton();
            this.tsb_copy_param = new System.Windows.Forms.ToolStripButton();
            this.tsb_del_param = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_layer2 = new System.Windows.Forms.ToolStripButton();
            this.tsb_layer3 = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgv_xml_show = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgv_param_show = new System.Windows.Forms.DataGridView();
            this.detail_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Frequency = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Loss_lower_limit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Loss_Upper_limit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Uncertainty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Difference = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Step = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Method = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Layer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Short_Length = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Medium_Length = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Long_Length = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RecordPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaveCurve = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaveImage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_xml_show)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_param_show)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_create_xml,
            this.tsb_measure_loadXml,
            this.tsb_save_xml,
            this.toolStripSeparator1,
            this.tsb_add_param,
            this.tsb_copy_param,
            this.tsb_del_param,
            this.toolStripSeparator2,
            this.tsb_layer2,
            this.tsb_layer3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip1.Size = new System.Drawing.Size(910, 39);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsb_create_xml
            // 
            this.tsb_create_xml.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsb_create_xml.Image = ((System.Drawing.Image)(resources.GetObject("tsb_create_xml.Image")));
            this.tsb_create_xml.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_create_xml.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_create_xml.Name = "tsb_create_xml";
            this.tsb_create_xml.Size = new System.Drawing.Size(108, 36);
            this.tsb_create_xml.Text = "新建档案";
            this.tsb_create_xml.Click += new System.EventHandler(this.tsb_create_xml_Click);
            // 
            // tsb_measure_loadXml
            // 
            this.tsb_measure_loadXml.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsb_measure_loadXml.Image = ((System.Drawing.Image)(resources.GetObject("tsb_measure_loadXml.Image")));
            this.tsb_measure_loadXml.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_measure_loadXml.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_measure_loadXml.Name = "tsb_measure_loadXml";
            this.tsb_measure_loadXml.Size = new System.Drawing.Size(76, 36);
            this.tsb_measure_loadXml.Text = "载入";
            // 
            // tsb_save_xml
            // 
            this.tsb_save_xml.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsb_save_xml.Image = ((System.Drawing.Image)(resources.GetObject("tsb_save_xml.Image")));
            this.tsb_save_xml.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_save_xml.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_save_xml.Name = "tsb_save_xml";
            this.tsb_save_xml.Size = new System.Drawing.Size(76, 36);
            this.tsb_save_xml.Text = "保存";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // tsb_add_param
            // 
            this.tsb_add_param.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsb_add_param.Image = ((System.Drawing.Image)(resources.GetObject("tsb_add_param.Image")));
            this.tsb_add_param.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_add_param.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_add_param.Name = "tsb_add_param";
            this.tsb_add_param.Size = new System.Drawing.Size(76, 36);
            this.tsb_add_param.Text = "新增";
            // 
            // tsb_copy_param
            // 
            this.tsb_copy_param.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsb_copy_param.Image = ((System.Drawing.Image)(resources.GetObject("tsb_copy_param.Image")));
            this.tsb_copy_param.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_copy_param.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_copy_param.Name = "tsb_copy_param";
            this.tsb_copy_param.Size = new System.Drawing.Size(76, 36);
            this.tsb_copy_param.Text = "复制";
            // 
            // tsb_del_param
            // 
            this.tsb_del_param.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsb_del_param.Image = ((System.Drawing.Image)(resources.GetObject("tsb_del_param.Image")));
            this.tsb_del_param.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_del_param.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_del_param.Name = "tsb_del_param";
            this.tsb_del_param.Size = new System.Drawing.Size(76, 36);
            this.tsb_del_param.Text = "删除";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // tsb_layer2
            // 
            this.tsb_layer2.Image = ((System.Drawing.Image)(resources.GetObject("tsb_layer2.Image")));
            this.tsb_layer2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_layer2.Name = "tsb_layer2";
            this.tsb_layer2.Size = new System.Drawing.Size(49, 36);
            this.tsb_layer2.Text = "2L";
            // 
            // tsb_layer3
            // 
            this.tsb_layer3.Image = ((System.Drawing.Image)(resources.GetObject("tsb_layer3.Image")));
            this.tsb_layer3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_layer3.Name = "tsb_layer3";
            this.tsb_layer3.Size = new System.Drawing.Size(49, 36);
            this.tsb_layer3.Text = "3L";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 39);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgv_xml_show);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.dgv_param_show);
            this.splitContainer1.Size = new System.Drawing.Size(910, 549);
            this.splitContainer1.SplitterDistance = 280;
            this.splitContainer1.TabIndex = 2;
            // 
            // dgv_xml_show
            // 
            this.dgv_xml_show.AllowUserToAddRows = false;
            this.dgv_xml_show.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_xml_show.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Step,
            this.Method,
            this.Layer,
            this.Description,
            this.Short_Length,
            this.Medium_Length,
            this.Long_Length,
            this.RecordPath,
            this.SaveCurve,
            this.SaveImage});
            this.dgv_xml_show.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_xml_show.Location = new System.Drawing.Point(0, 0);
            this.dgv_xml_show.Name = "dgv_xml_show";
            this.dgv_xml_show.ReadOnly = true;
            this.dgv_xml_show.RowTemplate.Height = 23;
            this.dgv_xml_show.Size = new System.Drawing.Size(910, 280);
            this.dgv_xml_show.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(0, 179);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(907, 86);
            this.panel1.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox5);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.textBox6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(488, 1);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(208, 80);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Other Limig";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(88, 49);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(100, 21);
            this.textBox5.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "Uncertainty";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(88, 22);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(100, 21);
            this.textBox6.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "Difference";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox3);
            this.groupBox2.Controls.Add(this.textBox4);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(244, 1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(208, 80);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Loss Limit";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(89, 49);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 21);
            this.textBox3.TabIndex = 7;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(89, 22);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 21);
            this.textBox4.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "Lower";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "Upper";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(0, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(208, 80);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Marker Setting";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(84, 49);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 21);
            this.textBox2.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(84, 22);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Frequency";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "测试编号";
            // 
            // dgv_param_show
            // 
            this.dgv_param_show.AllowUserToAddRows = false;
            this.dgv_param_show.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_param_show.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_param_show.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.detail_ID,
            this.Frequency,
            this.Loss_lower_limit,
            this.Loss_Upper_limit,
            this.Uncertainty,
            this.Difference});
            this.dgv_param_show.Location = new System.Drawing.Point(0, 3);
            this.dgv_param_show.Name = "dgv_param_show";
            this.dgv_param_show.ReadOnly = true;
            this.dgv_param_show.RowTemplate.Height = 23;
            this.dgv_param_show.Size = new System.Drawing.Size(910, 170);
            this.dgv_param_show.TabIndex = 0;
            // 
            // detail_ID
            // 
            this.detail_ID.HeaderText = "ID";
            this.detail_ID.Name = "detail_ID";
            this.detail_ID.ReadOnly = true;
            // 
            // Frequency
            // 
            this.Frequency.HeaderText = "Frequency";
            this.Frequency.Name = "Frequency";
            this.Frequency.ReadOnly = true;
            // 
            // Loss_lower_limit
            // 
            this.Loss_lower_limit.HeaderText = "Loss lower limit";
            this.Loss_lower_limit.Name = "Loss_lower_limit";
            this.Loss_lower_limit.ReadOnly = true;
            this.Loss_lower_limit.Width = 115;
            // 
            // Loss_Upper_limit
            // 
            this.Loss_Upper_limit.HeaderText = "Loss Upper limit";
            this.Loss_Upper_limit.Name = "Loss_Upper_limit";
            this.Loss_Upper_limit.ReadOnly = true;
            // 
            // Uncertainty
            // 
            this.Uncertainty.HeaderText = "Uncertainty(%)";
            this.Uncertainty.Name = "Uncertainty";
            this.Uncertainty.ReadOnly = true;
            // 
            // Difference
            // 
            this.Difference.HeaderText = "Difference";
            this.Difference.Name = "Difference";
            this.Difference.ReadOnly = true;
            // 
            // ID
            // 
            this.ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Width = 42;
            // 
            // Step
            // 
            this.Step.HeaderText = "Step";
            this.Step.Name = "Step";
            this.Step.ReadOnly = true;
            // 
            // Method
            // 
            this.Method.HeaderText = "Method";
            this.Method.Name = "Method";
            this.Method.ReadOnly = true;
            // 
            // Layer
            // 
            this.Layer.HeaderText = "Layer";
            this.Layer.Name = "Layer";
            this.Layer.ReadOnly = true;
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            // 
            // Short_Length
            // 
            this.Short_Length.HeaderText = "Short_Length";
            this.Short_Length.Name = "Short_Length";
            this.Short_Length.ReadOnly = true;
            // 
            // Medium_Length
            // 
            this.Medium_Length.HeaderText = "Medium_Length";
            this.Medium_Length.Name = "Medium_Length";
            this.Medium_Length.ReadOnly = true;
            // 
            // Long_Length
            // 
            this.Long_Length.HeaderText = "Long_Length";
            this.Long_Length.Name = "Long_Length";
            this.Long_Length.ReadOnly = true;
            // 
            // RecordPath
            // 
            this.RecordPath.HeaderText = "RecordPath";
            this.RecordPath.Name = "RecordPath";
            this.RecordPath.ReadOnly = true;
            // 
            // SaveCurve
            // 
            this.SaveCurve.HeaderText = "SaveCurve";
            this.SaveCurve.Name = "SaveCurve";
            this.SaveCurve.ReadOnly = true;
            // 
            // SaveImage
            // 
            this.SaveImage.HeaderText = "SaveImage";
            this.SaveImage.Name = "SaveImage";
            this.SaveImage.ReadOnly = true;
            // 
            // DelealParamSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 588);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "DelealParamSet";
            this.Text = "Delea L参数设置";
            this.Load += new System.EventHandler(this.DelealParamSet_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_xml_show)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_param_show)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsb_create_xml;
        private System.Windows.Forms.ToolStripButton tsb_measure_loadXml;
        private System.Windows.Forms.ToolStripButton tsb_save_xml;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsb_add_param;
        private System.Windows.Forms.ToolStripButton tsb_copy_param;
        private System.Windows.Forms.ToolStripButton tsb_del_param;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsb_layer2;
        private System.Windows.Forms.ToolStripButton tsb_layer3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgv_xml_show;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgv_param_show;
        private System.Windows.Forms.DataGridViewTextBoxColumn detail_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Frequency;
        private System.Windows.Forms.DataGridViewTextBoxColumn Loss_lower_limit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Loss_Upper_limit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Uncertainty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Difference;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Step;
        private System.Windows.Forms.DataGridViewTextBoxColumn Method;
        private System.Windows.Forms.DataGridViewTextBoxColumn Layer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Short_Length;
        private System.Windows.Forms.DataGridViewTextBoxColumn Medium_Length;
        private System.Windows.Forms.DataGridViewTextBoxColumn Long_Length;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecordPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaveCurve;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaveImage;
    }
}