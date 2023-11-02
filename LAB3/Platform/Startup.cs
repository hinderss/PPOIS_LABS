using Microsoft.AspNetCore.Authentication.Cookies;
using Platform.Classes;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Text.Json;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class Startup
{
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration)
    {
        this.Configuration = configuration;
    }

    Authentication authentication;
    Resources resources;
    CoursesDBHandler coursesDB;
    UsersDBHandler usersDB;
    Payment payment;
    IApplicationBuilder app;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options => options.LoginPath = "/login");
        services.AddAuthorization();
        services.AddRazorPages();
        services.AddControllersWithViews();
        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        this.app = app;
        string? connectionString = Configuration.GetConnectionString("DefaultConnection");
        coursesDB = new CoursesDBHandler(connectionString);
        usersDB = new UsersDBHandler(connectionString);

        string? resourcesPath = Configuration["ResourcesPath"];
        resources = new Resources(resourcesPath);

        string? smtpServer = Configuration["EmailSettings:SMTPServer"];
        int smtpPort = Configuration.GetValue<int>("EmailSettings:SMTPPort");
        string? senderEmail = Configuration["EmailSettings:SenderEmail"];
        string? senderPassword = Configuration["EmailSettings:SenderPassword"];
        var emailNewsletter = new EmailNewsletter(smtpServer, smtpPort, senderEmail, senderPassword, usersDB);

        string? invoice = Configuration["PaymentSettings:Invoice"];
        payment = new Payment(invoice);

        authentication = new Authentication(usersDB);

        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        RegisterMaps();
    }

    private void RegisterMaps()
    {
        IndexMapGet();
        AboutMapGet();
        LoginMapGet();
        LoginMapPost();
        RegisterMapGet();
        RegisterMapPost();
        LogoutMapPost();
        CabinetMapGet();
        ListCoursesMapGet();
        GetCourseMapGet();
        CheckLoginStatusMapGet();
        AlreadyPurchasedMapGet();
        GetCourseToBuyMapGet();
        BuyCourseMapGet();
        CourseMapGet();
        GetAllCoursesMapGet();
        GetTagCoursesMapGet();
        GetUserCoursesMapGet();
        SubscribeMapPost();
        PaymentMapGet();
        PaymentRequestMapPost();
    }

    private void IndexMapGet()
    {
        app.UseEndpoints(endpoints =>
            endpoints.MapGet("/", async (HttpContext context) =>
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.SendFileAsync("html/index.html");
            }
        ));
    }

    private void AboutMapGet()
    {
        app.UseEndpoints(endpoints =>
            endpoints.MapGet("/about", async (HttpContext context) =>
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.SendFileAsync("html/about.html");
            }
        ));
    }

    private void LoginMapGet()
    {
        app.UseEndpoints(endpoints =>
            endpoints.MapGet("/login", async (HttpContext context) =>
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.SendFileAsync("html/login.html");
            }
        ));        
    }
    
    private void LoginMapPost()
    {
        app.UseEndpoints(endpoints =>
            endpoints.MapPost("/login", async (string? returnUrl, HttpContext context) =>
            {
                var form = context.Request.Form;

                if (!form.ContainsKey("email") || !form.ContainsKey("password"))
                    return Results.BadRequest("Email �/��� ������ �� �����������");

                string email = form["email"];
                string password = form["password"];

                if (authentication.Login(email, password))
                {
                    var claims = new List<Claim> { new Claim(ClaimTypes.Name, email) };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                    await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return Results.Redirect(returnUrl ?? "/cabinet");
                }
                else
                {
                    var invalidEmailAlert = "<meta charset=\"UTF-8\"><script>alert('Ошибка. Вероятно аккаунта с таким адресом электронной почты не существует.'); window.location = '/login';</script>";
                    return Results.Content(invalidEmailAlert, "text/html");
                }
            }
        ));        
    }

    private void RegisterMapGet()
    {
        app.UseEndpoints(endpoints =>
            endpoints.MapGet("/register", async (HttpContext context) =>
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.SendFileAsync("html/register.html");
            }
        ));
    }

    private void RegisterMapPost()
    {
        app.UseEndpoints(endpoints =>
            endpoints.MapPost("/register", async (string? returnUrl, HttpContext context) =>
            {
                var form = context.Request.Form;

                if (!form.ContainsKey("email") || !form.ContainsKey("password"))
                    return Results.BadRequest("Unable to find Email or Password");

                string email = form["email"];
                string password = form["password"];

                var isSucces = authentication.Register(email, password);
                if (isSucces)
                {
                    var claims = new List<Claim> { new Claim(ClaimTypes.Name, email) };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                    await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return Results.Redirect(returnUrl ?? "/cabinet");
                }
                else
                {
                    var invalidEmailAlert = "<meta charset=\"UTF-8\"><script>alert('Ошибка. Вероятно аккаунта с таким адресом электронной почты уже существует.'); window.location = '/register';</script>";
                    return Results.Content(invalidEmailAlert, "text/html");
                }
            }
        ));
    }

    private void LogoutMapPost()
    {
        app.UseEndpoints(endpoints =>
            endpoints.MapPost("/logout", [Authorize] async (HttpContext context) =>
            {
                await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Results.Redirect("/login");
            }
        ));
    }
    
    private void CabinetMapGet()
    {
        app.UseEndpoints(endpoints =>
            endpoints.MapGet("/cabinet", [Authorize] async (HttpContext context) =>
            {

                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.SendFileAsync("html/cabinet.html");
            }
        ));
    }

    private void ListCoursesMapGet()
    {
        app.UseEndpoints(endpoints =>
            endpoints.MapGet("/listcourses", async (HttpContext context) =>
            {

                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.SendFileAsync("html/listcourses.html");
            }
        ));
    }

    private void GetCourseMapGet()
    {
        app.UseEndpoints(endpoints =>
           endpoints.MapGet("/getcourse", [Authorize] async (HttpContext context) =>
           {
               var query = context.Request.Query;
               if (query.ContainsKey("id"))
               {
                   var id = query["id"];
                   var json = resources.GetResource(id);

                   if (json != null)
                   {
                       context.Response.ContentType = "application/json";
                       await context.Response.WriteAsync(json);
                   }
                   else
                   {
                       context.Response.ContentType = "text/plain; charset=utf-8";
                       await context.Response.WriteAsync("Course not found.");
                   }
               }
               else
               {
                   context.Response.ContentType = "text/plain; charset=utf-8";
                   await context.Response.WriteAsync("No query parameter 'id' found.");
               }
           }
        ));
    }
    
    private void CheckLoginStatusMapGet()
    {
        app.UseEndpoints(endpoints =>
           endpoints.MapGet("/checkloginstatus", async (HttpContext context) =>
           {
               var response = new
               {
                   loginstatus = context.User.Identity.IsAuthenticated,
                   username = context.User.Identity.Name
               };

               context.Response.ContentType = "application/json";
               await context.Response.WriteAsync(JsonSerializer.Serialize(response));
           }
        ));
    }
    
    private void AlreadyPurchasedMapGet()
    {
        app.UseEndpoints(endpoints =>
           endpoints.MapGet("/alreadypurchased", async (HttpContext context) =>
           {
               var query = context.Request.Query;
               bool alreadypurchased = false;
               if (query.ContainsKey("id"))
               {
                   var id = Convert.ToInt32(query["id"]);
                   var name = context.User.Identity.Name;
                   if (name != null)
                   {
                       alreadypurchased = coursesDB.DoesUserHaveCourse(context.User.Identity.Name, id);
                   }
                   else
                       alreadypurchased = false;
               }
               else
               {
                   context.Response.ContentType = "text/plain; charset=utf-8";
                   await context.Response.WriteAsync("No query parameter 'id' found.");
               }

               var response = new
               {
                   alreadypurchased = Convert.ToInt16(alreadypurchased)
               };
               context.Response.ContentType = "application/json";
               await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
           }
        ));
    }
    
    private void GetCourseToBuyMapGet()
    {
        app.UseEndpoints(endpoints =>
           endpoints.MapGet("/getcoursetobuy", async (HttpContext context) =>
           {
               var query = context.Request.Query;
               var value = Convert.ToInt32(query["id"]);


               var course = coursesDB.GetById(value).JSON;

               context.Response.ContentType = "application/json";
               await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(course));
           }
        ));
    }
    
    private void BuyCourseMapGet()
    {
        app.UseEndpoints(endpoints =>
           endpoints.MapGet("/buycourse", async (HttpContext context) =>
           {
               context.Response.ContentType = "text/html; charset=utf-8";
               await context.Response.SendFileAsync("html/buycourse.html");
           }
        ));
    }
    
    private void CourseMapGet()
    {
        app.UseEndpoints(endpoints =>
           endpoints.MapGet("/course", [Authorize] async (HttpContext context) =>
           {
               context.Response.ContentType = "text/html; charset=utf-8";
               await context.Response.SendFileAsync("html/course.html");
           }
        ));
    }
    
    private void GetAllCoursesMapGet()
    {
        app.UseEndpoints(endpoints =>
           endpoints.MapGet("/getallcourses", async (HttpContext context) =>
           {
               var coursesList = coursesDB.GetAll();
               var jsonList = new List<string>();

               foreach (var course in coursesList)
               {
                   string json = course.JSON;
                   jsonList.Add(json);
               }

               context.Response.ContentType = "application/json";
               await context.Response.WriteAsync(JsonSerializer.Serialize(jsonList));
           }
        ));
    }
    
    private void GetTagCoursesMapGet()
    {
        app.UseEndpoints(endpoints =>
           endpoints.MapGet("/gettagcourses", async (HttpContext context) =>
           {
               var query = context.Request.Query;
               string tag = "";
               if (query.ContainsKey("tag"))
               {
                   tag = query["tag"];
               }
               else
               {
                   context.Response.ContentType = "text/plain; charset=utf-8";
                   await context.Response.WriteAsync("No query parameter 'tag' found.");
               }

               var coursesList = coursesDB.GetByTag(tag);
               var jsonList = new List<string>();

               foreach (var course in coursesList)
               {
                   string json = course.JSON;
                   jsonList.Add(json);
               }

               context.Response.ContentType = "application/json";
               await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(jsonList));
           }
        ));
    }

    private void GetUserCoursesMapGet()
    {
        app.UseEndpoints(endpoints =>
           endpoints.MapGet("/getusercourses", [Authorize] async (HttpContext context) =>
           {
               var userName = context.User.Identity.Name;
               var coursesList = coursesDB.GetByUser(userName);
               var jsonList = new List<string>();

               foreach (var course in coursesList)
               {
                   string json = course.JSON;
                   jsonList.Add(json);
               }

               context.Response.ContentType = "application/json";
               await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(jsonList));
           }
        ));
    }

    private void SubscribeMapPost()
    {
        app.UseEndpoints(endpoints =>
           endpoints.MapPost("/subscribe", async (HttpContext context) =>
           {
               string email = context.Request.Form["email"];

               usersDB.AddSubscribe(email);

               string responseMessage = "Спасибо за подписку!";
               context.Response.Headers.Add("Content-Type", "application/json");
               await context.Response.WriteAsync("{\"message\": \"" + responseMessage + "\"}");
           }
        ));
    }

    private void PaymentMapGet()
    {
        app.UseEndpoints(endpoints =>
           endpoints.MapGet("/payment", [Authorize] async (HttpContext context) =>
           {
               var query = context.Request.Query;
               if (query.ContainsKey("id"))
               {
                   var id = Convert.ToInt32(query["id"]);
                   var price = coursesDB.GetById(id).Price;
               }
               else
               {
                   context.Response.ContentType = "text/plain; charset=utf-8";
                   await context.Response.WriteAsync("No query parameter 'id' found.");
               }
               context.Response.ContentType = "text/html; charset=utf-8";
               await context.Response.SendFileAsync("form-credit_card2/index.html");
           }
        ));

    }

    private void PaymentRequestMapPost()
    {
        app.UseEndpoints(endpoints =>
           endpoints.MapPost("/paymentrequest", [Authorize] async (HttpContext context) =>
           {
               var query = context.Request.Query;
               if (query.ContainsKey("id"))
               {
                   var id = Convert.ToInt32(query["id"]);
                   var price = coursesDB.GetById(id).Price;


                   var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();

                   var paymentData = JsonSerializer.Deserialize<Card>(requestBody);
                   var status = payment.PayByCard(paymentData, price);
                   if (status)
                   {
                       var userName = context.User.Identity.Name;
                       coursesDB.AddUserCourse(id, userName);
                   }

                   var json = new
                   {
                       status = status,
                   };
                   return Results.Json(json);
               }
               else
               {
                   context.Response.ContentType = "text/plain; charset=utf-8";
                   await context.Response.WriteAsync("No query parameter 'id' found.");
                   return Results.Redirect("/listcourses");
               }
           }
        ));
    }
}
