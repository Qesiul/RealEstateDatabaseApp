   M S B A M L   `   `   `  ����  RealEstateDatabaseApp)    #RealEstateDatabaseApp.ReportsWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Raporty��5   	   $	��600��6   $
��1000��6&   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5	      $
��Auto �6   �� 5
      $��* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��0��6.   .+�5      �� $��Wybierz raport:��6   $��Center=�6/   $��0,0,10,0q�6J   �� 5      -   5         ��Name$ ComboBoxReports��5      $	��200��60   .@�5      �� $)��#Nieruchomości wg typu (grupowanie)��6   �� 5      $1��+Liczba udognien w nieruchomosciach (wykres)��6   �� 5      $2��,Formularz rezerwacji wybranej nieruchomości��6   �� 5      $��Lista najemców��6   5      5      �� 5   
   -   5         ��Name$ CriteriaPanel��5      $ 
Horizontal=�6,   $��1��6E   $��	0,10,0,10q�6R   .+�5      �� .+�5      �� $��Data od:��6   (  "System.Windows.Controls.DatePicker  5      -   5          Name$ DateFrom��5      $	��120��6/   5      �� 5!      $��20,0,0,0q�6   .+�5"      �� $��Adres Nieruchomości:��6   �� 5#      -   5         ��Name$ TxtEstateId��5#      $	��100��6/   5$      5<      �� 5>   
   -   5        $��Generuj raport��5>      $��2��6*   $	��120��67   $��30��6C   $
��Left=�6O   6j   (  "System.Windows.Controls.WebBrowser  5@   
   -   5          Name$ Browser��5@      $��3��6&   $��0,10,0,0q�63   5A      5B      