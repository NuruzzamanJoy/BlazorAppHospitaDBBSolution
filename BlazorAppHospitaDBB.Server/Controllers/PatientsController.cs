using BlazorAppHospitaDBB.Server.Data;
using BlazorAppHospitaDBB.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppHospitaDBB.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public PatientsController(AppDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<ActionResult<List<Patient>>> Get()
        {
            return await _db.Patients.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> Get(int id)
        {
            var patient = await _db.Patients.Include(x => x.Addresses).FirstOrDefaultAsync(x => x.Id == id);
            if (patient == null) { return NotFound(); }
            return patient;
        }
        [HttpPost]
        public async Task<ActionResult<int>> Post(Patient patient)
        {
            _db.Patients.Add(patient);
            await _db.SaveChangesAsync();
            return patient.Id;
        }
        [HttpPut]
        public async Task<ActionResult> Put(Patient patient)
        {
            _db.Entry(patient).State = EntityState.Modified;
            foreach (var address in patient.Addresses)
            {
                if (address.Id != 0)
                {
                    _db.Entry(address).State = EntityState.Modified;
                }
                else
                {
                    _db.Entry(address).State = EntityState.Added;
                }
            }
            var idsOfAddresses = patient.Addresses.Select(x => x.Id).ToList();
            var addressToDelete = await _db.Addresses.Where(x => !idsOfAddresses.Contains(x.Id) && x.PatientId == patient.Id).ToListAsync();
            _db.RemoveRange(addressToDelete);
            await _db.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var patiient = await _db.Patients.Include(s => s.Addresses).FirstOrDefaultAsync(s => s.Id == id);
            if (patiient == null) { return NotFound(); }
            _db.Addresses.RemoveRange(patiient.Addresses);
            _db.Patients.Remove(patiient);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
