namespace CriptoInvestAnalyst
{
    public class HabrExtraUrlArguments : HtmlLoaderExtraUrlArguments
    {
        public string GetUrlExtraArguments(int id)
        {
            return id.ToString();
        }
    }
}
