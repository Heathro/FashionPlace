using business.DTOs;
using business.Services;
using business.Entities;

namespace business;

public partial class MainPage : ContentPage
{
	private AuthService authService = new AuthService();
	private ApiService apiService = new ApiService();

	public MainPage()
	{
		InitializeComponent();
		SecureStorage.RemoveAll();
	}

	private async void OnEntryCompleted(object sender, EventArgs e)
	{
		await LoginAsync();
	}

	private async void OnLoginButtonClicked(object sender, EventArgs e)
	{
		await LoginAsync();
	}

	private void OnLogoutButtonClicked(object sender, EventArgs e)
	{
		Logout();
	}

	private async void OnCreateProduct(object sender, EventArgs e)
	{
		var viewModel = BindingContext as MainViewModel;
		var categories = new List<CreateCategoryDto>();
		foreach (var createCategory in viewModel.CreateCategories)
		{
			var parentCategoryId = createCategory.ParentCategoryId;
			var newCategories =  GetNewCategories(createCategory.NewCategories);

			if (parentCategoryId == null && newCategories.Count == 0) continue;

			categories.Add
			(
				new CreateCategoryDto
				{
					ParentCategoryId = parentCategoryId,
					NewCategories = newCategories
				}
			);
		};

		var createProductDto = new CreateProductDto
		{
			Description = Description.Text,
			Brand = Brand.Text,
			Model = Model.Text,
			Categories = categories,
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

		var productDto = await apiService.CreateProduct(createProductDto);

		Console.WriteLine("\n======>>>>>> Create Products:\n");
		Console.WriteLine(productDto.Id);
		Console.WriteLine("\n");
	}

	private async Task LoginAsync()
	{
		string username = Username.Text;
		string password = Password.Text;

		var result = await authService.Login(username, password);
		if (result)
		{
			ToggleLoginLogoutVisibility(true);
			ErrorMessage.IsVisible = false;

			
			BindingContext = new MainViewModel(await apiService.GetCategories());
		}
		else
		{
			ErrorMessage.Text = "Invalid username or password.";
			ErrorMessage.IsVisible = true;
		}
	}

	private void Logout()
	{
		authService.Logout();
		ToggleLoginLogoutVisibility(false);
	}

	private void ToggleLoginLogoutVisibility(bool isLoggedIn)
	{
		if (isLoggedIn)
		{
			LoginGrid.IsVisible = false;
			MainGrid.IsVisible = true;
		}
		else
		{
			LoginGrid.IsVisible = true;
			MainGrid.IsVisible = false;
		}
	}
	
	private void SelectParentCategoryId(object sender, EventArgs e)
    {
        if (sender is Picker picker && picker.SelectedItem is CategoryPicker selectedPicker)
        {
            if (picker.BindingContext is CreateCategory createCategory)
            {
                createCategory.ParentCategoryId = selectedPicker.Id;
            }
        }
    }

	private List<string> GetNewCategories(string input)
	{
		var categories = new List<string>();
		if (!string.IsNullOrEmpty(input))
		{
			categories = input
							.Split(',')
							.Select(x => x.Trim())
                          	.Where(x => !string.IsNullOrEmpty(x))
							.ToList();
		}
		return categories;
	}
}

