namespace DatabaseAccessLayer.Models
{
    public class Filters
    {
        public Filters(string filterstring)
        {
            Filterstring = filterstring ?? "all-all-all";
            string [] filters = Filterstring.Split('-');
            CategoryId = filters[0];
            Due = filters[1];
            StatusId = filters[2];
        }
        public string Filterstring { get;}
        public String CategoryId { get;}
        public String Due { get;}
        public String StatusId { get;}
        public bool HasCategory => CategoryId.ToLower() != "all";
        public bool HasDue => Due.ToLower() != "all";   
        public bool HasStatus => StatusId.ToLower() != "all";
        public static Dictionary<string,string>DueFilterValues => new Dictionary<string,string>
        {
            {"future","Future"},
            {"past","Past" },
            {"today","Today"}
        };
        public bool IsPast => Due.ToLower() == "past";
        public bool IsFuture => Due.ToLower() == "future";
        public bool IsToday => Due.ToLower() == "today";
    }
}
