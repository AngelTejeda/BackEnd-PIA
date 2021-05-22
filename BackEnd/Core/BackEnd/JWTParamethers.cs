using Core.Models;
using System.IO;
using System.Text.Json;

namespace Core.BackEnd
{
    public class JwtParamethers
    {
        private JwtParamehtersModel _json;

        public JwtParamethers()
        {
            string fileName = "../Core/jwt.json";
            string jsonString = File.ReadAllText(fileName);

            _json = JsonSerializer.Deserialize<JwtParamehtersModel>(jsonString);
        }

        public string GetSecretKey()
        {
            return _json.SecretKey;
        }

        public string GetIssuer()
        {
            return _json.Issuer;
        }

    }
}