using Dawn;
using Domain.Entites.Core;
using Domain.Entites.Orders;

namespace Domain.Entites.Customers;

public class Customer : Entity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Address { get; private set; }
    public string PostalCode { get; private set; }
    
    public virtual ICollection<Order> Orders { get; set; }

    private Customer() { }

    public Customer(string firstName, string lastName, string address, string postalCode)
    {
        SetFirstName(firstName);
        SetLastName(lastName);
        SetAddress(address);
        SetPostalCode(postalCode);
    }

    public void SetFirstName(string firstName) 
    {
        Guard.Argument(firstName, nameof(firstName)).
            NotNull().
            NotEmpty().
            NotWhiteSpace(); 

        FirstName = firstName;
    }

    public void SetLastName(string lastName)
    {
        Guard.Argument(lastName, nameof(lastName)).
            NotNull().
            NotEmpty().
            NotWhiteSpace();

        LastName = lastName;
    }

    public void SetAddress(string address)
    {
        Guard.Argument(address, nameof(address)).
            NotNull().
            NotEmpty().
            NotWhiteSpace();

        Address = address;
    }

    public void SetPostalCode(string postalCode)
    {
        Guard.Argument(postalCode, nameof(postalCode)).
            NotNull().
            NotEmpty().
            NotWhiteSpace();

        PostalCode = postalCode;
    }
}
