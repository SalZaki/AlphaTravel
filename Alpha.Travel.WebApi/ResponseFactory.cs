namespace Alpha.Travel.WebApi
{
    using Alpha.Travel.WebApi.Models;
    using Alpha.Travel.Application.Common.Queries;
    using System.Collections.Generic;

    public interface IResponseFactory
    {
        Response<T> CreateReponse<T>(T dto, string status, string version) where T : class;

        PagedResponse<T> CreatePagedReponse<T>(IList<T> dto, IPreviewQuery query, string status, string version) where T : class;
    }

    public class ResponseFactory : IResponseFactory
    {
        public Response<T> CreateReponse<T>(T dto,
            string status,
            string version) where T : class
        {
            return new Response<T>
            {
                Data = dto,
                Status = status,
                Version = version
            };
        }

        public PagedResponse<T> CreatePagedReponse<T>(IList<T> dto,
            IPreviewQuery query,
            string status,
            string version) where T : class
        {
            return new PagedResponse<T>
            {
                Data = dto,
                Pagination = new Pagination
                {
                    TotalRecords = dto.Count,
                    PageCount = query.GetTotalPages(dto.Count),
                    PageNumber = query.PageNumber,
                    HasNext = query.HasNext(dto.Count),
                    HasPrevious = query.HasPrevious(),
                    PageSize = query.PageSize
                },
                Status = status,
                Version = version,
                Metadata = new MetaData
                {
                   
                }
            };
        }
    }

    //[ExcludeFromCodeCoverage]
    //public static class ResponseFactory
    //{
    //    private readonly IMapper _mapper;

    //    public ResponseFactory(IMapper mapper)
    //    {
    //        _mapper = mapper;
    //    }

    //    public Response<Customer> CreateCustomerReponse(CustomerPreviewDto dto)
    //    {
    //        return new Response<Customer>
    //        {
    //            Data = _mapper.Map<Customer>(dto),
    //            Status = "success",
    //            Version = "1.0.0"
    //        };
    //    }

    //    public static Response<Destination> CreateDestinationReponse<T>(DestinationPreviewDto dto) where T : class
    //    {
    //        return new Response<T>();
    //    }

    //    public static PagedResponse<T> CreatePagedCustomerReponse<T>(IList<CustomerPreviewDto> dto,
    //        IMapper mapper,
    //        IPreviewQuery query,
    //        string version) where T : class
    //    {
    //        return new PagedResponse<T>
    //        {
    //            Data = mapper.Map<IList<T>>(dto),
    //            Pagination = new Pagination
    //            {
    //                TotalRecords = dto.Count,
    //                PageCount = query.GetTotalPages(dto.Count),
    //                PageNumber = query.PageNumber,
    //                HasNext = query.HasNext(dto.Count),
    //                HasPrevious = query.HasPrevious(),
    //                PageSize = query.PageSize
    //            },
    //            Status = status,
    //            Version = version
    //        };
    //    }

    //    public static PagedResponse<T> CreatePagedDestinationReponse<T>(IList<DestinationPreviewDto> dto,
    //        IMapper mapper,
    //        IPreviewQuery query,
    //        string version) where T : class
    //    {
    //        return new PagedResponse<T>
    //        {
    //            Data = mapper.Map<IList<T>>(dto),
    //            Pagination = new Pagination
    //            {
    //                TotalRecords = dto.Count,
    //                PageCount = query.GetTotalPages(dto.Count),
    //                PageNumber = query.PageNumber,
    //                HasNext = query.HasNext(dto.Count),
    //                HasPrevious = query.HasPrevious(),
    //                PageSize = query.PageSize
    //            },
    //            Status = "success",
    //            Version = version
    //        };
    //    }
    //}
}