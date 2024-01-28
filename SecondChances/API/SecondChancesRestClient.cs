using RestSharp;
using System;
using System.Threading.Tasks;

namespace PX.Objects.SecondChances {

    public class SecondChancesRestClient {

        public const string BASE_URL = "https://acumaticaUpcycle.myshopify.com";
        public const string ROUTE = "/products";

        public RestResponse PostProductListing(string title, string body_html, string product_type, byte[] imgBytes) {
            var img64 = Convert.ToBase64String(imgBytes);
            var req = new {
                product = new {
                    title,
                    body_html,
                    vendor = "Second Chances",
                    product_type,
                    tags = new string[] { "Upcycling", "Second Chances", "Hackathon" },
                    variants = new[] { new { price = "250.00" } },
                    images = new[] { new { attachment = img64 } }
                }
            };
            var client = new RestClient(BASE_URL + "/admin/api/2024-01");
            var request = new RestRequest(ROUTE + ".json", Method.Post);
            request.AddJsonBody(req);
            // easily add HTTP Headers
            request.AddHeader("X-Shopify-Access-Token", "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");
            // execute the request
            RestResponse response = client.Execute(request);
            return response;
        }
    }
}
