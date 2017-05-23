using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BLL.DTO;
using BLL.Managers.Abstract;
using PagedList;
using WebAnalyzer.Models.ViewModels;

namespace WebAnalyzer.Controllers
{
    public class HistoryController : Controller
    {
        private readonly IHistoryManager _historyManager;
        private readonly IMapper _mapper;

        public HistoryController(IHistoryManager historyManager)
        {
            this._historyManager = historyManager;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<WebSiteHistoryDto, AnalyzedWebSiteViewModel>();
                cfg.CreateMap<ResponseDataDto, ResponseDataViewModel>();
            });

            _mapper = config.CreateMapper();
        }

        // GET: History
        public ActionResult Index(int? page)
        {
            int pageSize = 20;
            int pageNumber = page ?? 1;

            var webSitesHistoryDtos = _historyManager.GetFullHistory().ToList();

            return View(_mapper.Map<List<WebSiteHistoryDto>, List<AnalyzedWebSiteViewModel>>(webSitesHistoryDtos).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ViewHistory(int? requestId)
        {
            if (requestId == null)
                return HttpNotFound();

            var requestData = _historyManager.GetRequestHistory(requestId.Value);

            if (requestData == null)
                return HttpNotFound();

            var model = new AnalysisDataViewModel()
            {
                Url = requestData.Url,
                SerializedData = requestData.SerializedData,
                ResponseData = _mapper.Map<List<ResponseDataDto>, List<ResponseDataViewModel>>(requestData.ResponseData)
            };

            model.ResponseData.ForEach(rd => rd.Place = model.ResponseData.IndexOf(rd) + 1);

            return View("~/Views/Home/Index.cshtml", model);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            _historyManager.Dispose();
        }
    }
}