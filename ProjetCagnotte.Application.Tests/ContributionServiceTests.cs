using Moq;
using ProjetCagnotte.Application.DTOs;
using ProjetCagnotte.Application.Interfaces;
using ProjetCagnotte.Application.Services;
using ProjetCagnotte.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetCagnotte.Application.Tests
{
    //Unit tests on ContributionService:
    public class ContributionServiceTests
    {
        private readonly Mock<IContributionRepository> _contributionRepositoryMock;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly ContributionService _contributionService;

        public ContributionServiceTests()
        {
            _contributionRepositoryMock = new Mock<IContributionRepository>();
            _productRepositoryMock=new Mock<IProductRepository>();

            _contributionService = new ContributionService(_contributionRepositoryMock.Object, _productRepositoryMock.Object);
        }


        [Fact]
        public async Task AddContribution_withPositiveAmount_andContributorName_ShouldBeOK()
        {
            // Arrange, this contribution=10
            var dto = new CreateContributionDto
            {
                ProductId = 1,
                Amount = 10,
                ContributorName = "Test"
            };

            //product price=100
            _productRepositoryMock
                .Setup(x => x.GetProductByIdAsync(1))
                .ReturnsAsync(new Product
                {
                    Price = 100
                });

            //total contributions amount=20
            _contributionRepositoryMock
                .Setup(x => x.GetTotalAmountByProductIdAsync(1))
                .ReturnsAsync(20);

            _contributionRepositoryMock
                .Setup(x => x.AddAsync(It.IsAny<Contribution>()))
                .Returns(Task.CompletedTask);

            // Act
            await _contributionService.AddAsync(dto);

            // Assert (10 ok because < (100-20))
            _contributionRepositoryMock.Verify(
                x => x.AddAsync(
                    It.Is<Contribution>(c =>
                        c.ProductID == 1 &&
                        c.Amount == 10 &&
                        c.Contributor == "Test"
                    )),
                Times.Once
            );
        }



        [Fact]
        public async Task AddContribution_WithAmountLessOrEqualToZero_ShouldThrowArgumentException()
        {
            // Arrange
            var dto = new CreateContributionDto
            {
                ProductId = 1,
                Amount = 0,
                ContributorName = "Test"
            };

            // Act
            Func<Task> action = () =>_contributionService.AddAsync(dto);
           

            // Assert
            var exception =await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Equal("Amount should be greater than zero", exception.Message);


            //check if repository never called because of amount=0
            _contributionRepositoryMock.Verify(
                x => x.AddAsync(It.IsAny<Contribution>()),
                Times.Never
            );
        }




        [Fact]
        public async Task AddContribution_WhenAmountExceedsRemainingAmount_ShouldThrowException()
        {
            // Arrange, contribution=30
            var dto = new CreateContributionDto
            {
                ProductId = 1,
                Amount = 30,
                ContributorName = "Test"
            };

            //product price=100
            _productRepositoryMock
                .Setup(x => x.GetProductByIdAsync(1))
                .ReturnsAsync(new Product
                {
                    Price = 100
                });

            //total contribution amount=80
            _contributionRepositoryMock
                .Setup(x => x.GetTotalAmountByProductIdAsync(1))
                .ReturnsAsync(80);

            // Act
            Func<Task> action = () => _contributionService.AddAsync(dto);

            // Assert, contribution exceed remaining amount --> error
            var exception = await Assert.ThrowsAsync<ArgumentException>(action);

            Assert.Equal("Contribution cannot exceed remaining amount",exception.Message);

            _contributionRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Contribution>()),Times.Never);
        }


        [Fact]
        public async Task AddContribution_WhenProductDoesNotExist_ShouldThrowException()
        {
            // Arrange
            var dto = new CreateContributionDto
            {
                ProductId = 999,
                Amount = 10,
                ContributorName = "Test"
            };

            _productRepositoryMock
                .Setup(x => x.GetProductByIdAsync(999))
                .ReturnsAsync((Product?)null);

            // Act
            Func<Task> action =
                () => _contributionService.AddAsync(dto);

            // Assert
            var exception =
                await Assert.ThrowsAsync<ArgumentException>(action);

            Assert.Equal(
                "Product not found",
                exception.Message
            );

            _contributionRepositoryMock.Verify(
                x => x.AddAsync(It.IsAny<Contribution>()),
                Times.Never
            );
        }









    }
}
