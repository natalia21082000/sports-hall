namespace SportsHall.Models.Domains
{
    public class UserReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public string? ProfileImageUrl { get; set; } // URL изображения профиля (может быть null)
        public bool IsEmailConfirmed { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string ContactPhoneNumber { get; set; } = null!;
    }
}
