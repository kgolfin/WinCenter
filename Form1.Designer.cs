namespace WinCenter
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.buttonWindowCenter = new System.Windows.Forms.Button();
            this.textBoxWndowName = new System.Windows.Forms.TextBox();
            this.checkBoxAlwaysOnTop = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonProcessName = new System.Windows.Forms.RadioButton();
            this.radioButtonWindowName = new System.Windows.Forms.RadioButton();
            this.buttonReset = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonWindowCenter
            // 
            this.buttonWindowCenter.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.buttonWindowCenter.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonWindowCenter.Location = new System.Drawing.Point(6, 23);
            this.buttonWindowCenter.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonWindowCenter.Name = "buttonWindowCenter";
            this.buttonWindowCenter.Size = new System.Drawing.Size(75, 29);
            this.buttonWindowCenter.TabIndex = 0;
            this.buttonWindowCenter.Text = "Win Center";
            this.buttonWindowCenter.UseVisualStyleBackColor = true;
            this.buttonWindowCenter.Click += new System.EventHandler(this.buttonWindowCenter_Click);
            // 
            // textBoxWndowName
            // 
            this.textBoxWndowName.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.textBoxWndowName.Location = new System.Drawing.Point(96, 27);
            this.textBoxWndowName.Name = "textBoxWndowName";
            this.textBoxWndowName.Size = new System.Drawing.Size(221, 23);
            this.textBoxWndowName.TabIndex = 1;
            this.textBoxWndowName.Text = "vprogram.exe*";
            // 
            // checkBoxAlwaysOnTop
            // 
            this.checkBoxAlwaysOnTop.AutoSize = true;
            this.checkBoxAlwaysOnTop.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.checkBoxAlwaysOnTop.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.checkBoxAlwaysOnTop.Location = new System.Drawing.Point(469, 8);
            this.checkBoxAlwaysOnTop.Name = "checkBoxAlwaysOnTop";
            this.checkBoxAlwaysOnTop.Size = new System.Drawing.Size(107, 19);
            this.checkBoxAlwaysOnTop.TabIndex = 2;
            this.checkBoxAlwaysOnTop.Text = "All ways on top";
            this.checkBoxAlwaysOnTop.UseVisualStyleBackColor = true;
            this.checkBoxAlwaysOnTop.CheckedChanged += new System.EventHandler(this.checkBoxAlwaysOnTop_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonProcessName);
            this.groupBox1.Controls.Add(this.radioButtonWindowName);
            this.groupBox1.Controls.Add(this.buttonReset);
            this.groupBox1.Controls.Add(this.buttonWindowCenter);
            this.groupBox1.Controls.Add(this.textBoxWndowName);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Location = new System.Drawing.Point(12, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(565, 80);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Center windows";
            // 
            // radioButtonProcessName
            // 
            this.radioButtonProcessName.AutoSize = true;
            this.radioButtonProcessName.Checked = true;
            this.radioButtonProcessName.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.radioButtonProcessName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.radioButtonProcessName.Location = new System.Drawing.Point(344, 51);
            this.radioButtonProcessName.Name = "radioButtonProcessName";
            this.radioButtonProcessName.Size = new System.Drawing.Size(100, 19);
            this.radioButtonProcessName.TabIndex = 3;
            this.radioButtonProcessName.TabStop = true;
            this.radioButtonProcessName.Text = "Process Name";
            this.radioButtonProcessName.UseVisualStyleBackColor = true;
            this.radioButtonProcessName.MouseHover += new System.EventHandler(this.radioButtonProcessName_MouseHover);
            // 
            // radioButtonWindowName
            // 
            this.radioButtonWindowName.AutoSize = true;
            this.radioButtonWindowName.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.radioButtonWindowName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.radioButtonWindowName.Location = new System.Drawing.Point(344, 20);
            this.radioButtonWindowName.Name = "radioButtonWindowName";
            this.radioButtonWindowName.Size = new System.Drawing.Size(105, 19);
            this.radioButtonWindowName.TabIndex = 3;
            this.radioButtonWindowName.Text = "Window Name";
            this.radioButtonWindowName.UseVisualStyleBackColor = true;
            // 
            // buttonReset
            // 
            this.buttonReset.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.buttonReset.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonReset.Location = new System.Drawing.Point(484, 23);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 23);
            this.buttonReset.TabIndex = 2;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 125);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkBoxAlwaysOnTop);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Center windows";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonWindowCenter;
        private System.Windows.Forms.TextBox textBoxWndowName;
        private System.Windows.Forms.CheckBox checkBoxAlwaysOnTop;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.RadioButton radioButtonProcessName;
        private System.Windows.Forms.RadioButton radioButtonWindowName;
    }
}

