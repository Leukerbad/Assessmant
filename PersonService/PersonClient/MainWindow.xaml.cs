using PersonService.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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

namespace PersonClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HttpClient client;
        public ObservableCollection<Person> _people;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadGrid();
        }

        private void LoadGrid()
        {
            ConfigureClient();
            GetPeople();
        }

        private void ConfigureClient()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7174");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async void GetPeople()
        {
            HttpResponseMessage response = await client.GetAsync("/api/Person");
            if (response.IsSuccessStatusCode)
            {
                _people = new ObservableCollection<Person>(await response.Content.ReadAsAsync<IEnumerable<Person>>());
                dg_pe.ItemsSource = _people;
            }
        }

        private void btn_det_Click(object sender, RoutedEventArgs e)
        {
            Person person = new Person(tb_id.Text, tb_fn.Text, tb_ln.Text, tb_em.Text, tb_ge.Text, tb_ac.Text, tb_un.Text, tb_no.Text);
            PostPerson(person);
        }

        private async void PostPerson(Person person)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("/api/Person", person);

            if (response.IsSuccessStatusCode)
            {
                LoadGrid();
            }
        }
    }
}
