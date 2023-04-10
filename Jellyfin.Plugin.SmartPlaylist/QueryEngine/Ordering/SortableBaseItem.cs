﻿using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Entities.Movies;
using MediaBrowser.Controller.Entities.TV;

namespace Jellyfin.Plugin.SmartPlaylist.QueryEngine.Ordering;

public readonly struct SortableBaseItem {
	public BaseItem BaseItem { get; }

	public SortableBaseItem(BaseItem baseItem) {
		BaseItem = baseItem;
	}

	public string Name => BaseItem.Name;
	public string OriginalTitle => BaseItem.OriginalTitle;
	public DateTime? PremiereDate => BaseItem.PremiereDate;
	public string Path => BaseItem.Path;
	public string Container => BaseItem.Container;
	public string Tagline => BaseItem.Tagline;
	public Guid ChannelId => BaseItem.ChannelId;
	public Guid Id => BaseItem.Id;
	public int Width => BaseItem.Width;
	public int Height => BaseItem.Height;
	public DateTime DateModified => BaseItem.DateModified;
	public DateTime DateLastSaved => BaseItem.DateLastSaved;
	public DateTime DateLastRefreshed => BaseItem.DateLastRefreshed;
	public string MediaType => BaseItem.MediaType;
	public string SortName => BaseItem.SortName;
	public string ForcedSortName => BaseItem.ForcedSortName;
	public DateTime? EndDate => BaseItem.EndDate;
	public string Overview => BaseItem.Overview;
	public int? ProductionYear => BaseItem.ProductionYear;

	public string CollectionName {
		get {
			if (BaseItem is Movie movie) {
				return movie.CollectionName;
			}

			return string.Empty;
		}
	}

	public bool HasSubtitles {
		get {
			return BaseItem switch {
				Movie movie => movie.HasSubtitles,
				Episode episode => episode.HasSubtitles,
				MusicVideo musicVideo => musicVideo.HasSubtitles,
				_ => false
			};
		}
	}

	public int? AiredSeasonNumber {
		get {
			return BaseItem switch {
				Episode episode => episode.AiredSeasonNumber,
				_ => null
			};
		}
	}

	public int? ParentIndexNumber {
		get {
			return BaseItem switch {
				Episode episode => episode.ParentIndexNumber,
				_ => null
			};
		}
	}

	public string SeasonName {
		get {
			return BaseItem switch {
				Episode episode => episode.SeasonName,
				_ => null
			};
		}
	}

	public string SeriesName {
		get {
			return BaseItem switch {
				Episode episode => episode.SeriesName,
				_ => null
			};
		}
	}
}
