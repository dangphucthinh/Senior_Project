using Doctor_Appointment.Infrastucture;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Doctor_Appointment.Repository
{
    public class HospitalReturnModel
    {
        public int Id { get; set; }
        //public int AddressNumber { get; set; }
        //public string Street { get; set; }
        //public string City { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
       // public int AddressId { get; set; }
    }

    public class HospitalRepository
    {
        public ApplicationDbContext db;

        public HospitalRepository()
        {
            db = new ApplicationDbContext();
        }

        public async Task<IEnumerable<HospitalReturnModel>> getListHospital()
        {
            List<HospitalReturnModel> ret = await (from hos in db.hospitalCenters
                                                   select new HospitalReturnModel()
                                                   {
                                                       // user:
                                                       Id = hos.Id,
                                                       Name = hos.Name,
                                                       Address = hos.Address

                                                   }).ToListAsync<HospitalReturnModel>();
            return ret;
        }
    }
}