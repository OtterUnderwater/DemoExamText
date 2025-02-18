using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DemoExamText.ViewModels;

namespace DemoExamText.Views;

public partial class ProductView : UserControl
{
    public ProductView()
	{
		DataContext = new ProductViewModel();
		InitializeComponent();
	}
	
}