using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer.Mappers
{
    public interface IMapper<in TSource, out TDestination>
    {
        TDestination Map(TSource source);
        IEnumerable<TDestination> MapList(IEnumerable<TSource> source);
    }
}
