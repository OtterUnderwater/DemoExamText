using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DemoExamText.Models;
using DemoExamText.ViewModels;

namespace DemoExamText.Views;

public partial class NewProductView : UserControl
{
    public NewProductView()
    {
        InitializeComponent();
		DataContext = new NewProductViewModel();
    }

	public NewProductView(Tour tour)
	{
		InitializeComponent();
		DataContext = new NewProductViewModel(tour);
	}
}