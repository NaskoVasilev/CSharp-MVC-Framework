using System.Reflection;
using MvcFramework.Validation;

namespace MvcFramework
{
	public class ControllerState : IControllerState
	{
		public ControllerState()
		{
			this.Reset();
		}

		public ModelStateDictionary ModelState { get; set; }

		public void Reset()
		{
			this.ModelState = new ModelStateDictionary();
		}

		public void Initialize(Controller controller)
		{
			this.ModelState = (ModelStateDictionary)typeof(Controller)
				.GetProperty("ModelState", BindingFlags.Instance | BindingFlags.NonPublic)
				.GetValue(controller);
		}

		public void SetState(Controller controller)
		{
			typeof(Controller).GetProperty("ModelState", BindingFlags.Instance | BindingFlags.NonPublic)
				.SetValue(controller, this.ModelState);
		}
	}
}
