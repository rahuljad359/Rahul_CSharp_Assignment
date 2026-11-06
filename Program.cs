using System.Net.Http;
using System.Text.Json;
using RapiddAssignment.Models;
using RapiddAssignment.Reports;


string apiUrl = "https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code=vO17RnE8vuzXzPJo5eaLLjXjmRW07law99QTD90zat9FfOQJKKUcgQ==";

using HttpClient client = new HttpClient();

string jsonData = await client.GetStringAsync(apiUrl);
var options = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
};

List<TimeEntry>? entries =
    JsonSerializer.Deserialize<List<TimeEntry>>(jsonData, options);

var employeeReports = entries
    .Where(x => x.DeletedOn == null)
    .Where(x => !string.IsNullOrWhiteSpace(x.EmployeeName))
    .Where(x => x.EndTimeUtc >= x.StarTimeUtc)
    .GroupBy(x => x.EmployeeName)
    .Select(group => new
    {
        EmployeeName = group.Key,

        TotalHours = group.Sum(x =>
            (x.EndTimeUtc - x.StarTimeUtc).TotalHours)
    })
    .OrderByDescending(x => x.TotalHours)
    .ToList();

HtmlReport report = new HtmlReport();

report.GenerateReport(employeeReports);

PieChartReport chartReport = new PieChartReport();

chartReport.GenerateChart(employeeReports);
Console.WriteLine("Press any key to exit...");
Console.ReadKey();