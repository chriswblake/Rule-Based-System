namespace Ch8_RuleBasedSystem
{
    partial class Form1
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
            System.Windows.Forms.Label labShowEvents;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label labTimeLength;
            this.tablePanel = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tbRules = new System.Windows.Forms.TextBox();
            this.panelControls = new System.Windows.Forms.Panel();
            this.tableControls = new System.Windows.Forms.TableLayoutPanel();
            this.chbShowRulesFiring = new System.Windows.Forms.CheckBox();
            this.chbMemoryChanges = new System.Windows.Forms.CheckBox();
            this.tbRulesAddress = new System.Windows.Forms.TextBox();
            this.btnLoadRules = new System.Windows.Forms.Button();
            this.btnProcess = new System.Windows.Forms.Button();
            this.chbVsTime = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tbWorkingMemory = new System.Windows.Forms.TextBox();
            this.tbTimeLength = new System.Windows.Forms.TextBox();
            labShowEvents = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            labTimeLength = new System.Windows.Forms.Label();
            this.tablePanel.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panelControls.SuspendLayout();
            this.tableControls.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labShowEvents
            // 
            labShowEvents.AutoSize = true;
            this.tableControls.SetColumnSpan(labShowEvents, 2);
            labShowEvents.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            labShowEvents.Location = new System.Drawing.Point(3, 0);
            labShowEvents.Name = "labShowEvents";
            labShowEvents.Size = new System.Drawing.Size(184, 31);
            labShowEvents.TabIndex = 1;
            labShowEvents.Text = "Show Events";
            // 
            // label2
            // 
            label2.AutoSize = true;
            this.tableControls.SetColumnSpan(label2, 2);
            label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label2.Location = new System.Drawing.Point(3, 193);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(89, 31);
            label2.TabIndex = 5;
            label2.Text = "Rules";
            // 
            // label1
            // 
            label1.AutoSize = true;
            this.tableControls.SetColumnSpan(label1, 2);
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.Location = new System.Drawing.Point(3, 285);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(104, 31);
            label1.TabIndex = 8;
            label1.Text = "Engine";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(3, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(188, 25);
            label3.TabIndex = 3;
            label3.Text = "Working Memory";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(3, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(157, 25);
            label4.TabIndex = 3;
            label4.Text = "Current Rules";
            // 
            // tablePanel
            // 
            this.tablePanel.ColumnCount = 3;
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tablePanel.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tablePanel.Controls.Add(this.panelControls, 0, 0);
            this.tablePanel.Controls.Add(this.tableLayoutPanel1, 2, 0);
            this.tablePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel.Location = new System.Drawing.Point(0, 0);
            this.tablePanel.Name = "tablePanel";
            this.tablePanel.RowCount = 1;
            this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tablePanel.Size = new System.Drawing.Size(1382, 816);
            this.tablePanel.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tbRules, 0, 1);
            this.tableLayoutPanel2.Controls.Add(label4, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel2.Location = new System.Drawing.Point(303, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(535, 810);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // tbRules
            // 
            this.tbRules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbRules.Location = new System.Drawing.Point(3, 28);
            this.tbRules.Multiline = true;
            this.tbRules.Name = "tbRules";
            this.tbRules.ReadOnly = true;
            this.tbRules.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbRules.Size = new System.Drawing.Size(529, 779);
            this.tbRules.TabIndex = 2;
            // 
            // panelControls
            // 
            this.panelControls.Controls.Add(this.tableControls);
            this.panelControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControls.Location = new System.Drawing.Point(3, 3);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(294, 810);
            this.panelControls.TabIndex = 0;
            // 
            // tableControls
            // 
            this.tableControls.AutoSize = true;
            this.tableControls.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableControls.ColumnCount = 2;
            this.tableControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableControls.Controls.Add(this.chbShowRulesFiring, 0, 2);
            this.tableControls.Controls.Add(labShowEvents, 0, 0);
            this.tableControls.Controls.Add(this.chbMemoryChanges, 0, 3);
            this.tableControls.Controls.Add(this.tbRulesAddress, 0, 7);
            this.tableControls.Controls.Add(label2, 0, 6);
            this.tableControls.Controls.Add(this.btnLoadRules, 1, 7);
            this.tableControls.Controls.Add(label1, 0, 9);
            this.tableControls.Controls.Add(this.btnProcess, 0, 10);
            this.tableControls.Controls.Add(this.chbVsTime, 0, 1);
            this.tableControls.Controls.Add(this.tbTimeLength, 1, 4);
            this.tableControls.Controls.Add(labTimeLength, 0, 4);
            this.tableControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableControls.Location = new System.Drawing.Point(0, 0);
            this.tableControls.Name = "tableControls";
            this.tableControls.RowCount = 12;
            this.tableControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableControls.Size = new System.Drawing.Size(294, 377);
            this.tableControls.TabIndex = 1;
            // 
            // chbShowRulesFiring
            // 
            this.chbShowRulesFiring.AutoSize = true;
            this.chbShowRulesFiring.Checked = true;
            this.chbShowRulesFiring.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tableControls.SetColumnSpan(this.chbShowRulesFiring, 2);
            this.chbShowRulesFiring.Location = new System.Drawing.Point(3, 69);
            this.chbShowRulesFiring.Name = "chbShowRulesFiring";
            this.chbShowRulesFiring.Size = new System.Drawing.Size(159, 29);
            this.chbShowRulesFiring.TabIndex = 0;
            this.chbShowRulesFiring.Text = "Rules Firing";
            this.chbShowRulesFiring.UseVisualStyleBackColor = true;
            // 
            // chbMemoryChanges
            // 
            this.chbMemoryChanges.AutoSize = true;
            this.chbMemoryChanges.Checked = true;
            this.chbMemoryChanges.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbMemoryChanges.Location = new System.Drawing.Point(3, 104);
            this.chbMemoryChanges.Name = "chbMemoryChanges";
            this.chbMemoryChanges.Size = new System.Drawing.Size(213, 29);
            this.chbMemoryChanges.TabIndex = 2;
            this.chbMemoryChanges.Text = "Memory Changes";
            this.chbMemoryChanges.UseVisualStyleBackColor = true;
            // 
            // tbRulesAddress
            // 
            this.tbRulesAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbRulesAddress.Location = new System.Drawing.Point(3, 227);
            this.tbRulesAddress.Name = "tbRulesAddress";
            this.tbRulesAddress.ReadOnly = true;
            this.tbRulesAddress.Size = new System.Drawing.Size(213, 31);
            this.tbRulesAddress.TabIndex = 4;
            this.tbRulesAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbRulesAddress.TextChanged += new System.EventHandler(this.tbRulesAddress_TextChanged);
            // 
            // btnLoadRules
            // 
            this.btnLoadRules.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnLoadRules.Location = new System.Drawing.Point(222, 227);
            this.btnLoadRules.Name = "btnLoadRules";
            this.btnLoadRules.Size = new System.Drawing.Size(60, 35);
            this.btnLoadRules.TabIndex = 6;
            this.btnLoadRules.Text = "...";
            this.btnLoadRules.UseVisualStyleBackColor = true;
            this.btnLoadRules.Click += new System.EventHandler(this.btnLoadRules_Click);
            // 
            // btnProcess
            // 
            this.btnProcess.AutoSize = true;
            this.btnProcess.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnProcess.Enabled = false;
            this.btnProcess.Location = new System.Drawing.Point(3, 319);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(161, 35);
            this.btnProcess.TabIndex = 7;
            this.btnProcess.Text = "Process Rules";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // chbVsTime
            // 
            this.chbVsTime.AutoSize = true;
            this.chbVsTime.Checked = true;
            this.chbVsTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tableControls.SetColumnSpan(this.chbVsTime, 2);
            this.chbVsTime.Location = new System.Drawing.Point(3, 34);
            this.chbVsTime.Name = "chbVsTime";
            this.chbVsTime.Size = new System.Drawing.Size(122, 29);
            this.chbVsTime.TabIndex = 9;
            this.chbVsTime.Text = "Vs Time";
            this.chbVsTime.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tbWorkingMemory, 0, 1);
            this.tableLayoutPanel1.Controls.Add(label3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(844, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(535, 810);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // tbWorkingMemory
            // 
            this.tbWorkingMemory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbWorkingMemory.Location = new System.Drawing.Point(3, 28);
            this.tbWorkingMemory.Multiline = true;
            this.tbWorkingMemory.Name = "tbWorkingMemory";
            this.tbWorkingMemory.ReadOnly = true;
            this.tbWorkingMemory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbWorkingMemory.Size = new System.Drawing.Size(529, 779);
            this.tbWorkingMemory.TabIndex = 2;
            this.tbWorkingMemory.TextChanged += new System.EventHandler(this.tbAutoScrollToEnd_TextChanged);
            // 
            // tbTimeLength
            // 
            this.tbTimeLength.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbTimeLength.Location = new System.Drawing.Point(222, 139);
            this.tbTimeLength.MaxLength = 4;
            this.tbTimeLength.Name = "tbTimeLength";
            this.tbTimeLength.Size = new System.Drawing.Size(60, 31);
            this.tbTimeLength.TabIndex = 10;
            this.tbTimeLength.Text = "100";
            // 
            // labTimeLength
            // 
            labTimeLength.AutoSize = true;
            labTimeLength.Dock = System.Windows.Forms.DockStyle.Right;
            labTimeLength.Location = new System.Drawing.Point(31, 136);
            labTimeLength.Name = "labTimeLength";
            labTimeLength.Size = new System.Drawing.Size(185, 37);
            labTimeLength.TabIndex = 11;
            labTimeLength.Text = "Time Length (ms):";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1382, 816);
            this.Controls.Add(this.tablePanel);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Ch 8 - Rule Based Systems";
            this.tablePanel.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panelControls.ResumeLayout(false);
            this.panelControls.PerformLayout();
            this.tableControls.ResumeLayout(false);
            this.tableControls.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tablePanel;
        private System.Windows.Forms.Panel panelControls;
        private System.Windows.Forms.TableLayoutPanel tableControls;
        private System.Windows.Forms.CheckBox chbShowRulesFiring;
        private System.Windows.Forms.CheckBox chbMemoryChanges;
        private System.Windows.Forms.TextBox tbRulesAddress;
        private System.Windows.Forms.Button btnLoadRules;
        private System.Windows.Forms.TextBox tbWorkingMemory;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox tbRules;
        private System.Windows.Forms.CheckBox chbVsTime;
        private System.Windows.Forms.TextBox tbTimeLength;
    }
}

