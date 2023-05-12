using CryptoSystem.ViewModel;
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
    /// Interaction logic for DecryptionWindow.xaml
    /// </summary>
    public partial class DecryptionWindow : Window
    {
        public DecryptionWindow()
        {
            InitializeComponent();
            DataContext = new DecryptionDialogVM();
        }

        public DecryptionDialogVM? getContext()
        {
            return DataContext as DecryptionDialogVM;
        }

        private void OnMakeDecryptionClicked(object sender, RoutedEventArgs e)
        {
            getContext().DecryptionInfo.CryptStatus = Model.Status.RUNNING;
            DialogResult = true;
        }
    }
}
