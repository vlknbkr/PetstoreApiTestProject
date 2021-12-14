using RestSharp;
using NUnit.Framework;
using System;
using FluentAssertions;
namespace PetstoreApiTestProject;

public class StoreApiTest
{
    private const string BASE_URL = "https://petstore.swagger.io";
    private const string orderID = "2";


    [Test, Order(1), Description("id fields has unvalid type")]
    public void BadRequest_ResponseForUnvalidField()
    {
        string jsonData =   "{\"id\": aa," +
                            "\"petId\": 0," +
                            "\"quantity\": 0," +
                            "\"shipDate\":\"2021-12-14T09:17:50.771Z\"," +
                            "\"status\": \"placed\"," +
                            "\"complete\": true}";

        var restClient = new RestClient(BASE_URL);
        var restRequest = new RestRequest("/v2/store/order/", Method.POST);
        restRequest.AddJsonBody(jsonData);
        restRequest.AddHeader("accept", "application/json");
        restRequest.RequestFormat = DataFormat.Json;

        IRestResponse response = restClient.Execute(restRequest);
        var content = response.Content;
        Console.WriteLine("content :" + content);

        response.StatusCode.ToString().Should().Be("BadRequest");
    }


    [Test, Order(2), Description("/v2/store/order/ 200 OK Request")]
    public void OK_ResponseForValidFields()
    {
        string jsonData =   "{\"id\": 2," +
                            "\"petId\": 0," +
                            "\"quantity\": 0," +
                            "\"shipDate\":\"2021-12-14T09:17:50.771Z\"," +
                            "\"status\": \"placed\"," +
                            "\"complete\": true}";

        var restClient = new RestClient(BASE_URL);
        var restRequest = new RestRequest("/v2/store/order/", Method.POST);
        restRequest.AddJsonBody(jsonData);
        restRequest.AddHeader("accept", "application/json");
        restRequest.RequestFormat = DataFormat.Json;

        IRestResponse response = restClient.Execute(restRequest);
        var content = response.Content;
        Console.WriteLine("content :" + content);

        response.StatusCode.ToString().Should().Be("OK");

    }


    [Test, Order(3), Description("Available order id requested")]
    public void OK_ResponseforAvailableOrderID()
    {
        var restClient = new RestClient(BASE_URL);
        var restRequest = new RestRequest("/v2/store/order/" + orderID, Method.GET);
        restRequest.AddHeader("accept", "application/json");
        restRequest.RequestFormat = DataFormat.Json;

        IRestResponse response = restClient.Execute(restRequest);
        var content = response.Content;
        Console.WriteLine("content :" + content);

        response.StatusCode.ToString().Should().Be("OK");
    }


    [Test, Order(4), Description("Delete existing order id")]
    public void OK_ResponseForExistOrderId()
    {
        var restClient = new RestClient(BASE_URL);
        var restRequest = new RestRequest("/v2/store/order/" + orderID, Method.DELETE);
        restRequest.AddHeader("accept", "application/json");
        restRequest.RequestFormat = DataFormat.Json;

        IRestResponse response = restClient.Execute(restRequest);
        var content = response.Content;
        Console.WriteLine("content :" + content);

        response.StatusCode.ToString().Should().Be("OK");

    }


    [Test, Order(5), Description("Delete Unexist order id")]
    public void NotFound_ResponseForUnExistOrderId()
    {
        var restClient = new RestClient(BASE_URL);
        var restRequest = new RestRequest("/v2/store/order/888", Method.GET);
        restRequest.AddHeader("accept", "application/json");
        restRequest.RequestFormat = DataFormat.Json;

        IRestResponse response = restClient.Execute(restRequest);
        var content = response.Content;
        Console.WriteLine("content :" + content);

        response.StatusCode.ToString().Should().Be("NotFound");
    }


    [Test, Order(6), Description("map of status codes to quantities")]
    public void OK_ResponseForMapOfStatusCodes()
    {
        var restClient = new RestClient(BASE_URL);
        var restRequest = new RestRequest("/v2/store/inventory", Method.GET);
        restRequest.AddHeader("accept", "application/json");

        IRestResponse response = restClient.Execute(restRequest);
        var content = response.Content;
        Console.WriteLine("content :" + content);

        response.StatusCode.ToString().Should().Be("OK");
    }

}