﻿
using TaskManager.Core.Domain.Entities;
using TaskManager.Core.Application.Mappings.Base;
using AutoMapper;

namespace TaskManager.Core.Application.Dtos.Task
{
    public class UpdateTaskDto : IMapFrom<Tasks>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Done { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateTaskDto, Tasks>().ReverseMap();
        }
    }
}
