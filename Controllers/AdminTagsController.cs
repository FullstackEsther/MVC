using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BloggieMvc.Models.ViewModel;
using BloggieMvc.Context;
using BloggieMvc.Models.Entities;
using Org.BouncyCastle.Asn1.Iana;
using Microsoft.EntityFrameworkCore;
using BloggieMvc.Repositories.Interface;

namespace BloggieMvc.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository _tagRepository;
        public AdminTagsController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;

        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddTagRequest request)
        {
            Tag tag = new Tag
            {
                Name = request.Name,
                DisplayName = request.DisplayName
            };
            await _tagRepository.AddAsync(tag);
            return RedirectToAction("List");
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var tags = await _tagRepository.GetAllAsync();
            return View(tags);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = await _tagRepository.GetAsync(id);
            if (tag != null)
            {
                var EditTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    DisplayName = tag.DisplayName,
                    Name = tag.Name
                };
                return View(EditTagRequest);
            }
            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            Tag tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };
            var updatedTag = await _tagRepository.UpdateAsync(tag);
            if (updatedTag != null)
            {

            }
            else
            {

            }
            return RedirectToAction("Edit", new { id = tag.Id });
        }
        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var deleted = await _tagRepository.DeleteAsync(editTagRequest.Id);
            if (deleted != null)
            {
                return RedirectToAction("List");
            }
            else
            {
               return RedirectToAction("Edit", new { id = editTagRequest.Id }); 
            }
            
        }
    }
}