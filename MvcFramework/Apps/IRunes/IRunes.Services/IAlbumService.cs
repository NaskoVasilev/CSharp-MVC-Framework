using IRunes.Models;
using System.Collections.Generic;

namespace IRunes.Services
{
	public interface IAlbumService
	{
		IEnumerable<Album> GetAllAlbums();

		Album CreateAlbum(Album album);

		Album GetAlbumById(string id);

		Album GetById(string id);

		void UpdateAlbum(Album album);
	}
}
