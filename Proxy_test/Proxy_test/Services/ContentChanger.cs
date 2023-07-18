using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Xml;

namespace Proxy_test.Services;

public class ContentChanger : IContentChanger
{
    public string ChangeContent(string content)
    {
        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(content);

        var textNodes = doc.DocumentNode.DescendantsAndSelf()
            .Where(n => n.NodeType == HtmlNodeType.Text);

        foreach (var node in textNodes)
        {
            string[] words = Regex.Split(node.InnerHtml, @"\W+");

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length == 6)
                {
                    words[i] += "™";
                }
            }

            node.InnerHtml = string.Join(" ", words);
        }

        string modifiedContent = doc.DocumentNode.OuterHtml;

        return modifiedContent;
    }
}
