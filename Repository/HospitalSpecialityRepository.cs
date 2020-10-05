using Doctor_Appointment.Infrastucture;
using Doctor_Appointment.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Doctor_Appointment.Repository
{
    public class HospitalSpecialityRepository
    {
        public ApplicationDbContext db;

        public HospitalSpecialityRepository()
        {
            this.db = new ApplicationDbContext();
        }

        public async Task<IEnumerable<HospitalSpeciality>> GetAllHospitalSpecialities()
        {
            return await this.db.hospitalSpecialities.ToListAsync();
        }

        public async Task<IEnumerable<Specialty>> GetAllSpeciallities(int HsId)
        {
            return await this.db.specialties.Where(s => s.HsId == HsId).ToListAsync();
        }

        
    }
}