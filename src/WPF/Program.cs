using System;
using BenchmarkDotNet.Running;
using WPF.Bindings.NotifyPropertyChanged;

namespace WPF
{
    public class Program
    {
        static void Main( String[] args )
            => BenchmarkSwitcher.FromAssembly( typeof( Program ).Assembly ).Run( args );
    }
}
