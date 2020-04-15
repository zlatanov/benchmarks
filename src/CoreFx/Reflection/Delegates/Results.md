``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.752 (1909/November2018Update/19H2)
Intel Core i7-8750H CPU 2.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.201
  [Host] : .NET Core 3.1.3 (CoreCLR 4.700.20.11803, CoreFX 4.700.20.12001), X64 RyuJIT
  Dry    : .NET Core 3.1.3 (CoreCLR 4.700.20.11803, CoreFX 4.700.20.12001), X64 RyuJIT

Job=Dry  Jit=RyuJit  Platform=X64  
Runtime=.NET Core 3.1  InvocationCount=100000000  IterationCount=1  
LaunchCount=1  RunStrategy=Monitoring  UnrollFactor=1  
WarmupCount=10  

```
|          Method |     Mean | Error | Ratio | Gen 0 | Gen 1 | Gen 2 | Allocated | Code Size |
|---------------- |---------:|------:|------:|------:|------:|------:|----------:|----------:|
|        Delegate | 4.563 ns |    NA |  1.00 |     - |     - |     - |         - |      35 B |
| ExpressionTrees | 2.592 ns |    NA |  0.57 |     - |     - |     - |         - |      35 B |
|   DynamicMethod | 3.384 ns |    NA |  0.74 |     - |     - |     - |         - |      35 B |
