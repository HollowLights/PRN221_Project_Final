﻿#pragma checksum "..\..\..\ViewOrderOfTableWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8303924F2DE6633618B943DB5E1C3B0ED6F02B20"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Final_Project_PRN221;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace Final_Project_PRN221 {
    
    
    /// <summary>
    /// ViewOrderOfTable
    /// </summary>
    public partial class ViewOrderOfTable : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\..\ViewOrderOfTableWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lvOrderDetail;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\ViewOrderOfTableWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbServiceFee;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\ViewOrderOfTableWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbTableFee;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\ViewOrderOfTableWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbDiscount;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\ViewOrderOfTableWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbTotal;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\ViewOrderOfTableWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image qrImage;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\..\ViewOrderOfTableWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPay;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\..\ViewOrderOfTableWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnQr;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\..\ViewOrderOfTableWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnBacK;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.3.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Final_Project_PRN221;component/vieworderoftablewindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\ViewOrderOfTableWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.3.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.lvOrderDetail = ((System.Windows.Controls.ListView)(target));
            return;
            case 2:
            this.lbServiceFee = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.lbTableFee = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.tbDiscount = ((System.Windows.Controls.TextBox)(target));
            
            #line 67 "..\..\..\ViewOrderOfTableWindow.xaml"
            this.tbDiscount.LostFocus += new System.Windows.RoutedEventHandler(this.tbDiscount_LostFocus);
            
            #line default
            #line hidden
            
            #line 67 "..\..\..\ViewOrderOfTableWindow.xaml"
            this.tbDiscount.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.tbDiscount_PreviewKeyDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.lbTotal = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.qrImage = ((System.Windows.Controls.Image)(target));
            return;
            case 7:
            this.btnPay = ((System.Windows.Controls.Button)(target));
            
            #line 83 "..\..\..\ViewOrderOfTableWindow.xaml"
            this.btnPay.Click += new System.Windows.RoutedEventHandler(this.btnPay_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnQr = ((System.Windows.Controls.Button)(target));
            
            #line 84 "..\..\..\ViewOrderOfTableWindow.xaml"
            this.btnQr.Click += new System.Windows.RoutedEventHandler(this.btnQr_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btnBacK = ((System.Windows.Controls.Button)(target));
            
            #line 85 "..\..\..\ViewOrderOfTableWindow.xaml"
            this.btnBacK.Click += new System.Windows.RoutedEventHandler(this.btnBacK_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

