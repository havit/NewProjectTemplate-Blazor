using Havit.GoranG3.Model.Crm;
using Havit.GoranG3.Model.Projects;
using Havit.GoranG3.Model.Security;
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
		public void Project_IsActiveEffective_Self()
		{
			// arrange
			var root = new Project() { Id = (int)Project.Entry.Root, IsActive = true };
			var project1 = new Project() { Parent = root };

			// act
			project1.IsActive = false;

			// assert
			Assert.IsFalse(project1.IsActiveEffective);

		}

		[TestMethod]
		public void Project_IsActiveEffective_Inheritance()
		{
			// arrange
			var root = new Project() { Id = (int)Project.Entry.Root, IsActive = true };
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
			var root = new Project() { Id = (int)Project.Entry.Root, IsActive = true };
			var project1 = new Project() { Parent = root, IsActive = false };
			var project11 = new Project() { Parent = project1 };
			var project2 = new Project() { Parent = root };

			// act
			project11.Parent = project2;

			// assert
			Assert.IsTrue(project11.IsActiveEffective);
		}

		[TestMethod]
		public void Project_ProjectManagerEffective_Self()
		{
			// arrange
			var user1 = new User();
			var root = new Project() { Id = (int)Project.Entry.Root };
			var project1 = new Project() { Parent = root };

			// act
			project1.ProjectManager = user1;

			// assert
			Assert.AreSame(user1, project1.ProjectManagerEffective);
		}

		[TestMethod]
		public void Project_ProjectManagerEffective_Inheritance()
		{
			// arrange
			var user1 = new User();
			var root = new Project() { Id = (int)Project.Entry.Root };
			var project1 = new Project() { Parent = root };
			var project11 = new Project() { Parent = project1 };

			// act
			project1.ProjectManager = user1;

			// assert
			Assert.AreSame(user1, project11.ProjectManagerEffective);
		}

		[TestMethod]
		public void Project_ProjectManagerEffective_HierarchyChange()
		{
			// arrange
			var user1 = new User();
			var user2 = new User();
			var root = new Project() { Id = (int)Project.Entry.Root };
			var project1 = new Project() { Parent = root, ProjectManager = user1 };
			var project11 = new Project() { Parent = project1 };
			var project2 = new Project() { Parent = root, ProjectManager = user2 };

			// act
			project11.Parent = project2;

			// assert
			Assert.AreSame(user2, project11.ProjectManagerEffective);
		}

		[TestMethod]
		public void Project_BusinessPartnerEffective_Self()
		{
			// arrange
			var contact1 = new Contact();
			var root = new Project() { Id = (int)Project.Entry.Root };
			var project1 = new Project() { Parent = root };

			// act
			project1.BusinessPartner = contact1;

			// assert
			Assert.AreSame(contact1, project1.BusinessPartnerEffective);
		}

		[TestMethod]
		public void Project_BusinessPartnerEffective_Inheritance()
		{
			// arrange
			var contact1 = new Contact();
			var root = new Project() { Id = (int)Project.Entry.Root };
			var project1 = new Project() { Parent = root };
			var project11 = new Project() { Parent = project1 };

			// act
			project1.BusinessPartner = contact1;

			// assert
			Assert.AreSame(contact1, project11.BusinessPartnerEffective);
		}

		[TestMethod]
		public void Project_BusinessPartnerEffective_HierarchyChange()
		{
			// arrange
			var contact1 = new Contact();
			var contact2 = new Contact();
			var root = new Project() { Id = (int)Project.Entry.Root };
			var project1 = new Project() { Parent = root, BusinessPartner = contact1 };
			var project11 = new Project() { Parent = project1 };
			var project2 = new Project() { Parent = root, BusinessPartner = contact2 };

			// act
			project11.Parent = project2;

			// assert
			Assert.AreSame(contact2, project11.BusinessPartnerEffective);
		}

		[TestMethod]
		public void Project_OverheadToPersonalCostsRatioEffective_Self()
		{
			// arrange
			var root = new Project() { Id = (int)Project.Entry.Root };
			var project1 = new Project() { Parent = root };

			// act
			project1.OverheadToPersonalCostsRatio = 0.99m;

			// assert
			Assert.AreEqual(0.99m, project1.OverheadToPersonalCostsRatioEffective);
		}

		[TestMethod]
		public void Project_OverheadToPersonalCostsRatioEffective_Inheritance()
		{
			// arrange
			var root = new Project() { Id = (int)Project.Entry.Root };
			var project1 = new Project() { Parent = root };
			var project11 = new Project() { Parent = project1 };

			// act
			project1.OverheadToPersonalCostsRatio = 0.99m;

			// assert
			Assert.AreEqual(0.99m, project11.OverheadToPersonalCostsRatioEffective);
		}

		[TestMethod]
		public void Project_OverheadToPersonalCostsRatioEffective_HierarchyChange()
		{
			// arrange
			var root = new Project() { Id = (int)Project.Entry.Root };
			var project1 = new Project() { Parent = root, OverheadToPersonalCostsRatio = 0.1m };
			var project11 = new Project() { Parent = project1 };
			var project2 = new Project() { Parent = root, OverheadToPersonalCostsRatio = 0.2m };

			// act
			project11.Parent = project2;

			// assert
			Assert.AreEqual(0.2m, project11.OverheadToPersonalCostsRatioEffective);
		}
	}
}
