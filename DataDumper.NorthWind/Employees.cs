using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataDumper.NorthWind
{
    public class Employees
    {
        public Employees()
        {
            AddressDetails = new AddressDetails();
        }

        [Key]
        public int EmployeeId { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string Title { get; set; }
        public string TitleOfCourtesy { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? HireDate { get; set; }
        public AddressDetails AddressDetails { get; set; }
        public string HomePhone { get; set; }
        public string Extension { get; set; }
        public byte[] Photo { get; set; }
        public string Notes { get; set; }
        public int? ReportsTo { get; set; }
        public string PhotoPath { get; set; }

        [ForeignKey("ReportsTo")]
        public virtual Employees Superior { get; set; }

        public virtual ICollection<Employees> Subordinates { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<Territories> Territories { get; set; }
    }

    public class EmployeesConfiguration : EntityTypeConfiguration<Employees>
    {
        public EmployeesConfiguration()
        {
            HasMany(x => x.Territories)
                .WithMany(x => x.Employees)
                .Map(m => m.MapLeftKey("EmployeeId")
                    .MapRightKey("TerritoryId")
                    .ToTable("EmployeeTerritories"));
        }
    }
}