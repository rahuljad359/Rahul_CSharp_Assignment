using ScottPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapiddAssignment.Reports
{
    public class PieChartReport
    {
        public void GenerateChart(dynamic employees)
        {
            List<PieSlice> slices = new List<PieSlice>();
            var colors = new[]
                {
                    Colors.Red,
                    Colors.Blue,
                    Colors.Green,
                    Colors.Orange,
                    Colors.Purple,
                    Colors.Gold,
                    Colors.Cyan,
                    Colors.Magenta,
                    Colors.Brown,
                    Colors.DarkGreen
                };


            double totalHours = 0;

            foreach (var employee in employees)
            {
                totalHours += (double)employee.TotalHours;
            }

            for (int i = 0; i < employees.Count; i++)
            {
                var employee = employees[i];

                double hours = (double)employee.TotalHours;
                string name = (string)employee.EmployeeName;

                double percentage = (hours / totalHours) * 100;

                slices.Add(new PieSlice()
                {
                    Value = hours,
                    FillColor = colors[i % colors.Length],
                    Label = $"{name} ({percentage:F1}%)",
                    LegendText = name
                });
            }

            ScottPlot.Plot plot = new ScottPlot.Plot();

            var pie = plot.Add.Pie(slices);

            pie.SliceLabelDistance = 1.3;

            plot.ShowLegend();
            plot.Axes.Frameless();
            plot.HideGrid();

            Directory.CreateDirectory("Output");

            plot.SavePng("Output/EmployeeChart.png", 1000, 700);

            Console.WriteLine("Pie Chart Generated Successfully!");
        }
    }
}
