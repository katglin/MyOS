using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace MyOS
{
    public partial class Packet : Form
    {
        private OpenFileDialog opd = new OpenFileDialog();
        public Packet()
        {
            InitializeComponent();    
            opd.Title = "Выберите файл пакета";
            opd.Filter = "Текстовые файлы|*.txt";
        }

        private void ChoosePacket_Click(object sender, EventArgs e)
        {
            StreamReader filePacket;
            StreamWriter fileErrors;
            string command;
            List <string> errorList = new List<string>();
            int error;
            string[] str;
            if (opd.ShowDialog() == DialogResult.OK)
            {
                filePacket = new StreamReader(opd.FileName);
                fileErrors = new StreamWriter("Packet_Errors.txt");
                while (!filePacket.EndOfStream)
                {
                    command = filePacket.ReadLine();
                    if (command.Length > 0)
                    {
                        str = command.Split(' ');
                        if (str.Length > 0 && str.Length < 4 && (str[0] == "start" || str[0]=="stop"))
                        {
                            command = "System Info: " + command;
                            error = (new AddProcess()).analyseCommand(command);
                            Thread.Sleep(100);
                            if (error != 0) errorList.Add(command + " - Недопустимая команда, код ошибки: "+ AddProcess.errorByCode(error));
                        }
                        else errorList.Add(command +" - Недопустимая команда");
                    }
                }
                fileErrors.WriteLine("Выполнение пакета "+opd.FileName+":");
                for (int i = 0; i < errorList.Count; i++)
                {
                    fileErrors.WriteLine(errorList[i]);
                }
                if (errorList.Count > 0)
                    textResult.Text = "Внимание! При выполнении пакета " +
                        " в нем были обнаружены ошибки " +
                        "Их описание приведено в файле 'Packet_Errors.txt'";

                else
                {
                    textResult.Text = "Пакет выполнен успешно. " +
                            "Ошибок не обнаружено";
                    fileErrors.WriteLine(" ошибок не найдено");
                }
                textResult.Visible = true;
                filePacket.Close();
                fileErrors.Close();
            }
        }

        private void Packet_Load(object sender, EventArgs e)
        {
            textResult.Visible = false;
        }
    }
}
