using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MyOS
{
    public partial class SignIn : Form
    {
        public SignIn()
        {
            InitializeComponent();
        }

        private void SignIn_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < MainProgram.users.Count; i++)
            {
                cbUsers.Items.Add(MainProgram.users[i].getLogin());
            }
           // cbUsers.Text = "admin";
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EnterEnter_Click(object sender, EventArgs e)
        {
            string log = cbUsers.Text;
            string pass = tbPass.Text;
            int i = 0;
            bool pr = false;
            if (pass.Length > 0 && log.Length > 0)
            {
                while (i < MainProgram.users.Count && !pr)
                {
                    if (MainProgram.users[i].getLogin() == log)
                    {
                        pr = true;
                        if (MainProgram.users[i].getPass() == User.getHash(pass))
                        {
                            MainProgram.currentuser = MainProgram.users[i];
                            MainProgram.addPr.UserStatus.Text = "Текущий пользователь:  " +
                                   MainProgram.currentuser.getLogin();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Неверный пароль");
                            tbPass.Clear();
                            break;
                        }
                    }
                    else 
                    {
                        i++;
                    }
                }
                if (!pr)
                {
                    MessageBox.Show("Такой пользователь не найден");
                    cbUsers.Text = "";
                    tbPass.Clear();
                }

            }
            else
            {
                MessageBox.Show("Введите логин и пароль!");
            }

        }
    }
}
