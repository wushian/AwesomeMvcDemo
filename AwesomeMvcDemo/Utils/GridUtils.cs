namespace AwesomeMvcDemo.Utils
{
    public static class GridUtils
    {
        public static string EditFormat(string popupName, string key = "Id")
        {
            return string.Format("<button type='button' class='awe-btn' onclick=\"awe.open('{0}', {{ params:{{ id: '.{1}' }} }})\"><span class='ico-edit'></span></button>",
                popupName, key);
        }

        public static string DeleteFormat(string popupName, string key = "Id", string deleteContent = "<span class='ico-del'></span>", string btnClass = null)
        {
            return string.Format("<button type='button' class='awe-btn {3}' onclick=\"awe.open('{0}', {{ params:{{ id: '.{1}' }} }})\">{2}</button>",
                popupName, key, deleteContent, btnClass);

            // if you need to set title, buttons dynamically you can do it like this:
            //awe.open('{0}', {{ params:{{ id: '.{1}' }}, udb:0, p: {{ t:'my title' }}, b:['btn1', btn1Func] }})
        }

        public static string InlineDeleteFormat(string popupName, string key = "Id")
        {
            return DeleteFormat(popupName, key, btnClass:"inlDel") + "<button type='button' class='awe-btn gcancelbtn' style='display:none;'>Cancel</button>";
        }

        public static string InlineEditFormat()
        {
            return "<button type='button' class='awe-btn geditbtn'>Edit</button><button type='button' class='awe-btn gsavebtn' style='display:none;'>Save</button>";
        }

        public static string EditFormatForGrid(string gridId, string key = "Id")
        {
            return EditFormat("edit" + gridId, key);
        }

        public static string DeleteFormatForGrid(string gridId, string key = "Id")
        {
            return DeleteFormat("delete" + gridId, key);
        }

        public static string EditGridNestFormat()
        {
            return "<button type='button' class='awe-btn editnst'><span class='ico-edit'></span></button>";
        }

        public static string DeleteGridNestFormat()
        {
            return "<button type='button' class='awe-btn delnst'><span class='ico-del'></span></button>";
        }

        public static string AddChildFormat()
        {
            return "<button type='button' class='awe-btn' onclick=\"awe.open('createNode', { params:{ parentId: '.Id' } })\">add child</button>";
        }
    }
}