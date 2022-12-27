﻿using AutoMapper;
using Blog_DAL.Models;
using Blog.DTO;
using System.Linq;
using System.Collections.Generic;
using Blog.ViewModels;

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

            CreateMap<UserViewModel, User>().ForMember(x => x.UserName, opt => opt.MapFrom(c => c.Login))
                          .ForMember(x => x.Roles, opt => opt.MapFrom(c => new List<Role>()));
            CreateMap<User, UserViewModel>().ForMember(x => x.Login, opt => opt.MapFrom(c => c.UserName));

            CreateMap<RegisterViewModel, User>().ForMember(x => x.UserName, opt => opt.MapFrom(c => c.Login))
                                             .ForMember(x => x.Roles, opt => opt.MapFrom(c => new List<Role>()));

            CreateMap<Post, PostDTO>().ForMember(x => x.Tags, opt => opt.MapFrom(c => c.Tags.Select(v => v.Id).ToList())); 
            CreateMap<PostDTO, Post>().ForMember(x => x.Tags, opt => opt.MapFrom(c => new List<Tag>()));

            CreateMap<Post, PostViewModel>();
            CreateMap<PostViewModel, Post>().ForMember(x => x.Tags, opt => opt.MapFrom(c => c.Tags.Where(v=>v.Selected)));

            CreateMap<Tag, TagDTO>();
            CreateMap<TagDTO, Tag>();

            CreateMap<Tag, TagViewModel>();
            CreateMap<TagViewModel, Tag>();

            CreateMap<RoleDTO, Role>();
            CreateMap<Role, RoleDTO>();

            CreateMap<Role, RoleViewModel>();
            CreateMap<RoleViewModel, Role>();

            CreateMap<CommentDTO, Comment>();
            CreateMap<Comment, CommentDTO>();

            CreateMap<CommentViewModel, Comment>();
            CreateMap<Comment, CommentViewModel>();

            CreateMap<Post, PostAndCommentsViewModel>();
            CreateMap<PostAndCommentsViewModel, Post>();
        }
    }
}