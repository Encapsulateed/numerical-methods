using System.Runtime.CompilerServices;

namespace ConsoleApp1
{
    internal class Program
    {
        delegate void Writer() ;
        static async Task Main(string[] args)
        {
            var i = 0;
            List<Writer> list = new List<Writer>(); 
            for(; i<10;i++) { list.Add(delegate { Console.Write(i); }); }

            foreach(Writer writer in list)
            {
                writer();
            }

        }

 
        
    }

    class Calc
    {
       public void fun( int a)
        {

        }
    }
}