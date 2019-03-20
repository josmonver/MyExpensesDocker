using System;

namespace MyExpenses.Api.Infrastructure.HttpErros
{
    public interface IHttpErrorFactory
    {
        HttpError CreateFrom(Exception exception);
    }
}