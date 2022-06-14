using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace mtg_app.Controllers
{
    [Route("hello")]
    public class HelloController : Controller
    {
        [Route("index")]
        public String Index()
        {
            return "Hello";
        }

        [Route("sayhi")]
        public String SayHi(String name){
            return $"Hi {name}";
        } 
    }
}