using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFinderApp
{
    public interface IFileHelper
    {
        void Copy(string fromFile, string toFile);
    }
}
