using IntegratedSystems.Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Service.Interface
{
    public interface IVaccineService
    {
        public List<Vaccine> GetVaccines();
        public Vaccine GetVaccineById(Guid id);
        public Vaccine CreateNewVaccine(Vaccine vaccine);
        public Vaccine UpdateVaccine(Vaccine vaccine);
        public void DeleteVaccine(Guid id);
    }
}
