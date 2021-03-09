using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTMLParser_HtmlAgilityPack
{
    public class Program
    {
        public static List<tagsHierarchyModel> _tagsHierarchyList = new List<tagsHierarchyModel>();
        public static int indexCount = 1;

        public static void Main()
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.Load(@"C:\repo\Hackathon2021\Hackathon\html\index.html");
            getTagsHierarchyDetails(htmlDoc);

            foreach (var item in _tagsHierarchyList)
            {
                string TagValue = item.TagValue == "" ? "NULL" : item.TagValue;
                Console.Write(item.Id + "---" + item.TagName + "---" + TagValue + "---" + item.Index);
                Console.WriteLine();
            }

            Console.ReadLine();
        }

        public static void getTagsHierarchyDetails(HtmlDocument htmlDoc)
        {
            HtmlNode _htmlNode = htmlDoc.DocumentNode.SelectNodes("//body").FirstOrDefault();

            foreach (HtmlNode node in _htmlNode.ChildNodes)
            {
                checkNode(node);
            }
        }

        public static void checkNode(HtmlNode node)
        {

            foreach (HtmlNode _childNodes in node.ChildNodes)
            {
                if (_childNodes.HasChildNodes)
                {
                    checkNode(_childNodes);
                }
                else
                {
                    if (_childNodes.ParentNode.Name != "div" || _childNodes.Name == "input")
                    {
                        tagsHierarchyModel _tagsHierarchyModel = new tagsHierarchyModel();
                        _tagsHierarchyModel.Id = indexCount;
                        _tagsHierarchyModel.TagName = _childNodes.ParentNode.Name == "div" ? _childNodes.Name : _childNodes.ParentNode.Name;
                        _tagsHierarchyModel.TagValue = _childNodes.InnerText;
                        _tagsHierarchyModel.Index = indexCount;

                        _tagsHierarchyList.Add(_tagsHierarchyModel);

                        indexCount++;
                    }
                }
            }
        }
    }

    public class tagsHierarchyModel
    {
        public int Id;
        public string TagName { get; set; }
        public string TagValue { get; set; }
        public int Index { get; set; }
    }
}
