using CRUDwebapp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace CRUDwebapp.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly APIGateway apiGateway;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configs;


        public EmployeeController(APIGateway ApiGateway, IHttpClientFactory httpClientFactory, IConfiguration configs)
        {
            this.apiGateway = ApiGateway;
            _httpClient = httpClientFactory.CreateClient();
            _configs = configs;
        }





        public IActionResult Index()
        {
            /*List<Employee> employees = new List<Employee>();*/
            List<Employee> employees;
            //api get will come
            employees = apiGateway.ListEmployees();
            return View(employees);
        }


        [HttpGet]
        public IActionResult Create()
        {
            Employee employee = new Employee();
            return View(employee);
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            apiGateway.CreateEmployee(employee);
            return RedirectToAction("index");
        }


        public IActionResult Details(int Id)
        {
            Employee employee = new Employee();
            employee = apiGateway.GetEmployee(Id);
            return View(employee);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            Employee employee;
            employee = apiGateway.GetEmployee(Id);
            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            apiGateway.UpdateEmployee(employee);
            return RedirectToAction("Index");
        }


/*        [HttpGet]
        public IActionResult Delete(int Id)
        {
            Employee employee;
            employee = apiGateway.GetEmployee(Id);
            return View(employee);
        }*/
        /*
                [HttpPost]
                public IActionResult Delete(Employee employee)
                {
                    apiGateway.DeleteEmployee(employee.Id);
                    return RedirectToAction("Index");
                }



        */



        public async Task<IActionResult> Delete(int id)
        {

            _httpClient.DefaultRequestHeaders.Add("ApiKey", _configs.GetValue<string>("ApiKey"));
            var response = await _httpClient.DeleteAsync($"http://localhost:5088/api/Employee{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                // Process the successful response here
                return Ok(content);
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                // Handle 404 Not Found error
                return NotFound();
            }
            else
            {
                // Handle other error cases
                return StatusCode((int)response.StatusCode);
            }
        }











    }
}
