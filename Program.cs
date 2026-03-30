using APDB_Cw1_s21417.Models;
using APDB_Cw1_s21417.Policies;
using APDB_Cw1_s21417.Services;

var penaltyCalculator = new DailyPenaltyCalculator(perDayRate: 5.00m);
var deviceService = new DeviceService();
var userService = new UserService();
var loanService = new LoanService(penaltyCalculator);
var reportService = new ReportService(deviceService, loanService, userService);

Console.WriteLine("=== Demo ===");
Console.WriteLine();

var laptop1 = new Laptop("Dell XPS 15", "Intel i7-13700H", 16);
var laptop2 = new Laptop("MacBook Pro 14", "Apple M3 Pro", 18);
var projector1 = new Projector("Epson EB-W51", "HDMI", "1920x1080");
var camera1 = new Camera("Canon EOS R6", 20.1, "RF 24-105mm");
var camera2 = new Camera("Sony A7 IV", 33.0, "FE 28-70mm");

deviceService.Add(laptop1);
deviceService.Add(laptop2);
deviceService.Add(projector1);
deviceService.Add(camera1);
deviceService.Add(camera2);

Console.WriteLine(">> Added 5 devices.");
Console.WriteLine(reportService.GenerateDeviceList());
Console.WriteLine();

var student1 = new Student("Anna", "Kowalska");
var student2 = new Student("Jan", "Nowak");
var employee1 = new Employee("Maria", "Wiśniewska");

userService.Add(student1);
userService.Add(student2);
userService.Add(employee1);

Console.WriteLine(">> Added 3 users:");
foreach (var u in userService.GetAll())
    Console.WriteLine($"   {u}");
Console.WriteLine();

var loan1 = loanService.Borrow(student1, laptop1, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(9));
var loan2 = loanService.Borrow(student1, camera1, DateTime.Now, DateTime.Now.AddDays(7));
var loan3 = loanService.Borrow(employee1, projector1, DateTime.Now, DateTime.Now.AddDays(14));

Console.WriteLine();
Console.WriteLine("* Attempting loan exceeding student limit:");
try
{
    loanService.Borrow(student1, laptop2, DateTime.Now, DateTime.Now.AddDays(7));
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"   Error: {ex.Message}");
}

Console.WriteLine();
Console.WriteLine("* Attempting to borrow already-borrowed device:");
try
{
    loanService.Borrow(student2, laptop1, DateTime.Now, DateTime.Now.AddDays(7));
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"   Error: {ex.Message}");
}

Console.WriteLine();
Console.WriteLine("* Marking device unavailable and attempting to borrow:");
deviceService.MarkUnavailable(laptop2.Id);
try
{
    loanService.Borrow(student2, laptop2, DateTime.Now, DateTime.Now.AddDays(7));
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"   Error: {ex.Message}");
}

Console.WriteLine();
Console.WriteLine("* Return on time:");
loanService.Return(loan2.Id, DateTime.Now);

Console.WriteLine();
Console.WriteLine("* Late return (3 days overdue):");
loanService.Return(loan1.Id, loan1.DueDate.AddDays(3));

Console.WriteLine();
Console.WriteLine(reportService.GenerateUserLoanReport(employee1.Id));

Console.WriteLine();
Console.WriteLine(reportService.GenerateOverdueReport());

Console.WriteLine();
Console.WriteLine(reportService.GenerateSummary());
