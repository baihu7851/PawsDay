using ApplicationCore.Common;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.OpenApi.Writers;
using PawsDay.Interfaces.Account;
using PawsDay.Models.SitterCenter.WebApi;
using PawsDay.Services.ShoppingCart;
using PawsDay.ViewModels.Layout;
using PawsDay.ViewModels.ShoppingCart.Carts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PawsDay.Services.Layout
{
    public class MenuServices
    {
        private readonly IAccountManager _accountManager;
        private readonly CartServices _services;
        private readonly IRepository<Member> _member;
        private readonly IRepository<RegisterSitter> _sitter;
        private readonly IRepository<ApplicationCore.Entities.Product> _product;
        private readonly IRepository<ProductDiscount> _discount;
        private readonly IRepository<ProductImage> _image;
        private readonly IRepository<ServiceType> _servicetype;
        private readonly IRepository<ProductServicePetType> _pettype;
        private readonly IRepository<Schedule> _schedule;
        private readonly IRepository<Cart> _cart;
        private readonly IRepository<CartDetail> _cartdetail;
        private readonly IRepository<CartSchedule> _cartschedule;
        public MenuServices(CartServices services, IRepository<Member> member, IRepository<RegisterSitter> sitter, IAccountManager accountManager, IRepository<ApplicationCore.Entities.Product> product, IRepository<ProductDiscount> discount, IRepository<ProductImage> image, IRepository<ServiceType> servicetype, IRepository<ProductServicePetType> pettype, IRepository<Schedule> schedule, IRepository<Cart> cart, IRepository<CartDetail> cartdetail, IRepository<CartSchedule> cartschedule)
        {
            _accountManager = accountManager;
            _services = services;
            _member = member;
            _sitter = sitter;
            _product = product;
            _discount = discount;
            _image = image;
            _servicetype = servicetype;
            _pettype = pettype;
            _schedule = schedule;
            _cart = cart;
            _cartdetail = cartdetail;
            _cartschedule = cartschedule;
        }
        public ListCartItemViewModel ShoppingCart()
        {
            int memberId = _accountManager.GetLoginMemberId();
            if (memberId == 0)
            {
                return null;
            }
            else
            {
                var cartList = _services.EstablishCartView(memberId);
                return cartList;
            }
        }
        public int GetMemberId()
        {
            int memberId = _accountManager.GetLoginMemberId();
            return memberId;
        }
        public ResultDto NotLoginCartWebApi(List<NotLoginCartDto> carts)
        {
            var response = new ResultDto();
            var schedule = _schedule.GetAllReadOnly().ToList();
            var products = _product.GetAllReadOnly().Where(x => carts.Select(y => y.ProductId).Contains(x.ProductId)).ToList();
            var sitters = _sitter.GetAllReadOnly().ToList();
            var servicetype = _servicetype.GetAllReadOnly().ToList();
            var imgs = _image.GetAllReadOnly().ToList();
            var pets = _pettype.GetAllReadOnly().ToList();
            var discounts = _discount.GetAllReadOnly().ToList();
            try
            {
                var cartList = new List<CartMenuViewModel>();
                foreach (var cart in carts)
                {
                    var product = products.First(p => p.ProductId == cart.ProductId);
                    if (product.ProductStatus == (int)ProductStatus.OffSale || product.IsDelete == true)
                    {
                        continue;
                    }
                    var sitter = sitters.First(s => s.MemberId == product.SitterId);
                    var times = cart.Times.Split(",");
                    var starttime = schedule.First(t => t.ScheduleId == int.Parse(times[0])).TimeDesrcipt.Split("~")[0];
                    var endtime = schedule.First(t => t.ScheduleId == int.Parse(times[times.Length - 1])).TimeDesrcipt.Split("~")[1];
                    var productpet = pets.Where(p => p.ProductId == product.ProductId).ToList();
                    var cartpets = cart.PetTypes.Split(",");
                    var cartitem = new CartMenuViewModel()
                    {
                        ProductId = cart.ProductId,
                        SitterName = sitter.SitterName,
                        ServiceType = servicetype.First(x => x.ServiceTypeId == product.ServiceType).TypeName,
                        Image = imgs.First(img => img.ProductId == product.ProductId && img.Sort == 1).Image,
                        ServiceDate = cart.Date,
                        ServiceTime = $"{starttime}~ {endtime}"
                    };
                    var cartpetList= new List<CartPetDto>();
                    foreach (var cartpet in cartpets)
                    {
                        var pettype = Int32.Parse(cartpet.Split("-")[0]);
                        var shapetype = Int32.Parse(cartpet.Split("-")[1]);
                        var thisprice = productpet.FirstOrDefault(p => p.PetType == pettype && p.ShapeType == shapetype);
                        if(thisprice == null)
                        {
                            continue;
                        }
                        var item = new CartPetDto()
                        {
                            PetTypeId = pettype,
                            PetType = EnumDisplayHelper.GetDisplayName<PetType>((PetType)pettype),
                            ShapeTypeId = shapetype,
                            ShapeType = EnumDisplayHelper.GetDisplayName<ShapeType>((ShapeType)shapetype),
                            Price = thisprice.Price,
                            OvernightPrice = thisprice.OvernightPrice
                        };
                        cartpetList.Add(item);
                    }
                    cartitem.CartPets = cartpetList;
                    var productScheduleIds = times;
                    decimal price = 0;
                    decimal overnightPrice = 0;
                    foreach (var item in cartitem.CartPets)
                    {
                        var target = productpet.Where(x => x.PetType == item.PetTypeId && x.ShapeType == item.ShapeTypeId).First();
                        price += target.Price;
                        overnightPrice += target.OvernightPrice;
                    }
                    var dayTimeQuantity = 0;
                    var nightTimeQuantity = 0;
                    foreach (var time in productScheduleIds)
                    {
                        var parttime = schedule.First(s => s.ScheduleId == int.Parse(time)).PartTimeId;
                        if (parttime == 0)
                        {
                            nightTimeQuantity++;
                        }
                        else
                        {
                            dayTimeQuantity++;
                        }
                    };
                    var TimeQuantity = dayTimeQuantity + nightTimeQuantity;
                    decimal beforeprice = dayTimeQuantity * price + nightTimeQuantity * overnightPrice;
                    decimal totalPrice = 0;
                    var discount = discounts.FirstOrDefault(p => p.ProductId == product.ProductId);
                    if (discount != null && TimeQuantity >= discount.Quantity)
                    {
                        totalPrice = beforeprice * (discount.Discount / 10);
                    }
                    else
                    {
                        totalPrice = beforeprice;
                    }
                    cartitem.TotalPrice = totalPrice.ToString("#,###");
                    cartList.Add(cartitem);
                }

                response.IsSuccess = true;
                response.Data = cartList;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"讀取失敗:{ex.Message}";
                return response;
            }
        }
        public ResultDto ShoppingCartWebApi(int memberId)
        {
            var response = new ResultDto();
            var schedule = _schedule.GetAllReadOnly().ToList();
            var products = _product.GetAllReadOnly().ToList();
            var sitters =_sitter.GetAllReadOnly().ToList();
            var servicetype =_servicetype.GetAllReadOnly().ToList();
            var imgs=_image.GetAllReadOnly().ToList();
            var carts = _cart.GetAllReadOnly().Where(c => c.CustomerId == memberId).ToList();
            var cartschedules=_cartschedule.GetAllReadOnly().ToList();
            var cartpets= _cartdetail.GetAllReadOnly().ToList(); 
            var pets=_pettype.GetAllReadOnly().ToList();
            var discounts = _discount.GetAllReadOnly().ToList();
            try
            {
                var cartList = new List<CartMenuViewModel>();
                foreach (var cart in carts)
                {
                    var product = products.First(p => p.ProductId == cart.ProductId);
                    if (product.ProductStatus== (int)ProductStatus.OffSale || product.IsDelete==true)
                    {
                        continue;
                    }
                    var sitter = sitters.First(s => s.MemberId == product.SitterId);
                    var cartschedule = cartschedules.Where(c => c.CartId == cart.CartId);
                    var starttime = schedule.First(c => c.ScheduleId == cartschedule.First().Schedule).TimeDesrcipt.Split("~")[0];
                    var endtime = schedule.First(c => c.ScheduleId == cartschedule.OrderByDescending(x=>x.Schedule).First().Schedule).TimeDesrcipt.Split("~")[1];
                    var productpet = pets.Where(p => p.ProductId == product.ProductId).ToList();
                    var pettype = cartpets.Where(p => p.CartId == cart.CartId).ToList();
                    var cartitem = new CartMenuViewModel()
                    {
                        ProductId = cart.ProductId,
                        SitterName = sitter.SitterName,
                        ServiceType = servicetype.First(x => x.ServiceTypeId == product.ServiceType).TypeName,
                        Image = imgs.First(img => img.ProductId == product.ProductId && img.Sort == 1).Image,
                        ServiceDate = cartschedule.First().ServiceDate.ToShortDateString(),
                        ServiceTime= $"{starttime}~ {endtime}"
                    };
                    var cartpetList = new List<CartPetDto>();
                    foreach (var pet in pettype)
                    {
                        var thisprice = productpet.FirstOrDefault(p => p.PetType == pet.PetType && p.ShapeType == pet.ShapeType);
                        if (thisprice == null)
                        {
                            continue;
                        }
                        var item = new CartPetDto()
                        {
                            PetTypeId = pet.PetType,
                            PetType = EnumDisplayHelper.GetDisplayName<PetType>((PetType)pet.PetType),
                            ShapeTypeId = pet.ShapeType,
                            ShapeType = EnumDisplayHelper.GetDisplayName<ShapeType>((ShapeType)pet.ShapeType),
                            Price = thisprice.Price,
                            OvernightPrice = thisprice.OvernightPrice
                        };
                        cartpetList.Add(item);
                    }
                    cartitem.CartPets = cartpetList;
                    var productScheduleIds = cartschedule.Select(x => x.Schedule).ToList();
                    decimal price = 0;
                    decimal overnightPrice = 0;
                    foreach (var item in cartitem.CartPets)
                    {
                        var target = productpet.Where(x =>x.PetType == item.PetTypeId && x.ShapeType == item.ShapeTypeId).First();
                        price += target.Price;
                        overnightPrice += target.OvernightPrice;
                    }
                    var dayTimeQuantity = 0;
                    var nightTimeQuantity = 0;
                    foreach (var time in productScheduleIds)
                    {
                        var parttime = schedule.First(s => s.ScheduleId == time).PartTimeId;
                        if (parttime == 0)
                        {
                            nightTimeQuantity++;
                        }
                        else
                        {
                            dayTimeQuantity++;
                        }
                    };
                    var TimeQuantity = dayTimeQuantity + nightTimeQuantity;
                    decimal beforeprice = dayTimeQuantity * price + nightTimeQuantity * overnightPrice;
                    decimal totalPrice=0;
                    var discount = discounts.FirstOrDefault(p=>p.ProductId==product.ProductId);
                    if (discount!=null && TimeQuantity >= discount.Quantity)
                    {
                        totalPrice = beforeprice * (discount.Discount / 10);
                    }
                    else
                    {
                        totalPrice = beforeprice;
                    }
                    cartitem.TotalPrice = totalPrice.ToString("#,###");
                    cartList.Add(cartitem);
                }
                
                response.IsSuccess = true;
                response.Data = cartList;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"讀取失敗:{ex.Message}";
                return response;
            }
        }
        public UserMenuViewModel MemberMenu()
        {
            int memberId = _accountManager.GetLoginMemberId();
            if (memberId == 0)
            {
                return null;
            }
            else
            {
                var member = _member.GetAllReadOnly().First(c => c.MemberId == memberId);
                var sitter = _sitter.GetAllReadOnly().FirstOrDefault(x => x.MemberId == memberId);
                var memberMenu = new UserMenuViewModel
                {
                    Name = member.Name,
                    Image = member.ProfileImage ?? "/images/paw.png",
                    IsSitter = false,
                    SitterStatus = 0
                };
                if (sitter != null)
                {
                    memberMenu.IsSitter = true;
                    memberMenu.SitterStatus = sitter.RegisterStatus;
                }

                return memberMenu;
            }
        }
        public UserMenuViewModel SitterMenu()
        {
            int memberId = _accountManager.GetLoginMemberId();
            var member = _member.GetAllReadOnly().First(c => c.MemberId == memberId);
            var sitter = _sitter.GetAllReadOnly().First(s => s.MemberId == memberId);
            var sitterMenu = new UserMenuViewModel
            {
                Name = sitter.SitterName,
                Image = sitter.SitterPicture ?? member.ProfileImage
            };
            return sitterMenu;
        }
    }
}

