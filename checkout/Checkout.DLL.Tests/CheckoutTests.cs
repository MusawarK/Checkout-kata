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
            var sut = new Checkout();
            sut.Scan("A");
            var total = sut.GetTotalPrice();
            
            Assert.That(total, Is.EqualTo(50));
        }

        [Test]
        public void Two_Different_Items_ItemA_And_ItemB_Should_Return_80()
        {
            var sut = new Checkout();
            sut.Scan("A");
            sut.Scan("B");
            var total = sut.GetTotalPrice();
            
            Assert.That(total, Is.EqualTo(80));
        }

        [Test]
        public void Three_ItemAs_Should_Apply_Bulk_Discount_And_Return_130()
        {
            var sut = new Checkout();
            sut.Scan("A");
            sut.Scan("A");
            sut.Scan("A");

            var total = sut.GetTotalPrice();

            Assert.That(total, Is.EqualTo(130));
        }


    }
}