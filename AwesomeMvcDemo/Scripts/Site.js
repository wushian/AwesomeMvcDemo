//on document ready
function documentReady(root) {
    layPage();

    handleAnchors();
    
    $(document).on('aweload', 'table.awe-ajaxlist', wrapLists);

    $(window).on('resize', layPage);

    $('#btnLogo').click(function (e) {
        if ($(window).width() < 1050) {
            e.preventDefault();
            $('#btnMenuToggle').click();
        }
    });
    
    $('#btnMenuToggle').click(function () {
        MenuToggle($('#demoMenu').is(':visible'), 1);
    });

    $('pre').addClass('prettyprint');

    //show code 
    $('.code').hide().before('<br/>').before($('<span class="shcode">show code</span>').click(function () {
        var btn = $(this);
        btn.toggleClass("hideCode showCode");

        if (btn.hasClass("hideCode")) {
            btn.html("hide code");
            $(this).next().fadeIn();
        } else {
            btn.html("show code");
            $(this).next().fadeOut();
        }
    }));

    //tabs 
    $('.tabs').each(function (i, item) {
        var $tabstrip = $(item);
        var $tabs = $tabstrip.children();
        $tabs.wrapAll('<div class="tabscontent"/>');
        var $bar = $('<div class="tabsbar"></div>');
        $tabstrip.prepend($bar);
        $tabs.each(function (ti, tab) {
            var $tab = $(tab);
            var btnclass = "tab-btn";

            if (ti)
                $tab.hide();
            else
                btnclass += " active";

            var $btn = $('<button type="button" class="' + btnclass + '">' + $tab.data('caption') + '</button>');
            $btn.click(function () {
                $bar.children().removeClass('active');
                $btn.addClass('active');
                $tabs.hide();
                $tab.show();
            });
            $bar.append($btn);
        });
    });

    //change popup
    $('#chPopupMod').change(function () {
        var newmod = $('#chPopupMod').val();
        var theme = $('#chTheme').val();
        
        popupMod = newmod;

        $('.awe-popup').each(function () {
            if ($(this).data('api'))
                $(this).data('api').close();
        });

        $('.awe-multilookup, .awe-lookup').each(function () { $(this).data('api').initPopup(); });
        
        Cookies.set('awedemset50', theme + '|' + newmod, { expires: 30 });
    });

    $('#chTheme').change(function () {
        var newmod = $('#chPopupMod').val();
        var theme = $('#chTheme').val();
        
        $('#aweStyle').attr('href', root + "Content/themes/" + theme + "/AwesomeMvc.css?v=73");
        $('#modsStyle').attr('href', root + "Content/themes/" + theme + "/awem.css?v=73");
        $('body').attr('class', theme);

        Cookies.set('awedemset50', theme + '|' + newmod, { expires: 30 });
    });
}

function handleAnchors() {
    var anchor = location.hash.replace('#', '').replace(/\(|\)|:|\.|\;|\\|\/|\?|,/g, '');
    $('h3,h2').each(function (_, e) {
        var $e = $(e);
        var name = $e.data('name');
        if (!name) name = $.trim($e.html()).replace(/ /g, '-').replace(/\(|\)|:|\.|\;|\\|\/|\?|,/g, '');
        name = name.replace('&lt', '').replace('&gt', '');
        $e.append("<a class='anchor' name='" + name + "' href='#" + name + "' tabIndex='-1'>&nbsp;<i class='glyphicon glyphicon-link'></i></a>");
        if (name == anchor) {
            $e.addClass("awe-changing").removeClass('awe-changing', 3000);
        }
    });
}

var lastw = 0;

function layPage() {
    var ww = $(window).width();

    $("#main").css("min-height", ($(window).height() - 120) + "px");

    if (lastw != ww)
        MenuToggle(ww < 1050);
    lastw = ww;
}

var menuNodes;
function getMenuGridFunc(menuNodes) {
    function addParentsTo(res, node, all) {
        if (node.ParentId) {
            var isFound;
            $.each(res, function (_, o) {
                if (o.Id == node.ParentId) {
                    isFound = true;
                    return false;
                }
            });

            if (!isFound) {
                var parent = $.grep(all, function (o) { return o.Id == node.ParentId; })[0];
                res.push(parent);
                addParentsTo(res, parent, all);
            }
        }
    }
    
    function equals(a, b) {
        return new RegExp("^" + a + "$", "i").test(b);
    }

    function buildMenuGridModel(g) {
        var selectedItems = $.grep(menuNodes, function (o) {
            o.Selected = '';
            return equals(g.action, o.Action) &&
                equals(g.controller, o.Controller);
        });

        var selItems = selectedItems.slice(0);

        if (selectedItems.length) {
            selectedItems[0].Selected = "selected";
            $.each(selItems, function (_, item) {
                addParentsTo(selectedItems, item, menuNodes);
            });
        }

        $.each(selectedItems, function (_, o) {
            o.IsNodeSelected = true;
        });

        var words = g.search.split(" ");

        var regs = $.map(words, function (w) {
            return new RegExp(w, "i");
        });

        var regsl = regs.length;

        var result = $.grep(menuNodes, function (o) {
            var matches = 0;
            var s = o.Keywords + ' ' + o.Name;
            $.each(regs, function (_, reg) {
                reg.test(s) && matches++;
            });

            return regsl == matches;
        });

        var searchResult = result.slice(0);

        $.each(searchResult, function (_, o) {
            addParentsTo(result, o, menuNodes);
        });

        var rootNodes = $.grep(result, function (o) { return !o.ParentId; });

        var getChildren = function (node, nodeLevel) {
            return $.grep(result, function (o) { return o.ParentId == node.Id; });
        };

        function makeHeader(info) {
            var isNodeSelected = info.NodeItem.IsNodeSelected;
            return {
                Item: info.NodeItem,
                Collapsed: !g.search && info.NodeItem.Collapsed && !isNodeSelected,
                IgnorePersistence: g.search || isNodeSelected
            };
        }

        return utils.gridModelBuilder(this, g, rootNodes, { key: "Id", getChildren: getChildren, defaultKeySort: 1, forceSort: 1, makeHeader: makeHeader });
    }

    return function(gp) {
        var g = utils.getGridParams(gp);
        var url = this.tag.ItemsUrl;
        var storageKey = awe.ppk + "_menuNodes";
        if (menuNodes) {
            return buildMenuGridModel(g);
        }
        else if (localStorage && localStorage[storageKey]) {
            menuNodes = JSON.parse(localStorage[storageKey]);
            return buildMenuGridModel(g);
        } else {
            return $.post(url).then(function(items) {
                menuNodes = items;
                localStorage[storageKey] = JSON.stringify(items);
                return buildMenuGridModel(g);
            });
        }
    };
}

