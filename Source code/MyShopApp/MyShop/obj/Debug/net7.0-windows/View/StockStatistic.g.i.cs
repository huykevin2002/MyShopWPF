﻿#pragma checksum "..\..\..\..\View\StockStatistic.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4EB505246FEEB3B5FA005F0FB4BA66BE63839CFD"
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
using LiveCharts.Wpf;
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
    /// StockStatistic
    /// </summary>
    public partial class StockStatistic : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\..\View\StockStatistic.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MyShop.View.StockStatistic stockStatisticPanel;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\View\StockStatistic.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton btnDay;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\View\StockStatistic.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton btnWeek;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\View\StockStatistic.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton btnMonth;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\View\StockStatistic.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton btnYear;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\..\View\StockStatistic.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel chooseDate;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\..\View\StockStatistic.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker datePickerFrom;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\..\View\StockStatistic.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker datePickerTo;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\..\View\StockStatistic.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LiveCharts.Wpf.Axis LineY;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\..\View\StockStatistic.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LiveCharts.Wpf.Axis LineX;
        
        #line default
        #line hidden
        
        
        #line 91 "..\..\..\..\View\StockStatistic.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LiveCharts.Wpf.LineSeries LineSeries;
        
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
            System.Uri resourceLocater = new System.Uri("/MyShop;component/view/stockstatistic.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\StockStatistic.xaml"
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
            this.stockStatisticPanel = ((MyShop.View.StockStatistic)(target));
            return;
            case 2:
            this.btnDay = ((System.Windows.Controls.RadioButton)(target));
            
            #line 29 "..\..\..\..\View\StockStatistic.xaml"
            this.btnDay.Click += new System.Windows.RoutedEventHandler(this.btnDay_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnWeek = ((System.Windows.Controls.RadioButton)(target));
            
            #line 30 "..\..\..\..\View\StockStatistic.xaml"
            this.btnWeek.Click += new System.Windows.RoutedEventHandler(this.btnWeek_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnMonth = ((System.Windows.Controls.RadioButton)(target));
            
            #line 31 "..\..\..\..\View\StockStatistic.xaml"
            this.btnMonth.Click += new System.Windows.RoutedEventHandler(this.btnMonth_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnYear = ((System.Windows.Controls.RadioButton)(target));
            
            #line 32 "..\..\..\..\View\StockStatistic.xaml"
            this.btnYear.Click += new System.Windows.RoutedEventHandler(this.btnYear_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 45 "..\..\..\..\View\StockStatistic.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Back_Button_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.chooseDate = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 8:
            this.datePickerFrom = ((System.Windows.Controls.DatePicker)(target));
            
            #line 51 "..\..\..\..\View\StockStatistic.xaml"
            this.datePickerFrom.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.datePickerFrom_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.datePickerTo = ((System.Windows.Controls.DatePicker)(target));
            
            #line 54 "..\..\..\..\View\StockStatistic.xaml"
            this.datePickerTo.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.datePickerTo_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            case 10:
            this.LineY = ((LiveCharts.Wpf.Axis)(target));
            return;
            case 11:
            this.LineX = ((LiveCharts.Wpf.Axis)(target));
            return;
            case 12:
            this.LineSeries = ((LiveCharts.Wpf.LineSeries)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

