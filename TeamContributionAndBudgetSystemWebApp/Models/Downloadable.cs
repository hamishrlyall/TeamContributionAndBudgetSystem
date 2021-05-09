using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace TeamContributionAndBudgetSystemWebApp.Models
{
    /// <summary>
    /// Used to store data prior to being downloaded.
    /// </summary>
    public class Downloadable : System.Web.Mvc.FileResult
    {
        /// <summary>
        /// This is the contents of the file to be downloaded.
        /// </summary>
        public string FileContent { get; set; }

        /// <summary>
        /// Send a file to the client browser as a download.
        /// </summary>
        /// <param name="fileContent">A string representing the content of the CSV file.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="contentType">The type of HTTP MIME content.</param>
        public Downloadable(string fileContent, string fileName, string contentType) : base(contentType)
        {
            FileDownloadName = fileName;
            FileContent = fileContent;
        }

        /// <summary>
        /// Send a CSV file to the client browser as a download.
        /// </summary>
        /// <param name="fileContent">A string representing the content of the CSV file.</param>
        /// <param name="fileName">The name of the file.</param>
        public static Downloadable CreateCSV(string fileContent, string fileName)
        {
            return new Downloadable(fileContent, fileName, "text/csv");
        }


        protected override void WriteFile(HttpResponseBase response)
        {
            //response.Clear(); // Clear everything and start from a clean slate
            //response.Buffer = true;
            //response.AppendHeader("content-disposition", "attachment; filename=" + FileDownloadName);
            //response.AppendHeader("content-length", FileContent.Length.ToString());
            //response.ContentType = "text/csv";
            //response.Output.Write(FileContent);
            //response.Flush();
            //response.End();
            response.Write(FileContent);
        }
    }
}