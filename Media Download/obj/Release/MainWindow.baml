   M S B A M L   `   `   `  ���� H  DMahApps.Metro, Version=1.6.0.0, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  I EMedia Download, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Media_Download.PropertiesMedia_Download.Properties /clr-namespace:Media_DownloadMedia_Download    Media_Download.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     \ XMahApps.Metro.IconPacks.Material, Version=2.2.0.12, Culture=neutral, PublicKeyToken=null=	iconPacks-http://metro.mahapps.com/winfx/xaml/iconpacks  8
properties'clr-namespace:Media_Download.Properties  (localclr-namespace:Media_Download  -     � Title$  Media Download��$	��210��$	��512�� � 
ResizeMode$ CanMinimize=�(   "MahApps.Metro.Controls.MetroWindow   ShowMaxRestoreButton .     ShowIconOnTitleBar .   � 	Resources  �� MergedDictionaries �� Source   SettingsButtonStyle �� 
TargetType �� Property	 �� Value
 �� 
TargetType �Name � ColumnDefinitions ��Name ��Name ��Name �� Triggers P� Property P� Value �� 
TargetName  AccentColorBrush3  AccentColorBrush2��  �� $ /Resources/Icons.xaml?��� $O Ipack://application:,,,/MahApps.Metro;component/Styles/Accents/Cobalt.xaml?��� $R Lpack://application:,,,/MahApps.Metro;component/Styles/Accents/BaseLight.xaml?�%\  &	        7  7  �� " d�.���  �@J�SnapsToDevicePixels	 . ��  �@�OverridesDefaultStyle	 . ��  � ��$
	 29px����  � ��$	 103px����  � ��	 �	#FFFFFFFF��  � ��	 �� "
 d�.R�� $
 Grid��
������  �� $ Column��$��0 ��� $��* �.+��� $ 	Rectangle��
������ $��0��$��0,8,0,8q�$��Stretch=�$��Stretch=��� $ Border��$��1��
���   �
������ ._��� $��	12,2,12,2q�$
��Left=�$��Center=���.  P�  �@J�IsMouseOver . . ��� $
 Grid�� � ��	 �	#22808080�� $ Column�� � ��$	 2 ��� $ 	Rectangle�� � ��$	 0,8,0,8q��� $ 	Rectangle��8	    � ��P�  �@d�	IsChecked$
 trueY�. ��� $
 Grid�� � ��	 �	#44808080�� $ Column�� � ��$	 2 ��� $ 	Rectangle�� � ��$	 0,6,0,6q��� $ 	Rectangle��8	   � �� J� CommandBindings )  #System.Windows.Input.CommandBinding  -      Command$ ApplicationCommands.Paste��   RightWindowCommands +   %MahApps.Metro.Controls.WindowCommands  .@��� -    ��Name$ btnSettings��.����  �� Orientation$ 
Horizontal=�.+��� $��16��$��16��$��2,2,2,2q�����  �� RelativeSource ��  �� AncestorType$ ButtonL� �� Path$ 
Foreground� J� OpacityMask !�  j� Stretch$
 Fill=� !� Visual /  )MahApps.Metro.IconPacks.MaterialExtension  �  �MahApps.Metro.IconPacks.PackIconExtension`2[[MahApps.Metro.IconPacks.PackIconMaterial, MahApps.Metro.IconPacks.Material, Version=2.2.0.12, Culture=neutral, PublicKeyToken=null],[MahApps.Metro.IconPacks.PackIconMaterialKind, MahApps.Metro.IconPacks.Material, Version=2.2.0.12, Culture=neutral, PublicKeyToken=null]]   Kind$ Settings=�    Flyouts  +   %MahApps.Metro.Controls.FlyoutsControl  .@�#   MahApps.Metro.Controls.Flyout  -   !  Name$! flyoutSettings��$��Settings��"   Position$" Right=�$	��512��#   Theme$# Adapt=�$��0,0,0,0q�
������ .��� $��1,0,0,0q� �� $	��144��$	��144 �$	��160���� $��1��$��1 �$��1���� $��* �.+�� $��LeftToRight=�$��0��.+�d� -   $ d�Name$$ btnSettingsDefault��$��0,8,0,0q�$��40��$	��Top=�$��Stretch=�#��[  .��� $��0,0,0,0q�.+��� $��Center=�
������   	TextBrush#���  $
��Left=�$��24,0,0,2q�
Defaults�� $��16��$��16��#���  $��Center=�$
��Left=�$��0,0,0,0q� !� $
 Fill=�   $ FormatListBulleted=�d� -   $$ btnSettingsTheme��$��0,48,0,0q�$��40��$	��Top=�$��Stretch=�#��[  .��� $��0,0,0,0q�.+��� $��Center=�
