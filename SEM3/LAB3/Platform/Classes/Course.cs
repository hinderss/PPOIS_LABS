namespace Platform.Classes
{
    public class Course
    {
        public int _id;
        private string _name;
        private string _description;
        private string _skills;
        private string _tag;
        private decimal _price;
        private int _duration;
        private string _resource;

        public decimal Price
        {
            get
            {
                return _price;
            }
        }

        public Course(int id, string name, string description, string skills, string tag, decimal price, int duration, string resource)
        {
            _id = id;
            _name = name;
            _description = description;
            _skills = skills;
            _tag = tag;
            _price = price;
            _duration = duration;
            _resource = resource;
        }

        public string JSON
        {
            get { return ToJSON(); }
        }

        private string ToJSON()
        {
            var json = new {
                id = _id,
                name = _name,
                description = _description,
                skills = _skills,
                tag = _tag,
                price = _price,
                duration = _duration,
                resource = _resource,
                alreadypurchased = false
            };

            return System.Text.Json.JsonSerializer.Serialize(json);
        }
    }
}
