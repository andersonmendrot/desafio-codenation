using RestSharp;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Criptografia
{
    public  class FormUpload
    {
        public async Task Enviar()
        {
            // 1. Criar um MultipartFormDataContent
            using var formContent = new MultipartFormDataContent();
            formContent.Headers.ContentType.MediaType = "multipart/form-data";

            // 2. Criar strings para armazenar url, nome do arquivo e caminho para o arquivo
            string url = "https://api.codenation.dev/v1/challenge/dev-ps/submit-solution?token=...";
            string nomeArquivo = "answer.txt";
            string nomeDiretorio = "C:\\Users\\..."; 

            // 3. Abrir o arquivo de texto utilizando OpenRead e armazenando em fileStream
            Stream fileStream = System.IO.File.OpenRead(nomeDiretorio + nomeArquivo);

            //4. Adicionar o arquivo ao Form
            formContent.Add(new StreamContent(fileStream), "answer", nomeArquivo);

            //5. Executar o metodo que envia e receber a resposta, ou retornar mensagem de erro
            using var client = new HttpClient();
            try
            { 
                var message = await client.PostAsync(url, formContent);
                var result = await message.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("Falhou: " + e.Message);
            }
        }
    }
}
