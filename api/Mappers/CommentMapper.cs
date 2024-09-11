using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Models;
using Microsoft.AspNetCore.Components.Web;

namespace api.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment comment)
        {
            return new CommentDto{
                CommentId = comment.CommentId,
                title = comment.title,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                CreatedBy = comment.AppUser.UserName,
                StockId = comment.StockId
            };
        }
        public static Comment ToCommentModel(this CreateCommentDto commentDto, int StockId){
            return new Comment{
                title = commentDto.title,
                Content = commentDto.Content,
                StockId = commentDto.StockId
            };
        }
        public static Comment ToCommentFromUpdate(this UpdateCommentDto commentDto)
        {
            return new Comment
            {
                title = commentDto.title,
                Content = commentDto.Content,
            };
        }
     }
}