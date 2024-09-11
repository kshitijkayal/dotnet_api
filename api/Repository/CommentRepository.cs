using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comment;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : IcommentRepository
    {
        private readonly ApplicationDBContext _context;

        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Comment?> CreateAsync(Comment comment)
        {
            await _context.Comment.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var commentModel = await _context.Comment.FirstOrDefaultAsync(c=> c.CommentId==id);

            if(commentModel == null){
                return null;
            }

            _context.Comment.Remove(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comment.Include(a => a.AppUser).ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comment.FindAsync(id);
        }

        public async Task<Comment?> UpdateAsync(int id, UpdateCommentDto updateCommentDto)
        {
            var existingComment = await _context.Comment.Include(a => a.AppUser).FirstOrDefaultAsync(c => c.CommentId==id);

            if(existingComment == null){
                return null;
            }
            existingComment.title = updateCommentDto.title;
            existingComment.Content = updateCommentDto.Content;

            await _context.SaveChangesAsync();
            return existingComment;
        }
    }
}