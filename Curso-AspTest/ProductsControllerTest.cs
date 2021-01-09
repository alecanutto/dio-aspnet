using Curso_AspAPI.Controllers;
using Curso_AspMVC.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Curso_AspTest
{
    public class ProductsControllerTest
    {
        private readonly Mock<DbSet<Product>> _mockSet;
        private readonly Mock<Context> _mockContext;
        private readonly Category _category;
        private readonly Product _product;

        public ProductsControllerTest()
        {
            _mockSet = new Mock<DbSet<Product>>();
            _mockContext = new Mock<Context>();
            _category = new Category { Id = 1, Description = "Category Test" };
            _product = new Product { Id = 1, Description = "Product Test", Amount = 6, Category = _category };

            _mockContext.Setup(m => m.Products)
                .Returns(_mockSet.Object);

            _mockContext.Setup(m => m.SetModified(_product));

            _mockContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            _mockContext.Setup(m => m.Products.FindAsync(1))
                .ReturnsAsync(_product);
            
        }
  
        [Fact]
        public async Task Get_product1()
        {
            var service = new ProductsController(_mockContext.Object);

            await service.GetProduct1(1);

            _mockSet.Verify(m => m.FindAsync(1),
                Times.Once());

        }

        [Fact]
        public async Task Put_product()
        {
            var service = new ProductsController(_mockContext.Object);

            await service.PutProduct(1, _product);

            _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()),
                Times.Once());
        }

        [Fact]
        public async Task Post_product()
        {
            var service = new ProductsController(_mockContext.Object);
            await service.PostProduct(_product);

            _mockSet.Verify(x => x.Add(_product), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()),
                Times.Once());
        }

        [Fact]
        public async Task Delete_product()
        {
            var service = new ProductsController(_mockContext.Object);
            await service.DeleteProduct(1);

            _mockSet.Verify(m => m.FindAsync(1),
                Times.Once());
            _mockSet.Verify(x => x.Remove(_product), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()),
                Times.Once());
        }

    }
}
