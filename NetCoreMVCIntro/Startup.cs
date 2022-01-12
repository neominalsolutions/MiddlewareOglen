using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreMVCIntro
{
    // IServiceProvider interface ile uygulama i�erisinde kullan�lan servisler yani uygulaman�n instance almas� gereken servislerin uygulama tan�t�lmas�n� bu interface �zerinden sa�l�yoruz.
    // ISession ile web sunucu �zerinde oturum bilgileri saklan�yor.
    // Web Client Web Server request att��� an itibari ile web server �zerinde o web clienta ait bir session oturum a��l�r ve o request i�in bir sessionID �retilir. Ki�i ayn� browserdan taray�c�dan yapt�kt��� istekler i�in bu SessionID de�i�mez. Taki End User Browser kapat�p uygulama ile ileti�imi kesene kadar.
    // Kullan�c�ya ait hasas bilgiler Cookie �zerinde saklanaca��na daha g�cenli olan ve sunucu taraf�nda saklanan Sessionda tutuluyor. State Management (Durum Y�netimi) ServerSide y�netiminde kullan�lan tekniklerden biriside Session'd�r. 

    // Web Client ile Web Server aras�nda veri payla��m�n� State Management ile yap�yoruz. Serverside tarafta Application yani uygulama bazl� durum y�netimi yapabiliriz. �rn: Aktif ziyaret�i say�s�, Session ile de web client olarak web server'a request atan kullan�c�ya ait oturum bilgilerini, her bir oturum a�an client bazl� tutabiliriz.

    // Client Side tarafta ise Cookie, SessionStorage, LocalStorage, HiddenInput ve QueryString gibi y�ntemler ile durum y�netimi yapabiliriz. Cookies genelde authenticated olan kullan�c�lar�n baz� hassas olmayan bilgilerinin taray�c� tutulup, depolan�p her bir web request de sunucuya iletilmesidir. Bu sayede sunucu oturum a�an hesap hakk�nda bilgi edinmi� olur.


    public delegate Task RequestDelegate(HttpContext context);
    // net core ortam�nda gelen isteklerin execute edilmesini �al��t�r�lmas�n� bu delegate sa�lar. yani gelen istekleri async olarak yakalar, i�erisinde HttpContext ile web uygulamas�na ait t�m nesneleri bar�nd�r�r. HttpContext i�erisinde temelde iki �nemli Nesne bulunmaktad�r. Request di�eri ise Response.  Request Client taraftan web sayfas�na gelen iste�i sunucu (web serverda) yakalamam�z� sa�lar. Response ise web sunucusunun Web Client'a nas�l bir istek d�nd�relece�i ile ilgilenir. Html Response, Json Response, Text Response gibi farkl� tipte Responselar suncuya d�nd�r�lebilir. Session kullan�c�ya ait Sunucu taraf�ndaki oturum bilgilerini HttpContext bar�nd�rabiliriz. Oturum a�an kullan�c�ya ait bilgiler User bilgilerini sunucuda saklayabiliriz. 

    // RequestDelegate Request Sevk edici. Gelen iste�in y�nlendirip bir eylemin �al��mas�n� sa�layan el�i.
    // Delegate methodlar�n ayn� imzada �al��mas�n� sa�lar. Bu sebep ile bir web request geldi�i an itibari ile Async yani Task tipinde sonu� d�nd�rmeyen ve i�erisinde parametre olarak HttpContext bar�nd�ran herhangi bir methodu �al��t�rabilir.


    public class Startup
    {
      

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        public async Task MyFunc(HttpContext context)
        {
            await Task.FromResult("OK");
        }

        public string MyFunc2(HttpContext context)
        {
            return "OK";
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

        
          

            // Middleware ara yaz�l�m. Uygulamaya yap�lan isteklerde iste�in sonlad�rmadan �nce araya girip uygulamaya yeni bir davran��� �al��ma zaman�nda ekleme i�lemi. 

            // run dan sonra bir middleware varsa bu middle next methoduna sahip olmad��� i�in yani sonland�r�c� bir middleware oldu�u i�in ba�ka hi� bir kod �al��t�rmaz.
            app.Run(async (context) =>
            {
                // JS callback benzeri bir yaz�m s�z konusudur.
                // IApplicationBuilder extention yaz�p, RequestDelegate ile bir action tetikleyecek bir mekanizma kurup, bu mekanizma �zerinden bir eylemi yaparak Request pipeline bir �zellik kazand�rm�� bunada Middleware demi�.


                // delegate ile ayn� imza bir method tan�mlay�p, delegate i�i devrettik. delegate de bu methodun �al��mas�n� sa�lad�.
                //var d = new RequestDelegate(MyFunc);
                //await d.Invoke(context); // delegate �zerinden method �al��t�rm�� olduk.


                await context.Response.WriteAsync("Hello World!");

            });

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}
            //app.UseHttpsRedirection();
            //app.UseStaticFiles();

            //app.UseRouting();
            ////app.UseWebSockets();
            ////app.UseAuthentication();
            ////app.UseSession();
            //app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");

            //});
        }
    }

  
}
