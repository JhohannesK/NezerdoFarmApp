using System.ComponentModel.DataAnnotations.Schema;

namespace NezerdoFarmApp.Models
{
	public class HealthRecord
	{
		public int HealthRecordId { get; set; }
		public required int LivestockId { get; set; }
		public Livestock Livestock { get; set; } = null!;
		public required string NameOfDrug { get; set; }
		[Column(TypeName = "varchar(50)")]
		public required DrugCategory TypeofDrug { get; set; }
		public required DateTime StartDate { get; set; }
		public required DateTime EndDate { get; set; }
		public required DateTime CreatedAt { get; set; } = DateTime.Now;
		public required DateTime UpdatedAt { get; set; }

	}

	public enum DrugCategory
	{
		AntiMicrobials,
		Vaccine,
		AntiInflammatories,
		Chemotherapeutics,
		NeutraceuticalAndSupplements,
		Analgesics,
		Anthelmintics,
		Ectoparasiticides,
		Respiratory,
		Gastrointestinal,
		Others
	}
}
