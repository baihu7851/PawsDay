using ApplicationCore.Common;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using PawsDay.Interfaces.Account;
using PawsDay.Services.Product;
using PawsDay.ViewModels.Product;
using PawsDay.ViewModels.SearchProduct;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using ProductViewModel = PawsDay.ViewModels.Product.ProductViewModel;

namespace PawsDay.Controllers
{
    public class ProductController : Controller
    {
        private readonly IAccountManager _accountManager;
        private readonly ProductServices _services;
        public ProductController(ProductServices services, IAccountManager accountManager)
        {
            _services = services;
            _accountManager = accountManager;
        }

        public IActionResult SearchProduct(string searchinput,string sort, string county, string district, string day, string time, string service, string pet)
        {
            ViewData["MemberId"] = _accountManager.GetLoginMemberId();
            ViewData["SearchSort"] = sort;
            SearchProductViewModel searchList = null;
            string input = searchinput == null || searchinput == "" ? "好評推薦" : searchinput.Trim();
            string filterCounty = county == null || county == "countyAll" ? "" : county;
            string filterDistrict = district == null || district == "districtAll" ? "" : district;
            string filterDay = day == null || day == "dayAll" ? "" : day;
            string filterTime = time == null || time == "timeAll" ? "" : time;
            string filterService = service == null || service == "serviceAll" ? "" : service;
            string filterPet = pet == null || pet == "petAll" ? "" : pet;
            var filters = new List<string>() {county ?? "countyAll", district??"districtAll" , day?? "dayAll" ,
                                                            time?? "timeAll" , service??"serviceAll" ,pet?? "petAll"};
            ViewData["Filters"] = filters;
            if (string.IsNullOrEmpty(searchinput) || input == "Recommend" || input == "推薦" || input == "好評推薦")
            {
                searchList = new SearchProductViewModel()
                {
                    SearchInput = "好評推薦",
                    ProductCard = _services.GetProductCardList().Where(p => p.EvaluationAverage >= 4)
                    .Where(p => p.County.Contains(filterCounty) && p.District.Contains(filterDistrict) &&
                                        p.FilterDays.Contains(filterDay) && p.FilterTimes.Contains(filterTime) &&
                                        p.ServiceType.Contains(filterService) && p.FilterPetTypes.Contains(filterPet))
                    .Select(p => new SearchCardModel
                    {
                        ProductId = p.ProductId,
                        SitterName = p.SitterName,
                        ServiceType = p.ServiceType,
                        Image = p.Image,
                        Introduce = p.Introduce.Substring(0, 80) + "．．．",
                        EvaluationAverage = p.EvaluationAverage,
                        EvaluationQuantity = p.EvaluationQuantity,
                        OrderQuantity = p.OrderQuantity,
                        Collect = p.Collect,
                        Price = p.Price.ToString("#,###,0"),
                        ServiceArea = p.ServiceArea.Take(5).Select(a => new AreaDto { District = a.District, County = a.County })
                    }).ToList()
                };
                switch (sort)
                {
                    case "Pawsday":
                        searchList.ProductCard = searchList.ProductCard.OrderByDescending(p => p.EvaluationAverage).ThenByDescending(p => p.OrderQuantity).ToList();
                        break;
                    case "Popular":
                        searchList.ProductCard = searchList.ProductCard.OrderByDescending(p => p.OrderQuantity).ToList();
                        break;
                    case "Evaluation":
                        searchList.ProductCard = searchList.ProductCard.OrderByDescending(p => p.EvaluationAverage).ThenByDescending(p => p.EvaluationQuantity).ToList();
                        break;
                    case "PriceHigh":
                        searchList.ProductCard = searchList.ProductCard.OrderByDescending(p => p.Price).ToList();
                        break;
                    case "PriceLow":
                        searchList.ProductCard = searchList.ProductCard.OrderBy(p => p.Price).ToList();
                        break;
                }
            }
            else
            {
                searchList = new SearchProductViewModel()
                {
                    SearchInput = input,
                    County = county,
                    District = district,
                    Day = day,
                    Time = time,
                    Service = service,
                    Pet = pet,
                    ProductCard = _services.GetProductCardList()
                    .Where(p => p.ServiceType.Contains(input) || p.SitterName.Contains(input) || p.FilterPetTypes.Contains(input) ||
                                            p.County.Contains(input) || p.District.Contains(input) || p.Introduce.Contains(input))
                    .Where(p => p.County.Contains(filterCounty) && p.District.Contains(filterDistrict) &&
                                        p.FilterDays.Contains(filterDay) && p.FilterTimes.Contains(filterTime) &&
                                        p.ServiceType.Contains(filterService) && p.FilterPetTypes.Contains(filterPet))
                    .Select(p => new SearchCardModel
                    {
                        ProductId = p.ProductId,
                        SitterName = p.SitterName,
                        ServiceType = p.ServiceType,
                        Image = p.Image,
                        Introduce = p.Introduce.Substring(0, 80) + "．．．",
                        EvaluationAverage = p.EvaluationAverage,
                        EvaluationQuantity = p.EvaluationQuantity,
                        OrderQuantity = p.OrderQuantity,
                        Collect = p.Collect,
                        Price = p.Price.ToString("#,###,0"),
                        ServiceArea = p.ServiceArea.Take(5).Select(a => new AreaDto { District = a.District, County = a.County })
                    }).ToList()
                };

                switch (sort)
                {
                    case "Pawsday":
                        searchList.ProductCard = searchList.ProductCard.OrderByDescending(p => p.EvaluationAverage).ThenByDescending(p => p.OrderQuantity).ToList();
                        break;
                    case "Popular":
                        searchList.ProductCard = searchList.ProductCard.OrderByDescending(p => p.OrderQuantity).ToList();
                        break;
                    case "Evaluation":
                        searchList.ProductCard = searchList.ProductCard.OrderByDescending(p => p.EvaluationAverage).ThenByDescending(p => p.EvaluationQuantity).ToList();
                        break;
                    case "PriceHigh":
                        searchList.ProductCard = searchList.ProductCard.OrderByDescending(p => p.Price).ToList();
                        break;
                    case "PriceLow":
                        searchList.ProductCard = searchList.ProductCard.OrderBy(p => p.Price).ToList();
                        break;
                }
            }
            return View(searchList);
        }

