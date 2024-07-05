using Path = System.IO.Path;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace HowTo_FileExplorer_AddressBar;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public string Address { get; set; } = @"C:\Users\Standard\source\repos\AsyncApp";
    public MainWindow()
    {
        InitializeComponent();
    }

    private void addressPath_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        addressPath.Visibility = Visibility.Hidden;
        addressPanel.Visibility = Visibility.Visible;
        
        var pathElements = GetAllRoutePaths(addressPath.Text);
        foreach (var item in pathElements)
        {
            ListBoxEx cmbo = CreateDirListBox(item, addressPath.ActualHeight);
            cmbo.cmbo.SelectedIndex = 0;
            addressPanel.Children.Add(cmbo);
        }
        var breakOut = new TextBlock 
        { 
            Background = Brushes.Transparent,
            Foreground = Brushes.Ivory,
            Text = "x",
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
        };
        breakOut.MouseLeftButtonUp += BreakOut_MouseLeftButtonUp;
        addressPanel.Children.Add(breakOut);
        addressPanel.UpdateLayout();
    }

    private void BreakOut_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        string path = addressPath.Text;
        addressPath.Text = "";
        addressPanel.Visibility= Visibility.Hidden;
        addressPanel.Children.Clear();
        addressPath.Visibility = Visibility.Visible;
        addressPath.Text = path;
        addressPath.Focus();
    }

    List<string>? GetAllRoutePaths(string routePath)
    {
        var routePathElements = routePath.Split(Path.DirectorySeparatorChar);
        if (routePathElements.Length < 1)
        {
            return null;
        }
        List<string> routePaths = new();
        for(int i = 0; i < routePathElements.Length; i++)
        {
            string path = GetPathFromElements(routePathElements, i);
            routePaths.Add(path);
        }
        return routePaths;
    }

    private string GetPathFromElements(string[] routePathElements, int stopIndex)
    {
        string path = "";
        for(int i = 0; i <= stopIndex; i++)
        {
            path += $@"{routePathElements[i]}{Path.DirectorySeparatorChar}";
        }
        return path.TrimEnd(Path.DirectorySeparatorChar);
    }

    ListBoxEx CreateDirListBox(string dirPath, double actualHeight)
    {
        ListBoxEx listBox = new ListBoxEx(actualHeight);
        listBox.cmbo.Items.Add(Path.GetFileName(dirPath));
        var subDirs = Directory.EnumerateDirectories(dirPath, "*", new EnumerationOptions { IgnoreInaccessible = true }).ToList();
        foreach ( var subDir in subDirs )
        {
            listBox.cmbo.Items.Add(Path.GetFileName( subDir ));
        }
        
        return listBox;
    }
}