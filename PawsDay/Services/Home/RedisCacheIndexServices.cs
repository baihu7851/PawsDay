using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using PawsDay.Interfaces.Index;
using PawsDay.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace PawsDay.Services.Home
{
    public class RedisCacheIndexServices : IIndexServices
    {
        private readonly IndexServices _indexServices;
        private readonly IDistributedCache _cache;
        private static readonly string _cachekey = "GetRecommend";
        private static readonly TimeSpan _defaultDuraction = TimeSpan.FromSeconds(30);

        public RedisCacheIndexServices(IndexServices indexServices, IDistributedCache cache)
        {
            _indexServices = indexServices;
            _cache = cache;
        }
        public List<RecommendViewModel> GetRank(List<RecommendViewModel> cardList)
        {
            return _indexServices.GetRank(cardList);
        }

        public List<RecommendViewModel> GetRecommendDapper()
        {
            var cacheItems = ByteArrayToObj<List<RecommendViewModel>>(_cache.Get(_cachekey));
            if (cacheItems is null)
            {
                var realItems = _indexServices.GetRecommendDapper();
                var byteArrResult = ObjectToByteArray(realItems);
                _cache.Set(_cachekey, byteArrResult, new DistributedCacheEntryOptions
                {
                    SlidingExpiration = _defaultDuraction,
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
                });
                return realItems;
            }

            return cacheItems;
        }

        public List<RecommendViewModel> Recommend()
        {
            string cachekey = $"recommend";
            var cacheItems = ByteArrayToObj<List<RecommendViewModel>>(_cache.Get(cachekey));
            if (cacheItems is null)
            {
                var realItems = _indexServices.Recommend();
                var byteArrResult = ObjectToByteArray(realItems);
                _cache.Set(cachekey, byteArrResult, new DistributedCacheEntryOptions
                {
                    SlidingExpiration = _defaultDuraction,
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
                });
                return realItems;
            }

            return cacheItems;
        }

        public RecommendListViewModel ServicesRecommend()
        {
            string cachekey = $"servicesRecommend";
            var cacheItems = ByteArrayToObj<RecommendListViewModel>(_cache.Get(cachekey));
            if (cacheItems is null)
            {
                var realItems = _indexServices.ServicesRecommend();
                var byteArrResult = ObjectToByteArray(realItems);
                _cache.Set(cachekey, byteArrResult, new DistributedCacheEntryOptions
                {
                    SlidingExpiration = _defaultDuraction,
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
                });
                return realItems;
            }

            return cacheItems;
        }
        
        private byte[] ObjectToByteArray(object obj)
        {
            return JsonSerializer.SerializeToUtf8Bytes(obj);
        }
        
        private T ByteArrayToObj<T>(byte[] byteArr) where T : class //條件約束  (where T : class)
        {
            return byteArr is null ? null : JsonSerializer.Deserialize<T>(byteArr);
        }
    }
}
