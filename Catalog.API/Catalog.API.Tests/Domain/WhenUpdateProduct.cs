using Catalog.API.Domain.Contracts.Commands;
using Catalog.API.Domain.Contracts.Exceptions;
using Catalog.API.Tests.Factories;
using NUnit.Framework;

namespace Catalog.API.Tests.Domain
{
    [TestFixture]
    public class WhenUpdateProduct
    {
        [Test]
        public void WithValidRequest_ShouldUpdateProperties()
        {
            var updatedCode = "updatedCode";
            var product = ProductFactory.Create();
            var command = new UpdateProduct(updatedCode, product.Name, product.Price);

            product.Update(command);

            Assert.That(product.Code, Is.EqualTo(updatedCode));
        }

        [Test]
        public void WithPriceAboveThreshold_ShouldNotBeApproved()
        {
            var updatedPrice = 1000;
            var product = ProductFactory.Create();
            var command = new UpdateProduct(product.Code, product.Name, updatedPrice);

            product.Update(command);

            Assert.That(product.Price, Is.EqualTo(updatedPrice));
            Assert.That(product.PriceApproved, Is.False);
        }

        [Test]
        public void WithNegativePrice_ShouldThrowException()
        {
            var updatedPrice = -10;
            var product = ProductFactory.Create();
            var command = new UpdateProduct(product.Code, product.Name, updatedPrice);

            Assert.That(() => product.Update(command), Throws.TypeOf<PriceInvalid>());
        }

        [Test]
        public void WithEmptyName_ShouldThrowException()
        {
            var product = ProductFactory.Create();
            var command = new UpdateProduct(product.Code, "", product.Price);

            Assert.That(() => product.Update(command), Throws.TypeOf<ProductNameEmpty>());
        }

        [Test]
        public void WithEmptyCode_ShouldThrowException()
        {
            var product = ProductFactory.Create();
            var command = new UpdateProduct("", product.Name, product.Price);

            Assert.That(() => product.Update(command), Throws.TypeOf<ProductCodeEmpty>());
        }
    }

    [TestFixture]
    public class WhenApproveProductPrice
    {
        [Test]
        public void WhenPriceAlreadyApproved_ShouldThrowException()
        {
            var product = ProductFactory.Create();
            var command = new ApproveProductPrice();
            
            Assert.That(() => product.ApprovePrice(command), Throws.TypeOf<PriceAlreadyApproved>());
        }

        [Test]
        public void WhenPriceNotApproved_ShouldApprove()
        {
            var product = ProductFactory.Create(price: 1000);
            var command = new ApproveProductPrice();

            Assert.That(product.PriceApproved, Is.False);

            product.ApprovePrice(command);

            Assert.That(product.PriceApproved, Is.True);
        }
    }
}