using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
//Készíts egy könyv objektumot, melyből hozz létre példányokat egy listában(sorszám, oldalszám). 
//A lista jelenjen meg az űrlapon.A könyvnek létrehozásakor véletlen szám sorsolással határozd meg az oldalszámát[50;150]. 
//A könyv alaphelyzetben zárva legyen.

//Készítsd el a következő metódusokat, mellyel a könyvet kezelni tudod:

//kinyit – egy véletlen számú oldalon nyissa ki a könyvet.
//bezar – zárja be a könyvet
//lapoz – lehessen egyesével lapozni a könyvet az általad megadott irányba (ha a könyv elejére vagy végére érsz, akkor az utolsó lapozás zárja be a könyvet)
//porget – lehessen lapozni a könyvet véletlenszerű lapszámmal az általad megadott irányba(ilyenkor nem zárható be)
//Az űrlapon lévő listában egy könyvre kattintva jelenjen meg a könyv állapota(nyitva / csukva; nyitott könyvnél az aktuális oldal)
//  és mindig a megfelelő gombok legyenek aktívak.

//Gondoskodj a megfelelő hibakezelésekről és üzenetekről.
namespace Konyvek
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Konyv> Konyvek = new ObservableCollection<Konyv>();
        Random rnd = new Random();
        
        public MainWindow()
        {
            InitializeComponent();
            for (int i = 0; i < 10; i++)
            {
                Konyvek.Add(new Konyv
                {
                    Sorszam = i,
                    OldalakSzama = rnd.Next(50, 150),
                    
                    AktualisOldal = -1
                });
            }
            DgrKonyvLista.ItemsSource = Konyvek;
        

        }

        private void Kinyit_Click(object sender, RoutedEventArgs e)
        {
            Konyvek[DgrKonyvLista.SelectedIndex].AktualisOldal = rnd.Next(1, Konyvek[DgrKonyvLista.SelectedIndex].OldalakSzama);
            OldalKiir();

            Nyitott();

        }

        

        private void Becsuk_Click(object sender, RoutedEventArgs e)
        {
            Konyvek[DgrKonyvLista.SelectedIndex].AktualisOldal = -1;
            Csukott();
        }

        private void EloreLapoz_Click(object sender, RoutedEventArgs e)
        {
            if (Konyvek[DgrKonyvLista.SelectedIndex].AktualisOldal < Konyvek[DgrKonyvLista.SelectedIndex].OldalakSzama - 1)
            {
                Konyvek[DgrKonyvLista.SelectedIndex].AktualisOldal= Konyvek[DgrKonyvLista.SelectedIndex].AktualisOldal+2;
                Nyitott();
                OldalKiir();
            }
            else
            {
                Konyvek[DgrKonyvLista.SelectedIndex].AktualisOldal = -1;
                Csukott();
            }
        }

        private void HatraLapoz_Click(object sender, RoutedEventArgs e)
        {
            if (Konyvek[DgrKonyvLista.SelectedIndex].AktualisOldal > 1)
            {
                Konyvek[DgrKonyvLista.SelectedIndex].AktualisOldal = Konyvek[DgrKonyvLista.SelectedIndex].AktualisOldal - 2;
                Nyitott();
                OldalKiir();
            }
            else
            {
                Konyvek[DgrKonyvLista.SelectedIndex].AktualisOldal = -1;
                Csukott();
            }
        }

        private void Porget_Click(object sender, RoutedEventArgs e)
        {
            Nyitott();
            Konyvek[DgrKonyvLista.SelectedIndex].AktualisOldal = rnd.Next(1, Konyvek[DgrKonyvLista.SelectedIndex].OldalakSzama);
            OldalKiir();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DgrKonyvLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BtnEloreLapoz.IsEnabled = true;
            BtnHatraLapoz.IsEnabled = true;
            BtnPorget.IsEnabled = true;
            
            if (Konyvek[DgrKonyvLista.SelectedIndex].AktualisOldal == -1)
            {
                Csukott();
            }
            else
            {
                OldalKiir();
                Nyitott();
            }
        }


        private void Nyitott()
        {
            BrdrCsukottKonyv.Visibility = Visibility.Collapsed;
            BrdrNyitottKonyv.Visibility = Visibility.Visible;
            BtnBecsuk.IsEnabled = true;
            BtnKinyit.IsEnabled = false;
            
        }

        private void Csukott()
        {
            BrdrCsukottKonyv.Visibility = Visibility.Visible;
            BrdrNyitottKonyv.Visibility = Visibility.Collapsed;
            BtnBecsuk.IsEnabled = false;
            BtnKinyit.IsEnabled = true;
            

        }

        private void OldalKiir()
        {
            int utolsoOldal;
            LblAktOldal.Content = Konyvek[DgrKonyvLista.SelectedIndex].AktualisOldal;
            if (Konyvek[DgrKonyvLista.SelectedIndex].OldalakSzama % 2 == 1
                && Konyvek[DgrKonyvLista.SelectedIndex].AktualisOldal == Konyvek[DgrKonyvLista.SelectedIndex].OldalakSzama)
                utolsoOldal = 0;
            else utolsoOldal = Konyvek[DgrKonyvLista.SelectedIndex].AktualisOldal+1;
                     LblKovOldal.Content = utolsoOldal;
        }

        
    }
}
