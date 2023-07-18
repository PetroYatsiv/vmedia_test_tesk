using HtmlAgilityPack;

namespace Proxy_test.Services;

public class UrlChanger : IUrlChanger
{
    public string ChangeUrl(string content)
    {
        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(content);

        var anchorTags = doc.DocumentNode.Descendants("a");

        foreach (var anchorTag in anchorTags)
        {
            string originalUrl = anchorTag.GetAttributeValue("href", "");

            if (!string.IsNullOrWhiteSpace(originalUrl) && !originalUrl.StartsWith("http") && !originalUrl.StartsWith("https"))
            {
                string newUrl = "https://localhost:7089" + originalUrl;

                anchorTag.SetAttributeValue("href", newUrl);
            }
        }

        string modifiedContent = doc.DocumentNode.OuterHtml;

        return modifiedContent;
    }
}
