   M S B A M L   `   `   `  ����=  9LM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:LMLM  5          LM.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      "localclr-namespace:LM     5        � Title$  
MainWindow��5      $	��450��6   $	��800��6#    � 	Resources 5	       �� 
TargetType �� Property �� Value��%  '��       '�� H     '�� �     �� 5
      " ��6
   .�5      ��  � ��$ 14(�6    5      �� 5      " ��6
   .�5      ��  � ��$ 14(�6    5      �� 5      " ��6
   .�5      ��  � ��$ 40��6   �� 5       � ��$ 5q�6   5      5      .��5      �  � RowDefinitions 5   
   �� 5      �� 5      $��50 �6    � ColumnDefinitions 5   
   �� 5      $	��100 �6   �� 5      .+�5    
   �� $��0��6   $��0��6   ��6/   �� *Chats+ �� SelectedItem 5!      �� *SelectedChat+ �� UpdateSourceTrigger$ PropertyChanged=���5"      �� 5#      .R�5$      � $��3q�6    5%      �� 5&      $��10 �6/   �� 5'      .+�5)      �� $��1��6$   ��64   �� 	 �� Path$	 ChatName�5*      5+      5-      �� 50   
   $��0��6   $��1��6   ��6/   �� *
Messages+��51      �� 52      .R�53      �� $��5q�6!   .+�54      �� $��10(�6$   ��55   
   �� $	 Username��� 56      ��6$   �� $	 Message�58      59      5;      � 5>   
   $��1��6   $��1��6    5?      �� 5@      �� 5A      $��60 �6   .+�5C      �� $��0��6   ��5D      �� *
NewMessage+$ PropertyChanged=��� 5E      $��1��6   $
��Send��6   ��5F      �� *SendCommand+5G      5I      5J      