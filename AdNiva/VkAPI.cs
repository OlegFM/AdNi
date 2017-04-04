using System;
using System.Collections.Generic;
using System.Linq;
using xNet;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Controls;
using System.Data;

namespace AdNiva
{
    /// <summary>
    /// Класс, обеспечивающий корректный парсинг сообщений об ошибке.
    /// </summary>
    public class Error
    {
        public int error_code { get; set; }
        public string error_msg { get; set; }
        public List<ErrorRequestParams> request_params { get; set; }
    } 
    /// <summary>
    /// Вспомогательные функции
    /// </summary>
    public class Helper
    {
        public Int32 GetUnixTime(string Date, string hour, string min)
        {
            char delimiter = '/';
            char[] Trim = new char[] { delimiter };
            string d = Date.Substring(Date.IndexOf(delimiter), Date.LastIndexOf(delimiter));
            string mo = Date.Substring(0, Date.IndexOf(delimiter));
            string y = Date.Substring(Date.LastIndexOf(delimiter));
            int day = Int32.Parse(d.Trim(Trim));
            int month = Int32.Parse(mo.Trim(Trim));
            int year = Int32.Parse(y.Trim(Trim));
            int h = Int32.Parse(hour);
            int m = Int32.Parse(min);
            Int32 UnixTime = (Int32)(new DateTime(year, month, day, h, m, 0).Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
            return UnixTime;
        }
        public HttpRequest CreateHttpRequest(string cabID, string APIV, string accessToken)
        {
            HttpRequest Request = new HttpRequest();
            Request.AddUrlParam("account_id", cabID);
            Request.AddUrlParam("access_token", accessToken);
            Request.AddUrlParam("v", APIV);
            return Request;
        }
    }
    public class ErrorRequestParams
    {
        public string key { get; set; }
        public string value { get; set; }
    }
  
    // Классы, обеспечивающие корректное составление запросов к серверу
    // в случаях, когда необходимо посылать сериализованый объект.
    // Название класса в этом блоке состоит из названия класса,
    // ипользуемого для обращения к серверу, и добавки Data.
 
   /// <summary>
   /// Для сериализации данных к методу CreateCampaing
   /// </summary>
    
    public class CreateCampaingResponse
    {
        public List<CreateCampaingResponseBody> response { get; set; }
        public Error error { get; set; }
    }

    public class CreateCampaingResponseBody
    {
        public string id { get; set; }
    }
    /// <summary>
    /// Сериализованные данные от метода GetCampaings
    /// </summary>
    public class GetCampaingResponse
    {
        public List<GetCampaingResponseBody> response { get; set; }
        public Error error { get; set; }
    }
    public class GetCampaingResponseBody
    {
            public int id { get; set; }
            public string type { get; set; }
            public string name { get; set; }
            public int status { get; set; }
            public string day_limit { get; set; }
            public string all_limit { get; set; }
            public string start_time { get; set; }
            public string stop_time { get; set; }
            public string create_time { get; set; }
            public string update_time { get; set; }
    }

    public class CreateCampaignData
    {
        public string type { get; set; }
        public string name { get; set; }
        public string day_limit { get; set; }
        public string all_limit { get; set; }
        public string start_time { get; set; } //unix-stamp
        public string stop_time { get; set; } //unix-stamp
        public bool status { get; set; }
    }
    public class CreateCampaignDataValidate: IDataErrorInfo
    {
        public string start_time_hour { get; set; }
        public string start_time_minute { get; set; }
        public string stop_time_hour { get; set; }
        public string stop_time_minute { get; set; }
        public string name { get; set; } = "";
        public string day_limit { get; set; }
        public string all_limit { get; set; }
        public bool[] Val = new bool[7];
        public string this[string columnName]     
        {
            get
            {
                string error = String.Empty;
                
                switch (columnName)
                {
                    case "start_time_hour":
                        {
                            int start_time_h=0;
                            if (Int32.TryParse(start_time_hour, out start_time_h))
                            {
                                if (start_time_h < 0 || start_time_h > 23)
                                {
                                    error = "Должно быть целое число от 0 до 23";
                                    Val[0] = false;
                                }
                                else
                                {
                                    Val[0] = true;
                                    
                                }
                            }
                            else
                            {
                                Val[0] = false;
                                error = "Должно быть положительное чило";
                            }
                            break;
                        }
                    case "start_time_minute":
                        {
                            int start_time_m=0;
                            if (Int32.TryParse(start_time_minute, out start_time_m))
                            {
                                if (start_time_m < 0 || start_time_m > 59)
                                {
                                    error = "Должно быть целое число от 0 до 59";
                                    Val[1] = false;
                                }
                                else
                                {
                                    Val[1] = true;
                                }
                            }
                            else
                            {
                                Val[1] = false;
                                error = "Должно быть положительное чило";
                            }
                            break;
                        }
                    case "stop_time_hour":
                        {
                            int stop_time_h = 0;
                            if (Int32.TryParse(stop_time_hour, out stop_time_h))
                            {
                                if (stop_time_h < 0 || stop_time_h > 23)
                                {
                                    error = "Должно быть целое число от 0 до 23";
                                    Val[2] = false;
                                }
                                else
                                {
                                    Val[2] = true;
                                }
                            }
                            else
                            {
                                Val[2] = false;
                                error = "Должно быть положительное чило";
                            }
                            break;
                        }
                    case "stop_time_minute":
                        {
                            int stop_time_m = 0;
                            if (Int32.TryParse(stop_time_minute, out stop_time_m))
                            {
                                if (stop_time_m < 0 || stop_time_m > 59)
                                {
                                    error = "Должно быть целое число от 0 до 59";
                                    Val[3] = false;
                                }
                                else
                                {
                                    Val[3] = true;
                                }
                            }
                            else
                            {
                                Val[3] = false;
                                error = "Должно быть положительное чило";
                            }
                            break;
                        }
                    case "name":
                        {
                                if (name.Length < 3 || name.Length > 60)
                                {
                                    error = "Длина имени должна быть от 3 до 60 символов";
                                    Val[4] = false;
                                }
                                else
                                {
                                    Val[4] = true;
                            }
                            break;
                        }
                    case "day_limit":
                        {
                            double day_l = 0;
                            if (Double.TryParse(day_limit, out day_l))
                            {
                                if (day_l < 0)
                                {
                                    error = "Должно быть положительное чило";
                                    Val[5] = false;
                                }
                                else
                                {
                                    Val[5] = true;
                                }
                            }
                            else
                            {
                                Val[5] = false;
                                error = "Должно быть положительное чило";
                            }
                            break;
                        }
                    case "all_limit":
                        {
                            Double all_l = 0;
                            if (Double.TryParse(all_limit, out all_l))
                            {
                                if (all_l < 0)
                                {
                                    error = "Должно быть положительное чило";
                                    Val[6] = false;
                                }
                                else
                                {
                                    Val[6] = true;
                                }
                            }
                            else
                            {
                                Val[6] = true;
                                error = "Должно быть положительное чило";
                            }
                            break;
                        }
                        
                }
                return error;
            }
        }

