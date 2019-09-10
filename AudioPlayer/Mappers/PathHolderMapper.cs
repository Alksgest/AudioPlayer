using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioPlayer.Models;

namespace AudioPlayer.Mappers
{
    public class PathHolderMapper :  IMapper<string, PathHolder>
    {
        public PathHolder Map(string source)
        {
            var splittedPath = source.Split('\\');
            var title = splittedPath[splittedPath.Length - 1];
            var fullPath = source;

            return new PathHolder(title, fullPath);
        }

        public IEnumerable<PathHolder> MapList(IEnumerable<string> source)
        {
            List<PathHolder> paths = new List<PathHolder>();
            foreach (var path in source)
            {
                paths.Add(Map(path));
            }

            return paths;
        }
    }
}
