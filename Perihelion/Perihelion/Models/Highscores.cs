using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace Perihelion.Models
{
    class Highscores
    {
        string responseString;

        public Highscores()
        {

        }

        public HighscoreTable getScores(string modeid, string format)
        {
            string postString = "ModeID=" + modeid + "&Format=" + format;
            return parseToHighscoreTable(webPost("http://www.themineralpatch.com/PH/requestscores.php", postString));
        }

        private HighscoreTable parseToHighscoreTable(string tableString)
        {
            const string SERVER_VALID_DATA_HEADER = "SERVER_";
            if (tableString.Trim().Length < SERVER_VALID_DATA_HEADER.Length ||
            !tableString.Trim().Substring(0, SERVER_VALID_DATA_HEADER.Length).Equals(SERVER_VALID_DATA_HEADER)) return null;
            string toParse = tableString.Trim().Substring(SERVER_VALID_DATA_HEADER.Length);
            string[] ranks = new string[10];
            string[] names = new string[10];
            string[] infos = new string[10];
            int[] scores = new int[10];
            string[] rows = Regex.Split(toParse, "_ROW_");
            for (int i = 0; i < 10; i++)
            {
                if (rows.Length > i && rows[i].Trim() != "")
                {
                    string[] cols = Regex.Split(rows[i], "_COL_");
                    if (cols.Length == 4)
                    {
                        names[i] = cols[0].Trim();
                        infos[i] = cols[1].Trim();
                        scores[i] = int.Parse(cols[2]);
                        ranks[i] = cols[3];
                    }
                }
                else
                {
                    names[i] = "";
                    infos[i] = "";
                    scores[i] = 0;
                    ranks[i] = "";
                }
            }
            return new HighscoreTable(ranks, names, scores);
        }
        private string webPost(string _URI, string _postString)
        {
            const string REQUEST_METHOD_POST = "POST";
            const string CONTENT_TYPE = "application/x-www-form-urlencoded";
            Stream dataStream = null;
            StreamReader reader = null;
            WebResponse response = null;
            responseString = null;

            // Create a request using a URL that can receive a post.
            WebRequest request = WebRequest.Create(_URI);

            // Set the Method property of the request to POST.
            request.Method = REQUEST_METHOD_POST;

            // Create POST data and convert it to a byte array.
            string postData = _postString;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            // Set the ContentType property of the WebRequest.
            request.ContentType = CONTENT_TYPE;
        
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;
        
            // Get the request stream.
            dataStream = request.GetRequestStream();

            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);

            // Close the Stream object.
            dataStream.Close();

            // Get the response.
            response = request.GetResponse();

            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();

            // Open the stream using a StreamReader for easy access.
            reader = new StreamReader(dataStream);

            // Read the content.
            responseString = reader.ReadToEnd();

            // Clean up the streams.
            if (reader != null) 
                reader.Close();
            if (dataStream != null) 
                dataStream.Close();
            if (response != null) 
                response.Close();

            return responseString;
        }

        private string hashString(string _value)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.ASCII.GetBytes(_value);
            data = x.ComputeHash(data);
            string ret = "";
            for (int i = 0; i < data.Length; i++) ret += data[i].ToString("x2").ToLower();
            return ret;
        }

        public string sendScore(string modeid, string name, string info, int score)
        {
            string highscoreString = name + info + score + "SuperSecretPasswordString";
            string postString = "ModeID=" + modeid + "&Name=" + name + "&Info=" + info + "&Score=" + score + "&Hash=" + hashString(highscoreString);
            string response = null;
            response = webPost("http://www.themineralpatch.com/PH/newScore.php", postString);
            return response.Trim();
        }
    }
}
