# CSMatIO WPF Demo Application

This is a .NET 8.0 WPF demo application for the CSMatIO library, which provides functionality for reading and writing MATLAB MAT-files in C#.

## Features

- Read MAT-files and display their contents
- Create MAT-files with various data types:
  - Cell arrays
  - Structures
  - Character arrays
  - Sparse arrays
  - Numeric arrays (Double, Single, Int8, UInt8, Int16, UInt16, Int32, UInt32, Int64, UInt64)
  - Imaginary matrices

## Requirements

- .NET 8.0 SDK or later
- Windows operating system

## Building and Running

1. Open the solution in Visual Studio 2022 or later
2. Build the solution
3. Run the CSMatIOTestWPF project

## Usage

1. To read a MAT-file, click the "Read MAT-File" button and select a file
2. To create a MAT-file:
   - Select the data types you want to include
   - Click the "Create MAT-File" button
   - Choose a location to save the file

The output window will display information about the operations performed. 