function renderMenuItem(o) {
    return o.Url ? "<a href='" + o.Url + "'>" + o.Name + "</a>" : o.Name;
}
//wrap ajaxlists for horizontal scrolling on small screens
function wrapLists() {
    $('table.awe-ajaxlist:not([wrapped])').each(function () {
        var columns = $(this).find('thead th').length;
        var mw = $(this).data('mw');
        if (columns || mw) {
            mw = mw || columns * 120;

            $(this).wrap('<div style="width:100%; overflow:auto;"></div>')
                .wrap('<div style="min-width:' + mw + 'px;padding-bottom:2px;"></div>')
                .attr('wrapped', 'wrapped');
        }
    });
}

function MenuToggle(hide) {
    var $demoPage = $('#demoPage').show();
    var $demoMenu = $('#demoMenu').css('width', '');

    if (hide) {
        $('#demoMenu').hide();
        $demoPage.css('margin-left', "0");
        $('#btnMenuToggle').show().removeClass('on');
        $('body').trigger('domlay');
    } else {
        $('#demoMenu').show();
        $demoPage.css('margin-left', "14.5em");

        if ($demoPage.width() < 200) {
            $demoPage.hide();
            $demoMenu.css('width', '100%');
        }

        $('#btnMenuToggle').addClass('on');
        $('body').trigger('domlay');
    }
}

/*beginpopup*/
//this code uses popupMod variable specific to this demo, you shouldn't need this, 
//use utils.setPopup to be able to use the DropdownPopup and Inline (already called in utils.init)
function setAweDemoPopup() {
    awe.popup = function (o) {
        if (o.tag && o.tag.DropdownPopup) {
            return awem.dropdownPopup(o);
        } else if (o.tag && o.tag.Inline) {
            return awem.inlinePopup(o);
        } else if (popupMod == 'inline') {
            return awem.inlinePopup(o);
        } else if (popupMod == 'bootstrap') {
            return awem.bootstrapPopup(o);
        } else if (popupMod == 'awesome') {
            return awem.dropdownPopup(o);
        } else {
            return awem.uiPopup(o);
        }
    };
}
/*endpopup*/

function gitCaption(item) {
    return '<img class="cthumb" src="' + item.avatar + '&s=40" />' + item.c;
}

function gitItem(item) {
    var res = "<div class='content'>" + '<div class="title">' + item.c + '<img class="thumb" src="' + item.avatar + '&s=40" />' + '</div>';
    if (item.desc) res += '<p class="desc">' + item.desc + '</p>';
    res += '</div>';
    return res;
}

function gitRepoSearch(o, info) {
    var term = info.term;
    var c = info.cache;
    c.termsUsed = c.termsUsed || {};
    c.nrterms = c.nrterms || []; // no result terms

    if (c.termsUsed[term]) return [];
    c.termsUsed[term] = 1;

    var nores = 0;
    // don't search terms that contain nrterms
    $.each(c.nrterms, function (i, val) {
        if (term.indexOf(val) >= 0) {
            nores = 1;
            return false;
        }
    });

    if (nores) {
        return [];
    }

    return $.get('https://api.github.com/search/repositories', { q: term })
        .then(function (data) {
            if (!data || !data.items.length) {
                c.nrterms.push(term);
            }

            return $.map(data.items, function (item) { return { k: item.id, c: item.full_name, avatar: item.owner.avatar_url, desc: item.description }; });
        })
        .fail(function () { c.termsUsed[term] = 0; });
}

var downloadLinks = [
        { k: "http://aspnetawesome.com/Download/MvcDemoApp", c: "Main Demo (this demo)" },
        { k: "http://aspnetawesome.com/Download/MvcMinSetupDemo", c: "Min Setup Demo (Template Project)" },
        { k: "http://aspnetawesome.com/Download/MvcTrial", c: "Trial version (dll, js, css for mvc 3/4/5)" },
        { k: "http://aspnetawesome.com/Download/SimpleDemo", c: "Simple Demo" },
        { k: "http://aspnetawesome.com/Download/VBnetDemo", c: "VB.net Demo" },
        { k: "http://aspnetawesome.com/Download/ProDinner", c: "ProDinner (uses EF, N-Tier, etc.)" },
        { k: "http://prodinner.aspnetawesome.com", c: "See ProDinner live" }
];