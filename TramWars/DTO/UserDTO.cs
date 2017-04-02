using Newtonsoft.Json;

namespace TramWars.Dto
{
    public class UserDto
    {
        public string Name { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Password { get; set; }
    }
}