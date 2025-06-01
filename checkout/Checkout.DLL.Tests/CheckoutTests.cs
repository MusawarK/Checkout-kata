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
    }
}