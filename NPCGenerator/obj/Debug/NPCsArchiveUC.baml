   M S B A M L   `   `   `  ����  NPCGenerator     NPCGenerator.NPCsArchiveUC   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5	      � $��0��6     � RowDefinitions  5
   
   �� 5      $��50 �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��0��6"   $��2��62    �� Orientation$ 
Horizontal=�6F   
�����F�.+�5      �� -   5         ��Name$ 	addNPCBtn��5      $��Add NPC��6(   $��5q�6:   $��30��6F   $��65��6R   $��Center=�6]   $��15,0,0,0q�6�   6�   �� 5      -   5         ��Name$ filterTB��5       �� TextWrapping$ NoWrap=�6(   $��30��6>   $ �� HorizontalScrollBarVisibility$ Hidden=�6J   $��Center=�6q   $	��250��6�   $��15,0,0,0q�6�   " �� VerticalScrollBarVisibility$ Disabled=�6�    �� AcceptsReturn .  	 �� 
AcceptsTab	 .  {�.  6$  5      � 5   
   $��1��6   $��0��6   .+�5      &   System.Windows.Controls.DataGrid  -   5        
  Name$
 NPCDataGrid��5      ��.     AutoGenerateColumns .     
IsReadOnly .    CanUserResizeColumns .  $��Disabled=�6�   $��Visible=�6�   
�������   Columns 5      4  .System.Windows.Controls.DataGridTemplateColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$	 NPC��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6:      CellTemplate 5       �� 5!      .R�5"   "   �� $ 
Horizontal=�6-   .+�5#   &   �� $��5q�6F   ��60   �� *Name+6F   �� 5$   &   $��5q�6F   ��60   �� *Race+6F   �� 5%   &   $��5q�6H   ��60   �� *Gender+6H   �� 5&   &   $��5q�6K   ��60   �� *	AgeChrono+6K   �� 5'   &   $��5q�6L   ��60   �� *
Occupation+6L   5(   #   5)      5+      5-      5/      � 50   
   $��1��6   $��1��6   .+�51      �� -   5         ��Name$ NPCView��51      52      53      55      