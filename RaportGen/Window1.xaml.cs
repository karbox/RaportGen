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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        List<Produkt> listaP = new List<Produkt>();
        public string file = "baza.xml";
        public Window1(List<Produkt> lista)
        {
            InitializeComponent();
            Title = "Edycja Bazy Danych";
            listaP = lista;
            dataGrid.ItemsSource = listaP;            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid.Columns[0].Visibility = Visibility.Collapsed;
            dataGrid.Columns[3].Visibility = Visibility.Collapsed;
        }
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            Produkt pTemp = dataGrid.SelectedItem as Produkt;
            listaP.Remove(pTemp);
            ((MainWindow)this.Owner).Serialize(listaP, file);
            //((MainWindow)this.Owner).deleteP(pTemp);
            dataGrid.Items.Refresh();
        }

        private void newButton_Click(object sender, RoutedEventArgs e)
        {
            Window2 window2 = new Window2();
            window2.Show();
            window2.Owner = this;
        }
        public void save(string name, double price)
        {
            Produkt p = new Produkt(name, price);
            listaP.Add(p);
            ((MainWindow)this.Owner).Serialize(listaP, file);
            //((MainWindow)this.Owner).addP(p);
            dataGrid.Items.Refresh();
        }
        public void save2(string name, double price, int ID)
        {
            var obj = listaP.FirstOrDefault(x => x.Id == ID);
            if (obj != null)
            {
                dataGrid.CommitEdit();
                obj.Price = price;
                obj.Name = name;
                ((MainWindow)this.Owner).Serialize(listaP, file);
                //((MainWindow)this.Owner).save(obj);
                dataGrid.CancelEdit();
                dataGrid.Items.Refresh();
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)this.Owner).comboBoxFill();
            Close();
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Produkt p = dataGrid.CurrentItem as Produkt;
            if(p!= null)
            {
                int id = p.Id;
                double price = p.Price;
                string name = p.Name;
                Window2 window2 = new Window2(name, price, id);
                window2.Show();
                window2.Owner = this;
            }
        }
    }
}
