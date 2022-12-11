﻿using AutoMapper;
using Blog_DAL.Models;
using Blog.ViewModels.Account;
using Blog.DTO;
using System.Linq;
using System.Collections.Generic;
using Blog.DTO.Request;

namespace Blog
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDTO, User>().ForMember(x => x.UserName, opt => opt.MapFrom(c => c.Login))
                                      .ForMember(x => x.Roles, opt => opt.MapFrom(c => new List<Role>()));
            CreateMap<User, UserDTO>().ForMember(x => x.Login, opt => opt.MapFrom(c => c.UserName))
                                      .ForMember(x => x.Roles, opt => opt.MapFrom(c => c.Roles.Select(v=>v.Id).ToList()));

            CreateMap<UserProfileDTO, User>().ForMember(x => x.UserName, opt => opt.MapFrom(c => c.Login))
                                             .ForMember(x => x.Roles, opt => opt.MapFrom(c => new List<Role>()));

            CreateMap<Post, PostDTO>().ForMember(x => x.Tags, opt => opt.MapFrom(c => c.Tags.Select(v => v.Id).ToList())); 
            CreateMap<PostDTO, Post>();

            CreateMap<Tag, TagDTO>();
            CreateMap<TagDTO, Tag>();

            CreateMap<RoleDTO, Role>();
            CreateMap<Role, RoleDTO>();

            CreateMap<CommentDTO, Comment>();
            CreateMap<Comment, CommentDTO>();
        }
    }
}