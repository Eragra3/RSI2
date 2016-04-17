using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Client.AccountingServiceReference;
using Client.LibraryServiceReference;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IAccountingService _accountingService;
        private ILibraryService _libraryService;

        private static bool _autoClearMainConsole = false;

        private readonly Brush _defaultBorderBrush;

        public MainWindow()
        {
            InitializeComponent();

            _accountingService = new AccountingServiceClient();
            _libraryService = new LibraryServiceClient();

            _defaultBorderBrush = AccountingDataTextBox.BorderBrush;
        }

        private void UIElement_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(((TextBox)sender).Text, @"^(-?\d*,?\d* ?)*$");
        }

        private void GetAccountingData(object sender, RoutedEventArgs e)
        {
            var accountingData = ReadValues();

            if (accountingData == null) return;

            var data = accountingData.ToArray();
            var avgTask = _accountingService.GetAverageAsync(data);
            var maxTask = _accountingService.GetMaximumAsync(data);
            var minTask = _accountingService.GetMinimumAsync(data);
            var stdDevTask = _accountingService.GetStandardDeviationAsync(data);

            avgTask.ContinueWith(taskResult =>
            {
                AverageLabel.Dispatcher.BeginInvoke((Action)(() =>
               {
                   AverageLabel.Content = taskResult.Result.Result.ToString("N2");
                   if (taskResult.Result.Success)
                   {
                       AverageLabel.BorderBrush = _defaultBorderBrush;
                   }
                   else
                   {
                       AverageLabel.BorderBrush = Brushes.Red;
                       Print(taskResult.Result.ErrorMessage);
                   }
               }));
            });

            maxTask.ContinueWith(taskResult =>
            {
                MaximumLabel.Dispatcher.BeginInvoke((Action)(() =>
                {
                    MaximumLabel.Content = taskResult.Result.Result.ToString("N2");
                    if (taskResult.Result.Success)
                    {
                        MaximumLabel.BorderBrush = _defaultBorderBrush;
                    }
                    else
                    {
                        MaximumLabel.BorderBrush = Brushes.Red;
                        Print(taskResult.Result.ErrorMessage);
                    }
                }));
            });

            minTask.ContinueWith(taskResult =>
            {
                MinimumLabel.Dispatcher.BeginInvoke((Action)(() =>
                {
                    MinimumLabel.Content = taskResult.Result.Result.ToString("N2");
                    if (taskResult.Result.Success)
                    {
                        MinimumLabel.BorderBrush = _defaultBorderBrush;
                    }
                    else
                    {
                        MinimumLabel.BorderBrush = Brushes.Red;
                        Print(taskResult.Result.ErrorMessage);
                    }
                }));
            });

            stdDevTask.ContinueWith(taskResult =>
            {
                StandardDeviationLabel.Dispatcher.BeginInvoke((Action)(() =>
                {
                    StandardDeviationLabel.Content = taskResult.Result.Result.ToString("N2");
                    if (taskResult.Result.Success)
                    {
                        StandardDeviationLabel.BorderBrush = _defaultBorderBrush;
                    }
                    else
                    {
                        StandardDeviationLabel.BorderBrush = Brushes.Red;
                        Print(taskResult.Result.ErrorMessage);
                    }
                }));
            });
        }

        private IList<double> ReadValues()
        {
            if (AccountingDataTextBox.Text.Equals(string.Empty) || AccountingDataTextBox.Text.Equals(" "))
            {
                AccountingDataTextBox.BorderBrush = Brushes.Red;
                return null;
            }

            try
            {
                var text = AccountingDataTextBox.Text;
                var splittedText = text.Split(' ');

                var data = new List<double>(100);

                data.AddRange(
                    from numberLiteral in splittedText
                    where numberLiteral != string.Empty
                    select double.Parse(numberLiteral));

                data.Reverse();
                return data;
            }
            catch (Exception e)
            {
                AccountingDataTextBox.BorderBrush = Brushes.Red;
                return null;
            }
        }

        private void Print(string text)
        {
            MainConsole.Dispatcher.BeginInvoke((Action)(() =>
            {
                if (_autoClearMainConsole) MainConsole.Text = string.Empty;
                MainConsole.AppendText(text + "\n" + "*".PadLeft(100, '*') + "\n");
                MainConsole.Focus();
                MainConsole.CaretIndex = MainConsole.Text.Length;
                MainConsole.ScrollToEnd();
            }));
        }

        private void SetDefaultBorderBrush(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).BorderBrush = _defaultBorderBrush;
        }

        private void PrintPublication(object sender, RoutedEventArgs e)
        {
            var publicationTitle = ReadPublicationTitle();

            var publicationContent = _libraryService.GetFileAsync(publicationTitle);
            publicationContent.ContinueWith((taskResult) =>
            {
                Print(taskResult.Result);
            });
        }

        private string ReadPublicationTitle()
        {
            if (PublicationTitleTextBox.Text.Equals(string.Empty) || PublicationTitleTextBox.Text.Equals(" "))
            {
                PublicationTitleTextBox.BorderBrush = Brushes.Red;
                return null;
            }

            var text = Regex.Replace(PublicationTitleTextBox.Text, " +", "_").ToLower();

            return text;
        }

        private void AcceptOnlyNumeric(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "[0-9]");
        }

        private void PublicationExists(object sender, RoutedEventArgs e)
        {
            var publicationTitle = ReadPublicationTitle();

            var task = _libraryService.FileExistsAsync(publicationTitle);

            task.ContinueWith(taskResult =>
            {
                Print(taskResult.Result
                    ? $"Publikacja o tytule {publicationTitle} jest dostępna"
                    : $"Publikacja o tytule {publicationTitle} nie istnieje");
            });
        }

        private void GetPublicationsList(object sender, RoutedEventArgs e)
        {
            var task = _libraryService.GetAllFileNamesAsync();

            task.ContinueWith(taskResult =>
            {
                Print(taskResult.Result);
            });
        }

        private void GetLines(object sender, RoutedEventArgs e)
        {
            var publicationTitle = ReadPublicationTitle();

            var lowerBoundary = 1;
            if (!string.IsNullOrEmpty(LowerBoundaryTextBox.Text))
                lowerBoundary = int.Parse(LowerBoundaryTextBox.Text);

            var upperBoundary = 0;
            if (!string.IsNullOrEmpty(UpperBoundaryTextBox.Text))
                upperBoundary = int.Parse(UpperBoundaryTextBox.Text);

            if (upperBoundary != 0 && upperBoundary < lowerBoundary)
            {
                UpperBoundaryTextBox.BorderBrush = Brushes.Red;
                return;
            }

            var task = _libraryService.GetLinesAsync(publicationTitle, lowerBoundary, upperBoundary);

            task.ContinueWith(taskResult =>
            {
                Print(taskResult.Result);
            });
        }

        private void ClearConsole(object sender, RoutedEventArgs e)
        {
            MainConsole.Text = string.Empty;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var isChecked = ((CheckBox)sender).IsChecked;
            _autoClearMainConsole = isChecked != null && isChecked.Value;
        }

        private void PublicationTitleTextBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                PrintPublication(sender, e);
        }

        private void AccountingDataTextBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                GetAccountingData(sender, e);
        }
    }
}
