using System;
using System.ComponentModel;
using System.Windows.Data;
using BenchmarkDotNet.Attributes;

namespace WPF.Bindings.NotifyPropertyChanged
{
    /// <summary>
    /// Lets see if binding to an object that has INotifyPropertyChanged is really faster
    /// than binding to an object that doesn't have it. Will there be a memory leak?
    /// </summary>
    [MemoryDiagnoser]
    [SimpleJob( warmupCount: 10, invocationCount: 30_000 )]
    public class NotifyPropertyChangedBenchmark
    {
        private Binding _bindingPlain;
        private Binding _bindingObservable;


        [Params( BindingMode.OneWay, BindingMode.OneTime )]
        public BindingMode Mode { get; set; }


        [GlobalSetup]
        public void Setup()
        {
            _bindingPlain = new Binding( "Name" )
            {
                Mode = Mode,
                Source = new PlainPerson( "Plain" )
            };
            _bindingObservable = new Binding( "Name" )
            {
                Mode = Mode,
                Source = new ObservablePerson
                {
                    Name = "Observable"
                }
            };
        }


        [Benchmark( Baseline = true )]
        public void BindPlain()
        {
            var obj = new BindingObject<String>();

            obj.SetBinding( _bindingPlain );
            obj.CheckValue( "Plain" );
        }


        [Benchmark]
        public void BindObservable()
        {
            var obj = new BindingObject<String>();

            obj.SetBinding( _bindingObservable );
            obj.CheckValue( "Observable" );
        }
    }
}
