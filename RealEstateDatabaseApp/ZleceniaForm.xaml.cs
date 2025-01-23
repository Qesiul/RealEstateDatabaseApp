using System.Data;
using System.Windows.Controls;
using System.Windows;
using System;

namespace RealEstateDatabaseApp;

public partial class ZleceniaForm : UserControl
{
    public ZleceniaForm()
    {
        InitializeComponent();
    }

    private void AddZlecenie(object sender, RoutedEventArgs e)
    {
        string type = TxtType.Text;
        string description = TxtDesc.Text;
        string status = TxtStatus.Text;
        string idAdmin = TxtIdAdmin.Text;
        string idHandyman = TxtIdHandyman.Text;

        // Walidacja wymaganych pól
        if (string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(status))
        {
            MessageBox.Show("Wszystkie wymagane pola muszą być wypełnione!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        // Tworzenie zapytania SQL z obsługą NULL dla opcjonalnych pól
        string query =
            $"INSERT INTO zlecenieserwisowe (typ_zlecenia, opis, status, id_administratora, id_serwisanta) VALUES ('{type}', '{description}', '{status}', '{idAdmin}', '{idHandyman}')";

        try
        {
            Database.ExecuteNonQuery(query);
            MessageBox.Show("Zlecenie serwisowe zostało dodane.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);

            // Odśwież dane w tabeli
            (this.Parent as ContentControl)?.RaiseEvent(new RoutedEventArgs(MainWindow.RefreshData));
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Błąd podczas dodawania zlecenia serwisowego: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }


    private void DeleteZlecenie(object sender, RoutedEventArgs e)
    {
        string type = TxtType.Text;
        string description = TxtDesc.Text;
        string status = TxtStatus.Text;
        string idAdmin = TxtIdAdmin.Text;
        string idHandyman = TxtIdHandyman.Text;

        // Walidacja
        if (string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(status))
        {
            MessageBox.Show("Proszę wypełnić wszystkie wymagane pola przed usunięciem.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        // Tworzenie zapytania SQL
        string query =
            $"DELETE FROM zlecenieserwisowe WHERE typ_zlecenia = '{type}' AND status = '{status}'";

        try
        {
            int rowsAffected = Database.ExecuteNonQuery(query);

            if (rowsAffected > 0)
            {
                MessageBox.Show("Zlecenie serwisowe zostało usunięte.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);

                // Odśwież dane w tabeli
                (this.Parent as ContentControl)?.RaiseEvent(new RoutedEventArgs(MainWindow.RefreshData));
            }
            else
            {
                MessageBox.Show("Nie znaleziono zlecenia o podanych danych.", "Brak danych", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Błąd podczas usuwania zlecenia serwisowego: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
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