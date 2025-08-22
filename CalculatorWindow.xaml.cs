using System;
using System.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace CalculatorApp_WPF
{
    public partial class CalculatorWindow : Window
{
    private bool justCalculated = false;

    public CalculatorWindow()
    {
        InitializeComponent();
    }

    private void Number_Click(object sender, RoutedEventArgs e)
    {
        string number = (string)((Button)sender).Content;

        if (justCalculated)
        {
            txtDisplay.Text = "0";
            justCalculated = false;
        }

        if (txtDisplay.Text == "0" && number != ".")
            txtDisplay.Text = number;
        else
            txtDisplay.Text += number;
    }

    private void Operator_Click(object sender, RoutedEventArgs e)
    {
        string op = (string)((Button)sender).Content;

        if (justCalculated)
            justCalculated = false;

        if (txtDisplay.Text.EndsWith("+") || txtDisplay.Text.EndsWith("-") ||
            txtDisplay.Text.EndsWith("×") || txtDisplay.Text.EndsWith("÷"))
        {
            // replace last operator
            txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1) + op;
        }
        else
        {
            txtDisplay.Text += op;
        }
    }

    private void Clear_Click(object sender, RoutedEventArgs e)
    {
        txtDisplay.Text = "0";
        justCalculated = false;
    }

    private void Equals_Click(object sender, RoutedEventArgs e)
    {
        if (justCalculated) return; // prevent re-calculating when pressing "=" again

        try
        {
            string expression = txtDisplay.Text.Replace("×", "*").Replace("÷", "/");

            DataTable dt = new DataTable();
            var result = dt.Compute(expression, "");

            if (result is double d)
            {
                if (double.IsInfinity(d) || double.IsNaN(d))
                    txtDisplay.Text = "Error";
                else
                    txtDisplay.Text = Math.Round(d, 13).ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                txtDisplay.Text = Convert.ToDouble(result).ToString(CultureInfo.InvariantCulture);
            }
        }
        catch
        {
            txtDisplay.Text = "Error";
        }

        justCalculated = true;
    }
}
}
