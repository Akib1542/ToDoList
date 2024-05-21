using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLayer.Models
{
    public class Pager
    {
        public int TotalItems { get; private set;}
        public int CurrentPage { get; private set;}
        public int PageSize { get; private set;}
        public int TotalPages { get; private set;}
        public int StartPage { get; private set;}
        public int EndPage { get; private set;}

        public Pager()
        {
            
        }

        public Pager(int totalItems, int page, int pageSize=0)
        {
            int totalPage = (int)Math.Ceiling( ((decimal)totalItems) / (decimal)pageSize);
            int currentPage = page;

            int startPage = currentPage-2;
            int endPage = currentPage+2;

            if(startPage<1)
            {
                endPage = endPage - (startPage - 1);
                startPage = 1;
            }

            if (endPage > totalPage)
            {
                endPage = totalPage;
                if(endPage > 5)
                {
                    startPage = endPage - 9;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPage;
            StartPage = startPage;
            EndPage = endPage;  
        }

       


    }
}
