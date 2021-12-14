using RestSharp;
using NUnit.Framework;
using System;
namespace PetstoreApiTestProject;

public class Tests
{
    private const string BASE_URL = "https://petstore.swagger.io";
    private const string orderID = "2";


    /**
    * /v2/store/order/ endpoint 400 Bad request case
    * @id field requseted as string instead of int
    **/
    [Test, Order(1)]
    public void postByOrderID_400()
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
            
        Assert.AreEqual("BadRequest", response.StatusCode.ToString());
    }

    /**
    * /v2/store/order/ endpoint 200 OK case
    * enpoint requested with below json body
    **/
    [Test, Order(2)]
    public void postByOrderID_200()
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

        Assert.AreEqual("OK", response.StatusCode.ToString());
    
    }

    /**
    * /v2/store/order/{orderId} endpoint 200 OK case
    * for GET method. 
    **/
    [Test, Order(3)]
    public void getByOrderID_200()
    {
        var restClient = new RestClient(BASE_URL);
        var restRequest = new RestRequest("/v2/store/order/" + orderID, Method.GET);
        restRequest.AddHeader("accept", "application/json");
        restRequest.RequestFormat = DataFormat.Json;

        IRestResponse response = restClient.Execute(restRequest);
        var content = response.Content;
        Console.WriteLine("content :" + content);

        Assert.AreEqual("OK", response.StatusCode.ToString());
    }

    /**
    * /v2/store/order/{orderId} endpoint 200 OK case
    * for DELETE method.
    **/
    [Test, Order(4)]
    public void deleteByOrderID_200()
    {
        var restClient = new RestClient(BASE_URL);
        var restRequest = new RestRequest("/v2/store/order/" + orderID, Method.DELETE);
        restRequest.AddHeader("accept", "application/json");
        restRequest.RequestFormat = DataFormat.Json;

        IRestResponse response = restClient.Execute(restRequest);
        var content = response.Content;
        Console.WriteLine("content :" + content);

        Assert.AreEqual("OK", response.StatusCode.ToString());    

    }

    /**
    * /v2/store/order/{orderId} endpoint 404 Not Found case
    **/
    [Test, Order(5)]
    public void getByOrderID_404()
    {
        var restClient = new RestClient(BASE_URL);
        var restRequest = new RestRequest("/v2/store/order/" + orderID, Method.GET);
        restRequest.AddHeader("accept", "application/json");
        restRequest.RequestFormat = DataFormat.Json;

        IRestResponse response = restClient.Execute(restRequest);
        var content = response.Content;
        Console.WriteLine("content :" + content);

        Assert.AreEqual("NotFound", response.StatusCode.ToString());
    }

    /**
    * /v2/store/order/{orderId} endpoint 200 OK case
    * for DELETE method.
    **/
    [Test, Order(6)]
    public void deleteByOrderID_404()
    {        
        var restClient = new RestClient(BASE_URL);
        var restRequest = new RestRequest("/v2/store/order/12", Method.DELETE);
        restRequest.AddHeader("accept", "application/json");
        restRequest.RequestFormat = DataFormat.Json;

        IRestResponse response = restClient.Execute(restRequest);
        var content = response.Content;
        Console.WriteLine("content :" + content);

        Assert.AreEqual("NotFound", response.StatusCode.ToString());
    }

    /**
    * /v2/store/inventory endpoint 200 OK case
    * it should be Returns a map of status codes to quantities
    **/
    [Test, Order(7)]
    public void getInventory_200()
    {
        var restClient = new RestClient(BASE_URL);
        var restRequest = new RestRequest("/v2/store/inventory", Method.GET);
        restRequest.AddHeader("accept", "application/json");

        IRestResponse response = restClient.Execute(restRequest);
        var content = response.Content;
        Console.WriteLine("content :" + content);

        Assert.AreEqual("OK", response.StatusCode.ToString());
    }
}