using Microsoft.AspNetCore.Mvc;
using RepositoryContractsDb.Contracts;
using RepositoryContractsDb.Models;

namespace CoordinationOfServiceDeliveryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : Controller
    {
        private ITestServiceInterface _test;

        public TestController(ITestServiceInterface test)
        {
            _test = test;
        }

        [HttpGet("get-test-id")]
        public Test Get(int id)
        {
            return _test.Get(id);
        }

        [HttpPost("add")]
        public void Add(Test test)
        {
            _test.Create(test);
        }
    }
}
