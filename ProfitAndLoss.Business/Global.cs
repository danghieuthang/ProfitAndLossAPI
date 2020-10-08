using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProfitAndLoss.Business.Models;
using ProfitAndLoss.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProfitAndLoss.Business
{
    public class Global
    {
        public static IMapper Mapper { get; private set; }
        public static MapperConfigurationExpression BaseMapping { get; private set; }
        public Global()
        {

        }
        public static void Init()
        {
            Temple();
            BaseMapping = BaseMapping ?? new MapperConfigurationExpression();
            //var subClass = typeof(Mapping).Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(Mapping))).Select(type => type.GetType());
            var subClass = AppDomain.CurrentDomain.GetAssemblies()
                   .SelectMany(t => t.GetTypes())
                   .Where(t => typeof(Mapping).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);
            foreach (var item in subClass)
            {
                var source = item.BaseType?.GetGenericArguments().FirstOrDefault();
                if (source != null)
                {
                    BaseMapping.CreateMap(source, item).ReverseMap();
                }
            }
            var config = new MapperConfiguration(BaseMapping);
            Mapper = new Mapper(config);
        }
        public static void AddMapping<TSource, TDest>()
        {
            Init();
            BaseMapping.CreateMap<TSource, TDest>().ReverseMap();
            var config = new MapperConfiguration(BaseMapping);
            Mapper = new Mapper(config);
        }
        public static void Temple()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TestSource, TestDestination>());
            var mapper = config.CreateMapper();
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<TestSource, TestDestination>());
            var executionPlan = configuration.BuildExecutionPlan(typeof(TestSource), typeof(TestDestination));

            //var expression = context.Entities.ProjectTo<Dto>().Expression;
        }
        public static void InitServices(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            AppDomain.CurrentDomain.GetAssemblies().SelectMany(t => t.GetTypes())
                    .Where(t => t.Name.EndsWith("Service") && t.Name.StartsWith("I") 
                            && typeof(IBaseService).IsAssignableFrom(t) 
                            && !t.Equals(typeof(IBaseService))
                            && t.IsInterface 
                            && !t.IsGenericType)
                    .ToList()
                    .ForEach( types =>
                    {
                        if (types != null)
                        {
                            var serviceClass = AppDomain.CurrentDomain
                                                    .GetAssemblies()
                                                    .SelectMany(st => st.GetTypes())
                                                    .Where(st => st.Name.EndsWith("Service")
                                                                && types.IsAssignableFrom(st)
                                                                && st.IsClass
                                                                && !st.IsAbstract)
                                                    .ToList();
                            var serviceType = serviceClass
                                                .FirstOrDefault(s => !s.Name.Contains(nameof(IBaseService)));
                            if (serviceType != null)
                            {
                                services.AddScoped(types, serviceType);
                            }
                        }
                    });
        }
    }
    public class TestSource
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public int Description { get; set; }
    }
    public class TestDestination
    {
        public int Name { get; set; }
        public int Description { get; set; }
    }
}
