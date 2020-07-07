using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PostOffice_Application.Models;

namespace PostOffice_Application.Controllers
{
    [ApiController]
    [Route("api/parcels")]
    public class ParcelController : Controller
    {
        ApplicationContext db;
        public ParcelController(ApplicationContext context)
        {
            db = context;
            if (!db.Parcels.Any())
            {
                db.Parcels.Add(new Parcel { Addresser = "Иванов Иван Иванович", Addressee = "Степанов Степан Степанович" });
                db.Parcels.Add(new Parcel { Addresser = "Кириллов Кирилл Кириллович", Addressee = "Степанов Степан Степанович" });
                db.Parcels.Add(new Parcel { Addresser = "Посылка Отправитель Сергеевич", Addressee = "Посылка Получатель Дмитриевич" });
                db.SaveChanges();
            }
        }
        [HttpGet]
        public IEnumerable<Parcel> Get()
        {
            return db.Parcels
                .OrderBy(x => x.Date)
                .ToList();
        }

        [HttpGet("{id}")]
        public Parcel Get(int id)
        {
            Parcel parcel = db.Parcels.FirstOrDefault(x => x.Id == id);
            return parcel;
        }

        [HttpPost]
        public IActionResult Post(Parcel parcel)
        {
            if (ModelState.IsValid)
            {
                db.Parcels.Add(parcel);
                db.SaveChanges();
                StatusAutoChanger.StatusTask(parcel); // Изменение статуса каждой новой посылки на сервере
                return Ok(parcel);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public IActionResult Put(Parcel parcel)
        {
            if (ModelState.IsValid)
            {
                db.Update(parcel);
                db.SaveChanges();
                return Ok(parcel);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Parcel parcel = db.Parcels.FirstOrDefault(x => x.Id == id);
            if (parcel != null)
            {
                db.Parcels.Remove(parcel);
                db.SaveChanges();
            }
            return Ok(parcel);
        }
    }
}
