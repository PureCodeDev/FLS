   M S B A M L   `   `   `  ����>  :WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:WPFWPF  5   	       WPF.ComparisonWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   #localclr-namespace:WPF     5   	     � Title$  ComparisonWindow��5   	   $	��450��6"   $	��800��60   .��5	      � .+�5
   
   &   System.Windows.Controls.DataGrid  -   5          Name$ dataGridNew��5
      $
��Left=�6(   $	��Top=�5      $	��300��6+   $��
300,50,0,0q�67      ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$	 125 6K      MinColumnWidth$ 30Q�6]   ��5
   C   ��  �� RelativeSource ��  �� AncestorType   WPF.ComparisonWindow"   �� Path$ 	Height-50�5   ]     5   
   -   5        $ dataGridOld��5      $
��Left=�6(   $	��Top=�5      $	��300��6+   $��0,50,0,0q�67   $	 125 6I   $ 30Q�6[   ��5   C   ��  �� "  $ 	Height-50�5   [   �� 5   
   -   5         ��Name$ button��5      $��Accept changes��6!   $��16(�6:   $
��Left=�6H   $��
646,39,0,0q�6c   $	��Top=�6w   $	��117��6�   $��66��6�   6�   �� 5   
   -   5        $ button1��5      $��Cancel��6"   $��16(�63   $
��Left=�6A   $��646,144,0,0q�6\   $	��Top=�6q   $	��117��6�   $��66��6�   �� 5   
   -   5         ��Name$ labelOld��5      $��	OLD TABLE��6"   $��	UltraBold"�66   $��32(�6N   
����p��
���   �$
��Left=�6�   $	��Top=�6�   $��45��6�   $	��300��6�   �� 5   
   -   5        $ labelNew��5      $��	NEW TABLE��6"   $��	UltraBold"�66   $��32(�6N   
������$
��Left=�6u   $��	305,0,0,0q�6�   $	��Top=�6�   $��45��6�   $	��295��6�   �� 5   
   -   5        	 ��Name$	 textBoxChangedId��5      $
��Left=�6,   $��Changed Id:��6G   $��	UltraBold"�6Z   $��18(�6r   
�������$	��161��6�   $��646,232,0,0q�6�   
 �� TextWrapping$

 Wrap=�6�   $	��Top=�6�   $	��117��6�   5      5      