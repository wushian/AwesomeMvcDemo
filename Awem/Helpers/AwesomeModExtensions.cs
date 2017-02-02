using System;
using System.Collections.Generic;
using Omu.AwesomeMvc;

namespace Omu.Awem.Helpers
{
    /// <summary>
    /// ASP.net MVC Awesome mod extensions
    /// </summary>
    public static class AwesomeModExtensions
    {
        /// <summary>
        /// Odropdown extension
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arl"></param>
        /// <param name="setCfg"></param>
        /// <returns></returns>
        public static AjaxRadioList<T> Odropdown<T>(this AjaxRadioList<T> arl, Action<OdropdownCfg> setCfg = null)
        {
            var res = arl.Mod("awem.odropdown");
            var odcfg = new OdropdownCfg();

            if (setCfg != null)
            {
                setCfg(odcfg);
                res.Tag(odcfg.ToTag());
            }

            return res;
        }

        /// <summary>
        /// menu dropdown extension
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arl"></param>
        /// <param name="setCfg"></param>
        /// <returns></returns>
        public static AjaxRadioList<T> MenuDropdown<T>(this AjaxRadioList<T> arl, Action<OdropdownCfg> setCfg = null)
        {
            var res = arl.Mod("awem.menuDropdown");
            var odcfg = new OdropdownCfg();

            if (setCfg != null)
            {
                setCfg(odcfg);
                res.Tag(odcfg.ToTag());
            }

            return res;
        }

        /// <summary>
        /// color dropdown extension
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arl"></param>
        /// <param name="setCfg"></param>
        /// <returns></returns>
        public static AjaxRadioList<T> ColorDropdown<T>(this AjaxRadioList<T> arl, Action<OdropdownCfg> setCfg = null)
        {
            arl.Mod("awem.colorDropdown");
            var odcfg = new OdropdownCfg();

            if (setCfg != null)
            {
                setCfg(odcfg);
                arl.Tag(odcfg.ToTag());
            }

            return arl;
        }

        /// <summary>
        /// combobox extension
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arl"></param>
        /// <param name="setCfg"></param>
        /// <returns></returns>
        public static AjaxRadioList<T> Combobox<T>(this AjaxRadioList<T> arl, Action<ComboboxCfg> setCfg = null)
        {
            arl.Mod("awem.combobox");
            var odcfg = new ComboboxCfg();

            if (setCfg != null)
            {
                setCfg(odcfg);
                arl.Tag(odcfg.ToTag());
            }

            return arl;
        }

        /// <summary>
        /// timepicker extension
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arl"></param>
        /// <param name="setCfg"></param>
        /// <returns></returns>
        public static AjaxRadioList<T> TimePicker<T>(this AjaxRadioList<T> arl, Action<TimePickerCfg> setCfg = null)
        {
            arl.Mod("awem.timepicker");
            arl.UnobsValid(false);

            var cfg = new TimePickerCfg();
            var tag = new TimePickerTag();

            if (setCfg != null)
            {
                setCfg(cfg);
                tag = cfg.ToTag();
            }

            var cformat = Autil.CurrentCulture().DateTimeFormat;
            var isAmPm = cformat.ShortTimePattern.Contains("h");

            if (isAmPm)
            {
                tag.AmPm = new[] { cformat.AMDesignator, cformat.PMDesignator };
            }

            arl.Tag(tag);

            arl.ValueRenderer(
                o =>
                {
                    if (o != null)
                    {
                        if (o is DateTime)
                        {
                            return ((DateTime)o).ToString(cformat.ShortTimePattern);
                        }

                        return o.ToString();
                    }

                    return string.Empty;
                });
            return arl;
        }

        /// <summary>
        /// imgdropdown extension
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arl"></param>
        /// <param name="setCfg"></param>
        /// <returns></returns>
        public static AjaxRadioList<T> ImgDropdown<T>(this AjaxRadioList<T> arl, Action<OdropdownCfg> setCfg = null)
        {
            arl.Mod("awem.imgDropdown");
            var odcfg = new OdropdownCfg();

            if (setCfg != null)
            {
                setCfg(odcfg);
                arl.Tag(odcfg.ToTag());
            }

            return arl;
        }

        /// <summary>
        /// buttongroup extension
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arl"></param>
        /// <param name="setCfg"></param>
        /// <returns></returns>
        public static AjaxRadioList<T> ButtonGroup<T>(this AjaxRadioList<T> arl, Action<OdropdownCfg> setCfg = null)
        {
            arl.Mod("awem.btnGroup");
            var odcfg = new OdropdownCfg();

            if (setCfg != null)
            {
                setCfg(odcfg);
                arl.Tag(odcfg.ToTag());
            }

            return arl;
        }

        /// <summary>
        /// buttongroup extension
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arl"></param>
        /// <param name="setCfg"></param>
        /// <returns></returns>
        public static AjaxCheckboxList<T> ButtonGroup<T>(this AjaxCheckboxList<T> arl, Action<OdropdownCfg> setCfg = null)
        {
            arl.Mod("awem.btnGroupChk");
            var odcfg = new OdropdownCfg();

            if (setCfg != null)
            {
                setCfg(odcfg);
                arl.Tag(odcfg.ToTag());
            }

            return arl;
        }

        /// <summary>
        /// multiselect extension
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arl"></param>
        /// <param name="setCfg"></param>
        /// <returns></returns>
        public static AjaxCheckboxList<T> Multiselect<T>(this AjaxCheckboxList<T> arl, Action<MultiselectCfg> setCfg = null)
        {
            arl.Mod("awem.multiselect");
            var odcfg = new MultiselectCfg();

            if (setCfg != null)
            {
                setCfg(odcfg);
            }

            arl.Tag(odcfg.ToTag());

            return arl;
        }

        /// <summary>
        /// dropdown popup
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <param name="setCfg"></param>
        /// <returns></returns>
        public static InitPopup<T> Mod<T>(this InitPopup<T> o, Action<PopupCfg> setCfg = null)
        {
            var cfg = new PopupCfg();
            if (setCfg != null)
            {
                setCfg(cfg);
            }

            o.Tag(cfg.ToTag());
            return o;
        }

        /// <summary>
        /// dropdown popup
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <param name="setCfg"></param>
        /// <returns></returns>
        public static InitPopupForm<T> Mod<T>(this InitPopupForm<T> o, Action<PopupCfg> setCfg = null)
        {
            var cfg = new PopupCfg();
            if (setCfg != null)
            {
                setCfg(cfg);
            }

            o.Tag(cfg.ToTag());
            return o;
        }


        /// <summary>
        /// dropdown popup
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <param name="setCfg"></param>
        /// <returns></returns>
        public static MultiLookup<T> Mod<T>(this MultiLookup<T> o, Action<PopupCfg> setCfg = null)
        {
            var cfg = new PopupCfg();
            if (setCfg != null)
            {
                setCfg(cfg);
            }

            o.Tag(cfg.ToTag());
            return o;
        }

        /// <summary>
        /// dropdown popup
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <param name="setCfg"></param>
        /// <returns></returns>
        public static Lookup<T> Mod<T>(this Lookup<T> o, Action<PopupCfg> setCfg = null)
        {
            var cfg = new PopupCfg();
            if (setCfg != null)
            {
                setCfg(cfg);
            }

            o.Tag(cfg.ToTag());
            return o;
        }

        /// <summary>
        /// grid mods extension
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="grid"></param>
        /// <param name="setCfg"></param>
        /// <returns></returns>
        public static Grid<T> Mod<T>(this Grid<T> grid, Action<GridModCfg> setCfg = null)
        {
            if (setCfg != null)
            {
                var cfg = new GridModCfg();
                setCfg(cfg);
                var info = cfg.GetInfo();
                var mods = new List<string>();
                if (info.PageSize) mods.Add("awem.gridPageSize");
                if (info.PageInfo) mods.Add("awem.gridPageInfo");
                if (info.ColumnsSelector) mods.Add("awem.gridColSel");
                if (info.InfiniteScroll) mods.Add("awem.gridInfScroll");

                if (info.AutoMiniPager) mods.Add("awem.gridAutoMiniPager");
                else if (info.MiniPager) mods.Add("awem.gridMiniPager");

                if (info.InlineEdit) mods.Add("awem.gridInlineEdit('" + info.CreateUrl + "','" 
                    + info.EditUrl + "', " 
                    + info.OneRow.ToString().ToLower() + "," 
                    + info.RelOnSav.ToString().ToLower() + ")");

                if (info.Loading) mods.Add("awem.gridLoading");

                var opt = new { G = info.GridIds };
                if (info.GridIds == null || info.GridIds.Length == 0) opt = null;

                if (info.MovableRows) mods.Add(string.Format("awem.gridMovRows({0})", Autil.Serialize(opt)));

                if (info.CustomMods != null)
                {
                    mods.AddRange(info.CustomMods);
                }

                grid.Mod(mods.ToArray());

                grid.BeforeRenderFuncs.Add(g =>
                            {
                                var autohide = false;
                                foreach (var column in g.GetColumns())
                                {
                                    var o = column.Tag as ColumnModTag;

                                    if (info.ColumnsAutohide)
                                    {
                                        autohide = true;

                                        if (o == null)
                                        {
                                            o = new ColumnModTag();
                                            column.Tag = o;
                                        }

                                        if (!o.Autohide.HasValue)
                                        {
                                            o.Autohide = 1;
                                        }

                                        if (o.Nohide)
                                        {
                                            o.Autohide = 0;
                                        }
                                    }
                                    else if (o != null && o.Autohide > 0)
                                    {
                                        autohide = true;
                                    }
                                }

                                if (autohide)
                                {
                                    g.GetMods().Add("awem.gridColAutohide");
                                }
                            });
            }

            return grid;
        }

        /// <summary>
        /// grid Column mod extension
        /// </summary>
        /// <param name="column"></param>
        /// <param name="setCfg"></param>
        /// <returns></returns>
        public static Column Mod(this Column column, Action<ColumnModCfg> setCfg = null)
        {
            if (setCfg != null)
            {
                var cfg = new ColumnModCfg(column);
                setCfg(cfg);
                var info = cfg.GetTag();
                column.Tag = info;
            }

            return column;
        }
    }
}