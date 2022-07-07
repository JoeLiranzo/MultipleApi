using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;

namespace API2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Api2OffersController : ControllerBase
    {
        private readonly ILogger<Api2OffersController> _logger;

        public Api2OffersController(ILogger<Api2OffersController> logger)
        {
            _logger = logger;
        }

        private static bool CheckPassword(string username, string password)
        {
            return username == "API2USER" && password == "API2PASSWORD";
        }

        private static int? AuthenticateUser(string credentials)
        {
            try
            {
                var encoding = Encoding.GetEncoding("iso-8859-1");
                credentials = encoding.GetString(Convert.FromBase64String(credentials));

                int separator = credentials.IndexOf(':');
                string name = credentials.Substring(0, separator);
                string password = credentials.Substring(separator + 1);

                if (CheckPassword(name, password))
                {
                    var identity = new GenericIdentity(name);
                    //SetPrincipal(new GenericPrincipal(identity, null));
                    return null;
                }
                else
                {
                    // Invalid username or password.
                    //HttpContext.Response.StatusCode = 401;
                    return 401;
                }
            }
            catch (FormatException)
            {
                //HttpContext.Response.StatusCode = 401;
                return 401;
            }
        }

        [HttpPost(Name = "GetOffersApi2")]
        public Api2_Output Get([FromBody] Api2_Offers apiOffers)
        {
            var request = HttpContext.Request;
            var authHeader = request.Headers["Authorization"];

            var authHeaderVal = AuthenticationHeaderValue.Parse(authHeader);

            // RFC 2617 sec 1.2, "scheme" name is case-insensitive
            if (authHeaderVal.Scheme.Equals("basic",
                    StringComparison.OrdinalIgnoreCase) &&
                authHeaderVal.Parameter != null)
            {
                var code = AuthenticateUser(authHeaderVal.Parameter);
                if (code != null)
                {
                    HttpContext.Response.StatusCode = code.Value;
                }
            }

            //Internally evaluate contact address and warehouse address and do some job to determine the real price of the product
            //In the wished city.
            //For this test I used a basic calculation of multiplication of every dimensions of the package and returning it as a result.
            long amount = 1;
            foreach (var dimension in apiOffers.Cartons)
            {
                amount *= dimension;
            }

            Api2_Output output = new()
            {
                Amount = amount
            };

            return output;
        }
    }
}