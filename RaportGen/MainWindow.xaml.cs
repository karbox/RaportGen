using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Xml.Serialization;

namespace RaportGen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //MyDbContext ctx = new MyDbContext();
        List<Produkt> produktList = new List<Produkt>();

        List<Produkt> endList = new List<Produkt>();
        public DateTime data = DateTime.Today.Date;
        public string file = "baza.xml";
   
        public MainWindow()
        {
            try
            {
                //this.Dispatcher.UnhandledException += OnDispatcherUnhandledException;
                InitializeComponent();
            }
            catch (Exception exc)
            {
                MessageBox.Show("1. " + exc.Message);
            }
            try
            {

                if (!File.Exists(file))
                {
                    produktList.Add(new Produkt("Przyklad", 32.4));
                    Serialize(produktList, file);
                }
                    produktList = Deserialize(file);
                    this.Title = "Ruda v1.0";
                   // for (int i = 1; i < 10; i++)
              //          comboBox1.Items.Add(i);
             //       comboBox1.SelectedIndex = 0;
                    comboBoxFill();

            }
            catch (Exception exc)
            {
                MessageBox.Show("2. " + exc.Message);
                if (exc.InnerException != null)
                    MessageBox.Show(exc.InnerException.ToString());
            }

        }

        void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            string errorMessage = string.Format("An unhandled exception occurred: {0}", e.Exception.Message);
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }
        public void comboBoxFill()
        {
            comboBox.Items.Clear();
            refresh();
            foreach (Produkt p in produktList)
                comboBox.Items.Add(p.Sstring());
            comboBox.Items.Refresh();
        }
        public void refresh()
        {
            produktList = Deserialize(file);
        }
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            Produkt tempP = new Produkt();
            foreach(Produkt p in produktList)
            {
                //if (comboBox.Text.Contains(p.Name) && comboBox.Text.Contains(p.Price.ToString()))
                if (comboBox.Text== p.Sstring())
                {
                    tempP = p;
                    tempP.Ilosc=double.Parse(iloscBox.Text);
                    endList.Add(tempP);
                    
                }
            }
            dataGrid.ItemsSource = endList;
            dataGrid.Columns[0].Visibility = Visibility.Collapsed;
            dataGrid.Items.Refresh();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            Produkt produktTemp = dataGrid.SelectedItem as Produkt;
            endList.Remove(produktTemp);            
            dataGrid.Items.Refresh();
            
        }
        public void deleteP(Produkt p)
        {
            var pT = produktList.Where(x => x.Id == p.Id).FirstOrDefault();
            produktList.Remove(pT);            
        }
        public void save(Produkt p)
        {
            var pT = produktList.Where(x => x.Id == p.Id).FirstOrDefault();
            pT.Name = p.Name;
            pT.Price = p.Price;
        }
        public void addP(Produkt p)
        {
            produktList.Add(p);
        }

        private void dbaseButton_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1(produktList);
            window1.Show();
            window1.Owner = this;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            double cenaRazem=0;
            string date = datePicker.Text;
            StreamWriter f = new StreamWriter("raport.txt");
            f.WriteLine("Raport z dnia: " + date);//data.ToString("dd/MM/yyyy"));
            f.WriteLine();
            foreach (Produkt p in endList)
            {
                string sztuk = "";
                if (p.Ilosc == 1)
                {
                    sztuk = " sztuka";
                }
                else if (p.Ilosc > 1 && p.Ilosc < 5)
                {
                    sztuk = " sztuki";
                }
                else sztuk = " sztuk";
                cenaRazem += p.Price*p.Ilosc;
                f.WriteLine("*"+p.Name + " - "+ p.Ilosc + sztuk + " - " + p.Price + "zł/szt");
            }
            f.WriteLine();
            f.WriteLine("Podsumowanie: target został wyrobiony(" + cenaRazem + "zł)");
            f.Close();
            Process.Start("raport.txt");
        }
        public void Serialize(List<Produkt> cars, string file)
        {
            XmlSerializer serializer = new XmlSerializer(
                typeof(List<Produkt>), new XmlRootAttribute("cars"));
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(file);
                serializer.Serialize(writer, cars);
                writer.Close();
            }
            catch
            {
                Console.WriteLine("Serialization problem");
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }
        public List<Produkt> Deserialize(string file)
        {
            XmlSerializer serializer = new XmlSerializer(
                typeof(List<Produkt>), new XmlRootAttribute("cars"));
            try
            {
                var reader = new StreamReader(file);
                return (List<Produkt>)serializer.Deserialize(reader);
            }
            catch
            {
                Console.WriteLine("Deserialization problem");
                return null;
            }
        }
    }

}
