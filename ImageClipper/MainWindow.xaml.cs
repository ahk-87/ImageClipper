using System;
using System.Collections.Generic;
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

using Tesseract;
using System.Diagnostics;

namespace ImageClipper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, StartCB);
        }

        private void clip()
        {
            //Stopwatch stw = new Stopwatch();
            //stw.Start();
            //Pix p = Pix.LoadFromFile("image.png");
            //TesseractEngine engine = new TesseractEngine(@"C:\Program Files (x86)\Winsoft\OCR.NET\DLL", "eng");
            //Tesseract.Page result = engine.Process(p,new Tesseract.Rect(35, 210, 325, 290));
            //s = result.GetText();
            //engine.Dispose();
            //p.Dispose();
            //stw.Stop();

            int count = Directory.EnumerateFiles("Cropped Images").Count();
            int repetition = int.Parse(Rep.Text);

            List<string> images = Directory.EnumerateFiles("images").Reverse().ToList();
            int i = int.Parse(Beg.Text);

            BitmapImage raw;
            CroppedBitmap cardType;
            CroppedBitmap cardCode;
            CroppedBitmap serial;

            foreach (string image in images)
            {
                int yCardType = 498;// 136;
                int yCardCode = 626;// 264;
                int ySerial = 584;// 222;

                int yDisplacement = 181;

                raw = new BitmapImage(new Uri(image, UriKind.Relative));

                for (int j = 0; j < 3; j++)
                {
                    cardType = new CroppedBitmap(raw, new Int32Rect(8, yCardType, 154, 26));
                    cardCode = new CroppedBitmap(raw, new Int32Rect(134, yCardCode, 212, 24));
                    serial = new CroppedBitmap(raw, new Int32Rect(49, ySerial, 183, 24));
                    CardInfo ci = new CardInfo();
                    BitmapSource source = ci.GetImage(i, cardType, cardCode, serial);

                    count++;


                    FileStream stream = new FileStream("Cropped Images\\image" + count + ".png", FileMode.Create);


                    BitmapFrame fr = BitmapFrame.Create(source);

                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(fr);
                    encoder.Save(stream);
                    stream.Dispose();

                    if (i == repetition)
                        i = 1;
                    else
                        i++;

                    yCardCode -= yDisplacement;
                    yCardType -= yDisplacement;
                    ySerial -= yDisplacement;
                }
            }
        }

        private void GoButton_Click(object sender, RoutedEventArgs e)
        {
            clip();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {

            if (!Directory.Exists("Cropped Images"))
                Directory.CreateDirectory("Cropped Images");

            //if (!File.Exists("image.png"))
            //    GoButton.IsEnabled = false;

        }
    }
}
