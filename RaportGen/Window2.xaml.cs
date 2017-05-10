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

namespace RaportGen
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        private bool add = true;
        private int id;
        public Window2()
        {
            InitializeComponent();
            add = true;
        }

        public Window2(string name, double price, int ID)
        {
            InitializeComponent();
            nazwaBox.Text = name;
            cenBox.Text = price.ToString();
            id = ID;
            add = false;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            string nazwa;
            double cena;
            if (double.TryParse(cenBox.Text, out cena))
            {
                nazwa = nazwaBox.Text;
                if (add)
                {
                    ((Window1)this.Owner).save(nazwa, cena);
                }else
                {
                    ((Window1)this.Owner).save2(nazwa, cena,id);
                }
                Close();
            }
            else MessageBox.Show("Podaj prawidłową cene np. 34,99", "Warning");

        }
    }
}
