using SportsStore.Models;
using Xunit;

namespace SportsStore.Tests;

public class CartTests
{
	[Fact]
	public void Can_Add_New_Lines()
	{
		// Arrange
		Product p1 = new Product { ProductID = 1, Name = "p1" };
		Product p2 = new Product { ProductID = 2, Name = "p2" };

		Cart target = new Cart();

		//Action
		target.AddItem(p1, 1);
		target.AddItem(p2, 1);
		CartLine[] results = target.Lines.ToArray();

		//Assert
		Assert.Equal(2, results.Length);
		Assert.Equal(p1, results[0].Product);
		Assert.Equal(p2, results[1].Product);

	}

	[Fact]
	public void Can_Add_Quantity_For_Existing_Lines()
	{
		// Arrange
		Product p1 = new Product { ProductID = 1, Name = "p1" };
		Product p2 = new Product { ProductID = 2, Name = "p2" };

		Cart target = new Cart();

		// Action
		target.AddItem(p1, 1);
		target.AddItem(p2, 1);
		target.AddItem(p1, 10);

		CartLine[] results = target.Lines.ToArray();

		// Assert
		Assert.Equal(2, results.Length);
		Assert.Equal(11, results[0].Quantity);
		Assert.Equal(1, results[1].Quantity);

	}

	[Fact]
	public void Can_Remove_Line()
	{
		//Arrange
		Product p1 = new Product { ProductID = 1, Name = "p1" };
		Product p2 = new Product { ProductID = 2, Name = "p2" };
		Product p3 = new Product { ProductID = 3, Name = "p3" };

		//Action
		Cart target = new Cart();
		target.AddItem(p1, 1);
		target.AddItem(p2, 3);
		target.AddItem(p3, 5);
		target.AddItem(p2, 1);

		target.RemoveLine(p2);

		//CartLine[] results = target.Lines.ToArray();
		//results

		//Assert
		Assert.Empty(target.Lines.Where(c => c.Product == p2));
		Assert.Equal(2, target.Lines.Count());
		
	}

	[Fact]
	public void Can_Calculate_Cart_Total()
	{
		// Arrange
		Product p1 = new Product { ProductID = 1, Name = "p1", Price = 40m };
		Product p2 = new Product { ProductID = 2, Name = "p2", Price = 50m };
		Product p3 = new Product { ProductID = 3, Name = "p3", Price = 60m };

		Cart target = new Cart();

		//Action
		target.AddItem(p1, 1);
		target.AddItem(p2, 4);
		target.AddItem(p3, 7);

		decimal result = target.ComputeTotalValue();

		//Assert
		Assert.Equal(660m, result);
	}

	[Fact]
	public void Can_Clear_Contents()
	{
		//Arrange
		Product p1 = new Product { ProductID = 1, Name = "p1" };
		Product p2 = new Product { ProductID = 2, Name = "p2" };

		Cart target = new Cart();

		//Act
		target.AddItem(p1, 1);
		target.AddItem(p2, 2);

		target.Clear();

		//Assert
		Assert.Empty(target.Lines);

	}

}

