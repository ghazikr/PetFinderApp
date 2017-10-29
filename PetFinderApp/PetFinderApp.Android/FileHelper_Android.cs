using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PetFinderApp.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper_Android))]

namespace PetFinderApp.Droid
{
    internal class FileHelper_Android : IFileHelper
    {
        public void Copy(string fromFile, string toFile)
        {
            System.IO.File.Copy(fromFile, toFile);
        }
    }
}