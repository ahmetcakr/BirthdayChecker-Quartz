using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayChecker.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public User()
    {
    }

    public User(int id, string name, DateTime birthDate, string email, string phoneNumber)
    {
        Id = id;
        Name = name;
        BirthDate = birthDate;
        Email = email;
        PhoneNumber = phoneNumber;
    }

}
