using MvcFramework.Attributes.Validation;

namespace SULS.InputModels.Problems
{
	public class ProblemCreateInputModel
	{
		[Required]
		[StringLength(5, 20)]
		public string Name { get; set; }

		[Range(50, 300)]
		public int Points { get; set; }
	}
}
