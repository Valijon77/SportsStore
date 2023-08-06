using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers;

public class HomeController : Controller
{
	private IStoreRepository repository;
	public int PageSize = 4;

	public HomeController(IStoreRepository repo)
	{
		repository = repo;
	}

	//public ViewResult Index(int productPage = 1) =>
	//	View(repository.Products
	//		.OrderBy(p => p.ProductID)
	//		.Skip((productPage - 1) * PageSize)
	//		.Take(PageSize)
	//		);

	public ViewResult Index(string? category, int productPage = 1)
		=> View(new ProductsListViewModel
		{
			Products = repository.Products
			.Where(p => category == null || category == p.Category)
			.OrderBy(p => p.ProductID)
			.Skip((productPage - 1) * PageSize)
			.Take(PageSize),
			PagingInfo = new PagingInfo
			{
				CurrentPage = productPage,
				ItemsPerPage = PageSize,
				TotalItems = category == null
				? repository.Products.Count()
				: repository.Products.
				Where(p => p.Category == category).Count()

			},
			CurrentCategory = category
		});

	//public IActionResult Index() => View(repository.Products);

	//public IActionResult Index() => View();
}

