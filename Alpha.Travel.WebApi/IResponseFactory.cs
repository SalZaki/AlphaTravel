namespace Alpha.Travel.WebApi
{
    using Alpha.Travel.WebApi.Models;
    using Alpha.Travel.Application.Common.Queries;
    using System.Collections.Generic;
    using System;

    public interface IResponseFactory
    {
        Response<T> CreateReponse<T>(T data, Type type, ResponseStatus status, string version) where T : BaseModel;

        PagedResponse<T> CreatePagedReponse<T>(IList<T> data, Type type, IPreviewQuery query, ResponseStatus status, string version) where T : BaseModel;
    }
}