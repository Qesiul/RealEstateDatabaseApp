using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace RealEstateDatabaseApp
{
    public partial class RezerwacjeForm : UserControl
    {
        public RezerwacjeForm()
        {
            InitializeComponent();
        }

        private void AddRezerwacja(object sender, RoutedEventArgs e)
        {
            string estateAddress = TxtEstateAddress.Text;
            string name = TxtName.Text;
            string surname = TxtSurname.Text;
            string contact = TxtContact.Text;
            string meetingDate = TxtMeetingDate.SelectedDate.HasValue
                ? TxtMeetingDate.SelectedDate.Value.ToString("yyyy-MM-dd")
                : string.Empty;
            string rentalType = (TxtRentalType.SelectedItem as ComboBoxItem)?.Content.ToString();
            string notes = TxtNotes.Text;

            if (string.IsNullOrWhiteSpace(estateAddress) || estateAddress == "Wybierz adres nieruchomości" ||
                string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(surname) ||
                string.IsNullOrWhiteSpace(contact) ||
                string.IsNullOrWhiteSpace(meetingDate) ||
                string.IsNullOrWhiteSpace(rentalType))
            {
                MessageBox.Show("Wszystkie wymagane pola muszą być wypełnione!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                string query = $@"
                    INSERT INTO rezerwacje (adres_nieruchomosci, imie, nazwisko, kontakt, data_spotkania, typ_wynajmu, uwagi)
                    VALUES ('{estateAddress}', '{name}', '{surname}', '{contact}', '{meetingDate}', '{rentalType}', '{notes}')
                ";

                Database.ExecuteNonQuery(query);

                (this.Parent as ContentControl)?.RaiseEvent(new RoutedEventArgs(MainWindow.RefreshData));
                MessageBox.Show("Rezerwacja została pomyślnie dodana!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas dodawania rezerwacji: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteRezerwacja(object sender, RoutedEventArgs e)
        {
            string estateAddress = TxtEstateAddress.Text;
            string name = TxtName.Text;
            string surname = TxtSurname.Text;
            string contact = TxtContact.Text;
            string meetingDate = TxtMeetingDate.SelectedDate.HasValue
                ? TxtMeetingDate.SelectedDate.Value.ToString("yyyy-MM-dd")
                : string.Empty;
            string rentalType = (TxtRentalType.SelectedItem as ComboBoxItem)?.Content.ToString();
            string notes = TxtNotes.Text;

            if (string.IsNullOrWhiteSpace(estateAddress) ||
                string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(surname) ||
                string.IsNullOrWhiteSpace(contact) ||
                string.IsNullOrWhiteSpace(meetingDate) ||
                string.IsNullOrWhiteSpace(rentalType))
            {
                MessageBox.Show("Proszę wypełnić wszystkie wymagane pola przed usunięciem.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                string query = $@"
                    DELETE FROM rezerwacje 
                    WHERE adres_nieruchomosci = '{estateAddress}' 
                      AND imie = '{name}'
                      AND nazwisko = '{surname}'
                      AND kontakt = '{contact}'
                      AND data_spotkania = '{meetingDate}'
                      AND typ_wynajmu = '{rentalType}'
                      AND uwagi = '{notes}'
                ";

                int rowsAffected = Database.ExecuteNonQuery(query);

                if (rowsAffected > 0)
                {
                    (this.Parent as ContentControl)?.RaiseEvent(new RoutedEventArgs(MainWindow.RefreshData));
                    MessageBox.Show("Rezerwacja została usunięta.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Nie znaleziono rezerwacji o podanych danych.", "Brak danych", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas usuwania rezerwacji: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadEstateAddresses(object sender, EventArgs e)
        {
            // Jeśli chcesz tylko dostępne nieruchomości, zastosuj podobne zapytanie jak wcześniej:
            // Tylko nieruchomości, które nie są zajęte przez umowę lub rezerwację.
            // Jeśli chcesz wszystkie, wystarczy SELECT adres FROM nieruchomosc;
            string query = @"
                SELECT n.adres
                FROM nieruchomosc n
                LEFT JOIN umowa u ON n.id_nieruchomosci = u.id_nieruchomosci
                WHERE u.id_nieruchomosci IS NULL;
            ";

            try
            {
                DataTable data = Database.ExecuteQuery(query);

                ComboBox comboBox = sender as ComboBox;
                if (comboBox == null)
                    return;

                comboBox.Items.Clear();

                if (data.Rows.Count > 0)
                {
                    foreach (DataRow row in data.Rows)
                    {
                        comboBox.Items.Add(row["adres"].ToString());
                    }
                }
                else
                {
                    comboBox.Items.Add("Brak dostępnych nieruchomości");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas ładowania nieruchomości: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
