namespace Jellyfin.Plugin.SmartPlaylist.Interfaces;

public interface ISmartPlaylistFileSystem {
	string SmartPlaylistsPath { get; }

	string GetSmartPlaylistFilePath(string smartPlaylistId);

	string[] GetSmartPlaylistFilePaths(string userId);

	string[] GetAllSmartPlaylistFilePaths();

	string GetSmartPlaylistPath(string userId, string playlistId);
}
