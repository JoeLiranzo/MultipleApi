using MultipleApi.ApisDef;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Xml;
using System.Xml.Serialization;
using System.Net.Http;
using System.Text;

public class MultiApiApp
{
    public static void Main()
    {
        Console.WriteLine("Hello World!");
        Console.ReadLine();
    }

    public async Task<Api1_Output> RequestAPI1(Api1_Input input)
    {
        var Baseurl = "https://localhost:7016/";

        using (var client = new HttpClient())
        {
            //Passing service base url
            client.BaseAddress = new Uri(Baseurl);
            client.DefaultRequestHeaders.Clear();

            var username = "API1USER";
            var password = "API1PASSWORD";

            string creds = string.Format("{0}:{1}", username, password);
            byte[] bytes = Encoding.ASCII.GetBytes(creds);
            var header = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytes));
            client.DefaultRequestHeaders.Authorization = header;

            string json = JsonConvert.SerializeObject(input);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            //Sending request to find web api REST service resource GetAllEmployees using HttpClient
            HttpResponseMessage Res = await client.PostAsync("Api1Offers", httpContent);

            //Checking the response is successful or not which is sent using HttpClient
            if (Res.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api
                var api1_ouput = Res.Content.ReadAsStringAsync().Result;
                //Deserializing the response recieved from web api and storing into the Employee list
                Api1_Output output = JsonConvert.DeserializeObject<Api1_Output>(api1_ouput);

                return output;
            }
        }

        return new Api1_Output { };
    }

    public async Task<Api2_Output> RequestAPI2(Api2_Input input)
    {
        var Baseurl = "https://localhost:7128/";

        using (var client = new HttpClient())
        {
            //Passing service base url
            client.BaseAddress = new Uri(Baseurl);
            client.DefaultRequestHeaders.Clear();

            var username = "API2USER";
            var password = "API2PASSWORD";

            string creds = string.Format("{0}:{1}", username, password);
            byte[] bytes = Encoding.ASCII.GetBytes(creds);
            var header = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytes));
            client.DefaultRequestHeaders.Authorization = header;


            string json = JsonConvert.SerializeObject(input);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            //Sending request to find web api REST service resource GetAllEmployees using HttpClient
            HttpResponseMessage Res = await client.PostAsync("Api2Offers", httpContent);

            //Checking the response is successful or not which is sent using HttpClient
            if (Res.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api
                var api1_ouput = Res.Content.ReadAsStringAsync().Result;
                //Deserializing the response recieved from web api and storing into the Employee list
                Api2_Output output = JsonConvert.DeserializeObject<Api2_Output>(api1_ouput);

                return output;
            }
        }

        return new Api2_Output { };
    }

    public async Task<Api3_Output> RequestAPI3(Api3_Input input)
    {
        var Baseurl = "https://localhost:7173/";

        using (var client = new HttpClient())
        {
            //Passing service base url
            client.BaseAddress = new Uri(Baseurl);
            client.DefaultRequestHeaders.Clear();

            var username = "API1USER";
            var password = "API1PASSWORD";

            string creds = string.Format("{0}:{1}", username, password);
            byte[] bytes = Encoding.ASCII.GetBytes(creds);
            var header = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytes));
            client.DefaultRequestHeaders.Authorization = header;

            //Create our own namespaces for the output
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();

            //Add an empty namespace and empty value
            ns.Add("", "");

            XmlSerializer xsSubmit = new XmlSerializer(typeof(Api3_Input));

            var settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;

            var subReq = input;
            var xml = "";

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww, settings))
                {
                    xsSubmit.Serialize(writer, subReq, ns);
                    xml = sww.ToString();
                }
            }

            StringContent httpContent = new StringContent(xml, System.Text.Encoding.UTF8, "application/xml");

            //Sending request to find web api REST service resource GetAllEmployees using HttpClient
            HttpResponseMessage Res = await client.PostAsync("Api3Offers", httpContent);

            //Checking the response is successful or not which is sent using HttpClient
            if (Res.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api
                var api1_ouput = Res.Content.ReadAsStringAsync().Result;
                //Deserializing the response recieved from web api and storing into the Employee list
                Api3_Output output = JsonConvert.DeserializeObject<Api3_Output>(api1_ouput);

                return output;
            }
        }

        return new Api3_Output { };
    }

    public async Task<long> GetBestDeal(Api1_Input api1_Input, Api2_Input api2_Input, Api3_Input api3_Input)
    {
        List<long> deals = new List<long>();

        deals.Add((await RequestAPI1(api1_Input)).total);
        deals.Add((await RequestAPI2(api2_Input)).amount);
        deals.Add((await RequestAPI3(api3_Input)).quote);

        var bestDeal = deals.Min();
        return bestDeal;
    }
}


//var api1_Input = new Api1_Input { };
//api1_Input.contact_address = "fsdafgasd";
//api1_Input.warehouse_address = "sfasdfasd ";
//api1_Input.package_dimensions = new long[] { 20, 65 };

//var api2_Input = new Api2_Input { };
//api2_Input.consignee = "fsdafgasd";
//api2_Input.consignor = "sfasdfasd ";
//api2_Input.cartons = new long[] { 25, 34 };

//var api3_Input = new Api3_Input { };
//api3_Input.source = "fsdafgasd";
//api3_Input.destination = "sfasdfasd ";
//api3_Input.packages = new packages{ package = new long[] { 27, 52 } };

//Console.WriteLine("Waiting...");
//Console.ReadLine();
//Console.Write("Result API 1: ");
//Console.WriteLine(RequestAPI1(api1_Input).Result.total);
//Console.Write("Result API 2: ");
//Console.WriteLine(RequestAPI2(api2_Input).Result.amount);
//Console.Write("Result API 3: ");
//Console.WriteLine(RequestAPI3(api3_Input).Result.quote);

//Console.WriteLine("Best Deal is: " + await GetBestDeal(api1_Input, api2_Input, api3_Input));
//Console.ReadLine();
