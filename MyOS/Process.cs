using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyOS
{
    public class Process
    {
        private string name;
        private int prior;
        private int time;
        private string userID;
        public bool pause = false;
       // public static int prCount = 0;

        public Process(string sName, int sPrior, int sTime, string sUserID)
        {
            name = sName;
            prior = sPrior;
            time = sTime;
            userID = sUserID;
        }

        public Process() { }
        ~Process() { }
              
        public void setPrior(int newPrior)
        {
            prior = newPrior;
        }

        public void setTime(int newTime)
        {
            time = newTime;
        }

        public string getName()
        {
            return name;
        }

        public int getPrior()
        {
            return prior;
        }

        public int getTime()
        {
            return time;
        }

        public string getUserID()
        {
            return userID;
        }

    }
}
