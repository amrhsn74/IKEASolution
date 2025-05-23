﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Common.Enums;
using Microsoft.AspNetCore.Http;

namespace IKEA.BLL.DTOs.Employees
{
    public class CreatedEmployeeDto
    {
        [MaxLength(50,ErrorMessage="Max Length Of Name Is 50 Chars")]
        [MinLength(5,ErrorMessage="Min Length Of Name Is 5 Chars")]
        [Required(ErrorMessage = "Name Is Required")]
        public string Name { get; set; }
        [Range(22, 30, ErrorMessage = "Age Must Be Between 22 And 30")]
        public int? Age { get; set; }
        [RegularExpression(@"[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,18}-[a-zA-Z]{5,10}$",
            ErrorMessage = "Invalid Address,Must Be Like 123-Street-City-Country")]
        public string? Address { get; set; }
        public decimal Salary { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Display(Name = "Phone Number")]
        [Phone]
        public string? PhoneNumber { get; set; }
        [Display(Name = "Hiring Date")]
        public DateOnly HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
        [Display(Name = "Department")]
        public int? DepartmentId { get; set; }
        public IFormFile? Image { get; set; }
    }
}
