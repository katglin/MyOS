namespace MyOS
{
     partial class AddProcess//
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
         System.ComponentModel.IContainer components = null;//

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
        public void InitializeComponent()
        {
            this.inp_process = new System.Windows.Forms.TextBox();
            this.AddPr = new System.Windows.Forms.Button();
            this.AllPr = new System.Windows.Forms.DataGridView();
            this.PrName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Prioritet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.User = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.UserStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.AllPr)).BeginInit();
            this.SuspendLayout();
            // 
            // inp_process
            // 
            this.inp_process.Location = new System.Drawing.Point(172, 26);
            this.inp_process.Name = "inp_process";
            this.inp_process.Size = new System.Drawing.Size(241, 20);
            this.inp_process.TabIndex = 1;
            // 
            // AddPr
            // 
            this.AddPr.Location = new System.Drawing.Point(419, 23);
            this.AddPr.Name = "AddPr";
            this.AddPr.Size = new System.Drawing.Size(72, 26);
            this.AddPr.TabIndex = 2;
            this.AddPr.Text = "Выполнить";
            this.AddPr.UseVisualStyleBackColor = true;
            this.AddPr.Click += new System.EventHandler(this.AddPr_Click);
            // 
            // AllPr
            // 
            this.AllPr.AllowUserToAddRows = false;
            this.AllPr.AllowUserToDeleteRows = false;
            this.AllPr.BackgroundColor = System.Drawing.SystemColors.Control;
            this.AllPr.CausesValidation = false;
            this.AllPr.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.AllPr.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AllPr.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PrName,
            this.Prioritet,
            this.Time,
            this.User});
            this.AllPr.GridColor = System.Drawing.SystemColors.Control;
            this.AllPr.Location = new System.Drawing.Point(46, 65);
            this.AllPr.Name = "AllPr";
            this.AllPr.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.AllPr.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.AllPr.Size = new System.Drawing.Size(445, 244);
            this.AllPr.TabIndex = 0;
            this.AllPr.TabStop = false;
            // 
            // PrName
            // 
            this.PrName.HeaderText = "PrName";
            this.PrName.Name = "PrName";
            // 
            // Prioritet
            // 
            this.Prioritet.HeaderText = "Prioritet";
            this.Prioritet.Name = "Prioritet";
            // 
            // Time
            // 
            this.Time.HeaderText = "Time";
            this.Time.Name = "Time";
            // 
            // User
            // 
            this.User.HeaderText = "User";
            this.User.Name = "User";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(43, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Введите команду:";
            // 
            // UserStatus
            // 
            this.UserStatus.AutoSize = true;
            this.UserStatus.Location = new System.Drawing.Point(-4, 326);
            this.UserStatus.Name = "UserStatus";
            this.UserStatus.Size = new System.Drawing.Size(125, 13);
            this.UserStatus.TabIndex = 6;
            this.UserStatus.Text = "Вы не авторизированы";
            // 
            // AddProcess
            // 
            this.AcceptButton = this.AddPr;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 353);
            this.Controls.Add(this.UserStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AllPr);
            this.Controls.Add(this.AddPr);
            this.Controls.Add(this.inp_process);
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddProcess";
            this.Text = "Планировщик процессов";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.AddProcess_HelpButtonClicked);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddProcess_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AddProcess_FormClosed);
            this.Load += new System.EventHandler(this.AddProcess_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AllPr)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox inp_process;
        private System.Windows.Forms.Button AddPr;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Prioritet;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn User;
        public System.Windows.Forms.DataGridView AllPr;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label UserStatus;
    }
}

