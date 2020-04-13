using System;
using System.ComponentModel;
using System.Windows.Data;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;

namespace WPF.Bindings.NotifyPropertyChanged
{
    /// <summary>
    /// Lets see if binding to an object that has INotifyPropertyChanged is really faster
    /// than binding to an object that doesn't have it.
    /// </summary>
    [Config( typeof( Config ) )]
    public class NotifyPropertyChangedBenchmark
    {
        private Binding _bindingPlain;
        private Binding _bindingObservable;

        private class Config : ManualConfig
        {
            public Config()
            {
                AddJob( Job.Dry.WithJit( Jit.RyuJit )
                               .WithPlatform( Platform.X64 )
                               .WithRuntime( CoreRuntime.Core31 )
                               .WithStrategy( BenchmarkDotNet.Engines.RunStrategy.Monitoring )
                               .WithWarmupCount( 10 )
                               .WithInvocationCount( 50_000 ) );

                AddDiagnoser( MemoryDiagnoser.Default );
            }
        }


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
