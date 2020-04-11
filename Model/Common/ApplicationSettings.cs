using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havit.GoranG3.Model.Common
{
    public class ApplicationSettings
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public enum Entry
        {
            Current = -1
        }
    }
}
