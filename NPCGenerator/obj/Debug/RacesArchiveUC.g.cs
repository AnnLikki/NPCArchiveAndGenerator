﻿#pragma checksum "..\..\RacesArchiveUC.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "56EFB2233AECDAA6A4FF69F76BF743CB38BF55D805E568FD201A1A34CF23975B"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace NPCGenerator {
    
    
    /// <summary>
    /// RacesArchiveUC
    /// </summary>
    public partial class RacesArchiveUC : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\RacesArchiveUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addRaceBtn;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\RacesArchiveUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox filterTB;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\RacesArchiveUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button clearFilterBtn;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\RacesArchiveUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid RaceDataGrid;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\RacesArchiveUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContentControl RaceView;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/NPCGenerator;component/racesarchiveuc.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\RacesArchiveUC.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.addRaceBtn = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\RacesArchiveUC.xaml"
            this.addRaceBtn.Click += new System.Windows.RoutedEventHandler(this.addRaceBtn_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.filterTB = ((System.Windows.Controls.TextBox)(target));
            
            #line 22 "..\..\RacesArchiveUC.xaml"
            this.filterTB.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.filterTB_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.clearFilterBtn = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\RacesArchiveUC.xaml"
            this.clearFilterBtn.Click += new System.Windows.RoutedEventHandler(this.clearFilterBtn_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.RaceDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 30 "..\..\RacesArchiveUC.xaml"
            this.RaceDataGrid.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.RaceDataGrid_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.RaceView = ((System.Windows.Controls.ContentControl)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

