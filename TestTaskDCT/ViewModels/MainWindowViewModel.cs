using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


    }
}
