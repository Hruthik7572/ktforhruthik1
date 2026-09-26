using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;
using Xunit;

namespace WebApplication1.Tests;

public class ValidationTests
{
    [Fact]
    public void StudentDetails_WithAllRequiredValues_IsValid()
    {
        var model = new Studentdetails
        {
            rollno = 12, studentname = "Alex", fathername = "Sam", mothername = "Lee",
            classroom = 4, address = "Main Street"
        };

        Assert.Empty(Validate(model));
    }

    [Fact]
    public void StudentDetails_WithMissingName_IsInvalid()
    {
        var model = new Studentdetails
        {
            rollno = 12, fathername = "Sam", mothername = "Lee", classroom = 4, address = "Main Street"
        };

        Assert.Contains(Validate(model), result => result.MemberNames.Contains(nameof(Studentdetails.studentname)));
    }

    [Fact]
    public void StudentDetails_WithNonPositiveClassroom_IsInvalid()
    {
        var model = new Studentdetails
        {
            rollno = 12, studentname = "Alex", fathername = "Sam", mothername = "Lee",
            classroom = 0, address = "Main Street"
        };

        Assert.Contains(Validate(model), result => result.MemberNames.Contains(nameof(Studentdetails.classroom)));
    }

    [Fact]
    public void EmployeeDetails_WithValidValues_IsValid()
    {
        var model = new EmployeeDetails
        {
            Employeeid = 7, Employeename = "Jordan", Department = "Engineering", Salary = 50000m
        };

        Assert.Empty(Validate(model));
    }

    [Fact]
    public void EmployeeDetails_WithNegativeSalary_IsInvalid()
    {
        var model = new EmployeeDetails
        {
            Employeeid = 7, Employeename = "Jordan", Department = "Engineering", Salary = -1m
        };

        Assert.Contains(Validate(model), result => result.MemberNames.Contains(nameof(EmployeeDetails.Salary)));
    }

    [Fact]
    public void EmployeeDetails_WithMissingId_IsInvalid()
    {
        var model = new EmployeeDetails
        {
            Employeename = "Jordan", Department = "Engineering", Salary = 50000m
        };

        Assert.Contains(Validate(model), result => result.MemberNames.Contains(nameof(EmployeeDetails.Employeeid)));
    }

    private static List<ValidationResult> Validate(object model)
    {
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(model, new ValidationContext(model), results, validateAllProperties: true);
        return results;
    }
}
