using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoSystem.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {

        protected void OnPropertyChanged(string parameter)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(parameter));
        }

        protected void OnProperiesChanged(string[] parameters)
        {
            foreach (string param in parameters)
            {
                OnPropertyChanged(param);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

    }
}
