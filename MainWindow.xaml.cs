using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Change_Return
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Set up variables
        private decimal remainder = 0;
        private decimal missing = 0m;
        private readonly decimal q = 0.25m;
        private readonly decimal d = 0.10m;
        private readonly decimal n = 0.05m;
        private readonly decimal p = 0.01m;
        private decimal cost;
        private decimal total;
        private decimal amount;
        private int operation = 0;
        private bool IsValid = true;

        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        // Calcuate
        private void BtnCalculate_Click(object sender, RoutedEventArgs e)
        {
            // Reset Variable
            IsValid = true;

            // Reset Border Color If Need Be
            TbCost.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xA2, 0xA4, 0xA5));
            TbAmount.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xA2, 0xA4, 0xA5));

            // Remove Warning Message if Visible
            WarningMessage.Text = "";

            // If TextFields are empty, do not continue
            if (TbCost.Text == "" || TbAmount.Text == "") return;

            // Check if Cost is in the right format
            try
            {
                cost = decimal.Parse(TbCost.Text);
            }
            catch (Exception)
            {
                TbCost.BorderBrush = new SolidColorBrush(Colors.Red);
                TbCost.ToolTip = "Incorrect Format";
                IsValid = false;
            }

            // Check if Amount is in the right format
            try
            {
                amount = decimal.Parse(TbAmount.Text);
            }
            catch (Exception)
            {
                TbAmount.BorderBrush = new SolidColorBrush(Colors.Red);
                TbAmount.ToolTip = "Incorrect Format";
                IsValid = false;
            }

            // If TextBoxes Values are Valid
            // Else Stop Execution
            if (IsValid)
            {
                TbCost.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xA2, 0xA4, 0xA5));
                TbAmount.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xA2, 0xA4, 0xA5));
            }
            else
            {
                return;
            }

            // Get and Display Change
            total = (amount - cost);

            // If Cost is More than Amount Paid
            if (cost > amount)
            {
                missing = cost - amount;
                DisplayWarning(missing);
                return;
            }
            TbReturned.Text = total.ToString("C2", CultureInfo.CurrentCulture);

            // Find How Many Dollars and Display How Many Dollars
            // Else Dollars is Zero
            if (total >= 1)
            {
                operation = (int)total;
                remainder = total - operation;
                TbDollarsReturn.Text = operation.ToString();
            }
            else
            {
                remainder = total;
                TbDollarsReturn.Text = operation.ToString();
            }

            // Find How Many Quarters and Display How Many Quarters
            operation = (int)(remainder / q);
            remainder -= (operation * q);
            TbQuartersReturn.Text = operation.ToString();

            // Find How Many Dimes and Display How Many Dimes
            operation = (int)(remainder / d);
            remainder -= (operation * d);
            TbDimesReturn.Text = operation.ToString();

            // Find How Many Nickles and Display How Many Nickles
            operation = (int)(remainder / n);
            remainder -= (operation * n);
            TbNicklesReturn.Text = operation.ToString();

            // Find How Many Pennies and Display How Many Pennies
            operation = (int)(remainder / p);
            TbPenniesReturn.Text = operation.ToString();
        }

        // Clear Text Fields and Display Message
        private void DisplayWarning(decimal missing)
        {
            TbReturned.Text = "";
            TbQuartersReturn.Text = "";
            TbDollarsReturn.Text = "";
            TbDimesReturn.Text = "";
            TbNicklesReturn.Text = "";
            TbPenniesReturn.Text = "";
            WarningMessage.Text = "You need " + missing.ToString("C2", CultureInfo.CurrentCulture) + " more.";
        }

        // Cost TextBox
        private void TbCost_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"[^0-9\.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        // Paid Amount
        private void TbAmount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"[^0-9\.?]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        // Clear Reset Variables, Reset Border Color, Enable Elements, Text Fields
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            remainder = 0;
            missing = 0m;
            cost = 0;
            total = 0;
            amount = 0;
            operation = 0;
            IsValid = true;

            TbCost.ToolTip = "Enter Price Here";
            TbAmount.ToolTip = "Enter Amount Paid Here";

            TbCost.Text = "";
            TbAmount.Text = "";
            TbReturned.Text = "";
            TbDollarsReturn.Text = "";
            TbQuartersReturn.Text = "";
            TbDimesReturn.Text = "";
            TbNicklesReturn.Text = "";
            TbPenniesReturn.Text = "";

            TbCost.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xA2, 0xA4, 0xA5));
            TbAmount.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xA2, 0xA4, 0xA5));
        }
    }
}
