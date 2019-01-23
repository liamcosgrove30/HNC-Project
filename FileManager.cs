using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LiamCOOPAssessment
{
    public class FileManager
    {
        #region Variables
        List<List<string>> attributes = new List<List<string>>();
        List<List<string>> contents = new List<List<string>>();
        enum LoadType { Attributes, Contents };
        LoadType type;
        List<string> tempAttributes;
        List<string> tempContents;
        bool identifierFound = false;
        #endregion

        #region Methods
        public void LoadContent(string filename, List<List<string>> attributes, List<List<string>> contents)
        {
            //reads line from a selected file
            using (StreamReader reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    //checks to see if reading attribute
                    if (line.Contains("Load ="))
                    {
                        tempAttributes = new List<string>();
                        //removes read line
                        line.Remove(0, line.IndexOf("="));
                        type = LoadType.Attributes;
                    }
                    // checks to see if reading content
                    else
                    {
                        tempContents = new List<string>();
                        type = LoadType.Contents;
                    }

                    //separates items with square brakets
                    string[] lineArray = line.Split(']');

                    foreach (string counter in lineArray)
                    {
                        string newLine = counter.Trim('[', ' ', ']');
                        //checks what is in line
                        if(newLine != String.Empty)
                            {
                                if (type == LoadType.Contents)
                                    tempContents.Add(newLine);
                                else
                                    tempAttributes.Add(newLine);
                            }
                    }

                    if (type == LoadType.Contents && tempContents.Count > 0)
                    {
                        contents.Add(tempContents);
                        attributes.Add(tempAttributes);
                    }
                }
           }
        }

        //override loadcontent
        public void LoadContent(string filename, List<List<string>> attributes, List<List<string>> contents, string identifier)
        {
            //reads line from a selected file
            using (StreamReader reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    //checks to see if it should stop reading
                    if(line.Contains("EndLoad=") && line.Contains(identifier))
                    {
                        identifierFound = false;
                        break;
                    }
                    else if(line.Contains("Load=") && line.Contains(identifier))
                    {
                        identifierFound = true;
                        continue;
                    }

                    if (identifierFound)
                    {
                        //checks to see if reading attribute
                        if (line.Contains("Load ="))
                        {
                            tempAttributes = new List<string>();
                            //removes read line
                            line.Remove(0, line.IndexOf("="));
                            type = LoadType.Attributes;
                        }
                        // checks to see if reading content
                        else
                        {
                            tempContents = new List<string>();
                            type = LoadType.Contents;
                        }

                        //separates items with square brakets
                        string[] lineArray = line.Split(']');

                        foreach (string counter in lineArray)
                        {
                            string newLine = counter.Trim('[', ' ', ']');
                            //checks what is in line
                            if (newLine != String.Empty)
                            {
                                if (type == LoadType.Contents)
                                    tempContents.Add(newLine);
                                else
                                    tempAttributes.Add(newLine);
                            }
                        }

                        if (type == LoadType.Contents && tempContents.Count > 0)
                        {
                            contents.Add(tempContents);
                            attributes.Add(tempAttributes);
                        }
                    }
                }
            }
        }
        #endregion
    }
}

