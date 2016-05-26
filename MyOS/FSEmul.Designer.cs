namespace MyOS
{
    partial class FSEmul
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
            this.command = new System.Windows.Forms.TextBox();
            this.output = new System.Windows.Forms.TextBox();
            this.run = new System.Windows.Forms.Button();
            this.back = new System.Windows.Forms.Button();
            this.user = new System.Windows.Forms.Label();
            this.clean = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // command
            // 
            this.command.AcceptsReturn = true;
            this.command.Location = new System.Drawing.Point(35, 14);
            this.command.Name = "command";
            this.command.Size = new System.Drawing.Size(232, 20);
            this.command.TabIndex = 1;
            // 
            // output
            // 
            this.output.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.output.Location = new System.Drawing.Point(12, 40);
            this.output.Multiline = true;
            this.output.Name = "output";
            this.output.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.output.Size = new System.Drawing.Size(452, 323);
            this.output.TabIndex = 0;
            this.output.TabStop = false;
            // 
            // run
            // 
            this.run.Location = new System.Drawing.Point(295, 11);
            this.run.Name = "run";
            this.run.Size = new System.Drawing.Size(75, 23);
            this.run.TabIndex = 2;
            this.run.Text = "Выполнить";
            this.run.UseVisualStyleBackColor = true;
            this.run.Click += new System.EventHandler(this.run_Click);
            // 
            // back
            // 
            this.back.BackColor = System.Drawing.SystemColors.Control;
            this.back.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.back.Image = global::MyOS.Properties.Resources.arrow;
            this.back.Location = new System.Drawing.Point(12, 369);
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(75, 23);
            this.back.TabIndex = 3;
            this.back.UseVisualStyleBackColor = false;
            this.back.Click += new System.EventHandler(this.back_Click_1);
            // 
            // user
            // 
            this.user.AutoSize = true;
            this.user.Location = new System.Drawing.Point(371, 379);
            this.user.Name = "user";
            this.user.Size = new System.Drawing.Size(27, 13);
            this.user.TabIndex = 4;
            this.user.Text = "user";
            // 
            // clean
            // 
            this.clean.Location = new System.Drawing.Point(192, 369);
            this.clean.Name = "clean";
            this.clean.Size = new System.Drawing.Size(75, 23);
            this.clean.TabIndex = 5;
            this.clean.Text = "Очистить";
            this.clean.UseVisualStyleBackColor = true;
            this.clean.Click += new System.EventHandler(this.clean_Click);
            // 
            // FSEmul
            // 
            this.AcceptButton = this.run;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.back;
            this.ClientSize = new System.Drawing.Size(485, 397);
            this.Controls.Add(this.clean);
            this.Controls.Add(this.user);
            this.Controls.Add(this.back);
            this.Controls.Add(this.run);
            this.Controls.Add(this.output);
            this.Controls.Add(this.command);
            this.Name = "FSEmul";
            this.Text = "FSEmul";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FSEmul_FormClosing);
            this.Load += new System.EventHandler(this.FSEmul_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox command;
        private System.Windows.Forms.TextBox output;
        private System.Windows.Forms.Button run;
        private System.Windows.Forms.Button back;
        private System.Windows.Forms.Label user;
        private System.Windows.Forms.Button clean;
    }
}