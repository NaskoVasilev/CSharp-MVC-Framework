using MvcFramework.Attributes.Validation;

namespace SULS.InputModels.Submissions
{
	public class SubmissionCreateInputModel
	{
		public string ProblemId { get; set; }

		[Required]
		[StringLength(30, 800)]
		public string Code { get; set; }
	}
}