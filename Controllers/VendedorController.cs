#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InduccionSemana4.Models;
using InduccionSemana4.Models.Contexto;
using MediatR;
using InduccionSemana4.Models.Consultas.ReadVendedor;
using InduccionSemana4.Models.Comandos.CommandVendedores;
using Microsoft.Data.SqlClient;
using Dapper;

namespace InduccionSemana4.Controllers
{
    public class VendedorController : Controller
    {
        private readonly IMediator Mediator;

        public VendedorController(IMediator mediator)
        {
            Mediator = mediator;
        }

        // GET: Vendedors
        public async Task<ActionResult<List<Vendedor>>> Index()
        {
            var a = await Mediator.Send(new VendedorCrudSelect.SelectVendedor());
            return View(a);
        }

        //GET: Vendedors Menu
        public IActionResult Menu(string searchString)
        {
            string sqlRest = "SELECT * FROM Vendedores";
            using (var connection = new SqlConnection("id de estación de trabajo=restaurantOn.mssql.somee.com;tamaño del paquete=4096;id de usuario=Diml960810h_SQLLogin_1;pwd=9nkfjq4ka1;data source=restaurantOn.mssql.somee.com;persist security info=False;initial catalog=restaurantOn"))
            {
                var res = connection.Query<Vendedor>(sqlRest).AsQueryable();
                if (!String.IsNullOrEmpty(searchString))
                {
                    res = res.Where(a => a.Zona.Contains(searchString));
                }
                ViewData["CurrentFilter"] = searchString;
                return View(res.ToList());
            }
        }

        public IActionResult SesionVendedor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SesionVendedor(Vendedor _vendedor)
        {
            string sql = "SELECT * FROM Vendedores WHERE Correo = '" + _vendedor.Correo + "' AND Pwd = '" + _vendedor.Pwd + "'; ";

            //Cambiar la base de datos aqui si existe posible error ... OJO AQUI
            using (var connection = new SqlConnection("Data Source= MEX-J6TNMG3\\SQLEXPRESS; Initial Catalog= RestaurantOnline; Trusted_Connection= True; MultipleActiveResultSets= True "))
            {
                var vendedor = connection.Query<Vendedor>(sql).Where(x => x.Correo == _vendedor.Correo && x.Pwd == _vendedor.Pwd);
                if (vendedor != null)
                {
                    return RedirectToAction("Index", "Vendedor");
                }
                else
                {
                    return View();
                }
            }
        }


        public async Task<ActionResult<List<Vendedor>>> IndexByZone(Vendedor vendedor)
        {
            var vendedors = await Mediator.Send(new VendedorCrudSelectByZona.SelectZonaVendedor() { Zona = vendedor.Zona });
            return View("Index", vendedors);
        }

        // get: vendedors/details/5
        public async Task<ActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendedor = await Mediator.Send(new VendedorCrudSelectById.SelectIdVendedor() { Id = id });
            if (vendedor == null)
            {
                return NotFound();
            }

            return View(vendedor);
        }

        // GET: Vendedors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vendedors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClaveVendedor,NombreNegocio,TitularNegocio,Direccion,Zona,Telefono,FotoNegocio,Correo,Pwd")] Vendedor vendedor)
        {
            if (ModelState.IsValid)
            {
                await Mediator.Send(new VendedorCrudInsert.InsertVendedor() { Vendedor = vendedor });
                return RedirectToAction(nameof(Index));
            }
            return BadRequest("Errro al guardar");

        }

        // GET: Vendedors/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendedor = await Mediator.Send(new VendedorCrudSelectById.SelectIdVendedor() { Id = id });
            if (vendedor == null)
            {
                return NotFound();
            }
            return View(vendedor);
        }

        // POST: Vendedors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClaveVendedor,NombreNegocio,TitularNegocio,Direccion,Zona,Telefono,FotoNegocio,Correo,Pwd")] Vendedor vendedor)
        {
            if (id != vendedor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var numReg = await Mediator.Send(new VendedorCrudUpdate.UpdateVendedor { Vendedor = vendedor });
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound("Error al Actualizar");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vendedor);
        }

        // GET: Vendedors/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vend = await Mediator.Send(new VendedorCrudSelectById.SelectIdVendedor() { Id = id });
            if (vend == null) return NotFound();

            return View(vend);
        }

        // POST: Vendedors/Delete/5
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
                    var v = await Mediator.Send(new VendedorCrudSelectById.SelectIdVendedor() { Id = id });
                    var vend = await Mediator.Send(new VendedorCrudDelete.DeleteVendedor() { vendedor = v });
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound("Error al Actualizar");
                }

            }
            return RedirectToAction(nameof(Index));
        }

        //private bool VendedorExists(int id)
        //{
        //    return _context.Vendedores.Any(e => e.Id == id);
        //}
    }
}