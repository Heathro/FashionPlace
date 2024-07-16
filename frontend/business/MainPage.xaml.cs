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

		var brand = viewModel.Brand;

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

		var variants = new List<CreateVariantDto>();
		foreach (var createVariant in viewModel.CreateVariants)
		{
			variants.Add
			(
				new CreateVariantDto
				{
					Color = createVariant.Color,
					Size = createVariant.Size,
					Price = createVariant.Price,
					Discount = createVariant.Discount,
					Quantity = createVariant.Quantity,
					ImageUrl = createVariant.ImageUrl
				}
			);
		}

		var specifications = new List<CreateSpecificationDto>();
		foreach (var createSpecification in viewModel.CreateSpecifications)
		{
			specifications.Add
			(
				new CreateSpecificationDto
				{
					Type = createSpecification.Type,
					Value = createSpecification.Value
				}
			);
		}

		var createProductDto = new CreateProductDto
		{
			Description = Description.Text,
			Brand = brand,
			Model = Model.Text,
			Categories = categories,
			Variants = variants,
			Specifications = specifications
		};

		await apiService.CreateProduct(createProductDto);

		ResetForm();
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

			ResetForm();
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

	private void SelectBrand(object sender, EventArgs e)
	{
		if (sender is Picker picker && picker.SelectedItem is string selectedBrand)
    	{
			var viewModel = BindingContext as MainViewModel;
			if (selectedBrand == "(New)")
			{
				viewModel.Brand = "";
				BrandEntry.Text = "";
				BrandEntry.IsVisible = true;
				BrandEntry.Focus();
				BrandPicker.SetValue(Grid.ColumnSpanProperty, 1);
			}
			else
			{
				viewModel.Brand = selectedBrand;
				BrandEntry.Text = selectedBrand;
				BrandEntry.IsVisible = false;
				BrandPicker.SetValue(Grid.ColumnSpanProperty, 2);
			}
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

	private void OnDeleteCategoryClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var id = (Guid)button.CommandParameter;
        (BindingContext as MainViewModel).DeleteCategory(id);
    }

	private void SelectColor(object sender, EventArgs e)
    {
		if (sender is Picker picker && picker.SelectedItem is string selectedColor)
    	{
			if (picker.BindingContext is CreateVariant createVariant)
            {
            	var parentGrid = (Grid)picker.Parent;
            	var entry = parentGrid.Children.OfType<Entry>().FirstOrDefault();

            	if (selectedColor == "(New)")
            	{
            	    createVariant.Color = "";
					entry.Focus();
					entry.Text = "";
            	    entry.IsVisible = true;
            	    picker.SetValue(Grid.ColumnSpanProperty, 1);
            	}
            	else
            	{
            	    createVariant.Color = selectedColor;
					entry.Text = selectedColor;
            	    entry.IsVisible = false;
            	    picker.SetValue(Grid.ColumnSpanProperty, 2);
            	}
			}
    	}
    }

	private void SelectSize(object sender, EventArgs e)
    {
		if (sender is Picker picker && picker.SelectedItem is string selectedSize)
    	{
			if (picker.BindingContext is CreateVariant createVariant)
            {
            	var parentGrid = (Grid)picker.Parent;
            	var entry = parentGrid.Children.OfType<Entry>().FirstOrDefault();

            	if (selectedSize == "(New)")
            	{
            	    createVariant.Size = "";
					entry.Text = "";
            	    entry.IsVisible = true;
            	    picker.SetValue(Grid.ColumnSpanProperty, 1);
					entry.Focus();
            	}
            	else
            	{
            	    createVariant.Size = selectedSize;
					entry.Text = selectedSize;
            	    entry.IsVisible = false;
            	    picker.SetValue(Grid.ColumnSpanProperty, 2);
            	}
			}
    	}
    }

	private void OnDeleteVariantClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var id = (Guid)button.CommandParameter;
        (BindingContext as MainViewModel).DeleteVariant(id);
    }

	private void SelectType(object sender, EventArgs e)
    {
		if (sender is Picker picker && picker.SelectedItem is string selectedType)
    	{
			if (picker.BindingContext is CreateSpecification createSpecification)
            {
            	var parentGrid = (Grid)picker.Parent;
            	var entry = parentGrid.Children.OfType<Entry>().FirstOrDefault();

            	if (selectedType == "(New)")
            	{
            	    createSpecification.Type = "";
					entry.Text = "";
            	    entry.IsVisible = true;
            	    picker.SetValue(Grid.ColumnSpanProperty, 1);
					entry.Focus();
            	}
            	else
            	{
            	    createSpecification.Type = selectedType;
					entry.Text = selectedType;
            	    entry.IsVisible = false;
            	    picker.SetValue(Grid.ColumnSpanProperty, 2);
            	}
			}
    	}
    }

	private void OnDeleteSpecificationClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var id = (Guid)button.CommandParameter;
        (BindingContext as MainViewModel).DeleteSpecification(id);
    }

	private async void ResetForm()
	{
		BindingContext = new MainViewModel
		(
			await apiService.GetCategories(),
			await apiService.GetBrands(),
			await apiService.GetColors(),
			await apiService.GetSizes(),
			await apiService.GetSpecificationTypes()
		);
		Model.Text = "";
		Description.Text = "";
	}
}

