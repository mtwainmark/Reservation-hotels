   M S B A M L   `   `   `  ����C  ?WpfApp12, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:WpfApp12.ViewsWpfApp12.Views  5          WpfApp12.Views.SearchView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      U QMaterialDesignThemes.Wpf, Version=3.1.0.981, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      (localclr-namespace:WpfApp12.Views  5        � 	Resources  5         listTemplate �� Orientation �� StringFormat �� Path��%�  &	        �� 5   
   .R�5      �� $��5q�6   $ 
Horizontal=�6$   .+�5      �� $��Center=�6   ��5      `� 5      $  Город: {0} ��6'   .1�5      �� $
 City�6&   64   5      5      �� 5      $��Center=�6   ��5      `� 5      $  Отель: {0} ��6'   .1�5      �� $ Title�6&   65   5      5      �� 5      $��Center=�6   ��5       `� 5!      $, & Количество звезд: {0} ��6'   .1�5"      �� $ Stars�6&   65   5#      5%      �� 5'      $��Center=�6   ��5(      `� 5)      $   Тип номера: {0} ��6'   .1�5*      �� $ TypeRoom�6&   68   5+      5-      �� 5.      $��Center=�6   ��5/      `� 50      $> 8 Количество гостей в номере: {0} ��6'   .1�51      �� $ CountGuests�6&   6;   52      54      55      56      57      .��59      �  � RowDefinitions 5:   
   �� 5;      $
��Auto �6   �� 5<      .+�5?   
   �� $��0��6   $ 
Horizontal=�6"   $��10q�6;   $��Stretch=�6G   $	��Top=�6e   .+�5@      ��  �� 
IsEditable . )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ 
Город��6�   $��10q�6�   $	��150��6�   ��6   �� *
CityList+ �� SelectedItem 68   �� *City+ �� ValidatesOnDataErrors . 	 �� UpdateSourceTrigger$	 PropertyChanged=�5B      (  "System.Windows.Controls.DatePicker  5D      $ 
Заезд��6X   $��10q�6�   $	��100��6�   
   SelectedDate
 6   �� *
FromDate+ .    BlackoutDates 5E      /  )System.Windows.Controls.CalendarDateRange  5F      
   End$ 
03/14/2020��6(      Start$ 
03/01/2000��69   5H        5J      $ Отъезд��6U   $��10q�6~   $	��100��6�   
 6   �� *ToDate+ .  5K        5L      $ 
03/15/2020��6(   $ 
03/01/2000��69   5N      (  "MaterialDesignThemes.Wpf.RatingBar  5P      $��Center=�6L   $��10q�6h      Value 6'   �� *Stars+ �� Mode$ TwoWay=�5R      9� 5V      $��10q�6   $	��250��6#   $'��!Количество гостей��6/   .��5W      �  � ColumnDefinitions 5X      �� 5Y      $��2* �6+   �� 5Z      �� 5[      �� 5\       5_      �� 5`      �� 5a      �� 5b      .+�5f      �� $��0��6    $��0��60   $��Center=�6=   $��Center=�6Z   $��Гостей��6u   $��5q�6�   �� 5g      $��1��6   $��0��6-   $��-��6:   $��5q�6G   ��6R   �� *RemoveGuests+�� 5h      $��2��6    $��0��60   $��Center=�6=   $��Center=�6Z   $
��Bold"�6�   $��5q�6�   ��6u   �� *CountGuests+6�   �� 5i      $��3��6   $��0��6-   $��+��6:   $��5q�6G   ��6R   �� *	AddGuests+�� 5k      $��0��6    $��1��60   $��Center=�6=   $��Center=�6Z   $��Тип номера��6u   $��5q�6�   	� 5l      $��1��6"   $��1��62   $��2��6?   $��Center=�6S   $��Center=�6p   $��Стандарт��6�    d� 	IsChecked 6�   �� *StandartRoom+	� 5m      $��3��6"   $��1��62   $��Center=�6?   $��Center=�6\   $��Люкс��6w    6�   �� *	LuxRoom+5n      5o      �� 5q        MaterialDesignFlatButton#��[ 67   $��
Поиск��6i   ��6   �� *SearchCommand+6i   5r      � 5t   
   $��1��6   .+�5u      �� -   5         ��Name$ 
phonesList��5u      #��[  6*   $��Disabled=�5w      ��5v      �� *	RoomsList+ 5w   H   �� *SelectedRoom+��5x      �� 5y      .R�5z      �� 5{      5}      �� 5~      $��10q�6   $��Bottom=�6!   $ ��Забронировать��6<   ��6T   �� *CommandSelect+5      5�      5�      