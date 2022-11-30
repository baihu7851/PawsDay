using isRock.LineBot;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.Extensions.Options;
using PawsDay.Services.LineBot;
using Microsoft.Extensions.Configuration;

namespace PawsDay.WebApi.LineBot
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LineWebHookController : LineWebHookControllerBase
    {
        private readonly LineBotSearchService _searchservice;
        private readonly LineBotService _service;
        private readonly IConfiguration _configuration;

        public LineWebHookController(LineBotSearchService searchservice, LineBotService service,IConfiguration configuration)
        {
            _searchservice = searchservice;
            _service = service;
            _configuration = configuration;
        }

       
        [HttpPost]
        public IActionResult LineWebHookApi()
        {

            var AdminUserId = _configuration.GetSection("LineBot:AdminUserId").Value;
            var qaEndpoint = _configuration.GetSection("Azure-Lauguage:Endpoint").Value;
            var qaKey = _configuration.GetSection("Azure-Lauguage:Ocp-Apim-Subscription-Key").Value;

            try
            {
                //設定ChannelAccessToken
                this.ChannelAccessToken = _configuration.GetSection("LineBot:ChannelAccessToken").Value; 
                //配合Line Verify
                if (ReceivedMessage.events == null || ReceivedMessage.events.Count() <= 0 ||
                    ReceivedMessage.events.FirstOrDefault().replyToken == "00000000000000000000000000000000") return Ok();
                //取得Line Event
                var LineEvent = this.ReceivedMessage.events.FirstOrDefault();
                var responseMsg = "";
                var btnMsg = new ButtonsTemplate();
                var action = "";
                var postbackdata = "";
                
                //使用者資訊
                var usernumber = LineEvent.source.userId;
                
                //準備回覆訊息
                var userid=LineEvent.source.userId;

                if (LineEvent.type.ToLower() == "message")
                {
                    if (LineEvent.message.type == "text")
                    {
                        //先查詢是否有歷史紀錄
                        var history = _service.CheckHistory(userid);
                        if (history == "search")
                        {
                            //開始執行搜尋模式
                            var template = CreateProductCarouselTemplate(LineEvent.message.text);
                            this.ReplyMessage(LineEvent.replyToken, template);
                            return Ok();
                        }
                        else if (history == "question")
                        {
                            //開始執行發問模式
                            var helper = new LineBotQnAService(new Uri(qaEndpoint), qaKey);
                            var res = helper.GetAnswer(LineEvent.message.text);
                            responseMsg = res.answers[0].answer;
                            this.ReplyMessage(LineEvent.replyToken, responseMsg);
                            return Ok();
                        }
                        //判斷keyword
                        action = _service.SearchForKeyWord(LineEvent.message.text);
                    }
                    else if (LineEvent.message.type =="sticker")
                    {
                        this.ReplyMessage(LineEvent.replyToken, 11539, 52114116);
                    }
                }
                
                //處理postback
                if (LineEvent.postback != null)
                {
                    postbackdata = LineEvent.postback.data.ToLower();
                }


               

                if (postbackdata == "search" || action == "search")
                {
                    responseMsg = BeginSearch();
                    //如果輸入搜尋就存進歷史訊息
                    _service.CreateHistory(usernumber, "search");
                    this.ReplyMessage(LineEvent.replyToken, responseMsg);
                }
                else if(postbackdata == "question" || action == "question")
                {
                    responseMsg = BeginQuestion();
                    //如果輸入發問就開始連接到azure服務
                    _service.CreateHistory(usernumber, "question");
                    this.ReplyMessage(LineEvent.replyToken, responseMsg);
                }
                else if (postbackdata == "service" || action == "service")
                {
                    btnMsg = ServiceTypeTemplate();
                    this.ReplyMessage(LineEvent.replyToken, btnMsg);
                }
                else if (postbackdata == "process" || action == "process")
                {
                    btnMsg = ServiceProcessTemplate();
                    this.ReplyMessage(LineEvent.replyToken, btnMsg);
                }
                else if (postbackdata == "custom" || action == "custom")
                {
                    btnMsg = CustomTemplate();
                    this.ReplyMessage(LineEvent.replyToken, btnMsg);
                }
                else if (postbackdata == "new" || action == "new")
                {
                    btnMsg = NewsTemplate();
                    this.ReplyMessage(LineEvent.replyToken, btnMsg);
                }
                else if (postbackdata == "survey" || action == "survey")
                {
                    btnMsg = SurveyTemplate();
                    this.ReplyMessage(LineEvent.replyToken, btnMsg);
                }
                else if (postbackdata == "website" || action == "website")
                {
                    btnMsg = OfficialTemplate();
                    this.ReplyMessage(LineEvent.replyToken, btnMsg);
                }
                else
                {
                    responseMsg = "不太理解您的意思，請嘗試其他輸入方式";
                    this.ReplyMessage(LineEvent.replyToken, responseMsg);
                }

                //response OK
                return Ok();
            }
            catch (Exception ex)
            {
                //回覆訊息
                this.PushMessage(AdminUserId, "發生錯誤:\n" + ex.Message);
                //response OK
                return Ok();
            }
        }
       

        //查詢引導字串
        private string BeginSearch()
        {
            return "請輸入要查詢的保姆名稱";
        }

        //發問引導字串
        private string BeginQuestion()
        {
            return "請輸入問題或是關鍵字";
        }

        //客製功能(用後台來傳資料)
        private ButtonsTemplate CustomTemplate()
        {
            var template = _service.GetTemplate((int)TemplateType.custom);
            var detail = template.Detail;

            var action = new List<TemplateActionBase>();
            foreach (var d in detail)
            {
                if (d.Type == "message")
                {
                    action.Add(new MessageAction()
                    { label = d.DetailText, text = d.DetailText });
                }
                else
                {
                    action.Add(new UriAction()
                    { label = d.DetailText, uri = new Uri($"https://{d.Url.Split("//")[1]}") });
                }
            }
            var ButtonTemplate = new ButtonsTemplate()
            {
                altText = template.Title,
                title = template.Title,
                text = template.Text,
                thumbnailImageUrl = new Uri($"https://{template.Image.Split("//")[1]}"),
                actions = action
            };

            return ButtonTemplate;
        }

        //推播廣告(用後台來傳資料)
        private ButtonsTemplate NewsTemplate()
        {
            var newtemplate = _service.GetTemplate((int)TemplateType.news);
            var detail = newtemplate.Detail.First();

            var ButtonTemplate = new ButtonsTemplate()
            {
                altText = newtemplate.Title,
                title = newtemplate.Title,
                text = newtemplate.Text,
                thumbnailImageUrl = new Uri($"https://{newtemplate.Image.Split("//")[1]}"),
                actions = new List<TemplateActionBase>
                {
                    new UriAction
                    { 
                        label=detail.DetailText,
                        uri=new Uri($"https://{detail.Url.Split("//")[1]}")
                    }
                }
            };
            return ButtonTemplate;
        }

        //調查(用後台來傳資料)
        private ButtonsTemplate SurveyTemplate()
        {
            var template = _service.GetTemplate((int)TemplateType.survey);
            var detail = template.Detail;

            var action = new List<TemplateActionBase>();
            foreach (var d in detail)
            {
                action.Add(new MessageAction()
                { label = d.DetailText, text = d.DetailText });
            }
            var ButtonTemplate = new ButtonsTemplate()
            {
                altText = template.Title,
                title = template.Title,
                text = template.Text,
                thumbnailImageUrl = new Uri($"https://{template.Image.Split("//")[1]}"),
                actions = action
            };

            return ButtonTemplate;
        }

        //服務項目
        private ButtonsTemplate ServiceTypeTemplate()
        {
            var actions = new List<TemplateActionBase>();
            actions.Add(new UriAction()
            { label = "到府照顧", uri = new Uri("https://pawsday-frontend.azurewebsites.net/ServiceType/Homecare") });
            actions.Add(new UriAction()
            { label = "到府洗澡", uri = new Uri("https://pawsday-frontend.azurewebsites.net/ServiceType/Homesalon") });
            actions.Add(new UriAction()
            { label = "陪伴散步", uri = new Uri("https://pawsday-frontend.azurewebsites.net/ServiceType/Walking") });

            var ButtonTemplate = new ButtonsTemplate()
            {
                altText = "PawsDay三大服務項目",
                text = "到府照顧、到府洗澡、陪伴散步",
                title = "服務項目介紹",
                thumbnailImageUrl = new Uri("https://res.cloudinary.com/dnsu1sjml/image/upload/v1666464580/samoyed-dog-in-home-interior-picture-id648916670_bjto7q.jpg"),
                actions = actions 
            };
            return ButtonTemplate;
        }

        //預約流程
        private ButtonsTemplate ServiceProcessTemplate()
        {
            var ButtonTemplate = new ButtonsTemplate()
            {
                altText = "PawsDay三大服務項目",
                text = "到府照顧、到府洗澡、陪伴散步",
                title = "服務項目介紹",
                thumbnailImageUrl = new Uri("https://res.cloudinary.com/dnsu1sjml/image/upload/v1666452703/krista-mangulsone-9gz3wfHr65U-unsplash_wq1dzc.jpg"),
                actions = new List<TemplateActionBase> 
                {
                    new UriAction{ label="預約流程",uri=new Uri("https://pawsday-frontend.azurewebsites.net/StaticWeb/ServiceProcess")}
                } 
            };
            return ButtonTemplate;
        }

        //服務項目
        private ButtonsTemplate OfficialTemplate()
        {
            var ButtonTemplate = new ButtonsTemplate()
            {
                altText = "PawsDay官方網站",
                text = "全世界最棒的寵物保姆",
                title = "PawsDay官方網站",
                thumbnailImageUrl = new Uri("https://res.cloudinary.com/dnsu1sjml/image/upload/v1666251513/mm6kzcsqjg3zqchazw5h.jpg"),
                actions = new List<TemplateActionBase>
                {
                    new UriAction{ label="前往",uri=new Uri("https://pawsday-frontend.azurewebsites.net")}
                }
            };
            return ButtonTemplate;
        }

        //搜尋回傳商品卡
        private CarouselTemplate CreateProductCarouselTemplate(string input)
        {
            var result = _searchservice.LineBotSearch(input);

            var carousel = new CarouselTemplate()
            {
                altText = "搜尋結果",
                columns = result.Select(r =>
                new Column 
                {
                    title =$"{r.SitterName} | {r.ServiceType}",
                    text=$"{r.Price}元起",
                    thumbnailImageUrl=new Uri(r.Image),
                    actions= new List<TemplateActionBase>() { new UriAction() 
                    {
                        label="前往商品",
                        uri = new Uri($"https://pawsday-frontend.azurewebsites.net/Product/{r.ProductId}")
                    } }
                }
                ).ToList()
            };
            return carousel;
        }

        private enum TemplateType
        {
            news=1,
            custom=2,
            survey=4
        }
    }
}
