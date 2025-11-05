using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiranteIPT101Solution.MiranteDomain.Models;

namespace MiranteIPT101Solution.MiranteDomain.Queries
{
    public interface IGetAllYouTubeViewersQuery
    {
        Task<IEnumerable<YouTubeViewer>> Execute();
    }
}
