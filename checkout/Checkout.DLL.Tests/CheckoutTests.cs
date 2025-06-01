namespace Checkout.DLL.Tests
{
    public class CheckoutTests
    {
        [Test]
        public void Empty_Basket_Should_Return_0()
        {
            var sut = new Checkout();
            int total = sut.GetTotalPrice();

            Assert.That(total, Is.EqualTo(0));
        }

        [Test]
        public void One_ItemA_In_Basket_Should_Return_50()
        {
            var checkout = new Checkout();
            checkout.Scan("A");
            var total = checkout.GetTotalPrice();
            Assert.That(total, Is.EqualTo(50));
        }

        [Test]
        public void Two_Different_Items_ItemA_And_ItemB_Should_Return_80()
        {
            var checkout = new Checkout();
            checkout.Scan("A");
            checkout.Scan("B");
            var total = checkout.GetTotalPrice();
            Assert.That(total, Is.EqualTo(80));
        }

    }
}