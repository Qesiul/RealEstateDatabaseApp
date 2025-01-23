using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Reporting.NETCore; // przestrzeń z LocalReport, ReportDataSource

namespace RealEstateDatabaseApp
{
    public partial class ReportsWindow : Window
    {
        public ReportsWindow()
        {
            InitializeComponent();
            Loaded += ReportsWindow_Loaded;
        }

        private void ReportsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Na razie nic nie inicjalizujemy
        }

        private void ComboBoxReports_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedReport = (ComboBoxReports.SelectedItem as ComboBoxItem)?.Content.ToString();

            // Ukryj wszystkie pola
            DateFrom.Visibility = Visibility.Collapsed;
            TxtEstateId.Visibility = Visibility.Collapsed;

            // Logika dla pozostałych raportów zostaje nietknięta

            if (selectedReport == "Nieruchomości wg typu (grupowanie)")
            {
                DateFrom.Visibility = Visibility.Visible;
            }
            else if (selectedReport == "Liczba nieruchomości w czasie (wykres)")
            {
                DateFrom.Visibility = Visibility.Visible;
            }
            else if (selectedReport == "Formularz rezerwacji wybranej nieruchomości")
            {
                // Tylko dwa pola: DateFrom (data_spotkania) i TxtEstateId (adres_nieruchomosci)
                TxtEstateId.Visibility = Visibility.Visible;
                DateFrom.Visibility = Visibility.Visible; 
            }
            else if (selectedReport == "Lista najemców")
            {
            }
        }

        private void GenerateReport_Click(object sender, RoutedEventArgs e)
        {
            string selectedReport = (ComboBoxReports.SelectedItem as ComboBoxItem)?.Content.ToString();
            if (selectedReport == null)
            {
                MessageBox.Show("Proszę wybrać raport.");
                return;
            }

            // Pobierz kryteria
            DateTime? dateFrom = DateFrom.SelectedDate; // wykorzystywane jako data_spotkania w formularzu rezerwacji
            string estateId = TxtEstateId.Text; // wykorzystamy jako adres_nieruchomosci

            DataTable dt = null;
            string reportPath = "";

            if (selectedReport == "Nieruchomości wg typu (grupowanie)")
            {
                reportPath = "Reports/NieruchomosciWedlugTypu.rdlc";
                string query = "SELECT typ, COUNT(*) AS Liczba FROM nieruchomosc WHERE 1=1";
                query += " GROUP BY typ";

                dt = Database.ExecuteQuery(query);
            }
            else if (selectedReport == "Liczba udognien w nieruchomosciach (wykres)")
            {
                reportPath = "Reports/LiczbaNieruchomosciWykres.rdlc";
                string query = @"
                    SELECT 
                        ud.nazwa AS Udogodnienie,
                        COUNT(nud.id_nieruchomosci) AS Liczba
                    FROM 
                        udogodnienie ud
                    LEFT JOIN 
                        nieruchomosc_udogodnienie nud ON ud.id_udogodnienia = nud.id_udogodnienia
                    GROUP BY 
                        ud.nazwa";

                dt = Database.ExecuteQuery(query);
            }
            else if (selectedReport == "Formularz rezerwacji wybranej nieruchomości")
            {
                if (string.IsNullOrEmpty(estateId))
                {
                    MessageBox.Show("Proszę wprowadzić ID nieruchomości.");
                    return;
                }
                if (!dateFrom.HasValue)
                {
                    MessageBox.Show("Proszę wybrać datę rezerwacji.");
                    return;
                }

                reportPath = "Reports/FormularzRezerwacji.rdlc";

                // Wykorzystujemy estateId jako adres_nieruchomosci i dateFrom jako data_spotkania
                string query = "SELECT id_rezerwacji AS IdRezerwacji, " +
                               "CONCAT(imie, ' ', nazwisko) AS Rezerwujacy, " +
                               "data_spotkania AS DataSpotkania, " +
                               "typ_wynajmu AS TypWynajmu, " +
                               "uwagi AS Uwagi " +
                               "FROM rezerwacje WHERE 1=1";
                if (!string.IsNullOrEmpty(estateId))
                    query += $" AND adres_nieruchomosci = '{estateId}'";
                if (dateFrom.HasValue)
                    query += $" AND data_spotkania = '{dateFrom:yyyy-MM-dd}'";


                dt = Database.ExecuteQuery(query);
            }
            else if (selectedReport == "Lista najemców")
            {
                reportPath = "Reports/ListaNajemcow.rdlc";
                string query = "SELECT imie, nazwisko, kontakt FROM najemca WHERE 1=1"; 

                dt = Database.ExecuteQuery(query);
            }

            var localReport = new LocalReport();
            using (var rdlcStream = File.OpenRead(reportPath))
            {
                localReport.LoadReportDefinition(rdlcStream);
            }

            localReport.DataSources.Clear();
            localReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

            // Renderuj do PDF
            byte[] pdfBytes = localReport.Render("PDF");

            // Zapisz PDF do pliku tymczasowego
            string tempFile = Path.Combine(Path.GetTempPath(), "report.pdf");
            File.WriteAllBytes(tempFile, pdfBytes);

            // Załaduj PDF w WebBrowser
            Browser.Navigate(tempFile);
        }
    }
}
