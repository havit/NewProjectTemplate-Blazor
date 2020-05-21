using Havit.Data.EntityFrameworkCore.Attributes;
using Havit.Diagnostics.Contracts;
using Havit.GoranG3.Model.Attrida;
using Havit.GoranG3.Model.Crm;
using Havit.GoranG3.Model.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Model.Projects
{
	[Cache]
	public class Project
	{
		private const string FullNameSeparator = "~";

		public int Id { get; set; }

		public Project Parent
		{
			get => _parent;
			set
			{
				ProcessParentChange(_parent, value);
				_parent = value;
				UpdateEffectiveValues();
			}
		}
		private Project _parent;
		public int? ParentId { get; private set; }

		/// <summary>
		/// Root = 0.
		/// </summary>
		public int Depth { get; private set; }

		[Required]
		[MaxLength(20)]
		public string ProjectCode { get; set; }

		[Required]
		[MaxLength(100)]
		public string Name { get; set; }

		/// <summary>
		/// G2: ProjectManager (Employee => User!)
		/// </summary>
		public User ProjectManager
		{
			get => _projectManager;
			set
			{
				_projectManager = value;
				this.ProjectManagerId = value?.Id;
				UpdateProjectManagerEffective();
			}
		}

		private User _projectManager;
		public int? ProjectManagerId { get; private set; }

		public User ProjectManagerEffective { get; private set; }
		public int? ProjectManagerEffectiveId { get; private set; }

		/// <summary>
		/// G2: Klient
		/// </summary>
		public Contact BusinessPartner
		{
			get => _businessPartner;
			set
			{
				_businessPartner = value;
				this.BusinessPartnerId = value?.Id;
				UpdateBusinessPartnerEffective();
			}
		}
		private Contact _businessPartner;

		public int? BusinessPartnerId { get; set; }

		/// <summary>
		/// G2: KlientEffective
		/// </summary>
		public Contact BusinessPartnerEffective { get; private set; }
		public int? BusinessPartnerEffectiveId { get; private set; }

		/// <summary>
		/// G2: SplatnostPrijem
		/// </summary>
		public int? PaymentDueDaysDefault { get; set; }
		public int? PaymentDueDaysDefaultEffective => this.PaymentDueDaysDefault ?? this.Parent?.PaymentDueDaysDefaultEffective;

		/// <summary>
		/// Allows project-local override of OverheadToPersonalCostsRatio
		/// G2: UplatnovatRezijniPrirazkuOsobnichNakladu (false => 0m, true => null) 
		/// </summary>
		[Column(TypeName = "decimal(9, 4)")]
		public decimal? OverheadToPersonalCostsRatio
		{
			get => _overheadToPersonalCostsRatio;
			set
			{
				_overheadToPersonalCostsRatio = value;
				UpdateOverheadToPersonalCostsRatioEffective();
			}
		}
		private decimal? _overheadToPersonalCostsRatio;

		[Column(TypeName = "decimal(9, 4)")]
		public decimal? OverheadToPersonalCostsRatioEffective { get; private set; }

		/// <summary>
		/// G2: StavProjektu (Running => true, null => null, else => false)
		/// </summary>
		public bool? IsActive
		{
			get => _isActive;
			set
			{
				_isActive = value;
				UpdateIsActiveEffective();
			}
		}
		private bool? _isActive = true;

		/// <summary>
		/// G2: StavProjektuEffective (Running => true, null => null, else => false)
		/// </summary>
		public bool IsActiveEffective { get; private set; }

		public DateTime Created { get; set; }
		public DateTime? Deleted { get; set; }

		/// <summary>
		/// Not to be manipulated directly, set Project.Parent.
		/// </summary>
		public List<ProjectRelation> AllChildrenAndMeRelations { get; } = new List<ProjectRelation>();
		public IEnumerable<Project> AllChildrenAndMe => AllChildrenAndMeRelations.Select(r => r.LowerProject);

		/// <summary>
		/// Not to be manipulated directly, set Project.Parent.
		/// </summary>
		public List<ProjectRelation> AllParentsAndMeRelations { get; } = new List<ProjectRelation>();
		public IEnumerable<Project> AllParentsAndMe => AllParentsAndMeRelations.Select(r => r.HigherProject);

		/// <summary>
		/// Direct children.
		/// Not to be manipulated directly, set Project.Parent.
		/// </summary>
		public List<Project> Children { get; } = new List<Project>();

		public AttridaObject AttridaObject { get; set; }
		public int AttridaObjectId { get; set; }

		public string FullName
		{
			get
			{
				StringBuilder result = new StringBuilder();
				var currentProject = this;

				while (currentProject.Id != (int)Project.Entry.Root)
				{
					if (result.Length > 0)
					{
						result.Insert(0, FullNameSeparator);
					}
					result.Insert(0, currentProject.Name);
					currentProject = currentProject.Parent;
				}
				result.Insert(0, " - ");
				result.Insert(0, this.ProjectCode);

				return result.ToString();
			}
		}

		public Project()
		{
			this.AllChildrenAndMeRelations.Add(new ProjectRelation() { HigherProject = this, LowerProject = this });
			this.AllParentsAndMeRelations.Add(new ProjectRelation() { HigherProject = this, LowerProject = this });
		}

		private void UpdateProjectManagerEffective() // G2
		{
			var projektManagerEffective = ((this.Id == (int)Project.Entry.Root) || (this.ProjectManager != null)) ? this.ProjectManager : this.Parent.ProjectManagerEffective;
			bool changing = this.ProjectManagerEffective != projektManagerEffective;
			this.ProjectManagerEffective = projektManagerEffective;
			this.ProjectManagerEffectiveId = projektManagerEffective?.Id;
			if (changing)
			{
				Children.ForEach(project => project.UpdateProjectManagerEffective());
			}
		}

		private void UpdateBusinessPartnerEffective() // G2
		{
			var businessPartnerEffective = ((this.Id == (int)Project.Entry.Root) || (this.BusinessPartner != null)) ? this.BusinessPartner : this.Parent.BusinessPartnerEffective;
			bool changing = this.BusinessPartnerEffective != businessPartnerEffective;
			this.BusinessPartnerEffective = businessPartnerEffective;
			this.BusinessPartnerEffectiveId = businessPartnerEffective?.Id;
			if (changing)
			{
				Children.ForEach(project => project.UpdateBusinessPartnerEffective());
			}
		}

		private void UpdateOverheadToPersonalCostsRatioEffective() // G2
		{
			var overheadToPersonalCostsRatioEffective = ((this.Id == (int)Project.Entry.Root) || (this.OverheadToPersonalCostsRatio != null)) ? this.OverheadToPersonalCostsRatio : this.Parent.OverheadToPersonalCostsRatioEffective;
			bool changing = this.OverheadToPersonalCostsRatioEffective != overheadToPersonalCostsRatioEffective;
			this.OverheadToPersonalCostsRatioEffective = overheadToPersonalCostsRatioEffective;
			if (changing)
			{
				Children.ForEach(project => project.UpdateOverheadToPersonalCostsRatioEffective());
			}
		}

		private void UpdateIsActiveEffective()
		{
			bool isActiveEffective = (((this.Id == (int)Project.Entry.Root) || (this.IsActive != null)) ? this.IsActive : this.Parent.IsActiveEffective) ?? true;
			bool changing = this.IsActiveEffective != isActiveEffective;
			this.IsActiveEffective = isActiveEffective;
			if (changing)
			{
				Children.ForEach(project => project.UpdateIsActiveEffective());
			}
		}

		private void UpdateEffectiveValues()
		{
			UpdateProjectManagerEffective();
			UpdateBusinessPartnerEffective();
			UpdateOverheadToPersonalCostsRatioEffective();
			UpdateIsActiveEffective();
		}

		private void ProcessParentChange(Project oldParent, Project newParent) // G2
		{
			Contract.Requires<ArgumentNullException>(newParent != null, nameof(newParent));

			Project tempProject;

			if (oldParent != null)
			{
				// odebereme se z kolekce Children původního parenta
				oldParent.Children.Remove(this);

				// odstraníme původního parenta a jeho parenty z kolekce AllParentsAndMe své a projektů níže
				tempProject = oldParent; // ...původního parenta
				while (tempProject != null)
				{
					foreach (var item in this.AllChildrenAndMe) // ...ze svých a projektů níže
					{
						item.AllParentsAndMeRelations.RemoveAll(r => r.HigherProject == tempProject); // ...odstraníme z kolekce AllParentsAndMe
					}
					tempProject = tempProject.Parent; // ...původního parenta a jeho parenty
				}

				// odstraníme sebe a nižší projekty z kolekce AllChildrenAndMe starého parenta a jeho parentů
				tempProject = oldParent; // ...z původního parenta
				while (tempProject != null)
				{
					foreach (var item in this.AllChildrenAndMe) // ...sebe a nižší projekty
					{
						tempProject.AllChildrenAndMeRelations.RemoveAll(r => r.LowerProject == item);  // ...odstraníme z kolekce AllChildrenAndMe
					}
					tempProject = tempProject.Parent;  // ...původního parenta a jeho parentů
				}
			}

			// přidáme se do kolekce Children nového parenta
			newParent.Children.Add(this);

			// přidáme nového parenta a jeho parenty do kolekce AllParentsAndMe své a projektů níže
			tempProject = newParent; // ...nového parenta
			while (tempProject != null)
			{
				foreach (var item in this.AllChildrenAndMe) // ..do svých a projektů níže
				{
					item.AllParentsAndMeRelations.Add(new ProjectRelation()
					{
						LowerProject = this,
						LowerProjectId = this.Id,
						HigherProject = tempProject,
						HigherProjectId = tempProject.Id
					}); // ...do kolekce AllParentsAndMe
				}
				tempProject = tempProject.Parent; // ...nového parenta a jeho parenty
			}

			// přidáme sebe a nižší projekty do kolekce AllChildrenAndMe nového parenta a jeho parentů
			tempProject = newParent; // ...nového parenta
			while (tempProject != null)
			{
				foreach (var item in this.AllChildrenAndMe) // ...sebe a nižší projekty
				{
					tempProject.AllChildrenAndMeRelations.Add(new ProjectRelation()
					{
						LowerProject = item,
						LowerProjectId = item.Id,
						HigherProject = this,
						HigherProjectId = this.Id
					}); // ...přidáme sebe a nižší projekty
				}
				tempProject = tempProject.Parent; // ...nového parenta a jeho parentů
			}

			// nastavíme Depth pro sebe a své potomky
			int oldDepth = this.Depth;
			int newDepth = newParent.Depth + 1;
			foreach (var item in this.AllChildrenAndMe)
			{
				item.Depth = item.Depth - oldDepth + newDepth;
			}
		}

		public enum Entry
		{
			Root = -1
		}
	}
}
