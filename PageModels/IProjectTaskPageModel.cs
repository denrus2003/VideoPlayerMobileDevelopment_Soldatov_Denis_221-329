using CommunityToolkit.Mvvm.Input;
using VideoPlayer.Models;

namespace VideoPlayer.PageModels
{
    public interface IProjectTaskPageModel
    {
        IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
        bool IsBusy { get; }
    }
}