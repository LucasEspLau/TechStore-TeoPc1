using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TechStore.Models;

namespace TechStore.Controllers;

public class ProductoController : Controller
{
    private readonly ILogger<ProductoController> _logger;

    public ProductoController(ILogger<ProductoController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

        [HttpPost]
        public IActionResult Create([Bind("Nombre,Descripcion,Precio")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                producto.PrecioIgv = Math.Round(producto.Precio * 0.18, 2); // Redondear el IGV a dos decimales
                double sumTotal = Math.Round(producto.PrecioIgv + producto.Precio, 2); // Redondear el total a dos decimales

                ViewData["Message"] = "El producto se ha registrado exitosamente.\n" +
                    "Producto: " + producto.Nombre + "\n" +
                    "El igv del producto es: " + producto.PrecioIgv + "\n" +
                    "El precio total del producto con Igv es: " + sumTotal + "\n";

                // Redirigir al Index del ProductoController
                return View("Index", producto);
            }

            return View("Index", producto);
        }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}