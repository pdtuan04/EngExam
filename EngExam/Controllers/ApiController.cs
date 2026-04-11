using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EngExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private ISender _sender;
        protected ISender Sender => _sender ??= HttpContext.RequestServices.GetService<ISender>();
    }
}
