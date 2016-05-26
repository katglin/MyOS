using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyOS
{
    public class myFile   // 63 b
    {
        public int id;          // 4 b
        string name;            // 10 b
        string extend;          // 3 b
        string date_time;    // date of creation or last change  19 b
        ushort first_clust;  // 0 for directory; for listCl   2 b
        int size;           // number of files for directory  4 b  
        string rights;      // n, r, w, rw    2 b
        int parent_dir;      // 0 - root is parent dir; id of parent directory   4 b
        string owner;       // 15 b

        public myFile(int i, string n, string ext, string dt, ushort fc, int sz, string r, int par_dir, string own)
        {
            id = i;
            name = n;
            extend = ext;
            date_time = dt;
            first_clust = fc;
            size = sz;
            rights = r;
            parent_dir = par_dir;
            owner = own;
        }

        public myFile() { }
        ~myFile() { }

        #region RegionGetSet
        public string Name 
        {
            get { return name; }
            set { name = value; }
        }
        public string Extend
        {
            get { return extend; }
            set { extend = value; }
        }
        public string Date_time
        {
            get { return date_time; }
            set { date_time = value; }
        }
        public ushort First_clust
        {
            get { return first_clust; }
            set { first_clust = value; }
        }
        public int Size
        {
            get { return size; }
            set { size = value; }
        }
        public string Rights
        {
            get { return rights; }
            set { rights = value; }
        }
        public int Parent_dir
        {
            get { return parent_dir; }
            set { parent_dir = value; }
        }
        public string Owner
        {
            get { return owner; }
            set { owner = value; }
        }
        #endregion 

        public static int generateID()
        {
            int id = 1;
            bool pr = false;
            while (!pr)
            {
                pr = true;
                for (int i = 0; i < MainProgram.fs.files.Count; i++)
                {
                    if (MainProgram.fs.files[i].id == id) { pr = false; id++; }
                }
            }
            return id;
        }
    }
}
