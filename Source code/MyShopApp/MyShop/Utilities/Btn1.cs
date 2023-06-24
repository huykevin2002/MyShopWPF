using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MyShop.Utilities
{
    public class Btn1 : Button
    {
        static Btn1()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Btn1), new FrameworkPropertyMetadata(typeof(Btn1)));
        }
    }
}
