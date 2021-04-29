using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace WpfApp1 {

    //public enum OperDistinct{
    //    "+" = 0,
    //    DIV,
    //    MINUS,
    //    PLUS
    //}

    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window {
        private CalcViewModel _calc;

        public MainWindow() {
            InitializeComponent();

            _calc = new CalcViewModel();
            CalcGrid.DataContext = _calc;
        }


        private void Num_Click(object sender, RoutedEventArgs e) {
            _calc.CheckIfRefresh();
            string appendStr = (string)((System.Windows.Controls.Button)sender).Content;

            if (_calc.Oprt == String.Empty || _calc.Oprt == null) {
                if(_calc.Val1 == "0" && appendStr=="0") {
                }
                else {
                    _calc.Val1 += appendStr;
                }
            }
            else {
                if (_calc.Val2 == "0" && appendStr == "0") {
                }
                else {
                    _calc.Val2 += appendStr;
                }
            }
        }

        private void Oper_Click(object sender, RoutedEventArgs e) {
            if(_calc.Val1 != String.Empty && _calc.Val1 != null && _calc.ShouldRefresh == true) {
                _calc.ShouldRefresh = false;
            }

            string newOprt = (string)((System.Windows.Controls.Button)sender).Content;

            try {
                if (_calc.Val1 == String.Empty || _calc.Val1 == null) {
                    if(newOprt == "+" || newOprt == "-") {
                        if (newOprt != "=") {
                            _calc.Oprt = newOprt;
                        }
                    }
                }else if (_calc.Val1 == "+" || _calc.Val1 == "-") {
                    if (newOprt == "+" || newOprt == "-") {
                        if (newOprt != "=") {
                            _calc.Oprt = newOprt;
                        }
                    }
                }
                else if (_calc.Oprt == String.Empty || _calc.Oprt == null) {
                    if (newOprt != "=") {
                        _calc.Oprt = newOprt;
                    }
                }
                else if (_calc.Val2 == String.Empty || _calc.Val2 == null) {
                    if (newOprt != "=") {
                        _calc.Oprt = newOprt;
                    }
                }
                else {
                    double tmp1 = double.Parse(_calc.Val1);
                    double tmp2 = double.Parse(_calc.Val2);
                    double res = 0.0;
                    switch (_calc.Oprt) {
                        case "*":
                            res = tmp1 * tmp2;
                            break;
                        case "/":
                            if (tmp2 == 0) {
                                throw new DivideByZeroException();
                            }
                            res = tmp1 / tmp2;
                            break;
                        case "-":
                            res = tmp1 - tmp2;
                            break;
                        case "+":
                            res = tmp1 + tmp2;
                            break;
                    }
                    _calc.Val1 = res.ToString();
                    _calc.LastRes = res.ToString();
                    

                    switch (newOprt) {
                        case "=":
                            _calc.Oprt = string.Empty;
                            _calc.Val2 = string.Empty;
                            _calc.ShouldRefresh = true;
                            break;
                        default:
                            _calc.Oprt = newOprt;
                            _calc.Val2 = string.Empty;
                            break;
                    }
                }
            }
            catch(DivideByZeroException) {
                MessageBox.Show("0으로 나눌 수 없습니다.");
                _calc.Oprt = string.Empty;
                _calc.Val2 = string.Empty;
            }
            catch {
                MessageBox.Show("알 수 없는 에러가 발생했습니다.");
            }
        }


        private void Dot_Click(object sender, RoutedEventArgs e) {
            string appendStr = (string)((System.Windows.Controls.Button)sender).Content;

            if (_calc.Oprt == String.Empty || _calc.Oprt == null) {
                if (_calc.Val1.Contains(".") == false) {
                    _calc.Val1 += appendStr;
                }
                
            }
            else {
                if (_calc.Val2.Contains(".") == false) {
                    _calc.Val2 += appendStr;
                }
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e) {
            _calc.Val1 = string.Empty;
            _calc.Oprt = string.Empty;
            _calc.Val2 = string.Empty;
        }

        private void Back_Click(object sender, RoutedEventArgs e) {

            if (_calc.Oprt == String.Empty || _calc.Oprt == null) {
                if(_calc.Val1.Length >= 1) {
                    _calc.Val1 = _calc.Val1.Substring(0, _calc.Val1.Length - 1);
                }
            }
            else {
                if (_calc.Val2.Length >= 1) {
                    _calc.Val2 = _calc.Val2.Substring(0, _calc.Val2.Length - 1);
                }
            }
        }

    }
}
