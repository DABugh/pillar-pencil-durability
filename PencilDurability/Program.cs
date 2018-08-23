using System;

namespace PencilDurability
{
    class Program
    {
        static void Main(string[] args)
        {
            Paper sheet = new Paper();
            Pencil pencil = new Pencil(100, 3, 150);
            pencil.Write("This is a test of a virtual pencil" + System.Environment.NewLine
                + "and a virtual piece of virtual paper.", sheet);
            pencil.Erase("test", sheet);
            pencil.Write("demo", sheet, 10);

            Console.WriteLine(sheet.Text);
        }
    }
}
