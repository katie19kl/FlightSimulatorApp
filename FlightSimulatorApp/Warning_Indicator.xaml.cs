﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for Warning_Indicator.xaml
    /// </summary>
    public partial class Warning_Indicator : UserControl
    {
        public VM_Warnings vmWarnings;


        public Warning_Indicator()
        {
            InitializeComponent();
        }

        public void SetVM(VM_Warnings vm)
        {
            this.vmWarnings = vm;
        }

    }
}
