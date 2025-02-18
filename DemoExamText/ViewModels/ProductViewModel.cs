using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Threading.Tasks;
using DemoExamText.Models;
using DemoExamText.Views;
using Microsoft.EntityFrameworkCore;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using ReactiveUI;

namespace DemoExamText.ViewModels
{
	public class ProductViewModel : ViewModelBase
	{
		List<Tour> tours = new List<Tour>();
		List<Tour> listTours = new List<Tour>();
		List<Tour> filterList = new List<Tour>();
		List<Type> typeList = new List<Type>();
		Type selectedType;
		int sortIndex = 0;
		string search = "";
		int _countOnPage = 20;
		int _currentPage = 0;

		public List<Tour> Tours { get => tours; set =>  this.RaiseAndSetIfChanged(ref tours, value); }
		public List<Tour> ListTours { get => listTours; set => this.RaiseAndSetIfChanged(ref listTours, value); }
		public List<Tour> FilterList { get => filterList; set => this.RaiseAndSetIfChanged(ref filterList, value); }
		public List<Type> TypeList { get => typeList; set => this.RaiseAndSetIfChanged(ref typeList, value); }
		public List<string> SortList => ["без сортировки", "по возрастанию цены", "по убыванию цены"];	
		public int SortIndex { get => sortIndex; set { sortIndex = value; Filter(); } }
		public string Search { get => search; set { search = value; Filter(); } }
		public Type SelectedType { get => selectedType; set { this.RaiseAndSetIfChanged(ref selectedType, value); Filter(); } }
		
		public ProductViewModel()
		{
			Update();
		}
		private void Update()
		{
			ListTours = MainWindowViewModel.Context.Tours.Include(x => x.ToursTypes).ToList();
			TypeList = MainWindowViewModel.Context.Types.ToList();
			TypeList.Insert(0, new Type { Id = 0, Type1 = "¬се типы" });
			SelectedType = TypeList[0];
			Filter();
		}
		private void Filter()
		{
			FilterList = ListTours;
			if (Search != "") FilterList = FilterList.Where(x => x.Name.ToLower().Contains(Search.ToLower())).ToList();
			if (SortIndex == 1) FilterList = FilterList.OrderBy(x => x.Name).ToList();
			if (SortIndex == 2) FilterList = FilterList.OrderByDescending(x => x.Name).ToList();
			if (SelectedType.Id != 0) FilterList = FilterList.Where(x => x.ToursTypes.Any(x => x.Type == SelectedType)).ToList();
			UpdateTours();
		}
		private void UpdateTours()
		{
			Tours = FilterList.Skip(_currentPage * _countOnPage).Take(_countOnPage).ToList();
			this.RaisePropertyChanged(nameof(Tours));
		}
		public void Back()
		{
			if (_currentPage > 0)
			{
				_currentPage--;
				UpdateTours();
			}
		}
		public void Next()
		{
			if ((_currentPage + 1) * _countOnPage < FilterList.Count)
			{
				_currentPage++;
				UpdateTours();
			}
		}	
		public async void Delete(Tour tour)
		{
			bool answer = await Message();
			if (answer)
			{
				Tour del = MainWindowViewModel.Context.Tours.First(x => x.Id == tour.Id);
				MainWindowViewModel.Context.Tours.Remove(del);
				MainWindowViewModel.Context.SaveChanges();
				Update();
			}
		}

		private async Task<bool> Message()
		{
			ButtonResult answer = await MessageBoxManager.GetMessageBoxStandard("”даление", "¬ы уверены, что хотите удалить продукт?", ButtonEnum.YesNo).ShowAsync();
			return answer == ButtonResult.Yes; 
		}
	}
}