using System;
using System.ComponentModel;

namespace GM.UI.Common
{
    public abstract class ObjectBase : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected internal void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

   
}