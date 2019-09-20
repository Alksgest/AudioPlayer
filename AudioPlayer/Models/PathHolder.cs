using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer.Models
{
    public class PathHolder
    {
        public string Title { get; set; }
        public string FullPath { get; set; }

        public PathHolder() { }

        public PathHolder(string title, string fullPath)
        {
            this.Title = title;
            this.FullPath = fullPath;
        }
        public override string ToString() => Title;
    }
}
