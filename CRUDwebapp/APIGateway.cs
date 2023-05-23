using CRUDwebapp.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace CRUDwebapp
{
    public class APIGateway
    {


        private string url = "http://localhost:5088/api/Employee";
        private HttpClient httpClient = new HttpClient();
        private readonly IConfiguration _configs;

        public APIGateway(IConfiguration configs)
        {
            _configs = configs;
        }

        public List<Employee> ListEmployees()
        {
            httpClient.DefaultRequestHeaders.Add("ApiKey", _configs.GetValue<string>("ApiKey"));
            List<Employee> employees = new List<Employee>();
            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var datacol = JsonConvert.DeserializeObject<List<Employee>>(result);
                    if (datacol != null)
                        employees = datacol;
                }

                else
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception("Error Occured at the API endpoint, Error Info. " + result);
                }
            }

            catch (Exception ex)
            {
                throw new Exception("Error Occured at the API endpoint, Error Info. " + ex.Message);
            }

            finally { }
            return employees;

        }

        public Employee CreateEmployee(Employee employee)
        {
            httpClient.DefaultRequestHeaders.Add("ApiKey", "APIKEY");
            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string json = JsonConvert.SerializeObject(employee);
            try
            {
                HttpResponseMessage response = httpClient.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json")).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<Employee>(result);
                    if (data != null)
                        employee = data;
                }

                else
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception("Error Occured at the API Endpoint, Error info. " + result);
                }
            }

            catch (Exception ex)
            {
                throw new Exception("Error Occured at the API Endpoint, Error info. " + ex.Message);
            }
            finally { }
            return employee;

        }


        public Employee GetEmployee(int id)
        {
            httpClient.DefaultRequestHeaders.Add("ApiKey", _configs.GetValue<string>("ApiKey"));
            Employee employee = new Employee();
            url = url + "/" + id;
            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            try
            {
                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<Employee>(result);
                    if (data != null)
                        employee = data;
                }

                else
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception("Error Occured at the API Endpoint, Error info. " + result);
                }
            }

            catch (Exception ex)
            {
                throw new Exception("Error Occured at the API Endpoint, Error info. " + ex.Message);
            }
            finally { }
            return employee;
        }













        public void UpdateEmployee(Employee employee)
        {
            httpClient.DefaultRequestHeaders.Add("ApiKey", _configs.GetValue<string>("ApiKey"));
            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            int id = employee.Id;

            url = url + "/" + id;
            string json = JsonConvert.SerializeObject(employee);

            try
            {
                HttpResponseMessage response = httpClient.PutAsync(url, new StringContent(json, Encoding.UTF8, "application/json")).Result;
                if (!response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception("Error Occured at the API Endpoint, Error info. " + result);
                }
            }

            catch (Exception ex)
            {
                throw new Exception("Error Occured at the API Endpoint, Error info. " + ex.Message);
            }
            finally { }
            return;
        }




/*        public void DeleteEmployee(int id)
        {
            httpClient.DefaultRequestHeaders.Add("ApiKey", _configs.GetValue<string>("ApiKey"));
            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            url = url + "/" + id;

            try
            {
                HttpResponseMessage response = httpClient.DeleteAsync(url).Result;
                if (!response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception("Error Occured at the API Endpoint, Error info. " + result);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error Occured at the API Endpoint, Error info. " + ex.Message);
            }
            finally { }
            return;

        }*/









    }
}
