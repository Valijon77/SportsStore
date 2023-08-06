using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;


namespace SportsStore.Components;

public class NavigationMenuViewComponent : ViewComponent
{
	private IStoreRepository repository;

	public NavigationMenuViewComponent(IStoreRepository repo)
	{
		repository = repo;
	}


	public IViewComponentResult Invoke()
	{
		//Console.WriteLine("___________________________________________________________________Nav View Nav View Nav View!!!");
		ViewBag.SelectedCategory = RouteData?.Values["category"];
		return View(
			repository.Products
			.Select(x => x.Category)
			.Distinct()
			.OrderBy(x=>x)
		);
    }
}

