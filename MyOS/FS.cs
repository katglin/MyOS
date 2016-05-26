using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyOS
{
    class FS
    {
        public SuperBlock superblock;
        public ushort[] listCl;
        public List<myFile> files;

        public FS() { }
        public FS(SuperBlock sb, ushort[] lCl, List <myFile> f)
        {
            superblock = new SuperBlock(sb);
            listCl = new ushort[lCl.Length];
            listCl = lCl;
            files = new List<myFile>(f);
        }
        ~FS() { }

    }
}
