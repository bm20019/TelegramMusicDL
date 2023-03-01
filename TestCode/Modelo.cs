namespace TestCode;


public class Modelo{
    public Contents contents {get;set;}
}

public class Contents
{
    public TabbedSearchResultsRenderer tabbedSearchResultsRenderer {get; set;}
}

public class TabbedSearchResultsRenderer{
    public Tabs[] tabs {get;set;}
}

public class Tabs
{
    public TabRenderer tabRenderer{get;set;}
}

public class TabRenderer
{
    public Content content {get;set;}
}

public class Content{
    public SectionListRenderer sectionListRenderer {get;set;}
}

public class SectionListRenderer
{
    public Contents2[] contents {get;set;}
}

public class Contents2
{
    public MusicShelfRenderer musicShelfRenderer {get;set;}
}

public class MusicShelfRenderer
{
    public Contents3[] contents {get;set;}
}

public class Contents3{
    public MusicResponsiveListItemRenderer musicResponsiveListItemRenderer {get;set;}
}

public class MusicResponsiveListItemRenderer
{
    public Thumbnail thumbnail {get;set;}
    //public Overlay overlay {get;set;}
   public FlexColumns[] flexColumns {get;set;}
}

#region Thumbnail
public class Thumbnail
{
    public MusicThumbnailRenderer musicThumbnailRenderer {get;set;}
}

public class MusicThumbnailRenderer
{
    public Thumbnail2 thumbnail {get;set;}
}
public class Thumbnail2
{
    public Thumbnails[] thumbnails {get;set;}
}

public class Thumbnails
{
    public string url {get;set;}
    public int width {get;set;}
    public int height {get;set;}
}
#endregion

#region Overlay
public class Overlay
{

}
public class MusicItemThumbnailOverlayRenderer{

}
public class Content4{

} 

public class MusicPlayButtonRenderer{

}
#endregion

#region FlexColumns
public class FlexColumns
{
    public MusicResponsiveListItemFlexColumnRenderer musicResponsiveListItemFlexColumnRenderer {get;set;}   
}

public class MusicResponsiveListItemFlexColumnRenderer
{
    public Text text {get;set;}
}

public class Text
{
    public Runs[] runs {get;set;}
}

public class Runs{
    public string text {get;set;}
    public NavigationEndpoint navigationEndpoint {get;set;}
}

public class NavigationEndpoint{
    public WatchEndpoint watchEndpoint{get;set;}
}

public class WatchEndpoint{
    public string videoId{get;set;}
}
#endregion