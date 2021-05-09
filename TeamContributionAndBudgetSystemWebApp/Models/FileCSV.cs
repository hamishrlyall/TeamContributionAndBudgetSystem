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
        /// Contains the column names/titles.
        /// </summary>
        public string[] Header { get; set; }

        /// <summary>
        /// Contains a list of row data.
        /// Each row contains a list of values (one value per column).
        /// </summary>
        public List<string[]> Row { get; set; }

        /// <summary>
        /// Returns true if the CSV class contain data.
        /// </summary>
        public bool IsValid()
        {
            return (Header != null) && (Row != null);
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public FileCSV() { }

        /// <summary>
        /// Constructor which uses an uploaded file to setup the FileCSV class.
        /// </summary>
        /// <param name="file">The file which provides the data.</param>
        public FileCSV(HttpPostedFileBase file)
        {
            SetFromFile(file);
        }

        /// <summary>
        /// Use an uploaded file to setup this FileCSV class.
        /// </summary>
        /// <param name="file">The file which provides the data.</param>
        public void SetFromFile(HttpPostedFileBase file)
        {
            // Check if file is valid
            if ((file == null) || (file.ContentLength < 1)) throw new Exception("File is empty");

            // Get stream reader
            // This is used to read the data from the File
            System.IO.Stream stream = file.InputStream;
            System.IO.StreamReader reader = new System.IO.StreamReader(stream);

            // Get the first line of the CSV file, which should be the column names
            string lineText = reader.ReadLine();
            if (lineText == null) throw new Exception("File is empty");

            // Get line component separator
            int separator = 0;
            if (separator == 0) separator = DetermineSeparator(lineText, ',');

            // Get column names
            Header = BreakLineApart(lineText, 0, separator);

            // Get the number of column which are expected for each row
            // This should be the number of Header columns, plus 1 for any comments
            int expectedColumnsPerRow = Header.Length + 1;

            // Extract the rest of the data from the CSV file
            // This will be the row data
            Row = new List<string[]>();
            while (!reader.EndOfStream)
            {
                // Extract one line of text from the stream
                lineText = reader.ReadLine();

                // Break the line into its components and add it to the list of rows
                Row.Add(BreakLineApart(lineText, expectedColumnsPerRow, separator));
            }
        }

        /// <summary>
        /// Record a comment within a given row.
        /// </summary>
        /// <param name="row">The row which is to receive the comment.</param>
        /// <param name="comment">The comment to be added to the row.</param>
        public void SetComment(string[] row, string comment)
        {
            row[Header.Length] = comment;
        }

        /// <summary>
        /// Generate a CSV file containing all rows which encountered errors while running SetFromFile().
        /// </summary>
        /// <returns>A string containing the generated CSV file.</returns>
        public string GenerateErrorFile()
        {
            // Create a buffer to store the result
            System.Text.StringBuilder buffer = new System.Text.StringBuilder();

            // Define a new-line string
            const string newLine = "\r\n";

            // Add column titles/headers
            bool isFirstColumn = true;
            foreach (string column in Header)
            {
                if (isFirstColumn) isFirstColumn = false; else buffer.Append(',');
                buffer.Append(column);
            }
            buffer.Append(newLine);

            // Loop through all data rows
            foreach (string[] row in Row)
            {
                // Check if the row has an error recorded
                if ((row.Length >= Header.Length) && (row[Header.Length].Length > 0))
                {
                    // Add the row to the result
                    for (int column=0; column <= Header.Length; column++)
                    {
                        if (isFirstColumn) isFirstColumn = false; else buffer.Append(',');
                        buffer.Append(row[column]);
                    }
                    buffer.Append(newLine);
                }
            }

            // Return result
            return buffer.ToString();
        }

        /// <summary>
        /// Find the first instance of a usual component separator and return it.
        /// </summary>
        /// <param name="lineText">A line of a CSV which is likely to contain a separator.</param>
        /// <param name="defaultSeparator">A default separator to return if to other is found.</param>
        /// <returns>The separator to use, which might be the default separator.</returns>
        private static int DetermineSeparator(string lineText, int defaultSeparator = ',')
        {
            // Loop through the line of text
            bool inQuotes = false;
            foreach (int c in lineText)
            {
                // Check for quote character
                // If no, then check if currently within a quoted section
                if (c == '"')
                {
                    inQuotes = !inQuotes; // Toggle between being within or not within quotes
                }
                else if (!inQuotes)
                {
                    // Check if a suitable separator has been found
                    if ((c == '\t') || (c == ',')) return c;
                }
            }

            // If here then no suitable separator was found
            return defaultSeparator;
        }

        /// <summary>
        /// Take a CSV line and break it appart into its components.
        /// </summary>
        /// <param name="lineText">A single line from a CSV file</param>
        /// <param name="minComponents">An optional parameter which specifies the minimum number of components to be returned.</param>
        /// <param name="separator">An optional parameter used to specify the character used to separate components within the line (usually a comma or tab).</param>
        /// <returns>A list of the line components.</returns>
        private static string[] BreakLineApart(string lineText, int minComponents = 0, int separator = ',')
        {

            // Create an empty list which will store the line components
            List<string> components = new List<string>();

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
                    // Check for separator
                    // If found then record the sub-section of the text line
                    if (c == separator)
                    {
                        working = lineText.Substring(afterLastComma, currentPosition - afterLastComma);
                        components.Add(anyQuotes ? RemoveQuotes(working) : working);
                        afterLastComma = currentPosition + 1;
                        anyQuotes = false;
                    }
                }

                // Increment the record of the current position
                currentPosition++;
            }

            // Record the last sub-section of the text line
            working = lineText.Substring(afterLastComma, currentPosition - afterLastComma);
            components.Add(anyQuotes ? RemoveQuotes(working) : working);

            // Make sure there are enough components in the list
            while (components.Count < minComponents) components.Add("");

            // Return list of line components
            return components.ToArray();
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

        /// <summary>
        /// Check if the headers from the uploaded file match the intended headers.
        /// If not then an exception will be thrown.
        /// </summary>
        /// <param name="intendedHeaders">A list of the excepted headers, in order.</param>
        public void ValidateHeaders(string[] intendedHeaders)
        {
            // Make sure headers exist
            if (Header == null) throw new Exception("No data uploaded.");

            // Loop through intended headers and make sure they match
            for (int i=0; i<intendedHeaders.Length; i++)
            {
                if ((i >= Header.Length) || (intendedHeaders[i].ToLower() != Header[i].Trim().ToLower()))
                {
                    string found =
                        ((i >= Header.Length) || (Header[i].Trim() == "")) ?
                        "nothing" :
                        ("\"" + Header[i] + "\"");
                    throw new Exception("Expected column " + (i + 1).ToString() + " to contain \"" + intendedHeaders[i] + "\" but found " + found);
                }
            }
        }
    }
}