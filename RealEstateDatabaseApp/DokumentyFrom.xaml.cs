using System.Windows.Controls;
using System.Windows;
using System.Data;
using System;

namespace RealEstateDatabaseApp;

public partial class DokumentyFrom : UserControl
{
    public DokumentyFrom()
    {
        InitializeComponent();
    }
    
    private void AddDokument(object sender, RoutedEventArgs e)
    {
        string title = TxtTitle.Text;
        string docType = TxtDocType.Text;
        string estateId = TxtRealEstate.Text;

        if (!string.IsNullOrWhiteSpace(title) && !string.IsNullOrWhiteSpace(docType) &&
            !string.IsNullOrWhiteSpace(estateId))
        {
            string query =
                $"INSERT INTO dokument (nazwa, typ_dokumentu, id_nieruchomosci) VALUES ('{title}', '{docType}', '{estateId}')";
            Database.ExecuteNonQuery(query);

            // Odśwież dane w tabeli
            (this.Parent as ContentControl)?.RaiseEvent(new RoutedEventArgs(MainWindow.RefreshData));
            MessageBox.Show("Dokument został dodany.");
        }
        else
        {
            MessageBox.Show("Wszystkie pola muszą być wypełnione!");
        }
    }

    private void DeleteDokument(object sender, RoutedEventArgs e)
    {
        string title = TxtTitle.Text;
        string docType = TxtDocType.Text;
        string estateId = TxtRealEstate.Text;

        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(docType) || string.IsNullOrWhiteSpace(estateId))
        {
            MessageBox.Show("Proszę wypełnić wszystkie pola przed usunięciem.", "Błąd", MessageBoxButton.OK,
                MessageBoxImage.Warning);
            return;
        }
            string query =
                $"DELETE FROM dokument WHERE nazwa = '{title}' AND typ_dokumentu = '{docType}' AND id_nieruchomosci = '{estateId}'";

            int rowsAffected = Database.ExecuteNonQuery(query);

            if (rowsAffected > 0)
            {
                MessageBox.Show("Dokument zostal usunięty.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);

                // Odśwież dane w tabeli
                (this.Parent as ContentControl)?.RaiseEvent(new RoutedEventArgs(MainWindow.RefreshData));
            }
            else
            {
                MessageBox.Show("Nie znaleziono dokumentu o podanych danych.", "Brak danych", MessageBoxButton.OK,
                    MessageBoxImage.Information);
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