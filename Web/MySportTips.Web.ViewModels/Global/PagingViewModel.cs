namespace MySportTips.Web.ViewModels.Global
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PagingViewModel
    {
        // The rest of the properties are used for the pagination
        public int PageNumber { get; set; }

        public int EventsCount { get; set; }

        public int ItemsPerPage { get; set; }

        public int PagesCount => (int)Math.Ceiling((double)this.EventsCount / this.ItemsPerPage);

        public bool HasPreviousPage => this.PageNumber > 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int PreviousPageNumber => this.PageNumber - 1;

        public int NextPageNumber => this.PageNumber + 1;
    }
}
