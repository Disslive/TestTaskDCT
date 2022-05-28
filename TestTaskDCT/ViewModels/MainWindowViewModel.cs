using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskDCT.Models;
using TestTaskDCT.Services;
using TestTaskDCT.ViewModels.Base;

namespace TestTaskDCT.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
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

        public List<Asset> Assets { get; set; }
        public MainWindowViewModel()
        {
            Requests requests = new Requests();
            List<RequestParameter> parameters = new List<RequestParameter>()
            {
                new RequestParameter
                {
                    Name = "limit",
                    Value = 10
                }
            };
    
            Assets = requests.GetAssetsData(parameters);
        
        }


    }
}
