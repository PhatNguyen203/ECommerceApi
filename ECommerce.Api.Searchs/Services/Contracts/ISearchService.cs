using ECommerce.Api.Searchs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Searchs.Services.Contracts
{
    public interface ISearchService
    {
        Task<(Boolean isSuccess , dynamic searchResult)> SearchASync(int customerId);
    }
}
