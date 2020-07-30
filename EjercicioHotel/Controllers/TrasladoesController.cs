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
    public class TrasladoesController : Controller
    {
        private readonly EjercicioHotelTrasladoContext _context;

        public TrasladoesController(EjercicioHotelTrasladoContext context)
        {
            _context = context;
        }

        // GET: Trasladoes
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Traslado.ToListAsync());
            return View(await _context.Traslado.Find(_ => true).ToListAsync());
        }

        // GET: Trasladoes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // var traslado = await _context.Traslado
            //     .FirstOrDefaultAsync(m => m.Id == id);

            FilterDefinition<Traslado> filter = Builders<Traslado>.Filter.Eq("Id", id);
            var traslado = await _context.Traslado.Find(filter).FirstOrDefaultAsync();

            if (traslado == null)
            {
                return NotFound();
            }

            return View(traslado);
        }

        // GET: Trasladoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trasladoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,Punto,IdHuesped")] Traslado traslado)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(traslado);
                //await _context.SaveChangesAsync();
                await _context.Traslado.InsertOneAsync(traslado);
                return RedirectToAction(nameof(Index));
            }
            return View(traslado);
        }

        // GET: Trasladoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var traslado = await _context.Traslado.FindAsync(id);
            FilterDefinition<Traslado> filter = Builders<Traslado>.Filter.Eq("Id", id);
            var traslado = await _context.Traslado.Find(filter).FirstOrDefaultAsync();
            if (traslado == null)
            {
                return NotFound();
            }
            return View(traslado);
        }

        // POST: Trasladoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Fecha,Punto,IdHuesped")] Traslado traslado)
        {
            if (id != traslado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(traslado);
                    //await _context.SaveChangesAsync();
                    await _context.Traslado.ReplaceOneAsync(filter: g => g.Id == traslado.Id, replacement: traslado);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrasladoExists(traslado.Id))
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
            return View(traslado);
        }

        // GET: Trasladoes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

           // var traslado = await _context.Traslado
           //     .FirstOrDefaultAsync(m => m.Id == id);

            FilterDefinition<Traslado> filter = Builders<Traslado>.Filter.Eq("Id", id);
            var traslado = await _context.Traslado.Find(filter).FirstOrDefaultAsync();

            if (traslado == null)
            {
                return NotFound();
            }

            return View(traslado);
        }

        // POST: Trasladoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var traslado = await _context.Traslado.FindAsync(id);
            //_context.Traslado.Remove(traslado);
            // await _context.SaveChangesAsync();


            FilterDefinition<Traslado> data = Builders<Traslado>.Filter.Eq("Id", id);
            await _context.Traslado.DeleteOneAsync(data);

            return RedirectToAction(nameof(Index));
        }

        private bool TrasladoExists(string id)
        {
            // return _context.Traslado.Any(e => e.Id == id);
            FilterDefinition<Traslado> filter = Builders<Traslado>.Filter.Eq("Id", id);
            var traslado = _context.Traslado.Find(filter).FirstOrDefaultAsync();
            return (traslado != null);
        }
    }
}
