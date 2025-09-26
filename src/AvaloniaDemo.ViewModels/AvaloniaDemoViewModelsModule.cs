global using AvaloniaDemo.Application;
global using AvaloniaDemo.Application.Contracts;
global using AvaloniaDemo.Domain.Shared.Localization.Resources.AvaloniaDemo;
global using DynamicData;
global using JetBrains.Annotations;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Localization;
global using ReactiveUI;
global using ReactiveUI.SourceGenerators;
global using Splat;
global using System.Collections.ObjectModel;
global using System.Reactive.Disposables;
global using System.Reactive.Linq;
global using Volo.Abp.DependencyInjection;
global using Volo.Abp.Modularity;

namespace AvaloniaDemo.ViewModels;

[DependsOn(typeof(AvaloniaDemoApplicationModule))]
[UsedImplicitly]
public class AvaloniaDemoViewModelsModule : AbpModule;
