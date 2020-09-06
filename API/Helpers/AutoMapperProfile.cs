using System;
using AutoMapper;


namespace LikeButton.Helpers
{
    public class AutoMapperProfile : Profile
    {
       

        private static object GetDefaultValue(Type t)
        {
            return t.IsValueType ? Activator.CreateInstance(t) : null;
        }
    }
}
