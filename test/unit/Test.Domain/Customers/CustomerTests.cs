using Domain.Entites.Customers;

namespace Test.Domain.Customers
{
    public class CustomerTests
    {
        private Customer customer;

        public CustomerTests()
        {
            customer = new Customer("John", "Doe", "123 Main St", "12345");
        }

        [Fact]
        public void SetFirstName_Success()
        {
            customer.SetFirstName("Jane");

            Assert.Equal("Jane", customer.FirstName);
        }

        [Fact]
        public void SetFirstName_ShouldFail_WithInvalidValue()
        {
            Assert.Throws<ArgumentNullException>(() => customer.SetFirstName(null));
            Assert.Throws<ArgumentException>(() => customer.SetFirstName(string.Empty));
            Assert.Throws<ArgumentException>(() => customer.SetFirstName("   "));
        }

        [Fact]
        public void SetLastName_Success()
        {
            customer.SetLastName("Smith");

            Assert.Equal("Smith", customer.LastName);
        }

        [Fact]
        public void SetLastName_ShouldFail_WithInvalidValue()
        {
            Assert.Throws<ArgumentNullException>(() => customer.SetLastName(null));
            Assert.Throws<ArgumentException>(() => customer.SetLastName(string.Empty));
            Assert.Throws<ArgumentException>(() => customer.SetLastName("   "));
        }

        [Fact]
        public void SetAddress_Success()
        {
            customer.SetAddress("456 Elm St");

            Assert.Equal("456 Elm St", customer.Address);
        }

        [Fact]
        public void SetAddress_ShouldFail_WithInvalidValue()
        {
            Assert.Throws<ArgumentNullException>(() => customer.SetAddress(null));
            Assert.Throws<ArgumentException>(() => customer.SetAddress(string.Empty));
            Assert.Throws<ArgumentException>(() => customer.SetAddress("   "));
        }

        [Fact]
        public void SetPostalCode_Success()
        {
            customer.SetPostalCode("54321");

            Assert.Equal("54321", customer.PostalCode);
        }

        [Fact]
        public void SetPostalCode_ShouldFail_WithInvalidValue()
        {
            Assert.Throws<ArgumentNullException>(() => customer.SetPostalCode(null));
            Assert.Throws<ArgumentException>(() => customer.SetPostalCode(string.Empty));
            Assert.Throws<ArgumentException>(() => customer.SetPostalCode("   "));
        }
    }
}
