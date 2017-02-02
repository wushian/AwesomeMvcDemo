using System.Collections.Generic;
using Omu.AwesomeMvc;

namespace Omu.Awem.Helpers
{
    /// <summary>
    /// options for grid inline func
    /// </summary>
    public class InlineFuncOptions
    {
        private readonly ColumnModTag tag;
        internal InlineFuncOptions(ColumnModTag tag)
        {
            AddParams = new List<string>();
            this.tag = tag;
        }
        

        internal IList<string> AddParams { get; set; }

        /// <summary>
        /// viewmodel property to match in the edit/create action, when not set valProp will be used, used for validation only here, you set the input name in the string format
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public InlineFuncOptions ModelProp(string o)
        {
            tag.ModelProp = o;
            return this;
        }

        /// <summary>
        /// grid model property to get value from, when not set will use Column.Bind
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public InlineFuncOptions ValProp(string o)
        {
            tag.ValProp = o;
            return this;
        }

        /// <summary>
        /// add string parameter
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public InlineFuncOptions Param(string str)
        {
            AddParams.Add(str);
            return this;
        }

        /// <summary>
        /// add awe helper parameter
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helper"></param>
        /// <returns></returns>
        public InlineFuncOptions Param<T>(IAwesomeHelper<T> helper)
        {
            helper.Svalue("#Value");
            helper.Prefix("#Prefix");

            if (tag.ModelProp == null)
            {
                tag.ModelProp = helper.Awe.Prop;
            }

            AddParams.Add(helper.ToString());
            return this;
        }
    }
}