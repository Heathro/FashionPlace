using business.DTOs;

namespace business;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCreateProduct(object sender, EventArgs e)
	{
		var product = new CreateProductDto
		{
			Description = "new description",
			Brand = "new brand",
			Model = "new model",
			Categories = new List<CreateCategoryDto>
			{
				new CreateCategoryDto
				{
					ParentCategoryId = null,
					NewCategories = new List<string>
					{
						"new category"
					}
				}
			},
			Variants = new List<CreateVariantDto>
			{
				new CreateVariantDto
				{
					Color = "new color",
					Size = "new size",
					Price = 1000,
					Discount = 10,
					Quantity = 10,
					ImageUrl = ""
				}
			},
			Specifications = new List<CreateSpecificationDto>
			{
				new CreateSpecificationDto
				{
					Type = "new type",
					Value = "new value"
				}
			}
		};

		Console.WriteLine(product.Brand);
	}
}

