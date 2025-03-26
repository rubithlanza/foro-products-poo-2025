using System.Text.Json.Serialization;

namespace Productos.Api.Dtos.Common
{
    public class ResponseDtos <T>
    {
        [JsonIgnore] // Para que solo yo lo mire 
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
        public T Data { get; set; }
    }
}
