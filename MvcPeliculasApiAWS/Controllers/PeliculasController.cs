using Microsoft.AspNetCore.Mvc;
using MvcPeliculasApiAWS.Models;
using MvcPeliculasApiAWS.Services;

namespace MvcPeliculasApiAWS.Controllers
{
    public class PeliculasController : Controller
    {
        private ServicePeliculas service;

        public PeliculasController(ServicePeliculas service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Pelicula> peliculas =
                await this.service.GetPeliculasAsync();

            return View(peliculas);
        }

        public async Task<IActionResult> Details(int id)
        {
            Pelicula peliculas =
                await this.service.GetPeliculaByIdAsync(id);

            return View(peliculas);
        }

        public IActionResult PelisActor()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PelisActor(string actor)
        {
            List<Pelicula> peliculas =
                await this.service.GetPeliculasByActorAsync(actor);
            return View(peliculas);
        }

        public IActionResult CreatePeli()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePeli(Pelicula pelicula)
        {
            Pelicula p = await this.service.InsertarPeliculaAsync(pelicula);

            return RedirectToAction("Details", new { id = p.IdPelicula });
        }


        public async Task<IActionResult> UpdatePeli(int id)
        {
            Pelicula pelicula = await this.service.GetPeliculaByIdAsync(id);
            return View(pelicula);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePeli(Pelicula pelicula)
        {
            Pelicula p = await this.service.UpdatePeliculaAsync(pelicula);
            return RedirectToAction("Details", new { id = p.IdPelicula });
        }

        public async Task<IActionResult> EliminarPeli(int id)
        {
            await this.service.DeletePeliculaAsync(id);
            return RedirectToAction("Index");
        }

    }
}
