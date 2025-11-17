using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiranteIPT101Solution.MiranteDomain.Commands;
using MiranteIPT101Solution.MiranteFramework.DTOs;

namespace MiranteIPT101Solution.MiranteFramework.Commands
{
    public class DeleteYouTubeViewerCommand : IDeleteYouTubeViewerCommand
    {
        private readonly YouTubeViewersDbContextFactory _contextFactory;

        public DeleteYouTubeViewerCommand(YouTubeViewersDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(Guid id)
        {
            using (YouTubeViewersDbContext context = _contextFactory.Create())
            {
                YouTubeViewerDto youTubeViewerDto = new YouTubeViewerDto()
                {
                    Id = id,
                };

                context.YouTubeViewers.Remove(youTubeViewerDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
