using BL.Services.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface ICaseService
    {
        //Create a case
        Task<CaseDTO> CreateCase(CaseDTO caseDTO);
        //Update a case
        Task<CaseDTO> UpdateCase(CaseDTO caseDTO);

        //DeleteCase(int id);
        Task<Response<string>> DeleteCase(int id, int userId);
        // Update case status
        Task<Response<string>> UpdateCaseStatus(int caseId, string newStatus);

        // Get all cases
        Task<List<CaseDTO>> GetAllCases();

        // Get a case by ID
        Task<CaseDTO> GetCaseById(int id);


    }
    public class CaseManagmentService : ICaseService
    {
        private readonly List<CaseDTO> _caseStorage;
        private int _currentId = 1;
        //implement the service

            private readonly IFileManagementService _fileManagementService;
            private readonly INotificationService _notificationService;

            public CaseManagmentService(IFileManagementService fileManagementService, INotificationService notificationService) {
            _fileManagementService = fileManagementService;
            _notificationService = notificationService;
            _caseStorage = new List<CaseDTO>();

            InitializeHardcodedCases();
        }
        private void InitializeHardcodedCases()
        {
            _caseStorage.AddRange(new List<CaseDTO>
        {
            new CaseDTO
            {
                Id = _currentId++,
                CaseNumber = "C001",
                CaseName = "Case Alpha",
                Description = "This is the first test case.",
                Status = "Open",
                Priority = "High",
                Severity = "Critical",
                Type = "Incident",
                Parties = new List<string> { "Alice", "Bob" },
                CaseTitle = "Alpha Case Title"
            },
            new CaseDTO
            {
                Id = _currentId++,
                CaseNumber = "C002",
                CaseName = "Case Beta",
                Description = "This is the second test case.",
                Status = "In Progress",
                Priority = "Medium",
                Severity = "Moderate",
                Type = "Issue",
                Parties = new List<string> { "Charlie", "Dana" },
                CaseTitle = "Beta Case Title"
            },
            new CaseDTO
            {
                Id = _currentId++,
                CaseNumber = "C003",
                CaseName = "Case Gamma",
                Description = "This is the third test case.",
                Status = "Closed",
                Priority = "Low",
                Severity = "Minor",
                Type = "Request",
                Parties = new List<string> { "Eve", "Frank" },
                CaseTitle = "Gamma Case Title"
            }
        });
        }

            //Create a case
            public async Task<CaseDTO> CreateCase(CaseDTO CaseDTO)
        {
            CaseDTO.Id = _currentId++;
            _caseStorage.Add(CaseDTO);
            //Create a case

            // Validate case data
            if (!ValidateCaseData(CaseDTO))
            {
                throw new Exception("Invalid case data.");
            }
            var caseId = SaveCaseRecord(CaseDTO);
            await _fileManagementService.CreateCaseFileAsync(caseId, CaseDTO);
            await _notificationService.SendCaseCreationNotificationAsync(CaseDTO.Parties, caseId);

            return await Task.FromResult(CaseDTO);
            //return caseDTO;
        }
        private bool ValidateCaseData(CaseDTO caseData)
        {
            // Example validation logic
            return !string.IsNullOrEmpty(caseData.CaseTitle) && caseData.Parties.Any();
         }

    private string SaveCaseRecord(CaseDTO caseData)
    {
        // Simulate saving the case record and return a case ID
        return Guid.NewGuid().ToString();
    }


//Update a case

        public async Task<CaseDTO> UpdateCase(CaseDTO caseDTO)
        {
            var existingCase = _caseStorage.FirstOrDefault(c => c.Id == caseDTO.Id);
            if (existingCase == null)
            {
                throw new KeyNotFoundException("Case not found");
            }

            // Update fields
            existingCase.CaseName = caseDTO.CaseName;
            existingCase.Description = caseDTO.Description;
            existingCase.Status = caseDTO.Status;

            return await Task.FromResult(existingCase);
        }
        //Delete a case
        public async Task<Response<string>> DeleteCase(int id, int userId)
        {
             var caseToDelete = _caseStorage.FirstOrDefault(c => c.Id == id);
        if (caseToDelete == null)
        {
            return new Response<string>
            {
                success = false,
                data = "Case not found"
            };
        }

        _caseStorage.Remove(caseToDelete);

        return await Task.FromResult(new Response<string>
        {
            success = true,
            data = "Case deleted successfully"
        });
        }

        //update case status
        public async Task<Response<string>> UpdateCaseStatus(int caseId, string newStatus)
        {
            var existingCase = _caseStorage.FirstOrDefault(c => c.Id == caseId);
            if (existingCase == null)
            {
                return new Response<string>
                {
                    success = false,
                    data = "Case not found"
                };
            }

            existingCase.Status = newStatus;

            return await Task.FromResult(new Response<string>
            {
                success = true,
                data = "Case status updated successfully"
            });
        }

        //get all cases
        public async Task<List<CaseDTO>> GetAllCases()
        {
            return await Task.FromResult(_caseStorage);
        }
        // Get a case by ID
        public async Task<CaseDTO> GetCaseById(int id)
        {
            var caseDetails = _caseStorage.FirstOrDefault(c => c.Id == id);
            if (caseDetails == null)
            {
                throw new KeyNotFoundException("Case not found");
            }

            return await Task.FromResult(caseDetails);
        }



    }
}
