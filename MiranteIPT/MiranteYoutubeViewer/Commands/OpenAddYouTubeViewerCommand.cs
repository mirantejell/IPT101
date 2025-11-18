using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MiranteIPT101Solution.MiranteYoutubeViewer.Stores;
using MiranteIPT101Solution.MiranteYoutubeViewer.ViewModels;

namespace MiranteIPT101Solution.MiranteYoutubeViewer.Commands
{
    public class OpenAddYouTubeViewerCommand : CommandBase
    {
        private readonly YouTubeViewersStore _youTubeViewersStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public OpenAddYouTubeViewerCommand(YouTubeViewersStore youTubeViewersStore, ModalNavigationStore modalNavigationStore)
        {
            _youTubeViewersStore = youTubeViewersStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            AddYouTubeViewerViewModel addYouTubeViewerViewModel = new AddYouTubeViewerViewModel(_youTubeViewersStore, _modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = addYouTubeViewerViewModel;
        }
    }
}
