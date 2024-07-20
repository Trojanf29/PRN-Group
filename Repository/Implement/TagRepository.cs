using Bloggie_Web.Data;
using Bloggie_Web.Models.Domain;
using Bloggie_Web.Repository.Contract;
using Microsoft.EntityFrameworkCore;

namespace Bloggie_Web.Repository.Implement
{
    public class TagRepository : ITagRepository
    {
        private readonly BloggieContext _context;

        public TagRepository(BloggieContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Tag>> GetAllTagsAsync()
        {
            var tags = await _context.Tags.ToListAsync();
            return tags.DistinctBy(tag => tag.Name);
            
        }
    }
}
