using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RealEstateDatabaseApp
{
    public partial class EstateBooking : UserControl
    {
        public EstateBooking()
        {
            InitializeComponent();
            Loaded += EstateBooking_Loaded;
        }

        private void EstateBooking_Loaded(object sender, RoutedEventArgs e)
        {
            LoadEstateAddresses();
        }

        private void LoadEstateAddresses()
        {
            try
            {
                // Pobieramy listę nieruchomości
                string query = "SELECT n.adres FROM nieruchomosc n LEFT JOIN umowa u ON n.id_nieruchomosci = u.id_nieruchomosci WHERE u.id_nieruchomosci IS NULL"; 
                DataTable data = Database.ExecuteQuery(query);

                ComboBoxEstateAddress.Items.Clear();

                if (data.Rows.Count > 0)
                {
                    foreach (DataRow row in data.Rows)
                    {
                        ComboBoxEstateAddress.Items.Add(row["adres"].ToString());
                    }
                }
                else
                {
                    // Jeżeli brak nieruchomości
                    ComboBoxEstateAddress.Items.Add("Brak dostępnych nieruchomości");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas ładowania nieruchomości: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BookEstate_Click(object sender, RoutedEventArgs e)
        {
            string selectedAddress = ComboBoxEstateAddress.SelectedItem?.ToString();
            string name = TxtName.Text;
            string surname = TxtSurname.Text;
            string contact = TxtContact.Text;
            string meetingDate = DatePickerMeeting.SelectedDate?.ToString("yyyy-MM-dd");
            string rentalType = (ComboBoxRentalType.SelectedItem as ComboBoxItem)?.Content.ToString();
            string notes = TxtNotes.Text;

            if (string.IsNullOrWhiteSpace(selectedAddress) ||
                string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(surname) ||
                string.IsNullOrWhiteSpace(contact) ||
                string.IsNullOrWhiteSpace(meetingDate) ||
                string.IsNullOrWhiteSpace(rentalType))
            {
                MessageBox.Show("Wszystkie wymagane pola (Nieruchomość, Kontakt, Data spotkania, Typ wynajmu) muszą być wypełnione!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                string query = $@"
                    INSERT INTO rezerwacje (adres_nieruchomosci, imie, nazwisko, kontakt, data_spotkania, typ_wynajmu, uwagi)
                    VALUES ('{selectedAddress}', '{name}', '{surname}', '{contact}', '{meetingDate}', '{rentalType}', '{notes}')
                ";

                Database.ExecuteNonQuery(query);
                MessageBox.Show("Rezerwacja została pomyślnie zapisana!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);

                // Wyczyść pola po pomyślnym dodaniu
                ComboBoxEstateAddress.SelectedItem = null;
                TxtContact.Clear();
                DatePickerMeeting.SelectedDate = null;
                ComboBoxRentalType.SelectedIndex = 0;
                TxtNotes.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas rezerwacji: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
