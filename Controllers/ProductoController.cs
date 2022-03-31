using Dapper;
using InduccionSemana4.Models;
using InduccionSemana4.Models.Comandos.CommandProducto;
using InduccionSemana4.Models.Consultas.ReadProducto;
using InduccionSemana4.Models.Consultas.ReadVendedor;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace InduccionSemana4.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IMediator Mediator;

        public ProductoController(IMediator mediator)
        {
            Mediator = mediator;
        }

        // GET: Producto
        public async Task<ActionResult<List<Producto>>> Index()
        {
            var a = await Mediator.Send(new ProductoCrudSelect.SelectProducto());
            return View(a);
        }

        // Buscar por filtros
        public IActionResult Menu(string searchString)
        {
            string sqlRest = "SELECT * FROM Productos";
            using (var connection = new SqlConnection("Data Source= MEX-J6TNMG3\\SQLEXPRESS; Initial Catalog=RestaurantOnline; Trusted_Connection=True; MultipleActiveResultSets=true"))
            {
                var res = connection.Query<Producto>(sqlRest).AsQueryable();
                if (!String.IsNullOrEmpty(searchString))
                {
                    res = res.Where(a => a.Categoria.Contains(searchString));
                }
                ViewData["CurrentFilter"] = searchString;
                return View(res.ToList());
            }
        }


        // get: Producto/details/5
        public async Task<ActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await Mediator.Send(new ProductoCrudSelectById.SelectIdProducto() { Id = id });
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Producto/Create para insertar un pedido
        public async Task<ActionResult> Create2(Pedido pedido)
        {
            var vendedores = await Mediator.Send(new VendedorCrudSelect.SelectVendedor());
            List<SelectListItem> clientesSLI = new List<SelectListItem>();
            foreach (var item in vendedores)
            {
                SelectListItem listItem = new SelectListItem { Text = item.Id + " " + item.NombreNegocio, Value = item.Id.ToString() };
                clientesSLI.Add(listItem);
            }
            ViewBag.vendedores = clientesSLI;
            return View();
        }


        // GET: Producto/Create
        public async Task<ActionResult> Create()
        {
            var vendedores = await Mediator.Send(new VendedorCrudSelect.SelectVendedor());
            List<SelectListItem> clientesSLI = new List<SelectListItem>();
            foreach (var item in vendedores)
            {
                SelectListItem listItem = new SelectListItem { Text = item.Id + " " + item.NombreNegocio, Value = item.Id.ToString() };
                clientesSLI.Add(listItem);
            }
            ViewBag.vendedores = clientesSLI;
            return View();
        }

        // POST: Producto/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClaveProducto,NombreProducto,Stock,Precio,Descripcion,Categoria,VendedorId")] Producto producto)
        {


            if (ModelState.IsValid)
            {
                await Mediator.Send(new ProductoCrudInsert.InsertProducto() { Producto = producto });
                return RedirectToAction(nameof(Index));
            }
            return BadRequest("Errro al guardar");

        }

        // GET: Producto/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendedores = await Mediator.Send(new VendedorCrudSelect.SelectVendedor());
            List<SelectListItem> clientesSLI = new List<SelectListItem>();
            foreach (var item in vendedores)
            {
                SelectListItem listItem = new SelectListItem { Text = item.Id + " " + item.NombreNegocio, Value = item.Id.ToString() };
                clientesSLI.Add(listItem);
            }
            ViewBag.vendedores = clientesSLI;

            var producto = await Mediator.Send(new ProductoCrudSelectById.SelectIdProducto() { Id = id });
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        // POST: Producto/Edit/5        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClaveProducto,NombreProducto,Stock,Precio,Descripcion,Categoria,VendedorId")] Producto producto)
        {
            if (id != producto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var numReg = await Mediator.Send(new ProductoCrudUpdate.UpdateProducto { Producto = producto });
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound("Error al Actualizar");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // GET: Producto/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var producto = await Mediator.Send(new ProductoCrudSelectById.SelectIdProducto() { Id = id });
            if (producto == null) return NotFound();

            return View(producto);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var p = await Mediator.Send(new ProductoCrudSelectById.SelectIdProducto() { Id = id });
                    var produc = await Mediator.Send(new ProductoCrudDelete.DeleteProducto() { producto = p });
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound("Error al Actualizar");
                }

            }
            return RedirectToAction(nameof(Index));
        }


    }
}

