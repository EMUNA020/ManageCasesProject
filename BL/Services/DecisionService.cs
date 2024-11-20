using BL.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IDecisionService
    {
        Task<string> AddDecision(string caseId, DecisionDTO decision);
        Task<List<DecisionDTO>> GetDecisions(string caseId);
    }
    public class DecisionService : IDecisionService
    {
        private readonly Dictionary<string, List<DecisionDTO>> _decisions;

        public DecisionService()
        {
            _decisions = new Dictionary<string, List<DecisionDTO>>();
        }

        public async Task<string> AddDecision(string caseId, DecisionDTO decision)
        {
            decision.DecisionId = Guid.NewGuid().ToString();
            decision.CreatedAt = DateTime.UtcNow;

            if (!_decisions.ContainsKey(caseId))
                _decisions[caseId] = new List<DecisionDTO>();

            _decisions[caseId].Add(decision);

            return await Task.FromResult(decision.DecisionId);
        }

        public async Task<List<DecisionDTO>> GetDecisions(string caseId)
        {
            if (_decisions.TryGetValue(caseId, out var decisions))
                return await Task.FromResult(decisions);

            return new List<DecisionDTO>();
        }
    }
}
