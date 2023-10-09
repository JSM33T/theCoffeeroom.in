using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace theCoffeeroom.Api.Apps
{
   
    [ApiController]
    public class TagScraperController : ControllerBase
    {
        [HttpGet("api/instatags/{keyword}")]
        public async Task<IActionResult> GetTags(string keyword)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    string url = "https://www.tagsfinder.com/en-it/related/" + keyword;

                    string html = await httpClient.GetStringAsync(url);

                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(html);

                    // Select elements using XPath
                    HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//table[@class='table card-table']//tr/td//a//text()");

                    if (nodes != null)
                    {
                        string cleanedText = string.Join(" ", nodes.Select(node => node.InnerHtml.Trim()));
                        cleanedText = Regex.Replace(cleanedText, @"\s+", "\n").Trim();

                        return Ok(cleanedText);
                    }
                    else
                    {
                        return Ok("Tags not found on the page.");
                    }
                }
            }
            catch (HttpRequestException)
            {
                return BadRequest("Failed to fetch data from the website.");
            }
        }

        [HttpGet("api/tags2/{keyword}")]
        public async Task<IActionResult> GetTagsAgain(string keyword)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    string url = "https://www.tagsfinder.com/en-it/related/" + keyword;

                    string html = await httpClient.GetStringAsync(url);

                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(html);

                    // Select elements using XPath
                    HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//table[@class='table card-table']//text()");

                    if (nodes != null)
                    {
                        string cleanedText = string.Join(" ", nodes.Select(node => node.InnerHtml.Trim()));
                        cleanedText = Regex.Replace(cleanedText, @"\s+", "\n").Trim();

                        return Ok(cleanedText);
                    }
                    else
                    {
                        return Ok("Tags not found on the page.");
                    }
                }
            }
            catch (HttpRequestException)
            {
                return BadRequest("Failed to fetch data from the website.");
            }
        }

    }
}
