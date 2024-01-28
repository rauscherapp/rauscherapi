
namespace Application.Helpers
{
    public class PageLinks
    {
        public string PreviousPageLink { get; }
        public string NextPageLink { get; }

        public PageLinks(string previousPageLink, string nextPageLink)
        {
            PreviousPageLink = previousPageLink;
            NextPageLink = nextPageLink;
        }
    }
}
