﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Data.EntityFrameworkCore.Patterns.DataSources.Fakes;
using Havit.Data.EntityFrameworkCore.Patterns.SoftDeletes;
using Havit.Data.Patterns.Attributes;

namespace Havit.GoranG3.DataLayer.DataSources.Attrida.Fakes
{
	[Fake]
	[System.CodeDom.Compiler.GeneratedCode("Havit.Data.EntityFrameworkCore.CodeGenerator", "1.0")]
	public class FakeAttridaDocumentDataSource : FakeDataSource<Havit.GoranG3.Model.Attrida.AttridaDocument>, Havit.GoranG3.DataLayer.DataSources.Attrida.IAttridaDocumentDataSource
	{
		public FakeAttridaDocumentDataSource(params Havit.GoranG3.Model.Attrida.AttridaDocument[] data)
			: this((IEnumerable<Havit.GoranG3.Model.Attrida.AttridaDocument>)data)
		{			
		}

		public FakeAttridaDocumentDataSource(IEnumerable<Havit.GoranG3.Model.Attrida.AttridaDocument> data, ISoftDeleteManager softDeleteManager = null)
			: base(data, softDeleteManager)
		{
		}
	}
}