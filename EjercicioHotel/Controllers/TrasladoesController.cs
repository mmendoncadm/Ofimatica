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

                // **** Buscar el Huesped
                // *****************************************************
                
                FilterDefinition<Huesped> filter1 = Builders<Huesped>.Filter.Eq("DocIdentificacion", traslado.IdHuesped);
                var huesped = await _context.Huespedes.Find(filter1).FirstOrDefaultAsync();
                //Si no existe lo agrega
                if (huesped == null)
                {
                    return NotFound();
                }
                else
                {
                    await _context.Traslado.InsertOneAsync(traslado);

                }
                   

                // **** Actualiza el recorrido
                // *****************************************************
                // *****************************************************
                // *****************************************************
                // *****************************************************
                
                // buscar todos los traslados para la fecha
                FilterDefinition<Traslado> filter1 = Builders<Traslado>.Filter.Eq("Fecha", traslado.Fecha);
                IEnumerable<Traslado> ListaTraslado = await _context.Traslado.Find(filter1).ToListAsync();
                Transporte TransporteNuevo=new Transporte();
                TransporteNuevo.Fecha = traslado.Fecha;
                /*foreach (var t in ListaTraslado) 
                    {
                    TransporteNuevo.AgregarPunto(t.Punto);
                    }
                TransporteNuevo.ActualizarRecorrido();*/


                //Busca el traslado 
                FilterDefinition<Transporte> filter2 = Builders<Transporte>.Filter.Eq("Fecha", traslado.Fecha);
                var transporte = await _context.Transporte.Find(filter2).FirstOrDefaultAsync();
                //Si no existe lo agrega
                if (transporte == null)
                {
                    await _context.Transporte.InsertOneAsync(TransporteNuevo);
                }
                else
                {
                    //Si existe lo Actualiza
                    transporte.Km = TransporteNuevo.Km;
                    transporte.Recorrido = TransporteNuevo.Recorrido;
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            //_context.Update(traslado);
                            //await _context.SaveChangesAsync();
                            await _context.Transporte.ReplaceOneAsync(filter: g => g.Id == transporte.Id, replacement: transporte);
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


                }

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

        private bool TransporteExists(string id)
        {
            
            FilterDefinition<Transporte> filter = Builders<Transporte>.Filter.Eq("Id", id);
            var transporte = _context.Transporte.Find(filter).FirstOrDefaultAsync();
            return (transporte != null);
        }
    }
}
