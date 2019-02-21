using System;
using Catalog.API.Domain.Contracts.Exceptions;
using Catalog.API.Tests.Factories;
using NUnit.Framework;

namespace Catalog.API.Tests.Domain
{
    [TestFixture]
    public class WhenCreateProduct
    {
        [Test]
        public void WithValidProperties_ProductIsCreated()
        {
            var product = ProductFactory.Create();

            Assert.That(product.Id, Is.Not.EqualTo(default(Guid)));
        }

        [Test]
        public void WithPriceHigherThanThreshold_ShouldNotBeApproved()
        {
            var price = 1000;

            var product = ProductFactory.Create(price: price);

            Assert.That(product.Id, Is.Not.EqualTo(default(Guid)));
            Assert.That(product.Price, Is.EqualTo(price));
            Assert.That(product.PriceApproved, Is.False);
        }

        [Test]
        public void WithNegativePrice_ShouldThrowException()
        {
            var price = -10;

            Assert.That(() => ProductFactory.Create(price: price), Throws.TypeOf<PriceInvalid>());
        }

        [Test]
        public void WithEmptyName_ShouldThrowException()
        {
            var name = "";

            Assert.That(() => ProductFactory.Create(name: name), Throws.TypeOf<ProductNameEmpty>());
        }

        [Test]
        public void WithEmptyCode_ShouldThrowException()
        {
            var code = "";

            Assert.That(() => ProductFactory.Create(code: code), Throws.TypeOf<ProductCodeEmpty>());
        }
    }
}
