using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using csmatio.io;
using csmatio.types;

namespace CSMatIOTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool toggleCheck;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "MAT-files|*.mat|All files|*.*",
                Title = "Select a MAT-file"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var fileName = openFileDialog.FileName;

                txtOutput.Text = txtOutput.Text + "Attempting to read the file '" + fileName + "'...";
                try
                {
                    var mfr = new MatFileReader(fileName);
                    txtOutput.Text += "Done!\nMAT-file contains the following:\n";
                    txtOutput.Text += mfr.MatFileHeader + "\n";
                    foreach (var mla in mfr.Data)
                    {
                        txtOutput.Text = txtOutput.Text + mla.ContentToString() + "\n";
                    }
                }
                catch (IOException)
                {
                    txtOutput.Text = txtOutput.Text + "Invalid MAT-file!\n";
                    MessageBox.Show("Invalid binary MAT-file! Please select a valid binary MAT-file.",
                        "Invalid MAT-file", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            var mlList = new List<MLArray>();
            // Go through each of the options to add in the file
            if (chkCell.IsChecked == true)
            {
                mlList.Add(CreateCellArray());
            }
            if (chkStruct.IsChecked == true)
            {
                mlList.Add(CreateStructArray());
            }
            if (chkChar.IsChecked == true)
            {
                mlList.Add(CreateCharArray());
            }
            if (chkSparse.IsChecked == true)
            {
                mlList.Add(CreateSparseArray());
            }
            if (chkDouble.IsChecked == true)
            {
                mlList.Add(CreateDoubleArray());
            }
            if (chkSingle.IsChecked == true)
            {
                mlList.Add(CreateSingleArray());
            }
            if (chkInt8.IsChecked == true)
            {
                mlList.Add(CreateInt8Array());
            }
            if (chkUInt8.IsChecked == true)
            {
                mlList.Add(CreateUInt8Array());
            }
            if (chkInt16.IsChecked == true)
            {
                mlList.Add(CreateInt16Array());
            }
            if (chkUInt16.IsChecked == true)
            {
                mlList.Add(CreateUInt16Array());
            }
            if (chkInt32.IsChecked == true)
            {
                mlList.Add(CreateInt32Array());
            }
            if (chkUInt32.IsChecked == true)
            {
                mlList.Add(CreateUIn32Array());
            }
            if (chkInt64.IsChecked == true)
            {
                mlList.Add(CreateInt64Array());
            }
            if (chkUInt64.IsChecked == true)
            {
                mlList.Add(CreateUInt64Array());
            }
            if (chkImagMatrix.IsChecked == true)
            {
                mlList.Add(CreateImaginaryArray());
            }

            if (mlList.Count <= 0)
            {
                MessageBox.Show("You must select at least one MAT-file Creation Element in order to" +
                    " create a MAT-file.", "No MAT-file elements selected", MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                return;
            }

            // Get a filename name to write the file out to
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "MAT-Files|*.mat|All files|*.*"
            };
            
            if (saveFileDialog.ShowDialog() != true)
            {
                return;
            }
            
            var filename = saveFileDialog.FileName;

            txtOutput.Text += "Creating the MAT-file '" + filename + "'...";

            try
            {
                var mfw = new MatFileWriter(filename, mlList, chkCompress.IsChecked == true);
            }
            catch (Exception err)
            {
                MessageBox.Show("There was an error when creating the MAT-file: \n" + err,
                    "MAT-File Creation Error!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                txtOutput.Text += "Failed!\n";
                return;
            }

            txtOutput.Text += "Done!\nMAT-File created with the following data: \n";
            foreach (var mla in mlList)
                txtOutput.Text += mla.ContentToString() + "\n";
        }

        private void btnCheckEmAll_Click(object sender, RoutedEventArgs e)
        {
            toggleCheck = !toggleCheck;

            chkCell.IsChecked =
            chkStruct.IsChecked =
            chkChar.IsChecked =
            chkSparse.IsChecked =
            chkDouble.IsChecked =
            chkSingle.IsChecked =
            chkInt8.IsChecked =
            chkUInt8.IsChecked =
            chkInt16.IsChecked =
            chkUInt16.IsChecked =
            chkInt32.IsChecked =
            chkUInt32.IsChecked =
            chkInt64.IsChecked =
            chkUInt64.IsChecked =
            chkImagMatrix.IsChecked =
            toggleCheck;
        }

        private static MLArray CreateCellArray()
        {
            var names = new[] { "Hello", "World", "I am", "a", "MAT-file" };
            var cell = new MLCell("Names", new[] { names.Length, 1 });
            for (var i = 0; i < names.Length; i++)
                cell[i] = new MLChar(null, names[i]);
            return cell;
        }

        private static MLArray CreateStructArray()
        {
            var structure = new MLStructure("X", new[] { 1, 1 });
            structure["w", 0] = new MLUInt8("", new byte[] { 1 }, 1);
            structure["y", 0] = new MLUInt8("", new byte[] { 2 }, 1);
            structure["z", 0] = new MLUInt8("", new byte[] { 3 }, 1);
            return structure;
        }

        private static MLArray CreateCharArray()
        {
            return new MLChar("AName", "Hello from .NET 8.0 WPF!");
        }

        private static MLArray CreateSparseArray()
        {
            var sparse = new MLSparse("S", new[] { 3, 3 }, 0, 3);
            sparse.SetReal(1.5, 0, 0);
            sparse.SetReal(2.5, 1, 1);
            sparse.SetReal(3.5, 2, 2);
            return sparse;
        }

        private static MLArray CreateDoubleArray() => new MLDouble("Double", new[] { double.MaxValue, double.MinValue }, 1);

        private static MLArray CreateSingleArray() => new MLSingle("Single", new[] { float.MinValue, float.MaxValue }, 1);

        private static MLArray CreateInt8Array() => new MLInt8("Int8", new[] { sbyte.MinValue, sbyte.MaxValue }, 1);

        private static MLArray CreateUInt8Array() => new MLUInt8("UInt8", new[] { byte.MinValue, byte.MaxValue }, 1);

        private static MLArray CreateInt16Array()
        {
            return new MLInt16("Int16", new[] { short.MinValue, short.MaxValue }, 1);
        }

        private static MLArray CreateUInt16Array()
        {
            return new MLUInt16("UInt16", new[] { ushort.MinValue, ushort.MaxValue }, 1);
        }

        private static MLArray CreateInt32Array()
        {
            return new MLInt32("Int32", new[] { int.MinValue, int.MaxValue }, 1);
        }

        private static MLArray CreateUIn32Array()
        {
            return new MLUInt32("UInt32", new[] { uint.MinValue, uint.MaxValue }, 1);
        }

        private static MLArray CreateInt64Array()
        {
            return new MLInt64("Int64", new[] { long.MinValue, long.MaxValue }, 1);
        }

        private static MLArray CreateUInt64Array()
        {
            return new MLUInt64("UInt64", new[] { ulong.MinValue, ulong.MaxValue }, 1);
        }

        private static MLArray CreateImaginaryArray()
        {
            // Create a large, randomly generated imaginary array
            var myRealNums = new long[2000];
            var myImagNums = new long[2000];
            var numGen = new Random();
            for (var i = 0; i < myRealNums.Length; i++)
            {
                myRealNums[i] = numGen.Next(int.MinValue, int.MaxValue);
                myImagNums[i] = numGen.Next(int.MinValue, int.MaxValue);
            }
            var myImagArray =
                new MLInt64("IA", myRealNums, myImagNums, myRealNums.Length / 5);
            return myImagArray;
        }
    }
} 