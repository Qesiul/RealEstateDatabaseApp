using System.Windows.Controls;
using System.Windows;

namespace RealEstateDatabaseApp;

public partial class NajemcyForm : UserControl
{
    public NajemcyForm()
    {
        InitializeComponent();
    }

    private void AddNajemca(object sender, RoutedEventArgs e)
    {
        string name = TxtName.Text;
        string surname = TxtSurname.Text;
        string contact = TxtContact.Text;

        if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(surname) &&
            !string.IsNullOrWhiteSpace(contact))
        {
            string query = $"INSERT INTO najemca (imie, nazwisko, kontakt) VALUES ('{name}', '{surname}', '{contact}')";
            Database.ExecuteNonQuery(query);

            // Odśwież dane w tabeli
            (this.Parent as ContentControl)?.RaiseEvent(new RoutedEventArgs(MainWindow.RefreshData));
            MessageBox.Show("Najemca został dodany.");
        }
        else
        {
            MessageBox.Show("Wszystkie pola muszą być wypełnione!");
        }
    }

    private void DeleteNajemca(object sender, RoutedEventArgs e)
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
                $"DELETE FROM najemca WHERE imie = '{name}' AND nazwisko = '{surname}' AND kontakt = '{contact}'";

            int rowsAffected = Database.ExecuteNonQuery(query);

            if (rowsAffected > 0)
            {
                MessageBox.Show("Najemca został usunięty.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);

                // Odśwież dane w tabeli
                (this.Parent as ContentControl)?.RaiseEvent(new RoutedEventArgs(MainWindow.RefreshData));
            }
            else
            {
                MessageBox.Show("Nie znaleziono najemcy o podanych danych.", "Brak danych", MessageBoxButton.OK, MessageBoxImage.Information);
            }
    }
    

}