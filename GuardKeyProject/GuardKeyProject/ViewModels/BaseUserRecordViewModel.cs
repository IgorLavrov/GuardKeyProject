using GuardKeyProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace GuardKeyProject.ViewModels
{
    public  class BaseUserRecordViewModel: INotifyPropertyChanged
    {

        private UserRecord _ùserRecord;

        public UserRecord UserRecord
        {
            get { return _ùserRecord; }
            set { _ùserRecord = value; OnPropertyChanged(); }

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
        //protected void OnPropertyChanged([CallerMemberName] string propertyName="")
        //    {
        //    var changed=PropertyChanged;
        //    if (changed != null)
        //        return;

        //    changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}


        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;  // Remove this line

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //    public event PropertyChangedEventHandler PropertyChanged;

        //    UserRecordListViewModel lvm;
        //    public UserRecord Account { get; private set; }

        //    public UserRecordViewModel()
        //    {
        //        Account = new UserRecord();
        //    }

        //    public UserRecordListViewModel ListViewModel
        //    {
        //        get { return lvm; }
        //        set
        //        {
        //            if (lvm != value)
        //            {
        //                lvm = value;
        //                OnPropertyChanged("ListViewModel");
        //            }
        //        }
        //    }

        //    public string SourceGroupName
        //    {
        //        get { return Account.SourceGroupName; }
        //        set
        //        {
        //            if (Account.SourceGroupName != value)
        //            {
        //                Account.SourceGroupName = value;
        //                OnPropertyChanged("SourceGroupName");
        //            }
        //        }
        //    }

        //    public string ResourceName
        //    {
        //        get { return Account.ResourceName; }
        //        set
        //        {
        //            if (Account.ResourceName != value)
        //            {
        //                Account.ResourceName = value;
        //                OnPropertyChanged("ResourceName");
        //            }
        //        }
        //    }

        //    public string UserName
        //    {
        //        get { return Account.UserName; }
        //        set
        //        {
        //            if (Account.UserName != value)
        //            {
        //                Account.UserName = value;
        //                OnPropertyChanged("UserName");
        //            }
        //        }
        //    }

        //    public string Password
        //    {
        //        get { return Account.Password; }
        //        set
        //        {
        //            if (Account.Password != value)
        //            {
        //                Account.Password = value;
        //                OnPropertyChanged("Password");
        //            }
        //        }
        //    }

        //    public string Description 
        //    {
        //        get { return Account.Description; }
        //        set
        //        {
        //            if (Account.Description != value)
        //            {
        //                Account.Description = value;
        //                OnPropertyChanged("Description");
        //            }
        //        }
        //    }

        //    public bool IsValid
        //    {
        //        get
        //        {
        //            return ((!string.IsNullOrEmpty(SourceGroupName.Trim()))) ||
        //                (!string.IsNullOrEmpty(ResourceName.Trim())) ||
        //                (!string.IsNullOrEmpty(UserName.Trim())) ||
        //                (!string.IsNullOrEmpty(Password.Trim())) ||
        //                (!string.IsNullOrEmpty(Description.Trim()));
        //        }
        //    }

        //    protected void OnPropertyChanged(string propName)
        //    {
        //        if (PropertyChanged != null)
        //            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        //    }


    }
}
