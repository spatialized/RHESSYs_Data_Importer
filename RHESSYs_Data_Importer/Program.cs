// Program to import RHESSYs data into MSSQL database

// Data Folders
string folderAggregate = "C:\\Users\\Redux\\Documents\\FutureMountain\\aggregate";        // Folder location
string folderCubes = "C:\\Users\\Redux\\Documents\\FutureMountain\\fire_cubes";        // Folder location

Console.WriteLine("-- RHESSYS Data Importer v1.0 --");
Console.WriteLine("-- by David Gordon --");
Console.WriteLine("");
Console.WriteLine("Now Running...");

TextFileInput.ReadData(folderAggregate, folderCubes);

// See https://aka.ms/new-console-template for more information
