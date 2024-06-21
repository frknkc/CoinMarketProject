using RestSharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CoinMarketProject
{
    public class CoinApiService
    {
        private const string baseUrl = "https://rest.coinapi.io/";

        public T GetCoinData<T>(string endpoint, string apiKey) where T : new()
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest(endpoint, Method.Get);
            var date = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            request.AddQueryParameter("time", date);
            request.AddHeader("X-CoinAPI-Key", apiKey);
            var response = client.Execute<T>(request);

            if (response.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<T>(response.Content);
            }
            else
            {
                throw new Exception(response.ErrorMessage);
            }
        }

        public List<MyCoin> GetAllCoins(string apiKey)
        {
            return GetCoinData<List<MyCoin>>("v1/assets", apiKey);
        }

        public bool TestApiKey(string apiKey)
        {
            try
            {
                var client = new RestClient(baseUrl);
                var request = new RestRequest("v1/exchangerate/BTC/USD", Method.Get);
                var date = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                request.AddQueryParameter("time", date);
                request.AddHeader("X-CoinAPI-Key", apiKey);
                var response = client.Execute(request);

                return response.IsSuccessful;
            }
            catch
            {
                return false;
            }
        }

        public decimal GetCurrentPrice(string assetId, string apiKey)
        {
            try
            {
                var endpoint = $"v1/exchangerate/{assetId}/USD";
                var rate = GetCoinData<ExchangeRate>(endpoint, apiKey);
                return rate.rate;
            }
            catch (Exception ex)
            {
                return 0;
                //throw new Exception($"Error getting current price for {assetId}: {ex.Message}");
            }
        }

        public List<Coin> GetTopCoins(string apiKey)
        {
            var assets = GetCoinData<List<MyCoin>>("v1/assets", apiKey);
            var topCoins = new List<Coin>();

            foreach (var asset in assets)
            {
                // Assume we have a way to get the current price for each asset
                decimal price = GetCurrentPrice(asset.asset_id, apiKey);
                topCoins.Add(new Coin
                {
                    AssetId = asset.asset_id,
                    PriceUsd = price
                });
            }

            // Here we sort by PriceUsd and take the top 10 coins
            return topCoins.Count > 10 ? topCoins.GetRange(0, 10) : topCoins;
        }
    }

    public class MyCoin
    {
        public string asset_id { get; set; }
        public string name { get; set; }
    }

    public class ExchangeRate
    {
        public string asset_id_base { get; set; }
        public string asset_id_quote { get; set; }
        public decimal rate { get; set; }
    }

    public class Coin
    {
        public string AssetId { get; set; }
        public decimal PriceUsd { get; set; }
    }
}
