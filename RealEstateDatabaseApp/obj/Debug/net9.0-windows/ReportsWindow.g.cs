﻿#pragma checksum "..\..\..\ReportsWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0A1661714667BE073C576CE79B95A4791B0F7184"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace RealEstateDatabaseApp {
    
    
    /// <summary>
    /// ReportsWindow
    /// </summary>
    public partial class ReportsWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\ReportsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboBoxReports;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\ReportsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel CriteriaPanel;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\ReportsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker DateFrom;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\ReportsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TxtEstateId;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\ReportsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.WebBrowser Browser;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/RealEstateDatabaseApp;component/reportswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\ReportsWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.ComboBoxReports = ((System.Windows.Controls.ComboBox)(target));
            
            #line 16 "..\..\..\ReportsWindow.xaml"
            this.ComboBoxReports.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBoxReports_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.CriteriaPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 3:
            this.DateFrom = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 4:
            this.TxtEstateId = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            
            #line 62 "..\..\..\ReportsWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.GenerateReport_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Browser = ((System.Windows.Controls.WebBrowser)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

