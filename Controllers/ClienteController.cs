using Dapper;
using InduccionSemana4.Models;
using InduccionSemana4.Models.Comandos.CommandCliente;
using InduccionSemana4.Models.Consultas.ReadCliente;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace InduccionSemana4.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IMediator Mediator;

        public ClienteController(IMediator mediator)
        {
            Mediator = mediator;
        }
        // GET: ClienteController
        public async Task<ActionResult<List<Cliente>>> Index()
        {
            return View(await Mediator.Send(new ClienteCrudSelect.SelectCliente()));
        }

        public IActionResult OpcionCliente()
        {
           return View();
        }

        public IActionResult SesionCliente()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SesionCliente(Cliente _cliente)
        {
            string sql = "SELECT * FROM Clientes WHERE Correo = '" + _cliente.Correo + "' AND Pwd = '" + _cliente.Pwd + "'; ";

            //Cambiar la base de datos aqui si existe posible error ... OJO AQUI
            using (var connection = new SqlConnection("Data Source= MEX-J6TNMG3\\SQLEXPRESS; Initial Catalog= RestaurantOnline; Trusted_Connection= True; MultipleActiveResultSets= True "))
            {
                var vendedor = connection.Query<Cliente>(sql).Where(x => x.Correo == _cliente.Correo && x.Pwd == _cliente.Pwd);
                if (vendedor != null)
                {
                    return RedirectToAction("Menu", "Producto");
                }
                else
                {
                    return View();
                }
            }
        }




        // GET: ClienteController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await Mediator.Send(new ClienteCrudSelectById.SelectIdCliente { Id = id });
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: ClienteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Id,ClaveCliente,NombreCliente,Apellidos,Direccion,Telefono,Correo,Pwd")] Cliente cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await Mediator.Send(new ClienteCrudInsert.InsertCliente() { Cliente = cliente });
                    return RedirectToAction(nameof(Index));
                }
                return BadRequest("Errro al guardar");
            }
            catch
            {
                return View(cliente);
            }
        }

        // GET: ClienteController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendedor = await Mediator.Send(new ClienteCrudSelectById.SelectIdCliente { Id = id });
            if (vendedor == null)
            {
                return NotFound();
            }
            return View(vendedor);
        }

        // POST: ClienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("Id,ClaveCliente,NombreCliente,Apellidos,Direccion,Telefono,Correo,Pwd")] Cliente cliente)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    if (id != cliente.Id)
                    {
                        return NotFound();
                    }
                    var numReg = await Mediator.Send(new ClienteCrudUpdate.InsertCliente { Cliente = cliente });
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound("Error al Actualizar");
                }

            }
            return View(cliente);
        }

        // GET: ClienteController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cliente = await Mediator.Send(new ClienteCrudSelectById.SelectIdCliente() { Id = id });
            if (cliente == null) return NotFound();

            return View(cliente);
        }

        // POST: ClienteController/Delete/5
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
                    var cliente = await Mediator.Send(new ClienteCrudSelectById.SelectIdCliente() { Id = id });
                    var numReg = await Mediator.Send(new ClienteCrudDelete.DeleteProducto() { Cliente = cliente });
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
