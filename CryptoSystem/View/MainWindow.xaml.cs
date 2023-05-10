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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CryptoSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new CryptoSystemVM();
            //getContext().EncryptionWidgets.Add(new Model.EncryptionDTO() { FileToEncrypt = "1234.txt", ResultEncryptFile = "4321.txt" });
            //getContext().EncryptionWidgets.Add(new Model.EncryptionDTO() { FileToEncrypt = "1234.txt", ResultEncryptFile = "4321.txt" });
            //getContext().EncryptionWidgets.Add(new Model.EncryptionDTO() { FileToEncrypt = "1234.txt", ResultEncryptFile = "4321.txt" });
            //getContext().EncryptionWidgets.Add(new Model.EncryptionDTO() { FileToEncrypt = "1234.txt", ResultEncryptFile = "4321.txt" });
            //getContext().EncryptionWidgets.Add(new Model.EncryptionDTO() { FileToEncrypt = "1234.txt", ResultEncryptFile = "4321.txt" });
            //getContext().EncryptionWidgets.Add(new Model.EncryptionDTO() { FileToEncrypt = "1234.txt", ResultEncryptFile = "4321.txt" });
            //getContext().EncryptionWidgets.Add(new Model.EncryptionDTO() { FileToEncrypt = "1234.txt", ResultEncryptFile = "4321.txt" });
            //getContext().EncryptionWidgets.Add(new Model.EncryptionDTO() { FileToEncrypt = "1234.txt", ResultEncryptFile = "4321.txt" });
            //getContext().EncryptionWidgets.Add(new Model.EncryptionDTO() { FileToEncrypt = "1234.txt", ResultEncryptFile = "4321.txt" });
            //getContext().EncryptionWidgets.Add(new Model.EncryptionDTO() { FileToEncrypt = "1234.txt", ResultEncryptFile = "4321.txt" });
            //getContext().EncryptionWidgets.Add(new Model.EncryptionDTO() { FileToEncrypt = "1234.txt", ResultEncryptFile = "4321.txt" });
            //getContext().EncryptionWidgets.Add(new Model.EncryptionDTO() { FileToEncrypt = "1234.txt", ResultEncryptFile = "4321.txt" });
            //getContext().EncryptionWidgets.Add(new Model.EncryptionDTO() { FileToEncrypt = "1234.txt", ResultEncryptFile = "4321.txt" });
            //getContext().EncryptionWidgets.Add(new Model.EncryptionDTO() { FileToEncrypt = "1234.txt", ResultEncryptFile = "4321.txt" });
            //getContext().EncryptionWidgets.Add(new Model.EncryptionDTO() { FileToEncrypt = "1234.txt", ResultEncryptFile = "4321.txt" });
            //getContext().EncryptionWidgets.Add(new Model.EncryptionDTO() { FileToEncrypt = "1234.txt", ResultEncryptFile = "4321.txt" });
            //getContext().EncryptionWidgets.Add(new Model.EncryptionDTO() { FileToEncrypt = "1234.txt", ResultEncryptFile = "4321.txt" });
            //getContext().EncryptionWidgets.Add(new Model.EncryptionDTO() { FileToEncrypt = "1234.txt", ResultEncryptFile = "4321.txt" });
            //getContext().EncryptionWidgets.Add(new Model.EncryptionDTO() { FileToEncrypt = "1234.txt", ResultEncryptFile = "4321.txt" });
            //getContext().EncryptionWidgets.Add(new Model.EncryptionDTO() { FileToEncrypt = "1234.txt", ResultEncryptFile = "4321.txt" });
            //getContext().EncryptionWidgets.Add(new Model.EncryptionDTO() { FileToEncrypt = "1234.txt", ResultEncryptFile = "4321.txt" });
            //getContext().EncryptionWidgets.Add(new Model.EncryptionDTO() { FileToEncrypt = "1234.txt", ResultEncryptFile = "4321.txt" });
            //getContext().EncryptionWidgets.Add(new Model.EncryptionDTO() { FileToEncrypt = "1234.txt", ResultEncryptFile = "4321.txt" });
            //getContext().EncryptionWidgets.Add(new Model.EncryptionDTO() { FileToEncrypt = "1234.txt", ResultEncryptFile = "4321.txt" });
            //getContext().EncryptionWidgets.Add(new Model.EncryptionDTO() { FileToEncrypt = "1234.txt", ResultEncryptFile = "4321.txt" });
            //getContext().EncryptionWidgets.Add(new Model.EncryptionDTO() { FileToEncrypt = "1234.txt", ResultEncryptFile = "4321.txt" });
            //getContext().EncryptionWidgets.Add(new Model.EncryptionDTO() { FileToEncrypt = "1234.txt", ResultEncryptFile = "4321.txt" });
            //getContext().EncryptionWidgets.Add(new Model.EncryptionDTO() { FileToEncrypt = "1234.txt", ResultEncryptFile = "4321.txt" });
            //getContext().DecryptionWidgets.Add(new Model.DecryptionDTO() { FileToDecrypt = "1234.txt", ResultDecryptFile = "4321.txt" });
            //getContext().DecryptionWidgets.Add(new Model.DecryptionDTO() { FileToDecrypt = "1234.txt", ResultDecryptFile = "4321.txt" });
        }

        public CryptoSystemVM? getContext()
        {
            return DataContext as CryptoSystemVM;
        }
    }
}
