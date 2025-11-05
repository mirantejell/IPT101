using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiranteIPT101Solution.MiranteDomain.Commands
{
    public interface IDeleteYouTubeViewerCommand
    {
        Task Execute(Guid id);
    }
}
