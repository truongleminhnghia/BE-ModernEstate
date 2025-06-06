﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.TagRepositories
{
    public interface ITagRepository : IGenericRepository<Tag>
    {
        Task<Tag> FindByTitle(string title);
    }
   
}
