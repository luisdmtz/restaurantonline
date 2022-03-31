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
using InduccionSemana4.Models.Consultas.ReadPedido;
using InduccionSemana4.Models.Comandos.CommandPedido;
using InduccionSemana4.Models.Consultas.ReadCliente;
using InduccionSemana4.Models.Consultas.ReadVendedor;
using Microsoft.Data.SqlClient;
using Dapper;
using InduccionSemana4.Models.Consultas.ReadProducto;

namespace InduccionSemana4.Controllers
{
    public class PedidosController : Controller
    {
        private readonly IMediator _mediator;

        public PedidosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: Pedidoes
        public async Task<ActionResult> Index()
        {
            return View(await _mediator.Send(new PedidoCrudSelect.SelectProducto()));
        }

        // GET: Pedidoes/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendedor = await _mediator.Send(new PedidoCrudSelectGetById.SelectIdPedido { Id = id });
            if (vendedor == null)
            {
                return NotFound();
            }

            return View(vendedor);
        }

        // GET: Pedidoes/Create
        public async Task<ActionResult> Create(int ProductoId)
        {
            var vendedores = await _mediator.Send(new VendedorCrudSelect.SelectVendedor());
            List<SelectListItem> clientesSLI = new List<SelectListItem>();
            foreach (var item in vendedores)
            {
                SelectListItem listItem = new SelectListItem { Text = item.Id + " " + item.TitularNegocio, Value = item.Id.ToString() };
                clientesSLI.Add(listItem);
            }
            ViewBag.vendedores = clientesSLI;

            var mediatorProducto = await _mediator.Send(new ProductoCrudSelectById.SelectIdProducto() { Id = ProductoId });
            ViewBag.productoPrecio = mediatorProducto.Precio.ToString();
            Pedido pedido = new Pedido();
            pedido.ProductoId = ProductoId;



            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClavePedido,PrecioTotal,Cantidad,ClienteId,ProductoId")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {


                var numReg = await _mediator.Send(new PedidoCrudInsert.InsertProducto { Pedido = pedido });
                return RedirectToAction(nameof(Index));
            }
            return BadRequest("Error al guardar");
        }

        // GET: Pedidoes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _mediator.Send(new PedidoCrudSelectGetById.SelectIdPedido() { Id = id });
            if (pedido == null)
            {
                return NotFound();
            }
            return View(pedido);
        }

        // POST: Pedidoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClavePedido,PrecioTotal,Cantidad,ClienteId,ProductoId")] Pedido pedido)
        {
            if (id != pedido.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var numReg = await _mediator.Send(new PedidoCrudUpdate.UpdatePedido { Pedido = pedido });
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound("Error al Actualizar");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pedido);
        }

        // GET: Pedidoes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _mediator.Send(new PedidoCrudSelectGetById.SelectIdPedido() { Id = id });
            if (pedido == null)
            {
                return NotFound();
            }
            return View(pedido);
        }

        // POST: Pedidoes/Delete/5
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
                    var pedido = await _mediator.Send(new PedidoCrudSelectGetById.SelectIdPedido() { Id = id });
                    var numReg = await _mediator.Send(new PedidoCrudDelete.DeletePedido() { Pedido = pedido });
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound("Error al Actualizar");
                }

            }
            return RedirectToAction(nameof(Index));
        }

        //private bool PedidoExists(int id)
        //{
        //    return _context.Pedidos.Any(e => e.Id == id);
        //}
    }
}
