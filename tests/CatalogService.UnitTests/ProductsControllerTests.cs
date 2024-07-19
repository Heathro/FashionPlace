using MassTransit;
using Moq;
using AutoFixture;
using AutoMapper;
using CatalogService.Controllers;
using CatalogService.Helpers;
using CatalogService.Interfaces;
using CatalogService.DTOs;
using Microsoft.AspNetCore.Mvc;
using CatalogService.Entities;

namespace CatalogService.UnitTests;

public class ProductsControllerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IProductsRepository> _productsRepository;
    private readonly Mock<IBrandsRepository> _brandsRepository;
    private readonly Mock<ICategoriesRepository> _categoriesRepository;
    private readonly Mock<IColorsRepository> _colorsRepository;
    private readonly Mock<ISizesRepository> _sizesRepository;
    private readonly Mock<ISpecificationTypesRepository> _specificationTypesRepository;
    private readonly Mock<IPublishEndpoint> _publishEndpoint;
    private readonly Fixture _fixture;
    private readonly IMapper _mapper;
    private readonly ProductsController _productsController;

    public ProductsControllerTests()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _productsRepository = new Mock<IProductsRepository>();
        _brandsRepository = new Mock<IBrandsRepository>();
        _categoriesRepository = new Mock<ICategoriesRepository>();
        _colorsRepository = new Mock<IColorsRepository>();
        _sizesRepository = new Mock<ISizesRepository>();
        _specificationTypesRepository = new Mock<ISpecificationTypesRepository>();
        _publishEndpoint = new Mock<IPublishEndpoint>();
        _fixture = new Fixture();
        _mapper = new Mapper
        (
            new MapperConfiguration(mc => mc.AddMaps(typeof(AutoMapperProfiles).Assembly))
                .CreateMapper()
                .ConfigurationProvider
        );
        _productsController = new ProductsController
        (
            _unitOfWork.Object,
            _mapper,
            _publishEndpoint.Object
        );
    }

    [Fact]
    public async Task GetProducts_With10ProductsInDatabase_Returns10Products()
    {
        var products = _fixture.CreateMany<ProductDto>(10).ToList();
        _unitOfWork.Setup(u => u.Products).Returns(_productsRepository.Object);
        _unitOfWork.Setup(u => u.Products.GetProductsAsync()).ReturnsAsync(products);

        var result = await _productsController.GetProducts();

        Assert.IsType<ActionResult<List<ProductDto>>>(result);
        Assert.Equal(10, result.Value.Count);
    }

    [Fact]
    public async Task GetProducts_WithNoProductsInDatabase_ReturnsEmptyList()
    {
        var products = new List<ProductDto>();
        _unitOfWork.Setup(u => u.Products).Returns(_productsRepository.Object);
        _unitOfWork.Setup(u => u.Products.GetProductsAsync()).ReturnsAsync(products);

        var result = await _productsController.GetProducts();

        Assert.IsType<ActionResult<List<ProductDto>>>(result);
        Assert.Empty(result.Value);
    }

    [Fact]
    public async Task GetProduct_WithValidId_ReturnsProduct()
    {
        var product = _fixture.Create<ProductDto>();
        _unitOfWork.Setup(u => u.Products).Returns(_productsRepository.Object);
        _unitOfWork.Setup(u => u.Products.GetProductAsync(It.IsAny<Guid>())).ReturnsAsync(product);

        var result = await _productsController.GetProduct(product.Id);

        Assert.IsType<ActionResult<ProductDto>>(result);
        Assert.Equal(product, result.Value);
    }

    [Fact]
    public async Task GetProduct_WithInvalidId_ReturnsNotFound()
    {
        var product = _fixture.Create<ProductDto>();
        _unitOfWork.Setup(u => u.Products).Returns(_productsRepository.Object);
        _unitOfWork.Setup(u => u.Products.GetProductAsync(It.IsAny<Guid>())).ReturnsAsync(value: null);

        var result = await _productsController.GetProduct(Guid.NewGuid());

        Assert.IsType<ActionResult<ProductDto>>(result);
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task CreateProduct_WithValidCreateProductDto_ReturnsCreatedAtAction()
    {
        var product = _fixture.Create<CreateProductDto>();
        _unitOfWork.Setup(u => u.Products).Returns(_productsRepository.Object);
        _unitOfWork.Setup(u => u.Brands).Returns(_brandsRepository.Object);
        _unitOfWork.Setup(u => u.Categories).Returns(_categoriesRepository.Object);
        _unitOfWork.Setup(u => u.Colors).Returns(_colorsRepository.Object);
        _unitOfWork.Setup(u => u.Sizes).Returns(_sizesRepository.Object);
        _unitOfWork.Setup(u => u.SpecificationTypes).Returns(_specificationTypesRepository.Object);
        _unitOfWork.Setup(u => u.Products.AddProduct(It.IsAny<Product>()));
        _unitOfWork.Setup(u => u.SaveChangesAsync()).ReturnsAsync(true);

        var result = await _productsController.CreateProduct(product);
        var createdResult = result.Result as CreatedAtActionResult;

        Assert.NotNull(createdResult);
        Assert.Equal("GetProduct", createdResult.ActionName);
        Assert.IsType<ProductDto>(createdResult.Value);
    }

    [Fact]
    public async Task CreateProduct_FailedSave_ReturnsBadRequest()
    {
        var product = _fixture.Create<CreateProductDto>();
        _unitOfWork.Setup(u => u.Products).Returns(_productsRepository.Object);
        _unitOfWork.Setup(u => u.Brands).Returns(_brandsRepository.Object);
        _unitOfWork.Setup(u => u.Categories).Returns(_categoriesRepository.Object);
        _unitOfWork.Setup(u => u.Colors).Returns(_colorsRepository.Object);
        _unitOfWork.Setup(u => u.Sizes).Returns(_sizesRepository.Object);
        _unitOfWork.Setup(u => u.SpecificationTypes).Returns(_specificationTypesRepository.Object);
        _unitOfWork.Setup(u => u.Products.AddProduct(It.IsAny<Product>()));
        _unitOfWork.Setup(u => u.SaveChangesAsync()).ReturnsAsync(false);

        var result = await _productsController.CreateProduct(product);

        Assert.IsType<BadRequestObjectResult>(result.Result);
    }
}