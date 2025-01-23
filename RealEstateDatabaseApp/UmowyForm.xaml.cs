using System.Data;
using System.Windows.Controls;
using System.Windows;
using System;

namespace RealEstateDatabaseApp;

public partial class UmowyForm : UserControl
{
    public UmowyForm()
    {
        InitializeComponent();
    }

    private void AddUmowa(object sender, RoutedEventArgs e)
    {
        string type = TxtDealType.Text;
        string rent = TxtRent.Text;
        string deposit = TxtDeposit.Text;
        string dateStart = TxtDateStart.SelectedDate.HasValue
            ? TxtDateStart.SelectedDate.Value.ToString("yyyy-MM-dd")
            : string.Empty;
        string dateEnd = TxtDateEnd.SelectedDate.HasValue
            ? TxtDateEnd.SelectedDate.Value.ToString("yyyy-MM-dd")
            : string.Empty;
        string dateSigned = TxtDateSigned.SelectedDate.HasValue
            ? TxtDateSigned.SelectedDate.Value.ToString("yyyy-MM-dd")
            : string.Empty;
        string idOwner = TxtIdOwner.Text;
        string idRenter = TxtIdRenter.Text;
        string idEstate = TxtIdEstate.Text;

        if (!string.IsNullOrWhiteSpace(type) && !string.IsNullOrWhiteSpace(rent) &&
            !string.IsNullOrWhiteSpace(deposit) && !string.IsNullOrWhiteSpace(dateStart) && !string.IsNullOrWhiteSpace(type) && !string.IsNullOrWhiteSpace(dateEnd) &&
            !string.IsNullOrWhiteSpace(dateSigned) && !string.IsNullOrWhiteSpace(idOwner) && !string.IsNullOrWhiteSpace(idRenter) && !string.IsNullOrWhiteSpace(idEstate))
        {
            string query =
                $"INSERT INTO umowa (typ_umowy, czynsz, kaucja, data_rozpoczecia, data_zakonczenia, data_zawarcia, id_wlasciciela, id_najemcy, id_nieruchomosci) VALUES ('{type}', '{rent}', '{deposit}', '{dateStart}', '{dateEnd}', '{dateSigned}', '{idOwner}', '{idRenter}', '{idEstate}')";
            Database.ExecuteNonQuery(query);

            // Odśwież dane w tabeli
            (this.Parent as ContentControl)?.RaiseEvent(new RoutedEventArgs(MainWindow.RefreshData));
            MessageBox.Show("Umowa została dodana.");
        }
        else
        {
            MessageBox.Show("Wszystkie pola muszą być wypełnione!");
        }
    }

    private void DeleteUmowa(object sender, RoutedEventArgs e)
    {
        string type = TxtDealType.Text;
        string rent = TxtRent.Text;
        string deposit = TxtDeposit.Text;
        string dateStart = TxtDateStart.SelectedDate.HasValue
            ? TxtDateStart.SelectedDate.Value.ToString("yyyy-MM-dd")
            : string.Empty;
        string dateEnd = TxtDateEnd.SelectedDate.HasValue
            ? TxtDateEnd.SelectedDate.Value.ToString("yyyy-MM-dd")
            : string.Empty;
        string dateSigned = TxtDateSigned.SelectedDate.HasValue
            ? TxtDateSigned.SelectedDate.Value.ToString("yyyy-MM-dd")
            : string.Empty;
        string idOwner = TxtIdOwner.Text;
        string idRenter = TxtIdRenter.Text;
        string idEstate = TxtIdEstate.Text;

        if (string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(rent) ||
             string.IsNullOrWhiteSpace(deposit) || string.IsNullOrWhiteSpace(dateStart) || string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(dateEnd) ||
             string.IsNullOrWhiteSpace(dateSigned) || string.IsNullOrWhiteSpace(idOwner) || string.IsNullOrWhiteSpace(idRenter) || string.IsNullOrWhiteSpace(idEstate))
        {
            MessageBox.Show("Proszę wypełnić wszystkie pola przed usunięciem.", "Błąd", MessageBoxButton.OK,
                MessageBoxImage.Warning);
            return;
        }
            string query =
                $"DELETE FROM umowa WHERE typ_umowy = '{type}' AND czynsz = '{rent}' AND kaucja = '{deposit}' AND data_rozpoczecia = '{dateStart}' AND data_zakonczenia = '{dateEnd}' AND data_zawarcia = '{dateSigned}' AND id_wlasciciela = '{idOwner}' AND id_najemcy = '{idRenter}' AND id_nieruchomosci = '{idOwner}'";

            int rowsAffected = Database.ExecuteNonQuery(query);

            if (rowsAffected > 0)
            {
                MessageBox.Show("Umowa została usunięta.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);

                // Odśwież dane w tabeli
                (this.Parent as ContentControl)?.RaiseEvent(new RoutedEventArgs(MainWindow.RefreshData));
            }
            else
            {
                MessageBox.Show("Nie znaleziono umowy o podanych danych.", "Brak danych", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
    }

    private void LoadWlasciciele(object sender, EventArgs e)
    {
            // Zapytanie SQL, aby pobrać właścicieli
            string query = "SELECT id_wlasciciela FROM wlasciciel";
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
                comboBox.Items.Add(row["id_wlasciciela"].ToString());
            }
    }
    private void LoadNajemcy(object sender, EventArgs e)
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
    
    private void LoadNieruchomosci(object sender, EventArgs e)
    {
        // Zapytanie SQL, aby pobrać właścicieli
        string query = "SELECT id_nieruchomosci FROM nieruchomosc";
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
            comboBox.Items.Add(row["id_nieruchomosci"].ToString());
        }
    }


}