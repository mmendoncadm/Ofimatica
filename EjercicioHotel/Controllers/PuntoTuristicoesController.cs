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
    public class PuntoTuristicoesController : Controller
    {
        private readonly EjercicioHotelPuntoContext _context;

        public PuntoTuristicoesController(EjercicioHotelPuntoContext context)
        {
            _context = context;
        }

        // GET: PuntoTuristicoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.PuntoTuristico.Find(_ => true).ToListAsync());
            //return View(await _context.Traslado.Find(_ => true).ToListAsync());
        }

        // GET: PuntoTuristicoes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // var puntoTuristico = await _context.PuntoTuristico
            //     .FirstOrDefaultAsync(m => m.id == id);

            FilterDefinition<PuntoTuristico> filter = Builders<PuntoTuristico>.Filter.Eq("Id", id);
            var puntoturistico = await _context.PuntoTuristico.Find(filter).FirstOrDefaultAsync();

            if (puntoturistico == null)
            {
                return NotFound();
            }

            return View(puntoturistico);
        }

        // GET: PuntoTuristicoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PuntoTuristicoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Nombre,Descripcion,Ubicación")] PuntoTuristico puntoTuristico)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(puntoTuristico);
                //await _context.SaveChangesAsync();
                await _context.PuntoTuristico.InsertOneAsync(puntoTuristico);
                return RedirectToAction(nameof(Index));
            }
            return View(puntoTuristico);
        }

        // GET: PuntoTuristicoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var puntoTuristico = await _context.PuntoTuristico.FindAsync(id);
            FilterDefinition<PuntoTuristico> filter = Builders<PuntoTuristico>.Filter.Eq("Id", id);
            var puntoturistico = await _context.PuntoTuristico.Find(filter).FirstOrDefaultAsync();
            if (puntoturistico == null)
            {
                return NotFound();
            }
            return View(puntoturistico);
        }

        // POST: PuntoTuristicoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("id,Nombre,Descripcion,Ubicación")] PuntoTuristico puntoTuristico)
        {
            if (id != puntoTuristico.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // _context.Update(puntoTuristico);
                    // await _context.SaveChangesAsync();
                    await _context.PuntoTuristico.ReplaceOneAsync(filter: g => g.Id == puntoTuristico.Id, replacement: puntoTuristico);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PuntoTuristicoExists(puntoTuristico.Id))
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
            return View(puntoTuristico);
        }

        // GET: PuntoTuristicoes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var puntoTuristico = await _context.PuntoTuristico
            //    .FirstOrDefaultAsync(m => m.id == id);
            FilterDefinition<PuntoTuristico> filter = Builders<PuntoTuristico>.Filter.Eq("Id", id);
            var puntoTuristico = await _context.PuntoTuristico.Find(filter).FirstOrDefaultAsync();
            if (puntoTuristico == null)
            {
                return NotFound();
            }

            return View(puntoTuristico);
        }

        // POST: PuntoTuristicoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            //var puntoTuristico = await _context.PuntoTuristico.FindAsync(id);
            //_context.PuntoTuristico.Remove(puntoTuristico);
            //await _context.SaveChangesAsync();

            FilterDefinition<PuntoTuristico> data = Builders<PuntoTuristico>.Filter.Eq("Id", id);
            await _context.PuntoTuristico.DeleteOneAsync(data);


            return RedirectToAction(nameof(Index));
        }

        private bool PuntoTuristicoExists(string id)
        {
            //return _context.PuntoTuristico.Any(e => e.id == id);
            FilterDefinition<PuntoTuristico> filter = Builders<PuntoTuristico>.Filter.Eq("Id", id);
            var puntoTuristico = _context.PuntoTuristico.Find(filter).FirstOrDefaultAsync();
            return (puntoTuristico != null);
        }
    }
}
