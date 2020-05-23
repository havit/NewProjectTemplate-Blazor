using System;
using Havit.Data.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Havit.GoranG3.Entity
{
	public class GoranG3DbContext : Havit.Data.EntityFrameworkCore.DbContext
	{
		/// <summary>
		/// Konstruktor.
		/// Pro použití v unit testech, jiné použití nemá.
		/// </summary>
		internal GoranG3DbContext()
		{
			// NOOP
		}

		/// <summary>
		/// Konstruktor.
		/// </summary>
		public GoranG3DbContext(DbContextOptions options) : base(options)
		{
			// NOOP
		}

		/// <inheritdoc />
		protected override void CustomizeModelCreating(ModelBuilder modelBuilder)
		{
			base.CustomizeModelCreating(modelBuilder);

			modelBuilder.HasSequence<int>("ContactSequence");
			modelBuilder.HasSequence<int>("ProjectSequence");
			modelBuilder.HasSequence<int>("TeamSequence");

			modelBuilder.RegisterModelFromAssembly(typeof(Havit.GoranG3.Model.Localizations.Language).Assembly);
			modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
		}
	}
}
