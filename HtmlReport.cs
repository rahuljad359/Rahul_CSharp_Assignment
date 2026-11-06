using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapiddAssignment.Reports
{
    public class HtmlReport
    {
        public void GenerateReport(dynamic employees)
        {
            StringBuilder html = new StringBuilder();

            html.AppendLine("<html>");
            html.AppendLine("<head>");
            html.AppendLine("<title>Employee Time Report</title>");

            html.AppendLine("<style>");
            html.AppendLine("table { border-collapse: collapse; width: 100%; }");
            html.AppendLine("th, td { border: 1px solid black; padding: 10px; }");
            html.AppendLine("th { background-color: #333; color: white; }");
            html.AppendLine(".low-hours { background-color: #ffcccc; }");
            html.AppendLine("</style>");

            html.AppendLine("</head>");
            html.AppendLine("<body>");

            html.AppendLine("<h2>Employee Time Report</h2>");

            html.AppendLine("<table>");
            html.AppendLine("<tr><th>Employee Name</th><th>Total Hours</th></tr>");

            foreach (var employee in employees)
            {
                string cssClass = employee.TotalHours < 100
                    ? "low-hours"
                    : "";

                html.AppendLine(
                    $"<tr class='{cssClass}'>" +
                    $"<td>{employee.EmployeeName}</td>" +
                    $"<td>{employee.TotalHours:F2}</td>" +
                    "</tr>");
            }

            html.AppendLine("</table>");
            html.AppendLine("</body>");
            html.AppendLine("</html>");

            Directory.CreateDirectory("Output");

            File.WriteAllText(
                "Output/EmployeeReport.html",
                html.ToString());

            Console.WriteLine("HTML Report Generated Successfully!");
        }
    }
}
