using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EjercicioHotel;
using EjercicioHotel.Data;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Bson;

namespace EjercicioHotel.Controllers
{
    public class HuespedsController : Controller
    {
        private readonly EjercicioHotelContext _context;

        public HuespedsController(EjercicioHotelContext context)
        {
            _context = context;
        }

        // GET: Huespeds
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Huesped.ToListAsync());
            return View(await _context.Huesped.Find(_ => true).ToListAsync()); 
        }

        // GET: Huespeds/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var huesped = await _context.Huesped
            //    .FirstOrDefaultAsync(m => m.id == id);

            FilterDefinition<Huesped> filter = Builders<Huesped>.Filter.Eq("Id", id);
            var huesped= await _context.Huesped.Find(filter).FirstOrDefaultAsync();

            if (huesped == null)
            {
                return NotFound();
            }

            return View(huesped);
        }

        // GET: Huespeds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Huespeds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,DocIdentificacion,Nombre,Apellido")] Huesped huesped)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(huesped);
                await _context.Huesped.InsertOneAsync(huesped);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(huesped);
            //await _context.SaveChangesAsync();

        }

        // GET: Huespeds/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var huesped = await _context.Huesped.FindAsync(id);
            FilterDefinition<Huesped> filter = Builders<Huesped>.Filter.Eq("Id", id);
            var huesped = await _context.Huesped.Find(filter).FirstOrDefaultAsync();
            if (huesped == null)
            {
                return NotFound();
            }
            return View(huesped);
        }

        // POST: Huespeds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,DocIdentificacion,Nombre,Apellido")] Huesped huesped)
        {
            if (id != huesped.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(huesped);
                    //await _context.SaveChangesAsync();
                    await _context.Huesped.ReplaceOneAsync(filter: g => g.Id == huesped.Id, replacement: huesped);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HuespedExists(huesped.Id))
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
            return View(huesped);
        }

        // GET: Huespeds/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

           // var huesped = await _context.Huesped
            //    .FirstOrDefaultAsync(m => m.id == id);

            
            FilterDefinition<Huesped> filter = Builders<Huesped>.Filter.Eq("Id", id);
            var huesped = await _context.Huesped.Find(filter).FirstOrDefaultAsync();
         
                if (huesped == null)
            {
                return NotFound();
            }

            return View(huesped);
        }

        // POST: Huespeds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            //var huesped = await _context.Huesped.FindAsync(id);
            //_context.Huesped.Remove(huesped);
            //await _context.SaveChangesAsync();

            FilterDefinition<Huesped> data = Builders<Huesped>.Filter.Eq("Id", id);
            await _context.Huesped.DeleteOneAsync(data);

            return RedirectToAction(nameof(Index));
        }

        private bool HuespedExists(string id)
        {
            //return _context.Huesped.Any(e => e.id == id);
            //var huesped = await _context.Huesped.FindAsync(id);
            FilterDefinition<Huesped> filter = Builders<Huesped>.Filter.Eq("Id", id);
            var huesped =  _context.Huesped.Find(filter).FirstOrDefaultAsync();
            return (huesped != null);
            
        }
    }
}
