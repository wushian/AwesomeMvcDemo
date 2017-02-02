using System;
using System.Linq;
using Omu.AwesomeMvc;

namespace Omu.Awem.Helpers
{
    /// <summary>
    /// Column mods config
    /// </summary>
    public class ColumnModCfg
    {
        private readonly ColumnModTag tag = new ColumnModTag();

        private readonly Column column;

        internal ColumnModTag GetTag()
        {
            return tag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="column"></param>
        public ColumnModCfg(Column column)
        {
            this.column = column;
        }

        /// <returns></returns>
        /// <summary>
        /// hide column on small screens, or when resizing the browser (should be used with columns selector mod)
        /// </summary>
        /// <param name="order">hide order ( greater number - last to be hidden, 0 - no autohide, lesser number - first to be hidden ) </param>
        /// <returns></returns>
        public ColumnModCfg Autohide(int order = 1)
        {
            tag.Autohide = order;
            return this;
        }

        /// <summary>
        /// disable autohide for this column, use when setting autohide for all columns using grid ColumnsAutohide grid mod
        /// </summary>
        /// <returns></returns>
        public ColumnModCfg NoAutohide()
        {
            tag.Autohide = 0;
            return this;
        }

        /// <summary>
        /// use to disable hiding of the column from the columns selector, it will also disable autohiding
        /// </summary>
        /// <returns></returns>
        public ColumnModCfg Nohide()
        {
            tag.Nohide = true;
            return this;
        }

        /// <summary>
        /// set inline format for the column; value from valProp is used to replace #Value in the format
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helper">awesome editor helper</param>
        /// <param name="valProp">grid model property to get value from, when not set will use Column.Bind; </param>
        /// <returns></returns>
        public ColumnModCfg Inline<T>(IAwesomeHelper<T> helper, string valProp = null)
        {
            helper.Svalue("#Value");
            helper.Prefix("#Prefix");
            tag.Format = helper.ToString();
            tag.ValProp = valProp ?? column.Bind;
            tag.ModelProp = helper.Awe.Prop;

            return this;
        }

        /// <summary>
        /// set inline format for the column; value from valProp is used to replace #Value in the format
        /// </summary>
        /// <param name="format">editor string format, #Value and #Prefix will be replaced, prefix is used for unique ids </param>
        /// <param name="valProp">grid model property to get value from, when not set will use Column.Bind</param>
        /// <param name="modelProp">viewmodel property to match in the edit/create action, when not set valProp will be used, used for validation only here, you set the input name in the string format</param>
        /// <returns></returns>
        public ColumnModCfg Inline(string format, string valProp = null, string modelProp = null)
        {
            tag.Format = format;
            tag.ValProp = valProp ?? column.Bind;
            tag.ModelProp = modelProp ?? tag.ValProp;

            return this;
        }


        /// <summary>
        /// set readonly inline format for the column
        /// </summary>
        /// <param name="displayFormat">display format, .Prop will be replaced with values from the grid row model </param>
        /// <param name="valProp">grid model property to get value from, when not set will use Column.Bind</param>
        /// <param name="modelProp">viewmodel property to match in the edit/create action, when not set valProp will be used, used for validation only here, you set the input name in the string format</param>
        /// <returns></returns>
        public ColumnModCfg InlineReadonly(string displayFormat = null, string valProp = null, string modelProp = null)
        {
            var vprop = valProp ?? column.Bind;
            var mprop = modelProp ?? vprop;
            displayFormat = displayFormat ?? column.ClientFormat ?? "." + vprop;
            var format = displayFormat + "<input type='hidden' name='" + mprop + "' value='#Value'/>";
            tag.Format = format;
            tag.ValProp = vprop;
            tag.ModelProp = mprop;
            return this;
        }

        /// <summary>
        /// Inline hidden input for Id column, will use "Id" as name if bind or valprop is not specified
        /// </summary>
        /// <param name="valProp">grid model property to get value from, when not set will use Column.Bind</param>
        /// <param name="modelProp">viewmodel property to match in the edit/create action, when not set valProp will be used</param>
        /// <returns></returns>
        public ColumnModCfg InlineId(string valProp = null, string modelProp = null)
        {
            var vprop = valProp ?? column.Bind ?? "Id";
            var mprop = modelProp ?? vprop;
            tag.Format = "<input type='hidden' name='" + mprop + "' value='#Value'>" + "#Value";
            tag.ValProp = vprop;
            tag.ModelProp = mprop;

            return this;
        }

        /// <summary>
        /// Inline checkbox, by default will use column.Bind + "Checked" for valueProp and column.Bind for modelProp
        /// </summary>
        /// <param name="modelProp">viewmodel property to match in the edit/create action, when not set valProp will be used</param>
        /// <param name="valProp">grid model property to get value from, when not set will use Column.Bind</param>
        /// <param name="cssClass">css class for the checkbox input</param>
        /// <returns></returns>
        public ColumnModCfg InlineBool(string valProp = null, string modelProp = null, string cssClass = "")
        {
            var vprop = valProp ?? column.Bind;
            var mprop = modelProp ?? vprop;
            tag.Format = "<input type='checkbox' name='" + mprop + "' value='true' #Value class='" + cssClass + "' />";
            tag.ValProp = vprop;
            tag.ModelProp = mprop;
            //tag.Type = "bool";

            return this;
        }

        /// <summary>
        /// define a js function that will set the inline format of the column; the func will receive the grid row model as the first parameter + additional params
        /// </summary>
        /// <param name="func">js func</param>
        /// <param name="setOptions">set additional options</param>
        /// <returns></returns>
        public ColumnModCfg InlineFunc(string func, Action<InlineFuncOptions> setOptions = null)
        {
            tag.FormatFunc = func;
            var opt = new InlineFuncOptions(tag);
            tag.ValProp = column.Bind;

            if (setOptions != null)
            {
                setOptions(opt);
                tag.Fpar = opt.AddParams.ToArray();
            }

            return this;
        }
    }
}