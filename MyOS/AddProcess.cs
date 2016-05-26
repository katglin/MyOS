using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace MyOS
{

    public partial class AddProcess : Form
    {
        public AddProcess()
        {
            InitializeComponent();
        }

        private void AddProcess_Load(object sender, EventArgs e)
        {
            this.AllPr.Rows.Add(50);
            this.AllPr.ClearSelection();
            delegClear = new ClearRows(ClearRowsMethod);
        }

        public void ClearRowsMethod()
        {
            int col = AllPr.Rows.Count;
            AllPr.Rows.Clear();
            this.AllPr.Rows.Add(col);
            this.AllPr.ClearSelection();
        }

        private void AddPr_Click(object sender, EventArgs e) // "Выполнить"
        {
            int res = analyseCommand(Convert.ToString(inp_process));
            inp_process.Clear();

            switch (res)
            {
                case 1: case 2: case 3: MessageBox.Show(errorByCode(res));  break;
                case 4: DialogResult answer = new System.Windows.Forms.DialogResult();
                    answer = MessageBox.Show(errorByCode(res) +
                                "\n Открыть список команд? ", "Ошибка ввода", MessageBoxButtons.YesNo);
                        if (answer == DialogResult.Yes)
                                AddProcess_HelpButtonClicked(this, new CancelEventArgs());
                        break;

            }
        }

        private void AddProcess_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainProgram.planir.Abort();
        }

        public delegate void ClearRows();
        public ClearRows delegClear;

        private void AddProcess_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Help.ShowHelp(this, "ProcessHelp.chm", HelpNavigator.TableOfContents);
        }

        private void AddProcess_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainProgram.Save();
        }

        public int analyseCommand(string command)
        {
            int j;
            string[] str = command.Split(' ');
            if (str[2].ToLower() == "mt" && str.Length == 3)
            {
                MainProgram.memt = new MemoryTest(); //форма
                MainProgram.memt.Show();
                return 0;
            }
            #region start
            if (str[2].ToLower() == "start" && str.Length == 5 && str[3].Length > 0 && Int32.TryParse(str[4], out j))
            {
                if (ProcessControll.findProcess(str[3]) == -1) // процесс еще не запущен
                {
                   
                    this.AllPr.Rows.Add(1);
                    Random rand = new Random();
                    int time = rand.Next(1000, 2000);
                    Process tempProc = new Process(str[3], j, time, MainProgram.currentuser.getLogin()); //j = Convert.ToInt32(str[4])

                    lock (ProcessControll.listProc)
                        ProcessControll.listProc.Add(tempProc);
                    lock (MainProgram.qPr)
                            MainProgram.qPr.Enqueue(tempProc);
                    return 0;
                }
                else return 1; // MessageBox.Show("Процесс с таким именем уже запущен");
            }
            #endregion
            #region stop
            if (str[2].ToLower() == "stop" && str[3].ToLower() != "all" && str.Length == 4 && str[3].Length > 0)
            {
                Queue<Process> tempQu;
                Process tempPr = new Process();
                bool isFound = false;
                int i = 0;
                lock (MainProgram.qPr)
                    tempQu = new Queue<Process>(MainProgram.qPr);

                while (!isFound && i++< MainProgram.qPr.Count)
                {
                    tempPr = tempQu.Peek();
                    if (tempPr.getName() == str[3])
                    {
                        isFound = true;
                        if (MainProgram.currentuser.getLogin() == tempPr.getUserID() ||
                            MainProgram.currentuser.getLogin() == "admin" ||
                            tempPr.getUserID() == "UnknownUser")
                        {
                            tempQu.Peek().setTime(0);
                            tempQu.Peek().pause = false;
                            return 0;
                        }
                        else  return 2; // MessageBox.Show("У вас не достаточно прав");

                    }
                    else
                    {
                        tempQu.Dequeue();
                        tempQu.Enqueue(tempPr);
                    }
                }
                if (!isFound)
                {
                    return 3; // MessageBox.Show("Процесс с таким именем не запущен");
                }
            }
            #endregion
            #region stopAll
            if (str[2].ToLower() == "stop" && str[3].ToLower() == "all" && str.Length == 4)
            {
                if (MainProgram.currentuser.getLogin() == "admin")
                {
                    MainProgram.planir.Abort();
                    while (MainProgram.qPr.Count > 0)
                        MainProgram.qPr.Dequeue();
                    ProcessControll.listProc.Clear();
                    MainProgram.addPr.Invoke(MainProgram.addPr.delegClear); // очистка таблицы
                    MainProgram.planir = new Thread((new ProcessControll()).run);
                    MainProgram.planir.Start();
                    return 0;
                }
                return 2;
            }
            #endregion
            #region pause
            if (str[2].ToLower() == "pause" && str[3].Length > 0 && str.Length == 4)
            {
                Queue<Process> tempQu;
                Process tempPr = new Process();
                bool isFound = false;
                int i = 0;
                lock (MainProgram.qPr)
                    tempQu = new Queue<Process>(MainProgram.qPr);

                while (!isFound && i++ < MainProgram.qPr.Count)
                {
                    tempPr = tempQu.Peek();
                    if (tempPr.getName() == str[3])
                    {
                        isFound = true;
                        if (MainProgram.currentuser.getLogin() == tempPr.getUserID() ||
                            MainProgram.currentuser.getLogin() == "admin" ||
                            tempPr.getUserID() == "UnknownUser")
                        {
                            tempQu.Peek().pause = true;
                            return 0;//
                        }
                        else return 2; // MessageBox.Show("У вас не достаточно прав");

                    }
                    else
                    {
                        tempQu.Dequeue();
                        tempQu.Enqueue(tempPr);
                    }
                }
                if (!isFound)
                {
                    return 3; // MessageBox.Show("Процесс с таким именем не запущен");
                }
            }
            #endregion
            #region go
            if (str[2].ToLower() == "go" && str[3].Length > 0 && str.Length == 4)
            {
                Queue<Process> tempQu;
                Process tempPr = new Process();
                bool isFound = false;
                int i = 0;
                lock (MainProgram.qPr)
                    tempQu = new Queue<Process>(MainProgram.qPr);

                while (!isFound && i++ < MainProgram.qPr.Count)
                {
                    tempPr = tempQu.Peek();
                    if (tempPr.getName() == str[3])
                    {
                        isFound = true;
                        if (MainProgram.currentuser.getLogin() == tempPr.getUserID() ||
                            MainProgram.currentuser.getLogin() == "admin" ||
                            tempPr.getUserID() == "UnknownUser")
                        {
                            tempQu.Peek().pause = false;
                            return 0;
                        }
                        else return 2; // MessageBox.Show("У вас не достаточно прав");

                    }
                    else
                    {
                        tempQu.Dequeue();
                        tempQu.Enqueue(tempPr);
                    }
                }
                if (!isFound)
                {
                    return 3; // MessageBox.Show("Процесс с таким именем не запущен");
                }
            }
            #endregion
            #region registrate
            if (str[2].ToLower() == "register" && str.Length == 3)
            {
                (new Registrate()).Show();
                return 0;
            }
            #endregion
            #region login
            if (str[2].ToLower() == "login" && str.Length == 3)
            {
                (new SignIn()).Show();
                return 0;
            }
            #endregion
            #region logout
            if (str[2].ToLower() == "logout" && str.Length == 3)
            {
                MainProgram.resetCurrentuser();
                return 0;
            }
            #endregion
            #region delete
            if (str[2].ToLower() == "delete" && str.Length == 4 && str[3].Length > 0)
            {
                if (MainProgram.currentuser.getLogin() != "UknownUser")
                { MyOS.User.deleteUser(str[3]); return 0; }
                else return 2; // MessageBox.Show("У вас недостаточно прав");

            }
            #endregion
            #region packet
            if (str[2].ToLower() == "packet" && str.Length == 3)
            {
                Packet packet = new Packet();
                packet.Show();
                return 0;
            }
            #endregion
            #region fs
            if (str[2].ToLower() == "fs" && str.Length == 3)
            {
         //    if (MainProgram.currentuser.getLogin()!="UnknownUser")
         //   {
                (new FSEmul()).Show();
                FSEmul.FS_Start();
                return 0;
        //     }
        //     else
        //     {
        //         return 2;
        //     }
            }
            #endregion
            return 4; // не корректная команда
        }

        public static string errorByCode(int code)
        {
            switch (code)
            {
                case 1: return "Процесс с таким именем уже запущен";
                case 2: return "У вас не достаточно прав";
                case 3: return "Процесс с таким именем не запущен";
                case 4: return "Не корректная команда";
                default: return "";
            }
        }
    }
}
