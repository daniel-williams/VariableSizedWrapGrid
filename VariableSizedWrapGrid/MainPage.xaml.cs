using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace VariableSizedWrapGrid
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private static Random rnd = new Random();
        private List<Canvas> boxes = new List<Canvas>();
        private int baseWidth = 50;
        private int baseHeight = 40;

        public MainPage()
        {
            this.InitializeComponent();

            this.myGrid.ItemWidth = baseWidth;
            this.myGrid.ItemHeight = baseHeight;

            for (var i = 0; i < 100; i++)
            {
                int colSpan = rnd.Next(1, 3);
                int rowSpan = rnd.Next(1, 3);

                string name = (i + 1).ToString();
                Canvas canvas = new MyCanvas(name, baseWidth * colSpan, baseHeight * rowSpan);

                canvas.SetValue(Windows.UI.Xaml.Controls.VariableSizedWrapGrid.ColumnSpanProperty, colSpan);
                canvas.SetValue(Windows.UI.Xaml.Controls.VariableSizedWrapGrid.RowSpanProperty, rowSpan);
                this.myGrid.Children.Add(canvas);
            }
        }
    }

    public partial class MyCanvas : Canvas
    {
        private static Random rnd = new Random();
        public MyCanvas(string name, int width, int height)
        {
            this.Width = width - 10;
            this.Height = height - 10;
            this.Margin = new Thickness(5);

            var bytes = GetRandomColor();

            var color = new Windows.UI.Color
            {
                R = bytes[0],
                G = bytes[1],
                B = bytes[2],
                A = bytes[3]
            };

            this.Background = new SolidColorBrush(color);
            this.Children.Add(new TextBlock { Text = name });
        }

        public byte[] GetRandomColor()
        {
            byte[] bytes = new byte[4];

            rnd.NextBytes(bytes);

            System.Diagnostics.Debug.WriteLine("random color: " + bytes[0] + ", " + bytes[1] + ", " + bytes[2] + ", " + bytes[3]);
            return bytes;
        }
    }
}
