using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController: ControllerBase
    {
        private readonly IcommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        private readonly UserManager<AppUser> _userManager;
        public CommentController(IcommentRepository commentRepo, IStockRepository stockRepo, UserManager<AppUser> userManager)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(){

            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var comments = await _commentRepo.GetAllAsync();
            var commentDto = comments.Select(s=>s.ToCommentDto());
            return Ok(commentDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute]int id){

            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var comment = await _commentRepo.GetByIdAsync(id);

             if(comment == null){
                return NotFound();
             }

             return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentDto createCommentDto){

            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            if(!await _stockRepo.StockExistAsync(stockId)){
                return BadRequest("Stock does not exist");
            }

            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var commentModel = createCommentDto.ToCommentModel(stockId);
            commentModel.AppUserId = appUser.Id;
            await _commentRepo.CreateAsync(commentModel);

            return CreatedAtAction(nameof(GetById), new {id = commentModel.CommentId}, commentModel.ToCommentDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody] UpdateCommentDto commentDto){

            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var comment = await _commentRepo.UpdateAsync(id, commentDto);

            if (comment == null)
            {
                return NotFound("Comment not found");
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id){

            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var comment = await _commentRepo.DeleteAsync(id);

            if(comment == null){
                return NotFound();
            }

            return Ok(comment);
        }
    }
}