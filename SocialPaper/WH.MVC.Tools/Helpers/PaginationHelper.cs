using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.Mvc
{
    public interface IPagedList
    {
        int PagesCount { get; }

        int TotalCount { get; set; }

        int PageIndex { get; set; }

        int PageSize { get; set; }

        bool IsPreviousPage { get; }

        bool IsNextPage { get; }
    }

    public class PagedList<T> : List<T>, IPagedList
    {
        public PagedList(IEnumerable<T> source, int index, int pageSize)
        {
            this.TotalCount = source.Count();
            this.PageSize = pageSize;
            this.PageIndex = index;
            this.AddRange(source.Skip(index * pageSize).Take(pageSize).ToList());
        }

        public PagedList(IQueryable<T> source, int index, int pageSize)
        {
            this.TotalCount = source.Count();
            this.PageSize = pageSize;
            this.PageIndex = index;
            this.AddRange(source.Skip(index * pageSize).Take(pageSize).ToList());
        }

        public PagedList(List<T> source, int index, int pageSize)
        {
            this.TotalCount = source.Count();
            this.PageSize = pageSize;
            this.PageIndex = index;
            this.AddRange(source.Skip(index * pageSize).Take(pageSize).ToList());
        }

        public int PagesCount
        {
            get
            {
                if (TotalCount % PageSize == 0)
                    return (TotalCount / PageSize) - 1;
                else
                    return TotalCount / PageSize;
            }
        }

        public int TotalCount { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public bool IsPreviousPage
        {
            get
            {
                return (PageIndex > 0);
            }
        }

        public bool IsNextPage
        {
            get
            {
                return ((PageIndex + 1) * PageSize) < TotalCount;
            }
        }
    }

    public static class PaginationHelper
    {
        public static PagedList<T> ToPagedList<T>(this IQueryable<T> source, int index, int pageSize)
        {
            return new PagedList<T>(source, index, pageSize);
        }

        public static PagedList<T> ToPagedList<T>(this IQueryable<T> source, int index)
        {
            return new PagedList<T>(source, index, 15);
        }

        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> source, int index, int pageSize)
        {
            return new PagedList<T>(source, index, pageSize);
        }

        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> source, int index)
        {
            return new PagedList<T>(source, index, 15);
        }
    }
}