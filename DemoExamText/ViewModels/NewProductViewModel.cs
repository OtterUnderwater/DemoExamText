using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.Arm;
using DemoExamText.Models;
using ReactiveUI;

namespace DemoExamText.ViewModels
{
	public class NewProductViewModel : ViewModelBase
	{
		Tour tour = new Tour();
		public Tour Tour { get => tour; set => this.RaiseAndSetIfChanged(ref tour, value); }
		public NewProductViewModel(Tour value)
		{
			Tour = value;
		}
		public NewProductViewModel() { }
		public void Save()
		{
			if (Tour.Id == 0)
			{
				MainWindowViewModel.Context.Tours.Add(tour);
			}
			else
			{
				MainWindowViewModel.Context.Update(Tour);
			}


			MainWindowViewModel.Context.SaveChanges();
		}
	}
}