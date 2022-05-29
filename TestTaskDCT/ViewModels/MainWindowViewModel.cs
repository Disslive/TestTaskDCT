using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using TestTaskDCT.Models;
using TestTaskDCT.Services;
using TestTaskDCT.ViewModels.Base;
using TestTaskDCT.Infrastructure;
using System.Linq;

namespace TestTaskDCT.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        #region variables
        private string _Title = "Main window - Test task";
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        private string _Theme = "Switch to dark";
        public string Theme
        {
            get => _Theme;
            set => Set(ref _Theme, value);
        }

        public ObservableCollection<Asset> BestAssets { get; } 
        public ObservableCollection<Asset> Currencies { get; }
        #endregion
        #region Commands
        public ICommand CloseApplicationCommand { get; }
        private bool CanCloseApplicationCommandExecute(object p) => true;
        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        #endregion

        public MainWindowViewModel()
        {
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            Requests requests = new Requests();
           
            List<RequestParameter> parametersAllAssets = new List<RequestParameter>()
            {
                new RequestParameter
                {
                    Name = "limit",
                    Value = 2000
                }
            };
            Currencies = requests.GetAssetsData(parametersAllAssets);
            BestAssets = new ObservableCollection<Asset>();
            for(int i =0; i<10; i++)
            {
                BestAssets.Add(Currencies[i]);
            }
        }


    }
}
