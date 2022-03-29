namespace Desafio.Umbler.Application.Common
{
    public class Result
    {
        public Result(ResultType responseType, string messenge)
        {
            Message = messenge;
            ResponseType = responseType;
        }

        public Result(ResultType responseType)
        {
            ResponseType = responseType;
        }

        public Result(ResultType responseType, object data)
        {
            ResponseType = responseType;
            Data = data;
        }

        public string Message { get; }

        public ResultType ResponseType { get; }

        public object Data { get; }

        //404 Not Found - Client error
        //The server can not find the requested resource.
        public static Result NotFound(string message = "") => new Result(ResultType.NotFound, message);

        //400 Bad Request - Client error
        //The server cannot or will not process the request due to something that is perceived to be a client error.
        public static Result BadRequest(string message = "") => new Result(ResultType.BadRequest, message);

        //204 No Content - Sucess
        //There is no content to send for this request.
        public static Result NoContent() => new Result(ResultType.NoContent);

        //201 Created - Sucess
        //The request succeeded, and a new resource was created as a result.(POST/PUT)
        public static Result Created(object data) => new Result(ResultType.Created, data);

        //200 OK - Sucess
        //The request succeeded. (GET)
        public static Result Ok(object data) => new Result(ResultType.Ok, data);
    }
}
