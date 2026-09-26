using WebApplication1.BusinessLayer;
using WebApplication1.DataLayer;
using WebApplication1.Models;
using Xunit;

namespace WebApplication1.Tests;

public class UpdateStudentDetailsTests
{
    [Fact]
    public async Task UpsertStudent_DelegatesModelAndReturnsResult()
    {
        var model = new Studentdetails { rollno = 1 };
        var data = new RecordingStudentData { StudentResult = "Details Updated" };
        var service = new UpdateStudentDetails(data);

        var result = await service.updatestudentdetails(model);

        Assert.Equal("Details Updated", result);
        Assert.Same(model, data.StudentModel);
    }

    [Fact]
    public async Task FetchStudentName_DelegatesLookupParameters()
    {
        var data = new RecordingStudentData { StudentNameResult = "Taylor" };
        var service = new UpdateStudentDetails(data);

        var result = await service.FetchStudentName(12, 4);

        Assert.Equal("Taylor", result);
        Assert.Equal((12, 4), data.StudentLookup);
    }

    [Fact]
    public async Task FetchStudentNameByParents_DelegatesParentNames()
    {
        var data = new RecordingStudentData { StudentNameResult = "Taylor" };
        var service = new UpdateStudentDetails(data);

        var result = await service.FetchStudentNamebymf("Morgan", "Casey");

        Assert.Equal("Taylor", result);
        Assert.Equal(("Morgan", "Casey"), data.ParentLookup);
    }

    [Fact]
    public async Task FetchStudentAddress_DelegatesClassroom()
    {
        var data = new RecordingStudentData { AddressResult = "Main Street" };
        var service = new UpdateStudentDetails(data);

        var result = await service.FetchStudentAdress(4);

        Assert.Equal("Main Street", result);
        Assert.Equal(4, data.AddressLookup);
    }

    [Fact]
    public async Task UpsertEmployee_DelegatesModelAndReturnsResult()
    {
        var model = new EmployeeDetails { Employeeid = 8 };
        var data = new RecordingStudentData { EmployeeResult = "Employee details added." };
        var service = new UpdateStudentDetails(data);

        var result = await service.UpdateEmployeeDetails(model);

        Assert.Equal("Employee details added.", result);
        Assert.Same(model, data.EmployeeModel);
    }

    [Fact]
    public async Task FetchEmployeeName_DelegatesDepartment()
    {
        var data = new RecordingStudentData { EmployeeNameResult = "Taylor" };
        var service = new UpdateStudentDetails(data);

        var result = await service.Fetchemployeename("Engineering");

        Assert.Equal("Taylor", result);
        Assert.Equal("Engineering", data.DepartmentLookup);
    }

    private sealed class RecordingStudentData : IStudentdata
    {
        public string StudentResult { get; init; } = "student";
        public string StudentNameResult { get; init; } = "student name";
        public string AddressResult { get; init; } = "address";
        public string EmployeeResult { get; init; } = "employee";
        public string EmployeeNameResult { get; init; } = "employee name";
        public Studentdetails? StudentModel { get; private set; }
        public EmployeeDetails? EmployeeModel { get; private set; }
        public (int, int) StudentLookup { get; private set; }
        public (string, string) ParentLookup { get; private set; } = ("", "");
        public int AddressLookup { get; private set; }
        public string? DepartmentLookup { get; private set; }

        public Task<string> updatestudentdetails(Studentdetails studentdetails)
        {
            StudentModel = studentdetails;
            return Task.FromResult(StudentResult);
        }

        public Task<string> FetchStudentName(int rollNumber, int classroom)
        {
            StudentLookup = (rollNumber, classroom);
            return Task.FromResult(StudentNameResult);
        }

        public Task<string> FetchStudentNamebymf(string FatherName, string MotherName)
        {
            ParentLookup = (FatherName, MotherName);
            return Task.FromResult(StudentNameResult);
        }

        public Task<string> FetchStudentAdress(int clasroom)
        {
            AddressLookup = clasroom;
            return Task.FromResult(AddressResult);
        }

        public Task<string> UpdateEmployeeDetails(EmployeeDetails employeeDetails)
        {
            EmployeeModel = employeeDetails;
            return Task.FromResult(EmployeeResult);
        }

        public Task<string> Fetchemployeename(string Department)
        {
            DepartmentLookup = Department;
            return Task.FromResult(EmployeeNameResult);
        }
    }
}
