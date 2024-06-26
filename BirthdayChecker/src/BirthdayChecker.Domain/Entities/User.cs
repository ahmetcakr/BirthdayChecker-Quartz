﻿using Core.Persistence.Repositories;

namespace BirthdayChecker.Domain.Entities;

public class User : Entity<int>
{
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}
