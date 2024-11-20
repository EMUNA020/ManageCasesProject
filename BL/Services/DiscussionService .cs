using BL.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IDiscussionService
    {
        Task<string> AddDiscussion(string caseId, DiscussionDTO discussion);
        Task<List<DiscussionDTO>> GetDiscussions(string caseId);
    }

    public class DiscussionService : IDiscussionService
    {
        private readonly Dictionary<string, List<DiscussionDTO>> _discussions;

        public DiscussionService()
        {
            _discussions = new Dictionary<string, List<DiscussionDTO>>();
        }

        public async Task<string> AddDiscussion(string caseId, DiscussionDTO discussion)
        {
            discussion.DiscussionId = Guid.NewGuid().ToString();
            discussion.CreatedAt = DateTime.UtcNow;

            if (!_discussions.ContainsKey(caseId))
                _discussions[caseId] = new List<DiscussionDTO>();

            _discussions[caseId].Add(discussion);

            return await Task.FromResult(discussion.DiscussionId);
        }

        public async Task<List<DiscussionDTO>> GetDiscussions(string caseId)
        {
            if (_discussions.TryGetValue(caseId, out var discussions))
                return await Task.FromResult(discussions);

            return new List<DiscussionDTO>();
        }
    }
}
