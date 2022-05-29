using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using TestTaskDCT.Infrastructure;
using TestTaskDCT.Models;
using TestTaskDCT.Services;
using TestTaskDCT.ViewModels.Base;

namespace TestTaskDCT.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        #region Variables

        Requests requests = new Requests();
        Calculations calculations = new Calculations();


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
        private Asset _PlotCurrency = null;
        public Asset PlotCurrency
        {
            get => _PlotCurrency;
            set => Set(ref _PlotCurrency, value);
        }
        private Asset _Details = null;
        public Asset Details
        {
            get => _Details;
            set
            {
                _Details = value;
                OnPropertyChanged(nameof(Details));
            }
        }

        private ObservableCollection<Asset> _Info;
        public ObservableCollection<Asset> Info
        {
            get => _Info;
            set
            {
                _Info = value;
                OnPropertyChanged(nameof(Info));
            }
        }

        private ObservableCollection<Market> _Markets;
        public ObservableCollection<Market> Markets
        {
            get => _Markets;
            set
            {
                _Markets = value;
                OnPropertyChanged(nameof(Markets));
            }
        }

        public ObservableCollection<Asset> BestAssets { get; }
        public ObservableCollection<Asset> Currencies { get; }
        public ObservableCollection<DatePoints> Points { get; private set; } = null;
        #endregion
        #region Commands

        public ICommand ConvertCurrencyCommand { get; }

        private bool CanConvertCurrencyCommandExecute(object p)
        {
            if (_ToConvert != null && _ConvertInto != null && _ConvertAmount != 0) return true;
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
        public ICommand DrawChartCommand { get; }
        private bool CanDrawChartCommandExecute(object p)
        {
            if (PlotCurrency != null)
            {
                return true;
            }
            return false;
        }

        private void OnDrawChartCommandExecuted(object p)
        {
            ObservableCollection<GraphPoint> unixPoints = requests.GetPoints(PlotCurrency.Id, "d1");
            Points = new ObservableCollection<DatePoints>();
            foreach (var up in unixPoints)
            {
                DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                DateTime date = start.AddMilliseconds(up.Time).ToLocalTime();

                Points.Add(new DatePoints { PriceUSD = up.PriceUSD, Date = date });
            }
            OnPropertyChanged("Points");
        }
        public ICommand ShowInfoCommand { get; }
        private bool CanShowInfoCommandExecute(object p)
        {
            if (Details != null)
            {
                return true;
            }
            return false;
        }
        private void OnShowInfoCommandExecuted(object p)
        {
            Info = new ObservableCollection<Asset>()
            {
                Details
            };
            List<RequestParameter> parametersAllMarkets = new List<RequestParameter>()
            {
                new RequestParameter
                {
                    Name = "limit",
                    Value = 2000
                }
            };
            Markets = requests.GetMarketsData(Details.Id, parametersAllMarkets);

        }
        #endregion


        public MainWindowViewModel()
        {
            ShowInfoCommand = new LambdaCommand(OnShowInfoCommandExecuted, CanShowInfoCommandExecute);
            DrawChartCommand = new LambdaCommand(OnDrawChartCommandExecuted, CanDrawChartCommandExecute);
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
