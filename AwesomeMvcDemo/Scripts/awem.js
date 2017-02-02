awem = function ($) {
    var maxLookupDropdownHeight = 300;
    var maxDropdownHeight = 420;
    var reverseDefaultBtns;
    var closePopupOnOutClick;

    // keys you can type without opening menu dropdown
    // enter, esc, shift, left arrow, right arrow, tab
    var nonOpenKeys = [13, 27, 16, 37, 39, 9]; // keys that won't open the menu

    // down and up arrow, enter, esc, shift //, left arrow, right arrow
    var controlKeys = [40, 38, 13, 27, 16]; //, 37, 39

    var nonComboSearchKeys = [40, 38, 13, 27, 16, 37, 39, 9];

    var updownKeys = [38, 40];

    var isMobile = function () { return awem.isMobileOrTablet(); };

    var keycode = {
        enter: 13,
        backspace: 8,
        esc: 27,
        down: 40,
        up: 38,
        tab: 9
    };

    var loadingHtml = '<div class="awe-loading"><span></span></div>';

    var cache = {};
    var dpop = {};

    function cd() {
        return awem.clientDict;
    }

    function K(item) {
        return item.k;
    }

    function C(item) {
        return item.c;
    }

    function format(s, args) {
        return s.replace(/{(\d+)}/g, function (match, number) {
            return typeof args[number] != 'undefined'
      ? args[number]
      : match;
        });
    };

    function toUpperFirst(s) {
        return s.substr(0, 1).toUpperCase() + s.substr(1);
    }

    function toLowerFirst(s) {
        return s.substr(0, 1).toLowerCase() + s.substr(1);
    }

    function containsVal(itemK, vals) {
        var res = false;
        $.each(vals, function (_, value) {
            if (itemK == escape(value)) {
                res = true;
                return false;
            }
        });

        return res;
    }

    function inArray(val, vals) {
        var res = -1;
        $.each(vals, function (i, value) {
            if (val == value) {
                res = i;
                return false;
            }
        });

        return res;
    }

    function contains(key, keys) {
        return $.inArray(key, keys) != -1;
    }

    function strContainsi(c, squeryUp) {
        return (c || '').toString().toUpperCase().indexOf(squeryUp) != -1;
    }

    function strEqualsi(c, squeryUp) {
        return (c || '').toString().toUpperCase() == squeryUp;
    }

    var entityMap = {
        "&": "&amp;",
        "<": "&lt;",
        '"': '&quot;',
        "'": "&#39;",
        ">": "&gt;"
    };

    function escape(str) {
        return String(str).replace(/[&<>"']/g, function (s) {
            return entityMap[s];
        });
    }

    function fnothings(v) {
        return (v == null) ? '' : v.toString();
    }

    function unesc(str) {
        str = fnothings(str);
        for (key in entityMap) {
            str = str.replace(entityMap[key], key);
        }
        return str;
    }

    function readTag(o, prop, nullVal) {
        var res = nullVal;

        if (o.tag && o.tag[prop] != null) {
            res = o.tag[prop];
        }

        return res;
    }

    function layDropdownPopup(o, popup, isFixed, capHeight, $opener, midlay, kpos, canShrink) {
        var win = $(window);
        var winh = win.height();
        var winw = win.width();
        
        var maxw = winw - awe.scrollw() - 20;
        
        var mnw = Math.min(popup.outerWidth(), maxw);
        
        if (!kpos) {
            popup.css('left', 0);
            popup.css('top', 0);
        }

        popup.css('min-height', '');
        popup.css('height', '');
        popup.css('max-width', maxw);
        popup.css('min-width', canShrink ? '' : mnw);
        popup.css('position', '');

        var scrolltop = win.scrollTop();
        var toppos;
        var left;

        var topd = scrolltop + 10;

        if ($opener) {
            topd = $opener.offset().top;
            capHeight = capHeight || $opener.outerHeight();
        }

        var top = topd - scrolltop;
        var bot = winh - (top + capHeight);

        var autofls;
        if (midlay) {
            if (midlay(Math.max(top, bot))) {
                isFixed = 1;
                $opener = null;
                autofls = 1;
            }
        }

        if (isFixed) {
            topd = top;
            popup.css('position', 'fixed');
        }

        var w = popup.outerWidth(true);
        var h = popup.outerHeight(true);

        if ($opener) {
            left = $opener.offset().left;
            var lspace = winw - (left + w);
            if (lspace < 0) {

                var ow = $opener.outerWidth(true);
                if (ow < w)
                    left -= (w - ow);

                if (left < 10) {
                    left = 10;
                }
            }

            if (bot < h && (top > h || top > (bot + 5))) {
                // top
                toppos = topd - h;

                if (top < h) {
                    toppos = topd - top + 10;
                }

                popup.css('min-height', h + 'px');
            } else {
                toppos = topd + capHeight;

                if (bot < h) {
                    toppos -= (h - bot) + 10;
                }
            }
        } else {
            var diff = winh - h;
            toppos = diff / 2;

            if (diff > 200) toppos -= 45;

            left = Math.max((winw - popup.outerWidth()) / 2, 0);
            midlay && !autofls && midlay(Math.max(winh - top - 10));
        }

        if (!kpos || autofls) {
            popup.css('left', left);
            popup.css('top', toppos);
        }
    }

    function buttonGroupRadio(o) {
        return nbuttonGroup(o);
    }

    function buttonGroupCheckbox(o) {
        return nbuttonGroup(o, 1);
    }

    function bootstrapDropdown(o) {
        function render() {
            o.d.empty();
            var caption = cd().Select;
            var items = '';
            $.each(o.lrs, function (i, item) {
                var checked = $.inArray(K(item), awe.val(o.v)) > -1;
                if (checked) caption = C(item);
                items += '<li role="presentation"><input style="display:none;" type="radio" value="' + K(item) + '" name="' + o.nm + '" ' + (checked ? 'checked="checked"' : '') +
                    '" /><a role="menuitem" tabindex="-1" href="#" >' + C(item) + '</a></li>';
            });
            if (!items) items = "<li class='empty'>(empty)</li>";
            var res = '<div class="dropdown"><button class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown" aria-expanded="true"><span class="caption">'
                + caption +
                '</span> <i class="caret"></i></button><ul class="dropdown-menu" role="menu">' + items + "</ul><div>";

            o.d.append(res);
        };

        o.v.data('api').render = render;

        o.v.on('change', render);

        o.d.on('click', 'a', function (e) {
            e.preventDefault();
            $(this).prev().click();//click on the hidden radiobutton
        });
    }

    function nbuttonGroup(o, multiple) {
        var $odisplay;

        function init() {
            $odisplay = o.mo.odisplay;
            o.f.addClass('buttonGroup');

            $odisplay.on('click', '.awe-btn', function () {
                o.api.toggleVal($(this).data('val'));
            });
        }

        function setSelectionDisplay() {
            var val = awe.val(o.v);

            var items = '';
            $.each(o.lrs, function (index, item) {
                var selected = containsVal(K(item), val) ? "awe-selected" : "";
                items += '<button type="button" class="awe-btn awe-il ' + selected + '" data-index="' + index + '" data-val="' + K(item) + '">' + C(item) + '</button>';
            });

            $odisplay.html(items);
        }

        function setSelectionDisplayChange() {
            var vals = awe.val(o.v);
            $odisplay.children('.awe-selected').removeClass('awe-selected');
            $.each(vals, function (i, v) {
                $odisplay.children().filter(function () {
                    return $(this).data('val') == v;
                }).addClass('awe-selected');
            });
        }

        var opt = {
            setSel: setSelectionDisplay,
            setSelChange: setSelectionDisplayChange,
            init: init,
            multiple: multiple,
            noMenu: 1
        };

        return odropdown(o, opt);
    }

    function multiselb(o) {
        o.d.addClass("multiselb");
        function renderCaption() {
            return '<div class="caption">' + o.mo.inlabel + '</div>';
        }

        return odropdown(o, {
            multiple: 1,
            renderCaption: renderCaption
        });
    }

    function multiselect(o) {
        var captionText;
        var $multi = $('<div class="multi"></div>');
        var $searchtxt = $('<input type="text" class="osearch awe-il awe-txt" id="' + o.i + '-awed" />');
        var $dropmenu;
        var $caption;
        var glrs;
        var api;
        o.d.addClass("multiselect");
        var reor = readTag(o, "Reor");

        if (!isMobile())
            $multi.append($searchtxt);

        function init() {
            o.mo.odisplay.append($multi);
            $dropmenu = o.mo.dropmenu;
            captionText = o.mo.caption;

            if (!isMobile()) {
                o.mo.srctxt = $searchtxt;
            }

            api = o.api;
            glrs = api.glrs;
        }

        function handleCaption() {
            if ($multi.find('.multiRem').length)
                $caption.hide();
            else
                $caption.show();
        }

        var setSelectionDisplay = function () {
            var vals = awe.val(o.v);
            $multi.find('.caption').remove();
            $multi.find('.multiItem').remove();
            var items = '';
            var lrs = glrs();

            $.each(vals, function (_, val) {
                $.each(lrs, function (lindex, item) {
                    if (K(item) == escape(val)) {
                        items += renderSelectedItem(item, lindex);
                        return false;
                    }
                });
            });

            $caption = $('<span class="caption">' + captionText + '</span>');

            if (items) {
                autoWidth($searchtxt);
                $caption.hide();
            }

            $multi.prepend(items);
            $multi.prepend($caption);
            $searchtxt.val('');
        };

        function setSelectionDisplayChange() {
            var vals = awe.val(o.v);

            // remove keys and add items
            $multi.find('.multiRem').each(function () {
                var val = $(this).parent().data('val');
                var indexFound = inArray(val, vals);
                if (indexFound > -1) {
                    //remove from vals
                    vals.splice(indexFound, 1);
                } else {
                    $(this).parent().remove();
                }
            });

            //add multiRem for remaining vals
            var items = '';
            $.each(glrs(), function (index, item) {
                if (containsVal(K(item), vals)) {
                    items += renderSelectedItem(item, index);
                }
            });

            $searchtxt.val('');
            autoWidth($searchtxt);

            if (isMobile()) {
                $multi.append(items);
            } else {
                $searchtxt.before(items);
            }

            handleCaption();
        }

        $multi.on('click', function (e) {
            if (!$(e.target).is('.multiRem')) {

                if (!$dropmenu.hasClass('open')) {
                    api.toggleOpen();
                }

                $searchtxt.focus();
            }
        });

        $searchtxt.on('focusin', function () {
            $caption.hide();
            autoWidth($(this));
        });

        function autoWidth(input) {
            input.css('width', Math.min(Math.max((input.val().length + 2) * 10, 21), $multi.width()) + 'px');
        }

        var opt = {
            setSel: setSelectionDisplay,
            setSelChange: setSelectionDisplayChange,
            init: init,
            multiple: 1,
            prerender: function () { },
            afls: 0
        };

        function renderSelectedItem(item, index) {
            return '<div class="multiItem awe-il awe-btn" data-val="' + K(item) + '"><span class="multiCap">' + opt.renSelCap(item) + '</span><span class="multiRem" data-index="' + index + '">×</span></div>';
        }

        $searchtxt.on('keyup', function (e) {
            if (!$dropmenu.hasClass('open') && !contains(e.which, nonOpenKeys)) {
                if (!(e.which == keycode.backspace && !$searchtxt.val()))
                    o.v.data('api').toggleOpen();
            }

            if (!contains(e.which, controlKeys)) {
                var query = $(this).val();

                o.v.data('api').search(query);
            }
        });

        $searchtxt.on('keydown', function (e) {
            if (e.which == keycode.backspace && !$searchtxt.val()) {
                $multi.find('.multiRem:last').click();
            }

            autoWidth($searchtxt);
        });

        $searchtxt.on('focusout', function () {
            $searchtxt.val('').change();
            if (!$multi.children('.multiItem').length) {
                $searchtxt.css('width', '0');
                $caption.show();
            }
        });

        $multi.on('click', '.multiRem', function () {

            var val = $(this).parent().data('val');

            api.toggleVal(val);
            api.close();
            $searchtxt.focus();
        });

        if (reor) {
            var placeholder, drgObj, others, last;
            var justmoved, initm;
            function wrap(clone, dragObj) {
                initm = 1;
                placeholder = dragObj.clone().addClass('awe-changing placeh').hide();

                drgObj = dragObj.after(placeholder);
                others = $multi.find('.multiItem:not(.placeh)');
                last = $multi.find('.multiItem').last();
                return clone;
                //return $('</div>');
            }

            function end() {
                placeholder.remove();
                drgObj.show();
            }


            function hoverFunc(dragObj, x, y) {
                if (initm) {
                    drgObj.hide();
                    placeholder.show();
                    initm = 0;
                }
                var hovered;
                others.each(function (_, el) {
                    var mi = $(el);
                    var mix = mi.offset().left;
                    var miy = mi.offset().top;

                    if (x > mix && x < mix + mi.width() &&
                        y > miy && y < miy + mi.height()) {
                        hovered = mi;
                        return false;
                    }
                });

                if (justmoved) {
                    if (!hovered) {
                        justmoved = null;
                    } else if (justmoved.is(hovered)) {
                        hovered = null;
                    }
                }

                if (hovered) {
                    if (hovered.index() < placeholder.index()) {
                        hovered.before(placeholder);
                    } else {
                        hovered.after(placeholder);
                    }

                    justmoved = hovered;
                }
            }

            function dropFunc() {
                placeholder.after(drgObj).remove();
                api.moveVal(drgObj.data('val'), drgObj.prev().data('val'));
                drgObj.show();
            }

            dragAndDrop({
                from: $multi,
                sel: '.multiItem',
                to: [{ c: $('body'), drop: dropFunc, hover: hoverFunc }],
                wrap: wrap,
                end: end
            });
        }

        if (!isMobile()) {
            opt.searchOutside = 1;
            opt.noAutoSearch = 1;
        }

        return odropdown(o, opt);
    }

    function colorDropdown(o) {
        var caption;

        function init() {
            caption = o.mo.caption;
        }

        o.d.addClass("colordd");

        o.df = function () {
            return $.map(['#5484ED', '#A4BDFC', '#7AE7BF', '#51B749', '#FBD75B', '#FFB878', '#FF887C', '#DC2127', '#DBADFF', '#E1E1E1'],
                function (item) { return { k: item, c: item }; });
        };

        var renderCaption = function (selected) {
            var sel = caption;
            if (selected.length) {
                var color = K(selected[0]);
                sel = '<div style="background:' + color + '" class="color">&nbsp;</div>';
            }

            return '<span class="caption">' + sel + '</span>';
        };

        var renderItemDisplay = function (item) {
            return '<span class="colorItem" style="background:' + K(item) + '">&nbsp;</span>';
        };

        var opt = {
            renderItemDisplay: renderItemDisplay,
            renderCaption: renderCaption,
            noAutoSearch: 1,
            menuClass: "colorddmenu",
            init: init
        };

        odropdown(o, opt);
    }

    function imgDropdown(o) {
        var caption;

        o.d.addClass('imgdd');

        function init() {
            caption = o.mo.caption;
        }

        var opt = {
            menuClass: "imgddmenu",
            init: init
        };

        opt.renderItemDisplay = function (item) {
            return '<div class="imgddItem"><img src="' + item.url + '"/> ' + C(item) + '</div>';
        };

        opt.renderCaption = function (selected) {
            var sel = caption;
            if (selected.length)
                sel = '<img src="' + selected[0].url + '"/>' + C(selected[0]);
            return '<span class="caption">' + sel + '</caption>';
        };

        odropdown(o, opt);
    }

    function timepicker(o) {
        o.f.addClass("timep");

        function pad(num) {
            var s = "00" + num;
            return s.substr(s.length - 2);
        }

        o.df = function () {
            var step = readTag(o, "Step") || 30;
            var items = [];
            var ampm = o.tag.AmPm;
            for (var i = 0; i < 24 * 60; i += step) {
                var apindx = 0;
                var hour = Math.floor(i / 60);
                var minute = i % 60;

                if (ampm) {

                    if (hour >= 12) {
                        apindx = 1;
                    }

                    if (!hour) {
                        hour = 12;
                    }

                    if (hour > 12) {
                        hour -= 12;
                    }
                }

                var item = ampm ? hour : pad(hour);

                item += ":" + pad(minute);

                if (ampm) item += " " + ampm[apindx];

                items.push(item);
            }

            return $.map(items, function (v) { return { k: v, c: v }; });
        };

        return combobox(o);
    }

    function combobox(o) {
        o.d.addClass('combobox');

        var $v = o.v;
        var $combotxt = $('<input type="text" class="awe-txt combotxt osearch" size="1" autocomplete="off" id="' + o.i + '-awed" />');
        var $openbtn = $('<button type="button" class="cdropbtn odropbtn oddbtn awe-btn" tabindex="-1"><span class="selbtn"><i class="ocaret"></i></span></button>');
        var docClickRegistered = 0;
        var glrs;
        var closeOnEmpty = readTag(o, "CloseOnEmpty");
        var api;
        var dropmenu;
        var vprop;

        function init() {
            o.mo.odisplay.append($combotxt).append($openbtn);
            o.mo.srctxt = $combotxt;
            vprop = o.mo.vprop;
            $combotxt.attr('placeholder', o.mo.caption);
            api = o.api;
            glrs = api.glrs;
            dropmenu = o.mo.dropmenu;
        }

        function setSelectionDisplay() {
            var vals = awe.val($v);

            var selected = $.grep(glrs(), function (item) {
                return containsVal(item[vprop], vals);
            });

            var txtval = '';
            if (!selected.length && vals.length) {
                txtval = vals[0];
            }
            else if (selected.length) {
                txtval = unesc(C(selected[0]));
            }

            $combotxt.val(txtval);
        }

        function docClickFocusHandler(e) {
            var $target = $(e.target);
            if (!$target.closest(dropmenu).length && !$target.closest(o.d).length) {
                compval();
                checkComboval();
                docClickRegistered = 0;
                $(document).off('click focusin', docClickFocusHandler);
            }
        }

        $combotxt.on('focusin', function () {
            this.selectionStart = this.selectionEnd = this.value.length;

            if (!docClickRegistered) {
                $(document).on('click focusin', docClickFocusHandler);
                docClickRegistered++;
            }
        });

        $combotxt.on('keydown', function (e) {
            if (e.which == keycode.enter && !dropmenu.hasClass('open')) {
                e.preventDefault();
                checkComboval();
            }
        });

        $combotxt.on('keyup', function (e) {
            var val = $combotxt.val();
            var isOpen = dropmenu.hasClass('open');
            var key = e.which;

            if (closeOnEmpty && !val && !contains(key, updownKeys)) {
                if (isOpen) {
                    api.toggleOpen();
                }
            }
            else
                if (!isOpen) {
                    if (!contains(key, nonOpenKeys)) {
                        api.toggleOpen();
                    }

                    if (key == keycode.enter) {
                        checkComboval();
                    }
                }
        });

        function postSearchFunc(which) {
            if (!contains(which, nonComboSearchKeys)) {
                if (!dropmenu.find('.oitem:visible').length) {
                    api.close();
                }

                compval();
            }
        }

        $openbtn.on('click', function () {
            api.search('');
            if (!isMobile())
                $combotxt.focus();
        });

        function compval() {
            var query = $combotxt.val();
            var newVal = query;
            var cval = query;
            var indexFound;
            var itemFound;

            $.each(glrs(), function (i, item) {
                if (strEqualsi(C(item), query.toUpperCase())) {
                    indexFound = i;
                    itemFound = item;
                    newVal = item[vprop];
                    cval = C(item);
                    return false;
                }
            });

            dropmenu.find('.selected').removeClass('selected');

            if (itemFound) {
                dropmenu.find('.oitem').eq(indexFound).addClass('selected');
            }

            $v.data('comboval', newVal);
            $v.data('cval', cval);
        }

        function checkComboval() {
            if (!$v.parent().length) {
                return;
            }

            var comboval = $v.data('comboval');

            if (comboval != null) {
                api.toggleVal(comboval);
                $combotxt.val($v.data('cval'));
                api.close();
            }
        }

        odropdown(o, {
            searchOutside: 1,
            noAutoSearch: 1,
            setSel: setSelectionDisplay,
            Combo: 1,
            init: init,
            psf: postSearchFunc,
            prerender: function () { },
            afls: 0
        });
    }

    function menuDropdown(o) {
        o.d.addClass("menudd");
        var opt = {
            menuClass: "menuddmenu"
        };

        opt.renderCaption = function () {
            return '<div class="caption">' + o.mo.caption + '</div>';
        };

        opt.renderItems = function (rs) {
            var res = '';
            $.each(rs, function (i, item) {
                var attrs = ' class="oitem" data-index="' + i + '"';
                if (item.click) attrs += ' data-click="' + item.click + '"';
                res += '<li' + attrs + '>' + opt.renderItemDisplay(item) + '</li>';
            });

            if (!rs.length) {
                res += '<li class="empty">(empty)</li>';
            }

            return res;
        };

        opt.renderItemDisplay = opt.renderItemDisplay || function (item) {
            var res;
            var href = K(item) || item.href;
            if (href && !item.click) {
                res = '<a href="' + href + '">' + C(item) + '</a>';
            } else {
                res = C(item);
            }

            return res;
        };

        opt.onItemClick = function (e) {
            var $trg = $(e.target);
            if ($trg.is('.oitem')) {
                var click = $trg.data('click');

                if (click) {
                    eval(click);
                } else {
                    var $a = $trg.find('a');
                    if ($a.length)
                        $a.get(0).click();
                }
            }

            o.api.close();
        };

        return odropdown(o, opt);
    }

    function odropdown(o, opt) {

        var api = o.v.data('api');
        api.render = render;
        api.glrs = glrs;
        api.toggleVal = toggleVal;
        api.moveVal = moveVal;

        opt = opt || {};
        if (opt.afls == null) opt.afls = 1;

        var btnstr = '<button type="button" class="odropbtn oddbtn awe-btn" id="' + o.i + '-awed">{0}<span class="selbtn"><i class="ocaret"></i></span></button>';
        var srcinfo = '<li class="oinfo">' + cd().SearchForRes + '</li>';

        var $odropdown = $('<div class="odropdown"></div>');
        var $odisplay = $('<div class="odisplay oldngp">' + loadingHtml + '</div>');

        var inlabel = readTag(o, 'InLabel', '');
        var caption = readTag(o, 'Caption', cd().Select);
        var autoSelectFirst = readTag(o, 'AutoSelectFirst');
        var noSelClose = readTag(o, 'NoSelClose');
        var minWidth = readTag(o, 'MinWidth');
        var searchFunc = readTag(o, 'SrcFunc');
        var cacheKey = readTag(o, "Key", o.i);
        var itemFunc = readTag(o, 'ItemFunc');
        var captionFunc = readTag(o, 'CaptionFunc');
        var useConVal = readTag(o, "UseConVal");
        var popupClass = readTag(o, "Pc");
        var vprop = useConVal ? 'c' : 'k';

        var valInputType = opt.multiple ? "checkbox" : "radio";

        var $valCont = $('<div class="valCont"></div>').hide();

        var hostc = $('body');
        var modal = $('<div class="opmodal opc" tabindex="-1" data-i="' + o.i + '"></div>');
        var $dropmenu = $('<div class="omenu opc" tabindex="-1" data-i="' + o.i + '"></div>').addClass(opt.menuClass).addClass(popupClass); //.data('owner', $odropdown);

        if (o.rtl) $dropmenu.css('direction', 'rtl');
        if (minWidth) $odropdown.css('min-width', minWidth);

        var $menuSearchCont = $('<div class="osrccont oldngp"><input type="text" class="osearch awe-txt" placeholder="' + cd().Searchp + '" size="1"/>' + loadingHtml + '</div>');
        var $menuSearchTxt = $menuSearchCont.find('.osearch');

        var isFixed = 0;
        var zIndex = 1;

        if (isMobile())
            $dropmenu.addClass('omobile');

        $odropdown.append($odisplay);

        $dropmenu.append($menuSearchCont);

        var $itemscont = $('<div class="oitemscont"><ul class="omenuitems" tabindex="-1">' + (searchFunc ? srcinfo : '') + '</ul></div>');

        $dropmenu.append($itemscont);

        var $menu = $itemscont.children().first();

        o.d.append($valCont).append($odropdown);

        o.f.addClass('odfield');

        o.mo = { dropmenu: $dropmenu, odisplay: $odisplay, caption: caption, odropdown: $odropdown, inlabel: inlabel, msrctxt: $menuSearchTxt, vprop: vprop };
        
        opt.renderItemDisplay = opt.renderItemDisplay || function (item) {
            return itemFunc ? eval(itemFunc)(item) : C(item);
        };

        opt.renderCaption = opt.renderCaption || function (selected) {
            var sel = caption;
            if (selected.length) {
                sel = opt.renSelCap(selected[0]);
            }

            return '<div class="caption">' + inlabel + sel + '</div>';
        };

        opt.renSelCap = opt.renSelCap || function (item) {
            return captionFunc ? eval(captionFunc)(item) : C(item);
        }

        opt.setSel = opt.setSel || function () {
            $odisplay.html(opt.renderSelected());
        };

        opt.setSelChange = opt.setSelChange || opt.setSel;

        opt.renderItems = opt.renderItems || function (rs) {
            var res = '';
            $.each(rs, function (i, item) {
                res += '<li class="oitem" data-index="' + i + '" data-val="' + item[vprop] + '">' + opt.renderItemDisplay(item) + '</li>';
            });

            if (!rs.length) {
                res += '<li class="empty">(empty)</li>';
            }

            if (searchFunc) {
                res += srcinfo;
            }

            return res;
        };

        opt.renderSelected = opt.renderSelected || function () {
            var vals = awe.val(o.v);
            var selected = $.grep(glrs(), function (item) {
                return containsVal(K(item), vals);
            });
            
            var sel = opt.renderCaption(selected);

            return format(btnstr, [sel]);
        };

        opt.onItemClick = opt.onItemClick || function () {
            var $clickedItem = $(this);
            var val = $clickedItem.data('val');

            toggleVal(val);

            if (!noSelClose) {
                close();
            }

            var $osearch = $odisplay.find('.osearch');
            if ($osearch.length && !isMobile()) {
                $osearch.focus();
            } else {
                $odisplay.find('.odropbtn').focus();
            }

            $menuSearchTxt.val('');
            filter('', $clickedItem);

            if (noSelClose) {
                lay();
            }
        };

        opt.prerender = opt.prerender || function () {
            $odisplay.append(format(btnstr, [opt.renderCaption([])]));
        };

        // get last result + cache
        function glrs() {
            var cacheObj = cache[cacheKey];
            if (cacheObj) {
                var res = cacheObj.Items.slice(0);

                for (var i = 0; i < o.lrs.length; i++) {
                    if (cacheObj.Keys[K(o.lrs[i])] == null) {
                        res.push(o.lrs[i]);
                    }
                }

                return res;
            }
            return o.lrs;
        }

        function findvalinput(val) {
            return $valCont.find('input').filter(function () {
                return $(this).val() == val;
            });
        }

        function toggleVal(val) {
            //var valinput = $valCont.find('input[value="' + val + '"]');

            var valinput = findvalinput(val);

            if (valinput.length) {
                if (opt.multiple) {
                    valinput.click().remove();
                }
            } else {
                if (!opt.multiple) {
                    $valCont.empty();
                }

                valinput = $('<input type="' + valInputType + '" value="' + escape(val) + '" name="' + o.nm + '"/>');
                $valCont.append(valinput);
                valinput.click();
            }
        }

        function moveVal(val, afval) {
            var input = findvalinput(val);

            if (afval) {
                findvalinput(afval).after(input);
            } else {
                $valCont.prepend(input);
            }
        }

        function render() {
            opt.setSel();

            if ((searchFunc || glrs().length > 10 && !opt.noAutoSearch) && !opt.searchOutside) {
                $menuSearchCont.show();
            } else {
                $menuSearchCont.hide();
            }

            renderMenu();
        };

        function renderMenu() {
            if (!opt.noMenu) {
                $menu.html(opt.renderItems(glrs()));
            }

            $valCont.html(renderValInputs());

            markMenuSelectedItems();
        }

        function renderValInputs() {
            var res = '';
            var rawvals = awe.val(o.v);

            var vals = [];
            var lrs = glrs();
            for (var i = 0; i < rawvals.length; i++) {
                var val = rawvals[i];
                for (var j = 0; j < lrs.length; j++) {
                    if (escape(val) == lrs[j][vprop]) {
                        vals.push(lrs[j][vprop]);
                        break;
                    }
                }
            }
            
            if (autoSelectFirst && (!vals.length || vals.length == 1 && vals[0] == '')) {
                
                var allItems = glrs();
                if (allItems.length) {
                    vals = [allItems[0][vprop]];
                }
            }
            
            $.each(vals, function (_, value) {
                res += '<input type="' + valInputType + '" value="' + value + '" name="' + o.nm + '" checked="checked"/>';
            });

            if (!vals.length && opt.multiple) res = '<input type="checkbox" name="' + o.nm + '" />';

            return res;
        }

        function markMenuSelectedItems() {
            var val = awe.val(o.v);
            var items = glrs();
            $dropmenu.find('.oitem').each(function (i, element) {
                var checked = containsVal(items[i][vprop], val);
                if (checked) {
                    $(element).addClass('selected');
                } else {
                    $(element).removeClass('selected');
                }
            });
        }

        function autofocus($itemToFocus) {
            if ($itemToFocus) {
                focus($itemToFocus);
            } else {
                var $selected = $dropmenu.find('.selected:visible');
                if ($selected.length && !opt.multiple) {
                    focus($selected);
                } else {
                    focus($dropmenu.find('.oitem:visible:first'));
                }
            }

            scrollToFocused();
        }

        function scrollToFocused() {
            var $focused = $itemscont.find('.focus');

            if ($focused.length) {
                var contScrollTop = $itemscont.scrollTop();

                var focTopPosDelta = $focused.position().top - $itemscont.position().top;
                var focusedHeight = $focused.outerHeight(true);
                var menuHeight = $itemscont.height();

                // put focus at the bottom (skip clause if out of bounds)
                if (focTopPosDelta + focusedHeight > menuHeight && focTopPosDelta + focusedHeight <= menuHeight + focusedHeight) {
                    $itemscont.scrollTop(contScrollTop + focusedHeight);
                }
                    // focus at the top
                else if (focTopPosDelta < 0) {
                    $itemscont.scrollTop(contScrollTop + focTopPosDelta);
                }
                else if (focTopPosDelta > menuHeight) {
                    var nv = contScrollTop + focTopPosDelta - menuHeight / 2 + focusedHeight;
                    nv -= (nv % focusedHeight);
                    $itemscont.scrollTop(nv);
                }
            }
        }

        function focus(item) {
            $dropmenu.find('.focus').removeClass('focus');
            item.addClass('focus');
        }

        function filter(query, $itemToFocus) {
            var items = glrs(); //o.lrs;
            if (searchFunc) {
                var info = $itemscont.find('.oinfo');
                if (query) {
                    info.hide();
                } else {
                    info.show();
                }
            }

            var squery = query.toUpperCase();

            var count = 0;
            var f = function (s) { return s; }
            if (squery != escape(squery)) {
                f = unesc;
            }

            $dropmenu.find('.oitem').each(function (i, element) {
                if (strContainsi(f(C(items[i])), squery)) {
                    $(element).show();
                    count++;
                } else {
                    $(element).hide();
                }
            });

            autofocus($itemToFocus);

            return count;
        }

        function docClickHandler(e) {
            if ($(e.target).is('.opmodal') && e.type == 'touchstart') return;

            if (!$(e.target).closest($odisplay).length &&
                !$(e.target).closest($dropmenu).length) {
                close();
            }
        };

        function close() {
            if ($dropmenu.hasClass('open')) toggleOpen();
        }

        function toggleOpen() {
            $dropmenu.toggleClass('open');
            if ($dropmenu.hasClass('open')) {
                if (zIndex) {
                    modal.css('z-index', zIndex + 1);
                    $dropmenu.css('z-index', zIndex + 1);
                }

                $dropmenu.css('min-width', $odisplay.width() + 'px');
                $(document).on('mousedown touchstart', docClickHandler);//touchstart

                // render for searchfunc cache merging
                if (cache[cacheKey]) {
                    renderMenu();
                }

                lay();
                if (!opt.searchOutside && !isMobile()) {
                    $menuSearchTxt.focus();
                }

                autofocus();
                dpop[o.i] = $odropdown;
            } else {
                modal.hide();
                $(document).off('mousedown touchstart', docClickHandler);//touchstart

                $menuSearchTxt.val('');

                filter('');
            }
        }

        function lay() {
            if ($dropmenu.hasClass('open')) {
                var osrc = $dropmenu.find('.osrccont:visible');
                var oitemsc = $dropmenu.find('.oitemscont');
                var oitemscst = oitemsc.scrollTop();

                oitemsc.css('max-height', '');
                $dropmenu.css('width', '');
                var desh = Math.min(maxDropdownHeight, $dropmenu.find('.omenuitems').height());

                function midlay(av) {
                    if (av > maxDropdownHeight) av = maxDropdownHeight;
                    var winw = $(window).width();
                    var winh = $(window).height();

                    var fls;
                    if (!opt.Combo && opt.afls) {
                        if (av < desh + 100 && (winw < $dropmenu.width() + 200)) {
                            fls = 1;
                            $dropmenu.width(winw - 20);
                            av = winh - 50;
                        } else if (av < 170) {
                            av = winh - 10;
                        }
                    }

                    av -= 20;
                    av -= osrc.outerHeight();
                    av -= ($dropmenu.outerHeight() - $dropmenu.height());

                    oitemsc.css('max-height', av);
                    if (fls) modal.show();
                    else modal.hide();
                    return fls;
                }

                layDropdownPopup(o, $dropmenu, isFixed, null, o.d, midlay);

                oitemsc.scrollTop(oitemscst);
            }
        }

        opt.init && opt.init();

        var srctxt = o.mo.srctxt || $menuSearchTxt;

        opt.prerender();
        render();

        if (!opt.noMenu) {
            var uidialog = o.d.closest('.awe-uidialog');
            var parPop = o.d.closest('.opcont');

            var id = o.i + '-dropmenu';
            $('#' + id).remove();
            $dropmenu.attr('id', id);

            isFixed = 1;
            if (uidialog.length) {
                hostc = uidialog;
                zIndex = hostc.css('z-index');
            } else if (o.d.parents('.modal-dialog').length) {
                hostc = o.d.closest('.modal');
                zIndex = hostc.css('z-index');
            } else if (parPop.length) {
                zIndex = parPop.css('z-index');
            } else {
                isFixed = 0;
            }

            hostc.append(modal.hide());
            hostc.append($dropmenu);

            $dropmenu.on('click', '.oitem', opt.onItemClick).on('mousemove', '.oitem', function () {
                focus($(this));
            });

            $odropdown.on('click', '.odropbtn', function () {
                toggleOpen();
            });

            $odisplay.on('keydown', function (e) {
                if ($dropmenu.hasClass('open')) {
                    handleBasicKeys(e);
                }
            });

            $dropmenu.on('keydown', function (e) {
                handleBasicKeys(e);
            });

            function loadHandler() {
                if (cache[cacheKey]) {
                    cache[cacheKey] = { Items: [], Keys: {} };
                    renderMenu();
                }
            }

            o.v.on('aweload', loadHandler);

            function handleBasicKeys(e) {
                var which = e.which;
                var $focused = $dropmenu.find('.focus');
                if (contains(e.which, controlKeys))
                    e.preventDefault();

                if (which == keycode.down) {
                    var $next = $focused.nextAll('.oitem:visible:first');
                    if ($next.length) {
                        focus($next);
                        scrollToFocused();
                    }
                } else if (which == keycode.up) {
                    var $prev = $focused.prevAll('.oitem:visible:first');
                    if ($prev.length) {
                        focus($prev);
                        scrollToFocused();
                    }
                } else if (which == keycode.enter) {
                    $focused.click();
                } else if (which == keycode.esc) {
                    close();
                }
            }

            var searchTimerOn;
            var searchTimerTerm;
            var searchTimer;
            var localSearchResCount;

            srctxt.on('keyup', function (e) {
                if (!contains(e.which, controlKeys)) {
                    var term = srctxt.val().trim();
                    localSearchResCount = filter(term);

                    if (searchFunc && term) {
                        // cache can be already set by another odropdown
                        cache[cacheKey] = cache[cacheKey] || { Items: [], Keys: {} };

                        if (!searchTimerOn) {
                            searchTimerOn = 1;
                            searchTimerTerm = term;

                            searchTimer = setInterval(function () {
                                var newTerm = srctxt.val().trim();
                                if (newTerm == searchTimerTerm) {
                                    clearInterval(searchTimer);
                                    searchTimerOn = 0;

                                    if (searchTimerTerm) {
                                        srctxt.closest('.oldngp').addClass('oldng');
                                        $.when(eval(searchFunc)(o, { term: searchTimerTerm, count: localSearchResCount, cache: cache[cacheKey] }))
                                            .always(function () {
                                                srctxt.closest('.oldngp').removeClass('oldng');
                                            })
                                            .done(function (result) {
                                                if (result.length) {
                                                    $.each(result, function (i, item) {
                                                        var cacheObj = cache[cacheKey];
                                                        var keys = cacheObj.Keys;
                                                        var items = cacheObj.Items;
                                                        if (keys[K(item)] == null) {
                                                            keys[K(item)] = items.length;
                                                            items.push(item);
                                                        } else {
                                                            items[keys[K(item)]] = item;
                                                        }
                                                    });

                                                    renderMenu();
                                                    filter(searchTimerTerm);
                                                    lay();
                                                }

                                                opt.psf && opt.psf(e.which);
                                            });
                                    }
                                }

                                searchTimerTerm = newTerm;
                            }, 250);
                        }
                    } else {
                        opt.psf && opt.psf(e.which);
                    }
                }
            });

            api.toggleOpen = toggleOpen;
            api.layMenu = lay;
            api.search = filter;
            api.close = close;
        }

        o.v.on('change', function () {
            opt.setSelChange();
            markMenuSelectedItems();
            o.v.data('comboval', null);
        });

        $(window).on('resize domlay', lay);
    }

    function notif(text, time, clss) {
        var $popup = $('<div class="onotp"/>').addClass(clss);
        var $content = $('<div class="onotc"/>').html(text || 'error occured');
        var $closeBtn = $('<span class="oclose">×</span>');
        var notifCont = $('#onotifcont');

        if (!notifCont.length) {
            notifCont = $('<div class="onotcont" id="onotifcont"/>');
            notifCont.appendTo($('body'));
        }

        notifCont.prepend($popup);
        $popup.append($content);
        $popup.append($closeBtn);
        $popup.append('<div class="olbrd"/>');

        $closeBtn.on('click', close);

        $content.css('max-height', $(window).height() - 50);

        if (time) {
            setTimeout(function () {
                close(1);
            }, time);
        }

        function close(fade) {
            if (fade == 1) {
                $popup.fadeOut(function () { $popup.remove(); });
            } else {
                $popup.remove();
            }
        }
    }

    function dropdownPopup(o) {
        var p = o.p; //popup properties
        var popup = p.d; //popup div
        var wrap = $('<div class="opwrap"><div class="opcont opc" tabindex="-1" data-i="' + p.i + '"></div></div>').hide();
        var itmoved;
        var footer, header;
        var api = function () { };
        var $opener;
        var okbtn;

        var isFullscr = p.f;
        var fls;

        var occ = readTag(o, "Occ");
        var dropDown = readTag(o, "Dd", 1);
        var sh = readTag(o, "Sh");
        var toggle = readTag(o, "Tg");

        var sopener = o.opener;
        var $dropdownPopup = wrap.find('.opcont').addClass(p.pc);
        popup.addClass('opcontent');
        $dropdownPopup.append(popup);

        var modal = $('<div class="opmodal opc" tabindex="-1" data-i="' + p.i + '"></div>');
        modal.on('keyup', closeOnEsc);

        $dropdownPopup.on('keydown', function (e) {
            if (e.keyCode == keycode.tab) {
                var tabbables = $dropdownPopup.find(':tabbable'),
                    first = tabbables.first(),
                    last = tabbables.last();
                var trg = $(e.target);
                if (trg.is(last) && !e.shiftKey) {
                    first.focus();
                    return false;
                } else if (trg.is(first) && e.shiftKey) {
                    last.focus();
                    return false;
                }
            }
        });
        var isFixed;
        var zIndex = 1;

        function layPopup(isResize, canShrink) {
            if (isResize) {
                itmoved = 0;
            }

            if (!p.isOpen) return;
            popup.css('width', '');
            popup.css('height', '');

            var winh = $(window).height();
            var winw = $(window).width();

            modal.css('z-index', zIndex);

            $dropdownPopup.css('overflow-y', 'auto');
            var capHeight = o.f ? o.f.find('.awe-openbtn').outerHeight(true) : 0;

            if (zIndex) {
                $dropdownPopup.css('z-index', zIndex);
            }

            fls = isFullscr;

            function midlay(avh) {
                var dph = $dropdownPopup.outerHeight();
                var dpw = $dropdownPopup.outerWidth();
                var phd = dph - $dropdownPopup.height();
                var footerh = footer ? footer.outerHeight() : 0;
                var headerh = header ? header.outerHeight() : 0;
                var resth = phd + footerh + headerh;

                var kh = 0.8, kw = 0.7;
                if (winw - dpw < 50) kh = 0.69;
                if (api.lay && winw < 500) {
                    dph = Math.max(dph, maxLookupDropdownHeight + resth);
                    kh = 0.55;
                    kw = 0.55;
                }

                if (dph > winh * kh && dpw > winw * kw) {
                    fls = 1;
                }

                if (avh < 350) avh = winh - 20;

                avh -= resth;

                if (fls) {
                    popup.outerWidth(winw - awe.scrollw() - 30);
                    popup.outerHeight(winh - resth - 20);
                    avh = popup.height();
                }

                if (fls || p.m) {
                    modal.show();
                } else {
                    modal.hide();
                }

                // lookup
                if (api.lay) {
                    if (avh > maxLookupDropdownHeight && !fls) avh = maxLookupDropdownHeight;
                    api.lay(avh);
                } else {

                    if (p.w) {
                        var minw = Math.min(p.w - 50, winw - awe.scrollw() - 25);
                        popup.css('min-width', minw);
                    }

                    if (!$opener || !dropDown) {
                        if (p.h)
                            popup.css('min-height', Math.min(p.h - resth - 40, avh));

                        popup.css('max-height', winh - resth - 10);
                    }
                }

                return fls;
            }

            layDropdownPopup(o, $dropdownPopup, isFixed, capHeight, dropDown ? $opener : null, midlay, itmoved, canShrink);
        }

        function outClickClose(e) {
            var res;
            if (occ != null) {
                res = occ;
            } else {
                res = closePopupOnOutClick || $opener;
            }

            if (res) {
                var trg = $(e.target);

                function lookForMe(it) {
                    var popup = it.closest('.opc');

                    var pid, mclick = 0;
                    if (it.is('.opmodal')) {
                        mclick = 1;
                    }

                    if (popup.length) {
                        pid = popup.data('i');
                    }

                    if (pid) {
                        if (pid == p.i && !mclick) return 1;

                        var popener = dpop[pid];
                        if (popener)
                            return lookForMe(popener);
                    }
                }

                if (!lookForMe(trg)) {
                    if (!trg.closest($opener).length) {
                        var $omenu = $(e.target).closest('.omenu');
                        if ($omenu.length) {
                            if (!$omenu.data('owner').closest($dropdownPopup).length) {
                                api.close();
                            }
                        } else {
                            api.close();
                        }
                    }
                }

            }
        }

        $dropdownPopup.on('aweload awebeginload', function () { layPopup(); });

        $(window).on('resize domlay', function () { layPopup(1, 1); });

        api.open = function (e) {
            if (toggle) {

                if (p.isOpen) {
                    return api.close();
                }
            }

            if (sopener) {
                $opener = sopener;
            } else {
                if (e && e.target) {
                    $opener = $(e.target);
                    var btn = $opener.closest('.awe-btn');
                    if (btn.length) $opener = btn;
                }

                if (api.lay) {
                    $opener = o.f;
                }

                if ($opener && !$opener.is(':visible') || isFullscr) {
                    $opener = null;
                }
            }

            var hostc = $('body');
            isFixed = 1;
            if ($opener) {
                var uidialog = $opener.closest('.awe-uidialog');
                var parPop = $opener.closest('.opcont');

                if (uidialog.length) {
                    hostc = uidialog;
                    zIndex = hostc.css('z-index');
                } else if ($opener.parents('.modal-dialog').length) {
                    hostc = $opener.closest('.modal');
                    zIndex = hostc.css('z-index');
                }
                else if (parPop.length) {
                    zIndex = parPop.css('z-index');
                } else {
                    isFixed = 0;
                }

                header.hide();
            }

            if (!dropDown) {
                hostc = $('body');
                isFixed = 1;
                header.show();
            }

            if (sh || !dropDown) {
                header.show();
            }

            hostc.append(modal);
            hostc.append(wrap);
            wrap.show();
            p.isOpen = 1;
            layPopup(0, 1);

            dpop[p.i] = $opener;

            setTimeout(function () {
                $(document).on('mousedown.ddp', outClickClose);
            }, 100);

            if (!isMobile()) {
                setTimeout(function () { popup.find(':tabbable:first').focus(); }, 10);
            }
        };

        api.close = function () {
            wrap.hide();
            if (modal) modal.hide();
            p.isOpen = 0;
            if (p.cl) {
                p.cl();
            }

            popup.trigger('aweclose');

            if (!p.dntr) {
                wrap.remove();
                if (modal) modal.remove();
            }

            $(document).off('mousedown.ddp touchstart.ddp', outClickClose);
        };

        api.destroy = function () {
            api.close();
            wrap.remove();
            if (modal) modal.remove();
        };

        popup.data('api', api);

        header = $('<div class="opheader"><div class="optitle">' + (p.t || '&nbsp;') + '</div><span class="oclose">×</span></div>');
        $dropdownPopup.prepend(header);
        header.find('.oclose').click(api.close);

        function getDragPopup() {
            itmoved = 1;
            return $dropdownPopup;
        }

        dragAndDrop({
            from: header,
            ch: getDragPopup,
            kdh: 1,
            cancel: function () { return fls; }
        });

        var btns = p.btns;
        // add btns if any
        if (btns && btns.length) {
            var btnslen = btns.length;

            footer = $('<div/>').addClass('opbtns');
            $dropdownPopup.append(footer);

            if (reverseDefaultBtns && btnslen > 1) {
                if (btns[btnslen - 1].c) {
                    var cbtn = btns.pop();
                    var kbtn = btns.pop();
                    btns.push(cbtn);
                    btns.push(kbtn);
                }
            }

            $.each(btns, function (i, el) {
                var cls = !el.k ? 'awe-sbtn' : '';
                var btn = $('<button type="button">' + el.text + '</button>');
                if (el.tag) {
                    var tag = el.tag;
                    if (tag.K)
                        $.each(tag.K, function (indx, key) {
                            btn.attr(key, tag.V[indx]);
                        });
                }

                btn.addClass('awe-btn ' + cls + ' inl-btn');

                if (el.k) {
                    okbtn = btn;
                }

                btn.click(function () { el.click.call(popup); });
                footer.append(btn);
            });
        }

        function closeOnEsc(e) {
            if (e.which == keycode.esc) {
                api.close();
            }
        }

        $dropdownPopup.on('keyup', closeOnEsc);

        return wrap;
    }

    function uiPopup(o) {
        var $window = $(window);
        var soption = "option";
        var pp = o.p;
        var popup = pp.d;

        pp.mlh = 0;

        var autoSize = awe.autoSize;
        var fullscreen = pp.f;
        var draggable = true;

        if (!pp.r) pp.r = false;

        if (fullscreen) {
            pp.r = false;
            draggable = false;
            pp.m = true;
        }

        popup.dialog({
            draggable: draggable,
            width: pp.w,
            height: pp.h,
            modal: pp.m,
            resizable: pp.r,
            buttons: pp.btns,
            autoOpen: false,
            title: pp.t,
            resizeStop: function () {
                pp.w = popup.dialog(soption, 'width');
                pp.h = popup.dialog(soption, 'height');
                pp.p = popup.dialog(soption, 'position');
            },
            dragStop: function () {
                pp.p = popup.dialog(soption, 'position');
            }
        });

        var dialogClass = "awe-uidialog awe-popupw";
        if (o.rtl) {
            dialogClass += ' awe-rtl';
        }

        if (pp.pc) dialogClass = dialogClass + " " + pp.pc;
        popup.dialog(soption, { dialogClass: dialogClass });

        var autoResize = function () { };
        if (fullscreen || autoSize) {
            //autosize
            autoResize = function (e) {
                if (popup.is(':visible'))
                    if (!e || e.target == window || e.target == document) {

                        var wh = $window.height();
                        var ww = $window.width();

                        var sw = pp.w > ww - 10 || fullscreen ? ww - 10 : pp.w;
                        var sh = pp.h > wh - 5 || fullscreen ? wh - 20 : pp.h;
                        var opt = {
                            height: sh,
                            width: sw
                        };

                        //on ie9 it goes off screen on zoom when setting position
                        if (!fullscreen && pp.p) opt.position = pp.p;
                        popup.dialog(soption, opt).trigger('aweresize');
                    }
            };

            $window.on('resize', autoResize);
            autoResize();
            popup.on('change', autoResize);
        }//end if fullscreen or autoSize 

        popup.on('dialogclose', function () {
            popup.trigger('aweclose');

            pp.isOpen = 0;
            if (pp.cl) {
                pp.cl.call(o);
            }

            if (!pp.dntr) {
                if (autoSize || fullscreen) {
                    $window.off('resize', autoResize);
                }
                popup.find('*').remove();
                popup.remove();
            }


        }).on('dialogresize', function () {
            popup.trigger('aweresize');
        });

        var api = function () { };
        api.open = function () {
            popup.dialog('open');
            pp.isOpen = 1;
            popup.trigger('aweopen');
            autoResize();
        };
        api.close = function () {
            popup.dialog('close');
        };
        api.destroy = function () {
            api.close();
            $window.off('resize', autoResize);
            popup.remove();
        };

        popup.data('api', api);
    }

    function bootstrapPopup(o) {
        var p = o.p; //popup properties
        var popup = p.d; //popup div
        var modal = $('<div class="modal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">' +
            '<div class="modal-dialog"><div class="modal-content awe-popupw"><div class="modal-header"><button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>' +
            '<h4 class="modal-title"></h4></div></div></div></div>');

        var hasFooter = p.btns && p.btns.length;

        //minimum height of the lookup/multilookup content
        p.mlh = !p.f ? 250 : 0;

        if (!p.t) {
            p.t = "&nbsp;"; //put one space when no title
        }

        popup.addClass("modal-body");
        popup.css('overflow', 'auto');

        modal.find('.modal-content').append(popup);
        modal.find('.modal-title').html(p.t);
        popup.show();

        //use to resize the popup when fullscreen
        function autoResize() {
            var h = $(window).height() - 120;
            if (hasFooter) h -= 90;
            if (h < 400) h = 400;
            popup.height(h);
            popup.trigger('aweresize');
        }

        var api = function () { };
        api.open = function () {
            modal.appendTo($('body')); //appendTo each time to prevent modal to show under current top modal
            modal.modal('show');
            p.isOpen = 1;
            popup.trigger('aweopen');
            if (p.f) autoResize();
        };
        api.close = function () {
            modal.modal('hide');

            p.isOpen = 0;
            if (p.cl) {
                p.cl();
            }
            if (!p.dntr) {
                popup.find('*').remove();
                popup.closest('.modal').remove();
            }
        };

        api.destroy = function () {
            api.close();
            $(window).off('resize', autoResize);
            popup.closest('.modal').remove();
        };

        popup.data('api', api);

        modal.on('hidden.bs.modal', function () {
            popup.trigger('aweclose');
        });

        $('body').append(modal);

        //fullscreen
        if (p.f) {
            modal.find('.modal-dialog').css('width', 'auto').css('margin', '10px');
            $(window).on('resize', autoResize);
        }

        //add buttons if any
        if (hasFooter) {
            var footer = $('<div class="modal-footer"></div>');
            modal.find('.modal-footer').remove();
            modal.find('.modal-content').append(footer);
            $.each(p.btns, function (i, e) {
                var btn = $('<button type="button" class="btn btn-default">' + e.text + '</button>');
                btn.click(function () { e.click.call(popup); });
                footer.append(btn);
            });
        }
    }

    function inlinePopup(o) {
        var p = o.p; //popup properties
        var popup = p.d; //popup div
        var wrap = $('<div class="oinlpop awe-popupw"></div>').hide();

        //minimum height of the lookup/multilookup content
        p.mlh = 250;

        wrap.append(popup);

        //decide where to attach the inline popup
        //tag and tags are set using .Tag(object) .Tags(string)
        if (o.tag && o.tag.target) {
            $('#' + o.tag.target).append(wrap);
        } else if (o.tag && o.tag.cont) {// cont used in grid nesting
            o.tag.cont.prepend(wrap);
        } else if (o.tags) {
            $('#' + o.tags).append(wrap);
        } else if (o.f) { //component field
            o.f.after(wrap);
        } else {
            $('body').prepend(wrap);
        }

        var api = function () { };
        api.open = function () {
            wrap.show();
            p.isOpen = 1;
            popup.trigger('aweopen');
        };
        api.close = function () {
            wrap.hide();
            p.isOpen = 0;
            if (p.cl) {
                p.cl();
            }
            popup.trigger('aweclose');
            if (!p.dntr) {
                wrap.remove();
            }
        };
        api.destroy = function () {
            api.close();
            wrap.remove();
        };

        popup.data('api', api);

        var title = $('<div class="inl-title"></div>');
        var closeBtn = $('<button type="button" class="awe-btn">&nbsp;X&nbsp;</button>').click(api.close);
        title.append(closeBtn).append("<span class='inl-txt'>" + p.t + "</span>");

        if (!readTag(o, "NoTitle"))
            wrap.prepend(title);

        // add btns if any
        if (p.btns) {
            var footer = $('<div></div>');
            wrap.append(footer);
            $.each(p.btns, function (i, e) {
                var btn = $('<button type="button" class="awe-btn inl-btn">' + e.text + '</button>');
                btn.click(function () { e.click.call(popup); });
                footer.append(btn);
            });
        }

        return wrap;
    }

    function gridPageInfo(o) {
        var $grid = o.v;
        var $pageInfo = $('<div class="gridPageInfo"></div>');
        var delta = 0;
        var $footer = $grid.find('.awe-footer');
        if (!$footer.length) return;

        $grid.data('api').cdelta = function (x) {
            delta += x;
            render();
        }

        $grid.find('.awe-footer').prepend($pageInfo);

        $grid.on('aweload', function (e) {
            if (!$(e.target).is($grid)) return;
            delta = 0;
            render();
        });

        function render() {
            var lrs = $grid.data('o').lrs;
            var pageSize = lrs.ps;
            var itemsCount = lrs.ic + delta;

            var first = pageSize * (lrs.p - 1) + 1;
            var last = lrs.pgn ? first + pageSize - 1 + delta : itemsCount;
            if (last > itemsCount) last = itemsCount;
            if (!itemsCount || !last) first = 0;

            $pageInfo.html(first + ' - ' + (last) + ' ' + format(cd().GridInfo, [itemsCount]));
        }
    }

    function gridPageSize(o) {
        if (isMobile()) return;

        var items = [5, 10, 20, 50];
        function addIfLacks(ni) {
            if (!contains(ni, items)) {
                items.push(ni);
                items.sort(function (a, b) {
                    return a - b;
                });
            }
        }

        var $grid = o.v;

        var $footer = $grid.find('.awe-footer');
        if (!$footer.length) return;

        $grid.find('.awe-footer').append('<div class="awe-ajaxradiolist-field gridPageSize" ><input id="' + o.i + 'PageSize" class="awe-val" type="hidden" value="' + o.ps + '" /><div class="awe-display"></div></div>');

        addIfLacks(o.ps);

        var psi = o.i + 'PageSize';

        function setPages() {
            return $.map(items, function (val) {
                return { c: val, k: val };
            });
        }

        awe.radioList({ i: psi, nm: psi, df: setPages, l: 1, md: awem.odropdown, tag: { InLabel: "page size: " } });

        o.data.keys.push("pageSize");
        o.data.vals.push(psi);
        o.data.l.push(1);
    }

    function gridInfScroll(o) {
        var $grid = o.v;
        var $content = $grid.find('.awe-content');
        var $tw = $content.children().first();

        function adjustMargin() {
            var diff = (Math.max(($content.height() - $tw.height()) + 20, 20));

            $tw.css('margin-bottom', diff + 'px');
        }

        adjustMargin();

        $content.on('scroll', function () {
            var res = $grid.data('o').lrs;

            var st = $content.scrollTop();
            var sh = $content.prop('scrollHeight') - $content.height();
            var lstv = $content.data('lastsv');

            adjustMargin();

            if (st > sh) {
                st -= awe.scrollw();
            } // deduct horizontal scrollbar height

            if ((lstv + 1) == sh && st == sh) {
                if (res.p < res.pc) {
                    $.when(nextPage()).done(function () {
                        $content.scrollTop(1);
                        st = 1;
                    });
                }
            }
            else if (st == sh) {
                st--;
                $content.scrollTop(st);
            }
            else if ((lstv - 1) == 0 && st == 0) {
                if (res.p > 1) {
                    $.when(prevPage()).done(function () {
                        st = sh - 1;
                        $content.scrollTop(st);
                    });
                }
            } else if (st == 0) {
                st++;
                $content.scrollTop(st);
            }

            $content.data('lastsv', st);

            function nextPage() {
                return $grid.data('api').load({ oparams: { page: res.p + 1 } });
            }

            function prevPage() {
                return $grid.data('api').load({ oparams: { page: res.p - 1 } });
            }
        });
    }

    function isMobileOrTablet() {
        return false;
    }

    var clientDict = {
        GridInfo: "of {0} items",
        Select: 'please select',
        SearchForRes: 'search for more results',
        Searchp: 'search...',
        NoRecFound: 'no records found'
    };

    //requires css from here: http://tobiasahlin.com/spinkit (used in prodinner)
    function gridLoading(o, opt) {
        opt = opt || {};
        opt.lhtm = opt.lhtm || '<div class="spinner"><div class="dot1"></div><div class="dot2"></div></div>';
        var ctm = opt.ctm || 40;

        var $grid = o.v;
        var $mcontent = $grid.find('.awe-content');

        $grid.on('awebeginload', function (e) {
            if ($(e.target).is($grid)) {
                $grid.find('.ogempt').remove();
                var $spin = $('<div class="spinCont">' + opt.lhtm + '</div>').hide();
                $spin.height($mcontent.height());
                $mcontent.prepend($spin);
                $spin.children().first().css('margin-top', ($mcontent.height() / 2 - ctm) + 'px');
                $spin.fadeIn();
            }
        }).on('aweload', function (e, res) {
            if ($(e.target).is($grid)) {
                $mcontent.find('.spinCont:first').fadeOut().remove();
                if (!res.ic) {
                    $mcontent.prepend($('<div class="ogempt">' + cd().NoRecFound + '</div>').css('margin-top', Math.max(($mcontent.height() / 2) - 90, 10) + 'px'));
                }
            }
        });
    }

    function gridMovRows(opt) {
        return function (o) {
            var $grid = o.v;
            var placeh;
            var $fromCont = $grid.find('.awe-content');
            var hovered;
            var drgObj;
            var rowmodel;
            var prevIndx;
            var ogrow;
            var hi, di;
            var grids = [o.v.attr('id')];
            var currhovering;

            if (opt && opt.G) {
                grids = opt.G;
            }

            function getRow($c) {
                return $c.closest('.awe-grid').data('api').renderRow(rowmodel);
            }

            function wrap(clone, dragObj) {
                placeh = ogrow = currhovering = null;
                prevIndx = dragObj.index();
                drgObj = dragObj;
                rowmodel = drgObj.data('model');

                var res = $('<div class="awe-grid"/>')
                            .append($('<table/>')
                                .append(dragObj.closest('table').find('colgroup').clone())
                                .append(clone));

                return res;
            }

            function hoverFunc($c) {
                return function (dragObj, x, y) {

                    if (placeh) {
                        placeh.detach();
                    }

                    if (currhovering != $c) {
                        currhovering = $c;
                        placeh = getRow($c).addClass('awe-changing');
                        ogrow = placeh.clone();
                    }

                    drgObj.show();
                    di = drgObj.index();

                    if (!$c.is($fromCont)) {
                        $c.find('.awe-tbody').prepend(ogrow.show());
                        di = 0;
                    }

                    hovered = null;
                    $c.find('.awe-row').each(function (i, el) {
                        if ($(el).offset().top + $(el).height() > y) {
                            hovered = $(el);
                            return false;
                        }
                    });

                    if (hovered == null) {
                        $c.find('.awe-tbody').append(placeh);
                    } else {
                        hi = hovered.index();
                        if (di > hi) {
                            hovered.before(placeh);
                        } else {
                            hovered.after(placeh);
                        }
                    }

                    drgObj.hide();
                    ogrow.hide();
                }
            }

            function dropFunc($c) {
                return function (dragObj) {
                    var newRow = getRow($c);

                    if (hovered == null) {
                        $c.find('.awe-tbody').append(newRow);
                    } else if (di > hi)
                        hovered.before(newRow);
                    else {
                        hovered.after(newRow);
                    }

                    dragObj.remove();
                    var $grid = $c.closest('.awe-grid');
                    $grid.trigger('awerowmove', [newRow, prevIndx, $fromCont]);
                    $grid.data('o').lrso = 1;
                };
            }

            // called on move when switching containers and end
            function resetHover() {
                if (placeh) {
                    placeh.detach();
                    ogrow.detach();
                }
            }

            function end() {
                drgObj.show();
            }

            var to = [];
            var scroll = [];

            $.each(grids, function (i, val) {
                var $grid = $('#' + val).find('.awe-content');
                to.push({ c: $grid, drop: dropFunc($grid), hover: hoverFunc($grid) });
                scroll.push({ c: $grid, y: 1 });
            });

            scroll.push({ c: $(window), y: 1 });

            dragAndDrop({
                from: $fromCont,
                sel: '.awe-row',
                to: to,
                wrap: wrap,
                reshov: resetHover,
                scroll: scroll,
                cancel: function (isTouch, coords) {
                    var rleft = coords.pageX - $fromCont.offset().left;
                    return isTouch && (($fromCont.width() - rleft < 100 || rleft < 100));
                },
                end: end
            });
        };
    }

    function gridInlineEdit(createUrl, editUrl, oneRow, reloadOnSave) {
        return function (o) {
            var $grid = o.v;
            var api = $grid.data('api');
            var newic = 1;

            api.inlineCreate = function (newModel) {
                newModel = newModel || {};
                var $newRow = $grid.data('api').renderRow(newModel);
                $newRow.addClass('newrow');
                $grid.find('.awe-content .awe-tbody').prepend($newRow);
                inlineEdit($newRow);
            };

            function inlineEdit($row) {
                if (oneRow) {
                    $grid.find('.ginlrow').each(function (_, e) { cancelRow($(e)) });
                }

                $row.addClass('ginlrow');
                $row.find('.awe-btn').hide();
                $row.find('.gcancelbtn,.gsavebtn').show();

                var $colgroup = $row.closest('.awe-table').find('colgroup');
                var model = $row.data('model');
                var gridcfg = $grid.data('o');
                var columns = gridcfg.columns;
                var hidden = '';

                var prefix = gridcfg.i + (model[gridcfg.k] || '');

                if ($row.hasClass('newrow')) {
                    prefix += 'new' + (newic++);
                }

                function getVal(prop) {
                    var val = model[o.lrs.a ? toLowerFirst(prop) : prop];

                    val = awe.rgv(val);
                    val = val instanceof Array ? JSON.stringify(val) : val;
                    if (typeof val == "boolean") val = val ? 'checked' : '';
                    return val;
                }

                $.each(columns, function (_, column) {
                    var tdi = $colgroup.find('col[data-i="' + column.i + '"]').index();
                    var tag = column.Tag;
                    if (tag) {
                        var valProp = tag.ValProp || column.P || "";
                        var modelProp = tag.ModelProp || valProp;

                        var value = getVal(valProp);

                        var format = tag.Format;

                        if (tag.FormatFunc) {
                            format = eval(tag.FormatFunc)(model, tag.Fpar);
                        }

                        if (format) {
                            format = format.split('#Value').join(value).split('#Prefix').join(prefix);

                            for (var key in model) {
                                var sval = getVal(key);
                                
                                format = format.split(".(" + key +")").join(sval)
                                               .split(".(" + toUpperFirst(key)+")").join(sval)
                                               .split("." + key).join(sval)
                                               .split("." + toUpperFirst(key)).join(sval);
                            }

                            format = format.replace(/\.\(\w+\)/g, "");

                            if (column.Hid) {
                                hidden += format;
                            } else {
                                var validstr = modelProp && format.indexOf("awe-gvalidmsg") == -1 ? '<div class="awe-gvalidmsg ' + modelProp + '"></div>' : '';
                                $row.children().eq(tdi).html(format + validstr);
                            }
                        }
                    }
                });

                if (hidden) {
                    $row.children().first().append($('<div>' + hidden + '</div>').hide());
                }

                $row.find(':tabbable:first:not(.hasDatepicker)').focus();
                $row.trigger('aweinlineedit');
            }

            $grid.on('click', '.gsavebtn', function () {
                var $this = $(this);
                var $row = $this.closest('.awe-row');

                if ($row.data('slock')) return;
                $row.data('slock', 1);

                var url = $row.hasClass('newrow') ? createUrl : editUrl;

                $grid.data('o').lrso = 1;
                $.post(url, $row.find(':input').serializeArray().concat(awe.params($grid.data('o'), 1)), function (rdata) {
                    $row.find('.awe-gvalidmsg').empty();
                    var errors = rdata.e;
                    if (errors) {
                        for (var k in errors) {
                            var msg = '';
                            for (var i = 0; i < errors[k].length; i++) {
                                msg += '<div class="field-validation-error">' + errors[k][i] + '</div>';
                            }

                            if (!k || !$row.find('.' + k).length) {
                                $grid.find('.awe-gvalidmsg:last').append(msg);
                            } else {
                                $row.find('.' + k).html(msg);
                            }
                        }
                    } else {
                        var sep = {r: rdata};
                        $row.trigger('aweinlinesave', [sep]);
                        if (!sep.cancel) {
                            if (reloadOnSave) {
                                $grid.data('api').load();
                            } else if (rdata.Item) {
                                $row.remove();
                                var $nrow = $grid.data('api').renderRow(rdata.Item);
                                $grid.find(".awe-content .awe-tbody").prepend($nrow);
                                $nrow.addClass("awe-changing").removeClass("awe-changing", 1000);
                            } else {
                                var item = $row.data('model');
                                var key = $grid.data('o').k;
                                api.update(item[key]);
                            }
                        }
                    }
                }).fail(function (p1, p2, p3) {
                    awe.err(o, p1, p2, p3);
                }).always(function () {
                    $row.data('slock', 0);
                });
            })
                .on('click', '.gcancelbtn', function () {
                    cancelRow($(this).closest('.awe-row'));
                })
                .on('click', '.geditbtn', function () {
                    var $this = $(this);
                    var $row = $this.closest('.awe-row');
                    inlineEdit($row);
                });

            function cancelRow($row) {
                if ($row.hasClass('newrow')) {
                    $row.remove();
                } else {
                    var item = $row.data('model');
                    var key = $grid.data('o').k;
                    api.update(item[key]);
                }
            }
        };
    }

    function gridColAutohide(o) {
        function isColumnHidden(column) {
            return !o.sgc && column.Gd || column.Hid;
        }

        function autohide(col) {
            return col.Tag && col.Tag.Autohide || 0;
        }

        var $grid = o.v;
        var $columnsSelector = $grid.find('#' + o.i + 'ColSel');

        function autohideColumns() {
            var changes = 0;
            var avw = $grid.find('.awe-hcon').width() || $grid.find('.awe-content').width() - awe.scrollw();
            var eo = $grid.data('o');

            var ahcols = $.grep(eo.columns, function (col) {
                return autohide(col);
            }).sort(function (a, b) { return autohide(b) - autohide(a); }).reverse();

            // unhide autohidden
            $.each(ahcols, function (_, col) {
                if (col.Hid == 2) {
                    col.Hid = 0;
                    changes++;
                }
            });

            var contentWidth = o.api.conw();
            if (avw < contentWidth) {
                $.each(ahcols, function (_, col) {
                    if (!isColumnHidden(col)) {
                        col.Hid = 2;
                        changes--;
                        contentWidth -= col.W || col.Mw;
                        if (contentWidth <= avw) return false;
                    }
                });
            }

            if ($columnsSelector.length) {
                $columnsSelector.data('api').setItems();
            }

            return changes;
        }

        $grid.on('aweinit', function (e) {
            if ($(e.target).is($grid)) {
                autohideColumns();
            }
        });

        $(window).on('resize domlay', function () {
            if (autohideColumns() && o.lrs && !o.ldg) {
                $grid.data('api').render();
            }
        });
    }

    function gridColSel(o) {
        var $grid = o.v;
        var scid = o.i + 'ColSel';

        $grid.find('.awe-footer').prepend('<div class="awe-ajaxcheckboxlist-field gridColSel" ><input id="' + scid + '" class="awe-val awe-array" type="hidden" /><div class="awe-display"></div></div>');

        function getColumnsDataFunc() {
            var result = [];
            $.each($grid.data('o').columns, function (i, col) {
                var name = col.H || col.P || "col" + (i + 1);
                if (!(col.Tag && col.Tag.Nohide))
                    result.push({ k: i, c: name });
            });

            return result;
        }

        awe.checkboxList({ i: scid, nm: scid, df: getColumnsDataFunc, l: 1, md: awem.multiselb, tag: { InLabel: "<i class='o'></i><i class='o'></i><i class='o'></i>", NoSelClose: 1 } });
        var colSel = $('#' + scid);

        function setItems() {
            var selColIndx = []; // value
            $.each($grid.data('o').columns, function (i, col) {
                if (!col.Hid && !(col.Tag && col.Tag.Nohide)) selColIndx.push(i);
            });

            colSel.val(JSON.stringify(selColIndx));
            colSel.data('api').load();
        }

        $grid.on('aweload awereorder', function (e) {
            if ($(e.target).is($grid)) {
                setItems();
            }
        });

        colSel.on('change', function () {
            var colIndxs = $.parseJSON($(this).val() || "[]");
            $.each($grid.data('o').columns, function (i, col) {
                if ($.inArray(i.toString(), colIndxs) == -1 && !(col.Tag && col.Tag.Nohide)) {
                    if (!col.Hid) {
                        col.Hid = 1; //hide column
                        if (col.Gd) {
                            //remove grouped when hiding column
                            col.Gd = 0;
                            $grid.data('o').lrso = 1;
                        }
                    }
                } else {
                    col.Hid = 0;
                }
            });

            var api = $grid.data('api');
            api.persist();
            api.render();
        });

        // used by columns autohide mod
        colSel.data('api').setItems = setItems;
    }

    function gridMiniPager(o) {
        return gridAutoMiniPager(o, 1);
    }

    function gridAutoMiniPager(oo, useMiniPager) {
        var $grid = oo.v;
        var $footer = $grid.find('.awe-footer');
        if (!$footer.length) return;
        var api = $grid.data('api');
        var original = api.buildPager;
        var miniPager = function (o) {
            var pageCount = o.lrs.pc;
            var page = o.lrs.p || 1;
            if (o.lrs.pgn) {
                var result = '';

                result += renderButton(1, icon('glyphicon-backward'), 0, page < 2);
                result += renderButton(page - 1, icon('glyphicon-chevron-left'), 0, page < 2);

                result += renderButton(page, page, 1);

                result += renderButton(page + 1, icon('glyphicon-chevron-right'), 0, pageCount <= page);
                result += renderButton(pageCount, icon('glyphicon-forward'), 0, pageCount <= page);

                var $pager = $grid.find('.awe-pager');
                $pager.html(result);
                $pager.find('.awe-btn').on('click', function () {
                    o.pg = parseInt($(this).data('p'));
                    $grid.data('api').load();
                });
                setTimeout(function () {
                    api.lay();
                }, 10);
            }
        };

        var pagerType;
        api.buildPager = function (o) {
            if (useMiniPager || $(window).width() < 1000) {
                miniPager(o);
                pagerType = 1;
            } else {
                original(o);
                pagerType = 2;
            }
        };

        if (!useMiniPager) {
            $(window).resize(function () {
                var o = $grid.data('o');
                if (o.lrs) {
                    api.buildPager(o);
                }
            });
        }

        function icon(icls) {
            return '<span class="glyphicon ' + icls + '" aria-hidden="true"></span>';
        }

        function renderButton(page, caption, selected, disabled) {
            var clss = "awe-btn mpbtn ";
            if (selected) clss += "awe-selected ";
            if (disabled) clss += "awe-disabled ";
            var dis = disabled ? "disabled" : '';
            return '<button type="button" class="' + clss + '" data-p="' + page + '" ' + dis + '>' + caption + '</button>';
        }
    }

    function dragAndDrop(opt) {
        var dropContainers = [];
        var dropFuncs = [];
        var dropHoverFuncs = [];

        opt.to && $.each(opt.to, function (i, val) {
            dropContainers.push(val.c);
            dropHoverFuncs.push(val.hover);
            dropFuncs.push(val.drop);
        });

        awe.rdd(opt.from, opt.sel, dropContainers, dropFuncs, opt.dragClass, opt.hide, dropHoverFuncs, opt.end, opt.reshov, opt.scroll, opt.wrap, opt.ch, opt.cancel, opt.kdh, opt.move);
    }

    return {
        gridMovRows: gridMovRows,
        dragAndDrop: dragAndDrop,
        clientDict: clientDict,
        gridInlineEdit: gridInlineEdit,
        gridLoading: gridLoading,
        gridInfScroll: gridInfScroll,
        gridPageSize: gridPageSize,
        gridPageInfo: gridPageInfo,
        gridColSel: gridColSel,
        gridColAutohide: gridColAutohide,
        btnGroup: buttonGroupRadio,
        btnGroupChk: buttonGroupCheckbox,
        bootstrapDropdown: bootstrapDropdown,
        multiselect: multiselect,
        colorDropdown: colorDropdown,
        imgDropdown: imgDropdown,
        combobox: combobox,
        timepicker: timepicker,
        menuDropdown: menuDropdown,
        odropdown: odropdown,
        dropdownPopup: dropdownPopup,
        uiPopup: uiPopup,
        bootstrapPopup: bootstrapPopup,
        inlinePopup: inlinePopup,
        isMobileOrTablet: isMobileOrTablet,
        multiselb: multiselb,
        gridAutoMiniPager: gridAutoMiniPager,
        gridMiniPager: gridMiniPager,
        notif: notif,
        escape: escape
    };
}(jQuery);