using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PostOffice_Application.Models
{
    public class Parcel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ParcelNumber { get; set; }
        public string Addresser { get; set; }
        public string Addressee { get; set; }
        public Statuses Status { get; set; }
        public DateTime Date { get; set; }
    }
}
