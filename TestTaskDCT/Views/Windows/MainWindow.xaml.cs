using System.Windows;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System;

namespace TestTaskDCT
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SwitchLanguage("en");
            SwitchStyle("Dark");
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void ChooseEnglishLang(object sender, RoutedEventArgs e)
        {
            SwitchLanguage("en");
        }
        private void ChooseUkrainianLang(object sender, RoutedEventArgs e)
        {
            SwitchLanguage("ua");
        }
        private void ChooseLightTheme(object sender, RoutedEventArgs e)
        {
            SwitchStyle("Light");
        }
        private void ChooseDarkTheme(object sender, RoutedEventArgs e)
        {
            SwitchStyle("Dark");
        }
        private void SwitchLanguage(string languageCode)
        {
            ResourceDictionary dictionary = new ResourceDictionary();
            switch (languageCode)
            {
                case "en":
                    dictionary.Source = new Uri("..\\Lang/EN.xaml", UriKind.Relative);
                    break;
                case "ua":
                    dictionary.Source = new Uri("..\\Lang/UA.xaml", UriKind.Relative);
                    break;
                default:
                    dictionary.Source = new Uri("..\\Lang/EN.xaml", UriKind.Relative);
                    break;
            }
            this.Resources.MergedDictionaries.Add(dictionary);
        }
        private void SwitchStyle(string styleName)
        {
            ResourceDictionary dictionary = new ResourceDictionary();
            switch (styleName)
            {
                case "Dark":
                    dictionary.Source = new Uri("..\\Themes/Dark.xaml", UriKind.Relative);
                    break;
                case "Light":
                    dictionary.Source = new Uri("..\\Themes/Light.xaml", UriKind.Relative);
                    break;
                default:
                    dictionary.Source = new Uri("..\\Theme/Light.xaml", UriKind.Relative);
                    break;
            }
            this.Resources.MergedDictionaries.Add(dictionary);
        }

    }
}
