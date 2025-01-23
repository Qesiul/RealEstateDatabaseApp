using System.Data;
using System.Windows.Controls;
using System.Windows;
using System;

namespace RealEstateDatabaseApp;

public partial class ReklamacjeForm : UserControl
{
    public ReklamacjeForm()
    {
        InitializeComponent();
    }

    private void AddReklamacja(object sender, RoutedEventArgs e)
    {
        string type = TxtComplaintType.Text;
        string date = TxtComplaintDate.SelectedDate.HasValue
            ? TxtComplaintDate.SelectedDate.Value.ToString("yyyy-MM-dd")
            : string.Empty;
        string status = TxtComplaintStatus.Text;
        string description = TxtDesc.Text;
        string idRenter = TxtIdRenter.Text;
        string idAdmin = TxtIdAdmin.Text;

        // Walidacja wymaganych pól
        if (string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(date) ||
            string.IsNullOrWhiteSpace(status) || string.IsNullOrWhiteSpace(idRenter))
        {
            MessageBox.Show("Wszystkie wymagane pola muszą być wypełnione!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        // Tworzenie zapytania SQL z obsługą NULL dla opcjonalnych pól
        string query =
            $"INSERT INTO reklamacje (typ_reklamacji, data_zgloszenia, status, opis, id_najemcy, id_administratora) " +
            $"VALUES ('{type}', '{date}', '{status}', '{description}', '{idRenter}', " +
            $"{(string.IsNullOrWhiteSpace(idAdmin) ? "NULL" : $"'{idAdmin}'")})";

        try
        {
            Database.ExecuteNonQuery(query);
            MessageBox.Show("Reklamacja została dodana.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);

            // Odśwież dane w tabeli
            (this.Parent as ContentControl)?.RaiseEvent(new RoutedEventArgs(MainWindow.RefreshData));
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Błąd podczas dodawania reklamacji: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }


    private void DeleteReklamacja(object sender, RoutedEventArgs e)
    {
        string type = TxtComplaintType.Text;
        string date = TxtComplaintDate.SelectedDate.HasValue
            ? TxtComplaintDate.SelectedDate.Value.ToString("yyyy-MM-dd")
            : string.Empty;
        string status = TxtComplaintStatus.Text;
        string idRenter = TxtIdRenter.Text;

        // Walidacja
        if (string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(date) ||
            string.IsNullOrWhiteSpace(status) || string.IsNullOrWhiteSpace(idRenter))
        {
            MessageBox.Show("Proszę wypełnić wszystkie wymagane pola przed usunięciem.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        // Tworzenie zapytania SQL
        string query =
            $"DELETE FROM reklamacje WHERE typ_reklamacji = '{type}' AND data_zgloszenia = '{date}' AND status = '{status}' AND id_najemcy = '{idRenter}'";

        try
        {
            int rowsAffected = Database.ExecuteNonQuery(query);

            if (rowsAffected > 0)
            {
                MessageBox.Show("Reklamacja została usunięta.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);

                // Odśwież dane w tabeli
                (this.Parent as ContentControl)?.RaiseEvent(new RoutedEventArgs(MainWindow.RefreshData));
            }
            else
            {
                MessageBox.Show("Nie znaleziono reklamacji o podanych danych.", "Brak danych", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Błąd podczas usuwania reklamacji: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }


    private void LoadNajemca(object sender, EventArgs e)
    {
            // Zapytanie SQL, aby pobrać właścicieli
            string query = "SELECT id_najemcy FROM najemca";
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
                comboBox.Items.Add(row["id_najemcy"].ToString());
            }
    }
    private void LoadAdmin(object sender, EventArgs e)
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