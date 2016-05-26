using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyOS
{
    class SuperBlock
    {
        string fs_name; // "MyFS"
        ushort clust_size; // size of 1 cluster, 512 bytes
        ushort fat_first_clust;  // first cluster of fat-table (fat-table - list of clusters)
        ushort root_first_clust; // first cluster of root directory, list of files
        ushort files_first_clust; // first cluster of files block (files block - content of files)
        ushort files_clust_count; // count of clusters in file block, 512 clusters

        public string Name 
        {
            get { return fs_name;  } 
        }
        public ushort Cl_size { get {return clust_size; } }
        public ushort Fat_f_cl { get { return fat_first_clust; } }
        public ushort Root_f_cl { get { return root_first_clust; } }
        public ushort Files_f_cl { get { return files_first_clust; } }
        public ushort Files_cl_c { get { return files_clust_count; } }


        public SuperBlock(string fs_n, ushort cl_sz, ushort fat_f_cl, ushort root_f_cl, ushort files_f_cl, ushort files_cl_c)
        {
            fs_name = fs_n;
            clust_size = cl_sz;
            fat_first_clust = fat_f_cl;
            root_first_clust = root_f_cl;
            files_first_clust = files_f_cl;
            files_clust_count = files_cl_c;
        }
        public SuperBlock(SuperBlock sb)
        {
            fs_name = sb.fs_name;
            clust_size = sb.clust_size;
            fat_first_clust = sb.fat_first_clust;
            root_first_clust = sb.root_first_clust;
            files_first_clust = sb.files_first_clust;
            files_clust_count = sb.files_clust_count;
        }
    }
}
