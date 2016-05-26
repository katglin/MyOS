using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyOS
{
    public partial class Registrate : Form
    {
        public Registrate()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            string log = tbLogin.Text;
            string pass = tbPass.Text;
            
            if (log.Length > 0 && pass.Length > 0)
            {
                if (log.Length > 15)
                {
                    MessageBox.Show("Логин не должен содержать более 15 символов");
                    tbLogin.Clear();
                    return;
                }
                if (!UserExist(log))
                {
                    // добавить ограничения на пароль
                    User newUser = new User(log, false, User.getHash(pass));
                    MainProgram.users.Add(newUser);
                    MainProgram.currentuser = newUser;
                    MainProgram.addPr.UserStatus.Text = "Текущий пользователь:  " +
                                  MainProgram.currentuser.getLogin();
                    this.Close();
                }
                else 
                { 
                    MessageBox.Show("Пользователь с таким логином уже существует");
                    tbLogin.Clear();
                }
            }
            else
            {
                MessageBox.Show("Введите новый логин и пароль!");
            }
        }

        private bool UserExist(string log)
        {
            int i = 0;
            bool pr = false;
            while (i < MainProgram.users.Count && !pr)
            {
                if (MainProgram.users[i].getLogin() == log)
                    pr = true;
                else i++;
            }
            return pr;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
