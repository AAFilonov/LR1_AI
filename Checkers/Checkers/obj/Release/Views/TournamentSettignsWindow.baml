   M S B A M L   `   `   `  ���� C  ?Checkers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:Checkers.ViewModelsCheckers.ViewModels  -    'Checkers.Views.TournamentSettignsWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     8
viewModels!clr-namespace:Checkers.ViewModels       � Title$'  !Настройки турнира�� � SizeToContent$ WidthAndHeight=�.���  � RowDefinitions �� �� �� �� .+��  � ColumnDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto �.+�&   System.Windows.Controls.DataGrid  -     Name$ dgSourceBots��   AutoGenerateColumns .  ����  �� Path$
 Bots�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Имя��	   
IsReadOnly	 . 1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 �� $
 Name�  $ Студент��	 . 
 �� $ 
StudentFIO�  $ Группа��	 . 
 �� $ StudentGroup�  $ Победы��	 . 
 �� $
 Wins�  $ Поражения��	 . 
 �� $ Loses�  $ 
Ничьи��	 . 
 �� $ Draws�  $ Процент побед��	 . 
 �� $ WinRate��� $��1�� �� Orientation$ Vertical=�$��Center=�.+��� -   $��->��$��40��$��5q��� -   $��<-��$��40��$��5q�  -   $ dgSelectedBots��$��2�� .  ���� $ SelectedBots�   $ Имя��	 . 
 �� $
 Name�  $ Студент��	 . 
 �� $ 
StudentFIO�  $ Группа��	 . 
 �� $ StudentGroup��� $��1��$7��1Количество партий в раунде��.���� ���� $ NumberOfGamesPerFight��� $��2��$>��8Максимум параллельных потоков��.���� ���� $ 
MaxThreads��� $��3��$ 
Horizontal=�$��Center=�.+��� $��Начать турнир��$��5q����� $ StartTourmanentCommand��� $��Отмена��$��5q����� $ CancelCommand