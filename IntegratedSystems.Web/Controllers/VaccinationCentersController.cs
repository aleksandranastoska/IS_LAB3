using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Repository;
using IntegratedSystems.Service.Interface;
using System.Reflection.PortableExecutable;
using IntegratedSystems.Service.Implementation;

namespace IntegratedSystems.Web.Controllers
{
    public class VaccinationCentersController : Controller
    {
        private readonly IVaccinationCenterService vaccinationCenterService;
        private readonly IPatientService patientService;

        public VaccinationCentersController(IVaccinationCenterService vaccinationCenterService, IPatientService patientService)
        {
            this.vaccinationCenterService = vaccinationCenterService;
            this.patientService = patientService;
        }


        // GET: VaccinationCenters
        public IActionResult Index()
        {
            return View(vaccinationCenterService.GetVaccinationCenters());
        }

        // GET: VaccinationCenters/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccinationCenter = vaccinationCenterService.GetVaccinationCenterById(id.Value);
            if (vaccinationCenter == null)
            {
                return NotFound();
            }

            return View(vaccinationCenter);
        }

        // GET: VaccinationCenters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VaccinationCenters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Name,Address,MaxCapacity,Id")] VaccinationCenter vaccinationCenter)
        {
            if (ModelState.IsValid)
            {
                vaccinationCenterService.CreateNewVaccinationCenter(vaccinationCenter);
                return RedirectToAction(nameof(Index));
            }
            return View(vaccinationCenter);
        }

        // GET: VaccinationCenters/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccinationCenter = vaccinationCenterService.GetVaccinationCenterById(id.Value);
            if (vaccinationCenter == null)
            {
                return NotFound();
            }
            return View(vaccinationCenter);
        }

        // POST: VaccinationCenters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Name,Address,MaxCapacity,Id")] VaccinationCenter vaccinationCenter)
        {
            if (id != vaccinationCenter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    vaccinationCenterService.UpdateVaccinationCenter(vaccinationCenter);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VaccinationCenterExists(vaccinationCenter.Id))
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
            return View(vaccinationCenter);
        }

        // GET: VaccinationCenters/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccinationCenter = vaccinationCenterService.GetVaccinationCenterById(id.Value);
            if (vaccinationCenter == null)
            {
                return NotFound();
            }

            return View(vaccinationCenter);
        }

        // POST: VaccinationCenters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var  vaccinationCenter = vaccinationCenterService.GetVaccinationCenterById(id);
            if (vaccinationCenter != null)
            {
                vaccinationCenterService.DeleteVaccinationCenter(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool VaccinationCenterExists(Guid id)
        {
            return vaccinationCenterService.GetVaccinationCenterById(id) != null;
        }

        public IActionResult CapacityReached()
        {
            return View();
        }

        public IActionResult AddNewPatient(Guid? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var vaccinationCenter = vaccinationCenterService.GetVaccinationCenterById(Id.Value);
            if (vaccinationCenter == null)
            {
                return NotFound();
            }
            if (vaccinationCenterService.GetVaccineList(Id.Value).ToList().Count >= vaccinationCenter.MaxCapacity)
            {
                return RedirectToAction(nameof(CapacityReached));
            }
            var patients = patientService.GetPatients();
            ViewBag.Patients = patients;
            ViewBag.VaccinationCenterId = Id;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNewPatient(string Manufacturer, DateTime DateTaken, Guid PatientId, Guid VaccinationCenterId)
        {
            Console.WriteLine(Manufacturer);
            Console.WriteLine(DateTaken);
            Console.WriteLine(PatientId);
            Console.WriteLine(VaccinationCenterId);
            if (ModelState.IsValid)
            {
                Console.WriteLine("ENTERED");
                var newVaccine = new Vaccine
                {
                    Id = Guid.NewGuid(),
                    Certificate = Guid.NewGuid(),
                    Manufacturer = Manufacturer,
                    DateTaken = DateTaken,
                    PatientId = PatientId,
                    VaccinationCenter = VaccinationCenterId
                };
                Console.WriteLine(newVaccine);
                Console.WriteLine(VaccinationCenterId);
                vaccinationCenterService.AddNewVaccination(VaccinationCenterId, newVaccine);
                Console.WriteLine("Added new vaccine");
                Console.WriteLine(newVaccine);
                return RedirectToAction(nameof(Details), new { id = VaccinationCenterId });

            }
            Console.WriteLine("FAIL");
            return View();
        }
    }
}
