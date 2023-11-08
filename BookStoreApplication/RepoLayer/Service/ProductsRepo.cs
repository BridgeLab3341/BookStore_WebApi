using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepoLayer.Context;
using RepoLayer.Context.Models;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Service
{
    public class ProductsRepo : IProductsRepo
    {
        private readonly IConfiguration _configuration;
        private readonly dbBooksContext _booksContext;
        public ProductsRepo(IConfiguration configuration, dbBooksContext booksContext)
        {
            this._configuration = configuration;
            this._booksContext = booksContext;
        }
    }
}
