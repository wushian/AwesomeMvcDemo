using System.Collections.Generic;

namespace Omu.Awem.Helpers
{
    /// <summary>
    /// Grid mods configuration
    /// </summary>
    public class GridModCfg
    {
        private readonly GridModInfo info = new GridModInfo();

        /// <summary>
        /// automatically go to next/prev page when scrolling to the end/beginning of the page
        /// </summary>
        /// <returns></returns>
        public GridModCfg InfiniteScroll()
        {
            info.InfiniteScroll = true;
            return this;
        }

        /// <summary>
        /// add page info ( page 1 of 75 ) in the right bottom corner of the grid
        /// </summary>
        /// <returns></returns>
        public GridModCfg PageInfo()
        {
            info.PageInfo = true;
            return this;
        }

        /// <summary>
        /// automatically switch the pager to a more compact version on smaller screens, or when resizing the browser to smaller size
        /// </summary>
        /// <returns></returns>
        public GridModCfg AutoMiniPager()
        {
            info.AutoMiniPager = true;
            return this;
        }

        /// <summary>
        /// use minipager
        /// </summary>
        /// <returns></returns>
        public GridModCfg MiniPager()
        {
            info.MiniPager = true;
            return this;
        }

        /// <summary>
        /// add page size selector 
        /// </summary>
        /// <returns></returns>
        public GridModCfg PageSize()
        {
            info.PageSize = true;
            return this;
        }

        /// <summary>
        /// add columns selector dropdown
        /// </summary>
        /// <returns></returns>
        public GridModCfg ColumnsSelector()
        {
            info.ColumnsSelector = true;
            return this;
        }

        /// <summary>
        /// Autohide columns
        /// </summary>
        /// <returns></returns>
        public GridModCfg ColumnsAutohide()
        {
            info.ColumnsAutohide = true;
            return this;
        }

        /// <summary>
        /// set Inline editing urls
        /// </summary>
        /// <param name="createUrl"></param>
        /// <param name="editUrl"></param>
        /// <param name="oneRow">inline edit one row at a time</param>
        /// <param name="reloadOnSave"></param>
        /// <returns></returns>
        public GridModCfg InlineEdit(string createUrl, string editUrl, bool oneRow = false, bool reloadOnSave = false)
        {
            info.InlineEdit = true;
            info.CreateUrl = createUrl;
            info.EditUrl = editUrl;
            info.OneRow = oneRow;
            info.RelOnSav = reloadOnSave;
            return this;
        }

        /// <summary>
        /// set Loading animation
        /// </summary>
        /// <returns></returns>
        public GridModCfg Loading()
        {
            info.Loading = true;
            return this;
        }

        /// <summary>
        /// moveable grid rows
        /// </summary>
        /// <returns></returns>
        public GridModCfg MovableRows(params string[] gridIds)
        {
            info.MovableRows = true;
            info.GridIds = gridIds;
            return this;
        }

        /// <summary>
        /// set mod by string func name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public GridModCfg Custom(string name)
        {
            if (info.CustomMods == null)
            {
                info.CustomMods = new List<string> { name };
            }
            else
            {
                info.CustomMods.Add(name);
            }

            return this;
        }

        internal GridModInfo GetInfo()
        {
            return info;
        }
    }
}