using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obligatorisk1.Controllers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Obligatorisk1.Models;
using Obligatorisk1.Viewmodels;

namespace Obligatorisk1.Controllers.Tests
{
    [TestClass()]
    public class ComponentsControllerTests
    {
        private IQueryable<Component> Components = new List<Component>()
        {
            new Component()
            {
               ComponentName = "Com1"
            },
            new Component()
            {
               ComponentName = "Com2"
            },
            new Component()
            {
               ComponentName = "Com3"
            }

        }.AsQueryable();

        private IQueryable<Category> Categories = new List<Category>()
        {
            new Category()
            {
               Value = "Cat1"
            },
            new Category()
            {
               Value = "Cat2"
            },
             new Category()
            {
               Value = "Cat3"
            }

        }.AsQueryable();

        [TestMethod()]
        public void IndexTest()
        {
            var mockComSet = new Mock<DbSet<Component>>();
            mockComSet.As<IQueryable<Component>>().Setup(m => m.Provider).Returns(Components.Provider);
            mockComSet.As<IQueryable<Component>>().Setup(m => m.Expression).Returns(Components.Expression);
            mockComSet.As<IQueryable<Component>>().Setup(m => m.ElementType).Returns(Components.ElementType);
            mockComSet.As<IQueryable<Component>>().Setup(m => m.GetEnumerator()).Returns(Components.GetEnumerator());

            var mockCatSet = new Mock<DbSet<Category>>();
            mockCatSet.As<IQueryable<Category>>().Setup(m => m.Provider).Returns(Categories.Provider);
            mockCatSet.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(Categories.Expression);
            mockCatSet.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(Categories.ElementType);
            mockCatSet.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(Categories.GetEnumerator());

            Mock<DatabaseContext> mockContext = new Mock<DatabaseContext>();
            mockContext.Setup(x => x.Components).Returns(mockComSet.Object);
            mockContext.Setup(x => x.Components.Include(It.IsAny<string>())).Returns(mockComSet.Object);
            mockContext.Setup(x => x.Categories).Returns(mockCatSet.Object);
            mockContext.Setup(x => x.Categories.AsNoTracking()).Returns(mockCatSet.Object);


            var controller = new ComponentsController(mockContext.Object);

            var result = controller.Index("", "3");
            Assert.AreEqual(3, result);
        }

        [TestMethod()]
        public void CreateTest()
        {
            var mockComSet = new Mock<DbSet<Component>>();
            mockComSet.As<IQueryable<Component>>().Setup(m => m.Provider).Returns(Components.Provider);
            mockComSet.As<IQueryable<Component>>().Setup(m => m.Expression).Returns(Components.Expression);
            mockComSet.As<IQueryable<Component>>().Setup(m => m.ElementType).Returns(Components.ElementType);
            mockComSet.As<IQueryable<Component>>().Setup(m => m.GetEnumerator()).Returns(Components.GetEnumerator());

            Mock<DatabaseContext> mockContext = new Mock<DatabaseContext>();
            mockContext.Setup(x => x.Components).Returns(mockComSet.Object);
           
            var controller = new ComponentsController(mockContext.Object);

            var vModel = new CreateComponentViewmodel();
            vModel.Component = new Component();
            vModel.Component.ComponentName = "TEST";

            vModel.SpecificComponentListAsJson =
                "[{'ComponentNumber':'1','SerieNr':'a','LoanInformation':null},{'ComponentNumber':'2','SerieNr':'b','LoanInformation':null}]";

            var result = controller.Create(vModel);

            mockContext.Verify(x => x.Components.Add(It.IsAny<Component>()),Times.Exactly(1));
           
        }
    }
}