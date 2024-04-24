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
    public class VaccineService : IVaccineService
    {
        private readonly IRepository<Vaccine> _vaccineRepository;

        public VaccineService(IRepository<Vaccine> vaccineRepository)
        {
            _vaccineRepository = vaccineRepository;
        }

        public Vaccine CreateNewVaccine(Vaccine vaccine)
        {
            return _vaccineRepository.Insert(vaccine);
        }

        public void DeleteVaccine(Guid id)
        {
            var vaccine_to_delte = this.GetVaccineById(id);
            _vaccineRepository.Delete(vaccine_to_delte);
        }

        public Vaccine GetVaccineById(Guid id)
        {
            return _vaccineRepository.Get(id);
        }

        public List<Vaccine> GetVaccines()
        {
            return _vaccineRepository.GetAll().ToList();
        }

        public Vaccine UpdateVaccine(Vaccine vaccine)
        {
            return _vaccineRepository.Update(vaccine);
        }
    }
}
