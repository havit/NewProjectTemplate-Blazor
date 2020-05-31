using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Data.Patterns.UnitOfWorks;
using Havit.GoranG3.DataLayer.DataEntries.Projects;
using Havit.GoranG3.Model.Projects;
using Havit.GoranG3.TestHelpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Havit.GoranG3.IntegrationTests.Entities
{
	[TestClass]
	public class ProjectRelationTests : IntegrationTestBase
	{
		//[TestMethod]
		public void ProjectRelations_SimpleProjectLevel1()
		{
			// arrange
			var projectEntries = ServiceProvider.GetRequiredService<IProjectEntries>();
			var unitOfWork = ServiceProvider.GetRequiredService<IUnitOfWork>();

			var project = new Project()
			{
				Parent = projectEntries.Root
			};

			Debug.Assert(object.ReferenceEquals(project.AllChildrenAndMeRelations.Single(), project.AllParentsAndMeRelations.Single(r => (r.HigherProject == project) && (r.LowerProject == project))));

			// act
			unitOfWork.AddForInsert(project);
			unitOfWork.Commit();

			// assert
			Assert.AreNotEqual(default(int), project.Id);
		}

	}
}
