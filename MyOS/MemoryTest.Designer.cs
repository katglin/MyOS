namespace MyOS
{
    partial class MemoryTest
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
            this.start = new System.Windows.Forms.Button();
            this.tab = new System.Windows.Forms.TabControl();
            this.p1 = new System.Windows.Forms.TabPage();
            this.txt = new System.Windows.Forms.TextBox();
            this.p2 = new System.Windows.Forms.TabPage();
            this.tbl = new System.Windows.Forms.DataGridView();
            this.Process = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Activity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pages = new System.Windows.Forms.TabPage();
            this.tPages = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clean = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.page_count = new System.Windows.Forms.TextBox();
            this.proc_count = new System.Windows.Forms.TextBox();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btSave = new System.Windows.Forms.Button();
            this.tab.SuspendLayout();
            this.p1.SuspendLayout();
            this.p2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbl)).BeginInit();
            this.pages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tPages)).BeginInit();
            this.SuspendLayout();
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(418, 12);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(75, 23);
            this.start.TabIndex = 1;
            this.start.Text = "RUN";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // tab
            // 
            this.tab.Controls.Add(this.p1);
            this.tab.Controls.Add(this.p2);
            this.tab.Controls.Add(this.pages);
            this.tab.Location = new System.Drawing.Point(12, 75);
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.Size = new System.Drawing.Size(485, 305);
            this.tab.TabIndex = 2;
            // 
            // p1
            // 
            this.p1.Controls.Add(this.txt);
            this.p1.Location = new System.Drawing.Point(4, 22);
            this.p1.Name = "p1";
            this.p1.Padding = new System.Windows.Forms.Padding(3);
            this.p1.Size = new System.Drawing.Size(477, 279);
            this.p1.TabIndex = 0;
            this.p1.Text = "Отчет";
            this.p1.UseVisualStyleBackColor = true;
            // 
            // txt
            // 
            this.txt.Location = new System.Drawing.Point(11, 14);
            this.txt.Multiline = true;
            this.txt.Name = "txt";
            this.txt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt.Size = new System.Drawing.Size(450, 253);
            this.txt.TabIndex = 1;
            // 
            // p2
            // 
            this.p2.Controls.Add(this.tbl);
            this.p2.Location = new System.Drawing.Point(4, 22);
            this.p2.Name = "p2";
            this.p2.Padding = new System.Windows.Forms.Padding(3);
            this.p2.Size = new System.Drawing.Size(477, 279);
            this.p2.TabIndex = 1;
            this.p2.Text = "Потоки";
            this.p2.UseVisualStyleBackColor = true;
            // 
            // tbl
            // 
            this.tbl.AllowUserToAddRows = false;
            this.tbl.AllowUserToDeleteRows = false;
            this.tbl.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tbl.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.tbl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tbl.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Process,
            this.Activity});
            this.tbl.Cursor = System.Windows.Forms.Cursors.Default;
            this.tbl.Location = new System.Drawing.Point(23, 0);
            this.tbl.MinimumSize = new System.Drawing.Size(350, 0);
            this.tbl.Name = "tbl";
            this.tbl.ReadOnly = true;
            this.tbl.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbl.Size = new System.Drawing.Size(433, 273);
            this.tbl.TabIndex = 0;
            // 
            // Process
            // 
            this.Process.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Process.FillWeight = 121.8274F;
            this.Process.Frozen = true;
            this.Process.HeaderText = "Process";
            this.Process.Name = "Process";
            this.Process.ReadOnly = true;
            this.Process.Width = 200;
            // 
            // Activity
            // 
            this.Activity.FillWeight = 78.17259F;
            this.Activity.HeaderText = "Activity";
            this.Activity.Name = "Activity";
            this.Activity.ReadOnly = true;
            // 
            // pages
            // 
            this.pages.Controls.Add(this.tPages);
            this.pages.Location = new System.Drawing.Point(4, 22);
            this.pages.Name = "pages";
            this.pages.Padding = new System.Windows.Forms.Padding(3);
            this.pages.Size = new System.Drawing.Size(477, 279);
            this.pages.TabIndex = 2;
            this.pages.Text = "Страницы";
            this.pages.UseVisualStyleBackColor = true;
            // 
            // tPages
            // 
            this.tPages.AllowUserToAddRows = false;
            this.tPages.AllowUserToDeleteRows = false;
            this.tPages.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tPages.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.tPages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tPages.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.tPages.Cursor = System.Windows.Forms.Cursors.Default;
            this.tPages.Location = new System.Drawing.Point(61, 3);
            this.tPages.MinimumSize = new System.Drawing.Size(350, 0);
            this.tPages.Name = "tPages";
            this.tPages.ReadOnly = true;
            this.tPages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tPages.Size = new System.Drawing.Size(350, 273);
            this.tPages.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Page";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Status";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // clean
            // 
            this.clean.Location = new System.Drawing.Point(12, 12);
            this.clean.Name = "clean";
            this.clean.Size = new System.Drawing.Size(75, 23);
            this.clean.TabIndex = 3;
            this.clean.Text = "CLEAN";
            this.clean.UseVisualStyleBackColor = true;
            this.clean.Click += new System.EventHandler(this.clean_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(149, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Количество процессов:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(149, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Разделяемых страниц:";
            // 
            // page_count
            // 
            this.page_count.Location = new System.Drawing.Point(285, 29);
            this.page_count.Name = "page_count";
            this.page_count.Size = new System.Drawing.Size(100, 20);
            this.page_count.TabIndex = 6;
            this.page_count.Text = "5";
            // 
            // proc_count
            // 
            this.proc_count.Location = new System.Drawing.Point(285, 6);
            this.proc_count.Name = "proc_count";
            this.proc_count.Size = new System.Drawing.Size(100, 20);
            this.proc_count.TabIndex = 7;
            this.proc_count.Text = "5";
            // 
            // tbLog
            // 
            this.tbLog.Location = new System.Drawing.Point(285, 53);
            this.tbLog.Name = "tbLog";
            this.tbLog.Size = new System.Drawing.Size(100, 20);
            this.tbLog.TabIndex = 9;
            this.tbLog.Text = "ProcMemWork.log";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(149, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Сохранить отчет в файл:";
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(418, 51);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(75, 23);
            this.btSave.TabIndex = 10;
            this.btSave.Text = "SAVE";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // MemoryTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 388);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.tbLog);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.proc_count);
            this.Controls.Add(this.page_count);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.clean);
            this.Controls.Add(this.tab);
            this.Controls.Add(this.start);
            this.Name = "MemoryTest";
            this.Text = "MemoryTest";
            this.Load += new System.EventHandler(this.MemoryTest_Load);
            this.tab.ResumeLayout(false);
            this.p1.ResumeLayout(false);
            this.p1.PerformLayout();
            this.p2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbl)).EndInit();
            this.pages.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tPages)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button start;
        public System.Windows.Forms.TabControl tab;
        private System.Windows.Forms.TabPage p1;
        private System.Windows.Forms.TabPage p2;
        public System.Windows.Forms.TextBox txt;
        public System.Windows.Forms.DataGridView tbl;
        private System.Windows.Forms.Button clean;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox page_count;
        private System.Windows.Forms.TextBox proc_count;
        private System.Windows.Forms.TabPage pages;
        public System.Windows.Forms.DataGridView tPages;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Process;
        private System.Windows.Forms.DataGridViewTextBoxColumn Activity;
        private System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btSave;
    }
}