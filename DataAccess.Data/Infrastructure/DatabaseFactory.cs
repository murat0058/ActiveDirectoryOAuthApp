﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data.Infrastructure
{
    public class DatabaseFactory:Disposable, IDatabaseFactory
    {
        private ResourceManagerEntities dataContext;

        public ResourceManagerEntities Get()
        {
           return  dataContext??(dataContext=new ResourceManagerEntities());
        }
        protected override void DisposeCore()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }
}
