   M S B A M L   `   `   `  ���� C  ?Checkers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:Checkers.ViewModelsCheckers.ViewModels  '    !Checkers.Views.PlayerSelectWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     8
viewModels!clr-namespace:Checkers.ViewModels     k gGalaSoft.MvvmLight.Extras.WPF45, Version=4.3.31.28649, Culture=neutral, PublicKeyToken=0e27b6f8be9b2ef7.command http://www.galasoft.ch/mvvmlight  c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity    � Title$  Выбор игроков�� � SizeToContent$ WidthAndHeight=� J� 	AllowDrop . .  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger     	EventName$
 Drop��.  (System.Windows.Interactivity.TriggerBase   Actions. /  )GalaSoft.MvvmLight.Command.EventToCommand     PassEventArgsToCommand .    Command ��  �� Path$ FileDropCommand�.��� $��20q� . 	 � RowDefinitions	 �� $
��Auto ��� �� �� �� $
��Auto �
 � ColumnDefinitions
 �� �� $��10 ��� .+��� $��
Белый��$��25(�$��Center=�$��Center=��� $��Черный��$��25(�$��Center=�$��Center=�$��2���� $��1�� �� Orientation$ 
Horizontal=�.+��� $��Выбран игрок:���� $
��Bold"����� $ SelectedWhite.Name��� $��1��$��2��$ 
Horizontal=�.+��� Выбран игрок:�� $
��Bold"����� $ SelectedBlack.Name�&   System.Windows.Controls.DataGrid  -     Name$ 	dgWhiteAI��$��3��   CanUserAddRows .     AutoGenerateColumns .  ���� $
 Bots� J� InputBindings d�  d� Gesture$ LeftDoubleClicka� �� Command �� $  SelectWhiteAIPlayerCommand� �� CommandParameter ��  �� ElementName$ 	dgWhiteAI��$ SelectedItem�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Имя��   
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� $
 Name�  $ Студент�� .  �� $ 
StudentFIO�  $ Группа�� .  �� $ StudentGroup�  $ Победы�� .  �� $
 Wins�  $ Поражения�� .  �� $ Loses�  $ 
Ничьи�� .  �� $ Draws�  $ Процент побед�� .  �� $ WinRate�  -   $ 	dgBlackAI��$��3��$��2�� .   .  ���� $
 Bots� d� $ LeftDoubleClicka� �� $  SelectBlackAIPlayerCommand� �� $ 	dgBlackAI��$ SelectedItem�   $ Имя�� .  �� $
 Name�  $ Студент�� .  �� $ 
StudentFIO�  $ Группа�� .  �� $ StudentGroup�  $ Победы�� .  �� $
 Wins�  $ Поражения�� .  �� $ Loses�  $ 
Ничьи�� .  �� $ Draws�  $ Процент побед�� .  �� $ WinRate��� $��2������ $ SelectWhitePlayerHuman�.��Человек�� $��2��$��2������ $ SelectBlackPlayerHuman�.��Человек�� $ 
Horizontal=�$��4��$��3��$��Center=�.+��� $��OK��$��20q�$��40��$��30�� �� 	IsDefault . ���� $ 	OKCommand��� $��Cancel��$��20q�$��60��$��30�� �� IsCancel . ���� $ CancelCommand