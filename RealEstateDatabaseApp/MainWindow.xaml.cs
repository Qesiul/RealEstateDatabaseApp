using System.Windows;
using System.Data;
using System.Windows.Controls;

namespace RealEstateDatabaseApp
{
    public partial class MainWindow : Window
    {
        public static readonly RoutedEvent RefreshData = EventManager.RegisterRoutedEvent(
            "RefreshData", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(MainWindow));

        public MainWindow()
        {
            InitializeComponent();
            AddHandler(RefreshData, new RoutedEventHandler(OnRefreshData));
        }

        private void ComboBoxTabele_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedOption = ((ComboBoxItem)((ComboBox)sender).SelectedItem).Content.ToString();
            string query = "";

            switch (selectedOption)
            {
                // Zarządzanie tabelami
                case "Najemcy":
                    DynamicDataForm.Content = new NajemcyForm();
                    query = "SELECT id_najemcy AS IdNajemcy, imie AS Imię, nazwisko AS Nazwisko, kontakt as Kontakt FROM najemca";
                    break;

                case "Nieruchomości":
                    DynamicDataForm.Content = new NieruchomosciForm();
                    query = "SELECT id_nieruchomosci AS IdNieruchomości, adres AS Adres, typ AS Typ, metraz AS Metraż, liczba_pokoi AS LiczbaPokoi FROM nieruchomosc";
                    break;

                case "Dokumenty":
                    DynamicDataForm.Content = new DokumentyFrom();
                    query = "SELECT id_dokumentu AS IdDokumentu, nazwa AS Nazwa, typ_dokumentu AS TypDokumentu FROM dokument";
                    break;

                case "Administratorzy budynku":
                    DynamicDataForm.Content = new AdministratorzyForm();
                    query = "SELECT id_administratora AS IdAdministratora, imie AS Imię, nazwisko AS Nazwisko, kontakt AS Kontakt FROM administratorbudynku";
                    break;

                case "Płatności":
                    DynamicDataForm.Content = new PlatnosciForm();
                    query = "SELECT id_platnosci AS IdPłatności, kwota AS Kwota, rodzaj_platnosci AS RodzajPłatności, data_platnosci AS DataPłatności, opis AS Opis, id_umowy AS IdUmowy, id_serwisanta AS IdSerwisanta, id_administratora AS IdAdministratora FROM platnosc";
                    break;

                case "Reklamacje":
                    DynamicDataForm.Content = new ReklamacjeForm();
                    query = "SELECT id_reklamacji AS IdReklamacji, typ_reklamacji AS TypReklamacji, data_zgloszenia AS DataZgłoszenia, status AS Status, opis AS Opis, id_najemcy AS IdNajemcy, id_administratora AS IdAdministratora FROM reklamacje";
                    break;

                case "Serwisanci":
                    DynamicDataForm.Content = new SerwisanciForm();
                    query = "SELECT id_serwisanta AS IDSerwisanta, typ_serwisanta AS TypSerwisanta, imie AS Imię, nazwisko AS Nazwisko, kontakt AS Kontakt FROM serwisant";
                    break;

                case "Udogodnienia":
                    DynamicDataForm.Content = null;
                    query = "SELECT id_udogodnienia AS IdUdogodnienia, nazwa AS Nazwa FROM udogodnienie";
                    break;

                case "Umowy":
                    DynamicDataForm.Content = new UmowyForm();
                    query = "SELECT id_umowy AS IdUmowy, typ_umowy AS TypUmowy, czynsz AS Czynsz, kaucja AS Kaucja, data_rozpoczecia AS DataRozpoczęcia, data_zakonczenia AS DataZakończenia, data_zawarcia AS DataZawarcia, id_wlasciciela AS IdWłaściciela, id_najemcy AS IdNajemcy, id_nieruchomosci AS IdNieruchomości FROM umowa";
                    break;

                case "Właściciele":
                    DynamicDataForm.Content = new WlascicieleForm();
                    query = "SELECT id_wlasciciela AS IdWłaściciela, imie AS Imię, nazwisko AS Nazwisko, kontakt AS Kontakt FROM wlasciciel";
                    break;

                case "Zlecenia serwisowe":
                    DynamicDataForm.Content = new ZleceniaForm();
                    query = "SELECT id_zlecenia AS IdZlecenia, typ_zlecenia AS TypZlecenia, opis AS Opis, status AS Status, id_administratora AS IdAdministratora, id_serwisanta AS IdSerwisanta FROM zlecenieserwisowe";
                    break;
                
                case "Rezerwacje":
                    DynamicDataForm.Content = new RezerwacjeForm();
                    query = "SELECT id_rezerwacji AS IdRezerwacji, adres_nieruchomosci AS AdresNieruchomosci, imie as Imie, nazwisko AS Nazwisko, kontakt AS Kontakt, data_spotkania AS DataSpotkania, typ_wynajmu AS TypWynajmu, uwagi AS Uwagi from rezerwacje";
                    break;
                
                // Formularze klienta
                case "Rezerwacja nieruchomości":
                    DynamicDataForm.Content = new EstateBooking();
                    query = "SELECT \n    n.id_nieruchomosci AS 'ID Nieruchomości',\n    n.typ AS 'Typ',\n    n.metraz AS 'Metraż',\n    n.liczba_pokoi AS 'Liczba Pokoi',\n    n.adres AS 'Adres',\n    GROUP_CONCAT(DISTINCT ud.nazwa SEPARATOR ', ') AS 'Udogodnienia',\n    CONCAT(w.imie, ' ', w.nazwisko) AS 'Właściciel',\n    GROUP_CONCAT(DISTINCT d.nazwa SEPARATOR ', ') AS 'Dokumenty',\n    CASE WHEN u.id_nieruchomosci IS NULL THEN 'Dostępna' ELSE 'Niedostępna' END AS 'Dostępność'\nFROM nieruchomosc n\nLEFT JOIN umowa u ON n.id_nieruchomosci = u.id_nieruchomosci\nLEFT JOIN wlasciciel w ON n.id_wlasciciela = w.id_wlasciciela\n-- Tabela pośrednia łącząca nieruchomości z udogodnieniami\nLEFT JOIN nieruchomosc_udogodnienie nud ON n.id_nieruchomosci = nud.id_nieruchomosci\nLEFT JOIN udogodnienie ud ON nud.id_udogodnienia = ud.id_udogodnienia\n-- Załóżmy, że dokumenty mają bezpośrednio klucz obcy do nieruchomości. Jeśli nie, również potrzebna będzie tabela pośrednia.\nLEFT JOIN dokument d ON n.id_nieruchomosci = d.id_nieruchomosci\nGROUP BY n.id_nieruchomosci;\n"; // Dodatkowe zapytanie, jeśli potrzebne
                    break;

                case "Kontakt z administratorem budynku":
                   // DynamicDataForm.Content = new ContactAdminForm();
                    query = ""; // Dodatkowe zapytanie, jeśli potrzebne
                    break;
            }

            if (!string.IsNullOrEmpty(query))
            {
                DataTable data = Database.ExecuteQuery(query);
                DataGridTables.ItemsSource = data.DefaultView;
            }
        }

        private void LoadData(string tableName)
        {
            string query = $"SELECT * FROM {tableName}";
            DataTable data = Database.ExecuteQuery(query);
            DataGridTables.ItemsSource = data.DefaultView;
        }

        private void OnRefreshData(object sender, RoutedEventArgs e)
        {
            if (ComboBoxMenu.SelectedItem is ComboBoxItem selectedItem)
            {
                string selectedTable = selectedItem.Content.ToString();
                LoadData(selectedTable.ToLower());
            }
        }
        private void ShowReports_Click(object sender, RoutedEventArgs e)
        {
            // Tworzymy i wyświetlamy okno z raportami
            ReportsWindow reportsWindow = new ReportsWindow();
            reportsWindow.Show();
        }

}
}
