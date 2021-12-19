using NUnit.Framework;
using CsvHelper;
using CsvHelper.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Threading;
using OpenQA.Selenium.Safari;
using System.Collections.Generic;
using System.Web;
String file = "myOutput.csv";
using (String csv = File.CreateText(file));
namespace WebScraping
{
    public class NUnitTest1
    {
        //declear all urls
        String test_url_1 = "https://www.youtube.com/c/LambdaTest/videos";
        String test_url_2 = "https://be.indeed.com";
        String test_url_3 = "https://manganato.com/";

        static Int32 vcount = 1;
        public IWebDriver driver;


        public void start_Browser()
        {
            /* Local Selenium WebDriver */
            driver = new ChromeDriver();

        }

        [Test(Description = "Web Scraping LambdaTest YouTube Channel"), Order(1)]
        public void YouTubeScraping()
        {
            driver.Url = test_url_1;
            /* Explicit Wait to ensure that the page is loaded completely by reading the DOM state */
            var timeout = 10000; /* Maximum wait time of 10 seconds */
            var wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(timeout));
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));

            Thread.Sleep(5000);

            /* Once the page has loaded, scroll to the end of the page to load all the videos */
            /* Scroll to the end of the page to load all the videos in the channel */
            /* Get scroll height */
            Int64 last_height = (Int64)(((IJavaScriptExecutor)driver).ExecuteScript("return document.documentElement.scrollHeight"));
            while (true)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.documentElement.scrollHeight);");
                /* Wait to load page */
                Thread.Sleep(2000);
                /* Calculate new scroll height and compare with last scroll height */
                Int64 new_height = (Int64)((IJavaScriptExecutor)driver).ExecuteScript("return document.documentElement.scrollHeight");
                if (new_height == last_height)
                    /* If heights are the same it will exit the function */
                    break;
                last_height = new_height;
            }

            By elem_video_link = By.CssSelector("ytd-grid-video-renderer.style-scope.ytd-grid-renderer");
            ReadOnlyCollection<IWebElement> videos = driver.FindElements(elem_video_link);
            csv.WriteLine("Total number of videos in " + test_url_1 + " are " + videos.Count);

            /* Go through the Videos List and scrap the same to get the attributes of the videos in the channel */
            foreach (IWebElement video in videos)
            {
                string str_title, str_views, str_rel;
                IWebElement elem_video_title = video.FindElement(By.CssSelector("#video-title"));
                str_title = elem_video_title.Text;

                IWebElement elem_video_views = video.FindElement(By.XPath(".//*[@id='metadata-line']/span[1]"));
                str_views = elem_video_views.Text;

                IWebElement elem_video_reldate = video.FindElement(By.XPath(".//*[@id='metadata-line']/span[2]"));
                str_rel = elem_video_reldate.Text;

                csv.WriteLine("******* Video Number " + vcount + " *******");
                csv.WriteLine("Title: " + str_title);
                csv.WriteLine("Number of Views: " + str_views);
                csv.WriteLine("Link : " + str_rel);
                csv.WriteLine("\n");
                vcount++;
            }
            csv.WriteLine("Scraping Data YouTube Done ");
        }
        public void LTBlogScraping()
        {
            driver.Url = test_url_2;
            /* Explicit Wait to ensure that the page is loaded completely by reading the DOM state */
            var timeout = 10000; /* Maximum wait time of 10 seconds */
            var wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(timeout));
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));

            Thread.Sleep(5000);

            /* Find total number of blogs on the page */
            By elem_blog_list = By.CssSelector("div.col-xs-12.col-md-12.blog-list");
            ReadOnlyCollection<IWebElement> blog_list = driver.FindElements(elem_blog_list);
            csv.WriteLine("Total number of videos in " + test_url_2 + " are " + blog_list.Count);

            /* Reset the variable from the previous test */
            vcount = 1;

            /* Go through the Blogs List and scrap the same to get the attributes of the blogs on the page*/
            foreach (IWebElement blog in blog_list)
            {
                string str_blog_title, str_blog_author, str_blog_views, str_blog_link;

                IWebElement elem_blog_title = blog.FindElement(By.ClassName("blog-titel"));
                str_blog_title = elem_blog_title.Text;

                IWebElement elem_blog_link = blog.FindElement(By.ClassName("blog-titel"));
                IWebElement elem_blog_alink = elem_blog_link.FindElement(By.TagName("a"));
                str_blog_link = elem_blog_alink.GetAttribute("href");

                IWebElement elem_blog_author = blog.FindElement(By.ClassName("user-name"));
                str_blog_author = elem_blog_author.Text;

                IWebElement elem_blog_views = blog.FindElement(By.ClassName("comm-count"));
                vcount++;
                str_blog_views = elem_blog_views.Text;
                csv.WriteLine("******* Count " + vcount + " *******");
                csv.WriteLine("Title: " + str_blog_title);
                csv.WriteLine("Author : " + str_blog_author);
                csv.WriteLine("Link : " + str_blog_link);
                csv.WriteLine("\n");

            }
        }

        public void trd_BlogScraping()
        {
            driver.Url = test_url_3;
            /* Explicit Wait to ensure that the page is loaded completely by reading the DOM state */
            var timeout = 10000; /* Maximum wait time of 10 seconds */
            var wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(timeout));
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));

            Thread.Sleep(5000);

            /* Find total number of blogs on the page */
            By elem_blog_list = By.CssSelector("div.col-xs-12.col-md-12.blog-list");
            ReadOnlyCollection<IWebElement> blog_list = driver.FindElements(elem_blog_list);
            csv.WriteLine("Total number of videos in " + test_url_2 + " are " + blog_list.Count);

            /* Reset the variable from the previous test */
            vcount = 1;

            /* Go through the Blogs List and scrap the same to get the attributes of the blogs on the page*/
            foreach (IWebElement blog in blog_list)
            {
                string str_blog_title, str_blog_author, str_blog_views, str_blog_link;

                IWebElement elem_blog_title = blog.FindElement(By.ClassName("blog-titel"));
                str_blog_title = elem_blog_title.Text;

                IWebElement elem_blog_link = blog.FindElement(By.ClassName("blog-titel"));
                IWebElement elem_blog_alink = elem_blog_link.FindElement(By.TagName("a"));
                str_blog_link = elem_blog_alink.GetAttribute("href");

                IWebElement elem_blog_author = blog.FindElement(By.ClassName("user-name"));
                str_blog_author = elem_blog_author.Text;

                IWebElement elem_blog_views = blog.FindElement(By.ClassName("comm-count"));
                str_blog_views = elem_blog_views.Text;
                vcount++;
                csv.WriteLine("******* Count " + vcount + " *******");
                csv.WriteLine("Title: " + str_blog_title);
                csv.WriteLine("Author : " + str_blog_author);
                csv.WriteLine("Link : " + str_blog_link);
                csv.WriteLine("\n");

            }
        }
        /* closee the browser */

        [TearDown]
        public void close_Browser()
        {
            driver.Quit();
        }
    }
}
