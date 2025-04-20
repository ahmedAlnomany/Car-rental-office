var builder = WebApplication.CreateBuilder(args);

// إضافة خدمات MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// في حال كان التطبيق في بيئة التطوير
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// السماح بتحميل الصور والملفات الثابتة
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// تحديد الصفحة الرئيسية ليتم عرضها عند تشغيل التطبيق
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();