            public string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
    /// <summary>
    /// Сам класс взаимодействия с сервером ВКонтакте.
    /// Названия методов аналогичны названиям методов VK_API.
    /// </summary>
    class VkAPI
    {
        private const string __APPID = "5947030"; //id приложения
        private const string __VKAPIURL = "https://api.vk.com/method/"; //Ссылка для запросов
        private string _Token;  //Токен, использующийся при запросах
        private const string __API_VERSION = "5.63"; //Версия API ВКонтакте
        private string _CABID; //ID кабинета

        public VkAPI(string AuthToken)
        {
            _Token = AuthToken;
            _CABID = Properties.Settings.Default.AdCabinetID;
        }
        /// <summary>
        /// Возвращает ссылку на фото, имя, фамилию.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetUserInfo ()
        {
            Dictionary<string, string> Dict = new Dictionary<string, string>();
            HttpRequest GetUserInfo = new HttpRequest();
            GetUserInfo.AddUrlParam("user_ids", Properties.Settings.Default.UserID);
            string Params = "photo_100,first_name,last_name,";
            GetUserInfo.AddUrlParam("fields", Params);
            GetUserInfo.AddUrlParam("v", __API_VERSION);
            string Result = GetUserInfo.Get(__VKAPIURL + "users.get").ToString();
            Result = Result.Substring(13, Result.Length - 15);
            Dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(Result);
            return Dict;
        }
        /// <summary>
        /// Получает баланс кабинета.
        /// </summary>
        /// <returns></returns>
        public double GetBudget()
        {
            double Budget = -200.00;
            Dictionary<string, string> SBudget = new Dictionary<string, string>();
            HttpRequest GetBudget = new HttpRequest();
            GetBudget.AddUrlParam("account_id", _CABID);
            GetBudget.AddUrlParam("access_token", _Token);
            GetBudget.AddUrlParam("v", __API_VERSION);
            string Result = GetBudget.Get(__VKAPIURL + "ads.getBudget").ToString();
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            if (Result.Contains("response"))
            {
                SBudget = JsonConvert.DeserializeObject<Dictionary<string, string>>(Result);
                string x = SBudget["response"];
                Budget = Convert.ToDouble(x);
            }
            return Budget;
        }
        /// <summary>
        /// Создаёт рекламную кампанию.
        /// </summary>
        /// <returns></returns>
        public CreateCampaingResponse CreateCampaign(CreateCampaignData[] Cdata)
        {
            string data;
            HttpRequest Request = new HttpRequest();
            Request.AddUrlParam("account_id", _CABID);
            data = JsonConvert.SerializeObject(Cdata);
            Request.AddUrlParam("data", data);
            Request.AddUrlParam("access_token", _Token);
            Request.AddUrlParam("v", __API_VERSION);
            string json = Request.Get(__VKAPIURL + "ads.createCampaigns").ToString();
            CreateCampaingResponse campaings = JsonConvert.DeserializeObject<CreateCampaingResponse>(json);
            return campaings;
        }
        public GetCampaingResponse GetCampaings()
        {
            HttpRequest Request = new Helper().CreateHttpRequest(_CABID, __API_VERSION, _Token);
            string json = Request.Get(__VKAPIURL + "ads.getCampaigns").ToString();
            GetCampaingResponse response = JsonConvert.DeserializeObject<GetCampaingResponse>(json);
            return response;
        }
    }
}
