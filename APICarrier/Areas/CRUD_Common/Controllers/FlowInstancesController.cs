﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APICarrier.Areas.CRUD_Common.Controllers
{
    public class FlowInstancesController : Controller
    {
        // GET: CRUD_Common/FlowInstances
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 已完成的流程
        /// </summary>
        public ActionResult Disposed()
        {
            return View();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            return View();
        }

        /// <summary>
        /// 进度详情
        /// </summary>
        /// <returns></returns>
        public ActionResult Detail()
        {
            return View();
        }

        public ActionResult Verification()
        {
            return View();
        }

        //[HttpPost]
        //public string Verification(VerificationReq request)
        //{
        //    try
        //    {
        //        App.Verification(request);

        //    }
        //    catch (Exception ex)
        //    {
        //        Result.Code = 500;
        //        Result.Message = ex.Message;
        //    }
        //    return JsonHelper.Instance.Serialize(Result);
        //}

        //public string Get(string id)
        //{
        //    try
        //    {
        //        var flowinstance = App.Get(id);

        //        var result = new Response<FlowVerificationResp>
        //        {
        //            Result = flowinstance.MapTo<FlowVerificationResp>()
        //        };
        //        return JsonHelper.Instance.Serialize(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        Result.Code = 500;
        //        Result.Message = ex.Message;
        //    }
        //    return JsonHelper.Instance.Serialize(Result);
        //}

        ////添加或修改
        //[System.Web.Mvc.HttpPost]
        //[ValidateInput(false)]
        //public string Add(JObject obj)
        //{
        //    try
        //    {
        //        App.CreateInstance(obj);
        //    }
        //    catch (Exception ex)
        //    {
        //        Result.Code = 500;
        //        Result.Message = ex.Message;
        //    }
        //    return JsonHelper.Instance.Serialize(Result);
        //}

        ////添加或修改
        //[System.Web.Mvc.HttpPost]
        //public string Update(FlowInstance obj)
        //{
        //    try
        //    {
        //        App.Update(obj);

        //    }
        //    catch (Exception ex)
        //    {
        //        Result.Code = 500;
        //        Result.Message = ex.Message;
        //    }
        //    return JsonHelper.Instance.Serialize(Result);
        //}

        ///// <summary>
        ///// 加载列表
        ///// </summary>
        //public string Load([System.Web.Http.FromUri]QueryFlowInstanceListReq request)
        //{
        //    return JsonHelper.Instance.Serialize(App.Load(request));
        //}

        //[System.Web.Mvc.HttpPost]
        //public string Delete(string[] ids)
        //{
        //    try
        //    {
        //        App.Delete(ids);
        //    }
        //    catch (Exception e)
        //    {
        //        Result.Code = 500;
        //        Result.Message = e.Message;
        //    }

        //    return JsonHelper.Instance.Serialize(Result);
        //}
    }
}