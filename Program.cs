using CRUDwithMinimalAPI;
using CRUDwithMinimalAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>();
var app = builder.Build();

// GET all products
app.MapGet("/products", async (AppDbContext dbContext) =>
{
    try
    {
        var products = await dbContext.Products.ToListAsync();
        return Results.Ok(products);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

// GET product by ID
app.MapGet("/products/{id}", async (int id, AppDbContext dbContext) =>
{
    try
    {
        var product = await dbContext.Products.FindAsync(id);
        if (product == null)
        {
            return Results.NotFound();
        }
        return Results.Ok(product);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

// POST a new product
app.MapPost("/products", async (Product product, AppDbContext dbContext) =>
{
    try
    {
        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync();
        return Results.Created($"/products/{product.Id}", product);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

// PUT/UPDATE an existing product
app.MapPut("/products/{id}", async (int id, Product updatedProduct, AppDbContext dbContext) =>
{
    try
    {
        var product = await dbContext.Products.FindAsync(id);
        if (product == null)
        {
            return Results.NotFound();
        }
        product.Name = updatedProduct.Name;
        product.Price = updatedProduct.Price;
        await dbContext.SaveChangesAsync();
        return Results.NoContent();
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

// DELETE a product by ID
app.MapDelete("/products/{id}", async (int id, AppDbContext dbContext) =>
{
    try
    {
        var product = await dbContext.Products.FindAsync(id);
        if (product == null)
        {
            return Results.NotFound();
        }
        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync();
        return Results.NoContent();
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.Run();
