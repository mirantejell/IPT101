using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiranteIPT101Solution.MiranteDomain.Models;

namespace MiranteIPT101Solution.MiranteDomain.Commands
{
    public interface IUpdateYouTubeViewerCommand
    {
        Task Execute(YouTubeViewer youTubeViewer);
    }
}
