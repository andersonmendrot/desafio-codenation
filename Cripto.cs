using Newtonsoft.Json;
using System;

namespace Criptografia
{
    public class Cripto{
        [JsonProperty(PropertyName = "numero_casas")]
        public int NumeroCasas { get; set; }
        [JsonProperty(PropertyName = "token")]
        public String Token { get; set; }
        [JsonProperty(PropertyName = "cifrado")]
        public String Cifrado { get; set; }
        [JsonProperty(PropertyName = "decifrado")]
        public String Decifrado { get; set; }
        [JsonProperty(PropertyName = "resumo_criptografico")]
        public String ResumoCriptografico { get; set; }
    }
}
