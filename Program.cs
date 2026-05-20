using Microsoft.EntityFrameworkCore;
using WorldApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactAppPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<WorldDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}



//app.UseHttpsRedirection();
app.UseCors("ReactAppPolicy");

// --- Cities Endpoints ---
app.MapGet("/cities", async (WorldDbContext db) =>
    await db.Cities.ToListAsync());

app.MapGet("/cities/{id}", async (int id, WorldDbContext db) =>
    await db.Cities.FindAsync(id)
        is City city
            ? Results.Ok(city)
            : Results.NotFound());

app.MapPost("/cities", async (City city, WorldDbContext db) =>
{
    db.Cities.Add(city);
    await db.SaveChangesAsync();
    return Results.Created($"/cities/{city.ID}", city);
});

app.MapPut("/cities/{id}", async (int id, City inputCity, WorldDbContext db) =>
{
    var city = await db.Cities.FindAsync(id);
    if (city is null) return Results.NotFound();

    city.Name = inputCity.Name;
    city.CountryCode = inputCity.CountryCode;
    city.District = inputCity.District;
    city.Population = inputCity.Population;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/cities/{id}", async (int id, WorldDbContext db) =>
{
    if (await db.Cities.FindAsync(id) is City city)
    {
        db.Cities.Remove(city);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    return Results.NotFound();
});

// --- Countries Endpoints ---
app.MapGet("/countries", async (WorldDbContext db) =>
    await db.Countries.ToListAsync());

app.MapGet("/countries/{code}", async (string code, WorldDbContext db) =>
    await db.Countries.FindAsync(code)
        is Country country
            ? Results.Ok(country)
            : Results.NotFound());

app.MapPost("/countries", async (Country country, WorldDbContext db) =>
{
    db.Countries.Add(country);
    await db.SaveChangesAsync();
    return Results.Created($"/countries/{country.Code}", country);
});

app.MapPut("/countries/{code}", async (string code, Country inputCountry, WorldDbContext db) =>
{
    var country = await db.Countries.FindAsync(code);
    if (country is null) return Results.NotFound();

    country.Name = inputCountry.Name;
    country.Continent = inputCountry.Continent;
    country.Region = inputCountry.Region;
    country.SurfaceArea = inputCountry.SurfaceArea;
    country.IndepYear = inputCountry.IndepYear;
    country.Population = inputCountry.Population;
    country.LifeExpectancy = inputCountry.LifeExpectancy;
    country.GNP = inputCountry.GNP;
    country.GNPOld = inputCountry.GNPOld;
    country.LocalName = inputCountry.LocalName;
    country.GovernmentForm = inputCountry.GovernmentForm;
    country.HeadOfState = inputCountry.HeadOfState;
    country.Capital = inputCountry.Capital;
    country.Code2 = inputCountry.Code2;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/countries/{code}", async (string code, WorldDbContext db) =>
{
    if (await db.Countries.FindAsync(code) is Country country)
    {
        db.Countries.Remove(country);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    return Results.NotFound();
});

// --- CountryLanguages Endpoints ---
app.MapGet("/languages", async (WorldDbContext db) =>
    await db.CountryLanguages.ToListAsync());

app.MapGet("/languages/{countryCode}/{language}", async (string countryCode, string language, WorldDbContext db) =>
    await db.CountryLanguages.FindAsync(countryCode, language)
        is CountryLanguage cl
            ? Results.Ok(cl)
            : Results.NotFound());

app.MapPost("/languages", async (CountryLanguage language, WorldDbContext db) =>
{
    db.CountryLanguages.Add(language);
    await db.SaveChangesAsync();
    return Results.Created($"/languages/{language.CountryCode}/{language.Language}", language);
});

app.MapPut("/languages/{countryCode}/{language}", async (string countryCode, string languageName, CountryLanguage inputLanguage, WorldDbContext db) =>
{
    var language = await db.CountryLanguages.FindAsync(countryCode, languageName);
    if (language is null) return Results.NotFound();

    language.IsOfficial = inputLanguage.IsOfficial;
    language.Percentage = inputLanguage.Percentage;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/languages/{countryCode}/{language}", async (string countryCode, string languageName, WorldDbContext db) =>
{
    if (await db.CountryLanguages.FindAsync(countryCode, languageName) is CountryLanguage language)
    {
        db.CountryLanguages.Remove(language);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    return Results.NotFound();
});

app.Run();
