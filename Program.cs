
using System;
using System.IO;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Criptografia
{
    class Program
    {
        public static object FormUpload { get; private set; }

        static async Task Main(string[] args) { 

            //1. Deserializacao de arquivo JSON em um objeto cripto
            string deserializado = File.ReadAllText(@"answer.json");
            Cripto cripto = JsonConvert.DeserializeObject<Cripto>(deserializado);

            //2. Transformacao de todo o texto para minusculo
            cripto.Cifrado = cripto.Cifrado.ToLower();

            //3. Uso da funcao para descriptografar o texto
            cripto.Decifrado = Decifra(cripto.Cifrado, cripto.NumeroCasas);

            //4. Criptografia do texto decifrado usando sha1
            cripto.ResumoCriptografico = Hash(cripto.Decifrado);

            //5. Escrita de volta em arquivo 
            string serializado = JsonConvert.SerializeObject(cripto);
            File.WriteAllText("answer.txt", serializado);

            //6. Envio via POST da resposta
            var formUpload = new FormUpload();
            await formUpload.Enviar();
        }
        public static string Hash(string stringToHash)
        {
            using var sha1 = new SHA1Managed();
            string hash = BitConverter.ToString(sha1.ComputeHash(Encoding.UTF8.GetBytes(stringToHash)));
            return new String(hash.ToLower().Where(char.IsLetterOrDigit).ToArray());
        }

        public static string ByteToString(byte b)
        {
            return Encoding.ASCII.GetString(new byte[] { (byte)(b) });
        }

        public static string Decifra(string textoCifrado, int numeroCasas)
        {
            byte[] textoEmBytes = Encoding.ASCII.GetBytes(textoCifrado);
            string[] decifrado = new string[textoEmBytes.Length];
            int i = 0;

            foreach (byte b in textoEmBytes) 
            { 
                char caractere = (char)(ByteToString(b)[0]);
                byte letra;

                //verifica se o caractere é uma letra
                if (!Char.IsLetter(caractere))
                {
                    decifrado[i] = ByteToString(b);
                }

                else
                {
                    //se o salto for menor que 'a', então recomeçamos no final do alfabeto
                    if (b - numeroCasas < 96)
                    {
                        letra = (byte)(b + 26 - numeroCasas);
                    }

                    else
                    {
                        letra = (byte)(b - numeroCasas); 
                    }
                    decifrado[i] = ByteToString(letra);
                }
                i++;
            }

            return string.Join("", decifrado); 
        }
    }
}