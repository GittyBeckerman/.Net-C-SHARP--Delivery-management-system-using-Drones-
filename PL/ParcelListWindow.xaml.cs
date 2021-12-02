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
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for ParcelListWindow.xaml
    /// </summary>
    public partial class ParcelListWindow : Window
    {
        IBL.IBL bl;

        public ParcelListWindow(IBL.IBL blMain)
        {
            InitializeComponent();
            bl = blMain;
            ParcelListView.ItemsSource = bl.GetParcelsBL();
        }

        private void ViewCustomerWindow(object sender, RoutedEventArgs e)
        {
            new ParcelWindow(bl).Show();
        }
    }
}