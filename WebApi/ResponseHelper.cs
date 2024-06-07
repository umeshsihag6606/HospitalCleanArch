using Microsoft.AspNetCore.Mvc;
using Shared;

namespace WebApi
{
    public class ResponseHelper
    {
        public static ActionResult GenerateResponse<T>(Result<T> data)
        {
            if (data.Successed)
            {
                var responseObject = new
                {
                    Message = data.Messages,
                    Success = data.Successed,
                    data = data.Data,
                    exception = data.Exception,
                    code = data.Code,
                    Token=data.Token,

                };
                return new OkObjectResult(responseObject);
            }
            else
            {
                var errorObj = new 
                { 
                   message= data.Messages,
                   success = data.Successed,
                   data = new List<string>(),
                   exception= data.Exception,
                   code = data.Code,
                   Token= data.Token,


                };
                return new OkObjectResult(errorObj)
                {
                    StatusCode=data.Code,
                };

            }
        }
    }
}
