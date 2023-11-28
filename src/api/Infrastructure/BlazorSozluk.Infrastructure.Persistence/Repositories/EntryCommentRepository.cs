﻿using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorSozluk.Api.Infrastructure.Persistence.Repositories;

public class EntryCommentRepository : GenericRepository<EntryComment>, IEntrCommentRepository
{
    public EntryCommentRepository(DbContext context) : base(context)
    {
    }
}