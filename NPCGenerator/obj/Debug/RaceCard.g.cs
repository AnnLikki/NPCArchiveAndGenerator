﻿#pragma checksum "..\..\RaceCard.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "839CAA628FB65B763FED7D75E63DD9F346A09030F72CF526E6A52A5C90FF9163"
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
    /// RaceCard
    /// </summary>
    public partial class RaceCard : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 24 "..\..\RaceCard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button saveBtn;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\RaceCard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button deleteBtn;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\RaceCard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox nameTB;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\RaceCard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox descTB;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\RaceCard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox maturityTB;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\RaceCard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox expectancyTB;
        
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
            System.Uri resourceLocater = new System.Uri("/NPCGenerator;component/racecard.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\RaceCard.xaml"
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
            this.saveBtn = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\RaceCard.xaml"
            this.saveBtn.Click += new System.Windows.RoutedEventHandler(this.saveBtn_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.deleteBtn = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\RaceCard.xaml"
            this.deleteBtn.Click += new System.Windows.RoutedEventHandler(this.deleteBtn_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.nameTB = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.descTB = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.maturityTB = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.expectancyTB = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

