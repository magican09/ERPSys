

using DataDirectory.Common;
using Microsoft.EntityFrameworkCore;

namespace ConsoleAppForeTests;

public static class Extentions
{
    
    public static IQueryable<FooClassModel> ToDomain(this Microsoft.EntityFrameworkCore.DbSet<FooClass> foos) =>
                                                                    foos.Select(x => new FooClassModel
                                                                    {
                                                                        iD = x.Id.ToString(),
                                                                        name = x.Name
                                                                    }
                                                );

    /*public Microsoft.EntityFrameworkCore.DbSet<FooClass> ToDbSet(this IQueryable<FooClassModel> foo_models,DbSet<FooClass> db_foos) =>
        foo_models.Select(m_f => 
                                            db_foos.Select(db_f =>)
            
            );*/
}