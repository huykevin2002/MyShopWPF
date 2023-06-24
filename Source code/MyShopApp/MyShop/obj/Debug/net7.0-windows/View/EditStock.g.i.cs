﻿#pragma checksum "..\..\..\..\View\EditStock.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "CFB677FA2042A7F11715DB16AC94342B50B7B5DA"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using FontAwesome.Sharp;
using MyShop.View;
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


namespace MyShop.View {
    
    
    /// <summary>
    /// EditStock
    /// </summary>
    public partial class EditStock : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\..\View\EditStock.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MyShop.View.EditStock editStockPanel;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\View\EditStock.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtWarning;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\..\View\EditStock.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid EditStockForm;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\..\View\EditStock.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtTitle;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\..\..\View\EditStock.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtAuthor;
        
        #line default
        #line hidden
        
        
        #line 125 "..\..\..\..\View\EditStock.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtYear;
        
        #line default
        #line hidden
        
        
        #line 153 "..\..\..\..\View\EditStock.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbBoxClassification;
        
        #line default
        #line hidden
        
        
        #line 167 "..\..\..\..\View\EditStock.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtSellingPrice;
        
        #line default
        #line hidden
        
        
        #line 190 "..\..\..\..\View\EditStock.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtPurchasePrice;
        
        #line default
        #line hidden
        
        
        #line 213 "..\..\..\..\View\EditStock.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtQuantity;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.3.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MyShop;V1.0.0.0;component/view/editstock.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\EditStock.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.3.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.editStockPanel = ((MyShop.View.EditStock)(target));
            return;
            case 2:
            
            #line 25 "..\..\..\..\View\EditStock.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Back_Button_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 36 "..\..\..\..\View\EditStock.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Save_Button_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.txtWarning = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.EditStockForm = ((System.Windows.Controls.Grid)(target));
            return;
            case 6:
            this.txtTitle = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.txtAuthor = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.txtYear = ((System.Windows.Controls.TextBox)(target));
            
            #line 139 "..\..\..\..\View\EditStock.xaml"
            this.txtYear.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.txtYear_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 9:
            this.cbBoxClassification = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 10:
            this.txtSellingPrice = ((System.Windows.Controls.TextBox)(target));
            
            #line 181 "..\..\..\..\View\EditStock.xaml"
            this.txtSellingPrice.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.txtSellingPrice_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 11:
            this.txtPurchasePrice = ((System.Windows.Controls.TextBox)(target));
            
            #line 204 "..\..\..\..\View\EditStock.xaml"
            this.txtPurchasePrice.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.txtPurchasePrice_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 12:
            this.txtQuantity = ((System.Windows.Controls.TextBox)(target));
            
            #line 227 "..\..\..\..\View\EditStock.xaml"
            this.txtQuantity.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.txtQuantity_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

