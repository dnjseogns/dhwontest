using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1 {
    public class CalcViewModel : INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string properyName) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(properyName));
            }
        }
        private string _Res = String.Empty;
        private string _val1 = String.Empty;
        private string _oprt = String.Empty;
        private string _val2 = String.Empty;
        private string _lastRes = String.Empty;
        private string _lastOprt = String.Empty;
        private bool _shouldRefresh = false;


        public void CheckIfRefresh() {
            if (_shouldRefresh == true) {
                if(_val2 == String.Empty || _val2 == null) {
                    if(_oprt == String.Empty || _oprt == null) {
                        if (_val1 == _lastRes) {
                            _val1 = string.Empty;
                            _shouldRefresh = false;
                        }
                    }
                }
            }
        }

        public bool ShouldRefresh {
            get { return _shouldRefresh; }
            set { _shouldRefresh = value; }
        }


        public string LastOprt {
            get { return _lastOprt; }
            set { _lastOprt = value; }
        }

        public string LastRes {
            get { return _lastRes; }
            set {
                _lastRes = value;
                OnPropertyChanged("LastRes");
            }
        }
        public string Res {
            get { return _Res; }
            set { _Res = value;
                OnPropertyChanged("Res");
            }
        }
        public string Val1 {
            get { return _val1; }
            set {
                _val1 = value;
                Res = _val1 + _oprt + _val2;
            }
        }
        public string Oprt {
            get { return _oprt; }
            set { 
                _oprt = value;
                Res = _val1 + _oprt + _val2;
            }
        }
        public string Val2 {
            get { return _val2; }
            set { 
                _val2 = value;
                Res = _val1 + _oprt + _val2;
            }
        }

    }
}
