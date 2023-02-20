using System.Text.RegularExpressions;

namespace YTMUSICAPI;

    public class Action
    {
        public string addedVideoId;
        //public string Action;
        public string removedVideoId;
    }

    public class AdaptiveFormat
    {
        public int? itag;
        public string mimeType;
        public int? bitrate;
        public int? width;
        public int? height;
        public InitRange initRange;
        public IndexRange indexRange;
        public string lastModified;
        public string contentLength;
        public string quality;
        public int? fps;
        public string qualityLabel;
        public string projectionType;
        public int? averageBitrate;
        public string approxDurationMs;
        public string signatureCipher;
        public ColorInfo colorInfo;
        public bool? highReplication;
        public string audioQuality;
        public string audioSampleRate;
        public int? audioChannels;
        public double? loudnessDb;
    }

    public class AdBreakServiceRenderer
    {
        public string prefetchMilliseconds;
        public string getAdBreakUrl;
    }

    public class AddToWatchLaterCommand
    {
        public string clickTrackingParams;
        public PlaylistEditEndpoint playlistEditEndpoint;
    }

    public class AdPlacement
    {
        public AdPlacementRenderer adPlacementRenderer;
    }

    public class AdPlacementConfig
    {
        public string kind;
        public AdTimeOffset adTimeOffset;
        public bool? hideCueRangeMarker;
    }

    public class AdPlacementRenderer
    {
        public Config config;
        public Renderer renderer;
        public AdSlotLoggingData adSlotLoggingData;
    }

    public class AdSlotLoggingData
    {
        public string serializedSlotAdServingDataEntry;
    }

    public class AdTimeOffset
    {
        public string offsetStartMilliseconds;
        public string offsetEndMilliseconds;
    }

    public class AtrUrl
    {
        public string caseUrl;
        public int? elapsedMediaTimeSeconds;
        public List<Header> headers;
    }

    public class Attestation
    {
        public PlayerAttestationRenderer playerAttestationRenderer;
    }

    public class AudioConfig
    {
        public double? loudnessDb;
        public double? perceptualLoudnessDb;
        public bool? enablePerFormatLoudness;
    }

    public class AudioOnlyPlayability
    {
        public AudioOnlyPlayabilityRenderer audioOnlyPlayabilityRenderer;
    }

    public class AudioOnlyPlayabilityRenderer
    {
        public string trackingParams;
        public string audioOnlyAvailability;
    }

    public class BotguardData
    {
        public string program;
        public InterpreterSafeUrl interpreterSafeUrl;
        public int? serverEnvironment;
    }

    public class ClientForecastingAdRenderer
    {
        public List<ImpressionUrl> impressionUrls;
    }

    public class ColorInfo
    {
        public string primaries;
        public string transferCharacteristics;
        public string matrixCoefficients;
    }

    public class Config
    {
        public AdPlacementConfig adPlacementConfig;
    }

    public class DynamicReadaheadConfig
    {
        public int? maxReadAheadMediaTimeMs;
        public int? minReadAheadMediaTimeMs;
        public int? readAheadGrowthRateMs;
    }

    public class Format
    {
        public int? itag;
        public string mimeType;
        public int? bitrate;
        public int? width;
        public int? height;
        public string lastModified;
        public string quality;
        public int? fps;
        public string qualityLabel;
        public string projectionType;
        public string audioQuality;
        public string approxDurationMs;
        public string audioSampleRate;
        public int? audioChannels;
        public string signatureCipher;
    }

    public class GutParams
    {
        public string tag;
    }

    public class Header
    {
        public string headerType;
    }

    public class ImpressionUrl
    {
        public string baseUrl;
        public List<Header> headers;
    }

    public class IndexRange
    {
        public string start;
        public string end;
    }

    public class InitRange
    {
        public string start;
        public string end;
    }

    public class InterpreterSafeUrl
    {
        public string privateDoNotAccessOrElseTrustedResourceUrlWrappedValue;
    }

    public class LinkAlternate
    {
        public string hrefUrl;
        public string title;
        public string alternateType;
    }

    public class MediaCommonConfig
    {
        public DynamicReadaheadConfig dynamicReadaheadConfig;
    }

    public class Microformat
    {
        public MicroformatDataRenderer microformatDataRenderer { get; set; }
    }

    public class MicroformatDataRenderer
    {
        public string urlCanonical { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public Thumbnail thumbnail { get; set; }
        public string siteName { get; set; }
        public string appName { get; set; }
        public string androidPackage { get; set; }
        public string iosAppStoreId { get; set; }
        public string iosAppArguments { get; set; }
        public string ogType { get; set; }
        public string urlApplinksIos { get; set; }
        public string urlApplinksAndroid { get; set; }
        public string urlTwitterIos { get; set; }
        public string urlTwitterAndroid { get; set; }
        public string twitterCardType { get; set; }
        public string twitterSiteHandle { get; set; }
        public string schemaDotOrgType { get; set; }
        public bool? noindex { get; set; }
        public bool? unlisted { get; set; }
        public bool? paid { get; set; } 
        public bool? familySafe { get; set; }
        public List<string> tags { get; set; }
        public List<string> availableCountries { get; set; }
        public PageOwnerDetails pageOwnerDetails { get; set; }
        public VideoDetails videoDetails { get; set; }
        public List<LinkAlternate> linkAlternates { get; set; }
        public string viewCount { get; set; }
        public string publishDate { get; set; }
        public string category { get; set; }
        public string uploadDate { get; set; }

        public string GetYear()
        {
            string da="0000";
            MatchCollection MC = Regex.Matches(description,"\\d\\d\\d\\d-\\d\\d-\\d\\d");
            if (MC.Count == 0)
            {
                MatchCollection matchCollection = Regex.Matches(description,@"\d{4}");
                foreach (Match VARIABLE in matchCollection)
                {
                    da = VARIABLE.Value;
                }
            }
            else
            {
                foreach (Match VARIABLE in MC)
                {
                    da = (string)Regex.Match(VARIABLE.Value, @"\d{4}").Value;
                }
            }
            return da;
        }
    }

    public class Miniplayer
    {
        public MiniplayerRenderer miniplayerRenderer;
    }

    public class MiniplayerRenderer
    {
        public string playbackMode;
    }

    public class PageOwnerDetails
    {
        public string name;
        public string externalChannelId;
        public string youtubeProfileUrl;
    }

    public class Param
    {
        public string key;
        public string value;
    }

    public class PlayabilityStatus
    {
        public string status;
        public bool? playableInEmbed;
        public AudioOnlyPlayability audioOnlyPlayability;
        public Miniplayer miniplayer;
        public string contextParams;
    }

    public class PlaybackTracking
    {
        public VideostatsPlaybackUrl videostatsPlaybackUrl;
        public VideostatsDelayplayUrl videostatsDelayplayUrl;
        public VideostatsWatchtimeUrl videostatsWatchtimeUrl;
        public PtrackingUrl ptrackingUrl;
        public QoeUrl qoeUrl;
        public AtrUrl atrUrl;
        public List<int?> videostatsScheduledFlushWalltimeSeconds;
        public int? videostatsDefaultFlushIntervalSeconds;
    }

    public class PlayerAd
    {
        public PlayerLegacyDesktopWatchAdsRenderer playerLegacyDesktopWatchAdsRenderer;
    }

    public class PlayerAdParams
    {
        public bool? showContentThumbnail;
        public string enabledEngageTypes;
    }

    public class PlayerAttestationRenderer
    {
        public string challenge;
        public BotguardData botguardData;
    }

    public class PlayerConfig
    {
        public AudioConfig audioConfig;
        public StreamSelectionConfig streamSelectionConfig;
        public MediaCommonConfig mediaCommonConfig;
        public WebPlayerConfig webPlayerConfig;
    }

    public class PlayerLegacyDesktopWatchAdsRenderer
    {
        public PlayerAdParams playerAdParams;
        public GutParams gutParams;
        public bool? showCompanion;
        public bool? showInstream;
        public bool? useGut;
    }

    public class PlayerStoryboardSpecRenderer
    {
        public string spec;
    }

    public class PlaylistEditEndpoint
    {
        public string playlistId;
        public List<Action> actions;
    }

    public class PtrackingUrl
    {
        public string baseUrl;
        public List<Header> headers;
    }

    public class QoeUrl
    {
        public string baseUrl;
        public List<Header> headers;
    }

    public class RemoveFromWatchLaterCommand
    {
        public string clickTrackingParams;
        public PlaylistEditEndpoint playlistEditEndpoint;
    }

    public class Renderer
    {
        public AdBreakServiceRenderer adBreakServiceRenderer;
        public ClientForecastingAdRenderer clientForecastingAdRenderer;
    }

    public class ResponseContext
    {
        public List<ServiceTrackingParam> serviceTrackingParams;
    }

    public class Root
    {
        public ResponseContext ResponseContext;
        public PlayabilityStatus PlayabilityStatus;
        public StreamingData StreamingData;
        public List<PlayerAd> PlayerAds;
        public PlaybackTracking PlaybackTracking;
        public VideoDetails videoDetails { get; set; }
        public PlayerConfig PlayerConfig { get; set; }
        public Storyboards Storyboards { get; set; }
        public Microformat microformat { get; set; }
        public string TrackingParams { get; set; }
        public Attestation Attestation { get; set; }
        public List<AdPlacement> AdPlacements{ get; set; }
    }

    public class ServiceTrackingParam
    {
        public string service;
        public List<Param> Params;
    }

    public class Storyboards
    {
        public PlayerStoryboardSpecRenderer PlayerStoryboardSpecRenderer;
    }

    public class StreamingData
    {
        public string expiresInSeconds;
        public List<Format> formats;
        public List<AdaptiveFormat> adaptiveFormats;
        public string probeUrl;
    }

    public class StreamSelectionConfig
    {
        public string maxBitrate;
    }

    public class SubscribeCommand
    {
        public string clickTrackingParams;
        public SubscribeEndpoint subscribeEndpoint;
    }

    public class SubscribeEndpoint
    {
        public List<string> channelIds { get; set; }
        public string Params { get; set; }
    }

    public class Thumbnail
    {
        public List<Thumbnail2> thumbnails { get; set; }
    }

    public class Thumbnail2
    {
        public string url{ get; set; }
        public int? width { get; set; }
        public int? height { get; set; }
    }

    public class UnsubscribeCommand
    {
        public string clickTrackingParams;
        public UnsubscribeEndpoint unsubscribeEndpoint;
    }

    public class UnsubscribeEndpoint
    {
        public List<string> channelIds;
        public string Params;
    }

    public class VideoDetails
    {
        public string videoId { get; set; }
        public string title { get; set; }
        public string lengthSeconds { get; set; }
        public string channelId { get; set; }
        public bool? isOwnerViewing { get; set; }
        public bool? isCrawlable { get; set; }
        public Thumbnail thumbnail { get; set; }
        public bool? allowRatings { get; set; }
        public string viewCount { get; set; }
        public string author { get; set; }
        public bool? isPrivate { get; set; }
        public bool? isUnpluggedCorpus { get; set; }
        public string musicVideoType { get; set; }
        public bool? isLiveContent { get; set; }
        public string externalVideoId { get; set; }
        public string durationSeconds { get; set; }
        public string durationIso8601 { get; set; }
    }

    public class VideostatsDelayplayUrl
    {
        public string baseUrl;
        public List<Header> headers;
    }

    public class VideostatsPlaybackUrl
    {
        public string baseUrl;
        public List<Header> headers;
    }

    public class VideostatsWatchtimeUrl
    {
        public string baseUrl;
        public List<Header> headers;
    }

    public class WebPlayerActionsPorting
    {
        public SubscribeCommand subscribeCommand;
        public UnsubscribeCommand unsubscribeCommand;
        public AddToWatchLaterCommand addToWatchLaterCommand;
        public RemoveFromWatchLaterCommand removeFromWatchLaterCommand;
    }

    public class WebPlayerConfig
    {
        public bool? useCobaltTvosDash;
        public WebPlayerActionsPorting webPlayerActionsPorting;
    }

