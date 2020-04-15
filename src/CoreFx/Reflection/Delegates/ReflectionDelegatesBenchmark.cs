using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;

namespace CoreFx.Reflection.Delegates
{
    [Config( typeof( Config ) )]
    public class ReflectionDelegatesBenchmark
    {
        private class Config : ManualConfig
        {
            public Config()
            {
                AddJob( Job.Dry.WithJit( Jit.RyuJit )
                               .WithPlatform( Platform.X64 )
                               .WithRuntime( CoreRuntime.Core31 )
                               .WithStrategy( BenchmarkDotNet.Engines.RunStrategy.Monitoring )
                               .WithWarmupCount( 10 )
                               .WithInvocationCount( 100_000_000 ) );

                AddDiagnoser( MemoryDiagnoser.Default );
                AddDiagnoser( new DisassemblyDiagnoser( new DisassemblyDiagnoserConfig( printSource: true ) ) );
            }
        }


        private readonly Test _object = new Test();
        private MethodInfo _method;
        private Func<Test, Test> _delegate;
        private Func<Test, Test> _expression;
        private Func<Test, Test> _dynamicMethod;


        [GlobalSetup]
        public void Setup()
        {
            _method = typeof( Test ).GetMethod( nameof( Test.GetValue ) );
            _delegate = (Func<Test, Test>)_method.CreateDelegate( typeof( Func<Test, Test> ) );

            var expressionParameter = Expression.Parameter( typeof( Test ) );
            _expression = Expression.Lambda<Func<Test, Test>>( Expression.Call( expressionParameter, _method ), expressionParameter ).Compile();

            var dynamicMethod = new DynamicMethod( "GetValue", typeof( Test ), new Type[] { typeof( Test ) }, restrictedSkipVisibility: true );
            var il = dynamicMethod.GetILGenerator();

            il.Emit( OpCodes.Ldarg_0 );
            il.Emit( OpCodes.Call, _method );
            il.Emit( OpCodes.Ret );

            _dynamicMethod = (Func<Test, Test>)dynamicMethod.CreateDelegate( typeof( Func<Test, Test> ) );
        }


        [Benchmark( Baseline = true )]
        public void Delegate() => _delegate( _object );


        [Benchmark]
        public void ExpressionTrees() => _expression( _object );


        [Benchmark]
        public void DynamicMethod() => _dynamicMethod( _object );


        private sealed class Test
        {
            public Test GetValue() => this;
        }
    }
}
