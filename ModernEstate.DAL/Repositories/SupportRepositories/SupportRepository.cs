﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.SupportRepositories
{
    public class SupportRepository : GenericRepository<Support>, ISupportRepository
    {
        public SupportRepository(ApplicationDbConext context) : base(context) { }

       
    }
    
}
