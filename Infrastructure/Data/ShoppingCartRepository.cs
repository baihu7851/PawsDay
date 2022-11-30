using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using CloudinaryDotNet.Actions;
using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Infrastructure.Data
{
    public class ShoppingCartRepository
    {
        private readonly PawsDayContext _pawsDayContext;

        private readonly IRepository<Cart> _cartRepos;
        private readonly IRepository<CartDetail> _cartDetailRepos;
        private readonly IRepository<CartSchedule> _cartScheduleRepos;
        private readonly IRepository<County> _cityRepos;
        private readonly IRepository<District> _distRepos;


        public ShoppingCartRepository(PawsDayContext pawsDayContext, IRepository<Cart> cartRepos, IRepository<CartDetail> cartDetailRepos, IRepository<CartSchedule> cartScheduleRepos, IRepository<County> countryRepos, IRepository<District> distRepos)
        {
            _pawsDayContext = pawsDayContext;
            _cartRepos = cartRepos;
            _cartDetailRepos = cartDetailRepos;
            _cartScheduleRepos = cartScheduleRepos;
            _cityRepos = countryRepos;
            _distRepos = distRepos;
        }

        public void SavingCookieToCartDB(int userId ,List<SaveCookieCartDTO> source)
        {
            var cities = _cityRepos.GetAllReadOnly();
            var dists = _distRepos.GetAllReadOnly();

            using (var trans = _pawsDayContext.Database.BeginTransaction())
            {
                try
                {
                    if (source.Count == 0)
                    {
                        throw new Exception();
                    }
                    foreach (var cartItem in source)
                    {
                        //處理城市
                        var city = cities.Where(x => x.CountyName == cartItem.County).SingleOrDefault().CountyId;
                        //處理區域
                        var dist = dists.Where(x => x.DistrictName == cartItem.District && x.CountyId == city).SingleOrDefault().DistrictId;


                        var cart = new Cart()
                        {
                            ProductId = Int32.Parse(cartItem.Id),
                            CustomerId = userId,
                            CreateTime = DateTime.UtcNow.AddHours(8),
                            County = city,
                            District = dist,
                        };

                        _cartRepos.Add(cart);
                        //_pawsDayContext.SaveChanges();

                        string Types = cartItem.ShapeTypes;
                        var typesArr = Types.Split(",").ToList();
                        foreach (var type in typesArr)
                        {
                            var arr = type.Split("-");
                            int petType = Int32.Parse(arr[0]);
                            int shapeType = Int32.Parse(arr[1]);

                            var cartDetail = new CartDetail()
                            {
                                CartId = cart.CartId,
                                PetType = petType,
                                ShapeType = shapeType,
                            };

                            _cartDetailRepos.Add(cartDetail);


                        }

                        var date = DateTime.Parse(cartItem.Day); // 2022/10/26
                        

                        var times = cartItem.Time;
                        var timesArr = times.Split(",").ToList();
                        foreach (var t in timesArr)
                        {
                            int scheduleId = Int32.Parse(t);
                            //處理日期
                            var cartSchedule = new CartSchedule()
                            {
                                CartId = cart.CartId,
                                ServiceDate = date,
                                Schedule = scheduleId,
                            };

                            _cartScheduleRepos.Add(cartSchedule);   


                        }
                    }
                    _pawsDayContext.SaveChanges();
                    trans.Commit();
                }
                catch(Exception err)
                {
                    string msg = err.ToString();
                    trans.Rollback();
                    _pawsDayContext.SaveChanges();
                    Console.WriteLine($"錯誤訊息：{msg}");
                }


                



            }
                    

            



        }


        public void DeleteCompleteCart(IEnumerable<string> cartIds)
        {
            var carts = _cartRepos.GetAll();
            var cartSchedules = _cartScheduleRepos.GetAll();
            var cartDetails = _cartDetailRepos.GetAll();


            foreach (var cartId in cartIds)
            {
                var intCartId = int.Parse(cartId);
                //delete cart資料庫相關
                using (var trans = _pawsDayContext.Database.BeginTransaction())
                {
                    try
                    {
                        var targetCart = carts.Where(c => c.CartId == intCartId).SingleOrDefault();
                        if (targetCart is null)
                        {
                            throw new ArgumentNullException();
                        }

                        var targetCartDetail = cartDetails.Where(c => c.CartId == intCartId).ToList();
                        var targetCartSchedule = cartSchedules.Where(c => c.CartId == intCartId).ToList();
                        _cartDetailRepos.DeleteRange(targetCartDetail);
                        _cartScheduleRepos.DeleteRange(targetCartSchedule);
                        _cartRepos.Delete(targetCart);

                        trans.Commit();
                        _pawsDayContext.SaveChanges();

                    }
                    catch (Exception err)
                    {
                        Console.WriteLine($"發生錯誤：{err.ToString()}");
                        trans.Rollback();

                        _pawsDayContext.SaveChanges();


                    }
                }

            }
        }

    }
}
