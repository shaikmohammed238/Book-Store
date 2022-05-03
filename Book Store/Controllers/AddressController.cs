using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : Controller
    {

        IAddressBL addressBL;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributCache;
       
        public IConfiguration Configuration { get; }
        public AddressController(IAddressBL addressBL,IMemoryCache memoryCache, IDistributedCache distributCache)
        {
            this.addressBL = addressBL;
            this.memoryCache = memoryCache;
            this.distributCache = distributCache;
            
            

        }
        [HttpPost]
        [Route("addAddress")]
        public IActionResult AddAddress(AddressModel address)
        {
            try
            {
                string result = this.addressBL.AddAddress(address);
                if (result.Equals("Address Added successfully"))
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = result });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
        [HttpPut]
        [Route("updateAddress")]

        public IActionResult UpdateAddress(UpdateAddressModel address)
        {
            try
            {
                string result = this.addressBL.UpdateAddress(address);
                if (result.Equals("Address updated successfully"))
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = result });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        [HttpGet]
        [Route("getAllAddress")]
        public IActionResult GetAllAddresses()
        {
            try
            {
                var result = this.addressBL.GetAllAddresses();
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Retrieval all addresses succssful", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Retrieval is unsucessful" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }


        [HttpGet]
        [Route("getAddressbyUserid/")]
        public IActionResult GetAddressesbyUserid(int ID)
        {
            try
            {
                var result = this.addressBL.GetAddressesbyUserid(ID);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Retrieval all addresses succssful", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "UserId not Exist" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
        [HttpDelete]
        [Route("RemoveAddress/")]
        public IActionResult RemoveAddress(int ID)
        {
            try
            {
                string result = this.addressBL.RemoveAddress(ID);
                if (result.Equals("Address Removed successfully"))
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = result });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
        [HttpGet("Redis")]
        public async Task<IActionResult> GetAllAddressRedisCache()
        {

            var cacheKey = "AllAddres";
            string serializedAllAddres;
            var AllAddres = new List<AddressModel>();
            var redisAllAddres = await distributCache.GetAsync(cacheKey);
            if (redisAllAddres != null)
            {
                serializedAllAddres = Encoding.UTF8.GetString(redisAllAddres);
                AllAddres = JsonConvert.DeserializeObject<List<AddressModel>>(serializedAllAddres);
            }
            else
            {
                AllAddres = (List<AddressModel>)addressBL.GetAllAddresses();
                serializedAllAddres = JsonConvert.SerializeObject(AllAddres);
                redisAllAddres = Encoding.UTF8.GetBytes(serializedAllAddres);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributCache.SetAsync(cacheKey, redisAllAddres, options);
            }
            return Ok(AllAddres);
        }
    }
}
