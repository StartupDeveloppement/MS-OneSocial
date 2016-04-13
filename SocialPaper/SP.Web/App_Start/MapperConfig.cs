using AutoMapper;
using SP.Model;
using SP.Web.Models;
using System;
using WH.MVC.Tools.Commun;

namespace SP.Web
{
    public static class MapperConfig
    {
        public static void RegisterMapping()
        {
            // Mapping des objets
            Mapper.CreateMap<ArticleCreate, Article>()
                .ForMember(dest => dest.Content, v => v.MapFrom(source => SecurityTools.GetHtmlWithoutScript(source.Content)));
            Mapper.CreateMap<ArticleCreateLink, Article>()
                .ForMember(dest => dest.isLink, v => v.UseValue(true));
            Mapper.CreateMap<ArticleEdit, Article>()
                .ForMember(dest => dest.Content, v => v.MapFrom(source => SecurityTools.GetHtmlWithoutScript(source.Content)));
            Mapper.CreateMap<ArticleEditLink, Article>();
            Mapper.CreateMap<Article, ArticleEditLink>();
            Mapper.CreateMap<Article, ArticleIndex>()
                .ForMember(dest => dest.isPublish, v => v.MapFrom(source => source.Publication == null ? "Non Publié" : "Publié"));
            Mapper.CreateMap<Article, ArticleContent>();
            Mapper.CreateMap<Article, ArticleEdit>();
            Mapper.CreateMap<ArticleEdit, Article>();
        }
    }
}
