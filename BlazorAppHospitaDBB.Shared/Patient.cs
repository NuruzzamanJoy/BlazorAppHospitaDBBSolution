using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAppHospitaDBB.Shared
{
    public class Patient
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime AdmitDate { get; set; } = DateTime.Now;
        public List<Address> Addresses { get; set; } = new List<Address>();
    }
}
