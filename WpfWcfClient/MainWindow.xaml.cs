using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
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
using WpfWcfClient.MangaReference;

namespace WpfWcfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        public string _editButtonString;

        public string EditButtonString
        {
            get 
            { 
                return _editButtonString; 
            }

            set
            {
                _editButtonString = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(_editButtonString)));
            }
        }

        
        public bool _IsEditModeOn;
        public bool IsEditModeOn
        {
            get
            {
                return _IsEditModeOn;
            }

            set
            {
                _IsEditModeOn = value;
                if (_IsEditModeOn)
                {
                    _editButtonString = "Edit";
                }
                else
                {
                    _editButtonString = "Add";
                }
            }
        }

        public string _containsString;

        public string ContainsString
        {
            get { return _containsString; }
            set 
            { 
                _containsString = value; 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(_containsString)));
            }
        }

        public MangaClient mangaClient;
        public Manga[] Mangas;
        public Manga SelectedManga;

        public MainWindow()
        {
            ContainsString = "?";
            InitializeComponent();
            DataContext = this;
            IsEditModeOn = false;
            InstanceContext context = new InstanceContext(new CallBack(this));
            mangaClient = new MangaClient(context);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void btnAddModify_Click(object sender, RoutedEventArgs e)
        {
            if (IsEditModeOn)
            {
                mangaClient.Edit(SelectedManga.Id.Value, createManga());
            }
            else
            {
                mangaClient.Add(createManga());
            }
        }

        private Manga createManga()
        {
            return new Manga()
            {
                Author = txbAuthor.Text,
                Description = txbDescription.Text,
                ReleaseDate = dpDate.SelectedDate.Value,
                Rating = float.Parse(txbRating.Text),
                Title = txbTitle.Text
            };
        }
        private void btnCheckContains_Click(object sender, RoutedEventArgs e)
        {
            var tmp = createManga();
            mangaClient.Contains(createManga());
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            mangaClient.Delete(int.Parse(txbId.Text));
        }

        private void btnGetAll_Click(object sender, RoutedEventArgs e)
        {
            Mangas = mangaClient.GetAll();
            lvItems.ItemsSource = Mangas;
        }

   
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
           Mangas = mangaClient.GetAllByAuthor(txbSearchAuthor.Text);
           lvItems.ItemsSource = Mangas;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!IsEditModeOn)
            {
                IsEditModeOn = true;
                SelectedManga = (Manga)((Button)sender).Tag;
                txbAuthor.Text = SelectedManga.Author;
                txbDescription.Text = SelectedManga.Description;
                dpDate.SelectedDate = SelectedManga.ReleaseDate;
                txbRating.Text = SelectedManga.Rating.ToString();
                txbTitle.Text = SelectedManga.Title;
            }
            else
            {
                IsEditModeOn = false;
            }
        }
    }
}
