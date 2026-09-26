using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Studentdetails
    {

        [Required, Range(1, int.MaxValue)]
        public int? rollno { get; set; }
        [Required, StringLength(100, MinimumLength = 1)]
        public string studentname { get; set; }
        [Required, StringLength(100, MinimumLength = 1)]
        public string fathername { get; set; }
        [Required, StringLength(100, MinimumLength = 1)]
        public string mothername { get; set; }
        [Required, Range(1, int.MaxValue)]
        public int? classroom { get; set; }
        [Required, StringLength(500, MinimumLength = 1)]
        public string address { get; set; }
    }
    public class EmployeeDetails {
        [Required, Range(1, int.MaxValue)]
        public int? Employeeid { get; set; }
        [Required, StringLength(100, MinimumLength = 1)]
        public string Employeename { get; set; }
        [Required, StringLength(100, MinimumLength = 1)]
        public string Department { get; set; }

        [Required, Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal? Salary { get; set; }
    }
}

