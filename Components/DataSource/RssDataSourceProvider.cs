
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Satrabel.DataSource;
using DotNetNuke.Modules.Html;
using DotNetNuke.Common.Utilities;

using System.Xml;
using System.ServiceModel.Syndication;

namespace Satrabel.DataSource
{
    public class RssDataSourceProvider : DataSourceProvider
    {
        

        public static RssFeed RssFeed(string url)
        {            
            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();
            List<RssItem> items = new List<RssItem>();
            foreach (SyndicationItem item in feed.Items)
            {
                String Title = item.Title.Text;
                String Summary = item.Summary.Text;
                string ImageUrl = "";
                string Author = "";
                var au =item.Authors.FirstOrDefault();
                if (au != null){
                    Author = au.Name;
                }

                var image = item.Links.SingleOrDefault(l => l.RelationshipType == "enclosure");
                if (image != null)
                {
                    ImageUrl = image.Uri.ToString();
                }
                var elem = item.ElementExtensions.SingleOrDefault(e=> e.OuterName=="thumbnail");
                if (elem != null) {
                    ImageUrl = elem.GetObject<System.Xml.Linq.XElement>().Attributes().Single(a => a.Name == "url").Value;                        
                   //<media:thumbnail width="144" height="96" url="http://localhost:1218/DesktopModules/Blog/BlogImage.ashx?TabId=101&amp;ModuleId=497&amp;Blog=1&amp;Post=291&amp;w=144&amp;h=96&amp;c=1&amp;key=922ffe16-bb67-4f2a-87c1-61486ca0a2e5" xmlns:media="http://search.yahoo.com/mrss/"></media:thumbnail>
                }
                RssItem i = new RssItem
                {
                    Url = item.Id,
                    PublishDate = item.PublishDate.LocalDateTime,
                    Title = Title,
                    Summary = item.Summary.Text,
                    ImageUrl = ImageUrl,
                    Author = Author
                };
                i.Categories.AddRange( item.Categories.Select(c => new CategoryItem() { Name = c.Name }));
                items.Add(i);
            }
            return new RssFeed() { Items = items};
        }
    }
    public class RssFeed 
    {
        public RssFeed() {
            Items = new List<RssItem>();
        }
        public List<RssItem> Items { get; set; }
    }
    public class RssItem 
    {
        public RssItem()
        {
            Categories = new List<CategoryItem>();
        }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }

        public string Author { get; set; }

        public List<CategoryItem> Categories { get; set; }
    }
    public class CategoryItem
    {
        public string Name { get; set; }
    }
}