using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace MyOS
{
    public partial class MemoryTest : Form
    {
        public static int p_count;
        public static int pr_count;
        MemoryWork mw = new MemoryWork();
        public MemoryTest()
        {
            InitializeComponent();
        }

        private void start_Click(object sender, EventArgs e)
        {
            try 
            {
                p_count = Convert.ToInt32(page_count.Text);
                pr_count = Convert.ToInt32(this.proc_count.Text);
                if (p_count > MemoryWork.PAGE_COUNT)
                { MessageBox.Show("В памяти всего 100 страниц"); }
                else if (pr_count > 10)
                { MessageBox.Show("Нельзя запустить более 10 процессов одновременно"); }
                else if (pr_count < 1 || p_count < 1) throw (new Exception());
                else
                {
                    fillTables();
                    Thread myTh = new Thread(run);
                    myTh.Start();
                }
            }catch 
            {
                MessageBox.Show("Параметры должны быть целыми положительными числами");
            } 
        }
        public void run()
        {
            for (int page = 1; page <= p_count; page++)
            {
                mw.startProc(page); // выделяем p_count страниц для общего доступа
            }
        }
        public void saveToLog()
        {
            StreamWriter bw = new StreamWriter(File.Open(tbLog.Text, FileMode.OpenOrCreate));
            bw.Write(txt.Text);
            bw.Close();
        }
        private void MemoryTest_Load(object sender, EventArgs e)
        {
            p_count = Convert.ToInt32(page_count.Text);
            pr_count = Convert.ToInt32(this.proc_count.Text);
            txt.Parent = tab.TabPages[0];
            tbl.Parent = tab.TabPages[1];
            tPages.Parent = tab.TabPages[2];

            tbl.ReadOnly = true;
            tPages.ReadOnly = true;
            fillTables();
            tbl.ClearSelection();
            tPages.ClearSelection();
        }
        private void fillTables()
        {
            tbl.Rows.Clear();
            tPages.Rows.Clear();
            tbl.Rows.Add(12);
            tPages.Rows.Add(12);
            for (int i = 1; i <= pr_count; i++)
            {
                MainProgram.memt.tbl[0, i - 1].Value = "Поток " + i.ToString();
                MainProgram.memt.tbl[1, i - 1].Value = "Не запущен";
            }
            for (int i = 1; i <= p_count; i++)
            {
                MainProgram.memt.tPages[0, i - 1].Value = "Страница " + i.ToString();
                MainProgram.memt.tPages[1, i - 1].Value = "Свободна";
            }
        }

        private void clean_Click(object sender, EventArgs e)
        {
            this.txt.Clear();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            saveToLog();
        }
    }
}
