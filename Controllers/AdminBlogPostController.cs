using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggieMvc.Models.Entities;
using BloggieMvc.Models.ViewModel;
using BloggieMvc.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BloggieMvc.Controllers
{
    public class AdminBlogPostController : Controller
    {
        private readonly ITagRepository _tagRepository;
        private readonly IBlogPostRepository _blogPostRepository;
        public AdminBlogPostController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
        {
            _tagRepository = tagRepository;
            _blogPostRepository = blogPostRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var tag = await _tagRepository.GetAllAsync();
            var model = new AddBlogPostRequest
            {
                Tags = tag.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest request)
        {
            var blogPost = new BlogPost
            {
                Author = request.Author,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                Heading = request.Heading,
                PageTitle = request.PageTitle,
                PublishedDate = request.PublishedDate,
                ShortDescription = request.ShortDescription,
                UrlHandle = request.UrlHandle,
                Visible = request.Visible,
            };
            var selectedTags = new List<Tag>();
            foreach (var item in request.SelectedTags)
            {
                var guidId = Guid.Parse(item);
                var existingTag = await _tagRepository.GetAsync(guidId);
                if (existingTag != null)
                {
                    selectedTags.Add(existingTag);
                }
            }
            blogPost.Tags = selectedTags;
            await _blogPostRepository.AddAsync(blogPost);
            return RedirectToAction("Add");
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var blogPosts = await _blogPostRepository.GetAllAsync();
            return View(blogPosts);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)

        {
            var blogPost = await _blogPostRepository.GetAsync(id);
            var tagModel = await _tagRepository.GetAllAsync();
            //Map the domain with thw view model
            if (blogPost != null)
            {
                var model = new EditBlogPostRequest
                {
                    Author = blogPost.Author,
                    Content = blogPost.Content,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    Heading = blogPost.Heading,
                    Id = blogPost.Id,
                    PageTitle = blogPost.PageTitle,
                    PublishedDate = blogPost.PublishedDate,
                    Visible = blogPost.Visible,
                    ShortDescription = blogPost.ShortDescription,
                    UrlHandle = blogPost.UrlHandle,
                    Tags = tagModel.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    SelectedTags = blogPost.Tags.Select(x => x.Id.ToString()).ToArray()
                };
                return View(model);
            }
            return View(null);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogPostRequest editBlogPostRequest)
        {
            //map viewmodel back to domain model
            var blogPost = new BlogPost
            {
                Author = editBlogPostRequest.Author,
                Id = editBlogPostRequest.Id,
                Content = editBlogPostRequest.Content,
                FeaturedImageUrl = editBlogPostRequest.FeaturedImageUrl,
                Heading = editBlogPostRequest.Heading,
                PageTitle = editBlogPostRequest.PageTitle,
                PublishedDate = editBlogPostRequest.PublishedDate,
                ShortDescription = editBlogPostRequest.ShortDescription,
                UrlHandle = editBlogPostRequest.UrlHandle,
                Visible = editBlogPostRequest.Visible
            };
            //get the list of tags 
            var selectedTags = new List<Tag>();
            foreach (var tag in editBlogPostRequest.SelectedTags)
            {
                if (Guid.TryParse(tag, out var selectedTag))
                {
                  var getTag = await _tagRepository.GetAsync(selectedTag);
                  if (getTag != null)
                  {
                     selectedTags.Add(getTag);
                  }
                }
            }
            blogPost.Tags = selectedTags;
           var updatedBlog = await _blogPostRepository.UpdateAsync(blogPost);
           if (updatedBlog != null)
           {
                return RedirectToAction("Edit");
           }
           //show error message
           return RedirectToAction("Edit");

        }

         [HttpPost]
        public async Task<IActionResult> Delete(EditBlogPostRequest editBlogPostRequest)
        {
            var deletedBlog = await _blogPostRepository.DeleteAsync(editBlogPostRequest.Id);
            if (deletedBlog != null)
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new {id = editBlogPostRequest.Id});
        }
    }
}