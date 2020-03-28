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

namespace lab2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string filePath = "osoby.txt";
        public MainWindow()
        {
            InitializeComponent();
            //load data
            Osoba[] osoby = Dane.Load(filePath);
            if(osoby != null)
            {
                foreach (Osoba o in osoby)
                    Lista.Items.Add(o);
            }
        }

        bool IsEmpty(TextBox box)
        {
            Border border = box.Parent as Border;
            if (box.Text.Trim() == "")
            {
                border.BorderThickness = new Thickness(1);
                return true;
            } else
            {
                border.BorderThickness = new Thickness(0);
                return false;
            }
        }

        bool OnList(Osoba o)
        {
            foreach(Osoba x in Lista.Items)
            {
                if (x.SameAs(o)) return true;
            }
            return false;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if(!IsEmpty(TB_imie) & !IsEmpty(TB_nazwisko))
            {
                Osoba nowa = new Osoba(TB_imie.Text, TB_nazwisko.Text, (int)wiek.Value);
                if (!OnList(nowa))
                {
                    Lista.Items.Add(nowa);
                }
                else
                {
                    MessageBox.Show("Osoba znajduje się już na liście", "Uwaga!", MessageBoxButton.OK);      
                }
                Clear();
            }
        }

        void Clear()
        {
            TB_imie.Text = "";
            TB_nazwisko.Text = "";
            wiek.Value = 20;
            btnEdytuj.IsEnabled = false;
            btnUsun.IsEnabled = false;
            Lista.SelectedIndex = -1;
        }

        private void Lista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Lista.SelectedIndex >= 0)
            {
                LoadData((Osoba)Lista.SelectedItem);
                btnEdytuj.IsEnabled = true;
                btnUsun.IsEnabled = true;
            }
        }

        void LoadData(Osoba osoba)
        {
            TB_imie.Text = osoba.Imie;
            TB_nazwisko.Text = osoba.Nazwisko;
            wiek.Value = osoba.Wiek;
        }

        private void BtnEdytuj_Click(object sender, RoutedEventArgs e)
        {
            if(Lista.SelectedIndex >= 0)
            {
                if (!IsEmpty(TB_imie) & !IsEmpty(TB_nazwisko))
                {
                    Osoba nowa = new Osoba(TB_imie.Text, TB_nazwisko.Text, (int)wiek.Value);
                    if (!OnList(nowa))
                    {
                        Osoba stara = Lista.Items[Lista.SelectedIndex] as Osoba;
                        stara.Change(TB_imie.Text, TB_nazwisko.Text, (int)wiek.Value);
                        Lista.Items.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("Osoba znajduje się już na liście", "Uwaga!", MessageBoxButton.OK);
                    }
                    Clear();
                }
            }
        }

        private void BtnUsun_Click(object sender, RoutedEventArgs e)
        {
            if(Lista.SelectedIndex >= 0)
            {
                var dialog = MessageBox.Show("Na pewno chcesz usunąć?", "Uwaga", MessageBoxButton.YesNo);

                if(dialog == MessageBoxResult.Yes)
                {
                    Lista.Items.RemoveAt(Lista.SelectedIndex);
                    Clear();
                }
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(Lista.Items.Count > 0)
            {
                Osoba[] osoby = new Osoba[Lista.Items.Count];
                int i = 0;
                foreach (var o in Lista.Items)
                    osoby[i++] = o as Osoba;
                Dane.Save(filePath, osoby);
            }
        }
    }
}
