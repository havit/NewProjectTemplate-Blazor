using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Model.Attrida
{
    public enum FileType
    {
		/// <summary>
		/// G2: -1
		/// </summary>
		Unknown = 0,
		WordDocument = -2,
		ExcelSheet = -3,
		Pdf = -4,
		Gif = -5,
		Html = -6,
		Rtf = -7,
		PlainText = -8,
		Jpeg = -9,
		Png = -10,
		Bmp = -11
    }
}
