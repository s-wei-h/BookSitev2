﻿using BookSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BookSite.Models.ViewModels;

namespace BookSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IBookRepository _repository;

        public int PageSize = 5;

        public HomeController(ILogger<HomeController> logger, IBookRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IActionResult Index(int page = 1)
        {
            //create new booklistviewmodel
            return View(new BookListViewModel
            {
                BookShelf = _repository.BookShelf
                    .OrderBy(p => p.BookID)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize)
                ,
                //create new paging info
                PagingInfo = new PagingInfo
                {
                    Currentpage = page,
                    ItemsPerPage = PageSize,
                    TotalNumItems = _repository.BookShelf.Count()
                }
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
