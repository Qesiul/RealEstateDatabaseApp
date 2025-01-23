using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace RealEstateDatabaseApp;

public class TextBoxPlaceholder
{
    public static readonly DependencyProperty PlaceholderTextProperty =
        DependencyProperty.RegisterAttached(
            "PlaceholderText",
            typeof(string),
            typeof(TextBoxPlaceholder),
            new PropertyMetadata(string.Empty, OnPlaceholderTextChanged));

    public static string GetPlaceholderText(TextBox textBox)
    {
        return (string)textBox.GetValue(PlaceholderTextProperty);
    }

    public static void SetPlaceholderText(TextBox textBox, string value)
    {
        textBox.SetValue(PlaceholderTextProperty, value);
    }

    private static void OnPlaceholderTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is TextBox textBox)
        {
            textBox.GotFocus -= RemovePlaceholder;
            textBox.LostFocus -= AddPlaceholder;

            if (!string.IsNullOrEmpty((string)e.NewValue))
            {
                textBox.GotFocus += RemovePlaceholder;
                textBox.LostFocus += AddPlaceholder;
                AddPlaceholder(textBox, null); // Dodaj placeholder na starcie
            }
        }
    }

    private static void RemovePlaceholder(object sender, RoutedEventArgs e)
    {
        if (sender is TextBox textBox && textBox.Text == GetPlaceholderText(textBox))
        {
            textBox.Text = string.Empty;
            textBox.Foreground = Brushes.Black; // Zmień kolor tekstu na domyślny
        }
    }

    private static void AddPlaceholder(object sender, RoutedEventArgs e)
    {
        if (sender is TextBox textBox && string.IsNullOrWhiteSpace(textBox.Text))
        {
            textBox.Text = GetPlaceholderText(textBox);
            textBox.Foreground = Brushes.Gray; // Placeholder jest szary
        }
    }
}