using System;

namespace Aparts.Models.DLModels
{
    public class SubGroup
    {
		public int Id { get; set; }

		public int IdGroup { get; set; }

		public string Name { get; set; }

		public DateTime ReplDate { get; set; }

		public virtual Group Group { get; set; }
    }
}
