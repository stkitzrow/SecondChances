using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Objects.SecondChances.API
{
    public class SecondChancesRestClient
    {
        public SecondChancesRestClient()
        {

        }

        public async Task<RestResponse> PostProductListing() 
        {
            var product = new
            {
                product = new
                {
                    title = "Test Product Title",
                    body_html = "<strong>Test product</strong>",
                    vendor = "Second Chances",
                    product_type = "Item Class",
                }
            };
            var client = new RestClient("https://acumaticaUpcycle.myshopify.com/admin/api/2024-01");
            var request = new RestRequest("/products.json");
            // easily add HTTP Headers
            request.AddHeader("X-Shopify-Access-Token", "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");
            // execute the request
            RestResponse response = await client.PostAsync(request);
            return response;
        }
    }
}
