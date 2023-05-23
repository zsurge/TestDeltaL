
namespace TestDeltaL
{
    partial class DevConnectSet
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.combDevString = new System.Windows.Forms.ComboBox();
            this.btn_ConnectDev = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.combDevType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(75, 51);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(443, 21);
            this.textBox1.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "料号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "设备型号";
            // 
            // combDevString
            // 
            this.combDevString.FormattingEnabled = true;
            this.combDevString.Location = new System.Drawing.Point(261, 21);
            this.combDevString.Name = "combDevString";
            this.combDevString.Size = new System.Drawing.Size(257, 20);
            this.combDevString.TabIndex = 11;
            // 
            // btn_ConnectDev
            // 
            this.btn_ConnectDev.Location = new System.Drawing.Point(536, 19);
            this.btn_ConnectDev.Name = "btn_ConnectDev";
            this.btn_ConnectDev.Size = new System.Drawing.Size(75, 23);
            this.btn_ConnectDev.TabIndex = 10;
            this.btn_ConnectDev.Text = "连接仪器";
            this.btn_ConnectDev.UseVisualStyleBackColor = true;
            this.btn_ConnectDev.Click += new System.EventHandler(this.btn_ConnectDev_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(196, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "VISA位置:";
            // 
            // combDevType
            // 
            this.combDevType.FormattingEnabled = true;
            this.combDevType.Items.AddRange(new object[] {
            "E5080B 4-port",
            "E5080B 2-port",
            "E5071C 4-port",
            "E5071C 2-port",
            "E5063A",
            "PNA",
            "USB-ENA"});
            this.combDevType.Location = new System.Drawing.Point(75, 21);
            this.combDevType.Name = "combDevType";
            this.combDevType.Size = new System.Drawing.Size(102, 20);
            this.combDevType.TabIndex = 8;
            this.combDevType.Text = "E5063A";
            // 
            // DevConnectSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 304);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.combDevString);
            this.Controls.Add(this.btn_ConnectDev);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.combDevType);
            this.Name = "DevConnectSet";
            this.Text = "DevConnectSet";
            this.Load += new System.EventHandler(this.DevConnectSet_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox combDevString;
        private System.Windows.Forms.Button btn_ConnectDev;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox combDevType;
    }
}