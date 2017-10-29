using System;
using System.Collections.Generic;
using System.Text;

using Foundation;
using PetFinderApp.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper_iOS))]
namespace PetFinderApp.iOS
{
    internal class FileHelper_iOS
    {
        public void Copy(string fromFile, string toFile)
        {
            System.IO.File.Copy(fromFile, toFile);
        }
    }
}