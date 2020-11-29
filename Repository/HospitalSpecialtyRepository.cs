using Doctor_Appointment.Infrastucture;
using Doctor_Appointment.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Doctor_Appointment.Repository
{
    public class HospitalSpecialtyRepository
    {
        public ApplicationDbContext db;

        public HospitalSpecialtyRepository()
        {
            this.db = new ApplicationDbContext();
        }

        public async Task<IEnumerable<HospitalSpecialty>> GetAllHospitalSpecialty()
        {
            return await this.db.hospitalSpecialties.ToListAsync();
        }

        public async Task<IEnumerable<HospitalSpecialty>> GetHospitalSpecialtyBySearch(string searchPhrase)
        {
            return await this.db.hospitalSpecialties.Where(h => h.Name.Contains(searchPhrase)).ToListAsync();
        }

        public async Task<IEnumerable<Specialty>> GetAllSpecialties(int HsId)
        {
            return await this.db.specialties.Where(s => s.HsId == HsId).ToListAsync();
        }
    }
}