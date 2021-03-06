using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using PacknGo.Models;

namespace PacknGo.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
		AccountHandler _handler;

		public AccountController()
		{
			_handler = new AccountHandler();
		}

		[HttpPost("login")]
		public IActionResult MemberAuth(Dictionary<string, string> body)
		{
			JObject result = _handler.GetTokenByUser(body);
			if (result["errorCode"] != null)
			{
				HttpContext.Response.StatusCode = (int)result["errorCode"];
			}
			return new JsonResult(result);
		}

	    [HttpPost("device")]
	    public IActionResult GuestAuth(Dictionary<string, string> body)
	    {
			JObject result = _handler.GetTokenGuest(body);
			if (result["errorCode"] != null)
			{
				HttpContext.Response.StatusCode = (int)result["errorCode"];
			}
			return new JsonResult(result);
		}

		[HttpGet]
		public IActionResult Get()
		{
			JObject result = _handler.GetUserByAccessToken(HttpContext.Request.Headers["AccessToken"]);
			if (result["errorCode"] != null)
			{
				HttpContext.Response.StatusCode = (int)result["errorCode"];
			}
			return new JsonResult(result);
		}
    }
}