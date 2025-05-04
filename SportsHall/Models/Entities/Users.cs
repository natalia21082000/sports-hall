using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Numerics;
using System;

namespace SportsHall.Models.Entities
{
    public class Users
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public DateTime DateOfBirth { get; set; }

        public string ContactPhoneNumber { get; set; } = null!;

        public string EmailAddress { get; set; } = null!;

        public char Gender { get; set; }

        public string Status { get; set; } = null!;

        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Salt { get; set; } = null!;
    }
}