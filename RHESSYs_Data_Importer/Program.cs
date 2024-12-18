﻿// Program to import RHESSYs data into MSSQL or MySQL database
// Note: Change USE_MYSQL compilation symbol to switch between MySQL and MSSQL

bool importDates = false;
bool importCubeData = false;
bool importWaterData = false;
bool importFireData = false;
//bool importFireData = true;
//bool importPatchData = true;

// Data Folders
string folderAggregate = "C:\\Users\\Redux\\Documents\\FutureMountain\\aggregate";      
string folderCubes = "C:\\Users\\Redux\\Documents\\FutureMountain\\fire_cubes";
string folderWater = "C:\\Users\\Redux\\Documents\\FutureMountain\\water";
string folderFire = "C:\\Users\\Redux\\Documents\\FutureMountain\\fire"; 

Console.WriteLine("-- RHESSYS Data Importer v1.1 --");
Console.WriteLine("-- by David Gordon --");
Console.WriteLine("");
Console.WriteLine("Running...");

// -- TO DO: Check that Dates table exists
//           Check that CubeData table exists

if(importDates)
    TextFileInput.ReadDates(folderAggregate);
if(importCubeData)
    TextFileInput.ReadCubeData(folderAggregate, folderCubes);
if (importWaterData)
    TextFileInput.ReadWaterData(folderWater);
if (importFireData)
    TextFileInput.ReadFireData(folderFire);

Console.WriteLine("Finished importing data successfully...");

// See https://aka.ms/new-console-template for more information
