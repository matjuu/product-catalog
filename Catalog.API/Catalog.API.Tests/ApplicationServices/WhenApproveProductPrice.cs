using System;
using AutoMapper;
using Catalog.API.ApplicationServices.Exceptions;
using Catalog.API.Contracts.Commands;
using Catalog.API.Domain.Aggregates;
using Catalog.API.Infrastructure;
using Moq;
using NUnit.Framework;

namespace Catalog.API.Tests.ApplicationServices
{
    [TestFixture]
    public class WhenApproveProductPrice
    {
        [Test]
        public void WithInvalidProductId_ThrowsNotFoundException()
        {
            var productRepositoryMock = new Mock<IProductsRepository>();
            var mapperMock = new Mock<IMapper>();
            productRepositoryMock.Setup(m => m.Get(It.IsAny<Guid>())).ReturnsAsync(default(Product));

            var service =
                new API.ApplicationServices.CommandHandlers.WhenApproveProductPrice(productRepositoryMock.Object,
                    mapperMock.Object);
            var request = new ApproveProductPrice {Id = new Guid()};

            Assert.That(async () => await service.Handle(request), Throws.TypeOf<NotFoundException>());
        }
    }
}
