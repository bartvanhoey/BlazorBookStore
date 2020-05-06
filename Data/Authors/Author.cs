using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorBookStore.Data
{
    public class Author
    {
        public Author() {     
        }
        public Author(string id, string lastName, string firstName, string phoneNumber, string emailAddress, int salary, string city)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
            Salary = salary;
            City = city;

        }
        public string Id { get; set; }
        [Required(ErrorMessage="First name is required!")]
        public string FirstName { get; set; }
        [Required(ErrorMessage="Last name is required!")]
        
        public string LastName { get; set; }
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage="Please enter valid email address")]
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        [StringLength(20, ErrorMessage="City Name can't be longer than 20 characters")]
        public string City { get; set; }
        [Range(1000,999999, ErrorMessage="Salary should be greater than 1000")]
        public int Salary { get; set; }
    }
}