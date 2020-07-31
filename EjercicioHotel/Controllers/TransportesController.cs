using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EjercicioHotel.Data;
using EjercicioHotel.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Bson;

namespace EjercicioHotel.Controllers
{
    public class TransportesController : Controller
    {
        private readonly EjercicioHotelTransporteContext _context;

        public TransportesController(EjercicioHotelTransporteContext context)
        {
            _context = context;
        }

        // GET: Transportes
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Transporte.ToListAsync());
            return View(await _context.Transporte.Find(_ => true)
                .ToListAsync());
        }

        // GET: Transportes/Details/5
        public async Task<IActionResult> IndexHuesped(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // var traslado = await _context.Traslado
            //     .FirstOrDefaultAsync(m => m.Id == id);

            FilterDefinition<Transporte> filter = Builders<Transporte>.Filter.Eq("Id", id);
            var transporte = await _context.Transporte.Find(filter).FirstOrDefaultAsync();

            if (transporte == null)
            {
                return NotFound();
            }


            FilterDefinition<Traslado> filter2 = Builders<Traslado>.Filter.Eq("Fecha", transporte.Fecha);
            var traslados = await _context.Traslado.Find(filter2).FirstOrDefaultAsync();

            if (traslados == null)
            {
                return NotFound();
            }
            List<Traslado> ListaTraslado = _context.Traslado.Find(filter2).ToList();

            List<Huesped> ListaHuespedes = null;
            foreach (Traslado t in ListaTraslado)
            {
                FilterDefinition<Huesped> filter3 = Builders<Huesped>.Filter.Eq("DocIdentificacion", t.IdHuesped);
                var huesped = new Huesped();
                huesped = await _context.Huespedes.Find(filter3).FirstOrDefaultAsync();
                ListaHuespedes.Add(huesped);
            }
           
           return View(ListaHuespedes);
        }

      /*  public async Task<IActionResult> IndexPuntos(string id)
        {
           /* if (id == null)
            {
                return NotFound();
            }

            //var transporte = await _context.Transporte
            //    .FirstOrDefaultAsync(m => m.Id == id);

            FilterDefinition<Transporte> filter = Builders<Transporte>.Filter.Eq("Id", id);
            var transporte = await _context.Transporte.Find(filter).FirstOrDefaultAsync();

            if (transporte == null)
            {
                return NotFound();
            }

            //return View(transporte);
        }*/

        // GET: Transportes/Create
        /* public IActionResult Create()
         {
             return View();
         }

         // POST: Transportes/Create
         // To protect from overposting attacks, enable the specific properties you want to bind to, for 
         // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Create([Bind("Id,Fecha,Ruta,Km")] Transporte transporte)
         {
             if (ModelState.IsValid)
             {
                 _context.Add(transporte);
                 await _context.SaveChangesAsync();
                 return RedirectToAction(nameof(Index));
             }
             return View(transporte);
         }

         // GET: Transportes/Edit/5
         public async Task<IActionResult> Edit(string id)
         {
             if (id == null)
             {
                 return NotFound();
             }

             var transporte = await _context.Transporte.FindAsync(id);
             if (transporte == null)
             {
                 return NotFound();
             }
             return View(transporte);
         }

         // POST: Transportes/Edit/5
         // To protect from overposting attacks, enable the specific properties you want to bind to, for 
         // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Edit(string id, [Bind("Id,Fecha,Ruta,Km")] Transporte transporte)
         {
             if (id != transporte.Id)
             {
                 return NotFound();
             }

             if (ModelState.IsValid)
             {
                 try
                 {
                     _context.Update(transporte);
                     await _context.SaveChangesAsync();
                 }
                 catch (DbUpdateConcurrencyException)
                 {
                     if (!TransporteExists(transporte.Id))
                     {
                         return NotFound();
                     }
                     else
                     {
                         throw;
                     }
                 }
                 return RedirectToAction(nameof(Index));
             }
             return View(transporte);
         }*/

         // GET: Transportes/Delete/5
         public async Task<IActionResult> Delete(string id)
         {
             if (id == null)
             {
                 return NotFound();
             }

             //var transporte = await _context.Transporte
             //    .FirstOrDefaultAsync(m => m.Id == id);

            FilterDefinition<Transporte> filter = Builders<Transporte>.Filter.Eq("Id", id);
            var transporte = await _context.Transporte.Find(filter).FirstOrDefaultAsync();

            if (transporte == null)
             {
                 return NotFound();
             }

             return View(transporte);
         }

         // POST: Transportes/Delete/5
         [HttpPost, ActionName("Delete")]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> DeleteConfirmed(string id)
         {
            //var transporte = await _context.Transporte.FindAsync(id);
            //_context.Transporte.Remove(transporte);
            //await _context.SaveChangesAsync();
            FilterDefinition<Transporte> data = Builders<Transporte>.Filter.Eq("Id", id);
            await _context.Transporte.DeleteOneAsync(data);

            return RedirectToAction(nameof(Index));
         }

        private bool TransporteExists(string id)
        {
            //return _context.Transporte.Any(e => e.Id == id);
            FilterDefinition<Transporte> filter = Builders<Transporte>.Filter.Eq("Id", id);
            var transporte = _context.Transporte.Find(filter).FirstOrDefaultAsync();
            return (transporte != null);
        }
    }
}
