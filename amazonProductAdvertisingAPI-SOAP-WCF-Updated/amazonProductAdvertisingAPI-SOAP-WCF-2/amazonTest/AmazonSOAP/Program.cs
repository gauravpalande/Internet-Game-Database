using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Amazon.PAAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            // Instantiate Amazon ProductAdvertisingAPI client
            AWSECommerceServicePortTypeClient amazonClient = new AWSECommerceServicePortTypeClient();

            // prepare an ItemSearch request
            ItemSearchRequest request = new ItemSearchRequest();
            request.SearchIndex = "VideoGames";
            request.Title = "Minecraft";
            request.ResponseGroup = new string[] { "Small" };

            ItemSearch itemSearch = new ItemSearch();
            itemSearch.Request = new ItemSearchRequest[] { request };
            itemSearch.AWSAccessKeyId = ConfigurationManager.AppSettings["accessKeyId"];
            itemSearch.AssociateTag = "gauravpalande-20";

            // send the ItemSearch request
            ItemSearchResponse response = amazonClient.ItemSearch(itemSearch);

            // write out the results from the ItemSearch request
            foreach (var item in response.Items[0].Item)
            {
                Console.WriteLine(item.ItemAttributes.ListPrice);
            }
            AWSECommerceServicePortTypeClient amazonClient1 = new AWSECommerceServicePortTypeClient();

            // prepare an ItemSearch request
            ItemSearchRequest request1 = new ItemSearchRequest();
            request1.SearchIndex = "Toys";
            request1.Title = "Minecraft";
            request1.ResponseGroup = new string[] { "Small" };

            ItemSearch itemSearch1 = new ItemSearch();
            itemSearch1.Request = new ItemSearchRequest[] { request1 };
            itemSearch1.AWSAccessKeyId = ConfigurationManager.AppSettings["accessKeyId"];
            itemSearch1.AssociateTag = "gauravpalande-20";

            // send the ItemSearch request
            ItemSearchResponse response1 = amazonClient1.ItemSearch(itemSearch1);

            // write out the results from the ItemSearch request
            foreach (var item in response1.Items[0].Item)
            {
                Console.WriteLine(item.ItemAttributes.ListPrice);
            }
            AWSECommerceServicePortTypeClient amazonClient2 = new AWSECommerceServicePortTypeClient();

            // prepare an ItemSearch request
            ItemSearchRequest request2 = new ItemSearchRequest();
            request2.SearchIndex = "MobileApps";
            request2.Title = "Minecraft";
            request2.ResponseGroup = new string[] { "Small" };

            ItemSearch itemSearch2 = new ItemSearch();
            itemSearch2.Request = new ItemSearchRequest[] { request2 };
            itemSearch2.AWSAccessKeyId = ConfigurationManager.AppSettings["accessKeyId"];
            itemSearch2.AssociateTag = "gauravpalande-20";

            // send the ItemSearch request
            ItemSearchResponse response2 = amazonClient.ItemSearch(itemSearch2);

            // write out the results from the ItemSearch request
            foreach (var item in response2.Items[0].Item)
            {
                Console.WriteLine(item.ItemAttributes.ListPrice);
            }
            Console.WriteLine("done...enter any key to continue>");
            Console.ReadLine();

        }
    }
}
