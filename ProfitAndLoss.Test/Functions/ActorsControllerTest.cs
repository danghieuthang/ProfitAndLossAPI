using ProfitAndLoss.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ProfitAndLoss.Test.Functions
{
    public class DbFixture
    {
        private const string dbConnection = @"server=localhost;port=3306;database=efdemo;user=root;password=123456";
        public DbFixture()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContextPool<DataContext>(
                options => options.UseMySql(dbConnection)
                );

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; private set; }
    }
    public class ActorsControllerTest : IClassFixture<DbFixture>
    {
        private ServiceProvider _serviceProvide;

        public ActorsControllerTest(DbFixture fixture)
        {
            _serviceProvide = fixture.ServiceProvider;
        }

        //[Fact]
        //public void GetActorById()
        //{
        //    using (var context = _serviceProvide.GetService<DataContext>())
        //    {
        //        var controller = new ActorsController(context);
        //        var result = controller.GetActor(100).GetAwaiter();
        //        var actor = result.GetResult().Value as Actor;
        //        Assert.Equal(1, actor.ID);
        //        //Assert.Equal(actor.FirstName, "3");
        //        //Assert.Equal(actor.LastName, "1");
        //    }
        //}
    }
}
