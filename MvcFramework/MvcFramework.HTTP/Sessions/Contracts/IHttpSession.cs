namespace MvcFramework.HTTP.Sessions.Contracts
{
	public interface IHttpSession
	{
		string Id { get; }

		object GetParameter(string name);

		bool ConstainsParameter(string name);

		void AddParameter(string name, object parameter);

		void ClearParameters();
	}
}
