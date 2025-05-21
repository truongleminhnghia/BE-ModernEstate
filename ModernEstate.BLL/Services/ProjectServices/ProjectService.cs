

using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ModernEstate.BLL.JWTServices;
using ModernEstate.BLL.Services.AddressServices;
using ModernEstate.BLL.Services.HistoryServices;
using ModernEstate.BLL.Services.InvetorServices;
using ModernEstate.Common.Enums;
using ModernEstate.Common.Exceptions;
using ModernEstate.Common.Models.Pages;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.Common.srcs;
using ModernEstate.DAL;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Services.ProjectServices
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProjectService> _logger;
        private readonly IMapper _mapper;
        private readonly Utils _utils;
        private readonly IAddressService _addressService;
        private readonly IInvetorService _invetorService;
        private readonly IHistoryService _historyService;
        private readonly IJwtService _jwtService;
        public ProjectService(IUnitOfWork unitOfWork, ILogger<ProjectService> logger,
                                IMapper mapper, Utils utils, IAddressService addressService,
                                IInvetorService invetorService, IHistoryService historyService,
                                IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _utils = utils;
            _addressService = addressService;
            _invetorService = invetorService;
            _historyService = historyService;
            _jwtService = jwtService;
        }

        public async Task<ProjectResponse> GetById(Guid id)
        {
            try
            {
                var project = await _unitOfWork.Projects.FindById(id);
                if (project == null) throw new AppException(ErrorCode.NOT_FOUND);
                return _mapper.Map<ProjectResponse>(project);
            }
            catch (AppException ex)
            {
                _logger.LogWarning(ex, "AppException occurred: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred: {Message}", ex.Message);
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<PageResult<ProjectResponse>> GetProjects(EnumProjectType? projectType, string? title,
                                                                float? minArea, float? maxArea, EnumProjectStatus?
                                                                projectStatus, string? invetorName,
                                                                int pageCurrent, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.Projects.FindProjects(projectType, title, minArea, maxArea, projectStatus, invetorName);
                if (result == null) throw new AppException(ErrorCode.LIST_EMPTY);
                var pagedResult = result.Skip((pageCurrent - 1) * pageSize).Take(pageSize).ToList();
                var total = result.Count();
                var data = _mapper.Map<List<ProjectResponse>>(pagedResult);
                if (data == null || !data.Any()) throw new AppException(ErrorCode.LIST_EMPTY);
                var pageResult = new PageResult<ProjectResponse>(data, pageSize, pageCurrent, total);
                return pageResult;
            }
            catch (AppException ex)
            {
                _logger.LogWarning(ex, "AppException occurred: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred: {Message}", ex.Message);
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<bool> SaveProject(ProjectRequest request)
        {
            try
            {
                // Invetor? inventor = null;
                var proejctExisting = await _unitOfWork.Projects.FindByTitle(request.Title);
                if (proejctExisting != null) throw new AppException(ErrorCode.HAS_EXISTED);
                Guid addressId = await _addressService.CreateAddress(request.AddressRequest);
                if (addressId == null) throw new AppException(ErrorCode.NOT_NULL);
                var inventorExisting = await _unitOfWork.Invetors.FindByEmail(request.InvetorRequest.Email);
                if (inventorExisting == null)
                {
                    inventorExisting = await _invetorService.CreateInventor(request.InvetorRequest);
                }
                var project = _mapper.Map<Project>(request);
                project.AddressId = addressId;
                // project.Code = await _utils.GenerateUniqueBrokerCodeAsync("P_");
                project.Code = "PC1234";
                project.InvetorId = inventorExisting.Id;
                project.CreatedAt = DateTime.UtcNow;
                project.UpdatedAt = DateTime.UtcNow;
                await _unitOfWork.Projects.CreateAsync(project);
                History history = await setupHistory(EnumHistoryChangeType.INSERT, project.Id, "Create proejct");
                project.Histories.Add(history);
                var image = await setupImage(request.ImageRequests, project.Id);
                project.Images = image;
                await _unitOfWork.SaveChangesWithTransactionAsync();
                return true;
            }
            catch (AppException ex)
            {
                _logger.LogWarning(ex, "AppException occurred: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred: {Message}", ex.Message);
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public Task<bool> UpdateProject(Guid id, UpdateProjectRequest request)
        {
            throw new NotImplementedException();
        }

        private async Task<History> setupHistory(EnumHistoryChangeType type, Guid id, string reason)
        {
            string currentId = _jwtService.GetAccountId();
            if (currentId == null) throw new AppException(ErrorCode.NOT_NULL);
            var historyEntity = new History
            {
                TypeHistory = type,
                ReasonChange = reason,
                ProjectId = id,
                ChangeBy = currentId,
            };
            var history = await _historyService.CreateHistory(historyEntity);
            return history;
        }

        private async Task<List<Image>> setupImage(List<ImageRequest> imageRequests, Guid id)
        {
            var listImage = new List<Image>();
            foreach (var imageReq in imageRequests)
            {
                var imageEntity = _mapper.Map<Image>(imageReq);
                imageEntity.ProjectId = id;
                await _unitOfWork.Images.CreateAsync(imageEntity);
                listImage.Add(imageEntity);
            }
            return listImage;
        }
    }
}