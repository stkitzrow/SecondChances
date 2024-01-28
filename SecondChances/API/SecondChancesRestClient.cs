using RestSharp;
using System;
using System.Threading.Tasks;

namespace PX.Objects.SecondChances {

    public class SecondChancesRestClient {

        public const string BASE_URL = "https://acumaticaUpcycle.myshopify.com";
        public const string ROUTE = "/products";

        public async Task<RestResponse> PostProductListing(string title, byte[] imgBytes) {
            var img64 = Convert.ToBase64String(imgBytes);
            var product = new {
                title,
                body_html = "<strong>Test product</strong>",
                vendor = "Second Chances",
                product_type = "Item Class",
                tags = new string[] { "Upcycling", "Second Chances", "Hackathon" },
                variants = new[] { new { price = "250.00" } },
                images = new[] { new { attachment = img64 } }
            };
            var client = new RestClient(BASE_URL + "/admin/api/2024-01");
            var request = new RestRequest(ROUTE + ".json");
            request.AddJsonBody(product);
            // easily add HTTP Headers
            request.AddHeader("X-Shopify-Access-Token", "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");
            // execute the request
            RestResponse response = await client.PostAsync(request);
            return response;
        }
    }
}
