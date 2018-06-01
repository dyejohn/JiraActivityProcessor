using System;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Text;

namespace FeedReader
{
    class Program
    {
        private static StringBuilder _stb = new StringBuilder();

        static void Main(string[] args)
        {

            DateTime startingDate = new DateTime(2018, 5, 1);
            DateTime endingDate = startingDate.AddMonths(1);

            var reader = XmlReader.Create(@"C:\Users\way2t\downloads\jiraoutput.xml");
            var test = SyndicationFeed.Load(reader);

            StringBuilder stbOutput = new StringBuilder();


            foreach( SyndicationItem item in test.Items)
            {
                if (item.PublishDate > startingDate && item.PublishDate < endingDate)
                {
                    var cleanoutput = TrimOutHTMLTags(item.Title.Text);

                    if(!(cleanoutput.Contains("logged") && cleanoutput.Contains("hour")) )
                    {
                        stbOutput.Append("  " + item.PublishDate.ToString() + "  ");
                        stbOutput.AppendLine(cleanoutput);
                        stbOutput.AppendLine("");
                    }
                }
            }

            System.IO.StreamWriter output = new System.IO.StreamWriter("tasks.txt", false);

            output.Write(stbOutput.ToString());
            output.Close();
            
        }

        public static string TrimOutHTMLTags(string input)
        {
            _stb.Clear();
            bool skip = false;
            foreach(char i in input)
            {
                if(i == '<')
                {
                    skip = true;
                }


                if(!skip)
                {
                    _stb.Append(i);
                }
                if (i == '>')
                {
                    skip = false;
                }
            }

            return _stb.ToString();

        }
    }
}
