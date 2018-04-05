using System;
using System.Linq;
using System.Linq.Expressions;

namespace Expression_2.Mapping
{
    public class MappingGenerator
    {
        public Mapper<TSource, TDestination> Generate<TSource, TDestination>()
        {
            ParameterExpression source = Expression.Parameter(typeof(TSource), "");
            MemberInitExpression body = Expression.MemberInit(Expression.New(typeof(TDestination)),
                source.Type.GetProperties().Select(p => Expression.Bind(typeof(TDestination).GetProperty(p.Name), Expression.Property(source, p))));
            Expression<Func<TSource, TDestination>> expr = Expression.Lambda<Func<TSource, TDestination>>(body, source);

            return new Mapper<TSource, TDestination>(expr.Compile());
        }
    }
}