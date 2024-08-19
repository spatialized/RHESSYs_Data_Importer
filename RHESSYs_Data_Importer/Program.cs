// Program to import RHESSYs data into MSSQL or MySQL database
// Note: Change USE_MYSQL compilation symbol to switch between MySQL and MSSQL

bool importDates = true;
bool importData = true;

// Data Folders
string folderAggregate = "C:\\Users\\Redux\\Documents\\FutureMountain\\aggregate";        // Folder location
string folderCubes = "C:\\Users\\Redux\\Documents\\FutureMountain\\fire_cubes";        // Folder location

Console.WriteLine("-- RHESSYS Data Importer v1.0 --");
Console.WriteLine("-- by David Gordon --");
Console.WriteLine("");
Console.WriteLine("Running...");

// -- TO DO: Check that Dates table exists
//           Check that CubeData table exists

if(importDates)
    TextFileInput.ReadDates(folderAggregate);
if(importData)
    TextFileInput.ReadData(folderAggregate, folderCubes);

// See https://aka.ms/new-console-template for more information