        public IActionResult Product(int id)
        {
            ViewData["MemberId"] = _accountManager.GetLoginMemberId();
            var product = _services.ProductDtoDetail(id);
            if (product == null)
            {
                return View("NotFound");
            }
            else if (product.State == (int)ProductStatus.OffSale || product.IsDelete == true)
            {
                return View("NotSold");
            }
            var discount = _services.DiscountDetail(id);
            var choose = _services.ChooseDetail(id);
            var evaluations = _services.EvaluationDetail(id).Take(5).ToList();
            ProductViewModel getProduct = new ProductViewModel()
            {
                ProductDetail = new ProductModel
                {
                    ProductId = product.ProductId,
                    State= product.State,
                    Images = product.Images,
                    SitterId = product.SitterId,
                    SitterName = product.SitterName,
                    SitterImg = product.SitterImg,
                    SitterInfo = product.SitterInfo,
                    ServiceType = product.ServiceType,
                    NoticeIntroduce = product.NoticeIntroduce.Replace("\r\n", "<br/>"),
                    ServiceArea = product.ServiceArea.Select(a => new AreaDto { District = a.District, County = a.County }),
                    Introduce = product.Introduce,
                    Price = product.Price.ToString("#,###"),
                    EvaluationAverage = product.EvaluationAverage,
                    OrderQuantity = product.OrderQuantity,
                    Collect = product.Collect,
                    IsDelete = product.IsDelete
                },
                DiscountDetail = discount,
                ChooseDetail = new ProductChooseDto
                {
                    Weekday = choose.Weekday.Distinct().ToList(),
                    Times = choose.Times,
                    County = choose.County.Distinct().ToList(),
                    Areas = choose.Areas,
                    PetType = choose.PetType.Distinct().ToList(),
                    Types = choose.Types
                },
                EvaluationDetail = evaluations
            };
            return View(getProduct);
        }
    }
}
