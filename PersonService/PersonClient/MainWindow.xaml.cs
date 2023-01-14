using PersonService.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
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
            try
            {
                HttpResponseMessage response = await client.GetAsync("/api/Person");
                if (response.IsSuccessStatusCode)
                {
                    _people = new ObservableCollection<Person>(await response.Content.ReadAsAsync<IEnumerable<Person>>());
                    dg_pe.ItemsSource = _people;
                }
            }
            catch (Exception)
            {
                
            }
}

        private void btn_det_Click(object sender, RoutedEventArgs e)
        {
            Person person = new Person(tb_id.Text, tb_fn.Text, tb_ln.Text, tb_em.Text, tb_ge.Text, tb_ac.IsChecked.ToString(), tb_un.Text, tb_no.Text);
            SavePerson(person);
        }

        private async void SavePerson(Person person)
        {
            HttpResponseMessage response = await client.GetAsync("/api/Person/" + person.Id);
            if (response.IsSuccessStatusCode)
            {
                response = await client.PutAsJsonAsync("/api/Person/" + person.Id, person);
            }
            else if(response.StatusCode == HttpStatusCode.NotFound)
            {
                response = await client.PostAsJsonAsync("/api/Person", person);
            }
            LoadGrid();
        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            tb_id.Text ="";
            tb_fn.Text ="";
            tb_ln.Text ="";
            tb_em.Text ="";
            tb_ge.Text ="";
            tb_ac.IsChecked = false;
            tb_un.Text ="";
            tb_no.Text ="";
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            DeletePerson(tb_id.Text);
        }

        private void dg_pe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Person person = (Person)dg_pe.CurrentItem;
            if(person != null)
            {
                tb_id.Text = person.Id;
                tb_fn.Text = person.FirstName;
                tb_ln.Text = person.LastName;
                tb_em.Text = person.Email;
                tb_ge.Text = person.Gender;
                tb_ac.IsChecked = person.Active;
                tb_un.Text = person.UserName;
                tb_no.Text = person.Notes;

            }
        }

        private async void DeletePerson(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync("/api/Person/" + id);

            if (response.IsSuccessStatusCode)
            {
                LoadGrid();
            }
        }
    }
}
