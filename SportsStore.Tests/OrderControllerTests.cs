using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Models;
using SportsStore.Controllers;
using Xunit;

namespace SportsStore.Tests;

public class OrderControllerTests
{
	[Fact]
	public void Cannot_Checkout_Empty_Cart()
	{
		//Arrange - create mock repository
		Mock<IOrderRepository> mock = new Mock<IOrderRepository>();

		//Arrange - create empty cart
		Cart cart = new Cart();

		//Arrange - create the order
		Order order = new Order();

		//Arrange - create an instance of the conroller
		OrderController target = new OrderController(mock.Object, cart);

		// Act
		ViewResult? result = target.Checkout(order) as ViewResult;

		//Assert - check that order has not been stored.
		mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);

		//Assert - check that method is returning the default view.
		Assert.True(string.IsNullOrEmpty(result?.ViewName));

		//Assert - check that I am passing invalid model to the view.
		Assert.False(result?.ViewData.ModelState.IsValid);
	}

	[Fact]
	public void Cannot_Checkout_Invalid_Shipping_Details()
	{
		// Arrange
		Mock<IOrderRepository> mock = new Mock<IOrderRepository>();

		Cart cart = new Cart();
		cart.AddItem(new Product(), 1);

		OrderController target = new OrderController(mock.Object, cart);

		target.ModelState.AddModelError("error", "error");

		// Act
		ViewResult? result = target.Checkout(new Order()) as ViewResult;

		// Assert
		mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);

		Assert.True(string.IsNullOrEmpty(result?.ViewName));

		Assert.False(result?.ViewData.ModelState.IsValid);
	}

	[Fact]
	public void Can_Checkout_And_Submit_Order()
	{
		// Arrange
		Mock<IOrderRepository> mock = new Mock<IOrderRepository>();

		Cart cart = new Cart();
		cart.AddItem(new Product(), 1);

		OrderController target = new OrderController(mock.Object, cart);

		RedirectToPageResult? result = target.Checkout(new Order()) as RedirectToPageResult;

		//target.Checkout(new Order());

		mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Once);

		Assert.Equal("/Completed", result?.PageName);
	}
}

