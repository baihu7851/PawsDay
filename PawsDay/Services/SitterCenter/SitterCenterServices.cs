using ApplicationCore.Common;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using CloudinaryDotNet.Actions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using PawsDay.Interfaces.Account;
using PawsDay.Models;
using PawsDay.Models.SitterCenter;
using PawsDay.Models.SitterCenter.WebApi;
using PawsDay.ViewModels.MemberCenter;
using PawsDay.ViewModels.SitterCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PawsDay.Services.SitterCenter
{
    public  class SitterCenterServices
    {
        #region 建構式
        private readonly IRepository<RegisterSitter> _registersitter;
        private readonly IRepository<ApplicationCore.Entities.Product> _product;
        private readonly IRepository<ProductImage> _productimage;
        private readonly IRepository<ProductServiceArea> _productservicearea;
        private readonly IRepository<ProductServicePetType> _productservicepettype;
        private readonly IRepository<ProductServiceTime> _productservicetime;
        private readonly IRepository<ProductDiscount> _productdiscount;
        private readonly IRepository<AdProject> _adproject;
        private readonly IRepository<District> _district;
        private readonly IRepository<County> _county;
        private readonly IRepository<ServiceType> _servicetype;
        private readonly IRepository<Member> _member;
        private readonly IRepository<Order> _order;
        private readonly IAccountManager _accountManager;
        private readonly IProductRepository _productrepository;

        int memberid;
        public SitterCenterServices
            (IRepository<RegisterSitter> registersitter,
            IRepository<ApplicationCore.Entities.Product> product,
            IRepository<ProductImage> productimage,
            IRepository<ProductServiceArea> productservicearea,
            IRepository<ProductServicePetType> productservicepettype,
            IRepository<ProductServiceTime> productservicetime,
            IRepository<ProductDiscount> productdiscount,
            IRepository<AdProject> adproject,
            IRepository<District> district,
            IRepository<County> county,
            IRepository<ServiceType> servicetype,
            IRepository<Member> member,
            IRepository<Order> order,
            IAccountManager accountManager,
            IProductRepository productrepository
)
        {
            _registersitter = registersitter;
            _product = product;
            _productimage = productimage;
            _productservicearea = productservicearea;
            _productservicepettype = productservicepettype;
            _productservicetime = productservicetime;
            _productdiscount = productdiscount;
            _adproject = adproject;
            _district = district;
            _county = county;
            _servicetype = servicetype;
            _member = member;
            _order = order;
            _accountManager = accountManager;
            _productrepository = productrepository;
            memberid = _accountManager.GetLoginMemberId();

        }
        #endregion

        #region 商品
        //商品列表(上下架頁面都會call)
        public TransResultDto<List<ProductIncludeAreaDto>> GetProductList(ProductStatus productstatus)
        {

            //先拿出productlist，把district先處理掉
            var products = _product.GetAllReadOnly().Where(p => p.ProductStatus == (int)productstatus && p.SitterId == memberid).ToList();
            var areas = from p in products
                        join area in _productservicearea.GetAllReadOnly() on p.ProductId equals area.ProductId
                        select new ProductAreaNameDto
                        {
                            ProductId = p.ProductId,
                            DistrictName = _district.GetAllReadOnly().First(d => d.DistrictId == area.District).DistrictName,
                            CountyName = _county.GetAllReadOnly().First(c => c.CountyId == area.County).CountyName
                        };

            var productlist = products.Select(result => new ProductIncludeAreaDto
            {
                ProductID = result.ProductId,
                ServiceType = _servicetype.GetAllReadOnly().First(s => s.ServiceTypeId == result.ServiceType).TypeName,
                Introduce = result.Introduce,
                MainImage = _productimage.GetAllReadOnly().First(img => img.ProductId == result.ProductId && img.Sort == 1).Image,
                SitterName = _registersitter.GetAllReadOnly().First(sit => sit.MemberId == result.SitterId).SitterName,
                ServiceArea = areas.Where(a => a.ProductId == result.ProductId).Take(2).ToList()
            }).ToList();

            return SitterCenterResponseHelper.ReadResponse(productlist);
        }

        /// <summary>
        /// 商品規格
        /// Get: ServiceType、ProductIntro、SitterName、UpdateTime走Model存取
        /// Post: 全走WebApi
        /// 其餘資料打WebApi
        /// </summary>

        public TransResultDto<string> GetSitterName()
        {
            var sittername = _registersitter.GetAllReadOnly().First(s => s.MemberId == memberid).SitterName;
            return SitterCenterResponseHelper.ReadResponse(sittername);
        }

        //讀取商品
        public TransResultDto<ProductDetailDto> GetProductDetail(int productid)
        {
            var productdata = _product.GetById(productid);
            var detail = new ProductDetailDto
            {
                ProductID = productid,
                ServiceType = productdata.ServiceType,
                Introduce = productdata.Introduce,
                UpdateTime = productdata.EditTime.AddHours(8),
                SitterName = _registersitter.GetAllReadOnly().First(s => s.MemberId == productdata.SitterId).SitterName
            };
            return SitterCenterResponseHelper.ReadResponse(detail);
        }

        //取得商品詳細資訊
        public ResultDto GetProducttDetailById(int productid)
        {
            var response = new ResultDto();

            var basic = _product.GetAllReadOnly().Where(p => p.ProductId == productid)
                .Select(p => new ProductBasicDto
                {
                    Status = p.ProductStatus == (int)ProductStatus.OnSale ? true : false,
                }).First();
            var counties = _productservicearea.GetAllReadOnly().Where(a => a.ProductId == productid).Select(a => a.County).Distinct().ToList();
            var districts = _productservicearea.GetAllReadOnly().Where(a => a.ProductId == productid).Select(a => a.District).Distinct().ToList();
            var productdata = _productservicepettype.GetAllReadOnly().Where(p => p.ProductId == productid)
                .Select(p => new ProductDetailWithPriceDto
                {
                    PetType = p.PetType,
                    ShapeType = p.ShapeType,
                    Price = p.Price,
                    NightPrice = p.OvernightPrice
                }).ToList();
            var timedata = _productservicetime.GetAllReadOnly().Where(p => p.ProductId == productid).ToList()
                .GroupBy(p => p.ServiceDay).Select(p => new ServiceTimeDto
                {
                    Week = p.Key,
                    PartTime = p.Select(p => p.ServicePartTime).ToArray()
                }).ToList();
            var imgdata = _productimage.GetAllReadOnly().Where(p => p.ProductId == productid).OrderBy(p => p.Sort).Select(p => p.Image).ToList();

            //組裝
            var data = new ServiceSettingDto
            {
                Id = productid,
                BasicInfo = basic,
                County = counties,
                District = districts,
                PriceDetail = productdata,
                Time = timedata,
                ImageUrl = imgdata
            };

            if (data is null)
            {
                response.Message = "Not Found";
                return response;
            }

            response.IsSuccess = true;
            response.Data = data;
            return response;

        }

        /// <summary>
        /// 呼叫專用型ProductRepository，一次新增、刪除4張表
        /// </summary>
        public async Task<ResultDto> CreateOrUpdate(ServiceSettingDto request)
        {
            var response = new ResultDto();
            //重組服務地區
            var newarea = new List<ProductServiceArea>();
            foreach (var item in request.District)
            {
                var a = new ProductServiceArea
                {
                    ProductId = request.Id,
                    County = _district.GetAllReadOnly().Where(d => d.DistrictId == item).Select(d => d.CountyId).First(),
                    District = item,
                };
                newarea.Add(a);
            }
            //重組服務價格
            var newprice = new List<ProductServicePetType>();
            foreach (var item in request.PriceDetail)
            {
                var pricedata = new ProductServicePetType
                {
                    ProductId = request.Id,
                    PetType = item.PetType,
                    ShapeType = item.ShapeType,
                    Price = item.Price,
                    OvernightPrice = item.NightPrice
                };
                newprice.Add(pricedata);
            }
            //重組服務時間
            var newtime = new List<ProductServiceTime>();
            //先解servicetime陣列
            foreach (var item in request.Time)
            {
                var weekid = item.Week;
                int dayid;
                //再解裡面的parttime陣列
                foreach (var part in item.PartTime)
                {
                    dayid = part;
                    var time = new ProductServiceTime
                    {
                        ProductId = request.Id,
                        ServiceDay = weekid,
                        ServicePartTime = dayid
                    };
                    //重新組織成ProductServiceTime後打包
                    newtime.Add(time);
                }
            }
            //重組服務照片
            var newimg = new List<ProductImage>();
            int index = 1;
            foreach (var item in request.ImageUrl)
            {
                var imgdata = new ProductImage
                {
                    ProductId = request.Id,
                    Image = item,
                    Sort = index
                };
                newimg.Add(imgdata);
                index++;
            }

            var check = _productservicearea.GetAllReadOnly().Any(p => p.ProductId == request.Id);
            if (!check) //如果id沒有資料存在，前往create(純新增)
            {
                try
                {
                    await _productrepository.CreateProductDetailById(newarea, newprice, newtime, newimg);
                }
                catch (Exception ex)
                {
                    response.Message = $"新增失敗:{ex.Message}";
                    return response;
                }
            }
            else //如果id有資料存在，前往update(先刪除後新增)
            {
                try
                {
                    await _productrepository.UpdateProductDetailById(request.Id, newarea, newprice, newtime, newimg);
                }
                catch (Exception ex)
                {
                    response.Message = $"新增失敗:{ex.Message}";
                    return response;
                }
            }
            response.IsSuccess = true;
            response.Message = "新增成功";
            return response;

        }

        //判斷純新增or更新
        public ResultDto CreateOrUpdateBasic(ProductBasicDto request)
        {
            var check = _productservicetime.GetAllReadOnly().Any(p => p.ProductId == request.Productid);
            if (!check)
            {
                return CreateProductBasicInfo(request);
            }
            else
            {
                return UpdateProductBasicInfo(request);
            }
        }

        //新增商品基本資料
        public ResultDto CreateProductBasicInfo(ProductBasicDto request)
        {
            var response = new ResultDto();
            var product = new ApplicationCore.Entities.Product();

            product.ProductStatus = request.Status ? (int)ProductStatus.OnSale : (int)ProductStatus.OffSale;
            product.ServiceType = request.ServiceType;
            product.Introduce = request.Introduce;
            product.CreateTime = DateTime.UtcNow;
            product.EditTime = DateTime.UtcNow;
            product.SitterId = memberid;
            product.IsDelete = false;

            try
            {
                var res = _product.Add(product);
                return new ResultDto(res);
            }
            catch (Exception ex)
            {
                response.Message = $"新增失敗:{ex.Message}";
                return response;
            }
        }

        //更新商品基本資料
        public ResultDto UpdateProductBasicInfo(ProductBasicDto request)
        {
            var response = new ResultDto();
            var product = _product.GetAll().First(p => p.ProductId == request.Productid);

            product.ProductStatus = request.Status == true ? (int)ProductStatus.OnSale : (int)ProductStatus.OffSale;
            product.ServiceType = request.ServiceType;
            product.Introduce = request.Introduce;
            product.EditTime = DateTime.UtcNow;

            try
            {
                var res = _product.Update(product);
                return new ResultDto(res);
            }
            catch (Exception ex)
            {
                response.Message = $"新增失敗:{ex.Message}";
                return response;
            }
        }


        //預覽商品促銷資訊、服務說明
        public ResultDto GetDiscountandIntro(int productid)
        {
            var discount = _productdiscount.GetAllReadOnly().FirstOrDefault(d => d.ProductId == productid);
            var notice = SitterCenterResponseHelper.GetServiceType(_product.GetById(productid).ServiceType);
            var info = _registersitter.GetAllReadOnly().First(s => s.MemberId == _product.GetById(productid).SitterId).SitterInfo;
            var previewdata = new PreviewDto();

            previewdata.Productid = productid;
            previewdata.ServiceNotice = notice;
            previewdata.SitterInfo = info;

            if (discount != null)
            {
                previewdata.Quantity = discount.Quantity;
                previewdata.Discount = discount.Discount;
            }

            return new ResultDto(previewdata);
        }


        #endregion

        #region 促銷
        //商品促銷列表
        public TransResultDto<List<ProductDicountDto>> GetProductDiscount()
        {
            var productids = _product.GetAllReadOnly().Where(p => p.SitterId == memberid && p.ProductStatus == (int)ProductStatus.OnSale).Select(p => p.ProductId).ToList();
            var producthasdiscountids = _productdiscount.GetAllReadOnly().Where(d => productids.Contains(d.ProductId)).Select(d => d.ProductId).ToList();
            var union = productids.Intersect(producthasdiscountids);

            var products = _product.GetAllReadOnly().Where(p => union.Contains(p.ProductId)).Select(p => new ProductDicountDto
            {
                ProductID = p.ProductId,
                ServiceType = _servicetype.GetAllReadOnly().First(s => s.ServiceTypeId == p.ServiceType).TypeName,
                MainImage = _productimage.GetAllReadOnly().First(img => img.ProductId == p.ProductId && img.Sort == 1).Image,
                SitterName = _registersitter.GetAllReadOnly().First(sit => sit.MemberId == p.SitterId).SitterName,
                ServiceArea = _productservicearea.GetAllReadOnly().Where(area => area.ProductId == p.ProductId).Take(3).Select(area => new CountyandDistrictDto
                {
                    District = _district.GetAllReadOnly().First(d => d.DistrictId == area.District).DistrictName,
                    County = _county.GetAllReadOnly().First(c => c.CountyId == area.County).CountyName
                }).ToList(),
                Quantity = _productdiscount.GetAllReadOnly().First(q => q.ProductId == p.ProductId).Quantity,
                Discount = _productdiscount.GetAllReadOnly().First(q => q.ProductId == p.ProductId).Discount,

            }).ToList();

            return SitterCenterResponseHelper.ReadResponse(products);
        }


        public async Task<ResultDto> CreateOrUpdateSales(DiscountDto request)
        {
            //檢核商品是否已存在促銷方案
            var checksales = _productdiscount.GetAllReadOnly().Any(p => p.ProductId == request.ProductId);
            if (!checksales)
            {
                return await CreateSales(request);
            }
            else
            {
                return await UpdateSales(request);
            }
        }
        //新增促銷(WebApi)
        public async Task<ResultDto> CreateSales(DiscountDto request)
        {
            //檢核商品是否存在
            var response = new ResultDto();
            var checkproduct = _product.GetAllReadOnly().Any(p => p.ProductId == request.ProductId);
            if (!checkproduct)
            {
                response.Message = "Not Found";
                return response;
            }

            //新增資料
            var source = new ProductDiscount
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                Discount = request.Discount
            };

            try
            {
                var res = await (_productdiscount.AddAsync(source));
                return new ResultDto(res);
            }
            catch (Exception ex)
            {
                response.Message = $"新增失敗:{ex.Message}";
                return response;
            }
        }
        //取得促銷(WebApi)
        public ResultDto GetSales(int id)
        {
            var response = new ResultDto();
            var product = _product.GetById(id);
            var productdiscount = _productdiscount.GetAllReadOnly().FirstOrDefault(d => d.ProductId == product.ProductId);

            if (product is null || productdiscount is null)
            {
                response.Message = "Not Found";
                return response;
            }

            var salesdata = new DiscountDto
            {
                ProductId = product.ProductId,
                Quantity = productdiscount.Quantity,
                Discount = productdiscount.Discount
            };

            return new ResultDto(salesdata);
        }
        //更新促銷(WebApi)
        public async Task<ResultDto> UpdateSales(DiscountDto request)
        {
            var source = _productdiscount.GetAll().First(p => p.ProductId == request.ProductId);
            var response = new ResultDto();

            source.ProductId = request.ProductId;
            source.Quantity = request.Quantity;
            source.Discount = request.Discount;

            try
            {
                var res = await (_productdiscount.UpdateAsync(source));
                return new ResultDto(res);  //帶參數建構式isSuccess=true
            }
            catch (Exception ex)
            {
                response.Message = $"儲存失敗:{ex.Message}";
                return response;
            }

        }
        //刪除促銷(WebApi)
        public async Task<ResultDto> DeteleSales(int id)
        {
            var response = new ResultDto();
            var source = _productdiscount.GetAll().First(p => p.ProductId == id);
            try
            {
                await _productdiscount.DeleteAsync(source);
                response.IsSuccess = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"刪除失敗:{ex.Message}";
                return response;
            }

        }

        //促銷Box
        public ResultDto GetProductList()
        {
            //取所有上架中商品
            var onsale = _product.GetAllReadOnly().Where(s => s.SitterId == memberid && s.ProductStatus == (int)ProductStatus.OnSale);
            //取其中已有設定折扣商品
            var hasdiscount = _productdiscount.GetAllReadOnly().Where(d => onsale.Select(on => on.ProductId).Contains(d.ProductId));
            //差集，取得沒有設定折扣的上架中商品id
            var ex = onsale.Select(on => on.ProductId).Except(hasdiscount.Select(has => has.ProductId));

            //先拿出productlist，把district先處理掉
            var products = onsale.Where(p => ex.Contains(p.ProductId));

            var areas = from p in products
                        join area in _productservicearea.GetAllReadOnly() on p.ProductId equals area.ProductId
                        select new ProductAreaNameDto
                        {
                            ProductId = p.ProductId,
                            DistrictName = _district.GetAllReadOnly().First(d => d.DistrictId == area.District).DistrictName,
                            CountyName = _county.GetAllReadOnly().First(c => c.CountyId == area.County).CountyName
                        };

            var productlist = products.Select(result => new ProductListDto
            {
                ProductID = result.ProductId,
                ServiceType = _servicetype.GetAllReadOnly().First(s => s.ServiceTypeId == result.ServiceType).TypeName,
                Introduce = result.Introduce.Substring(0, 30) + "...",
                ProductImage = _productimage.GetAllReadOnly().First(img => img.ProductId == result.ProductId && img.Sort == 1).Image,
                SitterName = _registersitter.GetAllReadOnly().First(sit => sit.MemberId == result.SitterId).SitterName,
                ServiceArea = areas.Where(a => a.ProductId == result.ProductId).Take(2).ToList()
            }).ToList();

            return new ResultDto(productlist);
        }




        #endregion

        #region 廣告
        //廣告列表
        public TransResultDto<List<ProductAdprojectDto>> GetProductAd()
        {
            var response = new TransResultDto<List<ProductAdprojectDto>>();
            var data = new List<ProductAdprojectDto>();
            //取得所有已上架商品
            var products = _product.GetAllReadOnly().Where(p => p.SitterId == memberid && p.ProductStatus==(int)ProductStatus.OnSale).ToList();
            //取得廣告商品
            var ad = _adproject.GetAllReadOnly().Where(a => products.Select(p => p.ProductId).Contains(a.ProductId)).ToList();
            //如果無商品，推薦保姆上架
            if (products==null) 
            {
                response.IsSuccess = true;
                response.Message = "無已上架商品";
                response.Data = data;
                return response;
            }

            //如果有商品，分類是否有廣告，全部列出
            var areas = from p in products
                        join area in _productservicearea.GetAllReadOnly() on p.ProductId equals area.ProductId
                        select new ProductAreaNameDto 
                        {
                            ProductId=p.ProductId,
                            DistrictName= _district.GetAllReadOnly().First(d => d.DistrictId == area.District).DistrictName,
                            CountyName = _county.GetAllReadOnly().First(c => c.CountyId == area.County).CountyName
                        };

            //有廣告的商品
            var adproductid = products.Select(p => p.ProductId).Intersect(ad.Select(a => a.ProductId)).ToList();
            var adproduct = products.Where(p => adproductid.Contains(p.ProductId)).Select(p => new ProductAdprojectDto 
            {
                ProductID= p.ProductId,
                ServiceType = _servicetype.GetAllReadOnly().First(s => s.ServiceTypeId == p.ServiceType).TypeName,
                MainImage = _productimage.GetAllReadOnly().First(img => img.ProductId == p.ProductId && img.Sort == 1).Image,
                SitterName = _registersitter.GetAllReadOnly().First(sit => sit.MemberId == p.SitterId).SitterName,
                BeginDate = ad.First(a=>a.ProductId==p.ProductId).BeginDate,
                EndDate = ad.First(a => a.ProductId == p.ProductId).EndDate,
                ServiceArea= areas.Where(a=>a.ProductId==p.ProductId).Take(2).ToList()
            });
            data.AddRange(adproduct);

            //無廣告的商品
            var unadproductid = products.Select(p => p.ProductId).Except(ad.Select(a => a.ProductId)).ToList();
            var unproduct = products.Select(p => new ProductAdprojectDto 
            {
                ProductID=p.ProductId,
                ServiceType = _servicetype.GetAllReadOnly().First(s => s.ServiceTypeId == p.ServiceType).TypeName,
                MainImage = _productimage.GetAllReadOnly().First(img => img.ProductId == p.ProductId && img.Sort == 1).Image,
                SitterName = _registersitter.GetAllReadOnly().First(sit => sit.MemberId == p.SitterId).SitterName,
                ServiceArea = areas.Where(a => a.ProductId == p.ProductId).Take(2).ToList()
            });
            data.AddRange(unproduct);

           
            return SitterCenterResponseHelper.ReadResponse(data);

        }
        #endregion



        #region 報表
        //報表
        public ResultDto GetChartData(string cycletype)
        {
            //傳入string type
            //傳出dto
            var cycle = ChartCycle(cycletype);

            List<string> label = new List<string>();
            List<int> totalprice = new List<int>();
            List<int> totalordercount = new List<int>();
            List<int> totalcustomer = new List<int>();

            var date = DateTime.UtcNow;
            var day = cycle.Days; //區間總日數
            var diff = cycle.Days / cycle.Degrees; //要分多少段
            for (int start = cycle.Degrees; start >= 1; start--)
            {
                var firstday = date.AddDays(-day);           //起日
                var lastday = date.AddDays(-day + diff + 1); //迄日

                var x = $"{firstday.ToString("yyyy,MM,dd")}起";
                label.Add(x);
                //取得期間所有的訂單金額
                var price = _order.GetAllReadOnly()
                    .Where(o => o.SitterId == memberid && firstday < o.CreateTime && o.CreateTime < lastday)
                    .Select(o => o.Amount * o.Discount).Sum();
                totalprice.Add((int)price);
                //取得期間所有的訂單數量
                var order = _order.GetAllReadOnly()
                    .Where(o => o.SitterId == memberid && firstday < o.CreateTime && o.CreateTime < lastday)
                    .Count();
                totalordercount.Add(order);
                //取得期間所有的客戶數
                var customer = _order.GetAllReadOnly()
                    .Where(o => o.SitterId == memberid && firstday < o.CreateTime && o.CreateTime < lastday)
                    .Select(o => o.CustomerId).Distinct().Count();
                totalcustomer.Add(customer);

                day = day - diff;
            }

            var data = new SitterChartDto
            {
                label = label,
                TotalPrice = totalprice,
                Ordercount = totalordercount,
                Customercount = totalcustomer
            };

            return new ResultDto(data);
        }

        //報表週期
        public Cycle ChartCycle(string key)
        {
            var cycledictionary = new Dictionary<string, Cycle>();
            cycledictionary.Add("week", new Cycle { Days = 7, Degrees = 7 });
            cycledictionary.Add("month", new Cycle { Days = 30, Degrees = 6 });
            cycledictionary.Add("season", new Cycle { Days = 90, Degrees = 6 });

            return cycledictionary[key];
        }
        #endregion


        #region 工具
        //取得縣市名
        public List<CountyDto> GetCounty()
        {
            return _county.GetAllReadOnly().Select(c => new CountyDto { CountyId = c.CountyId, County = c.CountyName }).ToList();
        }
        //取得各區域Dto
        public List<CountyandDistrictDto> GetCountyandDistrict()
        {
            var resource = from d in _district.GetAllReadOnly()
                           join c in _county.GetAllReadOnly()
                           on d.CountyId equals c.CountyId
                           select new CountyandDistrictDto
                           {
                               County = c.CountyName,
                               CountyId = c.CountyId,
                               District = d.DistrictName,
                               DistrictId = d.DistrictId,
                           };
            return resource.ToList();
        }
        #endregion

        

    }
}
