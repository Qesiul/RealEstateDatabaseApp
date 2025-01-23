using System.Data;
using System.Windows.Controls;
using System.Windows;
using System;

namespace RealEstateDatabaseApp;

public partial class PlatnosciForm : UserControl
{
    public PlatnosciForm()
    {
        InitializeComponent();
    }

    private void AddPlatnosc(object sender, RoutedEventArgs e)
{
    string amount = TxtAmount.Text;
    string paymentType = TxtPaymentType.Text;

    // Sprawdzamy, czy wybrano datę i formatujemy ją do "yyyy-MM-dd"
    string paymentDate = TxtPaymentDate.SelectedDate.HasValue 
        ? TxtPaymentDate.SelectedDate.Value.ToString("yyyy-MM-dd") 
        : string.Empty;

    string description = TxtDesc.Text;
    string deal = TxtDeal.Text; // id_umowy
    string handyman = TxtHandyman.Text; // id_serwisanta
    string admin = TxtAdmin.Text; // id_administratora

    // Walidacja warunków
    if (string.IsNullOrWhiteSpace(amount) || string.IsNullOrWhiteSpace(paymentType) || string.IsNullOrWhiteSpace(paymentDate))
    {
        MessageBox.Show("Kwota, rodzaj płatności oraz data płatności są wymagane!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
        return;
    }

    // Sprawdzanie relacji: id_umowy XOR (id_serwisanta AND id_administratora)
    if (!string.IsNullOrWhiteSpace(deal) && (!string.IsNullOrWhiteSpace(handyman) || !string.IsNullOrWhiteSpace(admin)))
    {
        MessageBox.Show("Gdy podano id umowy, id serwisanta i id administratora muszą być puste.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
        return;
    }
    if ((string.IsNullOrWhiteSpace(deal) && (string.IsNullOrWhiteSpace(handyman) || string.IsNullOrWhiteSpace(admin))))
    {
        MessageBox.Show("Jeśli id umowy jest puste, id serwisanta i id administratora muszą być wypełnione.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
        return;
    }

    // Tworzenie zapytania SQL
    string query = $"INSERT INTO platnosc (kwota, rodzaj_platnosci, data_platnosci, opis, id_umowy, id_serwisanta, id_administratora) " +
                   $"VALUES ('{amount}', '{paymentType}', '{paymentDate}', " +
                   $"'{description}', " +
                   $"{(string.IsNullOrWhiteSpace(deal) ? "NULL" : $"'{deal}'")}, " +
                   $"{(string.IsNullOrWhiteSpace(handyman) ? "NULL" : $"'{handyman}'")}, " +
                   $"{(string.IsNullOrWhiteSpace(admin) ? "NULL" : $"'{admin}'")})";

    // Wykonanie zapytania
    try
    {
        Database.ExecuteNonQuery(query);
        MessageBox.Show("Płatność została dodana.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
        (this.Parent as ContentControl)?.RaiseEvent(new RoutedEventArgs(MainWindow.RefreshData));
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Wystąpił błąd podczas dodawania płatności: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}



    private void DeletePlatnosc(object sender, RoutedEventArgs e)
{
    string amount = TxtAmount.Text;
    string paymentType = TxtPaymentType.Text;
    string paymentDate = TxtPaymentDate.Text;
    string deal = TxtDeal.Text;
    string handyman = TxtHandyman.Text;
    string admin = TxtAdmin.Text;

    if (string.IsNullOrWhiteSpace(amount) || string.IsNullOrWhiteSpace(paymentType) || string.IsNullOrWhiteSpace(paymentDate))
    {
        MessageBox.Show("Kwota, rodzaj płatności oraz data płatności są wymagane!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
        return;
    }

    string query;

    if (!string.IsNullOrWhiteSpace(deal)) // Usunięcie na podstawie id_umowy
    {
        query = $"DELETE FROM platnosc WHERE kwota = '{amount}' AND rodzaj_platnosci = '{paymentType}' AND data_platnosci = '{paymentDate}' AND id_umowy = '{deal}'";
    }
    else if (!string.IsNullOrWhiteSpace(handyman) && !string.IsNullOrWhiteSpace(admin)) // Usunięcie na podstawie id_serwisanta i id_administratora
    {
        query = $"DELETE FROM platnosc WHERE kwota = '{amount}' AND rodzaj_platnosci = '{paymentType}' AND data_platnosci = '{paymentDate}' AND id_serwisanta = '{handyman}' AND id_administratora = '{admin}'";
    }
    else
    {
        MessageBox.Show("Proszę podać id umowy lub id serwisanta oraz id administratora.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
        return;
    }

    // Wykonanie zapytania
    try
    {
        int rowsAffected = Database.ExecuteNonQuery(query);

        if (rowsAffected > 0)
        {
            MessageBox.Show("Płatność została usunięta.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
            (this.Parent as ContentControl)?.RaiseEvent(new RoutedEventArgs(MainWindow.RefreshData));
        }
        else
        {
            MessageBox.Show("Nie znaleziono płatności o podanych danych.", "Brak danych", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Wystąpił błąd podczas usuwania płatności: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}


    private void LoadUmowa(object sender, EventArgs e)
    {
            // Zapytanie SQL, aby pobrać właścicieli
            string query = "SELECT id_umowy FROM umowa";
            DataTable data = Database.ExecuteQuery(query);

            // Odczytanie ComboBox z sender
            ComboBox comboBox = sender as ComboBox;
            if (comboBox == null)
                return;

            // Wyczyść istniejące elementy w ComboBox
            comboBox.Items.Clear();

            // Dodaj każdy wiersz z wyniku zapytania jako element do ComboBox
            foreach (DataRow row in data.Rows)
            {
                comboBox.Items.Add(row["id_umowy"].ToString());
            }
    }
    
    private void LoadSerwisant(object sender, EventArgs e)
    {
        // Zapytanie SQL, aby pobrać właścicieli
        string query = "SELECT id_serwisanta FROM serwisant";
        DataTable data = Database.ExecuteQuery(query);

        // Odczytanie ComboBox z sender
        ComboBox comboBox = sender as ComboBox;
        if (comboBox == null)
            return;

        // Wyczyść istniejące elementy w ComboBox
        comboBox.Items.Clear();

        // Dodaj każdy wiersz z wyniku zapytania jako element do ComboBox
        foreach (DataRow row in data.Rows)
        {
            comboBox.Items.Add(row["id_serwisanta"].ToString());
        }
    }
    
    private void LoadAdministrator(object sender, EventArgs e)
    {
        // Zapytanie SQL, aby pobrać właścicieli
        string query = "SELECT id_administratora FROM administratorbudynku";
        DataTable data = Database.ExecuteQuery(query);

        // Odczytanie ComboBox z sender
        ComboBox comboBox = sender as ComboBox;
        if (comboBox == null)
            return;

        // Wyczyść istniejące elementy w ComboBox
        comboBox.Items.Clear();

        // Dodaj każdy wiersz z wyniku zapytania jako element do ComboBox
        foreach (DataRow row in data.Rows)
        {
            comboBox.Items.Add(row["id_administratora"].ToString());
        }
    }

}