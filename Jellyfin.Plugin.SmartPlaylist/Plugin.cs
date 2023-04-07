using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Plugins;
using MediaBrowser.Model.Serialization;

namespace Jellyfin.Plugin.SmartPlaylist;

public class Plugin : BasePlugin<BasePluginConfiguration>, IHasWebPages {

	public static Plugin Instance { get; private set; }

	public override Guid Id => Guid.Parse(SmartPlaylistConsts.PLUGIN_GUID);

	public override string Name => SmartPlaylistConsts.PLUGIN_NAME;

	public override string Description => SmartPlaylistConsts.PLUGIN_DESCRIPTION;

	public Plugin(IApplicationPaths applicationPaths,
				  IXmlSerializer xmlSerializer) : base(applicationPaths, xmlSerializer) =>
			Instance = this;

	public IEnumerable<PluginPageInfo> GetPages() {
		return new[] {
				new PluginPageInfo {
						Name = "configPage.html",
						EmbeddedResourcePath = $"{GetType().Namespace}.Configuration.configPage.html",
				}
		};
	}
}
