   M S B A M L   `   `   `  ����P  LRealEstateDatabaseApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:RealEstateDatabaseAppRealEstateDatabaseApp  5   	   =#clr-namespace:RealEstateDatabaseAppRealEstateDatabaseApp  5   	   &     RealEstateDatabaseApp.MainWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:RealEstateDatabaseApp     5   	   8behavior#clr-namespace:RealEstateDatabaseApp     5   	     � Title$(  "Baza danych wynajmu nieruchomości��5	   	   $	��800��63   $
��1000��6@   .��5
      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��10q�6.   $��0��6:   .+�5      �� -   5         ��Name$ ComboBoxMenu��5      $	��200��6-   .@�5      �� ${�False��6   $��Zarządzanie danymi��61   $
��Bold"�6N   �� 5      $��Najemcy��6   �� 5      $��Nieruchomości��6   �� 5      $��	Dokumenty��6   �� 5      $��Administratorzy budynku��6   �� 5      $��Płatności��6   �� 5      $��
Reklamacje��6   �� 5      $��
Serwisanci��6   �� 5      $��Udogodnienia��6   �� 5      $��Umowy��6   �� 5      $��Właściciele��6   �� 5       $��Zlecenia serwisowe��6   �� 5!      $��
Rezerwacje��6   �� 5"      ${�False��6   $��Formularze klienta��61   $
��Bold"�6N   �� 5#      $��Rezerwacja nieruchomości��6   5$      m� 5%      -   5        $��Raporty��5%      6(   5&      &   System.Windows.Controls.DataGrid  5)   
   -   5          Name$ DataGridTables��5)         AutoGenerateColumns$
 True��6+   $��10q�6F   $��1��6R   �� 5,   
   -   5         ��Name$ DynamicDataForm��5,      $��10q�64   $��2��6@   5-      5.      