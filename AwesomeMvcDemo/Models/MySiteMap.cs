using System.Collections.Generic;

namespace AwesomeMvcDemo.Models
{
    public static class MySiteMap
    {
        public static readonly IList<SiteMapItem> Items = new List<SiteMapItem>();

        static MySiteMap ()
        {
            var grid = new SiteMapItem { Name = "Grid" };
            
            Items.Add(grid);
            Items.Add(new SiteMapItem{Name = "Quick View", Controller = "GridDemo", Action = "Index", Parent = grid});
            Items.Add(new SiteMapItem{Name = "Grouping/Aggregates", Controller = "GridDemo", Action = "Grouping", Parent = grid});
            Items.Add(new SiteMapItem{Name = "Sorting", Controller = "GridDemo", Action = "Sorting", Parent = grid});
            Items.Add(new SiteMapItem{Name = "Client Side API", Controller = "GridDemo", Action = "ClientSideApi", Parent = grid});
            Items.Add(new SiteMapItem{Name = "Custom Querying", Controller = "GridDemo", Action = "CustomQuerying", Parent = grid});
            Items.Add(new SiteMapItem{Name = "Custom Format", Controller = "GridDemo", Action = "CustomFormat", Parent = grid});
            Items.Add(new SiteMapItem{Name = "RTL Support", Controller = "GridDemo", Action = "RTLSupport", Parent = grid});
            Items.Add(new SiteMapItem{Name = "Nesting", Controller = "GridNestingDemo", Action = "Index", Keywords = "master detail", Parent = grid});
            Items.Add(new SiteMapItem{Name = "Tree Grid", Controller = "TreeGrid", Action = "Index", Parent = grid});
            Items.Add(new SiteMapItem { Name = "Header Groups", Controller = "GridMultiRowHeaderGroups", Action = "Index", Parent = grid });

            var demos = new SiteMapItem { Name = "Demos", Parent = grid };
            Items.Add(demos);
            Items.Add(new SiteMapItem { Name = "Grid Client Data", Controller = "GridClientDataDemo", Action = "Index", Parent = demos });
            Items.Add(new SiteMapItem { Name = "Scheduler", Controller = "SchedulerDemo", Action = "Index", Parent = demos });
            Items.Add(new SiteMapItem { Name = "Grid Crud (Popup)", Controller = "GridCrudDemo", Action = "Index", Keywords = "editing", Parent = demos});
            Items.Add(new SiteMapItem { Name = "Grid Inline Editing", Controller = "GridInlineEditDemo", Action = "Index", Parent = demos });
            Items.Add(new SiteMapItem { Name = "Grid In Nest Editing", Controller = "GridNestingDemo", Action = "Index", Anchor = "#In-nest-editing-grid", Keywords = "inline", Parent = demos});
            Items.Add(new SiteMapItem { Name = "TreeGrid Crud", Controller = "TreeGrid", Action = "Index", Anchor = "#Tree-Grid-with-CRUD-operations", Parent = demos });
            Items.Add(new SiteMapItem { Name = "Grid with checkboxes", Controller = "GridCheckboxesDemo", Action = "Index", Parent = demos });
            //Items.Add(new SiteMapItem { Name = "Hide columns api", Controller = "GridShowHideColumnsApiDemo", Action = "Index", Parent = demos });
            Items.Add(new SiteMapItem { Name = "Spreadsheet Grid", Controller = "GridSpreadsheetDemo", Action = "Index", Keywords = "inline", Parent = demos});
            Items.Add(new SiteMapItem { Name = "Master Detail crud", Controller = "MasterDetailCrudDemo", Action = "Index", Parent = demos });
            Items.Add(new SiteMapItem { Name = "Keyboard Navigation", Controller = "KeyboardNavigationDemo", Action = "Index", Parent = demos });
            Items.Add(new SiteMapItem { Name = "Infinite Scrolling", Controller = "GridInfiniteScrollingDemo", Action = "Index", Parent = demos });
            Items.Add(new SiteMapItem { Name = "Grid Mailbox", Controller = "MailboxDemo", Action = "Index", Parent = demos });

            var lookup = new SiteMapItem { Name = "Lookup", Collapsed = true};
            
            Items.Add(lookup);
            Items.Add(new SiteMapItem{Name = "Quick View", Controller = "LookupDemo", Action = "Index", Parent = lookup});
            Items.Add(new SiteMapItem{Name = "Custom Search", Controller = "LookupDemo", Action = "CustomSearch", Parent = lookup});
            Items.Add(new SiteMapItem{Name = "Misc", Controller = "LookupDemo", Action = "Misc", Parent = lookup});


            var multilookup = new SiteMapItem { Name = "MultiLookup", Collapsed = true};

            Items.Add(multilookup);
            Items.Add(new SiteMapItem{Name = "Quick View", Controller = "MultiLookupDemo", Action = "Index", Parent = multilookup});
            Items.Add(new SiteMapItem{Name = "Custom Search", Controller = "MultiLookupDemo", Action = "CustomSearch", Parent = multilookup});
            Items.Add(new SiteMapItem{Name = "Misc", Controller = "MultiLookupDemo", Action = "Misc", Parent = multilookup});

            var awesome = new SiteMapItem { Name = "Awesome" };

            Items.Add(awesome);
            Items.Add(new SiteMapItem{Name = "Drag And Drop", Controller = "DragAndDropDemo", Action = "Index", Parent = awesome, Keywords = "grid reorder rows move"});
            Items.Add(new SiteMapItem{Name = "AjaxDropdown", Controller = "AjaxDropdownDemo", Action = "Index", Parent = awesome});
            Items.Add(new SiteMapItem{Name = "AjaxRadioList", Controller = "AjaxRadioListDemo", Action = "Index", Keywords = "odropdown buttongroup select combobox", Parent = awesome});
            Items.Add(new SiteMapItem { Name = "AjaxCheckBoxList", Controller = "AjaxCheckBoxListDemo", Action = "Index", Keywords = "multiselect dropdown buttongroup", Parent = awesome });
            Items.Add(new SiteMapItem{Name = "Autocomplete", Controller = "AutocompleteDemo", Action = "Index", Parent = awesome});
            Items.Add(new SiteMapItem{Name = "Popup", Controller = "PopupDemo", Action = "Index", Parent = awesome});
            Items.Add(new SiteMapItem{Name = "PopupForm", Controller = "PopupFormDemo", Action = "Index", Parent = awesome});
            Items.Add(new SiteMapItem { Name = "Call", Controller = "CallDemo", Action = "Index", Parent = awesome });
            Items.Add(new SiteMapItem{Name = "DatePicker", Controller = "DatePickerDemo", Action = "Index", Parent = awesome});
            Items.Add(new SiteMapItem{Name = "TextBox", Controller = "TextBoxDemo", Action = "Index", Parent = awesome});
            Items.Add(new SiteMapItem{Name = "Form", Controller = "FormDemo", Action = "Index", Parent = awesome});
            Items.Add(new SiteMapItem{Name = "Pager", Controller = "PagerDemo", Action = "Index", Parent = awesome});

            var ajaxlist = new SiteMapItem { Name = "AjaxList", Collapsed = true};

            Items.Add(ajaxlist);
            Items.Add(new SiteMapItem{Name = "Quick View", Controller = "AjaxListDemo", Action = "Index", Parent = ajaxlist});
            Items.Add(new SiteMapItem{Name = "Custom Item Template", Controller = "AjaxListDemo", Action = "CustomItemTemplate", Parent = ajaxlist});
            Items.Add(new SiteMapItem{Name = "Table Layout", Controller = "AjaxListDemo", Action = "TableLayout", Parent = ajaxlist});
            Items.Add(new SiteMapItem{Name = "Client Side API", Controller = "AjaxListDemo", Action = "ClientSideApi", Parent = ajaxlist});
            Items.Add(new SiteMapItem { Name = "AjaxList Crud Demo", Controller = "Dinners", Action = "Index", Parent = ajaxlist });


            var generic = new SiteMapItem { Name = "Generic" };
            
            Items.Add(generic);
            Items.Add(new SiteMapItem{Name = "Unobtrusive validation", Controller = "Unobtrusive", Action = "Index", Parent = generic});
            Items.Add(new SiteMapItem{Name = "Rtl Demo", Controller = "RtlDemo", Action = "Index", Parent = generic});
            Items.Add(new SiteMapItem { Name = "List Binding", Controller = "ListBinding", Action = "Index", Parent = generic });
            Items.Add(new SiteMapItem { Name = "Attributes Demo", Controller = "AttributesDemo", Action = "Index", Parent = generic });
            Items.Add(new SiteMapItem { Name = "Error Handling", Controller = "ErrorHandlingDemo", Action = "Index", Parent = generic });
            Items.Add(new SiteMapItem { Name = "Client load demo", Controller = "ClientDataLoadingDemo", Action = "Index", Parent = generic });
            Items.Add(new SiteMapItem { Name = "Multilevel cascading", Controller = "MultipleLevelCascadingDemo", Action = "Index", Parent = generic });

            var misc = new SiteMapItem { Name = "Misc" };
            Items.Add(misc);
            Items.Add(new SiteMapItem { Name = "Wizard Demo", Controller = "WizardDemo", Action = "Index", Parent = misc });
            Items.Add(new SiteMapItem { Name = "CRUD inside Lookup", Controller = "CrudInLookup", Action = "Index", Parent = misc });
            
            Items.Add(new SiteMapItem { Name = "Grid export to excel", Controller = "GridExportToExcelDemo", Action = "Index", Parent = misc });
            
            Items.Add(new SiteMapItem{Name = "Cascading Grids", Controller = "CascadingGridDemo", Action = "Index", Parent = misc});
            
            //Items.Add(new SiteMapItem{Name = "Grid maintain selection", Controller = "GridMaintainSelectionDemo", Action = "Index", Parent = misc});
            Items.Add(new SiteMapItem { Name = "Grid selection", Controller = "GridDemo", Action = "Selection", Parent = misc });
            Items.Add(new SiteMapItem{Name = "Grid choose columns", Controller = "GridChooseColumnsDemo", Action = "Index", Parent = misc});
            //Items.Add(new SiteMapItem{Title = "Grid show hide columns", Controller = "GridShowHideColumnsDemo", Action = "Index", Parent = misc});
            Items.Add(new SiteMapItem { Name = "Angular.js Demo", Controller = "AngularDemo", Action = "Index", Parent = misc });
            Items.Add(new SiteMapItem { Name = "Knockout.js Demo", Controller = "KnockoutDemo", Action = "Index", Parent = misc });

            var more = new SiteMapItem { Name = "More", Collapsed = true };
            Items.Add(more);
            Items.Add(new SiteMapItem { Name = "Grid Custom Pager", Controller = "CustomPagerGridDemo", Action = "Index", Collapsed = true, Parent = more });
            Items.Add(new SiteMapItem { Name = "Grid Custom Loading", Controller = "GridNoRecordsFoundCustomLoadingDemo", Action = "Index", Collapsed = true, Parent = more });
            Items.Add(new SiteMapItem { Name = "Grid Array DataSource", Controller = "GridArrayDataSource", Action = "Index", Collapsed = true, Parent = more });
        }
    }
}