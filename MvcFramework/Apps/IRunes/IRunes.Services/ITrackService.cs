using IRunes.Models;

namespace IRunes.Services
{
	public interface ITrackService
	{
		Track CreateTrack(Track track);

		Track GetById(string id);
	}
}
