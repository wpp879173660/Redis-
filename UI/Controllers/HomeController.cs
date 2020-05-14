using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using model;
namespace UI.Controllers
{
    public class HomeController : Controller
    {
        SYEntities db = new SYEntities();
        RedisClient redis = new RedisClient("127.0.0.1",6379,"666");
        // GET: Home
        public ActionResult Index()
        {
            bool bl = redis.Set("test", "我是redis测试字段值");

            if (bl) {
                string str = redis.Get<string>("test");
                ViewBag.str = str;
            }
            return View();
        }

        //[OutputCache(Duration = 10)]
        public ActionResult dbhelp() {
            bool bl = redis.ContainsKey("id");
            if (bl)
            {
                redis.Set("aa", "缓存中");
                ViewBag.strg = redis.Get<string>("aa");
            }
            else
            {
                List<a> listaa = db.a.ToList();
                redis.Set("id", listaa);
                //对name键值进行缓存
                redis.Expire("id", 1);
                ViewBag.strg = redis.Get<List<a>>("id");
            }
            ViewBag.str = DateTime.Now;
            
            return View();
        }

        //1000 10 用100合成1瓶药  总为10瓶要 分别喝完 死亡老鼠100瓶药 合成9瓶药 死亡老鼠 9瓶要 依次8瓶分完 剩一瓶 如果都没死 那就是剩下没被喝掉的那一瓶
    }
}