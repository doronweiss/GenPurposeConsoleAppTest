using Avalonia.Controls;
using Avalonia.Interactivity;

namespace FirstTest.Views;

public partial class MainWindow : Window {
  private int idx = 0;
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OnClick(object? sender, RoutedEventArgs e) {
      outputTBLK.Text = $"Clicked {++idx} times ....";
    }
}
