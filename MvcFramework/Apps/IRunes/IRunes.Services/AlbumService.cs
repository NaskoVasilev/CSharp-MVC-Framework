using System.Collections.Generic;
using System.Linq;
using IRunes.Data;
using IRunes.Models;
using Microsoft.EntityFrameworkCore;

namespace IRunes.Services
{
	public class AlbumService : IAlbumService
	{
		private readonly RunesDbContext context;

		public AlbumService(RunesDbContext context)
		{
			this.context = context;
		}

		public Album CreateAlbum(Album album)
		{
			album =  context.Albums.Add(album).Entity;
			context.SaveChanges();
			return album;
		}

		public Album GetAlbumById(string id)
		{
			return context.Albums
				.Include(a => a.Tracks)
				.FirstOrDefault(a => a.Id == id);
		}

		public IEnumerable<Album> GetAllAlbums()
		{
			return context.Albums.ToList();
		}

		public Album GetById(string id)
		{
			return this.context.Albums.Find(id);
		}

		public void UpdateAlbum(Album album)
		{
			this.context.Update(album);
			this.context.SaveChanges();
		}
	}
}
