namespace SalesManagement.API.Models.Customers;

/// <summary>
/// Represents a request to update customer information.
/// </summary>
public class UpdateCustomerRequest
{
    /// <summary>
    /// Gets or sets the first name of the customer.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the customer.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets the address of the customer.
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Gets or sets the postal code of the customer.
    /// </summary>
    public string PostalCode { get; set; }
}
