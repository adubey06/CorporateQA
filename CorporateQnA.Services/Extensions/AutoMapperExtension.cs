using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;


namespace CorporateQnA.Services.Extensions
{
    static class AutoMapperExtension
    {
        static IMapper mapper = new AutoMapperConfiguration().GetConfiguration() ;

        public static T MapTo<T>(this object source)
        {
            return (source == default(object) ? default(T) : mapper.Map<T>(source));
        }

        public static IEnumerable<T> MapCollectionTo<T>(this IEnumerable<object> source)
        {
            return (source == null ? null : mapper.Map<IEnumerable<T>>(source));
        }
    }
}
