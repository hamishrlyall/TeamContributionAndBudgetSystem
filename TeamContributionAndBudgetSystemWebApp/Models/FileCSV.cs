using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace TeamContributionAndBudgetSystemWebApp.Models
{
    /// <summary>
    /// Used when uploading CSV files.
    /// </summary>
    public class FileCSV
    {
        /// <summary>
        /// Raw file.
        /// </summary>
        [Display(Name = "Upload CSV File")]
        [Required(ErrorMessage = "You must enter a CSV file.")]
        public HttpPostedFileBase File { get; set; }

        /// <summary>
        /// Processed data.
        /// </summary>
        public List<List<string>> Data { get; set; }

        /// <summary>
        /// Use the File property to generate processed data.
        /// </summary>
        /// <returns>True if the process was successful, or false if not.</returns>
        public bool GenerateDataFromFile()
        {
            // Check if file is valid
            if ((File == null) || (File.ContentLength < 1)) return false;

            // Create a new data list
            Data = new List<List<string>>();

            // Get stream reader
            // This is used to read the data from the File
            System.IO.Stream stream = File.InputStream;
            System.IO.StreamReader reader = new System.IO.StreamReader(stream);

            // Extract data from file
            // Loop until end of file is reached
            int separator = 0;
            while (!reader.EndOfStream)
            {
                // Extract one line of text from the stream
                string lineText = reader.ReadLine();

                // Create a new entry for this line of data within the file
                List<string> line = new List<string>();

                // Loop through all characters in the line and break it into its components;
                bool inQuotes = false;
                bool anyQuotes = false;
                int afterLastComma = 0;
                int currentPosition = 0;
                string working;
                foreach (int c in lineText)
                {
                    // Check for quote character
                    // If no, then check if currently within a quoted section
                    if (c == '"')
                    {
                        anyQuotes = true;
                        inQuotes = !inQuotes; // Toggle between being within or not within quotes
                    }
                    else if (!inQuotes)
                    {
                        // Check if separator is still unknown
                        // Try to determine what it is 
                        if (separator == 0)
                        {
                            if (c == '\t')
                                separator = '\t';
                            else if (c == ',')
                                separator = ',';
                        }

                        // Check for separator
                        // If found then record the sub-section of the text line
                        if ((c == separator) && (separator != 0))
                        {
                            working = lineText.Substring(afterLastComma, currentPosition - afterLastComma);
                            line.Add(anyQuotes ? RemoveQuotes(working) : working);
                            afterLastComma = currentPosition + 1;
                            anyQuotes = false;
                        }
                    }

                    // Increment the record of the current position
                    currentPosition++;
                }

                // Record the last sub-section of the text line
                working = lineText.Substring(afterLastComma, currentPosition - afterLastComma);
                line.Add(anyQuotes ? RemoveQuotes(working) : working);

                // Add the components of this line to the overall file data
                Data.Add(line);
            }

            return true;
        }

        /// <summary>
        /// Remove potential quotes from the ends of a string, and convert two quotes ("") to one quote (").
        /// </summary>
        private static string RemoveQuotes(string text)
        {
            string working =
                ((text.First() == '"') && (text.Last() == '"')) ?
                text.Substring(1, text.Length - 2) :
                text;
            return working.Replace("\"\"", "\"");
        }
    }
}