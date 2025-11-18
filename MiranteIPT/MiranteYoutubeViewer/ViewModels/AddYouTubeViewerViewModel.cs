using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MiranteIPT101Solution.MiranteYoutubeViewer.Commands;
using MiranteIPT101Solution.MiranteYoutubeViewer.Stores;

namespace MiranteIPT101Solution.MiranteYoutubeViewer.ViewModels
{
    public class AddYouTubeViewerViewModel : ViewModelBase
    {
        public YouTubeViewerDetailsFormViewModel YouTubeViewerDetailsFormViewModel { get; }

        public AddYouTubeViewerViewModel(YouTubeViewersStore youTubeViewersStore, ModalNavigationStore modalNavigationStore)
        {
            ICommand submitCommand = new AddYouTubeViewerCommand(this, youTubeViewersStore, modalNavigationStore);
            ICommand cancelCommand = new CloseModalCommand(modalNavigationStore);
            YouTubeViewerDetailsFormViewModel = new YouTubeViewerDetailsFormViewModel(submitCommand, cancelCommand);
        }
    }
}
