using System.Text.Json.Serialization;

namespace SharedLibrary.Dtos
{
    public class  Response<T> where T: class
    {
        public T Data { get; private set; }
        public int StatusCode { get; private set; }
        
        [JsonIgnore] //Seriliazi edilirken ignore edilecek
        public bool IsSuccessfull { get; set; }
        public ErrorDto Error { get; set; }

        public static  Response<T> Success(T data, int statusCode)
        {
            return new Response<T>()
            {
                Data = data,
                StatusCode = statusCode,
                IsSuccessfull = true
            };
        }

        public static Response<T> Success(int statusCode)
        {
            return new Response<T>()
            {
                Data = default,
                StatusCode = statusCode,
                IsSuccessfull = true
            };
        }

        public static Response<T> Fail(ErrorDto error, int statusCode)
        {
            return new Response<T>()
            {
                Error = error,
                StatusCode = statusCode,
                IsSuccessfull = false
            };
        }

        public static Response<T> Fail(string errormessage, int statusCode, bool isShow)
        {
            ErrorDto errorDto = new ErrorDto(errormessage, isShow);

            return new Response<T>()
            {
                Error = errorDto,
                StatusCode = statusCode,
                IsSuccessfull = false
            };
        }


    }
}