﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeiDream.Services.Systems.Dtos;
using BeiDream.Services.Systems.IServices;
using Util.Webs.EasyUi.Results;
using Util.Webs.EasyUi.Trees;

namespace BeiDream.EasyUi.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IMenuRepository menuRepository)
        {
            MenuRepository = menuRepository;
        }

        protected IMenuRepository MenuRepository { get; private set; }
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ViewBag.UserName = "管理员";
            return View();

        }

        public ActionResult LeftMenu()
        {
            List<MenuViewModel> treeNodes = MenuRepository.GetAllTopLevel().Select(menu => menu.ToDto()).ToList();
            return PartialView("Left", treeNodes);
        }

        public string GetMenuTree(string parentId)
        {
            //通过查找物理路径方式得到子孙节点
            //List<ITreeNode> navigationMenuTree = _menuService.GetNavigationMenuTreeByPath(parentId);
            //通过递归查询子节点方式,与上面的传递到前台数据不同，上面以ParentID定位子孙节点，此以children定位子节点
            List<ITreeNode> navigationMenuTree = MenuRepository.GetNavigationMenuTreeByChildren(parentId);
            return new TreeResult(navigationMenuTree).ToString();
        }

    }
}
