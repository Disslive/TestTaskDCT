using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using TestTaskDCT.Models;
using TestTaskDCT.Services;
using TestTaskDCT.ViewModels.Base;
using TestTaskDCT.Infrastructure;
using System.Text.RegularExpressions;

namespace TestTaskDCT.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        #region variables

        Requests requests = new Requests();
        Calculations calculations = new Calculations();

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

        private string _ConvertedCurrency = null;
        public string ConvertedCurrency
        {
            get => _ConvertedCurrency;
            set => Set(ref _ConvertedCurrency, value);
        }

        private Asset _ToConvert;
        public Asset ToConvert
        {
            get => _ToConvert;
            set => Set(ref _ToConvert, value);
        }
        private Asset _ConvertInto;
        public Asset ConvertInto
        {
            get => _ConvertInto;
            set => Set(ref _ConvertInto, value);
        }

        private double _ConvertAmount = 0;
        public double ConvertAmount
        {
            get => _ConvertAmount;
            set => Set(ref _ConvertAmount, value);
        }

        public ObservableCollection<Asset> BestAssets { get; } 
        public ObservableCollection<Asset> Currencies { get; }
        #endregion
        #region Commands

        public ICommand ConvertCurrencyCommand { get; }

        private bool CanConvertCurrencyCommandExecute(object p)
        {
            if (_ToConvert != null && _ConvertInto!=null && _ConvertAmount != 0) return true;
            return false;
        }

        private void OnConvertCurrencyCommandExecuted(object p)
        {
            double result = calculations.ConvertCurrency(_ToConvert.Id, _ConvertInto.Id, _ConvertAmount);
            if (result != 0)
            {
                ConvertedCurrency = result.ToString();
            }
           else
            {
                ConvertedCurrency = "Converting is impossible";
            }
        }


        public ICommand CloseApplicationCommand { get; }
        private bool CanCloseApplicationCommandExecute(object p) => true;
        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }


        #endregion
        public MainWindowViewModel()
        {
            ConvertCurrencyCommand = new LambdaCommand(OnConvertCurrencyCommandExecuted, CanConvertCurrencyCommandExecute);
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
       
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
