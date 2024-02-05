namespace MacrixPracticalTask_Client.Config
{
    public class Config
    {
        public string MainUrl { get; set; }

        public class MethodNamesConfig
        {
            public class GetAllConfig
            {
                public string GetAllUrl { get; set; }
                public string PageNumber { get; set; }
                public string PageSize { get; set; }
                public string OrderBy { get; set; }
            }
            public GetAllConfig GetAll { get; set; } = new GetAllConfig { };
            public string GetPersonByIdUrl { get; set; }
            public string CreatePersonUrl { get; set; }
            public string UpdatePersonUrl { get; set; }
            public string DeletePersonUrl { get; set; }
        }
        public MethodNamesConfig MethodNames { get; set; } = new MethodNamesConfig { };
    }
}
