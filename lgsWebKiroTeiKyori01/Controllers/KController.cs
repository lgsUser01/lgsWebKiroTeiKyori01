using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace lgsWebKiroTeiKyori01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KController : ControllerBase
    {
        ClgsWebKeisan01.ClgsWebKeisan01.lgsWebKeisan KK
           = new ClgsWebKeisan01.ClgsWebKeisan01.lgsWebKeisan();


        [HttpGet]
        public string Get()
        {
           

            KK.ServerComputerName = @"MR8000\LGSTRKSVR";    //【1】 距離計算DBのSQLServer Instance名
            KK.saPassWord = "LgsTrkSvr2";                   //【2】 saのパスワード  


            string Query = this.Request.GetDisplayUrl().ToString();
            


            // ?の位置 20190528
            int Pos = Query.IndexOf("?");
            Query = Query.Substring(Pos, Query.Length - Pos);

            // URL デコードする
            Query = System.Web.HttpUtility.UrlDecode(Query);

            Query = Query.Substring(1, Query.Length - 1);
            var dic = new Dictionary<string, string>();
            var apair = Query.Split('&');
            foreach (string strpair in apair)
            {
                var tpair = strpair.Split('=');
                dic.Add(tpair[0], tpair[1]);
            }


          
            string Kiten = String.Empty;
            try
            {
                Kiten = dic["f"];
            }
            catch
            {

            }

            string Shuuten = String.Empty;
            try
            {
                Shuuten = dic["t"];
            }
            catch
            {

            }



            KK.f = Kiten; // this.textBox起点.Text;
            KK.t = Shuuten; // this.textBox終点.Text;



            string strRet = KK.lgsKyoriKeisan();
            

            return strRet;
        }
    }
}
