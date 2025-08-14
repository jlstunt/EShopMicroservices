using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

/*
 * API Endpoints:
 * 
 * Method  Request URI          Use Case
 * GET     /products            List all products
 * GET     /products/{id}       Fetch a specific product
 * GET     /products/category    Get products by category
 * POST    /products            Create a new product
 * PUT     /products/{id}       Update a product
 * DELETE  /products/{id}       Remove a product
 */


app.Run();
