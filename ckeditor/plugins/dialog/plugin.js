﻿/*
Copyright (c) 2003-2009, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.plugins.add('dialog', {
    requires: ['dialogui']
});
CKEDITOR.DIALOG_RESIZE_NONE = 0;
CKEDITOR.DIALOG_RESIZE_WIDTH = 1;
CKEDITOR.DIALOG_RESIZE_HEIGHT = 2;
CKEDITOR.DIALOG_RESIZE_BOTH = 3;
(function () {
    function a(y) {
        return !!this._.tabs[y][0].$.offsetHeight;
    };

    function b() {
        var C = this;
        var y = C._.currentTabId,
            z = C._.tabIdList.length,
            A = CKEDITOR.tools.indexOf(C._.tabIdList, y) + z;
        for (var B = A - 1; B > A - z; B--)
        if (a.call(C, C._.tabIdList[B % z])) return C._.tabIdList[B % z];
        return null;
    };

    function c() {
        var C = this;
        var y = C._.currentTabId,
            z = C._.tabIdList.length,
            A = CKEDITOR.tools.indexOf(C._.tabIdList, y);
        for (var B = A + 1; B < A + z; B++)
        if (a.call(C, C._.tabIdList[B % z])) return C._.tabIdList[B % z];
        return null;
    };
    var d = {};
    CKEDITOR.dialog = function (y, z) {
        var A = CKEDITOR.dialog._.dialogDefinitions[z];
        if (!A) {
            console.log('Error: The dialog "' + z + '" is not defined.');
            return;
        }
        A = CKEDITOR.tools.extend(A(y), f);
        A = CKEDITOR.tools.clone(A);
        A = new j(this, A);
        this.definition = A = CKEDITOR.fire('dialogDefinition', {
            name: z,
            definition: A
        }, y).definition;
        var B = CKEDITOR.document,
            C = y.theme.buildDialog(y);
        this._ = {
            editor: y,
            element: C.element,
            name: z,
            contentSize: {
                width: 0,
                height: 0
            },
            size: {
                width: 0,
                height: 0
            },
            updateSize: false,
            contents: {},
            buttons: {},
            accessKeyMap: {},
            tabs: {},
            tabIdList: [],
            currentTabId: null,
            currentTabIndex: null,
            pageCount: 0,
            lastTab: null,
            tabBarMode: false,
            focusList: [],
            currentFocusIndex: 0,
            hasFocus: false
        };
        this.parts = C.parts;
        this.parts.dialog.setStyles({
            position: CKEDITOR.env.ie6Compat ? 'absolute' : 'fixed',
            top: 0,
            left: 0,
            visibility: 'hidden'
        });
        CKEDITOR.event.call(this);
        i
        f(A.onLoad) this.on('load', A.onLoad);
        if (A.onShow) this.on('show', A.onShow);
        if (A.onHide) this.on('hide', A.onHide);
        if (A.onOk) this.on('ok', function (M) {
            if (A.onOk.call(this, M) === false) M.data.hide = false;
        });
        if (A.onCancel) this.on('cancel', function (M) {
            if (A.onCancel.call(this, M) === false) M.data.hide = false;
        });
        var D = this,
            E = function (M) {
            var N = D._.contents,
                O = false;
            for (var P in N) for (var Q in N[P]) {
                O = M.call(this, N[P][Q]);
                if (O) return;
            }
        };
        this.on('ok', function (M) {
            E(function (N) {
                if (N.validate) {
                    var O = N.validate(this);
                    if (typeof O == 'string') {
                        alert(O);
                        O = false;
                    }
                    if (O === false) {
                        if (N.select) N.select();
                        else N.focus();
                        M.data.hide = false;
                        M.stop();
                        return true;
                    }
                }
            });
        }, this, null, 0);
        this.on('cancel', function (M) {
            E(function (N) {
                if (N.isChanged()) {
                    if (!confirm(y.lang.common.confirmCancel)) M.data.hide = false;
                    return true;
                }
            });
        }, this, null, 0);
        this.parts.close.on('click', function (M) {
            if (this.fire('cancel', {
                hide: true
            }).hide !== false) this.hide();
        }, this);

        function F(M) {
            var N = D._.focusList,
                O = M ? 1 : - 1;
            if (N.length < 1) return;
            var P = (D._.currentFocusIndex + O + N.length) % (N.length);
            while (!N[P].isFocusable()) {
                P = (P + O + N.length) % (N.length);
                if (P == D._.currentFocusIndex) break;
            }
            N[P].focus();
        };

        function G(M) {
            if (D != CKEDITOR.dialog._.currentTop) return;
            var N = M.data.getKeystroke(),
                O = false;
            if (N == 9 || N == CKEDITOR.SHIFT + 9) {
                var P = N == CKEDITOR.SHIFT + 9;
                if (D._.tabBarMode) {
                    var Q = P ? b.call(D) : c.call(D);
                    D.selectPage(Q);
                    D._.tabs[Q][0].focus();
                }
                else F(!P);
                O = true;
            }
            else if (N == CKEDITOR.ALT + 121 && !D._.tabBarMode) {
                D._.tabBarMode = true;
                D._.tabs[D._.currentTabId][0].focus();
                O = true;
            }
            else if ((N == 37 || N == 39) && (D._.tabBarMode)) {
                Q = N == 37 ? b.call(D) : c.call(D);
                D.selectPage(Q);
                D._.tabs[Q][0].focus();
                O = true;
            }
            if (O) {
                M.stop();
                M.data.preventDefault();
            }
        };
        this.on('show', function () {
            CKEDITOR.document.on('keydown', G, this, null, 0);
            if (CKEDITOR.env.ie6Compat) {
                var M = o.getChild(0).getFrameDocument();
                M.on('keydown', G, this, null, 0);
            }
        });
        this.on('hide', function () {
            CKEDITOR.document.removeListener('keydown', G);
        });
        this.on('iframeAdded', function (M) {
            var N = new CKEDITOR.dom.document(M.data.iframe.$.contentWindow.document);
            N.on('keydown', G, this, null, 0);
        });
        this.on('show', function () {
            var P = this;
            if (!P._.hasFocus) {
                P._.currentFocusIndex = -1;
                F(true);
                if (P._.editor.mode == 'wysiwyg' && CKEDITOR.env.ie) {
                    var M = y.document.$.selection,
                        N = M.createRange();
                    if (N) if (N.parentElement && N.parentElement().ownerDocument == y.document.$ || N.item && N.item(0).ownerDocument == y.document.$) {
                        var O = document.body.createTextRange();
                        O.moveToElementText(P.getElement().getFirst().$);
                        O.collapse(true);
                        O.select();
                    }
                }
            }
        }, this, null, 4294967295);
        if (CKEDITOR.env.ie6Compat) this.on('load', function (M) {
            var N = this.getElement(),
                O = N.getFirst();
            O.remove();
            O.appendTo(N);
        }, this);
        l(this);
        m(this);
        new CKEDITOR.dom.text(A.title, CKEDITOR.document).appendTo(this.parts.title);
        for (var H = 0; H < A.contents.length; H++) this.addPage(A.contents[H]);
        var I = /cke_dialog_tab(\s|$|_)/,
            J = /cke_dialog_tab(\s|$)/;
        this.parts.tabs.on('click', function (M) {
            var R = this;
            var N = M.data.getTarget(),
                O = N,
                P, Q;
            if (!(I.test(N.$.className) || N.getName() == 'a')) return;
            P = N.$.id.substr(0, N.$.id.lastIndexOf('_'));
            R.selectPage(P);
            if (R._.tabBarMode) {
                R._.tabBarMode = false;
                R._.currentFocusIndex = -1;
                F(true);
            }
            M.data.preventDefault();
        }, this);
        var K = [],
            L = CKEDITOR.dialog._.uiElementBuilders.hbox.build(this, {
            type: 'hbox',
            className: 'cke_dialog_footer_buttons',
            widths: [],
            children: A.buttons
        }, K).getChild();
        this.parts.footer.setHtml(K.join(''));
        for (H = 0; H < L.length; H++) this._.buttons[L[H].id] = L[H];
        CKEDITOR.skins.load(y, 'dialog');
    };

    function e(y, z, A) {
        this.element = z;
        this.focusIndex = A;
        this.isFocusable = function () {
            return true;
        };
        this.focus = function () {
            y._.currentFocusIndex = this.focusIndex;
            this.element.focus();
        };
        z.on('keydown', function (B) {
            if (B.data.getKeystroke() in {
                32: 1,
                13: 1
            }) this.fire('click');
        });
        z.on('focus', function () {
            this.fire('mouseover');
        });
        z.on('blur', function () {
            this.fire('mouseout');
        });
    };
    CKEDITOR.dialog.prototype = {
        resize: (function () {
            return function (y, z) {
                var A = this;
                if (A._.contentSize && A._.contentSize.width == y && A._.contentSize.height == z) return;
                CKEDITOR.dialog.fire('resize', {
                    dialog: A,
                    skin: A._.editor.skinName,
                    width: y,
                    height: z
                }, A._.editor);
                A._.contentSize = {
                    width: y,
                    height: z
                };
                A._.updateSize = true;
            };
        })(),
        getSize: function () {
            var A = this;
            if (!A._.updateSize) return A._.size;
            var y = A._.element.getFirst(),
                z = A._.size = {
                width: y.$.offsetWidth || 0,
                height: y.$.offsetHeight || 0
            };
            A._.updateSize = !z.width || !z.height;
            return z;
        },
        move: (function () {
            var y;
            return function (z, A) {
                var D = this;
                var B = D._.element.getFirst();
                if (y === undefined) y = B.getComputedStyle('position') == 'fixed';
                if (y && D._.position && D._.position.x == z && D._.position.y == A) return;
                D._.position = {
                    x: z,
                    y: A
                };
                if (!y) {
                    var C = CKEDITOR.document.getWindow().getScrollPosition();
                    z += C.x;
                    A += C.y;
                }
                B.setStyles({
                    left: (z > 0 ? z : 0) + ('px'),
                    top: (A > 0 ? A : 0) + ('px')
                });
            };
        })(),
        getPosition: function () {
            return CKEDITOR.tools.extend({}, this._.position);
        },
        show: function () {
            if (this._.editor.mode == 'wysiwyg' && CKEDITOR.env.ie) this._.editor.getSelection().lock();
            var y = this._.element,
                z = this.definition;
            if (!(y.getParent() && y.getParent().equals(CKEDITOR.document.getBody()))) y.appendTo(CKEDITOR.document.getBody());
            else return;
            if (CKEDITOR.env.gecko && CKEDITOR.env.version < 10900) {
                var A = this.parts.dialog;
                A.setStyle('position', 'absolute');
                setTimeout(function () {
                    A.setStyle('position', 'fixed');
                }, 0);
            }
            this.resize(z.minWidth, z.minHeight);
            this.selectPage(this.definition.contents[0].id);
            this.reset();
            if (CKEDITOR.dialog._.currentZIndex === null) CKEDITOR.dialog._.currentZIndex = this._.editor.config.baseFloatZIndex;
            this._.element.getFirst().setStyle('z-index', CKEDITOR.dialog._.currentZIndex += 10);
            if (CKEDITOR.dialog._.currentTop === null) {
                CKEDITOR.dialog._.currentTop = this;
                this._.parentDialog = null;
                p(this._.editor);
                CKEDITOR.document.on('keydown', s);
                CKEDITOR.document.on('keyup', t);
            }
            else {
                this._.parentDialog = CKEDITOR.dialog._.currentTop;
                var B = this._.parentDialog.getElement().getFirst();
                B.$.style.zIndex -= Math.floor(this._.editor.config.baseFloatZIndex / 2);
                CKEDITOR.dialog._.currentTop = this;
            }
            u(this, this, '\x1b', null, function () {
                this.getButton('cancel') && this.getButton('cancel').click();
            });
            this._.hasFocus = false;
            CKEDITOR.tools.setTimeout(function () {
                var C = CKEDITOR.document.getWindow().getViewPaneSize(),
                    D = this.getSize();
                this.move((C.width - z.minWidth) / (2), (C.height - D.height) / (2));
                this.parts.dialog.setStyle('visibility', '');
                this.fireOnce('load', {});
                this.fire('show', {});
                this.foreach(function (E) {
                    E.setInitValue && E.setInitValue();
                });
            }, 100, this);
        },
        foreach: function (y) {
            var B = this;
            for (var z in B._.contents)
            for (var A in B._.contents[z]) y(B._.contents[z][A]);
            return B;
        },
        reset: (function () {
            var y = function (z) {
                if (z.reset) z.reset();
            };
            return function () {
                this.foreach(y);
                return this;
            };
        })(),
        setupContent: function () {
            var y = arguments;
            this.foreach(function (z) {
                if (z.setup) z.setup.apply(z, y);
            });
        },
        commitContent: function () {
            var y = arguments;
            this.foreach(function (z) {
                if (z.commit) z.commit.apply(z, y);
            });
        },
        hide: function () {
            this.fire('hide', {});
            var y = this._.element;
            if (!y.getParent()) return;
            y.remove();
            this.parts.dialog.setStyle('visibility', 'hidden');
            v(this);
            if (!this._.parentDialog) q();
            else {
                var z = this._.parentDialog.getElement().getFirst();
                z.setStyle('z-index', parseInt(z.$.style.zIndex, 10) + Math.floor(this._.editor.config.baseFloatZIndex / 2));
            }
            CKEDITOR.dialog._.currentTop = this._.parentDialog;
            if (!this._.parentDialog) {
                CKEDITOR.dialog._.currentZIndex = null;
                CKEDITOR.document.removeListener('keydown', s);
                CKEDITOR.document.removeListener('keyup', t);
                var A = this._.editor;
                A.focus();
                if (A.mode == 'wysiwyg' && CKEDITOR.env.ie) A.getSelection().unlock(true);
            }
            else CKEDITOR.dialog._.currentZIndex -= 10;
            this.foreach(function (B) {
                B.resetInitValue && B.resetInitValue();
            });
        },
        addPage: function (y) {
            var I = this;
            var z = [],
                A = y.label ? ' title="' + CKEDITOR.tools.htmlEncode(y.label) + '"' : '',
                B = y.elements,
                C = CKEDITOR.dialog._.uiElementBuilders.vbox.build(I, {
                type: 'vbox',
                className: 'cke_dialog_page_contents',
                children: y.elements,
                expand: !! y.expand,
                padding: y.padding,
                style: y.style || 'width: 100%; height: 100%;'
            }, z),
                D = CKEDITOR.dom.element.createFromHtml(z.join('')),
                E = CKEDITOR.dom.element.createFromHtml(['<a class="cke_dialog_tab"', I._.pageCount > 0 ? ' cke_last' : 'cke_first', A, !! y.hidden ? ' style="display:none"' : '', ' id="', y.id + '_', CKEDITOR.tools.getNextNumber(), '" href="javascript:void(0)"', ' hidefocus="true">', y.label, '</a>'].join(''));
            if (I._.pageCount === 0) I.parts.dialog.addClass('cke_single_page');
            else I.parts.dialog.removeClass('cke_single_page');
            I._.tabs[y.id] = [E, D];
            I._.tabIdList.push(y.id);
            I._.pageCount++;
            I._.lastTab = E;
            var F = I._.contents[y.id] = {},
                G, H = C.getChild();
            while (G = H.shift()) {
                F[G.id] = G;
                if (typeof G.getChild == 'function') H.push.apply(H, G.getChild());
            }
            D.setAttribute('name', y.id);
            D.appendTo(I.parts.contents);
            E.unselectable();
            I.parts.tabs.append(E);
            if (y.accessKey) {
                u(I, I, 'CTRL+' + y.accessKey, x, w);
                I._.accessKeyMap['CTRL+' + y.accessKey] = y.id;
            }
        },
        selectPage: function (y) {
            var D = this;
            for (var z in D._.tabs) {
                var A = D._.tabs[z][0],
                    B = D._.tabs[z][1];
                if (z != y) {
                    A.removeClass('cke_dialog_tab_selected');
                    B.hide();
                }
            }
            var C = D._.tabs[y];
            C[0].addClass('cke_dialog_tab_selected');
            C[1].show();
            D._.currentTabId = y;
            D._.currentTabIndex = CKEDITOR.tools.indexOf(D._.tabIdList, y);
        },
        hidePage: function (y) {
            var z = this._.tabs[y] && this._.tabs[y][0];
            if (!z) return;
            z.hide();
        },
        showPage: function (y) {
            var z = this._.tabs[y] && this._.tabs[y][0];
            if (!z) return;
            z.show();
        },
        getElement: function () {
            return this._.element;
        },
        getName: function () {
            return this._.name;
        },
        getContentElement: function (y, z) {
            return this._.contents[y][z];
        },
        
        getValueOf: function (y, z) {
            return this.getContentElement(y, z).getValue();
        },
        setValueOf: function (y, z, A) {
            return this.getContentElement(y, z).setValue(A);
        },
        getButton: function (y) {
            return this._.buttons[y];
        },
        click: function (y) {
            return this._.buttons[y].click();
        },
        disableButton: function (y) {
            return this._.buttons[y].disable();
        },
        enableButton: function (y) {
            return this._.buttons[y].enable();
        },
        getPageCount: function () {
            return this._.pageCount;
        },
        getParentEditor: function () {
            return this._.editor;
        },
        getSelectedElement: function () {
            return this.getParentEditor().getSelection().getSelectedElement();
        },
        addFocusable: function (y, z) {
            var B = this;
            if (typeof z == 'undefined') {
                z = B._.focusList.length;
                B._.focusList.push(new e(B, y, z));
            }
            else {
                B._.focusList.splice(z, 0, new e(B, y, z));
                for (var A = z + 1; A < B._.focusList.length; A++) B._.focusList[A].focusIndex++;
            }
        }
    };
    

    CKEDITOR.tools.extend(CKEDITOR.dialog, {
        add: function (y, z) {
            if (!this._.dialogDefinitions[y] || typeof z == 'function') this._.dialogDefinitions[y] = z;
        },
        exists: function (y) {
            return !!this._.dialogDefinitions[y];
        },
        getCurrent: function () {
            return CKEDITOR.dialog._.currentTop;
        },
        okButton: (function () {
            var y = function (z, A) {
                A = A || {};
                return
                CKEDITOR.tools.extend({
                    id: 'ok',
                    type: 'button',
                    label: z.lang.common.ok,
                    'class': 'cke_dialog_ui_button_ok',
                    onClick: function (B) {
                        var C = B.data.dialog;
                        if (C.fire('ok', {
                            hide: true
                        }).hide !== false) C.hide();
                    }
                }, A, true);
            };
            y.type = 'button';
            y.override = function (z) {
                return CKEDITOR.tools.extend(function (A) {
                    return y(A, z);
                }, {
                    type: 'button'
                }, true);
            };
            return y;
        })(),
        cancelButton: (function () {
            var y = function (z, A) {
                A = A || {};
                return CKEDITOR.tools.extend({
                    id: 'cancel',
                    type: 'button',
                    label: z.lang.common.cancel,
                    'class': 'cke_dialog_ui_button_cancel',
                    onClick: function (B) {
                        var C = B.data.dialog;
                        if (C.fire('cancel', {
                            hide: true
                        }).hide !== false) C.hide();
                    }
                }, A, true);
            };
            y.type = 'button';
            y.override = function (z) {
                return CKEDITOR.tools.extend(function (A) {
                    return y(A, z);
                }, {
                    type: 'button'
                }, true);
            };
            return y;
        })(),
        addUIElement: function (y, z) {
            this._.uiElementBuilders[y] = z;
        }
    });
    CKEDITOR.dialog._ = {
        uiElementBuilders: {},
        dialogDefinitions: {},
        currentTop: null,
        currentZIndex: null
    };
    CKEDITOR.event.implementOn(CKEDITOR.dialog);
    CKEDITOR.event.implementOn(CKEDITOR.dialog.prototype, true);
    var f = {
        resizable: CKEDITOR.DIALOG_RESIZE_NONE,
        minWidth: 600,
        minHeight: 400,
        buttons: [CKEDITOR.dialog.okButton, CKEDITOR.dialog.cancelButton]
    },
        g = function (y, z, A) {
        for (var B = 0, C; C = y[B]; B++) {
            if (C.id == z) return C;
            if (A && C[A]) {
                var D = g(C[A], z, A);
                if (D) return D;
            }
        }
        return null;
    },
        h = function (y, z, A, B, C) {
        if (A) {
            for (var D = 0, E; E = y[D];
            D++) {
                if (E.id == A) {
                    y.splice(D, 0, z);
                    return z;
                }
                if (B && E[B]) {
                    var F = h(E[B], z, A, B, true);
                    if (F) return F;
                }
            }
            if (C) return null;
        }
        y.push(z);
        return z;
    },
        i = function (y, z, A) {
        for (var B = 0, C; C = y[B]; B++) {
            if (C.id == z) return y.splice(B, 1);
            if (A && C[A]) {
                var D = i(C[A], z, A);
                if (D) return D;
            }
        }
        return null;
    },
        j = function (y, z) {
        this.dialog = y;
        var A = z.contents;
        for (var B = 0, C;
        C = A[B];
        B++) A[B] = new k(y, C);
        CKEDITOR.tools.extend(this, z);
    };
    j.prototype = {
        getContents: function (y) {
            return g(this.contents, y);
        },
        getButton: function (y) {
            return g(this.buttons, y);
        },
        addContents: function (y, z) {
            return h(this.contents, y, z);
        },
        addButton: function (y, z) {
            return h(this.buttons, y, z);
        },
        removeContents: function (y) {
            i(this.contents, y);
        },
        removeButton: function (y) {
            i(this.buttons, y);
        }
    };

    function k(y, z) {
        this._ = {
            dialog: y
        };
        CKEDITOR.tools.extend(this, z);
    };
    k.prototype = {
        get: function (y) {
            return g(this.elements, y, 'children');
        },
        add: function (y, z) {
            return h(this.elements, y, z, 'children');
        },
        remove: function (y) {
            i(this.elements, y, 'children');
        }
    };

    function l(y) {
        var z = null,
            A = null,
            B = y.getElement().getFirst(),
            C = y.getParentEditor(),
            D = C.config.dialog_magnetDistance,
            E = d[C.skinName].margins || [0, 0, 0, 0];

        function F(H) {
            var I = y.getSize(),
                J = CKEDITOR.document.getWindow().getViewPaneSize(),
                K = H.data.$.screenX,
                L = H.data.$.screenY,
                M = K - z.x,
                N = L - z.y,
                O, P;
            z = {
                x: K,
                y: L
            };
            A.x += M;
            A.y += N;
            if (A.x + E[3] < D) O = -E[3];
            else if (A.x - E[1] > J.width - I.width - D) O = J.width - I.width + E[1];
            else O = A.x;
            if (A.y + E[0] < D) P = -E[0];
            else if (A.y - E[2] > J.height - I.height - D) P = J.height - I.height + E[2];
            else P = A.y;
            y.move(O, P);
            H.data.preventDefault();
        };

        function G(H) {
            CKEDITOR.document.removeListener('mousemove', F);
            CKEDITOR.document.removeListener('mouseup', G);
            if (CKEDITOR.env.ie6Compat) {
                var I = o.getChild(0).getFrameDocument();
                I.removeListener('mousemove', F);
                I.removeListener('mouseup', G);
            }
        };
        y.parts.title.on('mousedown', function (H) {
            y._.updateSize = true;
            z = {
                x: H.data.$.screenX,
                y: H.data.$.screenY
            };
            CKEDITOR.document.on('mousemove', F);
            CKEDITOR.document.on('mouseup', G);
            A = y.getPosition();
            if (CKEDITOR.env.ie6Compat) {
                var I = o.getChild(0).getFrameDocument();
                I.on('mousemove', F);
                I.on('mouseup', G);
            }
            H.data.preventDefault();
        }, y);
    };

    function m(y) {
        var z = y.definition,
            A = z.minWidth || 0,
            B = z.minHeight || 0,
            C = z.resizable,
            D = d[y.getParentEditor().skinName].margins || [0, 0, 0, 0];

        function E(P, Q) {
            P.y += Q;
        };

        function F(P, Q) {
            P.x2 += Q;
        };

        function G(P, Q) {
            P.y2 += Q;
        };

        function H(P, Q) {
            P.x += Q;
        };
        var I = null,
            J = null,
            K = y._.editor.config.magnetDistance,
            L = ['tl', 't', 'tr', 'l', 'r', 'bl', 'b', 'br'];

        function M(P) {
            var Q = P.listenerData.part,
                R = y.getSize();
            J = y.getPosition();
            CKEDITOR.tools.extend(J, {
                x2: J.x + R.width,
                y2: J.y + R.height
            });
            I = {
                x: P.data.$.screenX,
                y: P.data.$.screenY
            };
            CKEDITOR.document.on('mousemove', N, y, {
                part: Q
            });
            CKEDITOR.document.on('mouseup', O, y, {
                part: Q
            });
            if (CKEDITOR.env.ie6Compat) {
                var S = o.getChild(0).getFrameDocument();
                S.on('mousemove', N, y, {
                    part: Q
                });
                S.on('mouseup', O, y, {
                    part: Q
                });
            }
            P.data.preventDefault();
        };

        function N(P) {
            var Q = P.data.$.screenX,
                R = P.data.$.screenY,
                S = Q - I.x,
                T = R - I.y,
                U = CKEDITOR.document.getWindow().getViewPaneSize(),
                V = P.listenerData.part;
            if (V.search('t') != - 1) E(J, T);
            if (V.search('l') != - 1) H(J, S);
            if (V.search('b') != - 1) G(J, T);
            if (V.search('r') != - 1) F(J, S);
            I = {
                x: Q,
                y: R
            };
            var W, X, Y, Z;
            if (J.x + D[3] < K) W = -D[3];
            else if (V.search('l') != - 1 && J.x2 - J.x < A + K) W = J.x2 - A;
            else W = J.x;
            if (J.y + D[0] < K) X = -D[0];
            else if (V.search('t') != - 1 && J.y2 - J.y < B + K) X = J.y2 - B;
            else X = J.y;
            if (J.x2 - D[1] > U.width - K) Y = U.width + D[1];
            else if (V.search('r') != - 1 && J.x2 - J.x < A + K) Y = J.x + A;
            else Y = J.x2;
            if (J.y2 - D[2] > U.height - K) Z = U.height + D[2];
            else if (V.search('b') != - 1 && J.y2 - J.y < B + K) Z = J.y + B;
            else Z = J.y2;
            y.move(W, X);
            y.resize(Y - W, Z - X);
            P.data.preventDefault();
        };

        function O(P) {
            CKEDITOR.document.removeListener('mouseup', O);
            CKEDITOR.document.removeListener('mousemove', N);
            if (CKEDITOR.env.ie6Compat) {
                var Q = o.getChild(0).getFrameDocument();
                Q.removeListener('mouseup', O);
                Q.removeListener('mousemove', N);
            }
        };
    };
    var n, o, p = function (y) {
        var z = CKEDITOR.document.getWindow();
        if (!o) {
            var A = ['<div style="position: ', CKEDITOR.env.ie6Compat ? 'absolute' : 'fixed', '; z-index: ', y.config.baseFloatZIndex, '; top: 0px; left: 0px; ', 'background-color: ', y.config.dialog_backgroundCoverColor, '" id="cke_dialog_background_cover">'];
            if (CKEDITOR.env.ie6Compat) {
                var B = CKEDITOR.env.isCustomDomain();
                A.push('<iframe hidefocus="true" frameborder="0" id="cke_dialog_background_iframe" src="javascript:');
                A.push(B ? "void((function(){document.open();document.domain='" + document.domain + "';" + 'document.close();' + '})())' : "''");
                A.push('" style="position:absolute;left:0;top:0;width:100%;height: 100%;progid:DXImageTransform.Microsoft.Alpha(opacity=0)"></iframe>');
            }
            A.push('</div>');
            o = CKEDITOR.dom.element.createFromHtml(A.join(''));
        }
        var C = o,
            D = function () {
            var G = z.getViewPaneSize();
            C.setStyles({
                width: G.width + 'px',
                height: G.height + 'px'
            });
        },
            E = function () {
            var G = z.getScrollPosition(),
                H = CKEDITOR.dialog._.currentTop;
            C.setStyles({
                left: G.x + 'px',
                top: G.y + 'px'
            });
            do {
                var I = H.getPosition();
                H.move(I.x, I.y);
            } while (H = H._.parentDialog)
        };
        n = D;
        z.on('resize', D);
        D();
        if (CKEDITOR.env.ie6Compat) {
            var F = function () {
                E();
                arguments.callee.prevScrollHandler.apply(this, arguments);
            };
            z.$.setTimeout(function () {
                F.prevScrollHandler = window.onscroll || (function () {});
                window.onscroll = F;
            }, 0);
            E();
        }
        C.setOpacity(y.config.dialog_backgroundCoverOpacity);
        C.appendTo(CKEDITOR.document.getBody());
    },
        q = function () {
        if (!o) return;
        var y = CKEDITOR.document.getWindow();
        o.remove();
        y.removeListener('resize', n);
        if (CKEDITOR.env.ie6Compat) y.$.setTimeout(function () {
            var z = window.onscroll && window.onscroll.prevScrollHandler;
            window.onscroll = z || null;
        }, 0);
        n = null;
    },
        r = {},
        s = function (y) {
        var z = y.data.$.ctrlKey || y.data.$.metaKey,
            A = y.data.$.altKey,
            B = y.data.$.shiftKey,
            C = String.fromCharCode(y.data.$.keyCode),
            D = r[(z ? 'CTRL+' : '') + (A ? 'ALT+' : '') + (B ? 'SHIFT+' : '') + C];
        if (!D || !D.length) return;
        D = D[D.length - 1];
        D.keydown && D.keydown.call(D.uiElement, D.dialog, D.key);
        y.data.preventDefault();
    },
        t = function (y) {
        var z = y.data.$.ctrlKey || y.data.$.metaKey,
            A = y.data.$.altKey,
            B = y.data.$.shiftKey,
            C = String.fromCharCode(y.data.$.keyCode),
            D = r[(z ? 'CTRL+' : '') + (A ? 'ALT+' : '') + (B ? 'SHIFT+' : '') + C];
        if (!D || !D.length) return;
        D = D[D.length - 1];
        D.keyup && D.keyup.call(D.uiElement, D.dialog, D.key);
        y.data.preventDefault();
    },
        u = function (y, z, A, B, C) {
        var D = r[A] || (r[A] = []);
        D.push({
            uiElement: y,
            dialog: z,
            key: A,
            keyup: C || y.accessKeyUp,
            keydown: B || y.accessKeyDown
        });
    },
        v = function (y) {
        for (var z in r) {
            var A = r[z];
            for (var B = A.length - 1; B >= 0; B--) if (A[B].dialog == y || A[B].uiElement == y) A.splice(B, 1);
            if (A.length === 0) delete r[z];
        }
    },
        w = function (y, z) {
        if (y._.accessKeyMap[z]) y.selectPage(y._.accessKeyMap[z]);
    },
        x = function (y, z) {};
    (function () {
        CKEDITOR.ui.dialog = {
            uiElement: function (y, z, A, B, C, D, E) {
                if (arguments.length < 4) return;
                var F = (B.call ? B(z) : B) || ('div'),
                    G = ['<', F, ' '],
                    H = (C && C.call ? C(z) : C) || ({}),
                    I = (D && D.call ? D(z) : D) || ({}),
                    J = (E && E.call ? E(y, z) : E) || (''),
                    K = this.domId = I.id || CKEDITOR.tools.getNextNumber() + '_uiElement',
                    L = this.id = z.id,
                    M;
                I.id = K;
                var N = {};
                if (z.type) N['cke_dialog_ui_' + z.type] = 1;
                if (z.className) N[z.className] = 1;
                var O = I['class'] && I['class'].split ? I['class'].split(' ') : [];
                for (M = 0; M < O.length; M++) if (O[M]) N[O[M]] = 1;
                var P = [];
                for (M in N) P.push(M);
                I['class'] = P.join(' ');
                if (z.title) I.title = z.title;
                var Q = (z.style || '').split(';');
                for (M in H) Q.push(M + ':' + H[M]);
                if (z.hidden) Q.push('display:none');
                for (M = Q.length - 1; M >= 0; M--) if (Q[M] === '') Q.splice(M, 1);
                if (Q.length > 0) I.style = (I.style ? I.style + '; ' : '') + (Q.join('; '));
                for (M in I) G.push(M + '="' + CKEDITOR.tools.htmlEncode(I[M]) + '" ');
                G.push('>', J, '</', F, '>');
                A.push(G.join(''));
                (this._ || (this._ = {})).dialog = y;
                if (typeof z.isChanged == 'boolean') this.isChanged = function () {
                    return z.isChanged;
                };
                if (typeof z.isChanged == 'function') this.isChanged = z.isChanged;
                CKEDITOR.event.implementOn(this);
                this.registerEvents(z);
                if (this.accessKeyUp && this.accessKeyDown && z.accessKey) u(this, y, 'CTRL+' + z.accessKey);
                var R = this;
                y.on('load', function () {
                    if (R.getInputElement()) R.getInputElement().on('focus', function () {
                        y._.tabBarMode = false;
                        y._.hasFocus = true;
                        R.fire('focus');
                    }, R);
                });
                if (this.keyboardFocusable) {
                    this.focusIndex = y._.focusList.push(this) - 1;
                    this.on('focus', function () {
                        y._.currentFocusIndex = R.focusIndex;
                    });
                }
                CKEDITOR.tools.extend(this, z);
            },
            hbox: function (y, z, A, B, C) {
                if (arguments.length < 4) return;
                this._ || (this._ = {});
                var D = this._.children = z,
                    E = C && C.widths || null,
                    F = C && C.height || null,
                    G = {},
                    H, I = function () {
                    var J = ['<tbody><tr class="cke_dialog_ui_hbox">'];
                    for (H = 0; H < A.length; H++) {
                        var K = 'cke_dialog_ui_hbox_child',
                            L = [];
                        if (H === 0) K = 'cke_dialog_ui_hbox_first';
                        if (H == A.length - 1) K = 'cke_dialog_ui_hbox_last';
                        J.push('<td class="', K, '" ');
                        if (E) {
                            if (E[H]) L.push('width:' + CKEDITOR.tools.cssLength(E[H]));
                        } else L.push('width:' + Math.floor(100 / A.length) + '%');
                        if (F) L.push('height:' + CKEDITOR.tools.cssLength(F));
                        if (C && C.padding != undefined) L.push('padding:' + CKEDITOR.tools.cssLength(C.padding));
                        if (L.length > 0) J.push('style="' + L.join('; ') + '" ');
                        J.push('>', A[H], '</td>');
                    }
                    J.push('</tr></tbody>');
                    return J.join('');
                };
                CKEDITOR.ui.dialog.uiElement.call(this, y, C || {
                    type: 'hbox'
                }, B, 'table', G, C && C.align && {
                    align: C.align
                } || null, I);
            },
            vbox: function (y, z, A, B, C) {
                if (arguments.length < 3) return;
                this._ || (this._ = {});
                var D = this._.children = z,
                    E = C && C.width || null,
                    F = C && C.heights || null,
                    G = function () {
                    var H = ['<table cellspacing="0" border="0" '];
                    H.push('style="');
                    if (C && C.expand) H.push('height:100%;');
                    H.push('width:' + CKEDITOR.tools.cssLength(E || '100%'), ';');
                    H.push('"');
                    H.push('align="', CKEDITOR.tools.htmlEncode(C && C.align || (y.getParentEditor().lang.dir == 'ltr' ? 'left' : 'right')), '" ');
                    H.push('><tbody>');
                    for (var I = 0; I < A.length; I++) {
                        var J = [];
                        H.push('<tr><td ');
                        if (E) J.push('width:' + CKEDITOR.tools.cssLength(E || '100%'));
                        if (F) J.push('height:' + CKEDITOR.tools.cssLength(F[I]));
                        else if (C && C.expand) J.push('height:' + Math.floor(100 / A.length) + '%');
                        if (C && C.padding != undefined) J.push('padding:' + CKEDITOR.tools.cssLength(C.padding));
                        if (J.length > 0) H.push('style="', J.join('; '), '" ');
                        H.push(' class="cke_dialog_ui_vbox_child">', A[I], '</td></tr>');
                    }
                    H.push('</tbody></table>');
                    return H.join('');
                };
                CKEDITOR.ui.dialog.uiElement.call(this, y, C || {
                    type: 'vbox'
                }, B, 'div', null, null, G);
            }
        };
    })();
    CKEDITOR.ui.dialog.uiElement.prototype = {
        getElement: function () {
            return CKEDITOR.document.getById(this.domId);
        },
        getInputElement: function () {
            return this.getElement();
        },
        getDialog: function () {
            return this._.dialog;
        },
        setValue: function (y) {
            this.getInputElement().setValue(y);
            this.fire('change', {
                value: y
            });
            return this;
        },
        getValue: function () {
            return this.getInputElement().getValue();
        },
        isChanged: function () {
            return false;
        },
        selectParentTab: function () {
            var B = this;
            var y = B.getInputElement(),
                z = y,
                A;
            while ((z = z.getParent()) && (z.$.className.search('cke_dialog_page_contents') == - 1)) {}
            if (!z) return B;
            A = z.getAttribute('name');
            if (B._.dialog._.currentTabId != A) B._.dialog.selectPage(A);
            return B;
        },
        focus: function () {
            this.selectParentTab().getInputElement().focus();
            return this;
        },
        registerEvents: function (y) {
            var z = /^on([A-Z]\w+)/,
                A, B = function (D, E, F, G) {
                E.on('load', function () {
                    D.getInputElement().on(F, G, D);
                });
            };
            for (var C in y) {
                if (!(A = C.match(z))) continue;
                if (this.eventProcessors[C]) this.eventProcessors[C].call(this, this._.dialog, y[C]);
                else B(this, this._.dialog, A[1].toLowerCase(), y[C]);
            }
            return this;
        },
        eventProcessors: {
            onLoad: function (y, z) {
                y.on('load', z, this);
            },
            onShow: function (y, z) {
                y.on('show', z, this);
            },
            onHide: function (y, z) {
                y.on('hide', z, this);
            }
        },
        accessKeyDown: function (y, z) {
            this.focus();
        },
        accessKeyUp: function (y, z) {},
        disable: function () {
            var y = this.getInputElement();
            y.setAttribute('disabled', 'true');
            y.addClass('cke_disabled');
        },
        enable: function () {
            var y = this.getInputElement();
            y.removeAttribute('disabled');
            y.removeClass('cke_disabled');
        },
        isEnabled: function () {
            return !this.getInputElement().getAttribute('disabled');
        },
        isVisible: function () {
            return !!this.getInputElement().$.offsetHeight;
        },
        isFocusable: function () {
            if (!this.isEnabled() || !this.isVisible()) return false;
            return true;
        }
    };
    CKEDITOR.ui.dialog.hbox.prototype = CKEDITOR.tools.extend(new CKEDITOR.ui.dialog.uiElement(), {
        getChild: function (y) {
            var z = this;
            if (arguments.length < 1) return z._.children.concat();
            if (!y.splice) y = [y];
            if (y.length < 2) return z._.children[y[0]];
            else return z._.children[y[0]] && z._.children[y[0]].getChild ? z._.children[y[0]].getChild(y.slice(1, y.length)) : null;
        }
    }, true);
    CKEDITOR.ui.dialog.vbox.prototype = new CKEDITOR.ui.dialog.hbox();
    (function () {
        var y = {
            build: function (z, A, B) {
                var C = A.children,
                    D, E = [],
                    F = [];
                for (var G = 0; G < C.length && (D = C[G]);
                G++) {
                    var H = [];
                    E.push(H);
                    F.push(CKEDITOR.dialog._.uiElementBuilders[D.type].build(z, D, H));
                }
                return new CKEDITOR.ui.dialog[A.type](z, F, E, B, A);
            }
        };
        CKEDITOR.dialog.addUIElement('hbox', y);
        CKEDITOR.dialog.addUIElement('vbox', y);
    })();
    CKEDITOR.dialogCommand = function (y) {
        this.dialogName = y;
    };
    CKEDITOR.dialogCommand.prototype = {
        exec: function (y) {
            y.openDialog(this.dialogName);
        },
        canUndo: false
    };
    (function () {
        var y = /^([a]|[^a])+$/,
            z = /^\d*$/,
            A = /^\d*(?:\.\d+)?$/;
        CKEDITOR.VALIDATE_OR = 1;
        CKEDITOR.VALIDATE_AND = 2;
        CKEDITOR.dialog.validate = {
            functions: function () {
                return function () {
                    var H = this;
                    var B = H && H.getValue ? H.getValue() : arguments[0],
                        C = undefined,
                        D = CKEDITOR.VALIDATE_AND,
                        E = [],
                        F;
                    for (F = 0; F < arguments.length; F++) if (typeof arguments[F] == 'function') E.push(arguments[F]);
                    else break;
                    if (F < arguments.length && typeof arguments[F] == 'string') {
                        C = arguments[F];
                        F++;
                    }
                    if (F < arguments.length && typeof arguments[F] == 'number') D = arguments[F];
                    var G = D == CKEDITOR.VALIDATE_AND ? true : false;
                    for (F = 0; F < E.length; F++) if (D == CKEDITOR.VALIDATE_AND) G = G && E[F](B);
                    else G = G || E[F](B);
                    if (!G) {
                        if (C !== undefined) alert(C);
                        if (H && (H.select || H.focus)) H.select || H.focus();
                        return false;
                    }
                    return true;
                };
            },
            regex: function (B, C) {
                return function () {
                    var E = this;
                    var D = E && E.getValue ? E.getValue() : arguments[0];
                    if (!B.test(D)) {
                        if (C !== undefined) alert(C);
                        if (E && (E.select || E.focus)) if (E.select) E.select();
                        else E.focus();
                        return false;
                    }
                    return true;
                };
            },
            notEmpty: function (B) {
                return this.regex(y, B);
            },
            integer: function (B) {
                return this.regex(z, B);
            },
            number: function (B) {
                return this.regex(A, B);
            },
            equals: function (B, C) {
                return this.functions(function (D) {
                    return D == B;
                }, C);
            },
            notEqual: function (B, C) {
                return this.functions(function (D) {
                    return D != B;
                }, C);
            }
        };
    })();
    CKEDITOR.skins.add = (function () {
        var y = CKEDITOR.skins.add;
        return function (z, A) {
            d[z] = {
                margins: A.margins
            };
            return y.apply(this, arguments);
        };
    })();
})();
CKEDITOR.tools.extend(CKEDITOR.editor.prototype, {
    openDialog: function (a) {
        var b = CKEDITOR.dialog._.dialogDefinitions[a];
        if (typeof b == 'function') {
            var c = this._.storedDialogs || (this._.storedDialogs = {}),
                d = c[a] || (c[a] = new CKEDITOR.dialog(this, a));
            d.show();
            return d;
        } else if (b == 'failed') throw new Error('[CKEDITOR.dialog.openDialog] Dialog "' + a + '" failed when loading definition.');
        var e = CKEDITOR.document.getBody(),
            f = e.$.style.cursor,
            g = this;
        e.setStyle('cursor', 'wait');
        CKEDITOR.scriptLoader.load(CKEDITOR.getUrl(b), function () {
            if (typeof CKEDITOR.dialog._.dialogDefinitions[a] != 'function') CKEDITOR.dialog._.dialogDefinitions[a] = 'failed';
            g.openDialog(a);
            e.setStyle('cursor', f);
        });
        return null;
    }
});
CKEDITOR.config.dialog_backgroundCoverColor = 'white';
CKEDITOR.config.dialog_backgroundCoverOpacity = 0.5;
CKEDITOR.config.dialog_magnetDistance = 20;