using HtmlAgilityPack;
using System;
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

            var delimiter = htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"myCanvas\"]/div[5]/br[1]");
            var delimiter2 = htmlDoc.DocumentNode.SelectSingleNode("//div[contains(@class, 'noprint separator nomobile')]"); //<div class="noprint separator nomobile"></div>

            foreach (var item in htmlDoc.DocumentNode.SelectNodes($"//div[contains(@class, 'card cardBlock card-right flip')]")) //remove all flips
            {
                item.ParentNode.RemoveChild(item);
            }

            int classV = 1;
            //for (int classV = 13 - 1; classV >= 1; classV--)
            {
                var divList = htmlDoc.DocumentNode.SelectNodes($"//div[contains(@class, 'card card-left cardBlock class-{classV}')]");
                try
                {
                    var a = divList[0].ParentNode; 
                }
                catch (System.Exception e)
                {

                    Console.WriteLine($"{classV}\n{e}");
                  //  continue;
                }
                
                var currentListNode = divList[0].ParentNode;// htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"myCanvas\"]/div[5]");

                int cId = 0;

                Console.Write($"{classV} :");
                F("Трюк, ");
                for (int i = 1; i < 10; i++)
                {
                    F(i + " круг, ");
                    Console.Write($"{i} ");
                }


                currentListNode.AppendChild(delimiter);
                currentListNode.AppendChild(delimiter2);
                Console.WriteLine($"|{classV} - OK");

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