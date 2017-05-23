using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Enums;
using BLL.HelpModels;
using BLL.Managers.Abstract;
using BLL.AnalyzerCore;
using DAL.DomainModels;
using DAL.Repositories.Abstract;

namespace BLL.Managers.Concrete
{
    public class AnalyzeManager : IAnalyzeManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private readonly AnalyzeResultData _resultData;
        public AnalyzeManager(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._resultData = new AnalyzeResultData { ResultState = AnalyzeResult.Success };

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ResponseData, ResponseDataDto>();
            });

            _mapper = config.CreateMapper();
        }

        public async Task<AnalyzeResultData> StartAnalyzeAsync(string url, int? pageLimitation, CancellationToken cancellationToken)
        {
            //Saving request time
            var requestTime = DateTime.Now;

            //getting domain url
            var domainUrl = GetDomainFromUrl(url);

            if (domainUrl == null)
            {
                _resultData.ResultState = AnalyzeResult.NotCorrectUrl;
                return _resultData;
            }
            _resultData.DomainUrl = domainUrl;

            //searching for sitemap.xml
            var searchEngiene = new SiteMapSearchEngine(domainUrl);
            var siteMapContent = await searchEngiene.SearchSiteMapContentAsync(cancellationToken);

            if (siteMapContent == null)
            {
                if (searchEngiene.SiteMapSearchResult == AnalyzeResult.Success)
                {
                    _resultData.ResultState = AnalyzeResult.GenericError;
                    return _resultData;
                }

                _resultData.ResultState = searchEngiene.SiteMapSearchResult;
                return _resultData;
            }

            //urls parsing
            var parser = new SiteMapXmlParser(siteMapContent, pageLimitation);
            var urls = parser.GetUrls();

            if (urls == null || !urls.Any())
            {
                _resultData.ResultState = AnalyzeResult.CorruptedSitemap;
                return _resultData;
            }

            //benchmarking
            var benchmark = new ResponseBenchmark();
            await benchmark.StartAnalyzingAsync(urls, cancellationToken);

            cancellationToken.ThrowIfCancellationRequested();

            SaveResults(domainUrl, requestTime, benchmark.Result);

            var resultDataDto = _mapper.Map<List<ResponseData>, List<ResponseDataDto>>(benchmark.Result)
                .OrderByDescending(rd => rd.ElapsedTime);

            _resultData.SerializedData = JsonCustomConverter.ConvertCollection(resultDataDto);
            _resultData.ResponseData = resultDataDto;
            
            return _resultData;
        }

        private string GetDomainFromUrl(string url)
        {
            var uri = new Uri(url, UriKind.RelativeOrAbsolute);

            if (!Uri.IsWellFormedUriString(uri.Host, UriKind.RelativeOrAbsolute))
                return null;

            return $"{uri.Scheme}://{uri.Host}";
        }

        private void SaveResults(string url, DateTime requestTime, IEnumerable<ResponseData> responseData)
        {
            var isSiteExists = _unitOfWork.AnalyzedWebSite.Find(ws => ws.Url.Equals(url)).Any();
            AnalyzedWebSite site;

            if (!isSiteExists)
            {
                site = new AnalyzedWebSite() { Url = url };
                site = _unitOfWork.AnalyzedWebSite.Create(site);
            }
            else
                site = _unitOfWork.AnalyzedWebSite.Find(ws => ws.Url.Equals(url)).First();

            var analyzedData = new WebSiteAnalyzedData();
            analyzedData.ResponseData.AddRange(responseData);
            analyzedData.RequestTime = requestTime;
            analyzedData.AnalyzedWebSite = site;

            _unitOfWork.WebSiteAnalyzedData.Create(analyzedData);
            _unitOfWork.Save();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
