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

namespace ImageClipper
{
    /// <summary>
    /// Interaction logic for CardInfo.xaml
    /// </summary>
    public partial class CardInfo : UserControl
    {
        public CardInfo()
        {
            InitializeComponent();
        }

        public BitmapSource GetImage(int cardNumber,BitmapSource cardType, BitmapSource secretImage, BitmapSource serialImage)
        {
            this.NumberTB.Text = cardNumber.ToString();
            this.CardType.Source = cardType;
            this.SecretImage.Source = secretImage;
            this.SerialImage.Source = serialImage;

            this.Background = Brushes.White;
            this.Measure(new Size(320, 300));
            this.Arrange(new Rect(0, 0, 320, 300));
            RenderTargetBitmap finalImage = new RenderTargetBitmap(320, 300, 96, 96, PixelFormats.Default);
           


            finalImage.Render(this);
            return finalImage;
        }
    }
}
