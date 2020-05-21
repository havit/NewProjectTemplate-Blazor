using Havit.GoranG3.Model.Projects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.ModelTests.Projects
{
    [TestClass]
	public class ProjectTests
    {
		[TestMethod]
		public void Project_Hierarchy_NewProjects()
		{
			// arrange
			var root = new Project() { Id = (int)Project.Entry.Root };

			// act
			var project1 = new Project() { Parent = root };

			// assert
			Assert.IsTrue(project1.AllParentsAndMe.Contains(root));
			Assert.IsTrue(project1.AllParentsAndMe.Contains(project1));
		}

		[TestMethod]
		public void Project_Hierarchy_ParentChange()
		{
			// arrange
			var root = new Project() { Id = (int)Project.Entry.Root };
			var project1 = new Project() { Parent = root };
			var project11 = new Project() { Parent = project1 };
			var project12 = new Project() { Parent = project1 };
			var project2 = new Project() { Parent = root };

			// act
			project1.Parent = project2;

			// assert
			CollectionAssert.AreEquivalent(new[] { root, project2, project1, project11 }, project11.AllParentsAndMe.ToList());
			CollectionAssert.AreEquivalent(new[] { root, project2, project1, project12 }, project12.AllParentsAndMe.ToList());
			CollectionAssert.AreEquivalent(new[] { project2, project1, project11, project12 }, project2.AllChildrenAndMe.ToList());
			CollectionAssert.AreEquivalent(new[] { project1, project11, project12 }, project1.AllChildrenAndMe.ToList());
			CollectionAssert.AreEquivalent(new[] { project1, project2, root }, project1.AllParentsAndMe.ToList());
		}

		[TestMethod]
		public void Project_IsActiveEffective_Inheritance()
		{
			// arrange
			var root = new Project() { Id = (int)Project.Entry.Root };
			var project1 = new Project() { Parent = root };
			var project11 = new Project() { Parent = project1 };

			// act
			project1.IsActive = false;

			// assert
			Assert.IsFalse(project11.IsActiveEffective);

		}

		[TestMethod]
		public void Project_IsActiveEffective_HierarchyChange()
		{
			// arrange
			var root = new Project() { Id = (int)Project.Entry.Root };
			var project1 = new Project() { Parent = root, IsActive = false };
			var project11 = new Project() { Parent = project1 };
			var project2 = new Project() { Parent = root };

			// act
			project11.Parent = project2;

			// assert
			Assert.IsTrue(project11.IsActiveEffective);
		}

	}
}
