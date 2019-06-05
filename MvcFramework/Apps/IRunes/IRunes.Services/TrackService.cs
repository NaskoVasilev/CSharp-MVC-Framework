using IRunes.Data;
using IRunes.Models;

namespace IRunes.Services
{
	public class TrackService : ITrackService
	{
		private readonly RunesDbContext context;

		public TrackService(RunesDbContext context)
		{
			this.context = context;
		}

		public Track CreateTrack(Track track)
		{
			context.Tracks.Add(track);
			context.SaveChanges();
			return track;
		}

		public Track GetById(string id)
		{
			Track track = context.Tracks.Find(id);
			return track;
		}
	}
}
