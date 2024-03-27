using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;

namespace Shared.ControlBases;

public class CustomBaseController : ControllerBase
{
    public IActionResult CreateActionResult<T>(ResponseDto<T> response)
    {
        if (response.StatusCode == StatusCodes.Status204NoContent)
        {
            return new ObjectResult(null)
            {
                StatusCode = response.StatusCode
            };
        }
        return new ObjectResult(response)
        {
            StatusCode = response.StatusCode
        };
    }
}
