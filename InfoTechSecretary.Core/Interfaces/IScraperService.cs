﻿using InfoTechSecretary.Core.Model;
using InfoTechSecretary.Database.Values;

namespace InfoTechSecretary.Core.Interfaces;

public interface IScraperService
{
    IBlogScraper GetScraperBlogListAsync(BlogProvider provider);

    Task<IEnumerable<BlogInfo>> GetScraperBlogListAsync(CancellationToken cancellationToken = default);

    Task ProcessBlogAsync(BlogInfo blogInfo, CancellationToken cancellationToken = default);
}
