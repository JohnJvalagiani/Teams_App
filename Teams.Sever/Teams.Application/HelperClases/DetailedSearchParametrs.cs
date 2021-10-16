using Application.HelperClases;

namespace Application.Services.Implementation
{
    public class DetailedSearchParametrs
    {
      
            public PagingParametrs pagingParametrs { get; set; }
            public taskSearch SearchPersonsBy { get; set; }
            public taskSearch OrderPersonsby { get; set; }
            public string SearchValue { get; set; }


    }
}