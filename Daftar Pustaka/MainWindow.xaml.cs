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
using System.Text.RegularExpressions;
using System.Reflection.Metadata;

namespace Daftar_Pustaka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        string inisialNama(string nama)
        {
            if (String.IsNullOrEmpty(nama))
            {
                return "";
            }

            string inisialNama = "";
            string buatInisial = nama.Trim();
            string[] inisialBaru = Regex.Split(buatInisial, @"[\u0020\u0009\u000D\u00A0]+");

            for (int i = 0; i < inisialBaru.Length; i++)
            {
                inisialBaru[i] = $"{inisialBaru[i][0].ToString()}{inisialBaru[i].Substring(1).ToLower()}";
            }

            if (inisialBaru.Length > 1)
            {
                //inisialNama = $"{inisialBaru[inisialBaru.Length - 1]}, {inisialBaru[0][0]}, ";
                inisialNama = $"{inisialBaru[inisialBaru.Length - 1]},";
                for (int i = 0; i < inisialBaru.Length - 1; i++)
                {
                    inisialNama = inisialNama + $" {inisialBaru[i]}";
                }
            }
            else
            {
                inisialNama = $"{inisialBaru[0]}";
            }

            if (!String.IsNullOrEmpty(this.NamaPenulis2_TextBox.Text) && !String.IsNullOrEmpty(this.NamaPenulis3_TextBox.Text) && !String.IsNullOrEmpty(this.NamaPenulis4_TextBox.Text))
            {
                return $"{inisialNama}, dkk";
            }
            else if (!String.IsNullOrEmpty(this.NamaPenulis2_TextBox.Text) && !String.IsNullOrEmpty(this.NamaPenulis3_TextBox.Text))
            {
                return $"{inisialNama}, {this.NamaPenulis2_TextBox.Text}, dan {this.NamaPenulis3_TextBox.Text}";
            }
            else if (!String.IsNullOrEmpty(this.NamaPenulis2_TextBox.Text))
            {
                return $"{inisialNama}, dan {this.NamaPenulis2_TextBox.Text}";
            }

            return inisialNama;
        }

        static string tentukanPustaka(string namaPustaka, string next)
        {
            if (!String.IsNullOrEmpty(namaPustaka) && next == "titik")
            {
                return $"{namaPustaka.Trim()}. ";
            }
            else if (!String.IsNullOrEmpty(namaPustaka) && next == "titikStop")
            {
                return $"{namaPustaka.Trim()}.";
            }
            else if (!String.IsNullOrEmpty(namaPustaka) && next == "colon")
            {
                return $"{namaPustaka.Trim()}: ";
            }
            else if (!String.IsNullOrEmpty(namaPustaka) && next == "colonBulan")
            {
                return $"({namaPustaka.Trim()}): ";
            }
            else if (!String.IsNullOrEmpty(namaPustaka) && next == "artikel")
            {
                return $"\"{namaPustaka.Trim()}.\" ";
            }
            else if (!String.IsNullOrEmpty(namaPustaka) && next == "spasi")
            {
                return $"{namaPustaka.Trim()} ";
            }
            else
            {
                return "";
            }
        }
        private void Update_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (state == "buku")
            {
                if (String.IsNullOrEmpty(this.NamaPenulis_TextBox.Text) || String.IsNullOrEmpty(this.TahunTerbit_TextBox.Text) ||
                String.IsNullOrEmpty(this.JudulBuku_TextBox.Text) || String.IsNullOrEmpty(this.NamaPenerbit_TextBox.Text) ||
                String.IsNullOrEmpty(this.TempatTerbit_TextBox.Text))
                {
                    PustakaSemua.Foreground = Brushes.Red;
                }
                else
                {
                    PustakaSemua.Foreground = Brushes.Black;
                    ButtonTambah.Visibility = Visibility.Visible;
                }
            }
            
            else if (state == "ebooks")
            {
                Artikel_TextBlock.Visibility = Visibility.Visible;
                Artikel_TextBox.Visibility = Visibility.Visible;

                /*
                namaPenulis
                tahunTerbit
                artikel
                judulBuku
                edisiBuku
                bulanTerbit
                halaman
                tempatTerbit
                namaPenerbit
                platformPenerbit
                link
                */

                if (String.IsNullOrEmpty(this.NamaPenulis_TextBox.Text) || String.IsNullOrEmpty(this.TahunTerbit_TextBox.Text) ||
                String.IsNullOrEmpty(this.JudulBuku_TextBox.Text) || String.IsNullOrEmpty(this.NamaPenerbit_TextBox.Text) ||
                String.IsNullOrEmpty(this.TempatTerbit_TextBox.Text))
                {
                    PustakaSemua.Foreground = Brushes.Red;
                }
                else
                {
                    PustakaSemua.Foreground = Brushes.Black;
                    ButtonTambah.Visibility = Visibility.Visible;
                }
            }

            this.PustakaNamaPenulis.Text  = tentukanPustaka(inisialNama(this.NamaPenulis_TextBox.Text), "titik");
            this.PustakaTahunTerbit.Text  = tentukanPustaka(this.TahunTerbit_TextBox.Text, "titik");
            this.PustakaJudulBuku.Text    = tentukanPustaka(this.JudulBuku_TextBox.Text, "titik");
            this.PustakaEdisiBuku.Text    = tentukanPustaka(this.EdisiBuku_TextBox.Text, "titik");
            this.PustakaTempatTerbit.Text = tentukanPustaka(this.TempatTerbit_TextBox.Text, "colon");
            this.PustakaNamaPenerbit.Text = tentukanPustaka(this.NamaPenerbit_TextBox.Text, "titikStop");
        }

        private void TambahPenulisButton1_Click(object sender, RoutedEventArgs e)
        {
            if (NamaPenulis_TextBox.Visibility == Visibility.Visible && NamaPenulis2_TextBox.Visibility == Visibility.Visible && NamaPenulis3_TextBox.Visibility == Visibility.Visible)
            {
                if (String.IsNullOrEmpty(NamaPenulis_TextBox.Text) || String.IsNullOrEmpty(NamaPenulis2_TextBox.Text) || String.IsNullOrEmpty(NamaPenulis3_TextBox.Text))
                {
                    MessageBox.Show("Lengkapi Nama Penulis Terlebih Dahulu!");
                }
                else
                {
                    NamaPenulis2_TextBlock.Visibility = Visibility.Visible;
                    NamaPenulis2_TextBox.Visibility   = Visibility.Visible;
                    HapusPenulis2_Button.Visibility   = Visibility.Visible;

                    NamaPenulis3_TextBlock.Visibility = Visibility.Visible;
                    NamaPenulis3_TextBox.Visibility   = Visibility.Visible;
                    HapusPenulis3_Button.Visibility   = Visibility.Visible;

                    NamaPenulis4_TextBlock.Visibility = Visibility.Visible;
                    NamaPenulis4_TextBox.Visibility   = Visibility.Visible;
                    HapusPenulis4_Button.Visibility   = Visibility.Visible;

                    TambahPenulisButton1.Visibility   = Visibility.Collapsed;
                }
            }
            else if (NamaPenulis2_TextBox.Visibility == Visibility.Visible)
            {
                if (String.IsNullOrEmpty(NamaPenulis_TextBox.Text) || String.IsNullOrEmpty(NamaPenulis2_TextBox.Text))
                {
                    MessageBox.Show("Lengkapi Nama Penulis Terlebih Dahulu!");
                }
                else
                {
                    NamaPenulis2_TextBlock.Visibility = Visibility.Visible;
                    NamaPenulis2_TextBox.Visibility   = Visibility.Visible;
                    HapusPenulis2_Button.Visibility   = Visibility.Visible;

                    NamaPenulis3_TextBlock.Visibility = Visibility.Visible;
                    NamaPenulis3_TextBox.Visibility   = Visibility.Visible;
                    HapusPenulis3_Button.Visibility   = Visibility.Visible;

                    NamaPenulis4_TextBlock.Visibility = Visibility.Collapsed;
                    NamaPenulis4_TextBox.Visibility   = Visibility.Collapsed;
                    HapusPenulis4_Button.Visibility   = Visibility.Collapsed;
                }
            }
            else
            {
                if (String.IsNullOrEmpty(NamaPenulis_TextBox.Text))
                {
                    MessageBox.Show("Lengkapi Nama Penulis Terlebih Dahulu!");
                }
                else
                {
                    NamaPenulis2_TextBlock.Visibility = Visibility.Visible;
                    NamaPenulis2_TextBox.Visibility   = Visibility.Visible;
                    HapusPenulis2_Button.Visibility   = Visibility.Visible;

                    NamaPenulis3_TextBlock.Visibility = Visibility.Collapsed;
                    NamaPenulis3_TextBox.Visibility   = Visibility.Collapsed;
                    HapusPenulis3_Button.Visibility   = Visibility.Collapsed;

                    NamaPenulis4_TextBlock.Visibility = Visibility.Collapsed;
                    NamaPenulis4_TextBox.Visibility   = Visibility.Collapsed;
                    HapusPenulis4_Button.Visibility   = Visibility.Collapsed;

                }
            }
        }

        private void HapusPenulis2_Button_Click(object sender, RoutedEventArgs e)
        {
            if (NamaPenulis3_TextBlock.Visibility == Visibility.Visible && NamaPenulis4_TextBlock.Visibility == Visibility.Visible)
            {
                NamaPenulis4_TextBlock.Visibility = Visibility.Collapsed;
                NamaPenulis4_TextBox.Visibility   = Visibility.Collapsed;
                HapusPenulis4_Button.Visibility   = Visibility.Collapsed;

                NamaPenulis2_TextBox.Text = NamaPenulis3_TextBox.Text;
                NamaPenulis3_TextBox.Text = NamaPenulis4_TextBox.Text;
                NamaPenulis4_TextBox.Text = null;
            }
            else if (NamaPenulis3_TextBlock.Visibility == Visibility.Visible && NamaPenulis4_TextBlock.Visibility == Visibility.Collapsed)
            {
                NamaPenulis3_TextBlock.Visibility = Visibility.Collapsed;
                NamaPenulis3_TextBox.Visibility   = Visibility.Collapsed;
                HapusPenulis3_Button.Visibility   = Visibility.Collapsed;

                NamaPenulis2_TextBox.Text = NamaPenulis3_TextBox.Text;
                NamaPenulis3_TextBox.Text = null;
                NamaPenulis4_TextBox.Text = null;
            }
            else
            {
                NamaPenulis2_TextBlock.Visibility = Visibility.Collapsed;
                NamaPenulis2_TextBox.Visibility = Visibility.Collapsed;
                HapusPenulis2_Button.Visibility = Visibility.Collapsed;
                
                NamaPenulis2_TextBox.Text = null;
            }
            TambahPenulisButton1.Visibility = Visibility.Visible;
        }

        private void HapusPenulis3_Button_Click(object sender, RoutedEventArgs e)
        {
            if (NamaPenulis4_TextBlock.Visibility == Visibility.Visible)
            {
                NamaPenulis4_TextBlock.Visibility = Visibility.Collapsed;
                NamaPenulis4_TextBox.Visibility   = Visibility.Collapsed;
                HapusPenulis4_Button.Visibility   = Visibility.Collapsed;

                NamaPenulis3_TextBox.Text = NamaPenulis4_TextBox.Text;
                NamaPenulis4_TextBox.Text = null;
            }
            else
            {
                NamaPenulis3_TextBlock.Visibility = Visibility.Collapsed;
                NamaPenulis3_TextBox.Visibility   = Visibility.Collapsed;
                HapusPenulis3_Button.Visibility   = Visibility.Collapsed;
                
                NamaPenulis3_TextBox.Text = null;
            }
            TambahPenulisButton1.Visibility = Visibility.Visible;
        }

        private void HapusPenulis4_Button_Click(object sender, RoutedEventArgs e)
        {
            NamaPenulis4_TextBlock.Visibility = Visibility.Collapsed;
            NamaPenulis4_TextBox.Visibility   = Visibility.Collapsed;
            HapusPenulis4_Button.Visibility   = Visibility.Collapsed;

            NamaPenulis4_TextBox.Text = null;
            TambahPenulisButton1.Visibility = Visibility.Visible;
        }

        class newPustaka
        {
            public string namaPenulis { get; set; }
            public string tahunTerbit { get; set; }
            public string artikel { get; set; }
            public string judulBuku { get; set; }
            public string edisiBuku { get; set; }
            public string bulanTerbit { get; set; }
            public string halaman { get; set; }
            public string tempatTerbit { get; set; }
            public string namaPenerbit { get; set; }

            public string platformPenerbit { get; set; }
            public string link { get; set; }
        }

        List<newPustaka> pustaka2 = new List<newPustaka>();
        List<String> pustaka = new List<String>();
        string state = "buku";

        private void ButtonTambah_Click(object sender, RoutedEventArgs e)
        {
            pustaka2.Add(new newPustaka
            {
                namaPenulis      = inisialNama(this.NamaPenulis_TextBox.Text),
                tahunTerbit      = this.TahunTerbit_TextBox.Text,
                artikel          = this.Artikel_TextBox.Text,
                judulBuku        = this.JudulBuku_TextBox.Text,
                edisiBuku        = this.EdisiBuku_TextBox.Text,
                bulanTerbit      = this.BulanTerbit_TextBox.Text,
                halaman          = this.Halaman_TextBox.Text,
                tempatTerbit     = this.TempatTerbit_TextBox.Text,
                namaPenerbit     = this.NamaPenerbit_TextBox.Text,
                platformPenerbit = this.PlatformPenerbit_TextBox.Text,
                link             = this.Link_TextBox.Text
            });

            HapusDaftarPustaka.Visibility = Visibility.Visible;
            pustaka.Add(this.PustakaSemua.Text);
            pustaka2 = pustaka2.OrderBy(x => x.namaPenulis).ToList();
            DaftarPustaka.Document.Blocks.Clear();
            ButtonHapusSatu.Children.Clear();

            Run[] namaPenulisRun     = new Run[pustaka.Count];
            Run[] tahunTerbitRun     = new Run[pustaka.Count];
            Run[] artikelRun         = new Run[pustaka.Count];
            Run[] judulBukuRun       = new Run[pustaka.Count];
            Run[] edisiBukuRun       = new Run[pustaka.Count];
            Run[] bulanTerbitRun     = new Run[pustaka.Count];
            Run[] halamanRun         = new Run[pustaka.Count];
            Run[] tempatTerbitRun    = new Run[pustaka.Count];
            Run[] namaPenerbitRun    = new Run[pustaka.Count];
            Run[] platformPenerbitRun  = new Run[pustaka.Count]; 
            Run[] linkRun            = new Run[pustaka.Count];

            Paragraph[] arrayParagraph = new Paragraph[pustaka.Count];
            Button[] arrayButton       = new Button[pustaka.Count];
            
            for (int i = 0; i < pustaka.Count; i++)
            {
             
                var namaPenulisRunSet      = new Run();
                var tahunTerbitRunSet      = new Run();
                var artikelRunSet          = new Run();     
                var judulBukuRunSet        = new Run();
                var edisiBukuRunSet        = new Run();
                var bulanTerbitRunSet      = new Run();
                var halamanRunSet          = new Run();
                var tempatTerbitRunSet     = new Run();
                var namaPenerbitRunSet     = new Run();
                var platformPenerbitRunSet = new Run();
                var linkRunSet             = new Run();

                namaPenulisRun[i]      = namaPenulisRunSet;
                tahunTerbitRun[i]      = tahunTerbitRunSet;
                artikelRun[i]          = artikelRunSet;     
                judulBukuRun[i]        = judulBukuRunSet;   
                edisiBukuRun[i]        = edisiBukuRunSet;  
                bulanTerbitRun[i]      = bulanTerbitRunSet; 
                halamanRun[i]          = halamanRunSet;     
                tempatTerbitRun[i]     = tempatTerbitRunSet;
                platformPenerbitRun[i] = platformPenerbitRunSet;
                namaPenerbitRun[i]     = namaPenerbitRunSet;

                linkRun[i]          = linkRunSet;        

                namaPenulisRunSet.Text      = tentukanPustaka(pustaka2[i].namaPenulis, "titik");
                tahunTerbitRunSet.Text      = tentukanPustaka(pustaka2[i].tahunTerbit, "titik");
                artikelRunSet.Text          = pustaka2[i].artikel;
                judulBukuRunSet.Text        = tentukanPustaka(pustaka2[i].judulBuku, "titik");
                edisiBukuRunSet.Text        = tentukanPustaka(pustaka2[i].edisiBuku, "titik");
                bulanTerbitRunSet.Text      = pustaka2[i].bulanTerbit;
                halamanRunSet.Text          = pustaka2[i].halaman;
                tempatTerbitRunSet.Text     = tentukanPustaka(pustaka2[i].tempatTerbit, "colon");
                namaPenerbitRunSet.Text     = pustaka2[i].namaPenerbit;
                platformPenerbitRunSet.Text = tentukanPustaka(pustaka2[i].platformPenerbit, "titikStop");
                linkRunSet.Text             = tentukanPustaka(pustaka2[i].link, "titikStop");

                judulBukuRunSet.FontStyle = FontStyles.Italic;

                var tes = new Paragraph();
                arrayParagraph[i] = tes;
                tes.LineHeight = 1;

                tes.Inlines.Add(namaPenulisRun[i]);
                tes.Inlines.Add(tahunTerbitRun[i]);
                tes.Inlines.Add(artikelRun[i]);
                tes.Inlines.Add(judulBukuRun[i]);
                tes.Inlines.Add(edisiBukuRun[i]);
                tes.Inlines.Add(bulanTerbitRun[i]);
                tes.Inlines.Add(halamanRun[i]);
                tes.Inlines.Add(tempatTerbitRun[i]);
                tes.Inlines.Add(namaPenerbitRun[i]);
                tes.Inlines.Add(platformPenerbitRun[i]);
                tes.Inlines.Add(linkRun[i]);


                DaftarPustaka.Document.Blocks.Add(arrayParagraph[i]);

                Button newButton = new Button();
                var txt2 = new Button();
                arrayButton[i] = txt2;
                txt2.Content = "Hapus";
                txt2.Name = $"Btn{i}";
                ButtonHapusSatu.Children.Add(arrayButton[i]);
                txt2.AddHandler(Button.ClickEvent, new RoutedEventHandler(BtnSatu_Click)); 
            }
        }

        void BtnSatu_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            string index = button.Name;
            int remove = Convert.ToInt32(index.Substring(index.Length - 1));

            pustaka.RemoveAt(remove);
            DaftarPustaka.Document.Blocks.Clear();
            ButtonHapusSatu.Children.Clear();

            pustaka2 = pustaka2.OrderBy(x => x.namaPenulis).ToList();
            DaftarPustaka.Document.Blocks.Clear();
            ButtonHapusSatu.Children.Clear();

            Run[] namaPenulisRun = new Run[pustaka.Count];
            Run[] tahunTerbitRun = new Run[pustaka.Count];
            Run[] artikelRun = new Run[pustaka.Count];
            Run[] judulBukuRun = new Run[pustaka.Count];
            Run[] edisiBukuRun = new Run[pustaka.Count];
            Run[] bulanTerbitRun = new Run[pustaka.Count];
            Run[] halamanRun = new Run[pustaka.Count];
            Run[] tempatTerbitRun = new Run[pustaka.Count];
            Run[] namaPenerbitRun = new Run[pustaka.Count];
            Run[] platformPenerbitRun = new Run[pustaka.Count];
            Run[] linkRun = new Run[pustaka.Count];

            Paragraph[] arrayParagraph = new Paragraph[pustaka.Count];
            Button[] arrayButton = new Button[pustaka.Count];

            for (int i = 0; i < pustaka.Count; i++)
            {

                var namaPenulisRunSet = new Run();
                var tahunTerbitRunSet = new Run();
                var artikelRunSet = new Run();
                var judulBukuRunSet = new Run();
                var edisiBukuRunSet = new Run();
                var bulanTerbitRunSet = new Run();
                var halamanRunSet = new Run();
                var tempatTerbitRunSet = new Run();
                var namaPenerbitRunSet = new Run();
                var platformPenerbitRunSet = new Run();
                var linkRunSet = new Run();

                namaPenulisRun[i] = namaPenulisRunSet;
                tahunTerbitRun[i] = tahunTerbitRunSet;
                artikelRun[i] = artikelRunSet;
                judulBukuRun[i] = judulBukuRunSet;
                edisiBukuRun[i] = edisiBukuRunSet;
                bulanTerbitRun[i] = bulanTerbitRunSet;
                halamanRun[i] = halamanRunSet;
                tempatTerbitRun[i] = tempatTerbitRunSet;
                platformPenerbitRun[i] = platformPenerbitRunSet;
                namaPenerbitRun[i] = namaPenerbitRunSet;

                linkRun[i] = linkRunSet;

                namaPenulisRunSet.Text = tentukanPustaka(pustaka2[i].namaPenulis, "titik");
                tahunTerbitRunSet.Text = tentukanPustaka(pustaka2[i].tahunTerbit, "titik");
                artikelRunSet.Text = pustaka2[i].artikel;
                judulBukuRunSet.Text = tentukanPustaka(pustaka2[i].judulBuku, "titik");
                edisiBukuRunSet.Text = tentukanPustaka(pustaka2[i].edisiBuku, "titik");
                bulanTerbitRunSet.Text = pustaka2[i].bulanTerbit;
                halamanRunSet.Text = pustaka2[i].halaman;
                tempatTerbitRunSet.Text = tentukanPustaka(pustaka2[i].tempatTerbit, "colon");
                namaPenerbitRunSet.Text = pustaka2[i].namaPenerbit;
                platformPenerbitRunSet.Text = tentukanPustaka(pustaka2[i].platformPenerbit, "titikStop");
                linkRunSet.Text = tentukanPustaka(pustaka2[i].link, "titikStop");

                judulBukuRunSet.FontStyle = FontStyles.Italic;

                var tes = new Paragraph();
                arrayParagraph[i] = tes;
                tes.LineHeight = 1;

                tes.Inlines.Add(namaPenulisRun[i]);
                tes.Inlines.Add(tahunTerbitRun[i]);
                tes.Inlines.Add(artikelRun[i]);
                tes.Inlines.Add(judulBukuRun[i]);
                tes.Inlines.Add(edisiBukuRun[i]);
                tes.Inlines.Add(bulanTerbitRun[i]);
                tes.Inlines.Add(halamanRun[i]);
                tes.Inlines.Add(tempatTerbitRun[i]);
                tes.Inlines.Add(namaPenerbitRun[i]);
                tes.Inlines.Add(platformPenerbitRun[i]);
                tes.Inlines.Add(linkRun[i]);

                DaftarPustaka.Document.Blocks.Add(arrayParagraph[i]);

                Button newButton = new Button();
                var txt2 = new Button();
                arrayButton[i] = txt2;
                txt2.Content = "Hapus";
                txt2.Name = $"Btn{i}";
                ButtonHapusSatu.Children.Add(arrayButton[i]);
                txt2.AddHandler(Button.ClickEvent, new RoutedEventHandler(BtnSatu_Click));
            }
        }   
        private void HapusDaftarPustaka_Click(object sender, RoutedEventArgs e)
        {
            pustaka.Clear();
            DaftarPustaka.Document.Blocks.Clear();
            ButtonHapusSatu.Children.Clear();
            HapusDaftarPustaka.Visibility = Visibility.Collapsed;
        }
    }
}