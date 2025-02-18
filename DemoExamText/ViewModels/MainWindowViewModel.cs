using Avalonia.Controls;
using DemoExamText.Models;
using DemoExamText.Views;
using ReactiveUI;

namespace DemoExamText.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
	{
		public static MainWindowViewModel Instance;
		public static PostgresContext Context = new PostgresContext();

		public MainWindowViewModel()
		{
			Instance = this;
			CurrentPage = new ProductView();
		}

		UserControl _currentPage;
		public UserControl CurrentPage { get => _currentPage; set => this.RaiseAndSetIfChanged(ref _currentPage, value); }
	}
}
