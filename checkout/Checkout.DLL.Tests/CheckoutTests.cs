using Checkout.DLL.Strategy;

namespace Checkout.DLL.Tests
{
    // ***************************************************************************************************************************************
    // IMPORTANT NOTES
    // Future Enhancements
    // Current tests use in-memory mock/fake objects.
    // This can be enhanced by introducing mocking frameworks such as Moq or NSubstitute, especially when used in combination with Dependency Injection (DI),
    // to improve test flexibility, control, and isolation of dependencies during testing.
    //      However, this should be carefully planned to ensure the actual logic of the System Under Test (SUT) remains thoroughly tested.
    // ***************************************************************************************************************************************
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

            var expectedTotal = 0;

            var total = sut.GetTotalPrice();

            Assert.That(total, Is.EqualTo(expectedTotal));
        }

        [Test]
        public void One_Item_A_In_Basket_Should_Return_Correct_Total_Price() 
        {
            var sut = new Checkout(_pricingStrategy);
            sut.Scan("A");

            var expectedTotal = 50;
            var total = sut.GetTotalPrice();
            
            Assert.That(total, Is.EqualTo(expectedTotal));
        }

        [Test]
        public void Two_Different_Items_A_And_B_Should_Return_Correct_Total_Price()
        {
            var sut = new Checkout(_pricingStrategy);
            sut.Scan("A");
            sut.Scan("B");

            var expectedTotal = 80;
            var total = sut.GetTotalPrice();
            
            Assert.That(total, Is.EqualTo(expectedTotal));
        }

        [Test]
        public void Three_Items_A_Should_Apply_Bulk_Discount_And_Return_Correct_Total_Price()
        {
            var sut = new Checkout(_pricingStrategy);
            sut.Scan("A");
            sut.Scan("A");
            sut.Scan("A");

            var expectedTotal = 130;
            var total = sut.GetTotalPrice();

            Assert.That(total, Is.EqualTo(expectedTotal));
        }

        [Test]
        public void Two_Items_B_Should_Apply_Bulk_Discount_And_Return_Correct_Total_Price()
        {
            var sut = new Checkout(_pricingStrategy);
            sut.Scan("B");
            sut.Scan("B");

            var expectedTotal = 45;
            var total = sut.GetTotalPrice();

            Assert.That(total, Is.EqualTo(expectedTotal));
        }

        [Test]
        public void Single_Different_Items_C_And_D_Should_Return_Correct_Total_Price()
        {
            var sut = new Checkout(_pricingStrategy);
            sut.Scan("C");
            sut.Scan("D");

            var expectedTotal = 35;
            var total = sut.GetTotalPrice();

            Assert.That(total, Is.EqualTo(expectedTotal));
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

            var expectedMessage = "Unknown item 'E' in the basket.";
            var ex = Assert.Throws<InvalidOperationException>(() => sut.GetTotalPrice());

            Assert.That(ex.Message, Does.Contain(expectedMessage));
        }

        [Test]
        public void Items_Order_In_The_Basket_Should_Not_Effect_Total()
        {
            var sut = new Checkout(_pricingStrategy);
            sut.Scan("A");
            sut.Scan("B");
            sut.Scan("A");
            sut.Scan("A");

            var expectedTotal = 160;
            var total = sut.GetTotalPrice();

            Assert.That(total, Is.EqualTo(expectedTotal));
        }

        [Test]
        public void Four_Items_A_Should_Add_One_Bulk_And_One_Single_Item_Price()
        {
            var sut = new Checkout(_pricingStrategy);
            sut.Scan("A");
            sut.Scan("A");
            sut.Scan("A");
            sut.Scan("A");

            var expectedTotal = 180;
            var total = sut.GetTotalPrice();

            Assert.That(total, Is.EqualTo(expectedTotal));
        }

        [Test]
        public void For_Empty_Pricing_Strategies_List_Unknown_Item_In_The_Basket_Shoud_Throw_InvalidOperationException()
        {
            var sut = new Checkout(new CompositePricingStrategy(new List<IPricingStrategy>()));
            sut.Scan("A");

            var expectedMessage = "Unknown item 'A' in the basket.";
            var ex = Assert.Throws<InvalidOperationException>(() => sut.GetTotalPrice());

            Assert.That(ex.Message, Does.Contain(expectedMessage));
        }

    }
}