using Jellyfin.Data.Entities;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Entities.Movies;
using MediaBrowser.Controller.Entities.TV;
using MediaBrowser.Controller.Library;

namespace Jellyfin.Plugin.SmartPlaylist.QueryEngine;

internal class OperandFactory {
	// Returns a specific operand provided a baseitem, user, and library manager object.
	public static Operand GetMediaType(ILibraryManager libraryManager,
									   BaseItem baseItem,
									   User user) {
		var operand = new Operand(baseItem.Name);
		var people = libraryManager.GetPeople(baseItem);

		if (people.Any()) {
			operand.Actors.AddRange(people.Where(x => x.Type.Equals("Actor")).Select(x => x.Name));
			operand.Composers.AddRange(people.Where(x => x.Type.Equals("Composer")).Select(x => x.Name));
			operand.Directors.AddRange(people.Where(x => x.Type.Equals("Director")).Select(x => x.Name));
			operand.GuestStars.AddRange(people.Where(x => x.Type.Equals("GuestStar")).Select(x => x.Name));
			operand.Producers.AddRange(people.Where(x => x.Type.Equals("Producer")).Select(x => x.Name));
			operand.Writers.AddRange(people.Where(x => x.Type.Equals("Writer")).Select(x => x.Name));
		}

		operand.Genres.AddRange(baseItem.Genres);
		operand.Studios.AddRange(baseItem.Studios);
		operand.IsPlayed = baseItem.IsPlayed(user);
		operand.CommunityRating = baseItem.CommunityRating.GetValueOrDefault();
		operand.CriticRating = baseItem.CriticRating.GetValueOrDefault();
		operand.MediaType = baseItem.MediaType;
		operand.Album = baseItem.Album;
		operand.FolderPath = baseItem.ContainingFolderPath;
		operand.ProductionYear = baseItem.ProductionYear;
		operand.OriginalTitle = baseItem.OriginalTitle;
		operand.Height = baseItem.Height;
		operand.Width = baseItem.Width;
		operand.FileNameWithoutExtension = baseItem.FileNameWithoutExtension;
		operand.OfficialRating = baseItem.OfficialRating;
		operand.SortName = baseItem.SortName;

		operand.HasSubtitles = baseItem switch {
			Movie m => m.HasSubtitles,
			Episode e => e.HasSubtitles,
			MusicVideo mv => mv.HasSubtitles,
			_ => false,
		};

		try {
			if (baseItem.PremiereDate.HasValue) {
				operand.PremiereDate = new DateTimeOffset(baseItem.PremiereDate.Value).ToUnixTimeSeconds();
			}

			operand.DateCreated = new DateTimeOffset(baseItem.DateCreated).ToUnixTimeSeconds();
			operand.DateLastRefreshed = new DateTimeOffset(baseItem.DateLastRefreshed).ToUnixTimeSeconds();
			operand.DateLastSaved = new DateTimeOffset(baseItem.DateLastSaved).ToUnixTimeSeconds();
			operand.DateModified = new DateTimeOffset(baseItem.DateModified).ToUnixTimeSeconds();
		}
		catch {
			//Ignore
		}

		return operand;
	}
}
