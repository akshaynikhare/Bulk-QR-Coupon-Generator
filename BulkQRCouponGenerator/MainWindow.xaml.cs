using Microsoft.Win32;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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

namespace BulkImage_app
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

        public List<CsvTemplate> CodeList;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            of.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
            if (of.ShowDialog() == true)
            {
                PreviewBox.Source = new BitmapImage(new Uri(of.FileName));
                Base_image.Text = of.FileName;
            }
        }
        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            of.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
            if (of.ShowDialog() == true)
            {
                PreviewBox.Source = new BitmapImage(new Uri(of.FileName));
                Logo_on_QR.Text = of.FileName;
            }
        }

        public class CsvTemplate

        {
           public String t1;

            public CsvTemplate(string r1)
            {
                t1 = r1;
            }
            public override string ToString()
            {
                return t1.ToString();
            }
            public static CsvTemplate FromCsv(string csvLine)
            {
               string[] values = csvLine.Split(',');
                CsvTemplate dailyValues = new CsvTemplate(values[0]);
                return dailyValues;
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            of.Filter = "CSV|*.csv";
            if (of.ShowDialog() == true)
            {
                Import_CSV.Text = of.FileName;
                CodeList = File.ReadAllLines(of.FileName)

                                         .Skip(1)

                                         .Select(v => CsvTemplate.FromCsv(v))

                                         .ToList();

                Listofcodes.ItemsSource = CodeList;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog()
            {
                SelectedPath = AppDomain.CurrentDomain.BaseDirectory
            };

            if (dialog.ShowDialog() == true)
            {
                Output_Dir.Text = dialog.SelectedPath;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            

            if (!File.Exists(Base_image.Text))
            {
                MessageBox.Show("Please Check Base Image.");
                return ;
            }
            if (!File.Exists(Logo_on_QR.Text))
            {
                MessageBox.Show("Please Check Logo on QR.");
                return;
            }
            if (!File.Exists(Import_CSV.Text))
            {
                MessageBox.Show("Please Check Import CSV.");
                return;
            }

            if (Listofcodes.Items.Count <= 0)
            {
                MessageBox.Show("please add a code in the {Code} list");
                return;
            }
            /* if (!Directory.Exists(Output_Dir.Text))
            {
                MessageBox.Show("Please Check Import CSV.");
                return;
            }*/



            if (Convert.ToInt16(QR_Pos_X.Text)<1 
                && Convert.ToInt16(QR_Pos_Y.Text) < 1
                 && Convert.ToInt16(QR_Size_X.Text) < 1
                  && Convert.ToInt16(QR_Size_X.Text) < 1
                   && QR_Link.Text.Length < 0
                    && QR_Colour.Text.Length < 0
                )
            {
                MessageBox.Show("Please Check QR Code inuput data.");
                return;
            }

            if (Convert.ToInt16(TXT_Pos_X.Text) < 1
                && Convert.ToInt16(TXT_Pos_Y.Text) < 1
                   && Convert.ToInt16(TXT_ForntSize.Text) < 1
                    && TXT_Overlay.Text.Length < 0
                     && TXT_Colour.Text.Length < 0
                )
                {
                    MessageBox.Show("Please Check QR Code Overlay text.");
                    return;
                }

            PreviewBox.Source = ConvertBitmapToBitmapImage(GenrateImage(Listofcodes.Items[0].ToString())); 
        }

        public BitmapImage ConvertBitmapToBitmapImage(Bitmap src)
        {
            MemoryStream ms = new MemoryStream();
            ((System.Drawing.Bitmap)src).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            string InputPath = AppDomain.CurrentDomain.BaseDirectory+"\\Input";
            if (Directory.Exists(InputPath))
            {
                if (File.Exists(InputPath + "\\Base_image.jpg"))
                {
                    Base_image.Text = InputPath + "\\Base_image.jpg";
                    PreviewBox.Source = new BitmapImage(new Uri(InputPath + "\\Base_image.jpg"));
                }
                if (File.Exists(InputPath + "\\Logo_on_QR.png"))
                {
                    Logo_on_QR.Text = InputPath + "\\Logo_on_QR.png";
                }
                if (File.Exists(InputPath + "\\Import_Code.csv"))
                {
                    Import_CSV.Text = InputPath + "\\Import_Code.csv";
                    CodeList = File.ReadAllLines(InputPath + "\\Import_Code.csv")
                                             .Skip(1)
                                             .Select(v => CsvTemplate.FromCsv(v))
                                             .ToList();

                    Listofcodes.ItemsSource = CodeList;
                }
            }

            string OutputPath = AppDomain.CurrentDomain.BaseDirectory + "\\Output";
            if (Directory.Exists(OutputPath))
            {
                Output_Dir.Text = OutputPath;
            }
            TXT_Font.ItemsSource = System.Drawing.FontFamily.Families.ToList();

            int counter = 0;
           foreach(System.Drawing.FontFamily item in TXT_Font.Items)
            {
                if (item.Name == "Calibri")
                {
                    TXT_Font.SelectedIndex= counter;
                }
                counter++;
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(Base_image.Text))
            {
                MessageBox.Show("Please Check Base Image.");
                return;
            }
            if (!File.Exists(Logo_on_QR.Text))
            {
                MessageBox.Show("Please Check Logo on QR.");
                return;
            }
            if (!File.Exists(Import_CSV.Text))
            {
                MessageBox.Show("Please Check Import CSV.");
                return;
            }
             if (!Directory.Exists(Output_Dir.Text))
            {
                MessageBox.Show("Please Check Output Dir.");
                return;
            }

            if (Listofcodes.Items.Count <= 0)
            {
                MessageBox.Show("please add a code in the {Code} list");
                return;
            }

            if (Convert.ToInt16(QR_Pos_X.Text) < 1
                && Convert.ToInt16(QR_Pos_Y.Text) < 1
                 && Convert.ToInt16(QR_Size_X.Text) < 1
                  && Convert.ToInt16(QR_Size_X.Text) < 1
                   && QR_Link.Text.Length < 0
                    && QR_Colour.Text.Length < 0
                )
            {
                MessageBox.Show("Please Check QR Code inuput data.");
                return;
            }

            if (Convert.ToInt16(TXT_Pos_X.Text) < 1
                && Convert.ToInt16(TXT_Pos_Y.Text) < 1
                   && Convert.ToInt16(TXT_ForntSize.Text) < 1
                    && TXT_Overlay.Text.Length < 0
                     && TXT_Colour.Text.Length < 0
                )
            {
                MessageBox.Show("Please Check QR Code Overlay text.");
                return;
            }


            int counter = 0;
            foreach (CsvTemplate item in Listofcodes.Items)
            {
                Bitmap bmp = GenrateImage(item.t1);
                PreviewBox.Source = ConvertBitmapToBitmapImage(bmp);
                bmp.Save(Output_Dir.Text+"\\"+counter+"_"+ item.t1+".png", ImageFormat.Png);
                counter++;
            }

            MessageBox.Show("Succesfully Genrated " + counter + " number of images in Output Dir.");


        }


        public Bitmap GenrateImage(string code)
        {

            String QrDaynamicText = QR_Link.Text.Replace("{code}", code);
            String TXTDaynamicText = TXT_Overlay.Text.Replace("{code}", code);

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(QrDaynamicText, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);

            Bitmap qrCodeImage = qrCode.GetGraphic(100, System.Drawing.ColorTranslator.FromHtml(QR_Colour.Text), System.Drawing.ColorTranslator.FromHtml("#ffffff"), false);

            Bitmap resizedLogoOnQR = new Bitmap(new Bitmap(Logo_on_QR.Text), new System.Drawing.Size(qrCodeImage.Width / 3, qrCodeImage.Height / 3));

            int deltaHeigth = qrCodeImage.Height - resizedLogoOnQR.Height;
            int deltaWidth = qrCodeImage.Width - resizedLogoOnQR.Width;

            Graphics g = Graphics.FromImage(qrCodeImage);
            g.DrawImage(resizedLogoOnQR, new System.Drawing.Point(deltaWidth / 2, deltaHeigth / 2));


            Bitmap BaseImage = new Bitmap(Base_image.Text);
            Bitmap resizedqrCodeImage = new Bitmap(qrCodeImage, new System.Drawing.Size(Int16.Parse(QR_Size_X.Text), Int16.Parse(QR_Size_Y.Text)));

            g = Graphics.FromImage(BaseImage);
            g.DrawImage(resizedqrCodeImage, new System.Drawing.Point(Int16.Parse(QR_Pos_X.Text), Int16.Parse(QR_Pos_Y.Text)));


            System.Drawing.FontFamily selFam = (System.Drawing.FontFamily)TXT_Font.SelectedItem;
            g.DrawString(TXTDaynamicText, new Font(selFam.Name.ToString(), Convert.ToInt16(TXT_ForntSize.Text), System.Drawing.FontStyle.Bold), new SolidBrush(System.Drawing.ColorTranslator.FromHtml(TXT_Colour.Text)), Convert.ToInt16(TXT_Pos_X.Text), Convert.ToInt16(TXT_Pos_Y.Text));

            return BaseImage;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
           

            if (AddItemText.Text.Length > 0 )
            {
                if (Listofcodes.Items.Count > 0)
                {
                    Listofcodes.ItemsSource = null;
                    Listofcodes.Items.Clear();
                }
                CodeList.Add(new CsvTemplate(AddItemText.Text));
                Listofcodes.ItemsSource = CodeList;
                AddItemText.Text = "";

            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Listofcodes.ItemsSource = null;
            Listofcodes.Items.Clear();
            CodeList.Clear();
        }
    }
}
