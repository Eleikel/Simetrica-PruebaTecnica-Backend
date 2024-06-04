using AutoMapper;


namespace TaskManager.Core.Application.Mappings.Base
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}
