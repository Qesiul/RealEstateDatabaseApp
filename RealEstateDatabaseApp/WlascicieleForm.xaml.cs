using System.Windows.Controls;
using System.Windows;

namespace RealEstateDatabaseApp;

public partial class WlascicieleForm : UserControl
{
    public WlascicieleForm()
    {
        InitializeComponent();
    }

    private void AddWlasciciel(object sender, RoutedEventArgs e)
    {
        string name = TxtName.Text;
        string surname = TxtSurname.Text;
        string contact = TxtContact.Text;

        if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(surname) &&
            !string.IsNullOrWhiteSpace(contact))
        {
            string query = $"INSERT INTO wlasciciel (imie, nazwisko, kontakt) VALUES ('{name}', '{surname}', '{contact}')";
            Database.ExecuteNonQuery(query);

            // Odśwież dane w tabeli
            (this.Parent as ContentControl)?.RaiseEvent(new RoutedEventArgs(MainWindow.RefreshData));
            MessageBox.Show("Wlasciciel został dodany.");
        }
        else
        {
            MessageBox.Show("Wszystkie pola muszą być wypełnione!");
        }
    }

    private void DeleteWlasciciel(object sender, RoutedEventArgs e)
    {
        string name = TxtName.Text.Trim();
        string surname = TxtSurname.Text.Trim();
        string contact = TxtContact.Text.Trim();

        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname) || string.IsNullOrWhiteSpace(contact))
        {
            MessageBox.Show("Proszę wypełnić wszystkie pola przed usunięciem.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
            string query =
                $"DELETE FROM wlasciciel WHERE imie = '{name}' AND nazwisko = '{surname}' AND kontakt = '{contact}'";

            int rowsAffected = Database.ExecuteNonQuery(query);

            if (rowsAffected > 0)
            {
                MessageBox.Show("Wlasciciel został usunięty.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);

                // Odśwież dane w tabeli
                (this.Parent as ContentControl)?.RaiseEvent(new RoutedEventArgs(MainWindow.RefreshData));
            }
            else
            {
                MessageBox.Show("Nie znaleziono wlasciciela o podanych danych.", "Brak danych", MessageBoxButton.OK, MessageBoxImage.Information);
            }
    }
    

}