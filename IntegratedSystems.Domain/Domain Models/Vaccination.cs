using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Domain.Domain_Models
{
    public class Vaccination : BaseEntity
    {
        public Guid PatientId { get; set; }
        public virtual Patient? Patient { get; set; }
        public Guid VaccineId { get; set; }
        public virtual Vaccine? Vaccine { get; set; }
    }
}
