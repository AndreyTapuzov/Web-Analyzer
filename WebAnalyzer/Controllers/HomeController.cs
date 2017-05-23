using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BLL.DTO;
using BLL.Enums;
using BLL.HelpModels;
using BLL.Managers.Abstract;
using WebAnalyzer.Models.ViewModels;

namespace WebAnalyzer.Controllers
{
    //[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class HomeController : Controller
    {
        private readonly IAnalyzeManager _analyzeManager;
        private readonly IMapper _mapper;

        public HomeController(IAnalyzeManager analyzeManager)
        {
            this._analyzeManager = analyzeManager;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ResponseDataDto, ResponseDataViewModel>();
            });

            _mapper = config.CreateMapper();
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Analyze(AnalysisDataViewModel analysisDataViewModel,
            CancellationToken cancellationToken)
        {
            //Allow request cancellation
            CancellationToken disconnectedToken = Response.ClientDisconnectedToken;
            var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, disconnectedToken);


            AnalyzeResultData analyzeResultData = await _analyzeManager.StartAnalyzeAsync(analysisDataViewModel.Url,
                analysisDataViewModel.PagesLimitation, tokenSource.Token);

            if (analyzeResultData.ResultState != AnalyzeResult.Success)
            {
                string message = GetMessage(analyzeResultData.ResultState, analyzeResultData.DomainUrl);
                return GetViewForResult("_ResultAnalysis", analysisDataViewModel, message);
            }

            analysisDataViewModel.ResponseData =
                _mapper.Map<List<ResponseDataDto>, List<ResponseDataViewModel>>(analyzeResultData.ResponseData.ToList());
            analysisDataViewModel.ResponseData.ForEach(rd => rd.Place = analysisDataViewModel.ResponseData.IndexOf(rd) + 1);

            analysisDataViewModel.SerializedData = analyzeResultData.SerializedData;
            analysisDataViewModel.Url = analyzeResultData.DomainUrl;

            return PartialView("_ResultAnalysis", analysisDataViewModel);
        }


        private ActionResult GetViewForResult(string viewName, AnalysisDataViewModel model, string resultMsg = null)
        {
            ViewBag.ResultMessage = resultMsg;
            model.ResponseData = null;
            return PartialView(viewName, model);
        }

        private string GetMessage(AnalyzeResult analyzeResult, string url)
        {
            switch (analyzeResult)
            {
                case AnalyzeResult.CorruptedSitemap: return $"Sitemap for {url} is corrupted";
                case AnalyzeResult.NotCorrectUrl: return "Url is not valid";
                case AnalyzeResult.NoSitemap: return $"Sitemap for {url} was not found";
                case AnalyzeResult.GenericError: return "Sorry, an error occurred";
                default: return "Sorry, an error occurred";
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            _analyzeManager.Dispose();
        }
    }
}