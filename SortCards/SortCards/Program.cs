using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SortCards
{
    class Program
    {
        static void Main(string[] args)
        {
            //V();

            var htmlDoc = new HtmlDocument();

            htmlDoc.Load(path,true);

            var divList = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'card card-left cardBlock class-4')]");

            List<string> divContents = new List<string>();

            string filePath = @"E:\print.html";

            // Write the text to the file
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                
            

            foreach (var div in divList.Where(x => x.InnerHtml.Contains("Трюк, ")))
            {
                    writer.WriteLine(div.OuterHtml);
                divContents.Add(div.OuterHtml);
            }
            }
        }

        const string path = @"E:\V.html";

        static void V()
        {
            // Read the HTML file
            string html = File.ReadAllText(path);

            // Find all <div> elements with class "card card-left cardBlock class-4"
            //string pattern = "\\d круг";
            string pattern = "трюк,";
            var divElements = html.Split(new[] { "<div class=\"card card-left cardBlock class-4", "</div>" }, StringSplitOptions.RemoveEmptyEntries).Where(x => x.Contains(pattern));

            Console.WriteLine($"{html.Length} divElements {divElements.Count()}");

            
            // Print the filtered strings
            foreach (var filteredString in divElements)
            {
                Console.WriteLine(filteredString);
            }

            Console.ReadLine();
          
            // Extract the <b class="type srtype"> elements from each <div> element
            var typeElements = divElements.Select(x => x.Split(new[] { "<b class=\"type srtype\">", "</b>" }, StringSplitOptions.RemoveEmptyEntries));

            foreach (var group in typeElements)
            {
                for (int i = 0; i < group.Length; i++)
                {
                    Console.WriteLine($"{i}::::{group[i]} ");
                }
                
            }

                // Group the <div> elements by the <b class="type srtype"> element
                var groupedDivElements = typeElements.GroupBy(x => x[1]);

            // Sort the groups by the <b class="type srtype"> element
            var sortedGroupedDivElements = groupedDivElements.OrderBy(x => x.Key);

            // Print the sorted groups
            foreach (var group in sortedGroupedDivElements)
            {
                Console.WriteLine($"Type: {group.Key}");
                //foreach (var divElement in group)
                //{
                //    Console.WriteLine(divElement[1]);
                //}
            }
        }
    }
}