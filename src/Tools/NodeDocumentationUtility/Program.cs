using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeDocumentationUtility
{
    class Program
    {
        static void Main(string[] args)
        {
            var module = new ZeroTouchModule("ProtoGeometry.dll");
            if(module.TypeExists("Autodesk.DesignScript.Geometry.Point"))
                Console.WriteLine("Point class exists.");
            else
                Console.WriteLine("Point class doesn't exist.");

            if (module.MethodExists("Autodesk.DesignScript.Geometry.Point", "ByCoordinates"))
                Console.WriteLine("Point.ByCoordinates() method exists.");
            else
                Console.WriteLine("Point.ByCoordinates() method doesn't exist.");

            if (module.PropertyExists("Autodesk.DesignScript.Geometry.Point", "X"))
                Console.WriteLine("Point.X property exists.");
            else
                Console.WriteLine("Point.X property doesn't exist.");

            if (module.MethodExists("Autodesk.DesignScript.Geometry.Point", "XYZ"))
                Console.WriteLine("Point.XYZ() method exists.");
            else
                Console.WriteLine("Point.XYZ() method doesn't exist.");

            if (module.PropertyExists("Autodesk.DesignScript.Geometry.Point", "W"))
                Console.WriteLine("Point.W property exists.");
            else
                Console.WriteLine("Point.W property doesn't exist.");
        }
    }
}
