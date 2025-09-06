namespace CarProjectCQRS.CQRSPattern.Queries.AboutQueries
{
    public class GetAboutByIdQuery
    {
        public int Id { get; set; }

        public GetAboutByIdQuery()
        {
        }

        public GetAboutByIdQuery(int id)
        {
            Id = id;
        }
    }
}
