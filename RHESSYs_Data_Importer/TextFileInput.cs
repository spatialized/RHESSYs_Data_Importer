using System;
using System.IO;
using RHESSYs_Data_Importer.Models;
using RHESSYs_Data_Importer.DAL;
using System.Diagnostics;
using System.Resources;
using Newtonsoft.Json;

public static class TextFileInput
{
    /// <summary>
    /// Initializes cube data arrays from data file.
    /// </summary>
    /// <param name="dataFile">Data file.</param>
    public static void ReadData(string folderAggregate, string folderCubes)
    {
        RHESSYsDAL dal = new RHESSYsDAL();

        try
        {
            Console.WriteLine("Importing files from folder: "+ folderAggregate);

            foreach (string file in Directory.EnumerateFiles(folderAggregate))
            {
                int patchIdx = -1;
                int warmingIdx = -1;

                string fileName = Path.GetFileNameWithoutExtension(file);
                //Console.WriteLine("Found data file: " + fileName);

                if (fileName.Contains("hist"))
                {
                    warmingIdx = 0;
                }
                else
                {
                    string[] parts = file.Split('.')[0].Split("_fire");
                    string warmingStr = parts[0].Substring(parts[0].Length-1);
                    int warmingDegrees = int.Parse(warmingStr);
                    switch (warmingDegrees)
                    {
                        case 1:
                            warmingIdx = 1;
                            break;
                        case 2:
                            warmingIdx = 2;
                            break;
                        case 4:
                            warmingIdx = 3;
                            break;
                        case 6:
                            warmingIdx = 4;
                            break;
                        default:
                            warmingIdx = -1;
                            break;
                    }
                }

                //Console.WriteLine("Set warmingIdx: " + warmingIdx);

                List<string> lines = ReadFromFile(file);
                //Console.WriteLine("Read " + lines.Count +" lines from file "+fileName);

                int count = 0;
                foreach (string line in lines)
                {
                    if (count > 0)
                        AddDataPoint(line, count, warmingIdx, patchIdx);

                    count++;
                }
            }

            Console.WriteLine("Importing files from folder: " + folderCubes);

            foreach (string file in Directory.EnumerateFiles(folderCubes))
            {
                int warmingIdx = -1;

                string fileName = Path.GetFileNameWithoutExtension(file);

                string[] parts = fileName.Split('_');
                string patchIdxStr = parts[0].Split('p')[1];
                int patchIdx = int.Parse(patchIdxStr);

                if (fileName.Contains("hist"))
                {
                    warmingIdx = 0;
                }
                else
                {
                    parts = file.Split('.')[0].Split("_fire");
                    string warmingStr = parts[0].Substring(parts[0].Length - 1);
                    int warmingDegrees = int.Parse(warmingStr);

                    Console.WriteLine("Set patchIdx: " + patchIdx);

                    switch (warmingDegrees)
                    {
                        case 1:
                            warmingIdx = 1;
                            break;
                        case 2:
                            warmingIdx = 2;
                            break;
                        case 4:
                            warmingIdx = 3;
                            break;
                        case 6:
                            warmingIdx = 4;
                            break;
                        default:
                            warmingIdx = -1;
                            break;
                    }
                }

                List<string> lines = ReadFromFile(file);
                int count = 0;
                foreach (string line in lines)
                {
                    if (count > 0)
                        AddDataPoint(line, count, warmingIdx, patchIdx);

                    count++;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        finally
        {
            Console.WriteLine("Executing finally block.");
        }

        void AddDataPoint(string line, int newDateIdx, int newWarmingIdx, int newPatchIdx)
        {
            string[] str = line.Split(' ');

            CubeDataPoint data = new CubeDataPoint();

            if(newPatchIdx == -1)
            {
                //data.id; Primary key --> add in SQL Server
                data.dateIdx = newDateIdx;
                data.warmingIdx = newWarmingIdx;
                data.patchIdx = newPatchIdx;
                data.snow = float.Parse(str[1]);
                data.evap = float.Parse(str[2]);
                data.netpsn = float.Parse(str[3]);
                data.depthToGW = float.Parse(str[4]); ;
                data.vegAccessWater = float.Parse(str[5]);
                data.Qout = float.Parse(str[6]);
                data.litter = float.Parse(str[7]);
                data.soil = float.Parse(str[8]);
                data.heightOver = float.Parse(str[9]);
                data.transOver = float.Parse(str[10]);
                data.heightUnder = float.Parse(str[11]);
                //data.transUnder = float.Parse(str[12]);       // Not in data
                data.leafCOver = float.Parse(str[12]);
                data.stemCOver = float.Parse(str[13]);
                data.rootCOver = float.Parse(str[14]);
                data.leafCUnder = float.Parse(str[15]);
                data.stemCUnder = float.Parse(str[16]);
                data.rootCUnder = float.Parse(str[17]);
            }
            else
            {
                //data.id; Primary key --> add in SQL Server
                data.dateIdx = newDateIdx;
                data.warmingIdx = newWarmingIdx;
                data.patchIdx = newPatchIdx;
                data.snow = float.Parse(str[1]);
                data.evap = float.Parse(str[2]);
                data.netpsn = float.Parse(str[3]);
                data.depthToGW = float.Parse(str[4]); ;
                data.vegAccessWater = float.Parse(str[5]);
                data.Qout = float.Parse(str[6]);
                data.litter = float.Parse(str[7]);
                data.soil = float.Parse(str[8]);
                data.heightOver = float.Parse(str[9]);
                data.transOver = float.Parse(str[10]);
                data.heightUnder = float.Parse(str[11]);
                data.transUnder = float.Parse(str[12]);
                data.leafCOver = float.Parse(str[13]);
                data.stemCOver = float.Parse(str[14]);
                data.rootCOver = float.Parse(str[15]);
                data.leafCUnder = float.Parse(str[16]);
                data.stemCUnder = float.Parse(str[17]);
                data.rootCUnder = float.Parse(str[18]);
            }

            dal.AddDataPoint(data); 
        }
    }

    public static void ReadWaterData(string folderWater)
    {
        // List<WaterDataYear> waterData;          // List of formatted water data by warming idx.
        RHESSYsDAL dal = new RHESSYsDAL();

        try
        {
            //TextAsset patchExtTextAsset = (TextAsset)Resources.Load("WaterData/WaterData");
            string text = ReadFile(folderWater + "/" + "WaterData.json");
            List<WaterDataYear> waterData = JsonConvert.DeserializeObject<List<WaterDataYear>>(text);
            foreach (WaterDataYear year in waterData)
            {
                foreach (WaterDataMonth month in year.GetMonths())
                {
                    foreach (WaterDataFrame frame in month.GetFrames())
                    {
                        try
                        {
                            frame.index++;
                            dal.AddWaterDataFrame(frame);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("ReadWaterData()... ERROR... ex: "+ex.Message);
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            //Debug.Log("InitializeData()... waterData ERROR: " + e.Message);
        }
    }

    /// <summary>
    /// Initializes cube data arrays from data file.
    /// </summary>
    /// <param name="dataFile">Data file.</param>
    public static void ReadDates(string folderAggregate)
    {
        RHESSYsDAL dal = new RHESSYsDAL();

        try
        {
            foreach (string file in Directory.EnumerateFiles(folderAggregate))
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                Console.WriteLine("Importing dates from file: " + fileName);

                if (fileName.Contains("hist"))      // Take first file
                {
                    List<string> lines = ReadFromFile(file);
                    Console.WriteLine("Read " + lines.Count + " lines from file " + fileName);

                    int count = 0;
                    foreach (string line in lines)
                    {
                        if (count > 0)
                            AddDate(line);

                        if(count % 10000 == 0)
                            Console.WriteLine("Added " + count + " dates from file " + fileName);

                        count++;
                    }
                }
            }

        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        finally
        {
            Console.WriteLine("Finished...");
        }

        void AddDate(string line)
        {
            string[] str = line.Split(' ');

            Date date = new Date();

            //date.id; Primary key --> add in SQL Server
            date.year = int.Parse(str[18]);
            date.month = int.Parse(str[19]);
            date.day = int.Parse(str[20]);
            date.date = new DateTime(date.year, date.month, date.day);

            dal.AddDate(date);
        }
    }

    /// <summary>
    /// Text asset to list.
    /// </summary>
    /// <returns>The asset to list.</returns>
    /// <param name="ta">Ta.</param>
    private static List<string> ReadFromFile(string filePath)
    {
        List<string> lines = new List<string>();
        string line;

        Console.WriteLine("ReadFromFile()... filePath: " + filePath);

        // Pass the file path and file name to the StreamReader constructor
        StreamReader sr = new StreamReader(filePath);
        // Read the first line of text

        int ct = 0;
        line = sr.ReadLine();
        // Continue to read until you reach end of file
        while (line != null)
        {
            //Console.WriteLine(line);
            lines.Add(line);

            //Read the next line
            line = sr.ReadLine();
        }

        sr.Close();        // Close file
        return lines;
    }

    /// <summary>
    /// Text asset to list.
    /// </summary>
    /// <returns>The asset to list.</returns>
    /// <param name="ta">Ta.</param>
    private static string ReadFile(string filePath)
    {
        //List<string> lines = new List<string>();
        string text;

        Console.WriteLine("ReadFromFile()... filePath: " + filePath);

        // Pass the file path and file name to the StreamReader constructor
        StreamReader sr = new StreamReader(filePath);
        // Read the first line of text

        int ct = 0;
        text = sr.ReadToEnd();
        // Continue to read until you reach end of file
        //while (text != null)
        //{
        //    //Console.WriteLine(line);
        //    lines.Add(text);

        //    //Read the next line
        //    text = sr.ReadLine();
        //}

        sr.Close();        // Close file
        return text;
    }
}