������ #���  $
��Left=�$��24,0,0,2q�Style�� $��16��$��16��#���  $��Center=�$
��Left=�$��0,0,0,0q� !� $
 Fill=�   $ Palette=�d� -   $$ btnSettingsAbout��$��0,88,0,0q�$��40��$	��Top=�$��Stretch=�#��[  .��� $��0,0,0,0q�.+��� $��Center=�
������ #���  $
��Left=�$��24,0,0,2q�About�� $��16��$��16��#���  $��Center=�$
��Left=�$��0,0,0,0q� !� $
 Fill=�   $ InformationOutline=��� #��["�$��1��$��0,8,0,8q�$��Stretch=�$��Stretch=�� $��2��.+�� -   $ SettingsPageDefault��.+��� $	��Top=�$��Stretch=�$��0,4,0,0q�% �� TextAlignment$% Center=�
Defaults�� $	��Top=�$��Stretch=�$��
16,23,16,0q�$��1��#��� �� $��0,22,0,0q�.+��� $��Save Location:��$
��Left=�$��12,8,0,0q�$	��Top=��� -	   & ��Name$& lblFolderDefault��$��Medium"�$��Stretch=�$��
12,28,48,0q�$	��Top=����� ( "Media_Download.Properties.Settings'   Default( �� Source#( Z' $ DefaultSaveLocation�) �� Mode$) TwoWay=�* �� UpdateSourceTrigger$* PropertyChanged=��� -
   $ btnFolderDefault��  MetroCircleButtonStyle#���    AccentColorBrush#���  $��0q�$��Right=�$��40��$��40��$��0,16,8,0q�$	��Top=�.���� $��16��$��16������ $ 
Foreground� �� *FindAncestor+" �� !� $
 Fill=�   $ Folder=�� $��0,48,0,0q�$	��Top=�$��Stretch=�.+�	� -   + 	�Name$+ rdbtnAudioDefault��$��Audio (MP3)��$��8,0,0,0q�$
��Left=�$��Center=�, d� 	IsChecked, �� #( Z' $ 	TypeAudio�$) TwoWay=�	� -   $+ rdbtnVideoDefault��$��Video (MP4)��$��	128,0,0,0q�$
��Left=�$��Center=�, �� #( Z' $ 	TypeVideo�$) TwoWay=�	� -   $+ rdbtnCustomDefault��$��Custom: ��$��	248,0,0,0q�$
��Left=�$��Center=�, �� #( Z' $ 
TypeCustom�$) TwoWay=��� -   - ��Name$- txtFormatDefault��$��	8,50,48,0q�$��Stretch=�$��Center=����� #( Z' $ TypeCustomValue�$) TwoWay=�.����O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem �� 0sys&clr-namespace:System;assembly=mscorlib  " ��. �� 	Resources.   CueBannerBrush/ j� 
AlignmentX0 j� 
AlignmentY��%[   &	       !� $
/ Left=�$0 Center=�$
 None=� �� $�� Custom Format��
�������1 �� Triggers1 P�  � ��2 �� Empty# Z2 . ���  � ��#	 [ P�  � �� Z� . ���  � ��#	 [ P�  �@J�IsKeyboardFocused . . ���  � ��
	 ���� .���� -   $ btnHelpDefault��#���  #���  $��0q�$��Right=�$��40��$��40��$��0,42,8,0q�$	��Top=�.���� $��16��$��20������ $ 
Foreground� �� *FindAncestor+" �� !� $
 Fill=�   $
 Help=�� -   $ SettingsPageTheme��.+��� $	��Top=�$��Stretch=�$��0,4,0,0q�$% Center=�Themes�� $	��Top=�$��Stretch=�$��
16,23,16,0q�$��1��#��["�� $��0,24,0,0q�$	��Top=�$��Stretch=� �� $	��144��$	��144 �$	��144���� $��* �.+�� $��0��$��Stretch=�$��Stretch=�.+�	� -   $+ rdbtnThemeLight��$��Light Theme��$��	12,20,0,0q�$
��Left=�$	��Top=�, �� #( Z' $ 
ThemeLight�$) OneTime=�	� -   $+ rdbtnThemeDark��$��
Dark Theme��$��	12,44,0,0q�$
��Left=�$	��Top=�, �� #( Z' $ 	ThemeDark�$) OneTime=��� -   3 ��Name$3 
chkConsole��$��Show Console��$��	12,0,0,16q�$
��Left=�$��Bottom=�, �� #( Z' $ ShowConsole�$) TwoWay=�� $��1��$	��Top=�$��Stretch=�.+��� -   4 ��Name$4 SettingsThemeAccentList��$��0q�$��Stretch=�$��Stretch=�$��0,8,8,12q����� .R��� $ 
Horizontal=�.+��� ���� � -   $ SettingsPageAbout��.+��� $	��Top=�$��Stretch=�$��0,4,0,0q�$% Center=�About�� $	��Top=�$��Stretch=�$��
16,23,16,0q�$��1��#��� �� $��0,24,0,0q�.+��� $	��Top=�$
��Left=�$��12,4,0,0q�$��Medium"�Media Download�� $	��Top=�$
��Left=�$��	12,22,0,0q�Media downloading utility�� $	��Top=�$
��Left=�$��	12,40,0,0q�$"Mark Diedericks, Copyright © 2020�� -   $ 	btnUpdate��$	��160��$��	12,66,0,0q�$	��Top=�$
��Left=�.��Check For Updates� $��
184,52,0,0q�.+��� -   $ SettingsIconTick��5 � ToolTip$5 Update Completed��$y�Hidden=�$��16��$��16�����#101010$��Center=�$
��Left=�$��0,0,0,0q� !� $
 Fill=�   $ Check=��� -   $ SettingsIconCross��$5 Update Failed��$y�Hidden=�$��16��$��16�����#101010$��Center=�$
��Left=�$��0,0,0,0q� !� $
 Fill=�   $ AlertCircle=�)	   #MahApps.Metro.Controls.ProgressRing	  -   6 	 Name$6 SettingsIconLoad��$5 Updating��$y�Hidden=�$��16��$��16�����#101010$��Center=�$
��Left=�$��0,0,0,0q�.��� .+��� $��Save Location:��$
��Left=�$��	12,12,0,0q�$	��Top=��� -   $& 	lblFolder��$��Medium"�$��Stretch=�$��
96,12,48,0q�$	��Top=����� #( Z' $ DefaultSaveLocation�$) OneTime=��� -   $ 	btnFolder��#���  #���  $��0q�$��Right=�$��40��$��40��$��0,-2,8,0q�$	��Top=�.���� $��16��$��16������ $ 
Foreground� �� *FindAncestor+" �� !� $
 Fill=�   $ Folder=��� #��� �$��Stretch=�$	��Top=�$��1��$��0,35,0,0q�� $��0,42,0,0q�$��Stretch=�$��Stretch=�.+��� -   $- txtLink��$��Stretch=�$��
12,14,48,0q�$	��Top=�.����1&clr-namespace:System;assembly=mscorlibSystem �� 0sys&clr-namespace:System;assembly=mscorlib  " ��. ��%Q   &	       !� $
/ Left=�$0 Center=�$
 None=� �� $
�� URL��
�������1 P�  � ��# Z2 . ���  � ��#	 [ P�  � �� Z� . ���  � ��#	 [ P�  �@J�IsKeyboardFocused . . ���  � ��
	 ���� .���� -   $ btnLinkPaste��#���  #���  $��0q�$��Right=�$��40��$��40��$��0,2,8,0q�$	��Top=�.���� $��16��$��20������ $ 
Foreground� �� *FindAncestor+" �� !� $
 Fill=�   $ ContentPaste=�� $��0,48,0,0q�$	��Top=�$��Stretch=�.+�	� -   $+ 
rdbtnAudio��$��Audio (MP3)��$��8,0,0,0q�$
��Left=�$��Center=�, �� #( Z' $ 	TypeAudio�$) OneTime=�	� -   $+ 
rdbtnVideo��$��Video (MP4)��$��	112,0,0,0q�$
��Left=�$��Center=�, �� #( Z' $ 	TypeVideo�$) OneTime=�	� -    $+ rdbtnCustom��$��Custom: ��$��	216,0,0,0q�$
��Left=�$��Center=�, �� #( Z' $ 
TypeCustom�$) OneTime=��� -!   $- 	txtFormat��$��
296,0,48,0q�$��Stretch=�$��Center=����� #( Z' $ TypeCustomValue�$) OneTime=�.����1&clr-namespace:System;assembly=mscorlibSystem �� 0sys&clr-namespace:System;assembly=mscorlib  " ��. ��%[   &	       !� $
/ Left=�$0 Center=�$
 None=� �� $�� Custom Format��
�������1 P�  � ��# Z2 . ���  � ��#	 [ P�  � �� Z� . ���  � ��#	 [ P�  �@J�IsKeyboardFocused . . ���  � ��
	 ���� .���� -"   $ btnHelp��#���  #���  $��0q�$��Right=�$��40��$��40��$��0,-6,8,0q�$	��Top=�.���� $��16��$��20������ $ 
Foreground� �� *FindAncestor+" �� !� $
 Fill=�   $
 Help=��� -#   $ btnOK��$��Queue��$��96��$��Bottom=�$��Right=�$��0,0,8,8q��� -$   $& 	txtRemain��$��0 of 0��$% Right=�$y�Hidden=�$��Bottom=�$��Stretch=�$��14(�$��128,0,116,12q����� " ��1 P�  �@J�IsMouseOver . . ���  � ��
	 ������� -%   $& 
txtUpdates��$��Updates Available��$5 Update Youtube-dl��$% Right=�$y�Hidden=�$��Bottom=�$
��Left=�$��14(�$��	12,0,0,12q����� " ��1 P�  �@J�IsMouseOver . . ���  � ��
	 �����