using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;


namespace EntryPoint
{
    public class EntryController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Post()
        {
            Task<string> aa = this.Request.Content.ReadAsStringAsync();
            aa.Wait();
            return new HttpResponseMessage()
            {
                StatusCode =System.Net.HttpStatusCode.OK,
                Content = new StringContent(aa.Result)
            };

        }
        public HttpResponseMessage Get()
        {
            Task<string> aa = this.Request.Content.ReadAsStringAsync();
            aa.Wait();
            return new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent("HI")
            };
        }
    }
}
