using System.ComponentModel.DataAnnotations;

namespace BunnyBot.Resources.Database
{
    public class Vote
    {
        [Key]
        public ulong UserId { get; set; }
        public int Ammount { get; set; }
    }
}
