﻿using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Contracts.Queries
{
    public class CatalogExportsByFilter
    {
        /// <summary>
        /// How many query results to return in this request at most (default 10)
        /// </summary>
        [FromQuery(Name = "limit")]
        public int Limit { get; set; } = 10;

        /// <summary>
        /// How many results to skip (default 0)
        /// </summary>
        [FromQuery(Name = "offset")]
        public int Offset { get; set; } = 0;
    }
}