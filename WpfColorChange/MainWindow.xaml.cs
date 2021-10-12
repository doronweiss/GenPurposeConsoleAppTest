using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using PixelFormat = System.Windows.Media.PixelFormat;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace WpfColorChange {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window {
    public MainWindow() {
      InitializeComponent();
    }

    private void OnLoadImage(object sender, RoutedEventArgs e) {
      BitmapImage bitmap = new BitmapImage();
      bitmap.BeginInit();
      bitmap.UriSource = new Uri(@"c:\temp\blacksource.png");
      bitmap.EndInit();
      img.Source = bitmap;
    }

  }
}
