using System.Data;
using System.Windows.Controls;
using System.Windows;
using System;

namespace RealEstateDatabaseApp;

public partial class NieruchomosciForm : UserControl
{
    public NieruchomosciForm()
    {
        InitializeComponent();
    }

    private void AddNieruchomosc(object sender, RoutedEventArgs e)
    {
        string type = TxtType.Text;
        string area = TxtArea.Text;
        string rooms = TxtRooms.Text;
        string adress = TxtAdress.Text;
        string owner = TxtOwner.Text;
        string neighbour = TxtIdNeighbour.Text;

        if (!string.IsNullOrWhiteSpace(type) && !string.IsNullOrWhiteSpace(area) &&
            !string.IsNullOrWhiteSpace(adress) && !string.IsNullOrWhiteSpace(owner))
        {
            string query =
                $"INSERT INTO nieruchomosc (typ, metraz, liczba_pokoi, adres, id_wlasciciela, id_sasiada) VALUES ('{type}', '{area}', '{rooms}', '{adress}', '{owner}', '{neighbour}')";
            Database.ExecuteNonQuery(query);

            // Odśwież dane w tabeli
            (this.Parent as ContentControl)?.RaiseEvent(new RoutedEventArgs(MainWindow.RefreshData));
            MessageBox.Show("Nieruchomosc została dodana.");
        }
        else
        {
            MessageBox.Show("Wszystkie pola muszą być wypełnione!");
        }
    }

    private void DeleteNieruchomosc(object sender, RoutedEventArgs e)
    {
        string type = TxtType.Text;
        string area = TxtArea.Text;
        string rooms = TxtRooms.Text;
        string adress = TxtAdress.Text;
        string owner = TxtOwner.Text;
        string neighbour = TxtIdNeighbour.Text;

        if (string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(area) || string.IsNullOrWhiteSpace(adress) ||
            string.IsNullOrWhiteSpace(owner))
        {
            MessageBox.Show("Proszę wypełnić wszystkie pola przed usunięciem.", "Błąd", MessageBoxButton.OK,
                MessageBoxImage.Warning);
            return;
        }
            string query =
                $"DELETE FROM nieruchomosc WHERE typ = '{type}' AND metraz = '{area}' AND liczba_pokoi = '{rooms}' AND adres = '{adress}' AND id_wlasciciela = '{owner}' AND id_sasiada = '{neighbour}'";

            int rowsAffected = Database.ExecuteNonQuery(query);

            if (rowsAffected > 0)
            {
                MessageBox.Show("Nieruchomosc została usunięta.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);

                // Odśwież dane w tabeli
                (this.Parent as ContentControl)?.RaiseEvent(new RoutedEventArgs(MainWindow.RefreshData));
            }
            else
            {
                MessageBox.Show("Nie znaleziono nieruchomosci o podanych danych.", "Brak danych", MessageBoxButton.OK,
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
    private void LoadNeighbours(object sender, EventArgs e)
    {
            string query = "SELECT id_nieruchomosci FROM nieruchomosc";
            DataTable data = Database.ExecuteQuery(query);

            ComboBox comboBox = sender as ComboBox;
            if (comboBox == null)
                return;

            comboBox.Items.Clear();

            foreach (DataRow row in data.Rows)
            {
                comboBox.Items.Add(row["id_nieruchomosci"].ToString());
            }
    }


}