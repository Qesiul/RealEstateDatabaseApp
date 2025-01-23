using System.Data;
using System.Windows.Controls;
using System.Windows;

namespace RealEstateDatabaseApp;

public partial class SerwisanciForm : UserControl
{
    public SerwisanciForm()
    {
        InitializeComponent();
    }

    private void AddSerwisant(object sender, RoutedEventArgs e)
    {
        string handymanType = TxtHandymanType.Text;
        string name = TxtName.Text;
        string surname = TxtSurname.Text;
        string contact = TxtContact.Text;

        if (!string.IsNullOrWhiteSpace(handymanType) && !string.IsNullOrWhiteSpace(name) &&
            !string.IsNullOrWhiteSpace(surname) && !string.IsNullOrWhiteSpace(contact))
        {
            string query =
                $"INSERT INTO serwisant (typ_serwisanta, imie, nazwisko, kontakt) VALUES ('{handymanType}', '{name}', '{surname}', '{contact}')";
            Database.ExecuteNonQuery(query);

            // Odśwież dane w tabeli
            (this.Parent as ContentControl)?.RaiseEvent(new RoutedEventArgs(MainWindow.RefreshData));
            MessageBox.Show("Serwisant został dodany.");
        }
        else
        {
            MessageBox.Show("Wszystkie pola muszą być wypełnione!");
        }
    }

    private void DeleteSerwisant(object sender, RoutedEventArgs e)
    {
        string handymanType = TxtHandymanType.Text;
        string name = TxtName.Text;
        string surname = TxtSurname.Text;
        string contact = TxtContact.Text;

        if (string.IsNullOrWhiteSpace(handymanType) || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname) ||
            string.IsNullOrWhiteSpace(contact))
        {
            MessageBox.Show("Proszę wypełnić wszystkie pola przed usunięciem.", "Błąd", MessageBoxButton.OK,
                MessageBoxImage.Warning);
            return;
        }
            string query =
                $"DELETE FROM serwisant WHERE typ_serwisanta = '{handymanType}' AND imie = '{name}' AND nazwisko = '{surname}' AND kontakt = '{contact}'";

            int rowsAffected = Database.ExecuteNonQuery(query);

            if (rowsAffected > 0)
            {
                MessageBox.Show("Serwisant został usunięty.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);

                // Odśwież dane w tabeli
                (this.Parent as ContentControl)?.RaiseEvent(new RoutedEventArgs(MainWindow.RefreshData));
            }
            else
            {
                MessageBox.Show("Nie znaleziono serwisanta o podanych danych.", "Brak danych", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
    }
}