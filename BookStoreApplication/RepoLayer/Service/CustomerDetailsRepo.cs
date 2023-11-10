using Microsoft.Extensions.Configuration;
using RepoLayer.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Service
{
    public class CustomerDetailsRepo
    {
        private readonly IConfiguration _configuration;
        private readonly dbBooksContext _dbBooksContext;
        public CustomerDetailsRepo(IConfiguration configuration,dbBooksContext dbBooksContext)
        {
            this._configuration = configuration;
            this._dbBooksContext = dbBooksContext;
        }
    }
}
