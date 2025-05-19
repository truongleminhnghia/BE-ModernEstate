using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Common.Enums;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.PostPackageRepositories
{
    public class PostPackageRepository : GenericRepository<PostPackage>, IPostPackageRepository
    {
        public PostPackageRepository(ApplicationDbConext context) : base(context) { }

       
    }
    
}
