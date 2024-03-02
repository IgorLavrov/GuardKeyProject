using GuardKeyProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace GuardKeyProject.ViewModels
{
    public  class BaseUserRecordViewModel: INotifyPropertyChanged
    {

        private UserRecord _userRecord;

        public INavigation Navigation { get; set; }

        public UserRecord UserRecord
        {
            get { return _userRecord; }
            set { _userRecord = value; OnPropertyChanged(); }

        }
        bool isBusy = false;

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                setProperty(ref isBusy, value);
            }
        }

        protected bool setProperty<T>(ref T backingRecord, T value, [CallerMemberName] string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingRecord, value))
                return false;
            backingRecord = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }


        public event PropertyChangedEventHandler PropertyChanged;
       

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

      

    }
}
