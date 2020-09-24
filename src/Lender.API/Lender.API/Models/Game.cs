using Lender.API.Models.Base;

namespace Lender.API.Models
{
    public class Game : Entity
    {
        public string Name { get; set; }

        public string Gender { get; set; }

        public string Url { get; set; }

        public string PublicId { get; set; }

        public string UserId { get; set; }
        public AppUser User { get; set; }

        public void Update(string name, string gender)
        {
            Name = name;
            Gender = gender;
        }
    }
}
