using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Managers.Abstract;
using BLL.AnalyzerCore;
using DAL.DomainModels;
using DAL.Repositories.Abstract;

namespace BLL.Managers.Concrete
{
    public class HistoryManager : IHistoryManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HistoryManager(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ResponseData, ResponseDataDto>();
            });

            _mapper = config.CreateMapper();
        }
        public IEnumerable<WebSiteHistoryDto> GetFullHistory()
        {
            return _unitOfWork.WebSiteAnalyzedData.GetAll().Select(data => new WebSiteHistoryDto()
            {
                Id = data.Id,
                URL = data.AnalyzedWebSite.Url,
                RequestTime = data.RequestTime,
                CountPages = data.ResponseData.Count
            }).OrderByDescending(data => data.RequestTime);
        }

        public AnalysisDataDto GetRequestHistory(int requestId)
        {
            var requestData = _unitOfWork.WebSiteAnalyzedData.Find(data => data.Id == requestId).First();

            var result = new AnalysisDataDto();
            result.ResponseData = _mapper.Map<List<ResponseData>, List<ResponseDataDto>>(requestData.ResponseData)
                .OrderByDescending(data => data.ElapsedTime).ToList();
            result.SerializedData = JsonCustomConverter.ConvertCollection(result.ResponseData);
            result.Url = requestData.AnalyzedWebSite.Url;

            return result;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
