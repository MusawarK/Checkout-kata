using Checkout.DLL.Strategy;

namespace Checkout.DLL.Tests
{
    public class CheckoutTests
    {
      IPricingStrategy _pricingStrategy;

        [SetUp]
        public void Setup()
        {
            IPricingStrategy pricingStrategy = new CompositePricingStrategy(new List<IPricingStrategy>
            {
                new BulkPricingStrategy("A", 50, 3, 130),
                new BulkPricingStrategy("B", 30, 2, 45),
                new PricingStrategy("C", 20),
                new PricingStrategy("D", 15)
            });

            _pricingStrategy = pricingStrategy;
        }

        [Test]
        public void Empty_Basket_Should_Return_0()
        {
            var sut = new Checkout(_pricingStrategy);
            int total = sut.GetTotalPrice();

            Assert.That(total, Is.EqualTo(0));
        }

        [Test]
        public void One_ItemA_In_Basket_Should_Return_50()
        {
            var sut = new Checkout(_pricingStrategy);
            sut.Scan("A");
            var total = sut.GetTotalPrice();
            
            Assert.That(total, Is.EqualTo(50));
        }

        [Test]
        public void Two_Different_Items_ItemA_And_ItemB_Should_Return_80()
        {
            var sut = new Checkout(_pricingStrategy);
            sut.Scan("A");
            sut.Scan("B");
            var total = sut.GetTotalPrice();
            
            Assert.That(total, Is.EqualTo(80));
        }

        [Test]
        public void Three_ItemAs_Should_Apply_Bulk_Discount_And_Return_130()
        {
            var sut = new Checkout(_pricingStrategy);
            sut.Scan("A");
            sut.Scan("A");
            sut.Scan("A");

            var total = sut.GetTotalPrice();

            Assert.That(total, Is.EqualTo(130));
        }

        [Test]
        public void Two_ItemBs_Should_Apply_Bulk_Discount_And_Return_Total_Price()
        {
            var expected = 45;
            var sut = new Checkout(_pricingStrategy);
            sut.Scan("B");
            sut.Scan("B");
            var total = sut.GetTotalPrice();

            Assert.That(total, Is.EqualTo(expected));
        }

        [Test]
        public void Single_Different_Items_ItemC_And_ItemD_Should_Return_Total_Price()
        {
            var expected = 35;
            var sut = new Checkout(_pricingStrategy);
            sut.Scan("C");
            sut.Scan("D");
            var total = sut.GetTotalPrice();

            Assert.That(total, Is.EqualTo(expected));
        }

        [Test]
        public void Unknown_Item_In_The_Basket_Shoud_Throw_InvalidOperationException()
        {
            var sut = new Checkout(_pricingStrategy);
            sut.Scan("A");
            sut.Scan("A");
            sut.Scan("A"); // 3 Items A => 130
            sut.Scan("B");
            sut.Scan("B"); // 2 Items B => 45
            sut.Scan("C"); // 1 Item C => 20
            sut.Scan("E"); // 1 Item E => Price not set for this item

            var ex = Assert.Throws<InvalidOperationException>(() => sut.GetTotalPrice());

            Assert.That(ex.Message, Does.Contain("Unknown item 'E' in the basket."));
        }

        [Test]
        public void Items_Order_In_The_Basket_Should_Not_Effect_Total()
        {
            var sut = new Checkout(_pricingStrategy);
            sut.Scan("A");
            sut.Scan("B");
            sut.Scan("A");
            sut.Scan("A");

            var total = sut.GetTotalPrice();

            Assert.That(total, Is.EqualTo(160));
        }

        [Test]
        public void Four_ItemAs_Should_Add_One_Bulk_And_One_Single_Item_Price()
        {
            var sut = new Checkout(_pricingStrategy);
            sut.Scan("A");
            sut.Scan("A");
            sut.Scan("A");
            sut.Scan("A");

            var total = sut.GetTotalPrice();

            Assert.That(total, Is.EqualTo(180));
        }

    }
}