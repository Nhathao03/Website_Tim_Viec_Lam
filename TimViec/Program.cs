using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TimViec.Data;
using TimViec.Models;
using TimViec.Repository;
using TimViec.Respository;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình DbContext và chuỗi kết nối trong appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Cấu hình Identity sử dụng ApplicationUser thay vì IdentityUser
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>() // Sử dụng DbContext để lưu trữ thông tin người dùng
    .AddDefaultTokenProviders() // Cung cấp các phương thức token như xác nhận email, đổi mật khẩu
    .AddDefaultUI(); // Nếu bạn cần UI mặc định của Identity (đăng nhập, đăng ký, v.v.)

// Cấu hình Razor Pages (nếu bạn đang sử dụng Razor Pages)
builder.Services.AddRazorPages();

// Cấu hình xác thực với Google (nếu sử dụng)
builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    googleOptions.CallbackPath = "/login-with-google";
});

// Đăng ký các dịch vụ khác của ứng dụng
builder.Services.AddControllersWithViews();

// Đăng ký các repository của ứng dụng
builder.Services.AddScoped<ICompanyRespository, EFCompanyRespository>();
builder.Services.AddScoped<IJobRespository, EFJobRespository>();
builder.Services.AddScoped<IStatusRepository, EFStatusJobRepository>();
builder.Services.AddScoped<IApplicationUser, EFApplicationUser>();
builder.Services.AddScoped<IRankRespository, EFRankRespository>();
builder.Services.AddScoped<ICityRespository, EFCityRespository>();
builder.Services.AddScoped<IType_WorkRespository, EFType_WorkRespository>();
builder.Services.AddScoped<ISkillRespository, EFSkillRespository>();
builder.Services.AddScoped<IfavouriteJob, EFfavouriteJob>();
builder.Services.AddScoped<IfeedbackRepository, EFfeedbackRepository>();
builder.Services.AddScoped<ITypeCVRepository, EFTypeCVRepository>();
builder.Services.AddScoped<ITemplateRepository, EFTemplateRepository>();
builder.Services.AddScoped<ISectionRespository, EFSectionRepository>();

var app = builder.Build();

// Cấu hình đường dẫn và pipeline xử lý yêu cầu
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // Cấu hình HSTS cho sản phẩm (SSL)
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Để phục vụ các file tĩnh (CSS, JS, hình ảnh,...)

app.UseRouting();

// Đảm bảo rằng middleware cho xác thực và ủy quyền được đưa vào pipeline
app.UseAuthentication();
app.UseAuthorization();

// Định tuyến Razor Pages
app.MapRazorPages();

// Định tuyến các Controller cho các khu vực Admin, Company, v.v.
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "Admin",
        pattern: "{area:exists}/{controller=Manager}/{action=Index}/{id?}"
    );
    endpoints.MapControllerRoute(
        name: "Company",
        pattern: "{area:exists}/{controller=Company}/{action=Index}/{id?}"
    );
});

// Định tuyến mặc định cho các Controller trong ứng dụng
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run(); // Chạy ứng dụng
