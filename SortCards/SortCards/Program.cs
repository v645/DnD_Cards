using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace SortCards
{
    class Program
    {
        static void Main(string[] args)
        {
            var htmlDoc = new HtmlDocument();

            string filePath = @"E:\print.html";
            htmlDoc.Load(path, true);

            for (int classV = 0; classV < 10; classV++)
            {
                var divList = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'card card-left cardBlock class-4')]");
                var currentListNode = divList[0].ParentNode;// htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"myCanvas\"]/div[5]");

                List<string> divContents = new List<string>();

                foreach (var item in htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'card cardBlock card-right flip class-4')]")) //<div class="card cardBlock card-right flip class-4">
                {
                    currentListNode.RemoveChild(item);
                }

                var delimiter = htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"myCanvas\"]/div[5]/br[1]");
                var delimiter2 = htmlDoc.DocumentNode.SelectSingleNode("//div[contains(@class, 'noprint separator nomobile')]"); //<div class="noprint separator nomobile"></div>
                int cId = 0;

                F("Трюк, ");
                for (int i = 1; i < 10; i++)
                {
                    F(i + " круг, ");
                }

                currentListNode.AppendChild(delimiter);
                currentListNode.AppendChild(delimiter2);

                //// Write the text to the file
                //using (StreamWriter writer = new StreamWriter(filePath))
                //{
                //    writer.WriteLine(div.OuterHtml);
                //}

                void F(string level)
                {
                    foreach (var div in divList.Where(x => x.InnerHtml.Contains(level)))
                    {
                        currentListNode.RemoveChild(div);
                        var nPos = currentListNode.AppendChild(div);

                        if (cId++ >= 15)
                        {
                            currentListNode.AppendChild(delimiter);
                            currentListNode.AppendChild(delimiter2);
                            cId = 0;
                        }
                    }
                    //currentListNode.AppendChild(delimiter);
                    //currentListNode.AppendChild(delimiter2);
                }
            }
            htmlDoc.Save(filePath);
        }

        const string path = @"E:\V.html";
    }
}