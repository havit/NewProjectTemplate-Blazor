using Havit.Data.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Havit.NewProjectTemplate.Entity;

public class NewProjectTemplateDbContext : Havit.Data.EntityFrameworkCore.DbContext
{
	/// <summary>
	/// Konstruktor.
	/// Pro použití v unit testech, jiné použití nemá.
	/// </summary>
	internal NewProjectTemplateDbContext()
	{
		// NOOP
	}

	/// <summary>
	/// Konstruktor.
	/// </summary>
	public NewProjectTemplateDbContext(DbContextOptions options) : base(options)
	{
		// NOOP
	}

	/// <inheritdoc />
	protected override void CustomizeModelCreating(ModelBuilder modelBuilder)
	{
		base.CustomizeModelCreating(modelBuilder);

		// modelBuilder.HasSequence<int>("XySequence");

		modelBuilder.RegisterModelFromAssembly(typeof(Havit.NewProjectTemplate.Model.Localizations.Language).Assembly);
		modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
	}
}
