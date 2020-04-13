
### To INotifyPropertyChanged or Not

The results are not surprising. For the moment the WPF Runtime is written 
to favour objects implementing INotifyPropertyChanged and in fact is almost
2 times faster.


``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.720 (1909/November2018Update/19H2)
Intel Core i7-8750H CPU 2.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.201
  [Host] : .NET Core 3.1.3 (CoreCLR 4.700.20.11803, CoreFX 4.700.20.12001), X64 RyuJIT
  Dry    : .NET Core 3.1.3 (CoreCLR 4.700.20.11803, CoreFX 4.700.20.12001), X64 RyuJIT

Job=Dry  Jit=RyuJit  Platform=X64  
Runtime=.NET Core 3.1  InvocationCount=50000  IterationCount=1  
LaunchCount=1  RunStrategy=Monitoring  UnrollFactor=1  
WarmupCount=10  

```
|         Method |    Mode |     Mean | Error | Ratio |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|--------------- |-------- |---------:|------:|------:|-------:|-------:|------:|----------:|
|      **BindPlain** |  **OneWay** | **7.530 μs** |    **NA** |  **1.00** | **0.2800** | **0.0800** |     **-** |    **1782 B** |
| BindObservable |  OneWay | 5.070 μs |    NA |  0.67 | 0.2400 | 0.0600 |     - |    1579 B |
|                |         |          |       |       |        |        |       |           |
|      **BindPlain** | **OneTime** | **4.158 μs** |    **NA** |  **1.00** | **0.2400** |      **-** |     **-** |    **1192 B** |
| BindObservable | OneTime | 2.801 μs |    NA |  0.67 | 0.2000 |      - |     - |    1016 B |
