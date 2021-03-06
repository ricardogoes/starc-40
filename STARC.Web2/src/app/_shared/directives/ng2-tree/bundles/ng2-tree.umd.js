!function(e){function t(e){Object.defineProperty(this,e,{enumerable:!0,get:function(){return this[v][e]}})}function r(e){if("undefined"!=typeof System&&System.isModule?System.isModule(e):"[object Module]"===Object.prototype.toString.call(e))return e;var t={default:e,__useDefault:e};if(e&&e.__esModule)for(var r in e)Object.hasOwnProperty.call(e,r)&&(t[r]=e[r]);return new o(t)}function o(e){Object.defineProperty(this,v,{value:e}),Object.keys(e).forEach(t,this)}function n(e){return"@node/"===e.substr(0,6)?c(e,r(m(e.substr(6))),{}):p[e]}function u(e){var t=n(e);if(!t)throw new Error('Module "'+e+'" expected, but not contained in build.');if(t.module)return t.module;var r=t.linkRecord;return i(t,r),a(t,r,[]),t.module}function i(e,t){if(!t.depLoads){t.declare&&d(e,t),t.depLoads=[];for(var r=0;r<t.deps.length;r++){var o=n(t.deps[r]);t.depLoads.push(o),o.linkRecord&&i(o,o.linkRecord);var u=t.setters&&t.setters[r];u&&(u(o.module||o.linkRecord.moduleObj),o.importerSetters.push(u))}return e}}function d(t,r){var o=r.moduleObj,n=t.importerSetters,u=!1,i=r.declare.call(e,function(e,t){if(!u){if("object"==typeof e)for(var r in e)"__useDefault"!==r&&(o[r]=e[r]);else o[e]=t;u=!0;for(var i=0;i<n.length;i++)n[i](o);return u=!1,t}},{id:t.key});"function"!=typeof i?(r.setters=i.setters,r.execute=i.execute):(r.setters=[],r.execute=i)}function l(e,t,r){return p[e]={key:e,module:void 0,importerSetters:[],linkRecord:{deps:t,depLoads:void 0,declare:r,setters:void 0,execute:void 0,moduleObj:{}}}}function f(e,t,r,o){var n={};return p[e]={key:e,module:void 0,importerSetters:[],linkRecord:{deps:t,depLoads:void 0,declare:void 0,execute:o,executingRequire:r,moduleObj:{default:n,__useDefault:n},setters:void 0}}}function s(e,t,r){return function(o){for(var n=0;n<e.length;n++)if(e[n]===o){var u,i=t[n],d=i.linkRecord;return u=d?-1===r.indexOf(i)?a(i,d,r):d.moduleObj:i.module,u.__useDefault||u}}}function a(t,r,n){if(n.push(t),t.module)return t.module;var u;if(r.setters){for(var i=0;i<r.deps.length;i++){var d=r.depLoads[i],l=d.linkRecord;l&&-1===n.indexOf(d)&&(u=a(d,l,l.setters?n:[]))}r.execute.call(y)}else{var f={id:t.key},c=r.moduleObj;Object.defineProperty(f,"exports",{configurable:!0,set:function(e){c.default=c.__useDefault=e},get:function(){return c.__useDefault}});var p=s(r.deps,r.depLoads,n);if(!r.executingRequire)for(var i=0;i<r.deps.length;i++)p(r.deps[i]);var v=r.execute.call(e,p,c.__useDefault,f);void 0!==v&&(c.default=c.__useDefault=v);var m=c.__useDefault;if(m&&m.__esModule)for(var b in m)Object.hasOwnProperty.call(m,b)&&(c[b]=m[b])}var f=t.module=new o(r.moduleObj);if(!r.setters)for(var i=0;i<t.importerSetters.length;i++)t.importerSetters[i](f);return f}function c(e,t){return p[e]={key:e,module:t,importerSetters:[],linkRecord:void 0}}var p={},v="undefined"!=typeof Symbol?Symbol():"@@baseObject";o.prototype=Object.create(null),"undefined"!=typeof Symbol&&Symbol.toStringTag&&(o.prototype[Symbol.toStringTag]="Module");var m="undefined"!=typeof System&&System._nodeRequire||"undefined"!=typeof require&&"undefined"!=typeof require.resolve&&"undefined"!=typeof process&&process.platform&&require,y={};return Object.freeze&&Object.freeze(y),function(e,t,n,i){return function(d){d(function(d){var s={_nodeRequire:m,register:l,registerDynamic:f,registry:{get:function(e){return p[e].module},set:c},newModule:function(e){return new o(e)}};c("@empty",new o({}));for(var a=0;a<t.length;a++)c(t[a],r(arguments[a],{}));i(s);var v=u(e[0]);if(e.length>1)for(var a=1;a<e.length;a++)u(e[a]);return n?v.__useDefault:(v instanceof o&&Object.defineProperty(v,"__esModule",{value:!0}),v)})}}}("undefined"!=typeof self?self:global)

(["a"], ["21","c","1f","11"], true, function($__System) {
var require = this.require, exports = this.exports, module = this.module;
$__System.registerDynamic("b", ["c", "d", "e"], true, function ($__require, exports, module) {
    "use strict";

    var global = this || self,
        GLOBAL = global;
    Object.defineProperty(exports, "__esModule", { value: true });
    var core_1 = $__require("c");
    var tree_service_1 = $__require("d");
    var tree_1 = $__require("e");
    var TreeComponent = function () {
        function TreeComponent(treeService) {
            this.treeService = treeService;
            this.nodeCreated = new core_1.EventEmitter();
            this.nodeRemoved = new core_1.EventEmitter();
            this.nodeRenamed = new core_1.EventEmitter();
            this.nodeSelected = new core_1.EventEmitter();
            this.nodeMoved = new core_1.EventEmitter();
            this.nodeExpanded = new core_1.EventEmitter();
            this.nodeCollapsed = new core_1.EventEmitter();
        }
        TreeComponent.prototype.ngOnChanges = function (changes) {
            if (!this.treeModel) {
                this.tree = TreeComponent.EMPTY_TREE;
            } else {
                this.tree = new tree_1.Tree(this.treeModel);
            }
        };
        TreeComponent.prototype.ngOnInit = function () {
            var _this = this;
            this.treeService.nodeRemoved$.subscribe(function (e) {
                _this.nodeRemoved.emit(e);
            });
            this.treeService.nodeRenamed$.subscribe(function (e) {
                _this.nodeRenamed.emit(e);
            });
            this.treeService.nodeCreated$.subscribe(function (e) {
                _this.nodeCreated.emit(e);
            });
            this.treeService.nodeSelected$.subscribe(function (e) {
                _this.nodeSelected.emit(e);
            });
            this.treeService.nodeMoved$.subscribe(function (e) {
                _this.nodeMoved.emit(e);
            });
            this.treeService.nodeExpanded$.subscribe(function (e) {
                _this.nodeExpanded.emit(e);
            });
            this.treeService.nodeCollapsed$.subscribe(function (e) {
                _this.nodeCollapsed.emit(e);
            });
        };
        return TreeComponent;
    }();
    TreeComponent.EMPTY_TREE = new tree_1.Tree({ value: '' });
    TreeComponent.decorators = [{ type: core_1.Component, args: [{
            selector: 'tree',
            template: "<tree-internal [tree]=\"tree\" [settings]=\"settings\"></tree-internal>",
            providers: [tree_service_1.TreeService]
        }] }];
    /** @nocollapse */
    TreeComponent.ctorParameters = function () {
        return [{ type: tree_service_1.TreeService, decorators: [{ type: core_1.Inject, args: [tree_service_1.TreeService] }] }];
    };
    TreeComponent.propDecorators = {
        'treeModel': [{ type: core_1.Input, args: ['tree'] }],
        'settings': [{ type: core_1.Input }],
        'nodeCreated': [{ type: core_1.Output }],
        'nodeRemoved': [{ type: core_1.Output }],
        'nodeRenamed': [{ type: core_1.Output }],
        'nodeSelected': [{ type: core_1.Output }],
        'nodeMoved': [{ type: core_1.Output }],
        'nodeExpanded': [{ type: core_1.Output }],
        'nodeCollapsed': [{ type: core_1.Output }]
    };
    exports.TreeComponent = TreeComponent;

});
$__System.registerDynamic("f", [], true, function ($__require, exports, module) {
    "use strict";

    var global = this || self,
        GLOBAL = global;
    Object.defineProperty(exports, "__esModule", { value: true });
    function isEmpty(value) {
        if (typeof value === 'string') {
            return !/\S/.test(value);
        }
        if (Array.isArray(value)) {
            return value.length === 0;
        }
        return isNil(value);
    }
    exports.isEmpty = isEmpty;
    function trim(value) {
        return isNil(value) ? '' : value.trim();
    }
    exports.trim = trim;
    function has(value, prop) {
        return value && typeof value === 'object' && value.hasOwnProperty(prop);
    }
    exports.has = has;
    function isFunction(value) {
        return typeof value === 'function';
    }
    exports.isFunction = isFunction;
    function get(value, path, defaultValue) {
        var result = value;
        for (var _i = 0, _a = path.split('.'); _i < _a.length; _i++) {
            var prop = _a[_i];
            if (!result || !Reflect.has(result, prop)) {
                return defaultValue;
            }
            result = result[prop];
        }
        return isNil(result) || result === value ? defaultValue : result;
    }
    exports.get = get;
    function omit(value, propToSkip) {
        return Object.keys(value).reduce(function (result, prop) {
            if (prop === propToSkip) {
                return result;
            }
            return Object.assign(result, (_a = {}, _a[prop] = value[prop], _a));
            var _a;
        }, {});
    }
    exports.omit = omit;
    function size(value) {
        return isEmpty(value) ? 0 : value.length;
    }
    exports.size = size;
    function once(fn) {
        var result;
        return function () {
            var args = [];
            for (var _i = 0; _i < arguments.length; _i++) {
                args[_i] = arguments[_i];
            }
            if (fn) {
                result = fn.apply(null, args);
                fn = null;
            }
            return result;
        };
    }
    exports.once = once;
    function defaultsDeep(target) {
        var sources = [];
        for (var _i = 1; _i < arguments.length; _i++) {
            sources[_i - 1] = arguments[_i];
        }
        return [target].concat(sources).reduce(function (result, source) {
            if (!source) {
                return result;
            }
            Object.keys(source).forEach(function (prop) {
                if (isNil(result[prop])) {
                    result[prop] = source[prop];
                    return;
                }
                if (typeof result[prop] === 'object' && !Array.isArray(result[prop])) {
                    result[prop] = defaultsDeep(result[prop], source[prop]);
                    return;
                }
            });
            return result;
        }, {});
    }
    exports.defaultsDeep = defaultsDeep;
    function includes(target, value) {
        if (isNil(target)) {
            return false;
        }
        var index = typeof target === 'string' ? target.indexOf(value) : target.indexOf(value);
        return index > -1;
    }
    exports.includes = includes;
    function isNil(value) {
        return value === undefined || value === null;
    }
    exports.isNil = isNil;

});
$__System.registerDynamic("10", ["f"], true, function ($__require, exports, module) {
    "use strict";

    var global = this || self,
        GLOBAL = global;
    Object.defineProperty(exports, "__esModule", { value: true });
    var fn_utils_1 = $__require("f");
    var FoldingType = function () {
        function FoldingType(_cssClass) {
            this._cssClass = _cssClass;
        }
        Object.defineProperty(FoldingType.prototype, "cssClass", {
            get: function () {
                return this._cssClass;
            },
            enumerable: true,
            configurable: true
        });
        return FoldingType;
    }();
    FoldingType.Expanded = new FoldingType('node-expanded');
    FoldingType.Collapsed = new FoldingType('node-collapsed');
    FoldingType.Empty = new FoldingType('node-empty');
    FoldingType.Leaf = new FoldingType('node-leaf');
    exports.FoldingType = FoldingType;
    var TreeModelSettings = function () {
        function TreeModelSettings() {}
        TreeModelSettings.merge = function (sourceA, sourceB) {
            return fn_utils_1.defaultsDeep({}, fn_utils_1.get(sourceA, 'settings'), fn_utils_1.get(sourceB, 'settings'), { static: false, leftMenu: false, rightMenu: true });
        };
        return TreeModelSettings;
    }();
    exports.TreeModelSettings = TreeModelSettings;
    var TreeStatus;
    (function (TreeStatus) {
        TreeStatus[TreeStatus["New"] = 0] = "New";
        TreeStatus[TreeStatus["Modified"] = 1] = "Modified";
        TreeStatus[TreeStatus["IsBeingRenamed"] = 2] = "IsBeingRenamed";
    })(TreeStatus = exports.TreeStatus || (exports.TreeStatus = {}));

});
$__System.registerDynamic("e", ["f", "11", "10"], true, function ($__require, exports, module) {
    "use strict";

    var global = this || self,
        GLOBAL = global;
    Object.defineProperty(exports, "__esModule", { value: true });
    var fn_utils_1 = $__require("f");
    var Rx_1 = $__require("11");
    var tree_types_1 = $__require("10");
    var ChildrenLoadingState;
    (function (ChildrenLoadingState) {
        ChildrenLoadingState[ChildrenLoadingState["NotStarted"] = 0] = "NotStarted";
        ChildrenLoadingState[ChildrenLoadingState["Loading"] = 1] = "Loading";
        ChildrenLoadingState[ChildrenLoadingState["Completed"] = 2] = "Completed";
    })(ChildrenLoadingState || (ChildrenLoadingState = {}));
    var Tree = function () {
        /**
         * Build an instance of Tree from an object implementing TreeModel interface.
         * @param {TreeModel} model - A model that is used to build a tree.
         * @param {Tree} [parent] - An optional parent if you want to build a tree from the model that should be a child of an existing Tree instance.
         * @param {boolean} [isBranch] - An option that makes a branch from created tree. Branch can have children.
         */
        function Tree(node, parent, isBranch) {
            if (parent === void 0) {
                parent = null;
            }
            if (isBranch === void 0) {
                isBranch = false;
            }
            var _this = this;
            this._childrenLoadingState = ChildrenLoadingState.NotStarted;
            this._childrenAsyncOnce = fn_utils_1.once(function () {
                return new Rx_1.Observable(function (observer) {
                    setTimeout(function () {
                        _this._childrenLoadingState = ChildrenLoadingState.Loading;
                        _this._loadChildren(function (children) {
                            _this._children = (children || []).map(function (child) {
                                return new Tree(child, _this);
                            });
                            _this._childrenLoadingState = ChildrenLoadingState.Completed;
                            observer.next(_this.children);
                            observer.complete();
                        });
                    });
                });
            });
            this.buildTreeFromModel(node, parent, isBranch || Array.isArray(node.children));
        }
        // STATIC METHODS ----------------------------------------------------------------------------------------------------
        /**
         * Check that value passed is not empty (it doesn't consist of only whitespace symbols).
         * @param {string} value - A value that should be checked.
         * @returns {boolean} - A flag indicating that value is empty or not.
         * @static
         */
        Tree.isValueEmpty = function (value) {
            return fn_utils_1.isEmpty(fn_utils_1.trim(value));
        };
        /**
         * Check whether a given value can be considered RenamableNode.
         * @param {any} value - A value to check.
         * @returns {boolean} - A flag indicating whether given value is Renamable node or not.
         * @static
         */
        Tree.isRenamable = function (value) {
            return fn_utils_1.has(value, 'setName') && fn_utils_1.isFunction(value.setName) && fn_utils_1.has(value, 'toString') && fn_utils_1.isFunction(value.toString) && value.toString !== Object.toString;
        };
        Tree.cloneTreeShallow = function (origin) {
            var tree = new Tree(Object.assign({}, origin.node));
            tree._children = origin._children;
            return tree;
        };
        Tree.applyNewValueToRenamable = function (value, newValue) {
            var renamableValue = Object.assign({}, value);
            renamableValue.setName(newValue);
            return renamableValue;
        };
        Tree.prototype.buildTreeFromModel = function (model, parent, isBranch) {
            var _this = this;
            this.parent = parent;
            this.node = Object.assign(fn_utils_1.omit(model, 'children'), {
                settings: tree_types_1.TreeModelSettings.merge(model, fn_utils_1.get(parent, 'node'))
            });
            if (fn_utils_1.isFunction(this.node.loadChildren)) {
                this._loadChildren = this.node.loadChildren;
            } else {
                fn_utils_1.get(model, 'children', []).forEach(function (child, index) {
                    _this._addChild(new Tree(child, _this), index);
                });
            }
            if (!Array.isArray(this._children)) {
                this._children = this.node.loadChildren || isBranch ? [] : null;
            }
        };
        /**
         * Check whether children of the node are being loaded.
         * Makes sense only for nodes that define `loadChildren` function.
         * @returns {boolean} A flag indicating that children are being loaded.
         */
        Tree.prototype.childrenAreBeingLoaded = function () {
            return this._childrenLoadingState === ChildrenLoadingState.Loading;
        };
        /**
         * Check whether children of the node were loaded.
         * Makes sense only for nodes that define `loadChildren` function.
         * @returns {boolean} A flag indicating that children were loaded.
         */
        Tree.prototype.childrenWereLoaded = function () {
            return this._childrenLoadingState === ChildrenLoadingState.Completed;
        };
        Tree.prototype.canLoadChildren = function () {
            return this._childrenLoadingState === ChildrenLoadingState.NotStarted && this.foldingType === tree_types_1.FoldingType.Expanded && !!this._loadChildren;
        };
        /**
         * Check whether children of the node should be loaded and not loaded yet.
         * Makes sense only for nodes that define `loadChildren` function.
         * @returns {boolean} A flag indicating that children should be loaded for the current node.
         */
        Tree.prototype.childrenShouldBeLoaded = function () {
            return !!this._loadChildren;
        };
        Object.defineProperty(Tree.prototype, "children", {
            /**
             * Get children of the current tree.
             * @returns {Tree[]} The children of the current tree.
             */
            get: function () {
                return this._children;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Tree.prototype, "childrenAsync", {
            /**
             * By getting value from this property you start process of loading node's children using `loadChildren` function.
             * Once children are loaded `loadChildren` function won't be called anymore and loaded for the first time children are emitted in case of subsequent calls.
             * @returns {Observable<Tree[]>} An observable which emits children once they are loaded.
             */
            get: function () {
                if (this.canLoadChildren()) {
                    return this._childrenAsyncOnce();
                }
                return Rx_1.Observable.of(this.children);
            },
            enumerable: true,
            configurable: true
        });
        /**
         * Create a new node in the current tree.
         * @param {boolean} isBranch - A flag that indicates whether a new node should be a "Branch". "Leaf" node will be created by default
         * @returns {Tree} A newly created child node.
         */
        Tree.prototype.createNode = function (isBranch) {
            var tree = new Tree({ value: '' }, null, isBranch);
            tree.markAsNew();
            if (this.childrenShouldBeLoaded() && !(this.childrenAreBeingLoaded() || this.childrenWereLoaded())) {
                return null;
            }
            if (this.isLeaf()) {
                return this.addSibling(tree);
            } else {
                return this.addChild(tree);
            }
        };
        Object.defineProperty(Tree.prototype, "value", {
            /**
             * Get the value of the current node
             * @returns {(string|RenamableNode)} The value of the node.
             */
            get: function () {
                return this.node.value;
            },
            /**
             * Set the value of the current node
             * @param {(string|RenamableNode)} value - The new value of the node.
             */
            set: function (value) {
                if (typeof value !== 'string' && !Tree.isRenamable(value)) {
                    return;
                }
                var stringifiedValue = '' + value;
                if (Tree.isRenamable(this.value)) {
                    this.node.value = Tree.applyNewValueToRenamable(this.value, stringifiedValue);
                } else {
                    this.node.value = Tree.isValueEmpty(stringifiedValue) ? this.node.value : stringifiedValue;
                }
            },
            enumerable: true,
            configurable: true
        });
        /**
         * Add a sibling node for the current node. This won't work if the current node is a root.
         * @param {Tree} sibling - A node that should become a sibling.
         * @param [number] position - Position in which sibling will be inserted. By default it will be inserted at the last position in a parent.
         * @returns {Tree} A newly inserted sibling, or null if you are trying to make a sibling for the root.
         */
        Tree.prototype.addSibling = function (sibling, position) {
            if (Array.isArray(fn_utils_1.get(this.parent, 'children'))) {
                return this.parent.addChild(sibling, position);
            }
            return null;
        };
        /**
         * Add a child node for the current node.
         * @param {Tree} child - A node that should become a child.
         * @param [number] position - Position in which child will be inserted. By default it will be inserted at the last position in a parent.
         * @returns {Tree} A newly inserted child.
         */
        Tree.prototype.addChild = function (child, position) {
            return this._addChild(Tree.cloneTreeShallow(child), position);
        };
        Tree.prototype._addChild = function (child, position) {
            if (position === void 0) {
                position = fn_utils_1.size(this._children) || 0;
            }
            child.parent = this;
            if (Array.isArray(this._children)) {
                this._children.splice(position, 0, child);
            } else {
                this._children = [child];
            }
            this._setFoldingType();
            if (this.isNodeCollapsed()) {
                this.switchFoldingType();
            }
            return child;
        };
        /**
         * Swap position of the current node with the given sibling. If node passed as a parameter is not a sibling - nothing happens.
         * @param {Tree} sibling - A sibling with which current node shold be swapped.
         */
        Tree.prototype.swapWithSibling = function (sibling) {
            if (!this.hasSibling(sibling)) {
                return;
            }
            var siblingIndex = sibling.positionInParent;
            var thisTreeIndex = this.positionInParent;
            this.parent._children[siblingIndex] = this;
            this.parent._children[thisTreeIndex] = sibling;
        };
        Object.defineProperty(Tree.prototype, "positionInParent", {
            /**
             * Get a node's position in its parent.
             * @returns {number} The position inside a parent.
             */
            get: function () {
                return this.parent.children ? this.parent.children.indexOf(this) : -1;
            },
            enumerable: true,
            configurable: true
        });
        /**
         * Check whether or not this tree is static.
         * @returns {boolean} A flag indicating whether or not this tree is static.
         */
        Tree.prototype.isStatic = function () {
            return fn_utils_1.get(this.node.settings, 'static', false);
        };
        /**
         * Check whether or not this tree has a left menu.
         * @returns {boolean} A flag indicating whether or not this tree has a left menu.
         */
        Tree.prototype.hasLeftMenu = function () {
            return !fn_utils_1.get(this.node.settings, 'static', false) && fn_utils_1.get(this.node.settings, 'leftMenu', false);
        };
        /**
         * Check whether or not this tree has a right menu.
         * @returns {boolean} A flag indicating whether or not this tree has a right menu.
         */
        Tree.prototype.hasRightMenu = function () {
            return !fn_utils_1.get(this.node.settings, 'static', false) && fn_utils_1.get(this.node.settings, 'rightMenu', false);
        };
        /**
         * Check whether this tree is "Leaf" or not.
         * @returns {boolean} A flag indicating whether or not this tree is a "Leaf".
         */
        Tree.prototype.isLeaf = function () {
            return !this.isBranch();
        };
        /**
         * Check whether this tree is "Branch" or not. "Branch" is a node that has children.
         * @returns {boolean} A flag indicating whether or not this tree is a "Branch".
         */
        Tree.prototype.isBranch = function () {
            return Array.isArray(this._children);
        };
        /**
         * Check whether this tree has children.
         * @returns {boolean} A flag indicating whether or not this tree has children.
         */
        Tree.prototype.hasChildren = function () {
            return !fn_utils_1.isEmpty(this._children) || this.childrenShouldBeLoaded();
        };
        /**
         * Check whether this tree is a root or not. The root is the tree (node) that doesn't have parent (or technically its parent is null).
         * @returns {boolean} A flag indicating whether or not this tree is the root.
         */
        Tree.prototype.isRoot = function () {
            return this.parent === null;
        };
        /**
         * Check whether provided tree is a sibling of the current tree. Sibling trees (nodes) are the trees that have the same parent.
         * @param {Tree} tree - A tree that should be tested on a siblingness.
         * @returns {boolean} A flag indicating whether or not provided tree is the sibling of the current one.
         */
        Tree.prototype.hasSibling = function (tree) {
            return !this.isRoot() && fn_utils_1.includes(this.parent.children, tree);
        };
        /**
         * Check whether provided tree is a child of the current tree.
         * This method tests that provided tree is a <strong>direct</strong> child of the current tree.
         * @param {Tree} tree - A tree that should be tested (child candidate).
         * @returns {boolean} A flag indicating whether provided tree is a child or not.
         */
        Tree.prototype.hasChild = function (tree) {
            return fn_utils_1.includes(this._children, tree);
        };
        /**
         * Remove given tree from the current tree.
         * The given tree will be removed only in case it is a direct child of the current tree (@see {@link hasChild}).
         * @param {Tree} tree - A tree that should be removed.
         */
        Tree.prototype.removeChild = function (tree) {
            if (!this.hasChildren()) {
                return;
            }
            var childIndex = this._children.findIndex(function (child) {
                return child === tree;
            });
            if (childIndex >= 0) {
                this._children.splice(childIndex, 1);
            }
            this._setFoldingType();
        };
        /**
         * Remove current tree from its parent.
         */
        Tree.prototype.removeItselfFromParent = function () {
            if (!this.parent) {
                return;
            }
            this.parent.removeChild(this);
        };
        /**
         * Switch folding type of the current tree. "Leaf" node cannot switch its folding type cause it doesn't have children, hence nothing to fold.
         * If node is a "Branch" and it is expanded, then by invoking current method state of the tree should be switched to "collapsed" and vice versa.
         */
        Tree.prototype.switchFoldingType = function () {
            if (this.isLeaf() || !this.hasChildren()) {
                return;
            }
            this.node._foldingType = this.isNodeExpanded() ? tree_types_1.FoldingType.Collapsed : tree_types_1.FoldingType.Expanded;
        };
        /**
         * Check that tree is expanded.
         * @returns {boolean} A flag indicating whether current tree is expanded. Always returns false for the "Leaf" tree and for an empty tree.
         */
        Tree.prototype.isNodeExpanded = function () {
            return this.foldingType === tree_types_1.FoldingType.Expanded;
        };
        /**
         * Check that tree is collapsed.
         * @returns {boolean} A flag indicating whether current tree is collapsed. Always returns false for the "Leaf" tree and for an empty tree.
         */
        Tree.prototype.isNodeCollapsed = function () {
            return this.foldingType === tree_types_1.FoldingType.Collapsed;
        };
        /**
         * Set a current folding type: expanded, collapsed or leaf.
         */
        Tree.prototype._setFoldingType = function () {
            if (this.childrenShouldBeLoaded()) {
                this.node._foldingType = tree_types_1.FoldingType.Collapsed;
            } else if (this._children && !fn_utils_1.isEmpty(this._children)) {
                this.node._foldingType = tree_types_1.FoldingType.Expanded;
            } else if (Array.isArray(this._children)) {
                this.node._foldingType = tree_types_1.FoldingType.Empty;
            } else {
                this.node._foldingType = tree_types_1.FoldingType.Leaf;
            }
        };
        Object.defineProperty(Tree.prototype, "foldingType", {
            /**
             * Get a current folding type: expanded, collapsed or leaf.
             * @returns {FoldingType} A folding type of the current tree.
             */
            get: function () {
                if (!this.node._foldingType) {
                    this._setFoldingType();
                }
                return this.node._foldingType;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Tree.prototype, "foldingCssClass", {
            /**
             * Get a css class for element which displayes folding state - expanded, collapsed or leaf
             * @returns {string} A string icontaining css class (classes)
             */
            get: function () {
                return this.getCssClassesFromSettings() || this.foldingType.cssClass;
            },
            enumerable: true,
            configurable: true
        });
        Tree.prototype.getCssClassesFromSettings = function () {
            if (!this.node._foldingType) {
                this._setFoldingType();
            }
            if (this.node._foldingType === tree_types_1.FoldingType.Collapsed) {
                return fn_utils_1.get(this.node.settings, 'cssClasses.collapsed', null);
            } else if (this.node._foldingType === tree_types_1.FoldingType.Expanded) {
                return fn_utils_1.get(this.node.settings, 'cssClasses.expanded', null);
            } else if (this.node._foldingType === tree_types_1.FoldingType.Empty) {
                return fn_utils_1.get(this.node.settings, 'cssClasses.empty', null);
            }
            return fn_utils_1.get(this.node.settings, 'cssClasses.leaf', null);
        };
        Object.defineProperty(Tree.prototype, "nodeTemplate", {
            /**
             * Get a html template to render before every node's name.
             * @returns {string} A string representing a html template.
             */
            get: function () {
                return this.getTemplateFromSettings();
            },
            enumerable: true,
            configurable: true
        });
        Tree.prototype.getTemplateFromSettings = function () {
            if (this.isLeaf()) {
                return fn_utils_1.get(this.node.settings, 'templates.leaf', '');
            } else {
                return fn_utils_1.get(this.node.settings, 'templates.node', '');
            }
        };
        Object.defineProperty(Tree.prototype, "leftMenuTemplate", {
            /**
             * Get a html template to render for an element activatin left menu of a node.
             * @returns {string} A string representing a html template.
             */
            get: function () {
                if (this.hasLeftMenu()) {
                    return fn_utils_1.get(this.node.settings, 'templates.leftMenu', '<span></span>');
                }
                return '';
            },
            enumerable: true,
            configurable: true
        });
        /**
         * Check that current tree is newly created (added by user via menu for example). Tree that was built from the TreeModel is not marked as new.
         * @returns {boolean} A flag whether the tree is new.
         */
        Tree.prototype.isNew = function () {
            return this.node._status === tree_types_1.TreeStatus.New;
        };
        /**
         * Mark current tree as new (@see {@link isNew}).
         */
        Tree.prototype.markAsNew = function () {
            this.node._status = tree_types_1.TreeStatus.New;
        };
        /**
         * Check that current tree is being renamed (it is in the process of its value renaming initiated by a user).
         * @returns {boolean} A flag whether the tree is being renamed.
         */
        Tree.prototype.isBeingRenamed = function () {
            return this.node._status === tree_types_1.TreeStatus.IsBeingRenamed;
        };
        /**
         * Mark current tree as being renamed (@see {@link isBeingRenamed}).
         */
        Tree.prototype.markAsBeingRenamed = function () {
            this.node._status = tree_types_1.TreeStatus.IsBeingRenamed;
        };
        /**
         * Check that current tree is modified (for example it was renamed).
         * @returns {boolean} A flag whether the tree is modified.
         */
        Tree.prototype.isModified = function () {
            return this.node._status === tree_types_1.TreeStatus.Modified;
        };
        /**
         * Mark current tree as modified (@see {@link isModified}).
         */
        Tree.prototype.markAsModified = function () {
            this.node._status = tree_types_1.TreeStatus.Modified;
        };
        return Tree;
    }();
    exports.Tree = Tree;

});
$__System.registerDynamic("12", ["c", "e", "13", "14", "15", "d", "16"], true, function ($__require, exports, module) {
    "use strict";

    var global = this || self,
        GLOBAL = global;
    Object.defineProperty(exports, "__esModule", { value: true });
    var core_1 = $__require("c");
    var tree_1 = $__require("e");
    var node_menu_service_1 = $__require("13");
    var menu_events_1 = $__require("14");
    var editable_events_1 = $__require("15");
    var tree_service_1 = $__require("d");
    var EventUtils = $__require("16");
    var TreeInternalComponent = function () {
        function TreeInternalComponent(nodeMenuService, treeService, element) {
            this.nodeMenuService = nodeMenuService;
            this.treeService = treeService;
            this.element = element;
            this.isSelected = false;
            this.isRightMenuVisible = false;
            this.isLeftMenuVisible = false;
        }
        TreeInternalComponent.prototype.ngOnInit = function () {
            var _this = this;
            this.settings = this.settings || { rootIsVisible: true };
            this.nodeMenuService.hideMenuStream(this.element).subscribe(function () {
                _this.isRightMenuVisible = false;
                _this.isLeftMenuVisible = false;
            });
            this.treeService.unselectStream(this.tree).subscribe(function () {
                return _this.isSelected = false;
            });
            this.treeService.draggedStream(this.tree, this.element).subscribe(function (e) {
                if (_this.tree.hasSibling(e.captured.tree)) {
                    _this.swapWithSibling(e.captured.tree, _this.tree);
                } else if (_this.tree.isBranch()) {
                    _this.moveNodeToThisTreeAndRemoveFromPreviousOne(e, _this.tree);
                } else {
                    _this.moveNodeToParentTreeAndRemoveFromPreviousOne(e, _this.tree);
                }
            });
        };
        TreeInternalComponent.prototype.swapWithSibling = function (sibling, tree) {
            tree.swapWithSibling(sibling);
            this.treeService.fireNodeMoved(sibling, sibling.parent);
        };
        TreeInternalComponent.prototype.moveNodeToThisTreeAndRemoveFromPreviousOne = function (e, tree) {
            this.treeService.fireNodeRemoved(e.captured.tree);
            var addedChild = tree.addChild(e.captured.tree);
            this.treeService.fireNodeMoved(addedChild, e.captured.tree.parent);
        };
        TreeInternalComponent.prototype.moveNodeToParentTreeAndRemoveFromPreviousOne = function (e, tree) {
            this.treeService.fireNodeRemoved(e.captured.tree);
            var addedSibling = tree.addSibling(e.captured.tree, tree.positionInParent);
            this.treeService.fireNodeMoved(addedSibling, e.captured.tree.parent);
        };
        TreeInternalComponent.prototype.onNodeSelected = function (e) {
            if (EventUtils.isLeftButtonClicked(e)) {
                this.isSelected = true;
                this.treeService.fireNodeSelected(this.tree);
            }
        };
        TreeInternalComponent.prototype.showRightMenu = function (e) {
            if (!this.tree.hasRightMenu()) {
                return;
            }
            if (EventUtils.isRightButtonClicked(e)) {
                this.isRightMenuVisible = !this.isRightMenuVisible;
                this.nodeMenuService.hideMenuForAllNodesExcept(this.element);
            }
            e.preventDefault();
        };
        TreeInternalComponent.prototype.showLeftMenu = function (e) {
            if (!this.tree.hasLeftMenu()) {
                return;
            }
            if (EventUtils.isLeftButtonClicked(e)) {
                this.isLeftMenuVisible = !this.isLeftMenuVisible;
                this.nodeMenuService.hideMenuForAllNodesExcept(this.element);
                if (this.isLeftMenuVisible) {
                    e.preventDefault();
                }
            }
        };
        TreeInternalComponent.prototype.onMenuItemSelected = function (e) {
            switch (e.nodeMenuItemAction) {
                case menu_events_1.NodeMenuItemAction.NewTag:
                    this.onNewSelected(e);
                    break;
                case menu_events_1.NodeMenuItemAction.NewFolder:
                    this.onNewSelected(e);
                    break;
                case menu_events_1.NodeMenuItemAction.Rename:
                    this.onRenameSelected();
                    break;
                case menu_events_1.NodeMenuItemAction.Remove:
                    this.onRemoveSelected();
                    break;
                default:
                    throw new Error("Chosen menu item doesn't exist");
            }
        };
        TreeInternalComponent.prototype.onNewSelected = function (e) {
            this.tree.createNode(e.nodeMenuItemAction === menu_events_1.NodeMenuItemAction.NewFolder);
            this.isRightMenuVisible = false;
            this.isLeftMenuVisible = false;
        };
        TreeInternalComponent.prototype.onRenameSelected = function () {
            this.tree.markAsBeingRenamed();
            this.isRightMenuVisible = false;
            this.isLeftMenuVisible = false;
        };
        TreeInternalComponent.prototype.onRemoveSelected = function () {
            this.treeService.fireNodeRemoved(this.tree);
        };
        TreeInternalComponent.prototype.onSwitchFoldingType = function () {
            this.tree.switchFoldingType();
            this.treeService.fireNodeSwitchFoldingType(this.tree);
        };
        TreeInternalComponent.prototype.applyNewValue = function (e) {
            if ((e.action === editable_events_1.NodeEditableEventAction.Cancel || this.tree.isNew()) && tree_1.Tree.isValueEmpty(e.value)) {
                return this.treeService.fireNodeRemoved(this.tree);
            }
            if (this.tree.isNew()) {
                this.tree.value = e.value;
                this.treeService.fireNodeCreated(this.tree);
            }
            if (this.tree.isBeingRenamed()) {
                var oldValue = this.tree.value;
                this.tree.value = e.value;
                this.treeService.fireNodeRenamed(oldValue, this.tree);
            }
            this.tree.markAsModified();
        };
        TreeInternalComponent.prototype.shouldShowInputForTreeValue = function () {
            return this.tree.isNew() || this.tree.isBeingRenamed();
        };
        TreeInternalComponent.prototype.isRootHidden = function () {
            return this.tree.isRoot() && !this.settings.rootIsVisible;
        };
        return TreeInternalComponent;
    }();
    TreeInternalComponent.decorators = [{ type: core_1.Component, args: [{
            selector: 'tree-internal',
            template: "\n  <ul class=\"tree\" *ngIf=\"tree\" [ngClass]=\"{rootless: isRootHidden()}\">\n    <li>\n      <div class=\"value-container\"\n        [ngClass]=\"{rootless: isRootHidden()}\"\n        (contextmenu)=\"showRightMenu($event)\"\n        [nodeDraggable]=\"element\"\n        [tree]=\"tree\">\n\n        <div class=\"folding\" (click)=\"onSwitchFoldingType()\" [ngClass]=\"tree.foldingCssClass\"></div>\n        <div class=\"node-value\"\n          *ngIf=\"!shouldShowInputForTreeValue()\"\n          [class.node-selected]=\"isSelected\"\n          (click)=\"onNodeSelected($event)\">\n            <div *ngIf=\"tree.nodeTemplate\" class=\"node-template\" [innerHTML]=\"tree.nodeTemplate | safeHtml\"></div>\n            <span class=\"node-name\" [innerHTML]=\"tree.value | safeHtml\"></span>\n            <span class=\"loading-children\" *ngIf=\"tree.childrenAreBeingLoaded()\"></span>\n        </div>\n\n        <input type=\"text\" class=\"node-value\"\n           *ngIf=\"shouldShowInputForTreeValue()\"\n           [nodeEditable]=\"tree.value\"\n           (valueChanged)=\"applyNewValue($event)\"/>\n\n        <div class=\"node-left-menu\" *ngIf=\"tree.hasLeftMenu()\" (click)=\"showLeftMenu($event)\" [innerHTML]=\"tree.leftMenuTemplate\">\n        </div>\n        <node-menu *ngIf=\"tree.hasLeftMenu() && isLeftMenuVisible\"\n          (menuItemSelected)=\"onMenuItemSelected($event)\">\n        </node-menu>\n      </div>\n\n      <node-menu *ngIf=\"isRightMenuVisible\" (menuItemSelected)=\"onMenuItemSelected($event)\"></node-menu>\n\n      <ng-template [ngIf]=\"tree.isNodeExpanded()\">\n        <tree-internal *ngFor=\"let child of tree.childrenAsync | async\" [tree]=\"child\"></tree-internal>\n      </ng-template>\n    </li>\n  </ul>\n  "
        }] }];
    /** @nocollapse */
    TreeInternalComponent.ctorParameters = function () {
        return [{ type: node_menu_service_1.NodeMenuService, decorators: [{ type: core_1.Inject, args: [node_menu_service_1.NodeMenuService] }] }, { type: tree_service_1.TreeService, decorators: [{ type: core_1.Inject, args: [tree_service_1.TreeService] }] }, { type: core_1.ElementRef, decorators: [{ type: core_1.Inject, args: [core_1.ElementRef] }] }];
    };
    TreeInternalComponent.propDecorators = {
        'tree': [{ type: core_1.Input }],
        'settings': [{ type: core_1.Input }]
    };
    exports.TreeInternalComponent = TreeInternalComponent;

});
$__System.registerDynamic("17", [], true, function ($__require, exports, module) {
    "use strict";

    var global = this || self,
        GLOBAL = global;
    Object.defineProperty(exports, "__esModule", { value: true });
    var CapturedNode = function () {
        function CapturedNode(anElement, aTree) {
            this.anElement = anElement;
            this.aTree = aTree;
        }
        CapturedNode.prototype.canBeDroppedAt = function (element) {
            return !this.sameAs(element) && !this.contains(element);
        };
        CapturedNode.prototype.contains = function (other) {
            return this.element.nativeElement.contains(other.nativeElement);
        };
        CapturedNode.prototype.sameAs = function (other) {
            return this.element === other;
        };
        Object.defineProperty(CapturedNode.prototype, "element", {
            get: function () {
                return this.anElement;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(CapturedNode.prototype, "tree", {
            get: function () {
                return this.aTree;
            },
            enumerable: true,
            configurable: true
        });
        return CapturedNode;
    }();
    exports.CapturedNode = CapturedNode;

});
$__System.registerDynamic("18", ["c", "19", "17"], true, function ($__require, exports, module) {
    "use strict";

    var global = this || self,
        GLOBAL = global;
    Object.defineProperty(exports, "__esModule", { value: true });
    var core_1 = $__require("c");
    var node_draggable_service_1 = $__require("19");
    var captured_node_1 = $__require("17");
    var NodeDraggableDirective = function () {
        function NodeDraggableDirective(element, nodeDraggableService, renderer) {
            this.element = element;
            this.nodeDraggableService = nodeDraggableService;
            this.renderer = renderer;
            this.disposersForDragListeners = [];
            this.nodeNativeElement = element.nativeElement;
        }
        NodeDraggableDirective.prototype.ngOnInit = function () {
            if (!this.tree.isStatic()) {
                this.renderer.setElementAttribute(this.nodeNativeElement, 'draggable', 'true');
                this.disposersForDragListeners.push(this.renderer.listen(this.nodeNativeElement, 'dragenter', this.handleDragEnter.bind(this)));
                this.disposersForDragListeners.push(this.renderer.listen(this.nodeNativeElement, 'dragover', this.handleDragOver.bind(this)));
                this.disposersForDragListeners.push(this.renderer.listen(this.nodeNativeElement, 'dragstart', this.handleDragStart.bind(this)));
                this.disposersForDragListeners.push(this.renderer.listen(this.nodeNativeElement, 'dragleave', this.handleDragLeave.bind(this)));
                this.disposersForDragListeners.push(this.renderer.listen(this.nodeNativeElement, 'drop', this.handleDrop.bind(this)));
                this.disposersForDragListeners.push(this.renderer.listen(this.nodeNativeElement, 'dragend', this.handleDragEnd.bind(this)));
            }
        };
        NodeDraggableDirective.prototype.ngOnDestroy = function () {
            /* tslint:disable:typedef */
            this.disposersForDragListeners.forEach(function (dispose) {
                return dispose();
            });
            /* tslint:enable:typedef */
        };
        NodeDraggableDirective.prototype.handleDragStart = function (e) {
            e.stopPropagation();
            this.nodeDraggableService.captureNode(new captured_node_1.CapturedNode(this.nodeDraggable, this.tree));
            e.dataTransfer.setData('text', NodeDraggableDirective.DATA_TRANSFER_STUB_DATA);
            e.dataTransfer.effectAllowed = 'move';
        };
        NodeDraggableDirective.prototype.handleDragOver = function (e) {
            e.preventDefault();
            e.dataTransfer.dropEffect = 'move';
        };
        NodeDraggableDirective.prototype.handleDragEnter = function (e) {
            e.preventDefault();
            if (this.containsElementAt(e)) {
                this.addClass('over-drop-target');
            }
        };
        NodeDraggableDirective.prototype.handleDragLeave = function (e) {
            if (!this.containsElementAt(e)) {
                this.removeClass('over-drop-target');
            }
        };
        NodeDraggableDirective.prototype.handleDrop = function (e) {
            e.preventDefault();
            e.stopPropagation();
            this.removeClass('over-drop-target');
            if (!this.isDropPossible(e)) {
                return false;
            }
            if (this.nodeDraggableService.getCapturedNode()) {
                return this.notifyThatNodeWasDropped();
            }
        };
        NodeDraggableDirective.prototype.isDropPossible = function (e) {
            var capturedNode = this.nodeDraggableService.getCapturedNode();
            return capturedNode && capturedNode.canBeDroppedAt(this.nodeDraggable) && this.containsElementAt(e);
        };
        NodeDraggableDirective.prototype.handleDragEnd = function (e) {
            this.removeClass('over-drop-target');
            this.nodeDraggableService.releaseCapturedNode();
        };
        NodeDraggableDirective.prototype.containsElementAt = function (e) {
            var _a = e.x,
                x = _a === void 0 ? e.clientX : _a,
                _b = e.y,
                y = _b === void 0 ? e.clientY : _b;
            return this.nodeNativeElement.contains(document.elementFromPoint(x, y));
        };
        NodeDraggableDirective.prototype.addClass = function (className) {
            var classList = this.nodeNativeElement.classList;
            classList.add(className);
        };
        NodeDraggableDirective.prototype.removeClass = function (className) {
            var classList = this.nodeNativeElement.classList;
            classList.remove(className);
        };
        NodeDraggableDirective.prototype.notifyThatNodeWasDropped = function () {
            this.nodeDraggableService.fireNodeDragged(this.nodeDraggableService.getCapturedNode(), this.nodeDraggable);
        };
        return NodeDraggableDirective;
    }();
    NodeDraggableDirective.DATA_TRANSFER_STUB_DATA = 'some browsers enable drag-n-drop only when dataTransfer has data';
    NodeDraggableDirective.decorators = [{ type: core_1.Directive, args: [{
            selector: '[nodeDraggable]'
        }] }];
    /** @nocollapse */
    NodeDraggableDirective.ctorParameters = function () {
        return [{ type: core_1.ElementRef, decorators: [{ type: core_1.Inject, args: [core_1.ElementRef] }] }, { type: node_draggable_service_1.NodeDraggableService, decorators: [{ type: core_1.Inject, args: [node_draggable_service_1.NodeDraggableService] }] }, { type: core_1.Renderer, decorators: [{ type: core_1.Inject, args: [core_1.Renderer] }] }];
    };
    NodeDraggableDirective.propDecorators = {
        'nodeDraggable': [{ type: core_1.Input }],
        'tree': [{ type: core_1.Input }]
    };
    exports.NodeDraggableDirective = NodeDraggableDirective;

});
$__System.registerDynamic("15", [], true, function ($__require, exports, module) {
    "use strict";

    var global = this || self,
        GLOBAL = global;
    Object.defineProperty(exports, "__esModule", { value: true });
    var NodeEditableEventAction;
    (function (NodeEditableEventAction) {
        NodeEditableEventAction[NodeEditableEventAction["Cancel"] = 0] = "Cancel";
    })(NodeEditableEventAction = exports.NodeEditableEventAction || (exports.NodeEditableEventAction = {}));

});
$__System.registerDynamic("1a", ["c", "15"], true, function ($__require, exports, module) {
    "use strict";

    var global = this || self,
        GLOBAL = global;
    Object.defineProperty(exports, "__esModule", { value: true });
    var core_1 = $__require("c");
    var editable_events_1 = $__require("15");
    var NodeEditableDirective = function () {
        function NodeEditableDirective(renderer, elementRef) {
            this.renderer = renderer;
            this.elementRef = elementRef;
            /* tslint:enable:no-input-rename */
            this.valueChanged = new core_1.EventEmitter(false);
        }
        NodeEditableDirective.prototype.ngOnInit = function () {
            var nativeElement = this.elementRef.nativeElement;
            this.renderer.invokeElementMethod(nativeElement, 'focus', []);
            this.renderer.setElementProperty(nativeElement, 'value', this.nodeValue);
        };
        NodeEditableDirective.prototype.applyNewValue = function (newNodeValue) {
            this.valueChanged.emit({ type: 'keyup', value: newNodeValue });
        };
        NodeEditableDirective.prototype.applyNewValueByLoosingFocus = function (newNodeValue) {
            this.valueChanged.emit({ type: 'blur', value: newNodeValue });
        };
        NodeEditableDirective.prototype.cancelEditing = function () {
            this.valueChanged.emit({
                type: 'keyup',
                value: this.nodeValue,
                action: editable_events_1.NodeEditableEventAction.Cancel
            });
        };
        return NodeEditableDirective;
    }();
    NodeEditableDirective.decorators = [{ type: core_1.Directive, args: [{
            selector: '[nodeEditable]'
        }] }];
    /** @nocollapse */
    NodeEditableDirective.ctorParameters = function () {
        return [{ type: core_1.Renderer, decorators: [{ type: core_1.Inject, args: [core_1.Renderer] }] }, { type: core_1.ElementRef, decorators: [{ type: core_1.Inject, args: [core_1.ElementRef] }] }];
    };
    NodeEditableDirective.propDecorators = {
        'nodeValue': [{ type: core_1.Input, args: ['nodeEditable'] }],
        'valueChanged': [{ type: core_1.Output }],
        'applyNewValue': [{ type: core_1.HostListener, args: ['keyup.enter', ['$event.target.value']] }],
        'applyNewValueByLoosingFocus': [{ type: core_1.HostListener, args: ['blur', ['$event.target.value']] }],
        'cancelEditing': [{ type: core_1.HostListener, args: ['keyup.esc'] }]
    };
    exports.NodeEditableDirective = NodeEditableDirective;

});
$__System.registerDynamic("16", [], true, function ($__require, exports, module) {
    "use strict";

    var global = this || self,
        GLOBAL = global;
    Object.defineProperty(exports, "__esModule", { value: true });
    var Keys;
    (function (Keys) {
        Keys[Keys["Escape"] = 27] = "Escape";
    })(Keys = exports.Keys || (exports.Keys = {}));
    var MouseButtons;
    (function (MouseButtons) {
        MouseButtons[MouseButtons["Left"] = 0] = "Left";
        MouseButtons[MouseButtons["Right"] = 2] = "Right";
    })(MouseButtons = exports.MouseButtons || (exports.MouseButtons = {}));
    function isLeftButtonClicked(e) {
        return e.button === MouseButtons.Left;
    }
    exports.isLeftButtonClicked = isLeftButtonClicked;
    function isRightButtonClicked(e) {
        return e.button === MouseButtons.Right;
    }
    exports.isRightButtonClicked = isRightButtonClicked;
    function isEscapePressed(e) {
        return e.keyCode === Keys.Escape;
    }
    exports.isEscapePressed = isEscapePressed;

});
$__System.registerDynamic("1b", ["c", "13", "14", "16"], true, function ($__require, exports, module) {
    "use strict";

    var global = this || self,
        GLOBAL = global;
    Object.defineProperty(exports, "__esModule", { value: true });
    var core_1 = $__require("c");
    var node_menu_service_1 = $__require("13");
    var menu_events_1 = $__require("14");
    var event_utils_1 = $__require("16");
    var NodeMenuComponent = function () {
        function NodeMenuComponent(renderer, nodeMenuService) {
            this.renderer = renderer;
            this.nodeMenuService = nodeMenuService;
            this.menuItemSelected = new core_1.EventEmitter();
            this.availableMenuItems = [{
                name: 'New tag',
                action: menu_events_1.NodeMenuItemAction.NewTag,
                cssClass: 'new-tag'
            }, {
                name: 'New folder',
                action: menu_events_1.NodeMenuItemAction.NewFolder,
                cssClass: 'new-folder'
            }, {
                name: 'Rename',
                action: menu_events_1.NodeMenuItemAction.Rename,
                cssClass: 'rename'
            }, {
                name: 'Remove',
                action: menu_events_1.NodeMenuItemAction.Remove,
                cssClass: 'remove'
            }];
            this.disposersForGlobalListeners = [];
        }
        NodeMenuComponent.prototype.ngOnInit = function () {
            this.disposersForGlobalListeners.push(this.renderer.listenGlobal('document', 'keyup', this.closeMenu.bind(this)));
            this.disposersForGlobalListeners.push(this.renderer.listenGlobal('document', 'mousedown', this.closeMenu.bind(this)));
        };
        NodeMenuComponent.prototype.ngOnDestroy = function () {
            this.disposersForGlobalListeners.forEach(function (dispose) {
                return dispose();
            });
        };
        NodeMenuComponent.prototype.onMenuItemSelected = function (e, selectedMenuItem) {
            if (event_utils_1.isLeftButtonClicked(e)) {
                this.menuItemSelected.emit({ nodeMenuItemAction: selectedMenuItem.action });
                this.nodeMenuService.fireMenuEvent(e.target, menu_events_1.NodeMenuAction.Close);
            }
        };
        NodeMenuComponent.prototype.closeMenu = function (e) {
            var mouseClicked = e instanceof MouseEvent;
            // Check if the click is fired on an element inside a menu
            var containingTarget = this.menuContainer.nativeElement !== e.target && this.menuContainer.nativeElement.contains(e.target);
            if (mouseClicked && !containingTarget || event_utils_1.isEscapePressed(e)) {
                this.nodeMenuService.fireMenuEvent(e.target, menu_events_1.NodeMenuAction.Close);
            }
        };
        return NodeMenuComponent;
    }();
    NodeMenuComponent.decorators = [{ type: core_1.Component, args: [{
            selector: 'node-menu',
            template: "\n    <div class=\"node-menu\">\n      <ul class=\"node-menu-content\" #menuContainer>\n        <li class=\"node-menu-item\" *ngFor=\"let menuItem of availableMenuItems\"\n          (click)=\"onMenuItemSelected($event, menuItem)\">\n          <div class=\"node-menu-item-icon {{menuItem.cssClass}}\"></div>\n          <span class=\"node-menu-item-value\">{{menuItem.name}}</span>\n        </li>\n      </ul>\n    </div>\n  "
        }] }];
    /** @nocollapse */
    NodeMenuComponent.ctorParameters = function () {
        return [{ type: core_1.Renderer, decorators: [{ type: core_1.Inject, args: [core_1.Renderer] }] }, { type: node_menu_service_1.NodeMenuService, decorators: [{ type: core_1.Inject, args: [node_menu_service_1.NodeMenuService] }] }];
    };
    NodeMenuComponent.propDecorators = {
        'menuItemSelected': [{ type: core_1.Output }],
        'menuContainer': [{ type: core_1.ViewChild, args: ['menuContainer'] }]
    };
    exports.NodeMenuComponent = NodeMenuComponent;

});
$__System.registerDynamic("14", [], true, function ($__require, exports, module) {
    "use strict";

    var global = this || self,
        GLOBAL = global;
    Object.defineProperty(exports, "__esModule", { value: true });
    var NodeMenuItemAction;
    (function (NodeMenuItemAction) {
        NodeMenuItemAction[NodeMenuItemAction["NewFolder"] = 0] = "NewFolder";
        NodeMenuItemAction[NodeMenuItemAction["NewTag"] = 1] = "NewTag";
        NodeMenuItemAction[NodeMenuItemAction["Rename"] = 2] = "Rename";
        NodeMenuItemAction[NodeMenuItemAction["Remove"] = 3] = "Remove";
    })(NodeMenuItemAction = exports.NodeMenuItemAction || (exports.NodeMenuItemAction = {}));
    var NodeMenuAction;
    (function (NodeMenuAction) {
        NodeMenuAction[NodeMenuAction["Close"] = 0] = "Close";
    })(NodeMenuAction = exports.NodeMenuAction || (exports.NodeMenuAction = {}));

});
$__System.registerDynamic("13", ["c", "11", "14"], true, function ($__require, exports, module) {
    "use strict";

    var global = this || self,
        GLOBAL = global;
    Object.defineProperty(exports, "__esModule", { value: true });
    var core_1 = $__require("c");
    var Rx_1 = $__require("11");
    var menu_events_1 = $__require("14");
    var NodeMenuService = function () {
        function NodeMenuService() {
            this.nodeMenuEvents$ = new Rx_1.Subject();
        }
        NodeMenuService.prototype.fireMenuEvent = function (sender, action) {
            var nodeMenuEvent = { sender: sender, action: action };
            this.nodeMenuEvents$.next(nodeMenuEvent);
        };
        NodeMenuService.prototype.hideMenuStream = function (treeElementRef) {
            return this.nodeMenuEvents$.filter(function (e) {
                return treeElementRef.nativeElement !== e.sender;
            }).filter(function (e) {
                return e.action === menu_events_1.NodeMenuAction.Close;
            });
        };
        NodeMenuService.prototype.hideMenuForAllNodesExcept = function (treeElementRef) {
            this.nodeMenuEvents$.next({
                sender: treeElementRef.nativeElement,
                action: menu_events_1.NodeMenuAction.Close
            });
        };
        return NodeMenuService;
    }();
    NodeMenuService.decorators = [{ type: core_1.Injectable }];
    /** @nocollapse */
    NodeMenuService.ctorParameters = function () {
        return [];
    };
    exports.NodeMenuService = NodeMenuService;

});
$__System.registerDynamic("1c", [], true, function ($__require, exports, module) {
    "use strict";

    var global = this || self,
        GLOBAL = global;
    var __extends = exports && exports.__extends || function () {
        var extendStatics = Object.setPrototypeOf || { __proto__: [] } instanceof Array && function (d, b) {
            d.__proto__ = b;
        } || function (d, b) {
            for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
        };
        return function (d, b) {
            extendStatics(d, b);
            function __() {
                this.constructor = d;
            }
            d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
        };
    }();
    Object.defineProperty(exports, "__esModule", { value: true });
    var NodeEvent = function () {
        function NodeEvent(node) {
            this.node = node;
        }
        return NodeEvent;
    }();
    exports.NodeEvent = NodeEvent;
    var NodeSelectedEvent = function (_super) {
        __extends(NodeSelectedEvent, _super);
        function NodeSelectedEvent(node) {
            return _super.call(this, node) || this;
        }
        return NodeSelectedEvent;
    }(NodeEvent);
    exports.NodeSelectedEvent = NodeSelectedEvent;
    var NodeDestructiveEvent = function (_super) {
        __extends(NodeDestructiveEvent, _super);
        function NodeDestructiveEvent(node) {
            return _super.call(this, node) || this;
        }
        return NodeDestructiveEvent;
    }(NodeEvent);
    exports.NodeDestructiveEvent = NodeDestructiveEvent;
    var NodeMovedEvent = function (_super) {
        __extends(NodeMovedEvent, _super);
        function NodeMovedEvent(node, previousParent) {
            var _this = _super.call(this, node) || this;
            _this.previousParent = previousParent;
            return _this;
        }
        return NodeMovedEvent;
    }(NodeDestructiveEvent);
    exports.NodeMovedEvent = NodeMovedEvent;
    var NodeRemovedEvent = function (_super) {
        __extends(NodeRemovedEvent, _super);
        function NodeRemovedEvent(node) {
            return _super.call(this, node) || this;
        }
        return NodeRemovedEvent;
    }(NodeDestructiveEvent);
    exports.NodeRemovedEvent = NodeRemovedEvent;
    var NodeCreatedEvent = function (_super) {
        __extends(NodeCreatedEvent, _super);
        function NodeCreatedEvent(node) {
            return _super.call(this, node) || this;
        }
        return NodeCreatedEvent;
    }(NodeDestructiveEvent);
    exports.NodeCreatedEvent = NodeCreatedEvent;
    var NodeRenamedEvent = function (_super) {
        __extends(NodeRenamedEvent, _super);
        function NodeRenamedEvent(node, oldValue, newValue) {
            var _this = _super.call(this, node) || this;
            _this.oldValue = oldValue;
            _this.newValue = newValue;
            return _this;
        }
        return NodeRenamedEvent;
    }(NodeDestructiveEvent);
    exports.NodeRenamedEvent = NodeRenamedEvent;
    var NodeExpandedEvent = function (_super) {
        __extends(NodeExpandedEvent, _super);
        function NodeExpandedEvent(node) {
            return _super.call(this, node) || this;
        }
        return NodeExpandedEvent;
    }(NodeEvent);
    exports.NodeExpandedEvent = NodeExpandedEvent;
    var NodeCollapsedEvent = function (_super) {
        __extends(NodeCollapsedEvent, _super);
        function NodeCollapsedEvent(node) {
            return _super.call(this, node) || this;
        }
        return NodeCollapsedEvent;
    }(NodeEvent);
    exports.NodeCollapsedEvent = NodeCollapsedEvent;

});
$__System.registerDynamic("1d", [], true, function ($__require, exports, module) {
    "use strict";

    var global = this || self,
        GLOBAL = global;
    Object.defineProperty(exports, "__esModule", { value: true });
    var NodeDraggableEvent = function () {
        function NodeDraggableEvent(captured, target) {
            this.captured = captured;
            this.target = target;
        }
        return NodeDraggableEvent;
    }();
    exports.NodeDraggableEvent = NodeDraggableEvent;

});
$__System.registerDynamic("19", ["c", "11", "1d"], true, function ($__require, exports, module) {
    "use strict";

    var global = this || self,
        GLOBAL = global;
    Object.defineProperty(exports, "__esModule", { value: true });
    var core_1 = $__require("c");
    var Rx_1 = $__require("11");
    var draggable_events_1 = $__require("1d");
    var NodeDraggableService = function () {
        function NodeDraggableService() {
            this.draggableNodeEvents$ = new Rx_1.Subject();
        }
        NodeDraggableService.prototype.fireNodeDragged = function (captured, target) {
            if (!captured.tree || captured.tree.isStatic()) {
                return;
            }
            this.draggableNodeEvents$.next(new draggable_events_1.NodeDraggableEvent(captured, target));
        };
        NodeDraggableService.prototype.captureNode = function (node) {
            this.capturedNode = node;
        };
        NodeDraggableService.prototype.getCapturedNode = function () {
            return this.capturedNode;
        };
        NodeDraggableService.prototype.releaseCapturedNode = function () {
            this.capturedNode = null;
        };
        return NodeDraggableService;
    }();
    NodeDraggableService.decorators = [{ type: core_1.Injectable }];
    /** @nocollapse */
    NodeDraggableService.ctorParameters = function () {
        return [];
    };
    exports.NodeDraggableService = NodeDraggableService;

});
$__System.registerDynamic("d", ["1c", "11", "c", "19"], true, function ($__require, exports, module) {
    "use strict";

    var global = this || self,
        GLOBAL = global;
    Object.defineProperty(exports, "__esModule", { value: true });
    var tree_events_1 = $__require("1c");
    var Rx_1 = $__require("11");
    var core_1 = $__require("c");
    var node_draggable_service_1 = $__require("19");
    var TreeService = function () {
        function TreeService(nodeDraggableService) {
            this.nodeDraggableService = nodeDraggableService;
            this.nodeMoved$ = new Rx_1.Subject();
            this.nodeRemoved$ = new Rx_1.Subject();
            this.nodeRenamed$ = new Rx_1.Subject();
            this.nodeCreated$ = new Rx_1.Subject();
            this.nodeSelected$ = new Rx_1.Subject();
            this.nodeExpanded$ = new Rx_1.Subject();
            this.nodeCollapsed$ = new Rx_1.Subject();
            this.nodeRemoved$.subscribe(function (e) {
                return e.node.removeItselfFromParent();
            });
        }
        TreeService.prototype.unselectStream = function (tree) {
            return this.nodeSelected$.filter(function (e) {
                return tree !== e.node;
            });
        };
        TreeService.prototype.fireNodeRemoved = function (tree) {
            this.nodeRemoved$.next(new tree_events_1.NodeRemovedEvent(tree));
        };
        TreeService.prototype.fireNodeCreated = function (tree) {
            this.nodeCreated$.next(new tree_events_1.NodeCreatedEvent(tree));
        };
        TreeService.prototype.fireNodeSelected = function (tree) {
            this.nodeSelected$.next(new tree_events_1.NodeSelectedEvent(tree));
        };
        TreeService.prototype.fireNodeRenamed = function (oldValue, tree) {
            this.nodeRenamed$.next(new tree_events_1.NodeRenamedEvent(tree, oldValue, tree.value));
        };
        TreeService.prototype.fireNodeMoved = function (tree, parent) {
            this.nodeMoved$.next(new tree_events_1.NodeMovedEvent(tree, parent));
        };
        TreeService.prototype.fireNodeSwitchFoldingType = function (tree) {
            if (tree.isNodeExpanded()) {
                this.fireNodeExpanded(tree);
            } else if (tree.isNodeCollapsed()) {
                this.fireNodeCollapsed(tree);
            }
        };
        TreeService.prototype.fireNodeExpanded = function (tree) {
            this.nodeExpanded$.next(new tree_events_1.NodeExpandedEvent(tree));
        };
        TreeService.prototype.fireNodeCollapsed = function (tree) {
            this.nodeCollapsed$.next(new tree_events_1.NodeCollapsedEvent(tree));
        };
        TreeService.prototype.draggedStream = function (tree, element) {
            return this.nodeDraggableService.draggableNodeEvents$.filter(function (e) {
                return e.target === element;
            }).filter(function (e) {
                return !e.captured.tree.hasChild(tree);
            });
        };
        return TreeService;
    }();
    TreeService.decorators = [{ type: core_1.Injectable }];
    /** @nocollapse */
    TreeService.ctorParameters = function () {
        return [{ type: node_draggable_service_1.NodeDraggableService, decorators: [{ type: core_1.Inject, args: [node_draggable_service_1.NodeDraggableService] }] }];
    };
    exports.TreeService = TreeService;

});
$__System.registerDynamic("1e", ["c", "1f"], true, function ($__require, exports, module) {
    "use strict";

    var global = this || self,
        GLOBAL = global;
    Object.defineProperty(exports, "__esModule", { value: true });
    var core_1 = $__require("c");
    var platform_browser_1 = $__require("1f");
    var SafeHtmlPipe = function () {
        function SafeHtmlPipe(sanitizer) {
            this.sanitizer = sanitizer;
        }
        SafeHtmlPipe.prototype.transform = function (value) {
            // return value;
            return this.sanitizer.bypassSecurityTrustHtml(value);
        };
        return SafeHtmlPipe;
    }();
    SafeHtmlPipe.decorators = [{ type: core_1.Pipe, args: [{ name: 'safeHtml' }] }];
    /** @nocollapse */
    SafeHtmlPipe.ctorParameters = function () {
        return [{ type: platform_browser_1.DomSanitizer }];
    };
    exports.SafeHtmlPipe = SafeHtmlPipe;

});
$__System.registerDynamic("20", ["c", "b", "12", "21", "18", "19", "1a", "1b", "13", "d", "1e"], true, function ($__require, exports, module) {
    "use strict";

    var global = this || self,
        GLOBAL = global;
    Object.defineProperty(exports, "__esModule", { value: true });
    var core_1 = $__require("c");
    var tree_component_1 = $__require("b");
    var tree_internal_component_1 = $__require("12");
    var common_1 = $__require("21");
    var node_draggable_directive_1 = $__require("18");
    var node_draggable_service_1 = $__require("19");
    var node_editable_directive_1 = $__require("1a");
    var node_menu_component_1 = $__require("1b");
    var node_menu_service_1 = $__require("13");
    var tree_service_1 = $__require("d");
    var safe_html_pipe_1 = $__require("1e");
    var TreeModule = function () {
        function TreeModule() {}
        return TreeModule;
    }();
    TreeModule.decorators = [{ type: core_1.NgModule, args: [{
            imports: [common_1.CommonModule],
            declarations: [node_draggable_directive_1.NodeDraggableDirective, tree_component_1.TreeComponent, node_editable_directive_1.NodeEditableDirective, node_menu_component_1.NodeMenuComponent, tree_internal_component_1.TreeInternalComponent, safe_html_pipe_1.SafeHtmlPipe],
            exports: [tree_component_1.TreeComponent],
            providers: [node_draggable_service_1.NodeDraggableService, node_menu_service_1.NodeMenuService, tree_service_1.TreeService]
        }] }];
    /** @nocollapse */
    TreeModule.ctorParameters = function () {
        return [];
    };
    exports.TreeModule = TreeModule;

});
$__System.registerDynamic("a", ["10", "e", "1c", "b", "20"], true, function ($__require, exports, module) {
  "use strict";

  var global = this || self,
      GLOBAL = global;
  Object.defineProperty(exports, "__esModule", { value: true });
  var tree_types_1 = $__require("10");
  exports.TreeModelSettings = tree_types_1.TreeModelSettings;
  exports.FoldingType = tree_types_1.FoldingType;
  var tree_1 = $__require("e");
  exports.Tree = tree_1.Tree;
  var tree_events_1 = $__require("1c");
  exports.NodeEvent = tree_events_1.NodeEvent;
  exports.NodeCreatedEvent = tree_events_1.NodeCreatedEvent;
  exports.NodeRemovedEvent = tree_events_1.NodeRemovedEvent;
  exports.NodeRenamedEvent = tree_events_1.NodeRenamedEvent;
  exports.NodeMovedEvent = tree_events_1.NodeMovedEvent;
  exports.NodeSelectedEvent = tree_events_1.NodeSelectedEvent;
  exports.NodeExpandedEvent = tree_events_1.NodeExpandedEvent;
  exports.NodeCollapsedEvent = tree_events_1.NodeCollapsedEvent;
  exports.NodeDestructiveEvent = tree_events_1.NodeDestructiveEvent;
  var tree_component_1 = $__require("b");
  exports.TreeComponent = tree_component_1.TreeComponent;
  var tree_module_1 = $__require("20");
  exports.TreeModule = tree_module_1.TreeModule;

});
})
(function(factory) {
  if (typeof define == 'function' && define.amd)
    define(["@angular/common","@angular/core","@angular/platform-browser","rxjs/Rx"], factory);
  else if (typeof module == 'object' && module.exports && typeof require == 'function')
    module.exports = factory(require("@angular/common"), require("@angular/core"), require("@angular/platform-browser"), require("rxjs/Rx"));
  else
    throw new Error("Module must be loaded as AMD or CommonJS");
});
//# sourceMappingURL=ng2-tree.umd.js.map