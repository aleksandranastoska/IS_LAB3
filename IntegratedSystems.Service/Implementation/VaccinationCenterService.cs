using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Repository.Interface;
using IntegratedSystems.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Service.Implementation
{
    public class VaccinationCenterService : IVaccinationCenterService
    {
        private readonly IRepository<VaccinationCenter> vaccinationCenterRepository;
        private readonly IRepository<Vaccine> vaccineRepository;

        public VaccinationCenterService(IRepository<VaccinationCenter> vaccinationCenterRepository, IRepository<Vaccine> vaccineRepository)
        {
            this.vaccinationCenterRepository = vaccinationCenterRepository;
            this.vaccineRepository = vaccineRepository;
        }

        public void AddNewVaccination(Guid id, Vaccine vaccine)
        {
            var vaccinationCenter = vaccinationCenterRepository.Get(id);
            vaccineRepository.Insert(vaccine);
            Console.WriteLine(vaccinationCenter.Name);
            if (vaccinationCenter != null)
            {
                vaccinationCenter.Vaccines = vaccinationCenter.Vaccines ?? new List<Vaccine>();
                vaccinationCenter.Vaccines.Add(vaccine);
                vaccinationCenterRepository.Update(vaccinationCenter);
            }
        }

        public VaccinationCenter CreateNewVaccinationCenter(VaccinationCenter vaccinationCenter)
        {
            return vaccinationCenterRepository.Insert(vaccinationCenter);
        }

        public void DeleteVaccinationCenter(Guid id)
        {
            var center_to_delete = vaccinationCenterRepository.Get(id);
            vaccinationCenterRepository.Delete(center_to_delete);  
        }

        public VaccinationCenter GetVaccinationCenterById(Guid id)
        {
            return vaccinationCenterRepository.Get(id);
        }

        public List<VaccinationCenter> GetVaccinationCenters()
        {
            return vaccinationCenterRepository.GetAll().ToList();
        }

        public IEnumerable<Vaccine> GetVaccineList(Guid id)
        {
            var center = vaccinationCenterRepository.Get(id);
            if (center != null && center.Vaccines != null)
            {
                return center.Vaccines; 
            }
            else
            {
                return Enumerable.Empty<Vaccine>();
            }
        }

        public VaccinationCenter UpdateVaccinationCenter(VaccinationCenter vaccinationCenter)
        {
            return vaccinationCenterRepository.Update(vaccinationCenter);
        }
    }
}
