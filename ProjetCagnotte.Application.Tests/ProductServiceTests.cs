using Moq;
using ProjetCagnotte.Application.DTOs;
using ProjetCagnotte.Application.Interfaces;
using ProjetCagnotte.Application.Services;
using ProjetCagnotte.Domain.Entities;

namespace ProjetCagnotte.Application.Tests.Services;


//Unit tests on ProductService:
public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IContributionRepository> _contributionRepositoryMock;
    private readonly Mock<IFileStorageService> _fileStorageServiceMock;

    private readonly ProductService _productService;

    public ProductServiceTests()
    {
        _productRepositoryMock =new Mock<IProductRepository>();
        _contributionRepositoryMock =new Mock<IContributionRepository>();
        _fileStorageServiceMock = new Mock<IFileStorageService>();

        _productService = new ProductService(_productRepositoryMock.Object,_contributionRepositoryMock.Object,_fileStorageServiceMock.Object);
    }


    [Fact]
    public async Task AddProduct_ShouldSaveImage_AndAddProduct()
    { //AAA: Arrange, Act, Assert

        // Arrange /////////////////////////////////////////////////////////////////////
        var dto = new CreateProductDto
        {
            ProductName = "Smart TV",
            ProductDescription = "Télévision 4K",
            Price = 500,
            Image = new UploadedFileDto
            {
                FileName = "tv.jpg",
                ContentType = "image/jpeg",
                Content = new MemoryStream()
            }
        };

        //when ProductService call SaveImageAsync --> return "/images/products/tv.jpg" without really save any file
        _fileStorageServiceMock
            .Setup(x => x.SaveImageAsync(dto.Image))
            .ReturnsAsync("/images/products/tv.jpg");



        //when ProductService call AddProductAsync --> return 1
        _productRepositoryMock
            .Setup(x => x.AddProductAsync(It.IsAny<Product>()))
            .ReturnsAsync(1);

        // Act //////////////////////////////////////////////////////////////////////////
        
        //all OK
        var result = await _productService.AddProduct(dto);
 

        // Assert //////////////////////////////////////////////////////////////////////

        //all OK
        Assert.Equal(1, result);


        //check if dependencies called only once
        _fileStorageServiceMock.Verify(x => x.SaveImageAsync(dto.Image),Times.Once);

        //check if ProductMapper ok
        _productRepositoryMock.Verify(x => x.AddProductAsync(It.Is<Product>(p =>
            p.ProductName == "Smart TV" &&
            p.ProductDescription == "Télévision 4K" &&
            p.Price == 500 &&
            p.ImageUrl == "/images/products/tv.jpg"
            )
                ), Times.Once);


    }


    [Fact]
    public async Task AddProduct_WhenImageSaveFails_ShouldNotAddProduct()
    { //AAA: Arrange, Act, Assert

        // Arrange /////////////////////////////////////////////////////////////////////
        var dto = new CreateProductDto
        {
            ProductName = "Smart TV",
            ProductDescription = "Télévision 4K",
            Price = 500,
            Image = new UploadedFileDto
            {
                FileName = "tv.jpg",
                ContentType = "image/jpeg",
                Content = new MemoryStream()
            }
        };

      
        //when ProductService call SaveImageAsync --> return error
        _fileStorageServiceMock
        .Setup(x => x.SaveImageAsync(dto.Image))
        .ThrowsAsync(new IOException("Unable to save image"));



        // Act //////////////////////////////////////////////////////////////////////////
              
        //error
        var exception = await Assert.ThrowsAsync<IOException>(() => _productService.AddProduct(dto));

        // Assert //////////////////////////////////////////////////////////////////////


        //error
        Assert.Equal("Unable to save image", exception.Message);


        //check if error -->  AddProductAsync never called
        _productRepositoryMock.Verify(x => x.AddProductAsync(It.IsAny<Product>()), Times.Never);
    }



    [Fact]
    public async Task UpdateProduct_ShouldUpdateProduct()
    {
        // Arrange
        int id = 1;

        var dto = new UpdateProductDto
        {
            ProductName = "Smart TV Updated",
            ProductDescription = "Télévision 4K mise à jour",
            Price = 600
        };

        _productRepositoryMock
            .Setup(x => x.UpdateProductAsync(id,It.IsAny<Product>()))
            .ReturnsAsync(true);

        // Act
        var result = await _productService.UpdateProduct(id, dto);

        // Assert
        Assert.True(result);

        _productRepositoryMock.Verify(
            x => x.UpdateProductAsync(
                id,
                It.Is<Product>(p =>
                    p.ProductName == "Smart TV Updated" &&
                    p.ProductDescription == "Télévision 4K mise à jour" &&
                    p.Price == 600
                )),
            Times.Once
        );
    }


    [Fact]
    public async Task UpdateProduct_WhenProductNameIsEmpty_ShouldThrowArgumentException()
    {
        // Arrange
        var dto = new UpdateProductDto
        {
            ProductName = "",
            ProductDescription = "Télévision",
            Price = 600
        };

        // Act
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _productService.UpdateProduct(1, dto));

        // Assert
        Assert.Equal("Product name is mandatory",exception.Message);



        //never arrives to repository if empty name
        _productRepositoryMock.Verify(
            x => x.UpdateProductAsync(
                It.IsAny<int>(),
                It.IsAny<Product>()),
            Times.Never
        );
    }


    [Fact]
    public async Task UpdateProduct_WhenPriceIsZero_ShouldThrowArgumentException()
    {
        // Arrange
        var dto = new UpdateProductDto
        {
            ProductName = "Smart TV",
            ProductDescription = "Télévision 4K",
            Price = 0
        };

        // Act
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _productService.UpdateProduct(1, dto));

        // Assert
        Assert.Equal("Price must be greater than 0",exception.Message);

        //never arrives to repository if price=0
        _productRepositoryMock.Verify(
            x => x.UpdateProductAsync(
                It.IsAny<int>(),
                It.IsAny<Product>()),
            Times.Never
        );
    }
}