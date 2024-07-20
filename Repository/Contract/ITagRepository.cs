using Bloggie_Web.Models.Domain;

namespace Bloggie_Web.Repository.Contract
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAllTagsAsync();
    }
}
