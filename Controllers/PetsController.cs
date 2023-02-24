using System.Net.NetworkInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pet_hotel.Models;
using Microsoft.EntityFrameworkCore;

namespace pet_hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public PetsController(ApplicationContext context) {
            _context = context;
        }

        // This is just a stub for GET / to prevent any weird frontend errors that 
        // occur when the route is missing in this controller
        [HttpGet]
        public IEnumerable<Pet> GetPets() {
            // Include the `petOwner` property
            // which is a list of `PetOwner` objects
            // .NET will do a JOIN for us!
            return _context.Pets.Include(pet => pet.petOwner);
        }

        [HttpPost]
        public Pet Post(Pet pet) 
        {
            _context.Add(pet);
            _context.SaveChanges();

            return pet;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            // Find the pet object by ID
            Pet pet = _context.Pets.Find(id);
            // Tell the database to remove the pet object
            _context.Pets.Remove(pet);
            // ...and save the changes to the database
            _context.SaveChanges();
        }

        [HttpPut("{id}/checkin")]
        public ActionResult<Pet> CheckinPet(int id)
        {
            Pet pet = _context.Pets.Find(id);
            if (pet.checkedInAt == null)
            {
                pet.checkedInAt = DateTime.Now;
                _context.SaveChanges();
                return pet;
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}/checkout")]
        public ActionResult<Pet> CheckoutPet(int id)
        {
            Pet pet = _context.Pets.Find(id);
            if (pet.checkedInAt == null)
            {
                return BadRequest();
            }
            else
            {
                pet.checkedInAt = null;
                _context.SaveChanges();
                return pet;
            }
        }

        // [HttpGet]
        // [Route("test")]
        // public IEnumerable<Pet> GetPets() {
        //     PetOwner blaine = new PetOwner{
        //         name = "Blaine"
        //     };

        //     Pet newPet1 = new Pet {
        //         name = "Big Dog",
        //         petOwner = blaine,
        //         color = PetColorType.Black,
        //         breed = PetBreedType.Poodle,
        //     };

        //     Pet newPet2 = new Pet {
        //         name = "Little Dog",
        //         petOwner = blaine,
        //         color = PetColorType.Golden,
        //         breed = PetBreedType.Labrador,
        //     };

        //     return new List<Pet>{ newPet1, newPet2};
        // }
    }
}
