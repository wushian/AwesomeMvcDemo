using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using AwesomeMvcDemo.Models;
using AwesomeMvcDemo.ViewModels.Input;

using Omu.AwesomeMvc;

namespace AwesomeMvcDemo.Controllers.Demos.Grid
{
    public class TreeGridController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /*begin1*/
        public ActionResult SimpleTree(GridParams g)
        {
            var rootNodes = Db.TreeNodes.Where(o => o.Parent == null);

            var builder = new GridModelBuilder<TreeNode>(rootNodes.AsQueryable(), g)
            {
                Key = "Id", //required for the TreeGrid
                GetChildren = (node, nodeLevel) => Db.TreeNodes.Where(o => o.Parent == node).AsQueryable(),
                Map = o => new { o.Name, o.Id }
            };

            return Json(builder.Build());
        }
        /*end1*/

        /*begin2*/
        public ActionResult LazyTree(GridParams g)
        {
            var rootNodes = Db.TreeNodes.Where(o => o.Parent == null);

            var builder = new GridModelBuilder<TreeNode>(rootNodes.AsQueryable(), g)
            {
                Key = "Id", // required for the TreeGrid
                GetChildren = (node, nodeLevel) =>
                    {
                        var children = Db.TreeNodes.Where(o => o.Parent == node).AsQueryable();

                        // set this node as lazy when it's above level 1 (relative), it has children, and this is not a lazy request already
                        if (nodeLevel > 1 && children.Any() && g.Key == null) return Awe.LazyNode;

                        return children;
                    },
                GetItem = () => // used for lazy loading
                    {
                        var id = Convert.ToInt32(g.Key);
                        return Db.TreeNodes.FirstOrDefault(o => o.Id == id);
                    },
                Map = o => new { o.Name, o.Id }
            };

            return Json(builder.Build());
        }
        /*end2*/

        /*begin3*/
        //clear nodesAdded on aweload
        public ActionResult CrudTree(GridParams g, string name, int[] nodesAdded)
        {
            name = name ?? "";
            nodesAdded = nodesAdded ?? new int[] { };

            var result = Db.TreeNodes.Where(o => o.Name.ToLower().Contains(name.ToLower()) || nodesAdded.Contains(o.Id)).ToList();
            var searchResult = result.ToList();

            foreach (var treeNode in searchResult)
            {
                AddParentsTo(result, treeNode);
            }

            var roots = result.Where(o => o.Parent == null);

            var model = new GridModelBuilder<TreeNode>(roots.AsQueryable(), g)
            {
                Key = "Id",
                GetChildren = (node, nodeLevel) =>
                {
                    // non lazy version
                    //var children = result.Where(o => o.Parent == node).ToArray();
                    //return children.AsQueryable();

                    var children = result.Where(o => o.Parent == node).AsQueryable();

                    // set this node as lazy when it's above level 1 (relative), it has children, and this is not a lazy request already
                    if (nodeLevel > 1 && children.Any() && g.Key == null) return Awe.LazyNode;

                    return children;
                },
                GetItem = () => // used for grid api updateItem
                {
                    var id = Convert.ToInt32(g.Key);
                    return Db.TreeNodes.FirstOrDefault(o => o.Id == id);
                },
                Map = MapNode
            };

            return Json(model.Build());
        }

        private object MapNode(TreeNode node)
        {
            return new { node.Name, node.Id };
        }

        private void AddParentsTo(ICollection<TreeNode> result, TreeNode node)
        {
            if (node.Parent != null)
            {
                if (!result.Contains(node.Parent))
                {
                    result.Add(node.Parent);
                    AddParentsTo(result, node.Parent);
                }
            }
        }

        public ActionResult Create(int? parentId)
        {
            return PartialView(new TreeNodeInput { ParentId = parentId ?? 0 });
        }

        [HttpPost]
        public ActionResult Create(TreeNodeInput input)
        {
            if (!ModelState.IsValid) return View(input);

            var parent = input.ParentId != 0 ? Db.Get<TreeNode>(input.ParentId) : null;
            var node = new TreeNode { Name = input.Name, Parent = parent };

            Db.Insert(node);

            var result = new
                {
                    Node = MapNode(node),
                    ParentId = node.Parent != null ? node.Parent.Id : 0 // we'll refresh the parent when adding child 
                };

            return Json(result);
        }

        public ActionResult Edit(int id)
        {
            var node = Db.Get<TreeNode>(id);
            return PartialView("Create", new TreeNodeInput { Id = node.Id, Name = node.Name });
        }

        [HttpPost]
        public ActionResult Edit(TreeNodeInput input)
        {
            if (!ModelState.IsValid) return View("Create", input);

            var node = Db.Get<TreeNode>(input.Id);
            node.Name = input.Name;
            Db.Update(node);
            return Json(new { node.Id });
        }

        public ActionResult Delete(int id, string gridId)
        {
            var node = Db.Get<TreeNode>(id);

            return PartialView(new DeleteConfirmInput
            {
                Id = id,
                Message = string.Format("Are you sure you want to delete node {0} ", node.Name)
            });
        }

        [HttpPost]
        public ActionResult Delete(DeleteConfirmInput input)
        {
            var node = Db.Get<TreeNode>(input.Id);
            DeleteNode(node);

            var result = new
            {
                node.Id,
                ParentId = node.Parent != null ? node.Parent.Id : 0 // we'll refresh the parent to remove collapse button when zero children
            };

            return Json(result);
        }

        private void DeleteNode(TreeNode node)
        {
            var children = Db.TreeNodes.Where(o => o.Parent == node).ToList();
            Db.Delete<TreeNode>(node.Id);

            foreach (var child in children)
            {
                DeleteNode(child);
            }
        }
        /*end3*/
    }
}