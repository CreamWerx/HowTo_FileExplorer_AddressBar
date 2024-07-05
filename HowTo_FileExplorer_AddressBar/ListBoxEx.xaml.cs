using System.Windows.Controls;

namespace HowTo_FileExplorer_AddressBar;

public partial class ListBoxEx : UserControl
{
    public ListBoxEx()
    {
        InitializeComponent();
        cmbo.Height = 30;
    }

    public ListBoxEx(double height)
    {
        InitializeComponent();
        cmbo.Height = height;
    }

    private void cmbo_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }
}
