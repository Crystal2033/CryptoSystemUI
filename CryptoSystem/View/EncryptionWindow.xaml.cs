﻿using CryptoSystem.ViewModel;
using System;
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

namespace CryptoSystem.View
{
    /// <summary>
    /// Interaction logic for EncryptionWindow.xaml
    /// </summary>
    public partial class EncryptionWindow : Window
    {
        public EncryptionWindow()
        {
            InitializeComponent();
            DataContext = new EncryptionDialogVM();
        }

        public EncryptionDialogVM? getContext()
        {
            return DataContext as EncryptionDialogVM;
        }

        private void OnMakeEncryptionClicked(object sender, RoutedEventArgs e)
        {
            getContext().EncryptionInfo.CryptStatus = Model.Status.RUNNING;
            DialogResult = true;
        }
    }
}
