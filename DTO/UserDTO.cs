using Newtonsoft.Json;

namespace TramWars.DTO
{
    public class UserDTO
    {
        public string Name { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Password { get; set; }
    }
}