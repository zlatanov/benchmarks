using System;
using BenchmarkDotNet.Running;

namespace CoreFx
{
    public class Program
    {
        static void Main( String[] args )
            => BenchmarkSwitcher.FromAssembly( typeof( Program ).Assembly ).Run( args );
    }
}
