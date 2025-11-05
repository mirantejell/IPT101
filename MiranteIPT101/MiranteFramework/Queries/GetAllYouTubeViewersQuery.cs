using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiranteIPT101Solution.MiranteDomain.Models;
using MiranteIPT101Solution.MiranteDomain.Queries;
using MiranteIPT101Solution.MiranteFramework.DTOs;

namespace MiranteIPT101Solution.MiranteFramework.Queries
{
    public class GetAllYouTubeViewersQuery : IGetAllYouTubeViewersQuery
    {
        private readonly YouTubeViewersDbContextFactory _contextFactory;

        public GetAllYouTubeViewersQuery(YouTubeViewersDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<YouTubeViewer>> Execute()
        {
            using (YouTubeViewersDbContext context = _contextFactory.Create())
            {
                IEnumerable<YouTubeViewerDto> youTubeViewerDtos = await context.YouTubeViewers.ToListAsync();

                return youTubeViewerDtos.Select(y => new YouTubeViewer(y.Id, y.Username, y.IsSubscribed, y.IsMember));
            }
        }
    }
}
